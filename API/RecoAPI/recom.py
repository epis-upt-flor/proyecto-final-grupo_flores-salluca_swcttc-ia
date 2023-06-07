import sqlalchemy
from sqlalchemy import create_engine
import pyodbc
import pandas as pd
import numpy as np
import nltk
import inflect
from nltk.tokenize import word_tokenize
from nltk.corpus import stopwords
from nltk.corpus import wordnet
from fastapi import FastAPI
from collections import Counter

app = FastAPI()

#Funcion para conectar con BD
def con():
    server = "."
    namedatabase = 'ArtemisBD'

    engine = create_engine("mssql+pyodbc://@"+server+"/"+namedatabase+"?driver=ODBC+Driver+17+for+SQL+Server")
    con = engine.connect()
    return con

#Funcion llamar Sentecias SQL
def SenSql():
    query_datos = "SELECT T.Id_Tecnico, T.Nombre, T.Apellido, T.Telefono, T.Id_Especialidad, E.Nombre, T.Id_Estado_Tecnico, T.Id_Usuario,  ET.Descripcion, TE.Id_Tipo_Especialidad, TE.TipoEspecialidad, RT.Id FROM Tecnico AS T INNER JOIN Especialidad AS E ON T.Id_Especialidad = E.Id_Especialidad INNER JOIN Estado_Tecnico AS ET ON T.Id_Estado_Tecnico = ET.Id_Estado_Tecnico INNER JOIN Tipo_Especialidad AS TE ON TE.Id_Especialidad = E.Id_Especialidad INNER JOIN RTecnico_TipoEspecialidad AS RT ON RT.Id_Tecnico = T.Id_Tecnico AND RT.Id_Tipo_Especialidad = TE.Id_Tipo_Especialidad WHERE T.Id_Estado_Tecnico = 1 AND RT.Id_Tecnico = T.Id_Tecnico AND T.Id_Especialidad = E.Id_Especialidad AND E.Id_Especialidad = TE.Id_Especialidad"
    return query_datos

#Funcion Convertir Sentencia SQL a array
def SenSqlArray(query_datos, con):
    df_datos = pd.read_sql(query_datos, con)
    datos = np.array(df_datos)
    return datos

@app.get("/recomendar")
def recomendar(texto: str):
    con_obj = con()
    query_datos = SenSql()
    datos = SenSqlArray(query_datos, con_obj)
    #texto = "Necesito alguien para instalaciones eléctricas"
    palabras = word_tokenize(texto.lower())
    stop_words = set(stopwords.words('spanish'))
    palabras_filtradas = [palabra for palabra in palabras if not palabra in stop_words]
    palabras_claves = set(palabras_filtradas)
    inflector = inflect.engine()
    recomendaciones = []

    for trabajo in datos:
        descripcion = trabajo[10]
        palabras_trabajo = word_tokenize(descripcion.lower())
        palabras_filtradas_trabajo = [palabra for palabra in palabras_trabajo if not palabra in stop_words]
        palabras_claves_trabajo = set(palabras_filtradas_trabajo)

        for palabra in palabras_filtradas_trabajo:
            singular = inflector.singular_noun(palabra)
            plural = inflector.plural_noun(palabra)
            sinonimos = set()
            for synset in wordnet.synsets(palabra):
                for lemma in synset.lemmas():
                    sinonimos.add(lemma.name().lower())
                    
            palabras_claves_trabajo.update(sinonimos)

            if singular == False and plural == False:
                palabras_claves_trabajo.add(palabra)
            else:
                palabras_claves_trabajo.add(singular if singular else plural)

        if len(palabras_claves.intersection(palabras_claves_trabajo)) >= 1:
            recomendaciones.append(trabajo[0])

    # Realizar la validación de números repetidos y ordenar por repeticiones    
    #repeticiones = Counter(recomendaciones)
    #recomendaciones_ordenadas = sorted(repeticiones.items(), key=lambda x: x[1], reverse=True)
    #recomendaciones_unicas = [num for num, _ in recomendaciones_ordenadas]



    # Realizar la validación de números repetidos
    repeticiones = Counter(recomendaciones)
    recomendaciones_ordenadas = sorted(repeticiones, key=lambda x: repeticiones[x], reverse=True)

    # Eliminar números repetidos en el array final
    recomendaciones_unicas = []
    for num in recomendaciones_ordenadas:
        if num not in recomendaciones_unicas:
            recomendaciones_unicas.append(num)
    
    return recomendaciones_unicas
    #return recomendaciones
    #return {"recomendaciones": recomendaciones}

       
    
