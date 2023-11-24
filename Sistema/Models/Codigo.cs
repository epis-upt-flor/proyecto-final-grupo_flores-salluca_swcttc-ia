namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Codigo")]
    public partial class Codigo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Codigo()
        {
            Calificacion = new HashSet<Calificacion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Codigo { get; set; }

        [Column("Codigo")]
        public int? Codigo1 { get; set; }

        [StringLength(20)]
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calificacion> Calificacion { get; set; }


        public List<Codigo> Listar()
        {
            var tipocodigos = new List<Codigo>();
            try
            {
                using (var db = new Model1())
                {
                    tipocodigos = db.Codigo.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return tipocodigos;

        }



    }
}
