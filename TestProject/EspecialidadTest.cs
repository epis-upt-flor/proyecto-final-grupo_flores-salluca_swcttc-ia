using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class EspecialidadTest
    {
        [TestMethod]
        public void Listar_ListarEspecialidades_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var especialidad = new Especialidad();

            // Act
            var listaEspecialidades = especialidad.Listar();

            // Assert
            Assert.IsNotNull(listaEspecialidades);
            Assert.IsTrue(listaEspecialidades.Count > 0);
        }
    }
}
