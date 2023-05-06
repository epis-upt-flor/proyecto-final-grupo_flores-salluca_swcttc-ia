namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Tecnico")]
    public partial class Tecnico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tecnico()
        {
            RTecnico_TipoEspecialidad = new HashSet<RTecnico_TipoEspecialidad>();
            Servicio = new HashSet<Servicio>();
        }

        [Key]
        public int Id_Tecnico { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; }

        public int Id_Especialidad { get; set; }

        public int Id_Estado_Tecnico { get; set; }

        public int Id_Usuario { get; set; }

        public virtual Especialidad Especialidad { get; set; }

        public virtual Estado_Tecnico Estado_Tecnico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RTecnico_TipoEspecialidad> RTecnico_TipoEspecialidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Servicio> Servicio { get; set; }

        public virtual Usuario Usuario { get; set; }



        public Tecnico Obtener(int id)
        {
            var tecnicos = new Tecnico();
            try
            {
                using (var db = new Model1())
                {
                    tecnicos = db.Tecnico.Include("Estado_Tecnico")
                        .Include("Tecnico_TipoEspecialidad")
                        .Include("Usuario")
                        .Where(x => x.Id_Usuario == id)
                                .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tecnicos;
        }


        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.Id_Tecnico > 0)
                    {
                        db.Entry(this).State = EntityState.Modified; //existe
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added; //nuevo registro
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<Tecnico> Listar()
        {
            var tecnico = new List<Tecnico>();
            try
            {
                using (var db = new Model1())
                {
                    tecnico = db.Tecnico.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tecnico;
        }


        //Buscar
        public List<Tecnico> Buscar(string criterio)
        {
            var categorias = new List<Tecnico>();
            try
            {
                using (var db = new Model1())
                {
                    categorias = db.Tecnico
                        .Include("Tipo_Usuario")
                        .Where(x => x.Nombre.Contains(criterio) ||
                                x.Apellido.Contains(criterio) )//|| 
                               // x.Especialidad.Contains(criterio))
                        .ToList();

                }
            }
            catch (Exception)
            {
                throw;
            }

            return categorias;

        }


    }
}
