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

        public List<Tipo_Especialidad> Listar(int id)
        {
            var tiposEspecialidad = new List<Tipo_Especialidad>();
            try
            {
                using (var db = new Model1())
                {
                    var idEspecialidad = db.Tecnico
                        .Where(tec => tec.Id_Tecnico == id)
                        .Select(tec => tec.Id_Especialidad)
                        .FirstOrDefault();

                    if (idEspecialidad != 0)
                    {
                        tiposEspecialidad = (from tipoes in db.Tipo_Especialidad
                                             where tipoes.Id_Especialidad == idEspecialidad
                                             && !db.RTecnico_TipoEspecialidad.Any(r => r.Id_Tecnico == id && r.Id_Tipo_Especialidad == tipoes.Id_Tipo_Especialidad)
                                             select tipoes).ToList();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tiposEspecialidad;
        }



        public List<Tipo_Especialidad> Listar1(int id)
        {
            var tiposEspecialidad = new List<Tipo_Especialidad>();
            try
            {
                using (var db = new Model1())
                {
                    var idEspecialidad = db.Tecnico
                        .Where(tec => tec.Id_Tecnico == id)
                        .Select(tec => tec.Id_Especialidad)
                        .FirstOrDefault();

                    if (idEspecialidad != 0)
                    {
                        tiposEspecialidad = (from tipoes in db.Tipo_Especialidad
                                             where tipoes.Id_Especialidad == idEspecialidad
                                             select tipoes).ToList();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tiposEspecialidad;
        }


        //public List<Tipo_Especialidad> Listar1(int id = 0)
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

        //            if (idEspecialidad != 0)
        //            {
        //                tiposEspecialidad = db.Tipo_Especialidad
        //                    .Where(te => te.Id_Especialidad == idEspecialidad)
        //                    .ToList();

        //                // Validar el campo "Id_Especialidad" de la tabla "Tipo_Especialidad" con el campo "id_Especialidad" de la tabla "Especialidad"
        //                tiposEspecialidad = tiposEspecialidad.Join(
        //                    db.Especialidad,
        //                    tipo => tipo.Id_Especialidad,
        //                    especialidad => especialidad.Id_Especialidad,
        //                    (tipo, especialidad) => tipo)
        //                    .ToList();
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return tiposEspecialidad;
        //}







    }
}
