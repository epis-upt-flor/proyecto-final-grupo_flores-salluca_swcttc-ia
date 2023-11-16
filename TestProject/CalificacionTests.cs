using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaArtemis.Models;
using System.Collections.Generic;

namespace TestProject
{
    [TestClass]
    public class CalificacionTests
    {
        [TestMethod]
        public void Guardar_CalificacionNueva_DeberiaAgregarCalificacion()
        {
            // Arrange
            var nuevaCalificacion = new Calificacion
            {
                Comentario = "Buen servicio",
                Calificacion1 = 5,
                Id_Cliente = 1,
                Id_Servicio = 1
            };

            // Act
            nuevaCalificacion.Guardar();

            // Assert
            var calificacionObtenida = new Calificacion().Obtener(nuevaCalificacion.Id_Servicio.Value);
            Assert.IsNotNull(calificacionObtenida);
            Assert.AreEqual(nuevaCalificacion.Comentario, calificacionObtenida.Comentario);
            
        }

        [TestMethod]
        public void Obtener_IdValido_DeberiaRetornarCalificacion()
        {
            // Arrange
            var idServicioExistente = 1;

            // Act
            var calificacionObtenida = new Calificacion().Obtener(idServicioExistente);

            // Assert
            Assert.IsNotNull(calificacionObtenida);
            Assert.AreEqual(idServicioExistente, calificacionObtenida.Id_Servicio);
            
        }

        [TestMethod]
        public void Listar_CalificacionesExistentes_DeberiaRetornarLista()
        {
            // Act
            var listaCalificaciones = new Calificacion().Listar();

            // Assert
            Assert.IsNotNull(listaCalificaciones);
            Assert.IsInstanceOfType(listaCalificaciones, typeof(List<Calificacion>));
            
        }

        [TestMethod]
        public void VerCalificacion_IdClienteValido_DeberiaRetornarLista()
        {
            // Arrange
            var idClienteExistente = 1;

            // Act
            var calificaciones = new Calificacion().VerCalificacion(idClienteExistente);

            // Assert
            Assert.IsNotNull(calificaciones);
            Assert.IsInstanceOfType(calificaciones, typeof(List<Calificacion>));
            
        }

        [TestMethod]
        public void VerCalificacionLista_ListaIdsValidos_DeberiaRetornarLista()
        {
            // Arrange
            var listaIdsValidos = new List<int> { 1, 2, 3 };

            // Act
            var calificaciones = new Calificacion().VerCalificacionLista(listaIdsValidos);

            // Assert
            Assert.IsNotNull(calificaciones);
            Assert.IsInstanceOfType(calificaciones, typeof(List<Calificacion>));
            
        }
    }
}
