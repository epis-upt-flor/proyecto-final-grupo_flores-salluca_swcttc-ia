using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SistemaArtemis.Controllers
{
    public class ProblemaController : Controller
    {
        private Model1 db = new Model1();

        // GET: Problema
        private Problema objProblema = new Problema();
        private Cliente objCliente = new Cliente();


        public ActionResult Create(int id)
        {
            ViewBag.Cliente = objCliente.Listar1(id);
            return View();
        }


        [HttpPost]
        //public ActionResult Create(Problema problema)
        public ActionResult Create([Bind(Include = "Id_Problema,Descripcion,Documento,Estado,Fecha_Inicio,Fecha_Fin,Id_Cliente")] Problema problema)
        {
            if (ModelState.IsValid==false)
            {
                db.Problema.Add(problema);
                db.SaveChanges();
                //problema.Guardar();
                return RedirectToAction("~/Cliente/Index");
            }
            return View(problema);
        }


        [HttpPost]
        public ActionResult GuardarDocumento(Problema problema, HttpPostedFileBase documento)
        {
            if (documento != null && documento.ContentLength > 0)
            {
                var fileName = Path.GetFileName(documento.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/Documentos"), fileName);
                documento.SaveAs(path);
                problema.Documento = fileName;
            }
            using (var db = new Model1())
            {
                db.Entry(problema).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        public ActionResult Index(int id)
        {
            return View(objProblema.Obtener(id));
        }

        public ActionResult ListarProblemaCliente(int id)
        {
            return View(objProblema.ListarProblema(id));
        }


        public ActionResult PublicarProblema()
        {
            return View();
        }
        public ActionResult Guardar(Problema model)
        {
            if (ModelState.IsValid == false)
            {
                model.Guardar();
                return Redirect("~/Cliente/index");
            }
            else
            {
                return View(model);
            }

        }


        //public ActionResult Agregar(int id = 0)
        //{
        //    ViewBag.Problema = objProblema.Listar();

        //    return View(id == 0 ? new Problema() // Agregarmos un nuevo objeto
        //        : objProblema.Obtener(id) //Devuelve el id del objeto
        //        );
        //}
       

        public ActionResult Eliminar(int id)
        {
            objProblema.Id_Problema = id;
            objProblema.Eliminar();
            return Redirect("~/Problema");
        }

    }
}