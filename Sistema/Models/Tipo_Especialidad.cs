namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

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




        //public List<Tipo_Especialidad> Listar(int id = 0)
        //{
        //    var tiposEspecialidad = new List<Tipo_Especialidad>();
        //    try
        //    {
        //        using (var db = new Model1())
        //        {
        //            var idEspecialidad = db.Tecnico
        //                .Where(tec => tec.Id_Tecnico == id)
        //                .Select(tec => tec.Id_Especialidad)
        //                .FirstOrDefault();

        //            tiposEspecialidad = db.Tipo_Especialidad
        //                .Where(te => te.Id_Especialidad == idEspecialidad)
        //                .ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return tiposEspecialidad;
        //}
        //public RTecnico_TipoEspecialidad Obtener(int id)
        //{
        //    var tecnicos = new RTecnico_TipoEspecialidad();
        //    try
        //    {
        //        using (var db = new Model1())
        //        {
        //            tecnicos = db.RTecnico_TipoEspecialidad
        //                .Include("Tipo_Especialidad")
        //                .Include("Tecnico")                      
        //                .Include("Usuario")
        //                .Where(x => x.Id_Tecnico == id)
        //                        .SingleOrDefault();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return tecnicos;
        //}


        public List<Tipo_Especialidad> Listar(int id = 0)
        {
            var tiposEspecialidad = new List<Tipo_Especialidad>();
            try
            {
                using (var db = new Model1())
                {
                    var idTecnico = db.Tecnico
                        .Where(tec => tec.Id_Usuario == id)
                        .Select(tec => tec.Id_Tecnico)
                        .FirstOrDefault();

                    if (idTecnico != 0)
                    {
                        var idEspecialidad = db.Tecnico
                            .Where(tec => tec.Id_Tecnico == idTecnico)
                            .Select(tec => tec.Id_Especialidad)
                            .FirstOrDefault();

                        tiposEspecialidad = db.Tipo_Especialidad
                            .Where(te => te.Id_Especialidad == idEspecialidad)
                            .ToList();
                        // Validar el campo "Id_Especialidad" de la tabla "Tipo_Especialidad" con el campo "id_Especialidad" de la tabla "Especialidad"
                        tiposEspecialidad = tiposEspecialidad.Join(
                            db.Especialidad,
                            tipo => tipo.Id_Especialidad,
                            especialidad => especialidad.Id_Especialidad,
                            (tipo, especialidad) => tipo)
                            .ToList();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tiposEspecialidad;
        }

    }
}
