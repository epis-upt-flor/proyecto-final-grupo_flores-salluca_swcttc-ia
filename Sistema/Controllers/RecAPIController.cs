/*
Versión: 1.0
Descripción: RecAPIController.cs Representada para gestionar recomendacion
Para el caso de uso: Gestionar Recomendacion

Fecha de creación: [07/08/2023]
Creado por: [DJFN]

Última modificación: [11/11/2023]
Modificado por: [JFSV]
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaArtemis.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace SistemaArtemis.Controllers
{
    public class RecAPIController : Controller
    {
        private Tecnico objtecnico = new Tecnico();
        private Especialidad objespecialidad = new Especialidad();

        // GET: RecAPI
        public async Task<ActionResult> IndexAsync(string Descripcion)
        {
            using (HttpClient client = new HttpClient())
            {
                string texto = Uri.EscapeDataString(Descripcion ?? string.Empty);
                string url = "http://127.0.0.1:8000/recomendar?texto=" + texto;
                //string url = "http://127.0.0.1:8000/recomendar_mejorado?texto=" + texto;
                HttpResponseMessage response = await client.GetAsync(url);

                //// Mostrar ventana emergente con el valor de Descripcion
                //string script = $"alert('{texto}');";
                //ViewBag.Script = script;
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.tipo = objespecialidad.Listar();
                    string result = await response.Content.ReadAsStringAsync();
                    List<int> numeros = ParseResultToList(result);
                    return View(objtecnico.ListarRecomendado(numeros));
                }
                else
                {
                    return View("Error");
                }
            }
        }

        private List<int> ParseResultToList(string result)
        {          
            List<int> numeros = new List<int>();
            // Remover los caracteres "[" y "]"
            result = result.Replace("[", "").Replace("]", "");
            // Dividir la cadena result por comas
            string[] numerosStr = result.Split(',');
            // Convertir cada elemento de la matriz en un número entero y agregarlo a la lista
            foreach (string numeroStr in numerosStr)
            {
                if (int.TryParse(numeroStr, out int numero))
                {
                    numeros.Add(numero);
                }
                else
                {
                    Console.WriteLine($"No se pudo convertir el elemento '{numeroStr}' a un número entero. Se omitirá este elemento.");
                }
            }

            return numeros;
        }

        public async Task<JsonResult> RecomendarEspecialidad(string Descripcion)
        //public async Task<List<string>> RecomendarEspecialidad(string Descripcion)
        {
            using (HttpClient client = new HttpClient())
            {
                string texto = Uri.EscapeDataString(Descripcion ?? string.Empty);
                string url = "http://127.0.0.1:8000/obtener_predicciones?texto=" + texto;
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    List<string> listaEspecialidades = ParseResultToLists(result);

                    //return Json(listaEspecialidades, JsonRequestBehavior.AllowGet);
                    ViewBag.LEspecialidad = listaEspecialidades;
                    return Json(listaEspecialidades);
                    //return listaEspecialidades;
                }
                else
                {
                    // Manejar el error devolviendo un mensaje de error como un elemento de la lista
                    //List<string> error = new List<string>() { "Error al obtener las especialidades" };
                    //return error;
                    // Manejar el error devolviendo un objeto JSON con un mensaje de error
                    return Json(new { error = "Error al obtener las especialidades" });
                    //return Json(new { error = "Error al obtener las especialidades" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        private List<string> ParseResultToLists(string result)
        {
            List<string> lista = new List<string>();
            // Remover los caracteres "[" y "]"
            result = result.Replace("[", "").Replace("]", "");
            // Dividir la cadena result por comas
            string[] elementos = result.Split(',');
            // Agregar cada elemento a la lista
            foreach (string elemento in elementos)
            {
                lista.Add(elemento.Trim());
            }

            return lista;
        }



    }
}