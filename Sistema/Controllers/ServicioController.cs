using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaArtemis.Controllers
{
    public class ServicioController : Controller
    {
     
        //private Servicio objServicio = new Servicio();
        //private Tecnico objTecnico= new Tecnico();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Guardar(Servicio model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Tecnico/Index");// ~/Miembro_Proyecto/Index"
            }
            else
            {
                return View("~/Tecnico/TrabajosDisponibles"); //Agregar
            }
        }

    }
}