namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Common;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Especialidad")]
    public partial class Especialidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Especialidad()
        {
            Modelo_Ia = new HashSet<Modelo_Ia>();
            Tecnico = new HashSet<Tecnico>();
            Tipo_Especialidad = new HashSet<Tipo_Especialidad>();
        }

        [Key]
        public int Id_Especialidad { get; set; }

        ///[Column("Especialidad")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Modelo_Ia> Modelo_Ia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tecnico> Tecnico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tipo_Especialidad> Tipo_Especialidad { get; set; }



        public List<Especialidad> Listar()
        {
            var tipoespecialidad = new List<Especialidad>();
            try
            {
                using (var db = new Model1())
                {
                    tipoespecialidad = db.Especialidad.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return tipoespecialidad;

        }

     






    }
}
