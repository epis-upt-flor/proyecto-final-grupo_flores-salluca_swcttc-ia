﻿using SistemaArtemis.Models;
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
        public ActionResult Index(int id = 0)  //ok
        {
            //int id = 0;
            ViewBag.ObtenerTotalServiciosCliente = objCliente.ObtenerTotalServiciosCliente(id);
            ViewBag.ObtenerTotalServiciosClienteEstadoEnProceso = objCliente.ObtenerTotalServiciosClienteEstadoEnProceso(id);
            ViewBag.ObtenerTotalTecnicosDisponibleNoDisponibles = objTecnico.ObtenerTotalTecnicosDisponibleNoDisponibles();
            ViewBag.ObtenerTotalTecnicoDisponibles = objTecnico.ObtenerTotalTecnicoDisponibles();

            return View();
        }       
        public ActionResult ListarTecnicos() //ok
        {
            return View(objTecnico.Listar());
        }
     
        public ActionResult Perfil(int id = 0) //ok 
        {
            if (id != 0)
                return View(objCliente.Obtener(id));
            else
                return Redirect("~/Cliente"); 
        }
    }
}