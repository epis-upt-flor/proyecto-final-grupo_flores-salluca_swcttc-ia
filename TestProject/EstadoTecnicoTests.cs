using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class EstadoTecnicoTests
    {
        [TestMethod]
        public void Listar_ListarEstadosTecnico_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var estadoTecnico = new Estado_Tecnico();

            // Act
            var listaEstadosTecnico = estadoTecnico.Listar();

            // Assert
            Assert.IsNotNull(listaEstadosTecnico);
            Assert.IsTrue(listaEstadosTecnico.Count > 0);
        }

        [TestMethod]
        public void Buscar_BuscarEstadosTecnicoPorCriterio_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var estadoTecnico = new Estado_Tecnico();
            var criterioBusqueda = "Disponible";

            // Act
            var estadosEncontrados = estadoTecnico.Buscar(criterioBusqueda);

            // Assert
            Assert.IsNotNull(estadosEncontrados);
            Assert.IsTrue(estadosEncontrados.Count > 0);
        }

        [TestMethod]
        public void Obtener_ObtenerEstadoTecnicoPorIdExistente_DeberiaDevolverEstadoTecnico()
        {
            // Arrange
            var estadoTecnico = new Estado_Tecnico();
            var idExistente = 1; 

            // Act
            var estadoObtenido = estadoTecnico.Obtener(idExistente);

            // Assert
            Assert.IsNotNull(estadoObtenido);
            Assert.AreEqual(idExistente, estadoObtenido.Id_Estado_Tecnico);
        }

        [TestMethod]
        public void Obtener_ObtenerEstadoTecnicoPorIdNoExistente_DeberiaDevolverNull()
        {
            // Arrange
            var estadoTecnico = new Estado_Tecnico();
            var idNoExistente = -1; 

            // Act
            var estadoObtenido = estadoTecnico.Obtener(idNoExistente);

            // Assert
            Assert.IsNull(estadoObtenido);
        }
    }
}
