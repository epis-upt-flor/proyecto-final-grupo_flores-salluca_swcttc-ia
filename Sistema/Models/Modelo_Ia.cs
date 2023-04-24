namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Modelo_Ia
    {
        [Key]
        public int Id_Modelo { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ultimo_Entrenamiento { get; set; }

        public decimal? precision { get; set; }
    }
}
