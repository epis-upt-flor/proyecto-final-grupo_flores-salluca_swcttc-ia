namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Tipo_Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tipo_Usuario()
        {
            Usuario = new HashSet<Usuario>();
        }

        [Key]
        public int Id_Tipo_Usuario { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }

        ///METODO LISTAR


        public List<Tipo_Usuario> Listar()
        {
            var tipousuario = new List<Tipo_Usuario>();
            try
            {
                using (var db = new Model1())
                {
                    tipousuario = db.Tipo_Usuario.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tipousuario;
        }
        public List<Tipo_Usuario> Buscar(string criterio)
        {
            var categorias = new List<Tipo_Usuario>();

            try
            {
                using (var db = new Model1())
                {
                    categorias = db.Tipo_Usuario.Where(x => x.Nombre.Contains(criterio)).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return categorias;
        }

        public Tipo_Usuario Obtener(int id)
        {
            var categoria = new Tipo_Usuario();
            try
            {
                using (var db = new Model1())
                {
                    categoria = db.Tipo_Usuario.Where(x => x.Id_Tipo_Usuario == id)
                                .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return categoria;
        }

        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.Id_Tipo_Usuario > 0)
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
