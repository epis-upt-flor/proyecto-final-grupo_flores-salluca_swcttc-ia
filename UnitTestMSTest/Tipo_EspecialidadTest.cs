using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaArtemis.Models;
using System;
using System.Data.Entity;
using System.Linq;

namespace UnitTestMSTest
{
    [TestClass]
    public class Tipo_EspecialidadTest
    {
        [TestMethod]
        public void Listar_TipoEspecialidadPorIdTecnicoExistente_DeberiaDevolverListaNoVacia()
        {
            //// Configura la cadena de conexión en tiempo de ejecución
            //var connectionString = "."; // Reemplaza con tu cadena de conexión
            //Database.SetInitializer(new NullDatabaseInitializer<SistemaArtemisContext>());

            //// Resto del código de la prueba
            //using (var context = new SistemaArtemisContext(connectionString))
            //{
            //    var tipoEspecialidad = new Tipo_Especialidad();
            //    var idTecnicoExistente = 1;

            //    var tiposEspecialidad = tipoEspecialidad.Listar(idTecnicoExistente);

            //    Assert.IsNotNull(tiposEspecialidad);
            //}
        }
    }
}
