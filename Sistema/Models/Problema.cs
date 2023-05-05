namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Runtime.Remoting;
    using System.Web.Mvc;

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

        [StringLength(100)]
        public string Documento { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        //[Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime Fecha_Inicio { get; set; }

        //[Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime Fecha_Fin { get; set; }

        public int Id_Cliente { get; set; }

        public virtual Cliente Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Servicio> Servicio { get; set; }


        //Model1 db = new Model1();


        public List<Problema> Listar()
        {
            var problema = new List<Problema>();
            try
            {
                using (var db = new Model1())
                {
                    problema = db.Problema.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return problema;

        }

        public Problema Obtener(int id)
        {
            var sc = new Problema();
            try
            {
                using (var db = new Model1())
                {
                    sc = db.Problema
                        .Include("Cliente")
                        .Where(x => x.Id_Cliente == id)
                                .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return sc;
        }
        public List<Problema> ListarProblema(int id)
        {
            var misproblemas = new List<Problema>();
            try
            {
                using (var db = new Model1())
                {
                    var icliente = db.Cliente.Where(u => u.Id_Usuario == id).Select(u => u.Id_Cliente).SingleOrDefault();

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
