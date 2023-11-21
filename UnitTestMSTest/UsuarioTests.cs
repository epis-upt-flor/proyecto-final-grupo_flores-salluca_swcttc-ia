using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaArtemis.Models;
using System;

namespace UnitTestMSTest
{
    [TestClass]
    public class UsuarioTests
    {
        [TestMethod]
        public void Registrar_NuevoUsuario_DeberiaDevolverResultadoExitoso()
        {
            // Arrange
            var nuevoUsuario = new Usuario
            {
                Nombre = "Nuevo",
                Apellido = "Usuario",
                Correo = "nuevo.usuario@example.com",
                Password = "password123",
                Estado = 1,
                Id_Tipo_Usuario = 1
            };

            // Act
            var resultado = nuevoUsuario.Registrar(nuevoUsuario);

            // Assert
            Assert.AreEqual(1, resultado);
        }

        [TestMethod]
        public void Listar_ListarUsuarios_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var usuario = new Usuario();

            // Act
            var listaUsuarios = usuario.Listar();

            // Assert
            Assert.IsNotNull(listaUsuarios);
            Assert.IsTrue(listaUsuarios.Count > 0);
        }

        [TestMethod]
        public void Obtener_UsuarioExistentePorId_DeberiaDevolverUsuario()
        {
            // Arrange
            var usuario = new Usuario();
            var idUsuarioExistente = 1;

            // Act
            var usuarioObtenido = usuario.Obtener(idUsuarioExistente);

            // Assert
            Assert.IsNotNull(usuarioObtenido);
            Assert.AreEqual(idUsuarioExistente, usuarioObtenido.Id_Usuario);
        }

        //[TestMethod]
        //public void Guardar_NuevoUsuario_DeberiaNoLanzarExcepcion()
        //{
        //    // Arrange
        //    var nuevoUsuario = new Usuario
        //    {
        //        Nombre = "Nuevo",
        //        Apellido = "Usuario",
        //        Correo = "nuevousuario@example.com",
        //        Password = "password",
        //        Estado = 1,
        //        Id_Tipo_Usuario = 1
        //    };

        //    // Act and Assert
        //    Assert.ThrowsException<Exception>(() => nuevoUsuario.Guardar());
        //}

        [TestMethod]
        public void Buscar_UsuariosPorCriterio_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var usuario = new Usuario();
            var criterioBusqueda = "Usuario";

            // Act
            var usuariosEncontrados = usuario.Buscar(criterioBusqueda);

            // Assert
            Assert.IsNotNull(usuariosEncontrados);
        }

        //[TestMethod]
        //public void Eliminar_UsuarioExistente_DeberiaNoLanzarExcepcion()
        //{
        //    // Arrange
        //    var usuarioExistente = new Usuario { Id_Usuario = 1 }; // Modificar 

        //    // Act and Assert
        //    Assert.ThrowsException<Exception>(() => usuarioExistente.Eliminar());
        //}

        [TestMethod]
        public void Autenticar_UsuarioExistenteConCredencialesCorrectas_DeberiaDevolverTrue()
        {
            // Arrange
            var usuarioExistente = new Usuario
            {
                Correo = "Cliente35@gmail.com",
                Password = "1234"
            }; // Modificar 

            // Act
            var autenticado = usuarioExistente.Autenticar();

            // Assert
            Assert.IsTrue(autenticado);
        }

        [TestMethod]
        public void ObtenerDatos_CorreoExistente_DeberiaDevolverUsuarioCorrecto()
        {
            // Arrange
            var usuario = new Usuario();
            var correoExistente = "Cliente35@gmail.com"; // Modificar 

            // Act
            var usuarioObtenido = usuario.ObtenerDatos(correoExistente);

            // Assert
            Assert.IsNotNull(usuarioObtenido);
        }

        //[TestMethod]
        //public void Acceder_CredencialesCorrectas_DeberiaDevolverRespuestaExitosa()
        //{
        //    // Arrange
        //    var usuarioExistente = new Usuario
        //    {
        //        Correo = "Cliente35@gmail.com",
        //        Password = "1234"
        //    }; // Modificar según tu caso

        //    // Act
        //    var respuesta = usuarioExistente.Acceder(usuarioExistente.Correo, usuarioExistente.Password);

        //    // Assert
        //    Assert.IsTrue(respuesta.Exito);
        //}

    }
}
