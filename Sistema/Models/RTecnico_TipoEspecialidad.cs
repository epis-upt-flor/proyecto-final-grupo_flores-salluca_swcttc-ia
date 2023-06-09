namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Runtime.Remoting.Contexts;

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

        public List<RTecnico_TipoEspecialidad> ListarR(int id)
        {
            var RtiposEspecialidad = new List<RTecnico_TipoEspecialidad>();
            try
            {
                using (var db = new Model1())
                {                        
                    RtiposEspecialidad = (
                        from tec in db.Tecnico
                        join esp in db.Especialidad on tec.Id_Especialidad equals esp.Id_Especialidad
                        join tipE in db.Tipo_Especialidad on esp.Id_Especialidad equals tipE.Id_Especialidad
                        join rtec in db.RTecnico_TipoEspecialidad on new { tipE.Id_Tipo_Especialidad, tec.Id_Tecnico } equals new { rtec.Id_Tipo_Especialidad, rtec.Id_Tecnico } into rtecJoin
                        from rtec in rtecJoin.DefaultIfEmpty()
                        where rtec.Id_Tecnico == id
                        select rtec
                    ).ToList();
                    
                }
            }
            catch (Exception)
            {
                throw;
            }

            return RtiposEspecialidad;
        }
      

        public void Eliminar()
        {
            try
            {
                using (var db = new Model1())
                {

                    db.Entry(this).State = EntityState.Deleted;

                    db.SaveChanges();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public RTecnico_TipoEspecialidad Obtener(int id)
        {
            var ie = new RTecnico_TipoEspecialidad();
            try
            {
                using (var db = new Model1())
                {
                    ie = db.RTecnico_TipoEspecialidad
                        .Include("Tipo_Especialidad")
                        .Include("Tecnico")
                        .Where(x => x.Id_Tecnico == 0)
                        .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ie;
        }
    }    
}
