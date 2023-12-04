/*
Versión: 1.0
Descripción: CalificaciònController.cs Representada gestionar calificaciòn
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
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaArtemis.Controllers
{
    public class CalificacionController : Controller
    {
        // GET: Calificacion
        static string cadena = "Data Source=.;Initial Catalog=ArtemisBD;Integrated security=true";
        ////static string cadena = "Data Source=artemis.database.windows.net;Initial Catalog=ArtemisBD;user id=administrador;password=upt.2023";


        Model1 db = new Model1();

        private Codigo objcodi = new Codigo();
        private Calificacion objcalifica = new Calificacion();
        private Problema objproblema = new Problema();
        private Servicio objservicio = new Servicio();
        private Tecnico objtecnico=new Tecnico();

  
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                // El parámetro 'id' tiene un valor, muestra otras calificaciones
                var listServicio = db.Servicio.Include("Tecnico").Where(s => s.Id_Estado_Servicio.Equals(3)).ToList();
                ViewBag.Service = listServicio;
            }
            else
            {
                // El parámetro 'id' es nulo,dirigir a otra página index cliente
                return RedirectToAction("Index", "Cliente");
            }

            return View();
        }


        public ActionResult Calificar(int id = 0)
        {
            ViewBag.idservicio = id;
            //var idcod = objcodi.Listar();
            // var califi = db.Codigo.ToList();
            ViewBag.Puntaje = db.Codigo.ToList();
            return View();
        }


        public ActionResult Guardar(string Comentario, int Calificacion1, int Id_Cliente, int Id_Servicio)
        {
            using (SqlConnection oconexion = new SqlConnection(cadena))
            {
                string query = "INSERT INTO Calificacion (Comentario, Id_codigo, Id_cliente, Id_Servicio) VALUES (@comentario, @calificacion1, @id_cliente, @id_Servicio)";

                SqlCommand cmd = new SqlCommand(query, oconexion);
                cmd.Parameters.AddWithValue("@comentario", Comentario);
                cmd.Parameters.AddWithValue("@calificacion1", Calificacion1);
                cmd.Parameters.AddWithValue("@id_cliente", Id_Cliente);
                cmd.Parameters.AddWithValue("@id_Servicio", Id_Servicio);

                oconexion.Open();
                cmd.ExecuteNonQuery();

            }

            CambiarEstadoServicio(Id_Servicio);

            return RedirectToAction("Index", "Calificacion", new { idCliente = Id_Cliente });
        }

        private void CambiarEstadoServicio(int? idServicio)
        {
            try
            {
                using (var db = new Model1())
                {
                    var servicio = db.Servicio.Find(idServicio);

                    if (servicio != null)
                    {
                        servicio.Id_Estado_Servicio = 4;
                        db.SaveChanges();
                    }
                    int idProblema = servicio.Id_Problema;
                    var problema = db.Problema.Find(idProblema);
                    if (problema != null)
                    {
                        problema.Estado = "Finalizado"; 
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult VerCalificacion(int id)
        {
            ViewBag.ltecnico = objtecnico.Listar();
            return View(objcalifica.VerCalificacion(id));
        }


        

    }
}