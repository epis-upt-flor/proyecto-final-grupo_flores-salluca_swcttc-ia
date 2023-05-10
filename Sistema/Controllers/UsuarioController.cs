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
       asignándolas a las variables `objUsuario` y `objTipo` respectivamente. Estas instancias se
       utilizan en toda la clase `UsuarioController` para interactuar con la base de datos y
       realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en las entidades `Usuario` y
       `Tipo_Usuario`. */
        private Usuario objUsuario = new Usuario();
        private Tipo_Usuario objTipo = new Tipo_Usuario();

        
        /// <summary>
        /// Esta función de C# devuelve una vista de todos los usuarios o una lista filtrada de usuarios
        /// según un criterio de búsqueda.
        /// </summary>
        /// <param name="criterio">un parámetro de cadena que se utiliza para filtrar la lista de usuarios.
        /// Si es nulo o está vacío, el método devuelve la lista completa de usuarios. Si tiene un valor, el
        /// método devuelve solo los usuarios que coinciden con los criterios especificados.</param>
        /// <returns>
        /// Si el parámetro `criterio` es nulo o una cadena vacía, el método devuelve el resultado de llamar
        /// al método `Listar` del objeto `objUsuario`. En caso contrario, devuelve el resultado de llamar al
        /// método `Buscar` del objeto `objUsuario` con el parámetro `criterio`. En ambos casos, el método
        /// devuelve una vista.
        /// </returns>
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

  
       /// <summary>
       /// Esta función de C# devuelve una vista de un objeto de usuario con un ID especificado.
       /// </summary>
       /// <param name="id">El parámetro "id" es un valor entero que representa el identificador único
       /// de un usuario. Este parámetro se utiliza para recuperar la información del usuario de la base
       /// de datos y pasarla a la vista para su visualización.</param>
       /// <returns>
       /// El método está devolviendo una vista con los datos obtenidos del método `Obtener` del objeto
       /// `objUsuario`, el cual toma un parámetro `id`.
       /// </returns>
        public ActionResult Visualizar(int id)
        {
            return View(objUsuario.Obtener(id));
        }

        
        /// <summary>
        /// Esta es una función de C# que toma un parámetro de cadena "criterio" y devuelve un
        /// ActionResult. Es probable que se utilice para buscar o filtrar datos.
        /// </summary>
        /// <param name="criterio">"criterio" es un parámetro de tipo string que se utiliza para pasar
        /// un criterio de búsqueda a un método llamado "Buscar" en un controlador ASP.NET MVC. Se
        /// espera que el método use este criterio para buscar algunos datos y devolver los resultados a
        /// la vista.</param>
        public ActionResult Buscar(string criterio)
        {
            return View(criterio == null || criterio == "" ? objUsuario.Listar() : objUsuario.Buscar(criterio));

        }


        /// <summary>
        /// Esta función devuelve una vista para agregar un nuevo usuario o editar un usuario existente y
        /// completa una lista desplegable con datos de otro objeto.
        /// </summary>
        /// <param name="id">El parámetro id es un parámetro entero opcional con un valor predeterminado de 0.
        /// Se utiliza para identificar a un usuario específico en el sistema. Si no se proporciona una
        /// identificación, el método creará un nuevo objeto de usuario. Si se proporciona una identificación,
        /// el método recuperará el objeto de usuario con esa identificación de</param>
        /// <returns>
        /// El método "Agregar" devuelve una vista con un objeto ViewBag que contiene una lista de elementos
        /// para completar una lista desplegable y una nueva instancia de la clase "Usuario" o una instancia
        /// existente recuperada del objeto "objUsuario" según el valor del parámetro "id".
        /// </returns>
        public ActionResult Agregar(int id = 0)
        {
            ViewBag.Tipo = objTipo.Listar();//llenar el combox
            return View(id == 0 ? new Usuario() : objUsuario.Obtener(id));
        }

        /// <summary>
        /// La función guarda un modelo de usuario y redirige a la página de índice si el estado del
        /// modelo es válido; de lo contrario, devuelve la vista de adición.
        /// </summary>
        /// <param name="Usuario">Es una clase modelo que representa a un usuario en la aplicación.
        /// Contiene propiedades y métodos relacionados con los datos y las operaciones del
        /// usuario.</param>
        /// <returns>
        /// Si ModelState es válido, el método devuelve una redirección a la página de índice del
        /// controlador Usuario. Si ModelState no es válido, el método devuelve una vista para la acción
        /// Agregar del controlador Usuario.
        /// </returns>
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

        /// <summary>
        /// Esta función elimina un usuario con una identificación específica y redirige a la página del
        /// usuario.
        /// </summary>
        /// <param name="id">El parámetro id es un número entero que representa el identificador único de un
        /// usuario que debe eliminarse del sistema.</param>
        /// <returns>
        /// El método devuelve una redirección a la página "Usuario".
        /// </returns>
        public ActionResult Eliminar(int id)
        {
            objUsuario.Id_Usuario = id;
            objUsuario.Eliminar();
            return Redirect("~/Usuario");
        }
    }
}
