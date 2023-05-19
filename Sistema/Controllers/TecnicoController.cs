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
    public class TecnicoController : Controller
    {
        private Tecnico objtecnico = new Tecnico();
        private Servicio objservicio = new Servicio();
        private Especialidad objTipo= new Especialidad();
        private Model1 db = new Model1();


        public ActionResult Index()
        {
            return View();

        }

        /// <summary>
        /// Esta función devuelve una vista del perfil de un técnico en función de su ID, o redirige a la página
        /// de inicio del técnico si no se proporciona una ID.
        /// </summary>
        /// <param name="id">El parámetro id es un número entero que se utiliza para identificar un registro u
        /// objeto específico en el sistema. En este caso, se utiliza para recuperar la información de perfil de
        /// un técnico con la identificación especificada. Si no se proporciona la identificación o es igual a
        /// 0, el método redirige al usuario a la</param>
        /// <returns>
        /// Si el parámetro `id` no es igual a 0, el método devuelve una vista con los datos obtenidos del
        /// método `Obtener` del objeto `objtecnico` para el `id` especificado. Si `id` es igual a 0, el método
        /// redirige a la acción `Tecnico`.
        /// </returns>
        public ActionResult Perfil(int id = 0)
        {
            ViewBag.Tipo=objTipo.Listar();
            if (id != 0)
                return View(objtecnico.Obtener(id));
            else
                return Redirect("~/Tecnico");
            //return View();
        }

        /// <summary>
        /// La función guarda el perfil de un técnico y lo redirige a la página de índice si el estado del
        /// modelo es válido; de lo contrario, regresa a la página de perfil.
        /// </summary>
        /// <param name="Tecnico">Es un modelo o clase que representa a un técnico en el sistema. Es probable
        /// que el código forme parte de una aplicación web que permite a los usuarios crear y administrar
        /// perfiles de técnicos. El método "Guardar" se utiliza para guardar la información del técnico en una
        /// base de datos u otro medio de almacenamiento. El código comprueba si el modelo</param>
        /// <returns>
        /// Si ModelState es válido, el método devolverá una redirección a la vista de índice del controlador
        /// Tecnico. Si ModelState no es válido, el método devolverá la vista Perfil del controlador Tecnico.
        /// </returns>
        public ActionResult Guardar(Tecnico model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Tecnico/Index");
            }
            else
            {
                model.Guardar();
                return View("~/Tecnico/Perfil");
            }
        }


       /// <summary>
       /// Esta función devuelve una vista con una lista de trabajos disponibles.
       /// </summary>
       /// <returns>
       /// El método `TrabajosDisponibles` está devolviendo una `Vista` con el resultado de llamar al
       /// método `Listar` del objeto `objservicio`.
       /// </returns>
        public ActionResult TrabajosDisponibles()
        {
            return View(objservicio.Listar());
        }

        /// <summary>
        /// Esta función devuelve una vista de los servicios pertenecientes a un usuario específico.
        /// </summary>
        /// <param name="id">El parámetro "id" es un valor entero que se utiliza para identificar a un
        /// usuario específico. Se pasa al método "MisServicios" del objeto "objservicio" para recuperar una
        /// lista de servicios asociados a ese usuario. El método devuelve la lista de servicios a la vista
        /// "Lista</param>
        /// <returns>
        /// El método `ListarMisServicios` está devolviendo una vista con los datos obtenidos del método
        /// `MisServicios` del objeto `objservicio`, que toma un parámetro `id`.
        /// </returns>
        public ActionResult ListarMisServicios(int id)
        {
            return View(objservicio.MisServicios(id));
        }


        /// <summary>
        /// Esta función devuelve una vista para visualizar un determinado tipo de calificación.
        /// </summary>
        /// <returns>
        /// El método está devolviendo un resultado Ver.
        /// </returns>
        public ActionResult VisualizarCalificacion1()
        {
            return View();

        }


        public ActionResult VerDetalles()
        {
            return View();

        }

    








        //Tecnico
        // GET: Tecnico/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnico personal = db.Tecnico.Find(id);
            if (personal == null)
            {
                return HttpNotFound();
            }
            return View(personal);
        }

        // GET: Tecnico/Create
        public ActionResult Create()
        {
            ViewBag.Id_Estado_Tecnico = new SelectList(db.Estado_Tecnico, "Id_Estado_Tecnico", "Descripcion");
            ViewBag.Id_Usuario = new SelectList(db.Usuario, "Id_Usuario", "Correo");
            
            return View();
        }



        // POST: Tecnico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Tecnico,Nombre,Apellido,Telefono,Especialidad,Id_Estado_Tecnico,Id_Usuario")] Tecnico tecnico)
        {
            if (ModelState.IsValid)
            {
                db.Tecnico.Add(tecnico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Estado_Tecnico = new SelectList(db.Estado_Tecnico, "Id_Estado_Tecnico", "Descripcion", tecnico.Id_Estado_Tecnico);
            ViewBag.Id_Usuario = new SelectList(db.Usuario, "Id_Usuario", "Correo", tecnico.Id_Usuario);
            return View(tecnico);
        }

        // GET: Tecnico/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnico tecnico = db.Tecnico.Find(id);
            if (tecnico == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Estado_Tecnico = new SelectList(db.Estado_Tecnico, "Id_Estado_Tecnico", "Descripcion", tecnico.Id_Estado_Tecnico);
            ViewBag.Id_Usuario = new SelectList(db.Usuario, "Id_Usuario", "Correo", tecnico.Id_Usuario);
            return View(tecnico);
        }

        // POST: Tecnico/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Tecnico,Nombre,Apellido,Telefono,Especialidad,Id_Estado_Tecnico,Id_Usuario")] Tecnico tecnico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tecnico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Estado_Tecnico = new SelectList(db.Estado_Tecnico, "Id_Estado_Tecnico", "Descripcion", tecnico.Id_Estado_Tecnico);
            ViewBag.Id_Usuario = new SelectList(db.Usuario, "Id_Usuario", "Correo", tecnico.Id_Usuario);
            return View(tecnico);
        }

        // GET: Tecnico/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnico tecnico = db.Tecnico.Find(id);
            if (tecnico == null)
            {
                return HttpNotFound();
            }
            return View(tecnico);
        }

        // POST: Tecnico/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tecnico tecnico = db.Tecnico.Find(id);
            db.Tecnico.Remove(tecnico);
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
