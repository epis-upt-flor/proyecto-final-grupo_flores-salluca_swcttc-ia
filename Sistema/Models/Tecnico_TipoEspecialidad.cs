namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tecnico_TipoEspecialidad
    {
        [Key]
        public int Id_Tecnico_TipoEspecialidad { get; set; }

        public int Id_Tecnico { get; set; }

        public int Id_Tipo_Especialidad { get; set; }

        public virtual Tecnico Tecnico { get; set; }

        public virtual TipoEspecialidad TipoEspecialidad { get; set; }
    }
}
