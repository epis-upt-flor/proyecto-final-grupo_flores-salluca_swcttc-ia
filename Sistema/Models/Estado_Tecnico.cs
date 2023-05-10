namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Estado_Tecnico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Estado_Tecnico()
        {
            Tecnico = new HashSet<Tecnico>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Estado_Tecnico { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tecnico> Tecnico { get; set; }


        /// <summary>
        /// Esta función de C# recupera una lista de objetos Estado_Tecnico de una base de datos.
        /// </summary>
        /// <returns>
        /// Una lista de objetos de tipo Estado_Tecnico.
        /// </returns>
        public List<Estado_Tecnico> Listar()
        {
            var sc = new List<Estado_Tecnico>();
            try
            {
                using (var db = new Model1())
                {
                    sc = db.Estado_Tecnico
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return sc;
        }


        /// <summary>
        /// Esta función de C# busca una lista de objetos Estado_Tecnico en función de un criterio dado.
        /// </summary>
        /// <param name="criterio">una cadena que representa el criterio de búsqueda de los objetos
        /// Estado_Tecnico. El método busca objetos Estado_Tecnico cuya propiedad Descripción contenga
        /// la cadena de criterio especificada.</param>
        /// <returns>
        /// Una lista de objetos Estado_Tecnico que coinciden con los criterios de búsqueda.
        /// </returns>

        public List<Estado_Tecnico> Buscar(string criterio)
        {
            var estadotecnico = new List<Estado_Tecnico>();
            try
            {
                using (var db = new Model1())
                {
                    estadotecnico = db.Estado_Tecnico
                        .Where(x => x.Descripcion.Contains(criterio))
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return estadotecnico;
        }


        /// <summary>
        /// Esta función de C# recupera un solo objeto Estado_Tecnico de una base de datos en función de
        /// su ID.
        /// </summary>
        /// <param name="id">un número entero que representa el identificador único del objeto
        /// Estado_Tecnico que debe recuperarse de la base de datos.</param>
        /// <returns>
        /// El método está devolviendo un objeto de tipo Estado_Tecnico.
        /// </returns>
        public Estado_Tecnico Obtener(int id)
        {
            var estadotecnico = new Estado_Tecnico();
            try
            {
                using (var db = new Model1())
                {
                    estadotecnico = db.Estado_Tecnico
                        .Where(x => x.Id_Estado_Tecnico == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return estadotecnico;
        }

       /// <summary>
       /// La función guarda los cambios realizados en una entidad de base de datos.
       /// </summary>
        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.Id_Estado_Tecnico > 0)
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

        /// <summary>
        /// Esta función elimina una entidad de una base de datos mediante Entity Framework en C#.
        /// </summary>
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

    }
}
