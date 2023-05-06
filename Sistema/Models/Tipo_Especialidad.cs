namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tipo_Especialidad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tipo_Especialidad()
        {
            RTecnico_TipoEspecialidad = new HashSet<RTecnico_TipoEspecialidad>();
        }

        [Key]
        public int Id_Tipo_Especialidad { get; set; }

        [StringLength(100)]
        public string TipoEspecialidad { get; set; }

        public int? Id_Especialidad { get; set; }

        public virtual Especialidad Especialidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RTecnico_TipoEspecialidad> RTecnico_TipoEspecialidad { get; set; }
    }
}
