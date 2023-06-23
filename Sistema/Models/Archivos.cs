using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaArtemis.Models
{
    [Table("Archivos")]
    public class Archivos
    {
        [Key]
        public int IdArchivo { get; set; }
        public string Nombre { get; set; }
        public byte[] Archivo { get; set; }
        public string Extension { get; set; }
        public int Id_Problema { get; set; }

        public List<Archivos> Listar(int id)
        {
            var archivo = new List<Archivos>();
            try
            {
                using (var db = new Model1())
                {
                    archivo = db.ARCHIVOS
                        .Where(p => p.Id_Problema == id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return archivo;
        }
    }


}