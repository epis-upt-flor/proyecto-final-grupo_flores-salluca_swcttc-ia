namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class RTecnico_TipoEspecialidad
    {
        public int Id { get; set; }

        public int Id_Tecnico { get; set; }

        public int Id_Tipo_Especialidad { get; set; }

        public virtual Tecnico Tecnico { get; set; }

        public virtual Tipo_Especialidad Tipo_Especialidad { get; set; }




        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.Id > 0)
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



    }
}
