﻿/*
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


namespace SistemaArtemis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;


    [Table("Archivos")]
    public partial class Archivos
    {
         [Key]
        public int IdArchivo { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        public byte[] Archivo { get; set; }

        [StringLength(50)]
        public string Extension { get; set; }

        public int? Id_Problema { get; set; }

        public int? Id_Tecnico { get; set; }

        public decimal? Precio { get; set; }

        public virtual Tecnico Tecnico { get; set; }



        public List<Archivos> Listar(int id)
        {
            var archivo = new List<Archivos>();
            try
            {
                using (var db = new Model1())
                {
                    archivo = db.Archivos
                        .Where(p => p.Id_Problema == id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return archivo;
        }
        public List<Archivos> ListarTodo()
        {
            var archivo = new List<Archivos>();
            try
            {
                using (var db = new Model1())
                {
                    archivo = db.Archivos.Include("Tecnico").ToList();
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
