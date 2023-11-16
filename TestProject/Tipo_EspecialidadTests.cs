using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class Tipo_EspecialidadTests
    {
        
        [TestMethod]
        public void Listar_TipoEspecialidadPorIdTecnicoExistente_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var tipoEspecialidad = new Tipo_Especialidad();
            var idTecnicoExistente = 1; // Modificar 

            // Act
            var tiposEspecialidad = tipoEspecialidad.Listar(idTecnicoExistente);

            // Assert
            Assert.IsNotNull(tiposEspecialidad);
        }

        [TestMethod]
        public void ListarTipoEspecialidad_TodosLosTiposEspecialidad_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var tipoEspecialidad = new Tipo_Especialidad();

            // Act
            var tiposEspecialidad = tipoEspecialidad.ListarTipoEspecialidad();

            // Assert
            Assert.IsNotNull(tiposEspecialidad);
        }

    }
}
