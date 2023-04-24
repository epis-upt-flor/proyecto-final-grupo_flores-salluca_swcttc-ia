namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
    using System.Linq;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            Cliente = new HashSet<Cliente>();
            Tecnico = new HashSet<Tecnico>();
        }

        [Key]
        public int Id_Usuario { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(50)]
        public string Apellido { get; set; }

        [StringLength(100)]
        public string Correo { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public int? Estado { get; set; }

        public int? Id_Tipo_Usuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cliente> Cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tecnico> Tecnico { get; set; }

        public virtual Tipo_Usuario Tipo_Usuario { get; set; }


        Model1 db = new Model1();


        public int Registrar(Usuario oUsuario)
        {
            int respuesta = 0;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarUsuario", oConexion);
                    cmd.Parameters.AddWithValue("Nombre", oUsuario.Nombre);
                    cmd.Parameters.AddWithValue("Apellido", oUsuario.Apellido);
                    cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                    cmd.Parameters.AddWithValue("Password", oUsuario.Password);
                    cmd.Parameters.AddWithValue("Estado", oUsuario.Estado);
                    cmd.Parameters.AddWithValue("Id_Tipo_Usuario", oUsuario.Id_Tipo_Usuario);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception )
                {
                    respuesta = 0;
                }
            }
            return respuesta;
        }




        public List<Usuario> Listar()
        {
            var usuarios = new List<Usuario>();
            try
            {
                using (var db = new Model1())
                {
                    usuarios = db.Usuario.Include("Tipo_Usuario").ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return usuarios;

        }



        public Usuario Obtener(int id)
        {
            var usuarios = new Usuario();
            try
            {
                using (var db = new Model1())
                {
                    usuarios = db.Usuario.Include("Tipo_Usuario").Where(x => x.Id_Usuario == id)
                                .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return usuarios;
        }


        public void Guardar()
        {
            try
            {
                using (var db = new Model1())
                {
                    if (this.Id_Usuario > 0)
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
            catch (Exception)
            {
                throw;
            }
        }





        public List<Usuario> Buscar(string criterio)
        {
            var categorias = new List<Usuario>();

            try
            {
                using (var db = new Model1())
                {
                    categorias = db.Usuario.Include("Tipo_Usuario").Where(x => x.Nombre.Contains(criterio) ||
                                x.Apellido.Contains(criterio) || x.Correo.Contains(criterio) ||
                                x.Tipo_Usuario.Nombre.Contains(criterio))
                                .ToList();

                }
            }
            catch (Exception)
            {
                throw;
            }

            return categorias;

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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //login
        public bool Autenticar()
        {

            return db.Usuario
                   .Where(x => x.Correo == this.Correo
                   && x.Password == this.Password)
                   .FirstOrDefault() != null;
        }

        //obtener datos del login
        public Usuario ObtenerDatos(string Correo)
        {
            var usuario = new Usuario();
            try
            {
                using (var db = new Model1())
                {
                    usuario = db.Usuario.Include("Tipo_Usuario")
                        .Where(x => x.Correo == Correo)
                        .SingleOrDefault();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return usuario;
        }
       


    }
     
    
}

