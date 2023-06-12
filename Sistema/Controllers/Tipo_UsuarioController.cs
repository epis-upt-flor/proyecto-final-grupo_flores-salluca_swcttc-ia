using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace SistemaArtemis.Controllers
{
    public class Tipo_UsuarioController : Controller
    {
        /* Creando una nueva instancia de la clase `Tipo_Usuario` y asignándola al campo privado
        `objTipoUsuario`*/
        private Tipo_Usuario objTipoUsuario = new Tipo_Usuario();
       
        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objTipoUsuario.Listar());
            }
            else
            {
                return View(objTipoUsuario.Buscar(criterio));
            }
        }

        public ActionResult Agregar(int id = 0)
        {
            return View(id == 0 ? new Tipo_Usuario() : objTipoUsuario.Obtener(id));
        }

        public ActionResult Guardar(Tipo_Usuario model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Tipo_Usuario/index");
            }
            else
            {
                return View("~/Tipo_Usuario/Agregar");
            }
        }

        public ActionResult Eliminar(int id)
        {
            objTipoUsuario.Id_Tipo_Usuario = id;
            objTipoUsuario.Eliminar();
            return Redirect("~/Tipo_Usuario");
        }
        
    }
}