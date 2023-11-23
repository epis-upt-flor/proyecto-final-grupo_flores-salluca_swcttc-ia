/*
Versión: 1.0
Descripción: ArchivoController representada gestionar la asignaciòn de trabajo.
Para el caso de uso: Gestionar Asignaciòn de Trabajo

Fecha de creación: [07/08/2023]
Creado por: [DJFN]

Última modificación: [11/11/2023]
Modificado por: [JFSV]
*/


using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaArtemis.Controllers
{
    public class ArchivoController : Controller
    {
        static string cadena = "Data Source=.;Initial Catalog=ArtemisBD;Integrated security=true";
        //NO CREAR ESTO HASTA LA VISTA DE INDEX
        //using ProyectoCarga.Models;
        static List<Archivos> olista = new List<Archivos>();

        //1.-PRIMERO CREAR LA VISTA DE SUBIR
        public ActionResult Subir(int id = 0)
        {
            var problemas = new Problema().Listar(id);
            ViewBag.Prob = problemas;
            return View();
        }
        //2.- CREAR EL MODELO - ARCHIVO
        //3.- CREAR PRIMERO LA LOGICA Y POR ULTIMO LA VISTA
        public ActionResult Index(int id)
        {
            olista = new List<Archivos>();

            using (SqlConnection oconexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM ARCHIVOS WHERE Id_Problema = @id_problema", oconexion);
                cmd.Parameters.AddWithValue("@id_problema", id);
                cmd.CommandType = CommandType.Text;
                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Archivos archivo_encontrado = new Archivos();
                        archivo_encontrado.IdArchivo = Convert.ToInt32(dr["IdArchivo"]);
                        archivo_encontrado.Nombre = dr["Nombre"].ToString();
                        archivo_encontrado.Archivo = dr["Archivo"] as byte[];
                        archivo_encontrado.Extension = dr["Extension"].ToString();

                        olista.Add(archivo_encontrado);
                    }
                }

            }
            ViewBag.IdProblema = id;
            return View(olista);
        }


        //public EmptyResult SubirArchivo(string Nombre, HttpPostedFileBase Archivo, int Id_Problema)
        [HttpPost]
        public ActionResult SubirArchivo(string Nombre, HttpPostedFileBase Archivo, int Id_Problema)
        {
            //var sv = new ServicioController();
            //sv.Guardar(sdfsdf.servicio)

            string Extension = Path.GetExtension(Archivo.FileName);
            MemoryStream ms = new MemoryStream();
            Archivo.InputStream.CopyTo(ms);
            byte[] data = ms.ToArray();

            using (SqlConnection oconexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO ARCHIVOS(Nombre, Archivo, Extension, Id_Problema) VALUES(@nombre, @archivo, @extension, @idProblema)", oconexion);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.Parameters.AddWithValue("@archivo", data);
                cmd.Parameters.AddWithValue("@extension", Extension);
                cmd.Parameters.AddWithValue("@idProblema", Id_Problema);
                cmd.CommandType = CommandType.Text;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            string mensaje = Nombre;
            ViewBag.Mensaje = mensaje;
            //return new EmptyResult();
            return RedirectToAction("Create/" + @Id_Problema, "Servicio");
        }


        [HttpPost]
        public FileResult DescargarArchivo(int IdArchivo)
        {
            CargarArchivosDesdeBD();
            Archivos oArchivo = olista.Where(a => a.Id_Problema == IdArchivo).FirstOrDefault();
            //Archivos oArchivo = olista.Where(a => a.IdArchivo == IdArchivo || a.Id_Problema==IdArchivo).FirstOrDefault();
            string NombreCompleto = oArchivo.Nombre + oArchivo.Extension;
            return File(oArchivo.Archivo, "application/" + oArchivo.Extension.Replace(".", ""), NombreCompleto);
        }
        private void CargarArchivosDesdeBD()
        {

            using (SqlConnection oconexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM ARCHIVOS", oconexion);

                cmd.CommandType = CommandType.Text;
                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Archivos archivo_encontrado = new Archivos();
                        archivo_encontrado.IdArchivo = Convert.ToInt32(dr["IdArchivo"]);
                        archivo_encontrado.Nombre = dr["Nombre"].ToString();
                        archivo_encontrado.Archivo = dr["Archivo"] as byte[];
                        archivo_encontrado.Extension = dr["Extension"].ToString();
                        archivo_encontrado.Id_Problema = Convert.ToInt32(dr["Id_Problema"]);

                        olista.Add(archivo_encontrado);
                    }
                }
            }
        }

    }
}