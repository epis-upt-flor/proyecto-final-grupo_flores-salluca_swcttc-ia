using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaArtemis.Models;
using System;

namespace UnitTestMSTest
{
    [TestClass]
    public class EspecialidadTests
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
