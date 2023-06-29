using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaArtemis.Controllers
{
    public class Imagen_ProblemaController : Controller
    {
        Model1 db = new Model1();
        private Imagen_Problema objImagenProblema = new Imagen_Problema();
        // GET: Imagen_Problema
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SubirImagenProblema(int id)
        {
            ViewBag.Id_Problema = id;
            return View(id);
        }

        [HttpPost]
        public ActionResult GuardarImagen(int id_Problema, HttpPostedFileBase[] fileInput)
        {
            Imagen_Problema.GuardarImagen(id_Problema, fileInput);
            return RedirectToAction("SubirImagenProblema", new { id = id_Problema });
        }

        public ActionResult VisualizarImagenes(int id_Problema)
        {
            var imagenes = objImagenProblema.VisualizarImagenes(id_Problema);
            return View(imagenes);
        }

    }
}