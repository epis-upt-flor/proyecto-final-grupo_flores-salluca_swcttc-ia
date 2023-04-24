using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaArtemis.Controllers
{
    public class HomeController : Controller
    {

        private Servicio objServicio = new Servicio();
        private Tecnico objTecnico = new Tecnico();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmpleosDisponibles()
        {
            return View(objServicio.Listar());
        }

        public ActionResult TecnicosDisponibles()
        {
            return View(objTecnico.Listar());
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}