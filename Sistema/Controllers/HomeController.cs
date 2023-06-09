﻿using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaArtemis.Controllers
{
    public class HomeController : Controller
    {
        private Problema objproblema = new Problema();
        private Tecnico objTecnico = new Tecnico();
        
        public ActionResult Index()
        {
            return View();
        }

       /// <summary> Esta función devuelve una vista con una lista de puestos de trabajo disponibles.</summary>
       /// <returns>El método `EmpleosDisponibles` está devolviendo una `Vista` con el resultado de llamar al método `Listar` del objeto `objServicio`</returns>
        public ActionResult EmpleosDisponibles()
        {
            return View(objproblema.ListProblem());
        }

        public ActionResult TecnicosDisponibles()
        {
            return View(objTecnico.Listar());
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}