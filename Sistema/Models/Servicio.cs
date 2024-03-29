/*
Versión: 1.0
Descripción: Servicio.cs Representada gestionar calificaciòn
Para el caso de uso: 
    Gestionar Calificaciòn
    Gestionar Progreso
    Gestionar Asignación Trabajo

Fecha de creación: [07/08/2023]
Creado por: [DJFN]

Última modificación: [11/11/2023]
Modificado por: [JFSV]
*/


using SistemaArtemis.Models;
using System.Collections.Generic;
using System;

namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Web.Mvc;
    using System.Web.Services.Description;

    [Table("Servicio")]
    public partial class Servicio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Servicio()
        {
            Calificacion = new HashSet<Calificacion>();
        }

        [Key]
        public int Id_Servicio { get; set; }

        [Required]
        [StringLength(250)]
        public string Descripcion { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Inicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha_Fin { get; set; }

        [StringLength(250)]
        public string Documento { get; set; }

        public int Id_Tecnico { get; set; }

        public int Id_Problema { get; set; }

        public int Id_Estado_Servicio { get; set; }    

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Calificacion> Calificacion { get; set; }

        public virtual Estado_Servicio Estado_Servicio { get; set; }

        public virtual Problema Problema { get; set; }

        public virtual Tecnico Tecnico { get; set; }

        public List<Servicio> data()
        {
            var datos=new List<Servicio>();
            try
            {
                using (var db = new Model1())
                {
                    var resultados = db.Tecnico
                        .Join(db.Servicio, t => t.Id_Tecnico, s => s.Id_Tecnico, (t, s) => new { Tecnico = t, Servicio = s })
                        .Join(db.Calificacion, ts => ts.Servicio.Id_Servicio, c => c.Id_Servicio, (ts, c) => new { ts.Tecnico, ts.Servicio, Calificacion = c })
                        .Join(db.Codigo, tsc => tsc.Calificacion.Id_Codigo, cd => cd.Id_Codigo, (tsc, cd) => new
                        {
                            IdTecnico = tsc.Tecnico.Id_Tecnico,
                            Nombre = tsc.Tecnico.Nombre,
                            Apellido = tsc.Tecnico.Apellido,
                            IdCodigo = cd.Id_Codigo,
                            Puntaje = cd.Descripcion,
                            Servicio = tsc.Servicio // Agrega la propiedad Servicio al resultado
                        })
                        .Where(result => result.Servicio.Id_Estado_Servicio == 3)
                        .ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return datos;
        }
        public List<Servicio> Listar()  //ok
        {
            var serv = new List<Servicio>();
            try
            {
                using (var db = new Model1())
                {
                    serv = db.Servicio
                           .Include("Tecnico")
                           .Include("Problema")
                           .Where(s => s.Id_Estado_Servicio == 1) // cambie validar
                     .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serv;
        }
        public List<Servicio> ListarTrabajosAceptados()  //ok
        {
            var serv = new List<Servicio>();
            try
            {
                using (var db = new Model1())
                {
                    serv = db.Servicio
                           .Include("Tecnico")
                           .Include("Problema")
                           .Where(s => s.Id_Estado_Servicio == 3) // cambie validar
                     .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serv;
        }

        /// <summary> Esta función recupera una lista de problemas asociados con una identificación de cliente específica.
        /// <param name="id">El parámetro id es un número entero que representa el id del usuario para el que queremos listar los problemas.</param>
        /// <returns>El método devuelve una lista de objetos Problema.
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


        /// <summary> La función recupera una lista de servicios asignados a un técnico con una identificación de usuario específica y un estado de servicio específico.
        /// <param name="id">El parámetro id es un número entero que representa el id del usuario para el que se debe recuperar la lista de servicios.</param>
        /// <returns>El método devuelve una lista de objetos de Servicio.
        public List<Servicio> MisServicios(int id)
        {
            var misservicios = new List<Servicio>();
            try
            {
                using (var db = new Model1())
                {
                    var itecnico = db.Tecnico
                        .Where(u => u.Id_Usuario == id)
                        .Select(u => u.Id_Tecnico)
                        .SingleOrDefault();
                    if (itecnico != 0)
                    {
                        misservicios = db.Servicio
                            .Include("Tecnico")
                            .Include("Estado_Servicio")
                            .Include("Problema")
                            .Where(x => x.Id_Tecnico == itecnico)// && x.Id_Estado_Servicio == 2)
                            .ToList();
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            return misservicios;
        }



        public List<Servicio> Mispostulantes()
        {
            var serv = new List<Servicio>();
            try
            {
                using (var db = new Model1())
                {
                    serv = db.Servicio
                           .Include("Tecnico")
                           .Include("Problema")
                           .Where(s => s.Id_Estado_Servicio != 3) // cambie validar
                     .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return serv;
        }

        /// <summary>Esta función guarda los cambios realizados en un objeto de servicio en una base de datos utilizando Entity Framework.
        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.Id_Servicio > 0)
                    {
                        db.Entry(this).State = EntityState.Added;
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


        //Servicio Obtener(int id)
        public List<Servicio> Obtener(int id)
        {
            var servicio = new List<Servicio>();
            try
            {
                using (var db = new Model1())
                {
                    servicio = db.Servicio.Include("Problema")
                        .Where(x => x.Id_Problema == id)
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return servicio;
        }

        public List<Servicio> Detalles(int id)
        {
            var detalles = new List<Servicio>();
            try
            {
                using (var db = new Model1())
                {
                    detalles = (from tec in db.Tecnico
                                join serv in db.Servicio on tec.Id_Tecnico equals serv.Id_Tecnico
                                join pro in db.Problema on serv.Id_Problema equals pro.Id_Problema
                                where serv.Id_Problema == id
                                select new
                                {
                                    Tecnico = tec,
                                    Id_Estado_Servicio = serv.Id_Estado_Servicio,
                                    Id_Problema = serv.Id_Problema,
                                    Documento = serv.Documento
                                }).AsEnumerable()
                              .Select(x => new Servicio
                              {
                                  Tecnico = x.Tecnico,
                                  Id_Estado_Servicio = x.Id_Estado_Servicio,
                                  Documento = x.Documento,
                                  Id_Problema= x.Id_Problema
                              })
                              .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return detalles;
        }

        public List<Servicio> ListService()  //ok
        {
            var servi = new List<Servicio>();
            try
            {
                using (var db = new Model1())
                {
                    servi = db.Servicio
                           .Include("Tecnico")                        
                           .Where(s => s.Id_Estado_Servicio == 3)
                     .ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return servi;
        }

        public int ObtenerTotalServiciosPorTecnico(int id)
        {
            int contador = 0;
            try
            {
                using (var db = new Model1())
                {
                    contador = db.Servicio
                        .Join(db.Tecnico, ser => ser.Id_Tecnico, tec => tec.Id_Tecnico, (ser, tec) => new { Ser = ser, Tec = tec })
                        .Count(st => st.Tec.Id_Tecnico == id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return contador;
        }

        public int ObtenerTotalServiciosFaltaAprobarEstado(int id)
        {
            int contador = 0;
            try
            {
                using (var db = new Model1())
                {
                    contador = db.Servicio
                        .Join(db.Tecnico, ser => ser.Id_Tecnico, tec => tec.Id_Tecnico, (ser, tec) => new { Ser = ser, Tec = tec })
                        .Count(st => st.Tec.Id_Tecnico == id && st.Ser.Id_Estado_Servicio == 2);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return contador;
        }

        public int ObtenerTotalServiciosEnProceso(int id)
        {
            int contador = 0;
            try
            {
                using (var db = new Model1())
                {
                    contador = db.Servicio
                        .Join(db.Tecnico, ser => ser.Id_Tecnico, tec => tec.Id_Tecnico, (ser, tec) => new { Ser = ser, Tec = tec })
                        .Count(st => st.Tec.Id_Tecnico == id && st.Ser.Id_Estado_Servicio == 3);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return contador;
        }


        public Servicio DetallesServicio(int id)
        {
            Servicio servicio = null;

            try
            {
                using (var db = new Model1())
                {
                    servicio = db.Servicio
                        .Include("Tecnico")
                        .Include("Problema")
                        .Where(x => x.Id_Servicio == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return servicio;
        }

        public int BuscarIdTecnico(int id)
        {
            int idTecnico = 0;

            try
            {
                using (var db = new Model1())
                {
                    idTecnico = db.Servicio
                        .Where(x => x.Id_Servicio == id)
                        .Select(x => x.Id_Tecnico)
                        .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return idTecnico;
        }


        public List<int> ListaServicioTecnico(int idTecnico)
        {
            List<int> serviciosRealizados = new List<int>();

            try
            {
                using (var db = new Model1())
                {
                    serviciosRealizados = db.Servicio
                        .Where(x => x.Id_Tecnico == idTecnico && x.Id_Estado_Servicio == 4)
                        .Select(x => x.Id_Servicio)
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return serviciosRealizados;
        }




    }
}


