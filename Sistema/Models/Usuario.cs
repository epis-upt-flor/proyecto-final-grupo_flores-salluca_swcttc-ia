namespace SistemaArtemis.Models
{
    using SistemaArtemis.Models.Login;
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

        /// <summary>
        /// La función registra un nuevo usuario en una base de datos mediante procedimientos
        /// almacenados y devuelve un valor entero como respuesta.
        /// </summary>
        /// <returns>
        /// El método devuelve un valor entero, que es el resultado de la ejecución del procedimiento almacenado.
        /// </returns>
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

                    /* `oConexion.Open();` está abriendo una conexión a la base de datos usando el objeto SqlConnection `oConexion`. Esto es necesario antes de ejecutar cualquier comando SQL en la base de datos. */
                    oConexion.Open();

                    /* `cmd.ExecuteNonQuery();` está ejecutando el procedimiento almacenado
                    `sp_registrarUsuario` en la base de datos usando el objeto `SqlCommand` `cmd`.*/
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToInt32(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception)
                {
                    respuesta = 0;
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Esta función recupera una lista de usuarios de una base de datos, incluidos sus tipos de
        /// usuarios asociados.
        /// </summary>
        /// <returns>
        /// Una lista de objetos de usuario, que incluye el objeto User_Type relacionado.
        /// </returns>

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



        /// <summary>
        /// Esta función recupera un usuario por su ID de una base de datos, incluido su tipo de
        /// usuario.
        /// </summary>
        /// <param name="id">un número entero que representa el identificador único del usuario que se
        /// recuperará de la base de datos.</param>
        /// <returns>
        /// El método devuelve una única instancia de la clase "Usuario" que coincide con el parámetro
        /// "id" proporcionado. La instancia se obtiene de una base de datos utilizando Entity Framework
        /// e incluye la entidad "Tipo_Usuario" relacionada.
        /// </returns>
        public Usuario Obtener(int id)
        {
            var usuarios = new Usuario();
            try
            {
                using (var db = new Model1())
                {
                    usuarios = db.Usuario
                        .Include("Tipo_Usuario")
                        .Include("Tecnico")
                        .Where(x => x.Id_Usuario == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return usuarios;
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

        /// <summary>
        /// Esta función busca usuarios en una base de datos según un criterio dado.
        /// </summary>
        /// <param name="criterio">una cadena que representa los criterios de búsqueda que se utilizarán
        /// para encontrar registros coincidentes en la base de datos. El método busca registros donde
        /// las propiedades Nombre, Apellido, Correo o Tipo_Usuario.Nombre contengan los criterios de
        /// búsqueda especificados.</param>
        /// <returns>
        /// Una lista de objetos Usuario que coinciden con los criterios de búsqueda especificados por
        /// la cadena de entrada "criterio".
        /// </returns>

        public List<Usuario> Buscar(string criterio)
        {
            var categorias = new List<Usuario>();
            try
            {
                using (var db = new Model1())
                {
                    categorias = db.Usuario
                        .Include("Tipo_Usuario")
                        .Where(x => x.Nombre.Contains(criterio)
                        || x.Apellido.Contains(criterio)
                        || x.Correo.Contains(criterio)
                        || x.Tipo_Usuario.Nombre.Contains(criterio))
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return categorias;
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //LOGIN
        /// <summary>
        /// Esta función verifica si el correo electrónico y la contraseña de un usuario coinciden con
        /// los almacenados en la base de datos.
        /// </summary>
        /// <returns>
        /// Un valor booleano que indica si el usuario con el correo electrónico y la contraseña dados
        /// existe en la base de datos o no.
        /// </returns>
        public bool Autenticar()
        {
            return db.Usuario
                   .Where(x => x.Correo == this.Correo
                   && x.Password == this.Password)
                   .FirstOrDefault() != null;
        }

        //obtener datos del login
        /// <summary>
        /// Esta función de C# recupera datos de usuario de una base de datos en función de su dirección
        /// de correo electrónico.
        /// </summary>
        /// <param name="Correo">La dirección de correo electrónico del usuario cuyos datos se están
        /// recuperando.</param>
        /// <returns>
        /// El método devuelve un objeto de tipo "Usuario", que contiene información sobre un usuario,
        /// incluida su dirección de correo electrónico e información relacionada, como su tipo de
        /// usuario (por ejemplo, técnico o cliente).
        /// </returns>

        public Usuario ObtenerDatos(string Correo)
        {
            var usuario = new Usuario();
            try
            {
                using (var db = new Model1())
                {
                    usuario = db.Usuario
                        .Include("Tecnico")                      
                        .Include("Cliente")
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



        /// <summary>
        /// La función "Acceder" verifica si el correo electrónico y la contraseña de un usuario
        /// coinciden con los almacenados en la base de datos y devuelve una respuesta que indica si el
        /// inicio de sesión fue exitoso o no.
        /// </summary>
        /// <param name="Correo">Una cadena que representa la dirección de correo electrónico del usuario
        /// que intenta iniciar sesión.</param>
        /// <param name="Password">Una variable de cadena que contiene la contraseña ingresada por el
        /// usuario.</param>
        /// <returns>
        /// El método devuelve un objeto ResponseModel.
        /// </returns>

        public ResponseModel Acceder(string Correo, string Password)
        {
            var rm = new ResponseModel();
            try
            {
                using (var db = new Model1())
                {
                    Password = HashHelper.MD5(Password);

                    var usuario = db.Usuario.Where(x => x.Correo == Correo)
                                            .Where(x => x.Password == Password)
                                            .SingleOrDefault();
                    if (usuario != null)
                    {
                        SessionHelper.AddUserToSession(Id_Usuario.ToString());
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.SetResponse(false, "Usuario y/o Password incorrectos...");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rm;
        }
    }
}
