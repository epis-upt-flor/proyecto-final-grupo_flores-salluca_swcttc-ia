namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Cliente")]
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            Calificacion = new HashSet<Calificacion>();
            Problema = new HashSet<Problema>();
        }

        [Key]
        public int Id_Cliente { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; }

        public int? Estado { get; set; }

        public int Id_Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calificacion> Calificacion { get; set; }

        public virtual Usuario Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Problema> Problema { get; set; }


        public List<Cliente> Listar1(int id)
        {
            var sc = new List<Cliente>();
            try
            {
                using (var db = new Model1())
                {
                    sc = db.Cliente.Include(c => c.Usuario)
                                   .Where(c => c.Usuario.Id_Usuario == id)
                                   .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return sc;
        }


        public Cliente Obtener(int id)
        {
            var clientes = new Cliente();
            try
            {
                using (var db = new Model1())
                {
                    clientes = db.Cliente
                        .Include("Usuario")
                        .Where(x => x.Id_Usuario == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return clientes;
        }



        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.Id_Cliente > 0)
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

        // listar Cliente
        public List<Cliente> Listar()
        {
            var sc = new List<Cliente>();
            try
            {
                using (var db = new Model1())
                {
                    sc = db.Cliente.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return sc;
        }

    }
}
