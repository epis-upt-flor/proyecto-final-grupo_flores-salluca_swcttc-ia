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
        // GET: RecAPI

        public async Task<ActionResult> IndexAsync(string Descripcion)
        {
            using (HttpClient client = new HttpClient())
            {
                string texto = Uri.EscapeDataString(Descripcion ?? string.Empty);
                string url = "http://127.0.0.1:8000/recomendar?texto=" + texto;
                HttpResponseMessage response = await client.GetAsync(url);

                //// Mostrar ventana emergente con el valor de Descripcion
                //string script = $"alert('{texto}');";
                //ViewBag.Script = script;

                if (response.IsSuccessStatusCode)
                {
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

            // Dividir la cadena result por comas
            string[] numerosStr = result.Split(',');

            // Convertir cada elemento de la matriz en un número entero y agregarlo a la lista
            foreach (string numeroStr in numerosStr)
            {
                int numero;
                if (int.TryParse(numeroStr, out numero))
                {
                    numeros.Add(numero);
                }
            }
            return numeros;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}