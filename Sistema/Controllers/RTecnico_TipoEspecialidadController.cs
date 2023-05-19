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
    public class RTecnico_TipoEspecialidadController : Controller
    {

        private Tipo_Especialidad objTipoespecialidad = new Tipo_Especialidad();
        private Tecnico objtecnico = new Tecnico();
        private Model1 db = new Model1();

        // GET: RTecnico_TipoEspecialidad
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            ViewBag.Tipo = objTipoespecialidad.Listar(id);
            return View();
        }


        [HttpPost]
        public ActionResult Create([Bind(Include ="Id,Id_Tecnico,Id_Tipo_Especialidad")] RTecnico_TipoEspecialidad rtecnicotipoespecialidad)
        {
            if (ModelState.IsValid == false)
            {
                db.RTecnico_TipoEspecialidad.Add(rtecnicotipoespecialidad);
                db.SaveChanges();
                return RedirectToAction("~/Tecnico/Index");
            }
            return View(rtecnicotipoespecialidad);
        }



        public ActionResult List(int id)
        {
            return View(objTipoespecialidad.Listar(id));
        }

    }
}