using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaArtemis.Controllers
{
    public class TecnicoController : Controller
    {
        //private Tecnico objtecnico = new Tecnico();
        private Servicio objservicio = new Servicio();


        public ActionResult Index()
        {
            return View();

        }

        public ActionResult TrabajosDisponibles()
        {
            return View(objservicio.Listar());
        }

        public ActionResult ListarMisServicios()
        {
            return View();
        }


        public ActionResult VisualizarCalificacion1()
        {
            return View();

        }


        public ActionResult VerDetalles()
        {
            return View();

        }

        public ActionResult Guardar(Tecnico model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Tecnico/Index");
            }
            else
            {
                return View("~/Tecnico/TrabajosDísponibles");
            }
        }


      

    }
}
