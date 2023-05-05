using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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

            //ViewBag.Id_Cliente = new SelectList(db.Problema, "Id_Cliente", "Nombre", problema.Id_Cliente);
            return View(problema);
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
            // Inicializar el modelo con la fecha actual
            objProblema.Fecha_Inicio = DateTime.Now;

            return View(objProblema);
        }

        public ActionResult Agregar(int id = 0)
        {
            ViewBag.TCliente = objProblema.ObtenerIdCliente(id);
            return View(id == 0 ? new Problema() : objProblema.Obtener(id));
        }
        

        public ActionResult Guardar(Problema model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Cliente/index");
            }
            else
            {
                return View("~/Problema/Agregar");
            }
        }

        public ActionResult Eliminar(int id)
        {
            objProblema.Id_Problema = id;
            objProblema.Eliminar();
            return Redirect("~/Problema");
        }

    }
}