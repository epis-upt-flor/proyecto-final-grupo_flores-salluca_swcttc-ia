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

        private bool IsValid(Usuario usuario)
        {
            return usuario.Autenticar();
        }

        // GET: Login
        public ActionResult Registrarse()
        {
            return View(new Usuario() { Nombre = "", Apellido = "", Correo = "", Password = "", Estado = ' ', Id_Tipo_Usuario = ' ' });
        }

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
       
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Home", "Index");
        }
    }
}


