/*
Versión: 1.0
Descripción: ClienteController.cs Representada gestionar la busqueda.
Para el caso de uso:   
    Gestionar Bùsqueda
    Gestionar Calificaciòn
    Gestionar Recomendaciòn

Fecha de creación: [07/08/2023]
Creado por: [DJFN]

Última modificación: [11/11/2023]
Modificado por: [JFSV]
*/


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
        Servicio objservicio = new Servicio();
        

        // GET: Cliente
        public ActionResult Index(int id = 0)  //ok
        {
            //int id = 0;
            ViewBag.ObtenerTotalServiciosCliente = objCliente.ObtenerTotalServiciosCliente(id);
            ViewBag.ObtenerTotalServiciosClienteEstadoEnProceso = objCliente.ObtenerTotalServiciosClienteEstadoEnProceso(id);
            ViewBag.ObtenerTotalTecnicosDisponibleNoDisponibles = objTecnico.ObtenerTotalTecnicosDisponibleNoDisponibles();
            ViewBag.ObtenerTotalTecnicoDisponibles = objTecnico.ObtenerTotalTecnicoDisponibles();


            var grafico = (from t in db.Tecnico
                           join s in db.Servicio on t.Id_Tecnico equals s.Id_Tecnico
                           join c in db.Calificacion on s.Id_Servicio equals c.Id_Servicio
                           join cd in db.Codigo on c.Id_Codigo equals cd.Id_Codigo
                           where s.Id_Tecnico == t.Id_Tecnico
                           select new
                           {
                               IdTecnico = t.Id_Tecnico,
                               Nombre = t.Nombre,
                               Apellido = t.Apellido,
                               IdCodigo = cd.Id_Codigo,
                               Puntaje = cd.Descripcion
                           }).ToList();
            ViewBag.datosgraficos = grafico;

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