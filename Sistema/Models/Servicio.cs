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

        [StringLength(250)]
        public string Documento { get; set; }

        public int Id_Tecnico { get; set; }

        public int Id_Problema { get; set; }

        public int Id_Estado_Servicio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calificacion> Calificacion { get; set; }

        public virtual Estado_Servicio Estado_Servicio { get; set; }

        public virtual Problema Problema { get; set; }

        public virtual Tecnico Tecnico { get; set; }


        public List<Servicio> Listar()
        {
            var serv = new List<Servicio>();
            try
            {
                using (var db = new Model1())
                {
                    serv = db.Servicio
                           .Include("Tecnico")
                           .Include("Problema")
                           .Where(s => s.Id_Estado_Servicio == 1)
                     .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serv;
        }

        /// <summary>
        /// Esta función recupera una lista de problemas asociados con una identificación de cliente
        /// específica.
        /// </summary>
        /// <param name="id">El parámetro id es un número entero que representa el id del usuario para
        /// el que queremos listar los problemas.</param>
        /// <returns>
        /// El método devuelve una lista de objetos Problema.
        /// </returns>
        public List<Problema> ListarProblema(int id)
        {
            var misproblemas = new List<Problema>();
            try
            {
                using (var db = new Model1())
                {
                    var icliente = db.Cliente
                        .Where(u => u.Id_Usuario == id)
                        .Select(u => u.Id_Cliente)
                        .SingleOrDefault();
                    if (icliente != 0)
                    {
                        misproblemas = db.Problema
                            .Include("Cliente")
                            .Where(x => x.Id_Cliente == icliente)
                            .ToList();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return misproblemas;
        }


        /// <summary>
        /// La función recupera una lista de servicios asignados a un técnico con una identificación de
        /// usuario específica y un estado de servicio específico.
        /// </summary>
        /// <param name="id">El parámetro id es un número entero que representa el id del usuario para el
        /// que se debe recuperar la lista de servicios.</param>
        /// <returns>
        /// El método devuelve una lista de objetos de Servicio.
        /// </returns>

        public List<Servicio> MisServicios(int id)
        {
            var misservicios = new List<Servicio>();
            try
            {
                using (var db = new Model1())
                {
                    var itecnico = db.Tecnico
                        .Where(u => u.Id_Usuario == id)
                        .Select(u => u.Id_Tecnico)
                        .SingleOrDefault();
                    if (itecnico != 0)
                    {
                        misservicios = db.Servicio
                            .Include("Tecnico")
                            .Include("Estado_Servicio")
                            .Include("Problema")
                            .Where(x => x.Id_Tecnico == itecnico && x.Id_Estado_Servicio == 2)
                            .ToList();
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            return misservicios;
        }

        /// <summary>
        /// Esta función guarda los cambios realizados en un objeto de servicio en una base de datos utilizando
        /// Entity Framework.
        /// </summary>

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



        public Servicio Obtener(int id)
        {
            var servicio = new Servicio();
            try
            {
                using (var db = new Model1())
                {
                    servicio = db.Servicio.Include("Problema")
                        .Where(x => x.Id_Problema == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return servicio;
        }

    }
}


