using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaArtemis.Models;
using System;

namespace UnitTestMSTest
{
    [TestClass]
    public class ProblemaTests
    {
        [TestMethod]
        public void ListarProblema_ClienteExistente_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var problema = new Problema();
            var idClienteExistente = 1; // Modifica

            // Act
            var misProblemas = problema.ListarProblema(idClienteExistente);

            // Assert
            Assert.IsNotNull(misProblemas);
            Assert.IsTrue(misProblemas.Count > 0);
        }

        [TestMethod]
        public void ListaProblemaCliente_ClienteExistente_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var problema = new Problema();
            var idClienteExistente = 1; // Modifica

            // Act
            var problemasCliente = problema.ListaProblemaCliente(idClienteExistente);

            // Assert
            Assert.IsNotNull(problemasCliente);
            Assert.IsTrue(problemasCliente.Count > 0);
        }

        [TestMethod]
        public void ListaProblemaCliente_ClienteNoExistente_DeberiaDevolverListaVacia()
        {
            // Arrange
            var problema = new Problema();
            var idClienteNoExistente = -1; // Modifica 

            // Act
            var problemasCliente = problema.ListaProblemaCliente(idClienteNoExistente);

            // Assert
            Assert.IsNotNull(problemasCliente);
            Assert.IsTrue(problemasCliente.Count == 0);
        }
    }
}
