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
        private Problema objproblema= new Problema();
        private Servicio objServicio = new Servicio();
        //private Tecnico objTecnico= new Tecnico();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return View(objproblema.ListProblem());
        }

        public ActionResult Create(int id=0)
        {
            var problemas = new Problema().Listar(id);
            ViewBag.Prob = problemas;

            return View(id==0 ? new Servicio() : objServicio.Obtener(id));
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