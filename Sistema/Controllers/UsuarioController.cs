using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaArtemis.Controllers
{
    public class UsuarioController : Controller
    {
       /* instancias de las clases `Usuario` `Tipo_Usuario` y asignándolas a las variables */
        private Usuario objUsuario = new Usuario();
        private Tipo_Usuario objTipo = new Tipo_Usuario();
               
        public ActionResult Index(string criterio)
        {
            if (criterio == null || criterio == "")
            {
                return View(objUsuario.Listar());
            }
            else
            {
                return View(objUsuario.Buscar(criterio));
            }
        }

        /// <returns>El método "Agregar" devuelve una vista con un objeto ViewBag que contiene una lista de elementos para completar una lista desplegable y una nueva instancia de la clase "Usuario" o una instancia existente recuperada del objeto "objUsuario" según el valor del parámetro "id".</returns>
        public ActionResult Agregar(int id = 0)
        {
            ViewBag.Tipo = objTipo.Listar();//llenar el combox
            return View(id == 0 ? new Usuario() : objUsuario.Obtener(id));
        }

        /// <summary> La función guarda un modelo de usuario y redirige a la página de índice si el estado del modelo es válido. </summary>        
        public ActionResult Guardar(Usuario model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Usuario/index");
            }
            else
            {
                return View("~/Usuario/Agregar");
            }
        }

        /// <summary> Esta función elimina un usuario con una identificación específica y redirige a la página del usuario. </summary>
        /// <returns>El método devuelve una redirección a la página "Usuario". </returns>
        public ActionResult Eliminar(int id)
        {
            objUsuario.Id_Usuario = id;
            objUsuario.Eliminar();
            return Redirect("~/Usuario");
        }
    }
}
