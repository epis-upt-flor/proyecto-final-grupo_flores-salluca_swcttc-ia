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
        private Tipo_Usuario objTipoUsuario = new Tipo_Usuario();

        private Model1 db = new Model1();
        

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
        public ActionResult Visualizar(int id)
        {
            return View(objTipoUsuario.Obtener(id));
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


        //TipoUsuario
        // GET: Tipo_Usuario/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Tipo_Usuario/Create
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