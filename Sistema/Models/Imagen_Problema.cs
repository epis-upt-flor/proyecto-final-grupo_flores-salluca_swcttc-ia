namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Imagen_Problema
    {
        [Key]
        public int Id_Imagen { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        public byte[] Archivo { get; set; }

        [StringLength(50)]
        public string Extension { get; set; }

        public int? Id_Problema { get; set; }

        public virtual Problema Problema { get; set; }
    }
}
