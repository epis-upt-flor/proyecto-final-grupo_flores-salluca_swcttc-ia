using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaArtemis.Controllers
{
    public class ClienteController : Controller
    {
        Tecnico objTecnico = new Tecnico();
       // Cliente objCliente = new Cliente();

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


        public ActionResult Guardar(Cliente model)
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Cliente/index");
            }
            else
            {
                return View("~/Cliente/Agregar");
            }
        }
  
    }
}