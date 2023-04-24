using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaArtemis.Controllers
{
    public class ProblemaController : Controller
    {
        // GET: Problema
        private Problema objProblema = new Problema();
        private Cliente objCliente = new Cliente();

        public ActionResult Index(int id)
        {
            return View(objProblema.Obtener(id));
        }

        public ActionResult PublicarProblema()
        {
            // Inicializar el modelo con la fecha actual
            objProblema.Fecha_Inicio = DateTime.Now;

            return View(objProblema);
        }

        public ActionResult Agregar(int id = 0)
        {
            ViewBag.TCliente = objCliente.Listar();

            return View(id == 0 ? new Problema() : objProblema.Obtener(id));
        }



    }
}