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
        `objTipoUsuario`. Esta instancia luego se usa en varios métodos de la clase
        `Tipo_UsuarioController` para realizar operaciones en los objetos `Tipo_Usuario`, como
        enumerarlos, buscarlos, agregarlos, editarlos y eliminarlos. */
        private Tipo_Usuario objTipoUsuario = new Tipo_Usuario();
        private Model1 db = new Model1();


        /// <summary>
        /// Esta función de C# devuelve una vista de todos los objetos TipoUsuario si no se proporciona
        /// ningún criterio de búsqueda, o una vista de los objetos TipoUsuario que coinciden con los
        /// criterios de búsqueda.
        /// </summary>
        /// <param name="criterio">un parámetro de cadena que se utiliza para filtrar los resultados de
        /// una consulta de búsqueda. Si es nulo o está vacío, el método devuelve todos los registros de
        /// la base de datos. Si tiene un valor, el método devuelve solo los registros que coinciden con
        /// los criterios de búsqueda.</param>
        /// <returns>
        /// El método está devolviendo una vista. La vista que se devuelve depende del valor del
        /// parámetro "criterio". Si el parámetro es nulo o una cadena vacía, el método devuelve una
        /// vista con una lista de todos los objetos TipoUsuario. Si el parámetro tiene un valor, el
        /// método devuelve una vista con una lista de objetos TipoUsuario que coinciden con el criterio
        /// de búsqueda.
        /// </returns>
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




        /// <summary>
        /// Esta función de C# devuelve una vista con los datos de un tipo de usuario específico
        /// identificado por su ID.
        /// </summary>
        /// <param name="id">El parámetro "id" es un valor entero que representa el identificador de un
        /// objeto o registro específico en el sistema. En este caso, se utiliza para recuperar un objeto
        /// TipoUsuario específico de la base de datos y pasarlo al método View para su
        /// renderizado.</param>
        /// <returns>
        /// El método `Visualizar` está devolviendo una vista con los datos obtenidos del método `Obtener`
        /// del objeto `objTipoUsuario`, que toma un parámetro `id`.
        /// </returns>
        public ActionResult Visualizar(int id)
        {
            return View(objTipoUsuario.Obtener(id));
        }



        /// <summary>
        /// Esta función de C# devuelve una vista para agregar un nuevo objeto "Tipo_Usuario" o editar uno
        /// existente según la ID proporcionada.
        /// </summary>
        /// <param name="id">un parámetro entero opcional con un valor predeterminado de 0. Se utiliza para
        /// identificar el tipo de usuario que se agregará o editará. Si es 0, se está agregando un nuevo
        /// tipo de usuario, de lo contrario, se está editando un tipo de usuario existente.</param>
        /// <returns>
        /// El método `Agregar` devuelve un objeto `ViewResult`. Si el parámetro `id` es igual a 0,
        /// devuelve una vista con una nueva instancia de la clase `Tipo_Usuario`. En caso contrario,
        /// devuelve una vista con el objeto `Tipo_Usuario` obtenido del método `Obtener` del objeto
        /// `objTipoUsuario`.
        /// </returns>
        public ActionResult Agregar(int id = 0)
        {
            return View(id == 0 ? new Tipo_Usuario() : objTipoUsuario.Obtener(id));
        }




        /// <summary>
        /// Esta función guarda un modelo Tipo_Usuario y redirige a la página de índice si el modelo es válido,
        /// de lo contrario devuelve la vista Agregar.
        /// </summary>
        /// <param name="Tipo_Usuario">Es una clase modelo que representa un tipo de usuario en el sistema. El
        /// código usa este modelo para guardar una nueva instancia del tipo de usuario en la base de
        /// datos.</param>
        /// <returns>
        /// Si el ModelState es válido, el método devolverá una redirección a la página de índice del
        /// controlador Tipo_Usuario. Si ModelState no es válido, el método devolverá una vista para la acción
        /// Agregar del controlador Tipo_Usuario.
        /// </returns>
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


        

      /// <summary>
      /// Esta función elimina un registro de un tipo específico de usuario y redirige a la página del
      /// tipo de usuario.
      /// </summary>
      /// <param name="id">un número entero que representa el ID del Tipo_Usuario (Tipo de Usuario) que
      /// necesita ser eliminado.</param>
      /// <returns>
      /// El método devuelve una redirección a la página "User_Type".
      /// </returns>
        public ActionResult Eliminar(int id)
        {
            objTipoUsuario.Id_Tipo_Usuario = id;
            objTipoUsuario.Eliminar();
            return Redirect("~/Tipo_Usuario");
        }


       
        public ActionResult Create()
        {
            return View();
        }



        // POST: Tipo_Usuario/Create
        /// <summary>
        /// Esta es una función de C# que crea un nuevo registro en la tabla Tipo_Usuario y redirige a la
        /// página de índice si el estado del modelo es válido.
        /// </summary>
        /// <param name="Tipo_Usuario">Esta es una clase modelo que representa un tipo de usuario en el
        /// sistema.</param>
        /// <returns>
        /// Si el estado del modelo es válido, el método agregará el nuevo objeto "tipo_usuario" a la base de
        /// datos y lo redirigirá a la acción "Index". Si el estado del modelo no es válido devolverá a la
        /// vista el objeto "tipo_usuario".
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Tipo_Usuario,Nombre")] Tipo_Usuario tipo_usuario)
        {
            if (ModelState.IsValid)
            {
                db.Tipo_Usuario.Add(tipo_usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipo_usuario);
        }


        // GET: Tipo_Usuario/Edit
        /// <summary>
        /// Esta función recupera un objeto Tipo_Usuario específico de la base de datos y lo devuelve a la
        /// vista Editar para su edición.
        /// </summary>
        /// <param name="id">El parámetro id es un entero que representa el identificador único de un objeto
        /// Tipo_Usuario en la base de datos. Se utiliza para recuperar el objeto específico que necesita ser
        /// editado de la base de datos. Si el parámetro id es nulo o no coincide con ningún objeto en la
        /// base de datos, el método devuelve una respuesta de error</param>
        /// <returns>
        /// El método está devolviendo una vista para editar un objeto Tipo_Usuario con la identificación
        /// especificada. Si el id es nulo o no se encuentra el objeto Tipo_Usuario con el id especificado,
        /// devuelve un error HTTP 400 Bad Request o HTTP 404 Not Found, respectivamente.
        /// </returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Usuario tipo_usuario = db.Tipo_Usuario.Find(id);
            if (tipo_usuario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_usuario);
        }

        // POST: Tipo_Usuario/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Tipo_Usuario,Nombre")] Tipo_Usuario tipo_usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipo_usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipo_usuario);
        }

        // GET: Tipo_Usuario/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tipo_Usuario tipo_usuario = db.Tipo_Usuario.Find(id);
            if (tipo_usuario == null)
            {
                return HttpNotFound();
            }
            return View(tipo_usuario);
        }

        // POST: Tipo_Usuario/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tipo_Usuario tipo_usuario = db.Tipo_Usuario.Find(id);
            db.Tipo_Usuario.Remove(tipo_usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}