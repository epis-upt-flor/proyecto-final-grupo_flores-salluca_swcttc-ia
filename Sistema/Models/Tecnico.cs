/*
Versión: 1.0
Descripción: Tecnico.cs Representada gestionar calificaciòn
Para el caso de uso: 
    Gestionar Calificaciòn
    Gestionar Progreso

Fecha de creación: [07/08/2023]
Creado por: [DJFN]

Última modificación: [11/11/2023]
Modificado por: [JFSV]
*/

namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Tecnico")]
    public partial class Tecnico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tecnico()
        {
            RTecnico_TipoEspecialidad = new HashSet<RTecnico_TipoEspecialidad>();
            Servicio = new HashSet<Servicio>();
        }

        [Key]
        public int Id_Tecnico { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; }

        public int Id_Especialidad { get; set; }

        public int Id_Estado_Tecnico { get; set; }

        public int Id_Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Archivos> Archivos { get; set; }

        public virtual Especialidad Especialidad { get; set; }

        public virtual Estado_Tecnico Estado_Tecnico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RTecnico_TipoEspecialidad> RTecnico_TipoEspecialidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Servicio> Servicio { get; set; }

        public virtual Usuario Usuario { get; set; }



        /// <summary>
        /// Esta función recupera la información de un técnico de una base de datos basada en su ID de
        /// usuario.
        /// </summary>
        /// <param name="id">un número entero que representa la ID del técnico que se recuperará de la
        /// base de datos.</param>
        /// <returns>
        /// El método está devolviendo un objeto de tipo "Tecnico".
        /// </returns>
        public Tecnico Obtener(int id)
        {
            var tecnicos = new Tecnico();
            try
            {
                using (var db = new Model1())
                {
                    tecnicos = db.Tecnico.Include("Estado_Tecnico")
                        //.Include("Especialidad")
                        .Include("Usuario")
                        .Where(x => x.Id_Usuario == id)
                                .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tecnicos;
        }


        /// <summary>
        /// La función guarda un registro en una base de datos utilizando Entity Framework, ya sea como
        /// un registro nuevo o como una actualización de un registro existente.
        /// </summary>
        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.Id_Tecnico > 0)
                    {
                        db.Entry(this).State = EntityState.Modified; //existe
                    }
                    else
                    {
                        db.Entry(this).State = EntityState.Added; //nuevo registro
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Esta función recupera una lista de todos los técnicos de una base de datos.
        /// </summary>
        /// <returns>
        /// Una lista de objetos de tipo "Tecnico".
        /// </returns>
        public List<Tecnico> Listar()
        {
            var tecnico = new List<Tecnico>();
            try
            {
                using (var db = new Model1())
                {
                    tecnico = db.Tecnico.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tecnico;
        }


        //Buscar
        /// <summary>
        /// Esta función busca técnicos según un criterio dado y devuelve una lista de técnicos
        /// coincidentes.
        /// </summary>
        /// <param name="criterio">una cadena que representa los criterios de búsqueda que se utilizarán
        /// para encontrar objetos Tecnico coincidentes en la base de datos. Puede ser el nombre o
        /// apellido de un Técnico.</param>
        /// <returns>
        /// Una lista de objetos de tipo "Tecnico" que coinciden con los criterios de búsqueda
        /// especificados por el parámetro "criterio".
        /// </returns>
        public List<Tecnico> Buscar(string criterio)
        {
            var categorias = new List<Tecnico>();
            try
            {
                using (var db = new Model1())
                {
                    categorias = db.Tecnico
                        .Include("Tipo_Usuario")
                        .Where(x => x.Nombre.Contains(criterio) ||
                                x.Apellido.Contains(criterio))//|| 
                                                              // x.Especialidad.Contains(criterio))
                        .ToList();

                }
            }
            catch (Exception)
            {
                throw;
            }

            return categorias;

        }

        //Listar tecnico especificos recomendados
        public List<Tecnico> ListarRecomendado(List<int> numeros)
        {
            List<Tecnico> tecnicos = new List<Tecnico>();
            try
            {
                using (var db = new Model1())
                {
                    tecnicos = db.Tecnico.Where(t => numeros.Contains(t.Id_Tecnico)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tecnicos;
        }


        public int ObtenerTotalTecnicosDisponibleNoDisponibles()
        {
            int contador = 0;
            try
            {
                using (var db = new Model1())
                {
                    contador = db.Tecnico
                        .Count(tec => tec.Id_Estado_Tecnico == 1 || tec.Id_Estado_Tecnico == 2);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return contador;
        }

        public int ObtenerTotalTecnicoDisponibles()
        {
            int contador = 0;
            try
            {
                using (var db = new Model1())
                {
                    contador = db.Tecnico
                        .Count(tec => tec.Id_Estado_Tecnico == 1);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return contador;
        }

        public int ObtenerTotalTipoEspecialidadPorTecnico(int id)
        {
            int contador = 0;
            try
            {
                using (var db = new Model1())
                {
                    contador = db.Tecnico
                        .Join(db.RTecnico_TipoEspecialidad, tec => tec.Id_Tecnico, rt => rt.Id_Tecnico, (tec, rt) => new { Tec = tec, RTec = rt })
                        .Join(db.Tipo_Especialidad, rt => rt.RTec.Id_Tipo_Especialidad, tip => tip.Id_Tipo_Especialidad, (rt, tip) => new { RTec = rt.RTec, Tip = tip })
                        .Count(st => st.RTec.Id_Tecnico == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return contador;
        }






    }
}
