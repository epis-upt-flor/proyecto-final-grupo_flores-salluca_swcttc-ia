﻿using SistemaArtemis.Models;
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
        private RTecnico_TipoEspecialidad objRTipoEspecialidad =new RTecnico_TipoEspecialidad();
        private Tipo_Especialidad objTipoespecialidad = new Tipo_Especialidad();
        private Tecnico objtecnico = new Tecnico();
        private Model1 db = new Model1();

        public ActionResult Create(int id = 0)
        {
            ViewBag.Tipo = objTipoespecialidad.Listar(id);

            var ie = id == 0
                ? new RTecnico_TipoEspecialidad()
                : objRTipoEspecialidad.Obtener(id);

            return View(ie);
          
        }

        public ActionResult Guardar(RTecnico_TipoEspecialidad model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Tecnico/Index");
            }
            else
            {
                
                return Redirect("~/RTecnico_TipoEspecialidad/Create");
            }
        }


        public ActionResult List(int id)
        {
            return View(objRTipoEspecialidad.Listar(id));
        }

    }
}





//public ActionResult Create(int id)
//{
//    ViewBag.Tipo = objTipoespecialidad.Listar(id);
//    return View();
//}


//[HttpPost]
//public ActionResult Create([Bind(Include ="Id,Id_Tecnico,Id_Tipo_Especialidad")] RTecnico_TipoEspecialidad rtecnicotipoespecialidad)
//{
//    if (ModelState.IsValid == false)
//    {
//        db.RTecnico_TipoEspecialidad.Add(rtecnicotipoespecialidad);
//        db.SaveChanges();
//        return RedirectToAction("~/Tecnico/Index");
//    }
//    return View(rtecnicotipoespecialidad);
//}

