namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Problema")]
    public partial class Problema
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Problema()
        {
            Servicio = new HashSet<Servicio>();
        }

        [Key]
        public int Id_Problema { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        [StringLength(100)]
        public string Documento { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Inicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Fin { get; set; }

        public int Id_Cliente { get; set; }

        public virtual Cliente Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Servicio> Servicio { get; set; }


        //Model1 db = new Model1();


        public List<Problema> Listar()
        {
            var problema = new List<Problema>();
            try
            {
                using (var db = new Model1())
                {
                    problema = db.Problema.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return problema;

        }

        public Problema Obtener(int id)
        {
            var sc = new Problema();
            try
            {
                using (var db = new Model1())
                {
                    sc = db.Problema
                        .Include("Cliente")
                        .Where(x => x.Id_Cliente == id)
                                .SingleOrDefault();
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
