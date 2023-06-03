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
                    var idTecnico = db.Tecnico
                    .Where(tec => tec.Id_Usuario == id) // Cambio aquí: Buscar por Id_Usuario en lugar de Id_Tecnico
                    .Select(tec => tec.Id_Tecnico)
                    .FirstOrDefault();

                    if (idTecnico != 0)
                    {
                        var idEspecialidad = db.Tecnico
                            .Where(tec => tec.Id_Tecnico == idTecnico)
                            .Select(tec => tec.Id_Especialidad)
                            .FirstOrDefault();

                        if (idEspecialidad != 0)
                        {
                            RtiposEspecialidad = (
                                from tec in db.Tecnico
                                from esp in db.Especialidad
                                from tipE in db.Tipo_Especialidad
                                from rtec in db.RTecnico_TipoEspecialidad
                                where tec.Id_Especialidad == esp.Id_Especialidad &&
                                      tipE.Id_Especialidad == esp.Id_Especialidad &&
                                      tipE.Id_Tipo_Especialidad == rtec.Id_Tipo_Especialidad &&
                                      tec.Id_Tecnico == idTecnico &&
                                      esp.Id_Especialidad == idEspecialidad
                                select rtec
                                ).ToList();
                        }
                    }
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


        //public List<Tipo_Especialidad> ListarR(int id)
        //{
        //    var RtiposEspecialidad = new List<Tipo_Especialidad>();
        //    try
        //    {
        //        using (var db = new Model1())
        //        {
        //            var idTecnico = db.Tecnico
        //            .Where(tec => tec.Id_Usuario == id) // Cambio aquí: Buscar por Id_Usuario en lugar de Id_Tecnico
        //            .Select(tec => tec.Id_Tecnico)
        //            .FirstOrDefault();

        //            if (idTecnico != 0)
        //            {
        //                var idEspecialidad = db.Tecnico
        //                    .Where(tec => tec.Id_Tecnico == idTecnico)
        //                    .Select(tec => tec.Id_Especialidad)
        //                    .FirstOrDefault();

        //                if (idEspecialidad != 0)
        //                {
        //                    RtiposEspecialidad = (
        //                        from tec in db.Tecnico
        //                        from esp in db.Especialidad
        //                        from tipE in db.Tipo_Especialidad
        //                        from rtec in db.RTecnico_TipoEspecialidad
        //                        where tec.Id_Especialidad == esp.Id_Especialidad &&
        //                              tipE.Id_Especialidad == esp.Id_Especialidad &&
        //                              tipE.Id_Tipo_Especialidad == rtec.Id_Tipo_Especialidad &&
        //                              tec.Id_Tecnico == idTecnico &&
        //                              esp.Id_Especialidad == idEspecialidad
        //                        select tipE
        //                        ).ToList();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return RtiposEspecialidad;
        //}

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
                        .Where(x => x.Id_Tipo_Especialidad == id)
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
