namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RTecnico_TipoEspecialidad
    {
        public int Id { get; set; }

        public int Id_Tecnico { get; set; }

        public int Id_Tipo_Especialidad { get; set; }

        public virtual Tecnico Tecnico { get; set; }

        public virtual Tipo_Especialidad Tipo_Especialidad { get; set; }
    }
}
