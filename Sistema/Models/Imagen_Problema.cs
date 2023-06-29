namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.IO;
    using System.Linq;
    using System.Web;

    public partial class Imagen_Problema
    {
        [Key]
        public int Id_Imagen { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        public byte[] Archivo { get; set; }

        [StringLength(50)]
        public string Extension { get; set; }

        public int? Id_Problema { get; set; }

        public virtual Problema Problema { get; set; }

        public static void GuardarImagen(int id_Problema, HttpPostedFileBase[] fileInput)
        {
            if (fileInput != null && fileInput.Length > 0)
            {
                using (var dbContext = new Model1()) 
                {
                    try
                    {
                        foreach (var file in fileInput)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                var imagen = new Imagen_Problema();
                                imagen.Nombre = Path.GetFileNameWithoutExtension(file.FileName);
                                imagen.Extension = Path.GetExtension(file.FileName);
                                imagen.Id_Problema = id_Problema;

                                using (var reader = new BinaryReader(file.InputStream))
                                {
                                    imagen.Archivo = reader.ReadBytes(file.ContentLength);
                                }

                                dbContext.Imagen_Problema.Add(imagen);
                            }
                        }

                        dbContext.SaveChanges();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        public List<Imagen_Problema> VisualizarImagenes(int id_Problema)
        {
            var imagenes = new List<Imagen_Problema>();
            try
            {
                using (var db = new Model1())
                {
                    imagenes = db.Imagen_Problema
                        .Include("Problema")
                        .Where(x => x.Id_Problema == id_Problema)
                        .ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return imagenes;
        }




        //public static void GuardarImagen(int id_Problema, HttpPostedFileBase[] fileInput)
        //{
        //    if (fileInput != null && fileInput.Length > 0)
        //    {
        //        using (var dbContext = new Model1()) 
        //        {
        //            foreach (var file in fileInput)
        //            {
        //                if (file != null && file.ContentLength > 0)
        //                {
        //                    var imagen = new Imagen_Problema();
        //                    imagen.Nombre = Path.GetFileNameWithoutExtension(file.FileName);
        //                    imagen.Extension = Path.GetExtension(file.FileName);
        //                    imagen.Id_Problema = id_Problema;

        //                    using (var reader = new BinaryReader(file.InputStream))
        //                    {
        //                        imagen.Archivo = reader.ReadBytes(file.ContentLength);
        //                    }

        //                    dbContext.Imagen_Problema.Add(imagen);
        //                }
        //            }

        //            dbContext.SaveChanges();
        //        }
        //    }
        //}



    }
}
