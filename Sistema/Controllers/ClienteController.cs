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
    public class ClienteController : Controller
    {
        Tecnico objTecnico = new Tecnico();
        Cliente objCliente = new Cliente();
        private Model1 db = new Model1();


        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListarCliente()
        {
            return View();
        }

        public ActionResult ListarTecnicos()
        {
            return View(objTecnico.Listar());
        }

        public ActionResult PublicarServicio()
        {
            return View();
        }

        public ActionResult Perfil(int id = 0)
        {
            if (id != 0)
                return View(objCliente.Obtener(id));
            else
                return Redirect("~/Cliente");
            //return View();
        }



        public ActionResult Guardar(Cliente model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Cliente/Index");
            }
            else
            {
                return View("~/Cliente/Perfil");
            }
        }


        //Cliente
        // GET: Cliente/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {           
            ViewBag.Id_Usuario = new SelectList(db.Usuario, "Id_Usuario", "Correo");
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Cliente,Nombre,Apellido,Telefono,Estado,Id_Usuario")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Cliente.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.Id_Usuario = new SelectList(db.Usuario, "Id_Usuario", "Correo", cliente.Id_Usuario);
            return View(cliente);
        }

        // GET: Cliente/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id_Usuario = new SelectList(db.Usuario, "Id_Usuario", "Correo", cliente.Id_Usuario);
            return View(cliente);
        }

        // POST: Cliente/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Cliente,Nombre,Apellido,Telefono,Estado,Id_Usuario")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Usuario = new SelectList(db.Usuario, "Id_Usuario", "Correo", cliente.Id_Usuario);
            return View(cliente);
        }

        // GET: Cliente/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
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