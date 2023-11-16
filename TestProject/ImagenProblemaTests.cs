using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class ImagenProblemaTests
    {
        [TestMethod]
        public void VisualizarImagenes_ImagenesExistentes_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var imagenProblema = new Imagen_Problema();
            var idProblemaExistente = 1;

            // Act
            var imagenes = imagenProblema.VisualizarImagenes(idProblemaExistente);

            // Assert
            Assert.IsNotNull(imagenes);
            Assert.IsTrue(imagenes.Count > 0);
        }

        [TestMethod]
        public void VisualizarImagenes_ImagenesNoExistentes_DeberiaDevolverListaVacia()
        {
            // Arrange
            var imagenProblema = new Imagen_Problema();
            var idProblemaNoExistente = -1; 

            // Act
            var imagenes = imagenProblema.VisualizarImagenes(idProblemaNoExistente);

            // Assert
            Assert.IsNotNull(imagenes);
            Assert.IsTrue(imagenes.Count == 0);
        }

       
    }
}
