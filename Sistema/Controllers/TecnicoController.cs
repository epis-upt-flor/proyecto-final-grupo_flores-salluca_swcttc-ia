/*
Versión: 1.0
Descripción: TecnicoController.cs Representada gestionar calificaciòn
Para el caso de uso:   
    Gestionar Calificaciòn
    Gestionar Progreso

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
    public class TecnicoController : Controller
    {
        private Model1 db = new Model1();
        private Tecnico objtecnico = new Tecnico();
        private Servicio objservicio = new Servicio();
        private Especialidad objTipo= new Especialidad();
        private Problema objproblema = new Problema();
        private Servicio objServicio = new Servicio();
        private Calificacion objCalificacion = new Calificacion();
        private Cliente objCliente = new Cliente();

        public ActionResult Index(int id = 0) //ok
        {
            //Validar ID A Vista
            ViewBag.ObtenerTotalServiciosPorTecnico = objservicio.ObtenerTotalServiciosPorTecnico(id);
            ViewBag.ObtenerTotalServiciosFaltaAprobarEstado = objservicio.ObtenerTotalServiciosFaltaAprobarEstado(id);
            ViewBag.ObtenerTotalServiciosEnProceso = objservicio.ObtenerTotalServiciosEnProceso(id);
            ViewBag.ObtenerTotalTipoEspecialidadPorTecnico = objtecnico.ObtenerTotalTipoEspecialidadPorTecnico(id);


            var grafico = (db.Servicio.Where(s => s.Id_Tecnico == id)
                                .GroupBy(s => new { s.Id_Tecnico, s.Id_Estado_Servicio })
                                .Select(g => new
                                {
                                    IdEstadoServicio = g.Key.Id_Estado_Servicio,
                                    CantidadServicios = g.Count()
                                }));

            ViewBag.datosgrafiTecnico = grafico;

            ViewBag.datosgrafiTecnico2 = (from s in db.Servicio
                                          join c in db.Calificacion on s.Id_Servicio equals c.Id_Servicio
                                          join cd in db.Codigo on c.Id_Codigo equals cd.Id_Codigo
                                          where s.Id_Tecnico == id
                                          group new { s, cd } by new { s.Id_Tecnico, cd.Descripcion } into grupo
                                          select new
                                          {
                                              IdTecnico = grupo.Key.Id_Tecnico,
                                              CodigoDescripcion = grupo.Key.Descripcion,
                                              CantidadServicios = grupo.Count()
                                          });




            return View();
        }

        /// <summary>Esta función devuelve una vista del perfil de un técnico en función de su ID.</summary>       
        /// <returns>Si el parámetro `id` no es igual a 0, el método `Obtener` del objeto `objtecnico` para el `id` especificado. Si `id` es igual a 0, el método redirige a la acción `Tecnico`.</returns>
        public ActionResult Perfil(int id = 0)  //ok
        {
            ViewBag.Tipo=objTipo.Listar();
            if (id != 0)
                return View(objtecnico.Obtener(id));
            else
                return Redirect("~/Tecnico");
        }

        /// <summary>La función guarda el perfil de un técnico y lo redirige a la página de índice si el estado del modelo es válido; de lo contrario, regresa a la página de perfil.</summary>     
        public ActionResult Guardar(Tecnico model) 
        {
            if (ModelState.IsValid)
            {
                model.Guardar();
                return Redirect("~/Tecnico/Index");
            }
            else
            {
                model.Guardar();
                return View("~/Tecnico/Perfil");
            }
        }

       /// <summary> Esta función devuelve una vista con una lista de trabajos disponibles.</summary>
       /// <returns>El método `TrabajosDisponibles` está devolviendo una `Vista` con el resultado de llamar al método `Listar` del objeto `objservicio`</returns>
        //public ActionResult TrabajosDisponibles()
        //{
        //    return View(objservicio.Listar());
        //}

        /// <summary> Esta función devuelve una vista de los servicios pertenecientes a un usuario específico.
        /// <returns> El método `ListarMisServicios` está devolviendo una vista con los datos obtenidos del método`MisServicios` del objeto `objservicio`, que toma un parámetro `id`.
        public ActionResult ListarMisServicios(int id)  //ok
        {
            ViewBag.listacliente = objCliente.Listar();
            return View(objservicio.MisServicios(id));
        }

        /// <summary> Esta función devuelve una vista para visualizar un determinado tipo de calificación.</summary>
        /// <returns> El método está devolviendo un resultado Ver. </returns>
        public ActionResult VisualizarCalificacion1(int id)
        {
            //lista de sercivo con IdTecnico
            var listtecnico = objServicio.ListaServicioTecnico(id);

            return View(objCalificacion.VerCalificacionLista(listtecnico));
        }

    }
}
