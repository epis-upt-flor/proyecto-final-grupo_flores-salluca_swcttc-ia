using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TestProject
{
    [TestClass]
    public class ClienteTests
    {
        [TestMethod]
        public void Listar1_IdUsuarioExistente_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var cliente = new Cliente();
            var idUsuarioExistente = 1;

            // Act
            var listaClientes = cliente.Listar1(idUsuarioExistente);

            // Assert
            Assert.IsNotNull(listaClientes);
            Assert.IsTrue(listaClientes.Count > 0);
        }

        [TestMethod]
        public void Obtener_IdClienteExistente_DeberiaDevolverClienteConUsuario()
        {
            // Arrange
            var cliente = new Cliente();
            var idClienteExistente = 1;

            // Act
            var clienteObtenido = cliente.Obtener(idClienteExistente);

            // Assert
            Assert.IsNotNull(clienteObtenido);
            Assert.AreEqual(idClienteExistente, clienteObtenido.Id_Cliente);
            Assert.IsNotNull(clienteObtenido.Usuario);
        }

        [TestMethod]
        public void Guardar_NuevoCliente_DeberiaDevolverSinErrores()
        {
            // Arrange
            var nuevoCliente = new Cliente
            {
                Nombre = "Nuevo",
                Apellido = "Cliente",
                Telefono = "123456789",
                Estado = 1,
                Id_Usuario = 1
            };

            // Act
            nuevoCliente.Guardar();

            // Assert
            // Verifica que no hay excepciones al guardar el nuevo cliente
        }

        [TestMethod]
        public void Listar_ListarClientes_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var cliente = new Cliente();

            // Act
            var listaClientes = cliente.Listar();

            // Assert
            Assert.IsNotNull(listaClientes);
            Assert.IsTrue(listaClientes.Count > 0);
        }

        [TestMethod]
        public void ObtenerTotalServiciosCliente_IdClienteExistente_DeberiaDevolverNumeroPositivo()
        {
            // Arrange
            var cliente = new Cliente();
            var idClienteExistente = 1;

            // Act
            var totalServicios = cliente.ObtenerTotalServiciosCliente(idClienteExistente);

            // Assert
            Assert.IsTrue(totalServicios >= 0);
        }

        [TestMethod]
        public void ObtenerTotalServiciosClienteEstadoEnProceso_IdClienteExistente_DeberiaDevolverNumeroPositivo()
        {
            // Arrange
            var cliente = new Cliente();
            var idClienteExistente = 1;

            // Act
            var totalServiciosEnProceso = cliente.ObtenerTotalServiciosClienteEstadoEnProceso(idClienteExistente);

            // Assert
            Assert.IsTrue(totalServiciosEnProceso >= 0);
        }
    }
}
