﻿using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SistemaArtemis.Controllers
{
    public class LoginController : Controller
    {
        private Tipo_Usuario objTipo = new Tipo_Usuario();
        Model1 db = new Model1();

        public ActionResult Index() 
        {            
            return View();
        }
        /// <summary>Esta es una función de C# que maneja una solicitud POST para iniciar sesión en un usuario y redirigirlo a la página adecuada según su tipo de usuario.
        /// <param name="Usuario">Una clase que representa a un usuario, con propiedades como Correo electrónico (correo electrónico), Contraseña, User_Type_Id (ID de tipo de usuario) y User_Id (ID de usuario).</param>
        /// <returns> El método devuelve un ActionResult, segun el tipo usuario reedirecciona a la vista devuelve tambien Session["Id_Tecnico"] Session["Correo"] 
        /// </returns>

        [HttpPost]
        public ActionResult Index(Usuario usuario)
        {
            usuario.Password = ConvertirSha256(usuario.Password);

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

            if (usuario.Id_Tipo_Usuario == 1)
            {               
                return RedirectToAction("Index", "Administrador");
            }
            else if (usuario.Id_Tipo_Usuario == 2)
            {
                var tecnico = new Tecnico().Listar().Find(x => x.Id_Usuario == usuario.Id_Usuario);
                Session["Id_Tecnico"] = tecnico.Id_Tecnico;         
                return RedirectToAction("Index/"+ tecnico.Id_Tecnico, "Tecnico");
            }
            else if (usuario.Id_Tipo_Usuario == 3)
            {
                var cliente = new Cliente().Listar().Find(x => x.Id_Usuario == usuario.Id_Usuario);
                Session["Id_Cliente"] = cliente.Id_Cliente;
                return RedirectToAction("Index/" + cliente.Id_Cliente, "Cliente");
            }
            else
            {
                TempData["mensaje"] = "Tu usuario o contraseña son incorrectos";
                return View();
            }

        }

        /// <summary> La función comprueba si un usuario determinado es válido mediante la autenticación de credenciales. </summary>  
        /// <returns> El método `IsValid` devuelve un valor booleano. Está devolviendo el resultado de llamar al método `Autenticar` sobre el objeto `usuario`.</returns>
        private bool IsValid(Usuario usuario)  ///ojo
        {
            return usuario.Autenticar();
        }

          
        /// <summary>Esta función devuelve una vista para registrar un nuevo usuario con valores predeterminados para el nombre, el correo electrónico, la contraseña y el tipo de usuario del usuario.
        /// <returns> Una vista con una nueva instancia de la clase Usuario inicializada con valores predeterminados para sus propiedades.   
        public ActionResult Registrarse()
        {
            ViewBag.Tipo = objTipo.Listar();
            return View(new Usuario() { Nombre = "", Apellido = "", Correo = "", Password = "", Estado = ' ', Id_Tipo_Usuario = ' ' });
        }

        /// <summary>Esta es una función de C# que registra un nuevo usuario y lo redirige a la página de inicio de sesión si tiene éxito.
        /// <param name="Usuario">Es una clase que representa a un usuario en el sistema, con propiedades como Nombre (name), Apellido (apellido)</param>
        /// <returns>Si el registro es exitoso, el método devuelve una redirección a la acción "Índice" del  controlador "Iniciar sesión"
       
        [HttpPost]
        public ActionResult Registrarse(Usuario usuario)
        {
            Usuario oUsuario = new Usuario()
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                Password = ConvertirSha256(usuario.Password),
                Estado = 1,
                Id_Tipo_Usuario = usuario.Id_Tipo_Usuario
            };
            int idusuario_respuesta = usuario.Registrar(oUsuario);
            if (idusuario_respuesta == 0)
            {                
                return RedirectToAction("Registrarse", "Login");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
       
        /// <summary>La función Cerrar sesión cierra la sesión del usuario, abandona la sesión y redirige a la página  de inicio.
        /// <returns>El método devuelve un resultado `RedirectToAction` que redirige al usuario a la acción "Inicio" del controlador "Índice".
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult VerificarCorreo(string correo)
        {
            // Realizar la verificación del correo electrónico en la base de datos
            bool correoExiste = VerificarExistenciaCorreo(correo);
            // Retornar un objeto JSON indicando si el correo existe
            return Json(new { existe = correoExiste });
        }

        private bool VerificarExistenciaCorreo(string correo)
        {
           bool correoExiste = db.Usuario.Any(u => u.Correo == correo);
           return correoExiste;
        }

        public static string ConvertirSha256(string texto)
        {            
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

    }
}


