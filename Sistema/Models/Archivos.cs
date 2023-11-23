/*
Versión: 1.0
Descripción: Clase Archivos que representa la entidad para el manejo de archivos en el sistema Artemis.
Para el caso de uso:
    Gestionar Asignación Trabajo
    Gestionar Progreso

Fecha de creación: [07/08/2023]
Creado por: [DJFN]

Última modificación: [11/11/2023]
Modificado por: [JFSV]
*/


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