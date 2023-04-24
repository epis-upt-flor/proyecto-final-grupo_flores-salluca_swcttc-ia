namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Servicio")]
    public partial class Servicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Servicio()
        {
            Calificacion = new HashSet<Calificacion>();
        }

        [Key]
        public int Id_Servicio { get; set; }

        [Required]
        [StringLength(250)]
        public string Descripcion { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Inicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Fin { get; set; }

        public int Id_Cliente { get; set; }

        public int Id_Tecnico { get; set; }

        public int Id_Problema { get; set; }

        public int Id_Tipo_Especialidad { get; set; }

        public int Id_Estado_Servicio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calificacion> Calificacion { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Estado_Servicio Estado_Servicio { get; set; }

        public virtual Problema Problema { get; set; }

        public virtual Tecnico Tecnico { get; set; }

        public virtual TipoEspecialidad TipoEspecialidad { get; set; }

        //Model1 db = new Model1();



  



        public List<Servicio> Listar()
        {
            var serv = new List<Servicio>();
            try
            {
                using (var db = new Model1())
                {
                    serv = db.Servicio
                           .Include("Tecnico")
                           .Include("Cliente")
                           .Include("Problema")
                           .Include("TipoEspecialidad")
                           .Where(s => s.Id_Estado_Servicio == 1)
                     .ToList();


                }
            }
            catch (Exception)
            {
                throw;
            }

            return serv;

        }


        public List<Servicio> MisServicios(int id)
        {
            var misservicios = new List<Servicio>();
            try
            {
                using (var db = new Model1())
                {
                    misservicios = db.Servicio
                        .Include("Tecnico").ToList()
                        .FindAll(x => x.Id_Tecnico == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return misservicios;
        }




        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.Id_Servicio > 0)
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

    }
}
