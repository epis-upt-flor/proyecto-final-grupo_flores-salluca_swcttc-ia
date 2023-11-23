/*
Versi�n: 1.0
Descripci�n: Calificacion.cs Representada gestionar calificaci�n
Para el caso de uso: 
    Gestionar Calificaci�n
    Gestionar Progreso

Fecha de creaci�n: [07/08/2023]
Creado por: [DJFN]

�ltima modificaci�n: [11/11/2023]
Modificado por: [JFSV]
*/

namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Calificacion")]
    public partial class Calificacion
    {
        [Key]
        public int Id_Calificacion { get; set; }

        [StringLength(100)]
        public string Comentario { get; set; }

        [Column("Calificacion")]
        public int? Calificacion1 { get; set; }

        public int? Id_Cliente { get; set; }

        public int? Id_Servicio { get; set; }

        public virtual Servicio Servicio { get; set; }

        public virtual Cliente Cliente { get; set; }



        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.Id_Calificacion > 0)
                    {
                        db.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added;
                    }
                    db.SaveChanges();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Calificacion Obtener(int id)
        {
            var califica = new Calificacion();
            try
            {
                using (var db = new Model1())
                {
                    califica = db.Calificacion
                        .Where(x => x.Id_Servicio == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return califica;
        }

        public List<Calificacion> Listar()
        {
            var tipousuario = new List<Calificacion>();
            try
            {
                using (var db = new Model1())
                {
                    tipousuario = db.Calificacion.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tipousuario;
        }

        public List<Calificacion> VerCalificacion(int id)
        {
            List<Calificacion> calificaciones = new List<Calificacion>();
            try
            {
                using (var db = new Model1())
                {
                    calificaciones = db.Calificacion
                       .Where(x => x.Id_Cliente == id)
                       .ToList();
                }
            }
            catch (Exception)
            {
                // Manejar la excepci�n aqu� si es necesario.
                throw;
            }

            return calificaciones; // Devuelve una lista de Calificacion.
        }





        //public List<Calificacion> VerCalificacionLista(List<int> id)
        //{
        //    List<Calificacion> calificaciones = new List<Calificacion>();
        //    try
        //    {
        //        using (var db = new Model1())
        //        {
        //            calificaciones = db.Calificacion
        //                .Where(x => id.Contains(x.Id_Servicio))
        //                .ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        // Manejar la excepci�n aqu� si es necesario.
        //        throw;
        //    }

        //    return calificaciones; // Devuelve una lista de Calificacion.
        //}

        public List<Calificacion> VerCalificacionLista(List<int> id)
        {
            List<Calificacion> calificaciones = new List<Calificacion>();
            try
            {
                using (var db = new Model1())
                {
                    calificaciones = db.Calificacion
                        .Where(x => id.Contains(x.Id_Servicio ?? 0))  // Usamos ?? 0 para manejar nulos
                        .ToList();
                }
            }
            catch (Exception)
            {
                // Manejar la excepci�n aqu� si es necesario.
                throw;
            }

            return calificaciones; // Devuelve una lista de Calificacion.
        }




        
    }
}
