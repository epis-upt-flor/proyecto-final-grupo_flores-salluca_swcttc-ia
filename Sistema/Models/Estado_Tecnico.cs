namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Estado_Tecnico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Estado_Tecnico()
        {
            Tecnico = new HashSet<Tecnico>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Estado_Tecnico { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tecnico> Tecnico { get; set; }


        public List<Estado_Tecnico> Listar()
        {
            var sc = new List<Estado_Tecnico>();
            try
            {
                using (var db = new Model1())
                {
                    sc = db.Estado_Tecnico
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return sc;
        }


        public List<Estado_Tecnico> Buscar(string criterio)
        {
            var estadotecnico = new List<Estado_Tecnico>();
            try
            {
                using (var db = new Model1())
                {
                    estadotecnico = db.Estado_Tecnico
                        .Where(x => x.Descripcion.Contains(criterio))
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return estadotecnico;
        }


        public Estado_Tecnico Obtener(int id)
        {
            var estadotecnico = new Estado_Tecnico();
            try
            {
                using (var db = new Model1())
                {
                    estadotecnico = db.Estado_Tecnico
                        .Where(x => x.Id_Estado_Tecnico == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estadotecnico;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.Id_Estado_Tecnico > 0)
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

        public void Eliminar()
        {
            try
            {
                using (var db = new Model1())
                {
                    db.Entry(this).State = EntityState.Deleted;
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
