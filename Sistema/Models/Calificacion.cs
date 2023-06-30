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


    }
}
