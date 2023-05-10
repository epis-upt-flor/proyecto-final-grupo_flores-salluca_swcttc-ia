using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
//using SistemaArtemis.Logica;

namespace SistemaArtemis.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Esta es una función de C# que maneja una solicitud POST para iniciar sesión en un usuario y
        /// redirigirlo a la página adecuada según su tipo de usuario.
        /// </summary>
        /// <param name="Usuario">Una clase que representa a un usuario, con propiedades como Correo
        /// electrónico (correo electrónico), Contraseña, User_Type_Id (ID de tipo de usuario) y User_Id
        /// (ID de usuario).</param>
        /// <returns>
        /// El método devuelve un ActionResult, que puede ser una redirección a otra acción o vista, o
        /// una vista con datos.
        /// </returns>
        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_obtenerUsuario", oConexion);
                cmd.Parameters.AddWithValue("Correo", usuario.Correo);
                cmd.Parameters.AddWithValue("Contrasena", usuario.Password);
                cmd.CommandType = CommandType.StoredProcedure;

                oConexion.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    usuario.Id_Tipo_Usuario = Convert.ToInt32(reader["Id_Tipo_Usuario"]);
                    usuario.Id_Usuario = Convert.ToInt32(reader["Id_Usuario"]);
                    Session["Correo"] = usuario.Correo;
                }                 
            }

            /* Este bloque de código verifica el tipo de usuario que intenta iniciar sesión en función
            de su propiedad `Id_Tipo_Usuario`. Si el usuario es administrador (tipo 1), el método lo
            redirige a la acción `Index` del controlador `Administrador`. Si el usuario es un
            técnico (tipo 2), el método recupera la propiedad `Id_Tecnico` del técnico y la almacena
            en la sesión antes de redirigirlo a la acción `Índice` del controlador `Tecnico`. Si el
            usuario es un cliente (tipo 3), el método recupera la propiedad `Id_Cliente` del cliente
            y la almacena en la sesión antes de redirigirlo a la acción `Índice` del controlador
            `Cliente`. Si el usuario no es uno de estos tipos, el método establece un mensaje en el
            objeto 'TempData' y devuelve la vista 'Índice'. */
            if (usuario.Id_Tipo_Usuario == 1)
            {               
                return RedirectToAction("Index", "Administrador");
            }
            else if (usuario.Id_Tipo_Usuario == 2)
            {
                var tecnico = new Tecnico().Listar().Find(x => x.Id_Usuario == usuario.Id_Usuario);
                Session["Id_Tecnico"] = tecnico.Id_Tecnico;
                return RedirectToAction("Index", "Tecnico");
            }
            else if (usuario.Id_Tipo_Usuario == 3)
            {
                var cliente = new Cliente().Listar().Find(x => x.Id_Usuario == usuario.Id_Usuario);
                Session["Id_Cliente"] = cliente.Id_Cliente;
                return RedirectToAction("Index", "Cliente");
            }
            else
            {
                TempData["mensaje"] = "Tu usuario o contraseña son incorrectos";
                return View();
            }

        }

      /// <summary>
      /// La función comprueba si un usuario determinado es válido mediante la autenticación de sus
      /// credenciales.
      /// </summary>
      /// <param name="Usuario">"Usuario" es una clase o tipo de dato que representa a un usuario en el
      /// sistema. El método "IsValid" toma una instancia de esta clase como parámetro llamado
      /// "usuario".</param>
      /// <returns>
      /// El método `IsValid` devuelve un valor booleano. Está devolviendo el resultado de llamar al
      /// método `Autenticar` sobre el objeto `usuario`.
      /// </returns>
        private bool IsValid(Usuario usuario)
        {
            return usuario.Autenticar();
        }



        // GET: Login
        /// <summary>
        /// Esta función devuelve una vista para registrar un nuevo usuario con valores predeterminados
        /// para el nombre, el correo electrónico, la contraseña y el tipo de usuario del usuario.
        /// </summary>
        /// <returns>
        /// Una vista con una nueva instancia de la clase Usuario inicializada con valores
        /// predeterminados para sus propiedades.
        /// </returns>
        public ActionResult Registrarse()
        {
            return View(new Usuario() { Nombre = "", Apellido = "", Correo = "", Password = "", Estado = ' ', Id_Tipo_Usuario = ' ' });
        }




        /// <summary>
        /// Esta es una función de C# que registra un nuevo usuario y lo redirige a la página de inicio de
        /// sesión si tiene éxito.
        /// </summary>
        /// <param name="Usuario">Es una clase que representa a un usuario en el sistema, con propiedades
        /// como Nombre (name), Apellido (apellido), Correo (email), Password (contraseña), Estado (status)
        /// e Id_Tipo_Usuario (identificación del tipo de usuario). El método es una acción HTTP POST para
        /// registrar un nuevo</param>
        /// <returns>
        /// Si el registro es exitoso, el método devuelve una redirección a la acción "Índice" del
        /// controlador "Iniciar sesión". Si hay un error durante el registro, el método devuelve la misma
        /// vista con un mensaje de error.
        /// </returns>
        [HttpPost]
        public ActionResult Registrarse(Usuario usuario)
        {
            Usuario oUsuario = new Usuario()
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                Password = usuario.Password,
                Estado = 1,
                Id_Tipo_Usuario = usuario.Id_Tipo_Usuario,
            };

            int idusuario_respuesta = usuario.Registrar(oUsuario);

            if (idusuario_respuesta == 0)
            {
                ViewBag.Error = "Error al registrar";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }


        }
       
        /// <summary>
        /// La función Cerrar sesión cierra la sesión del usuario, abandona la sesión y redirige a la página
        /// de inicio.
        /// </summary>
        /// <returns>
        /// El método devuelve un resultado `RedirectToAction` que redirige al usuario a la acción "Inicio"
        /// del controlador "Índice".
        /// </returns>
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Home", "Index");
        }
    }
}


