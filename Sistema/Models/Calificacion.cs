namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Calificacion")]
    public partial class Calificacion
    {
        [Key]
        public int Id_Calificacion { get; set; }

        [StringLength(100)]
        public string Comentario { get; set; }

        [Column("Calificacion")]
        public int? Calificacion1 { get; set; }

        public int? Id_Servicio { get; set; }

        public virtual Servicio Servicio { get; set; }
    }
}
