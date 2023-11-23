###
#Versión: 1.0
#Descripción: recom.py Representada para gestionar recomendacion
#Para el caso de uso: Gestionar Recomendacion

#Fecha de creación: [07/08/2023]
#Creado por: [DJFN]

#Última modificación: [11/11/2023]
#Modificado por: [JFSV]
###


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
from nltk.stem import WordNetLemmatizer


import re
import joblib

app = FastAPI()
nltk.download('punkt')
nltk.download('stopwords')
nltk.download('wordnet')

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

##Metodo coincidencias de palabras
@app.get("/recomendar_mejorado")
def recomendar_mejorado(texto: str):
    con_obj = con()
    query_datos = SenSql()
    datos = SenSqlArray(query_datos, con_obj)

    palabras_filtradas = filtrar_palabras(texto)
    palabras_claves = set(palabras_filtradas)

    inflector = inflect.engine()
    recomendaciones = []

    for trabajo in datos:
        descripcion = trabajo[10]
        palabras_filtradas_trabajo = filtrar_palabras(descripcion)
        palabras_claves_trabajo = set(palabras_filtradas_trabajo)

        lematizador = WordNetLemmatizer()
        palabras_claves_trabajo_lem = set(lematizador.lemmatize(palabra) for palabra in palabras_claves_trabajo)

        if palabras_claves.intersection(palabras_claves_trabajo_lem):
            recomendaciones.append((trabajo[0], len(palabras_claves.intersection(palabras_claves_trabajo_lem))))

    repeticiones = Counter(rec[0] for rec in recomendaciones)
    recomendaciones_unicas = sorted(set(recomendaciones), key=lambda x: repeticiones[x[0]], reverse=True)

    return [rec[0] for rec in recomendaciones_unicas]


def filtrar_palabras(texto):
    palabras = word_tokenize(texto.lower())
    stop_words = set(stopwords.words('spanish'))
    palabras_filtradas = [palabra for palabra in palabras if palabra not in stop_words]
    return palabras_filtradas


##Metodo coincidencias de palabras
@app.get("/obtener_predicciones")
def obtener_predicciones(texto: str):
    delimitadores = delimitador()

    # Cargar el modelo entrenado desde el archivo
    loaded_model = joblib.load('modelo_entrenado.pkl')
    # Crear una expresión regular que busca cualquiera de los delimitadores
    patron_delimitador = "|".join(map(re.escape, delimitadores))

    # Dividir el texto del usuario en partes separadas utilizando los delimitadores detectados
    partes_texto = re.split(patron_delimitador, texto)

    # Inicializar el array de predicciones
    predicciones = []

    # Realizar la predicción para cada parte del texto
    for parte in partes_texto:
        parte = parte.strip()
        if parte:
            prediccion = loaded_model.predict([parte])
            predicciones.append(prediccion[0])

    # Devolver el array de predicciones
    return predicciones
    
#Delimitadores
def delimitador():
    delimitadores = [
    r" y ",
    r", ",
    r" además de ",
    r" adicionalmente ",
    r" junto con ",
    r" dicho de otra forma, ",
    r" es decir, ",
    r" o sea, ",
    r" esto es, ",
    r" en otras palabras, ",
    r" a modo de ejemplo, ",
    r" a modo de ilustración, ",
    r" a título de ejemplo, ",
    r" como ejemplo, ",
    r" como muestra, ",
    r" como ilustración, ",
    r" como caso ilustrativo, ",
    r" para ejemplificar, ",
    r" a manera de ejemplo, ",
    r" para poner un ejemplo, ",
    r" por ejemplo, ",
    r" verbigracia, ",
    r" v.gr., ",
    r" p. ej., ",
    r" ej., ",
    r" entre ellos, ",
    r" incluyendo, ",
    r" tal como, ",
    r" así como, ",
    r" también, ",
    r" igualmente, ",
    r" asimismo, ",
    r" del mismo modo, ",
    r" de la misma manera, ",
    r" por otro lado, ",
    r" por otra parte, ",
    r" de igual manera, ",
    r" de manera similar, ",
    r" de forma similar, ",
    r" de igual modo, ",
    r" de forma análoga, ",
    r" al igual que, ",
    r" de manera análoga, ",
    r" en la misma línea, ",
    r" a semejanza de, ",
    r" a la par de, ",
    r" de la misma forma, ",
    r" al mismo tiempo, ",
    r" simultáneamente, ",
    r" a la vez, ",
    r" paralelamente, ",
    r" conjuntamente, ",
    r" correlativamente, ",
    r" de manera conjunta, ",
    r" de manera correlativa, ",
    r" a su vez, ",
    r" a continuación, ",
    r" seguidamente, ",
    r" posteriormente, ",
    r" luego, ",
    r" después, ",
    r" más tarde, ",
    r" en otro orden de cosas, ",
    r" en segundo lugar, ",
    r" en tercer lugar, ",
    r" en última instancia, ",
    r" por último, ",
    r" finalmente, ",
    r" en conclusión, ",
    r" en resumen, ",
    r" en síntesis, ",
    r" en suma, ",
    r" en definitiva, ",
    r" como resultado, ",
    r" por consiguiente, ",
    r" por lo tanto, ",
    r" así que, ",
    r" en consecuencia, ",
    r" como consecuencia, ",
    r" en virtud de eso, ",
    r" por eso, ",
    r" por ese motivo, ",
    r" por esa razón, ",
    r" de ahí que, ",
    r" de esta manera, ",
    r" de esta forma, ",
    r" en tal sentido, ",
    r" en ese caso, ",
    r" por ese motivo, ",
    r" en virtud de ello, ",
    r" por esa causa, ",
    r" así pues, ",
    r" entonces, ",
    r" así, ",
    r" por consiguiente, ",
    r" en ese sentido, ",
    r".",
    ]
    return delimitadores

