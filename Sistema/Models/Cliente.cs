namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Cliente")]
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            Calificacion = new HashSet<Calificacion>();
            Problema = new HashSet<Problema>();
        }

        [Key]
        public int Id_Cliente { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; }

        public int? Estado { get; set; }

        public int Id_Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calificacion> Calificacion { get; set; }

        public virtual Usuario Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Problema> Problema { get; set; }


        /// <summary>
        /// Esta función recupera una lista de clientes con su información de usuario asociada en función de una
        /// ID de usuario dada.
        /// </summary>
        /// <param name="id">El parámetro "id" es un valor entero que se utiliza para filtrar la lista de
        /// clientes en función del ID del usuario asociado. El método devuelve una lista de clientes que tienen
        /// un ID de usuario que coincide con el parámetro "id" proporcionado.</param>
        /// <returns>
        /// Una lista de objetos Cliente que tienen un Usuario con la identificación especificada.
        /// </returns>

        public List<Cliente> Listar1(int id)
        {
            var sc = new List<Cliente>();
            try
            {
                using (var db = new Model1())
                {
                    sc = db.Cliente.Include(c => c.Usuario)
                                   .Where(c => c.Usuario.Id_Usuario == id)
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
        /// Esta función recupera un objeto de cliente de una base de datos en función de un ID de usuario
        /// dado.
        /// </summary>
        /// <param name="id">El parámetro "id" es un valor entero que representa el identificador único de un
        /// cliente en una base de datos. Este método recupera un objeto de cliente de la base de datos en
        /// función del valor "id" proporcionado.</param>
        /// <returns>
        /// El método devuelve una única instancia de la clase "Cliente", que se obtiene de una base de datos
        /// utilizando el parámetro "id" proporcionado. El método utiliza Entity Framework para consultar la
        /// base de datos e incluye la entidad "Usuario" relacionada. Si no se encuentra ningún registro
        /// coincidente, el método devuelve nulo.
        /// </returns>
        public Cliente Obtener(int id)
        {
            var clientes = new Cliente();
            try
            {
                using (var db = new Model1())
                {
                    clientes = db.Cliente
                        .Include("Usuario")
                        .Where(x => x.Id_Usuario == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return clientes;
        }



        /// <summary>
        /// La función guarda un registro en una base de datos utilizando Entity Framework, ya sea como
        /// un registro nuevo o como uno existente.
        /// </summary>
        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.Id_Cliente > 0)
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
        /// Esta función de C# recupera una lista de clientes de una base de datos mediante Entity Framework.
        /// </summary>
        /// <returns>
        /// Una lista de objetos de tipo "Cliente".
        /// </returns>
        public List<Cliente> Listar()
        {
            var sc = new List<Cliente>();
            try
            {
                using (var db = new Model1())
                {
                    sc = db.Cliente.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return sc;
        }


        public int ObtenerTotalServiciosCliente(int id)
        {
            int contador = 0;
            try
            {
                using (var db = new Model1())
                {
                    contador = db.Servicio
                        .Join(db.Problema, Ser => Ser.Id_Problema, Pro => Pro.Id_Problema, (Ser, Pro) => new { Ser, Pro })
                        .Join(db.Cliente, Temp => Temp.Pro.Id_Cliente, Cli => Cli.Id_Cliente, (Temp, Cli) => new { Temp.Ser, Temp.Pro, Cli })
                        .Where(Temp => Temp.Cli.Id_Cliente == id)
                        .Count();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return contador;
        }


        public int ObtenerTotalServiciosClienteEstadoEnProceso(int id)
        {
            int total = 0;
            try
            {
                using (var db = new Model1())
                {
                    total = db.Servicio
                        .Join(db.Problema, ser => ser.Id_Problema, pro => pro.Id_Problema, (ser, pro) => new { Ser = ser, Pro = pro })
                        .Join(db.Cliente, sp => sp.Pro.Id_Cliente, cli => cli.Id_Cliente, (sp, cli) => new { Sp = sp, Cli = cli })
                        .Where(spcli => spcli.Cli.Id_Cliente == id && spcli.Sp.Ser.Id_Estado_Servicio == 3)
                        .Count();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return total;
        }




    }
}
