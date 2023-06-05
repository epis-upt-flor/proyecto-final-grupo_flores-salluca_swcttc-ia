using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaArtemis.Models.Login
{
    public class ResponseModel
    {
        public dynamic result { get; set; }
        public bool response { get; set; }
        public string message { get; set; }
        public string href { get; set; }
        public string function { get; set; }

        /* Este es un método constructor para la clase `ResponseModel`. Establece los valores
        predeterminados para la propiedad `respuesta` en `falso` y la propiedad `mensaje` en
        "Ocurrio un error inesperado" (que significa "Se produjo un error inesperado" en español). */
        public ResponseModel()
        {
            this.response = false;
            this.message = "Ocurrio un error inesperado";
        }

        /// <summary> La función establece una respuesta booleana y un mensaje de error opcional. Si la respuesta es falsa y no se proporciona ningún mensaje, se establece un mensaje de error predeterminado.</summary>
        /// <param name="r">un valor booleano que indica el estado de la respuesta (verdadero para el éxito, falso para el fracaso)</param>
        /// <param name="m">m es un parámetro de cadena que representa un mensaje opcional que se puede pasar al método. Si se proporciona un mensaje, se almacenará en la variable "mensaje" del objeto. Si no se proporciona ningún mensaje, la variable "mensaje" se establecerá en un mensaje de error predeterminado.</param>
        /// 
        public void SetResponse(bool r, string m = "")
        {
            this.response = r;
            this.message = m;

            if (!r && m == "") this.message = "Ocurrio un error inesperado";
        }

    }
}