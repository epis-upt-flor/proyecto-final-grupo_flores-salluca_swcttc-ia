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
        private Model1 db = new Model1();


        public ActionResult Index()
        {
            return View();

        }

        public ActionResult Perfil(int id = 0)
        {
            if (id != 0)
                return View(objtecnico.Obtener(id));
            else
                return Redirect("~/Tecnico");
            //return View();
        }

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


        public ActionResult TrabajosDisponibles()
        {
            return View(objservicio.Listar());
        }

        public ActionResult ListarMisServicios()
        {
            return View();
        }


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
