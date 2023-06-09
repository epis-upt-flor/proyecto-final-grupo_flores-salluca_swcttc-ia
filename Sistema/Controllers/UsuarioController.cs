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
       /* Estas líneas de código están creando instancias de las clases `Usuario` y `Tipo_Usuario` y
       asignándolas a las variables `objUsuario` y `objTipo` respectivamente*/

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


        /// <summary>Esta función de C# devuelve una vista de un objeto de usuario con un ID especificado.</summary>
        /// <returns>El método está devolviendo una vista con los datos obtenidos del método `Obtener` del objeto`objUsuario`, el cual toma un parámetro `id`.
        
        public ActionResult Visualizar(int id)
        {
            return View(objUsuario.Obtener(id));
        }

        
        /// <summary>
        /// Esta es una función de C# que toma un parámetro de cadena "criterio" y devuelve un
        /// ActionResult. que se utiliza para buscar o filtrar datos.
        /// </summary>
        
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objUsuario.Listar() : objUsuario.Buscar(criterio));

        }


        /// <summary>Esta función devuelve una vista para agregar un nuevo usuario o editar un usuario existente y completa una lista desplegable con datos de otro objeto.
        /// </summary>
        /// <returns>El método "Agregar" devuelve una vista con un objeto ViewBag que contiene una lista de elementos para completar una lista desplegable y una nueva instancia de la clase "Usuario" o una instancia existente recuperada del objeto "objUsuario" según el valor del parámetro "id".
        /// </returns>
        public ActionResult Agregar(int id = 0)
        {
            ViewBag.Tipo = objTipo.Listar();//llenar el combox
            return View(id == 0 ? new Usuario() : objUsuario.Obtener(id));
        }

        /// <summary> La función guarda un modelo de usuario y redirige a la página de índice si el estado del modelo es válido. </summary>
        /// <returns>Si ModelState es válido, el método devuelve una redirección a la página de índice del controlador Usuario. Si ModelState no es válido, el método devuelve una vista para la acción agregar del controlador Usuario</returns>
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
