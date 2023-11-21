using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaArtemis.Models;
using System;
using System.Collections.Generic;

namespace UnitTestMSTest
{
    [TestClass]
    public class TecnicoTests
    {
        [TestMethod]
        public void Obtener_TecnicoExistente_DeberiaDevolverTecnicoNoNulo()
        {
            // Arrange
            var tecnico = new Tecnico();
            var idUsuarioExistente = 1; // Modificar

            // Act
            var tecnicoObtenido = tecnico.Obtener(idUsuarioExistente);

            // Assert
            Assert.IsNotNull(tecnicoObtenido);
        }

        [TestMethod]
        public void Guardar_TecnicoNuevo_ObtieneIdTecnicoMayorCero()
        {
            // Arrange
            var tecnicoNuevo = new Tecnico
            {
                Nombre = "Nuevo",// Modificar 
                Apellido = "Tecnico",// Modificar 
                Telefono = "123456789",// Modificar 
                Id_Especialidad = 1, // Modificar 
                Id_Estado_Tecnico = 1, // Modificar 
                Id_Usuario = 1 // Modificar 
            };

            // Act
            tecnicoNuevo.Guardar();

            // Assert
            Assert.IsTrue(tecnicoNuevo.Id_Tecnico > 0);
        }

        [TestMethod]
        public void Listar_TecnicosEnBaseDeDatos_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var tecnico = new Tecnico();

            // Act
            var tecnicos = tecnico.Listar();

            // Assert
            Assert.IsNotNull(tecnicos);
            Assert.IsTrue(tecnicos.Count > 0);
        }

        [TestMethod]
        public void Buscar_TecnicoExistente_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var tecnico = new Tecnico();
            var criterioBusqueda = "Nombre"; // Modificar 
            // Act
            var tecnicosEncontrados = tecnico.Buscar(criterioBusqueda);

            // Assert
            Assert.IsNotNull(tecnicosEncontrados);
            Assert.IsTrue(tecnicosEncontrados.Count > 0);
        }

        [TestMethod]
        public void ListarRecomendado_TecnicosConIdsEspecificos_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var tecnico = new Tecnico();
            var idsTecnicosRecomendados = new List<int> { 1, 2, 3 }; // Modificar 

            // Act
            var tecnicosRecomendados = tecnico.ListarRecomendado(idsTecnicosRecomendados);

            // Assert
            Assert.IsNotNull(tecnicosRecomendados);
            Assert.IsTrue(tecnicosRecomendados.Count > 0);
        }

        [TestMethod]
        public void ObtenerTotalTecnicosDisponibleNoDisponibles_DeberiaDevolverNumeroCorrecto()
        {
            // Arrange
            var tecnico = new Tecnico();

            // Act
            var totalTecnicos = tecnico.ObtenerTotalTecnicosDisponibleNoDisponibles();

            // Assert
        }

        [TestMethod]
        public void ObtenerTotalTecnicoDisponibles_DeberiaDevolverNumeroCorrecto()
        {
            // Arrange
            var tecnico = new Tecnico();

            // Act
            var totalTecnicosDisponibles = tecnico.ObtenerTotalTecnicoDisponibles();

            // Assert
        }

        [TestMethod]
        public void ObtenerTotalTipoEspecialidadPorTecnico_IdTecnicoExistente_DeberiaDevolverNumeroCorrecto()
        {
            // Arrange
            var tecnico = new Tecnico();
            var idTecnicoExistente = 1; // Modificar

            // Act
            var totalTipoEspecialidad = tecnico.ObtenerTotalTipoEspecialidadPorTecnico(idTecnicoExistente);

            // Assert
        }
    }
}
