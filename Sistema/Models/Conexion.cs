using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaArtemis.Models
{

    /* La clase "Conexión" contiene una cadena estática que representa una conexión a una base de datos. 
    Esta conexion nos servira para los procedimientos almancenados*/
    public class Conexion
    {
        public static string CN = "Data Source=.;Initial Catalog=ArtemisBD;Integrated Security=True";
    }
}