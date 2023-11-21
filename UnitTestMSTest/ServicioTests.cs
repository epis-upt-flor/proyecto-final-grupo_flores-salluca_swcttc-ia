using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaArtemis.Models;
using System;

namespace UnitTestMSTest
{
    [TestClass]
    public class ServicioTests
    {
        [TestMethod]
        public void Listar_ServiciosEnEstadoEspecifico_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var servicio = new Servicio();

            // Act
            var servicios = servicio.Listar();

            // Assert
            Assert.IsNotNull(servicios);
            Assert.IsTrue(servicios.Count > 0);
        }

        [TestMethod]
        public void ListarProblema_IdClienteExistente_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var servicio = new Servicio();
            var idClienteExistente = 1; // Modificar 

            // Act
            var problemas = servicio.ListarProblema(idClienteExistente);

            // Assert
            Assert.IsNotNull(problemas);
            Assert.IsTrue(problemas.Count > 0);
        }

        [TestMethod]
        public void MisServicios_IdTecnicoExistente_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var servicio = new Servicio();
            var idTecnicoExistente = 1; // Modificar 
            // Act
            var misServicios = servicio.MisServicios(idTecnicoExistente);

            // Assert
            Assert.IsNotNull(misServicios);
            Assert.IsTrue(misServicios.Count > 0);
        }

        [TestMethod]
        public void Guardar_NuevoServicio_DeberiaGuardarCorrectamente()
        {
            // Arrange
            var servicio = new Servicio
            {
                Descripcion = "Descripción del servicio",
                Fecha_Inicio = DateTime.Now,
                Fecha_Fin = DateTime.Now.AddDays(2),
                Id_Tecnico = 1, // Modificar 
                Id_Problema = 1, // Modificar 
                Id_Estado_Servicio = 1 // Modificar
            };

            // Act
            servicio.Guardar();

            // Assert
        }

        [TestMethod]
        public void Obtener_ServicioExistente_DeberiaDevolverServicioNoNulo()
        {
            // Arrange
            var servicio = new Servicio();
            var idServicioExistente = 1; // Modificar 

            // Act
            var servicioObtenido = servicio.Obtener(idServicioExistente);

            // Assert
            Assert.IsNotNull(servicioObtenido);
        }

        [TestMethod]
        public void Detalles_ServicioExistente_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var servicio = new Servicio();
            var idServicioExistente = 1; // Modificar 

            // Act
            var detallesServicio = servicio.Detalles(idServicioExistente);

            // Assert
            Assert.IsNotNull(detallesServicio);
            Assert.IsTrue(detallesServicio.Count > 0);
        }

        [TestMethod]
        public void ListService_ServiciosEnEstadoEspecifico_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var servicio = new Servicio();

            // Act
            var servicios = servicio.ListService();

            // Assert
            Assert.IsNotNull(servicios);
            Assert.IsTrue(servicios.Count > 0);
        }

        [TestMethod]
        public void ObtenerTotalServiciosPorTecnico_IdTecnicoExistente_DeberiaDevolverNumeroCorrecto()
        {
            // Arrange
            var servicio = new Servicio();
            var idTecnicoExistente = 1; // Modificar 

            // Act
            var totalServicios = servicio.ObtenerTotalServiciosPorTecnico(idTecnicoExistente);

            // Assert
        }

        [TestMethod]
        public void ObtenerTotalServiciosFaltaAprobarEstado_IdTecnicoExistente_DeberiaDevolverNumeroCorrecto()
        {
            // Arrange
            var servicio = new Servicio();
            var idTecnicoExistente = 1; // Modificar

            // Act
            var totalServicios = servicio.ObtenerTotalServiciosFaltaAprobarEstado(idTecnicoExistente);

            // Assert
        }

        [TestMethod]
        public void ObtenerTotalServiciosEnProceso_IdTecnicoExistente_DeberiaDevolverNumeroCorrecto()
        {
            // Arrange
            var servicio = new Servicio();
            var idTecnicoExistente = 1; // Modificar 

            // Act
            var totalServicios = servicio.ObtenerTotalServiciosEnProceso(idTecnicoExistente);

            // Assert
        }

        [TestMethod]
        public void DetallesServicio_ServicioExistente_DeberiaDevolverServicioNoNulo()
        {
            // Arrange
            var servicio = new Servicio();
            var idServicioExistente = 1; // Modificar 

            // Act
            var servicioDetalles = servicio.DetallesServicio(idServicioExistente);

            // Assert
            Assert.IsNotNull(servicioDetalles);
        }

        [TestMethod]
        public void BuscarIdTecnico_IdServicioExistente_DeberiaDevolverIdTecnicoCorrecto()
        {
            // Arrange
            var servicio = new Servicio();
            var idServicioExistente = 1; // 

            // Act
            var idTecnico = servicio.BuscarIdTecnico(idServicioExistente);

            // Assert
        }

        [TestMethod]
        public void ListaServicioTecnico_IdTecnicoExistente_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var servicio = new Servicio();
            var idTecnicoExistente = 1; // Modificar

            // Act
            var listaServicios = servicio.ListaServicioTecnico(idTecnicoExistente);

            // Assert
            Assert.IsNotNull(listaServicios);
            Assert.IsTrue(listaServicios.Count > 0);
        }
    }
}
