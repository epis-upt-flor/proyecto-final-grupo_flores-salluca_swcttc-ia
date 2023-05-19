namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Problema")]
    public partial class Problema
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Problema()
        {
            Servicio = new HashSet<Servicio>();
        }

        [Key]
        public int Id_Problema { get; set; }

        [StringLength(100)]
        public string Descripcion { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Inicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Fin { get; set; }

        public int Id_Cliente { get; set; }

        public virtual Cliente Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Servicio> Servicio { get; set; }



        /// <summary>
        /// Esta función de C# recupera una lista de problemas en función de un ID determinado.
        /// </summary>
        /// <param name="id">El parámetro "id" es un valor entero que se utiliza para filtrar la lista de
        /// objetos "Problema" según su propiedad "Id_Problema". El método devuelve una lista de objetos
        /// "Problema" que tienen el mismo valor "Id_Problema" que el</param>
        /// <returns>
        /// Una lista de objetos Problema que tienen un Id_Problema igual al parámetro de entrada "id".
        /// </returns>
        public List<Problema> Listar(int id)
        {
            var problemas = new List<Problema>();
            try
            {
                using (var db = new Model1())
                {
                    problemas = db.Problema
                        .Include("Cliente")
                        .Include("Servicio")
                        .Where(p => p.Id_Problema == id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return problemas;
        }


        /// <summary>
        /// Esta función recupera una lista de problemas pendientes de una base de datos, incluida la
        /// información relacionada con el cliente.
        /// </summary>
        /// <returns>
        /// Una lista de objetos "Problema" que tienen la propiedad "Estado" establecida en "Pendiente" y
        /// se recuperan de la base de datos junto con sus objetos "Cliente".
        /// </returns>
        public List<Problema> ListProblem()
        {
            var problemas = new List<Problema>();
            try
            {
                using (var db = new Model1())
                {
                    problemas = db.Problema
                           .Include("Cliente")
                           .Where(s => s.Estado == "Pendiente")
                     .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return problemas;
        }

        /// <summary>
        /// Esta función de C# recupera un objeto problemático de una base de datos por su ID, incluida
        /// la información del cliente relacionada.
        /// </summary>
        /// <param name="id">un número entero que representa el identificador único del problema que se
        /// recuperará de la base de datos.</param>
        /// <returns>
        /// El método devuelve un objeto de tipo "Problema".
        /// </returns>

        public Problema Obtener(int id)
        {
            var sc = new Problema();
            try
            {
                using (var db = new Model1())
                {
                    sc = db.Problema
                        .Include("Cliente")
                        .Where(x => x.Id_Problema == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return sc;
        }


        /// <summary>
        /// Esta función guarda los cambios realizados en una entidad de base de datos.
        /// </summary>
        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.Id_Problema > 0)
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


        ///METODO Eliminar
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


        public List<Problema> ListarProblema(int id)
        {
            var misproblemas = new List<Problema>();
            try
            {
                using (var db = new Model1())
                {
                    var icliente = db.Cliente
                        .Where(u => u.Id_Usuario == id)
                        .Select(u => u.Id_Cliente)
                        .SingleOrDefault();
                    if (icliente != 0)
                    {
                        misproblemas = db.Problema
                            .Include("Cliente")
                            .Where(x => x.Id_Cliente == icliente)
                            .ToList();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return misproblemas;
        }

        public int ObtenerIdCliente(int id)
        {
            int idCliente = 0;
            try
            {
                using (var db = new Model1())
                {
                    var cliente = db.Cliente.SingleOrDefault(c => c.Id_Usuario == id);
                    if (cliente != null)
                    {
                        idCliente = cliente.Id_Cliente;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return idCliente;
        }
    }
}
