using SistemaArtemis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [TestClass]
    public class RTecnico_TipoEspecialidadTests
    {
        [TestMethod]
        public void Guardar_NuevaRelacion_DeberiaGuardarCorrectamente()
        {
            // Arrange
            var rTecnicoTipoEspecialidad = new RTecnico_TipoEspecialidad
            {
                Id_Tecnico = 1, // Modificar 
                Id_Tipo_Especialidad = 1 // Modificar
            };

            // Act
            rTecnicoTipoEspecialidad.Guardar();

            // Assert
            
        }

        [TestMethod]
        public void ListarR_TecnicoExistente_DeberiaDevolverListaNoVacia()
        {
            // Arrange
            var rTecnicoTipoEspecialidad = new RTecnico_TipoEspecialidad();
            var idTecnicoExistente = 1; // Modificar

            // Act
            var relaciones = rTecnicoTipoEspecialidad.ListarR(idTecnicoExistente);

            // Assert
            Assert.IsNotNull(relaciones);
            Assert.IsTrue(relaciones.Count > 0);
        }

        [TestMethod]
        public void Eliminar_RelacionExistente_DeberiaEliminarCorrectamente()
        {
            // Arrange
            var rTecnicoTipoEspecialidad = new RTecnico_TipoEspecialidad
            {
                Id = 1 // Modificar
            };

            // Act
            rTecnicoTipoEspecialidad.Eliminar();

            // Assert
            
        }

        [TestMethod]
        public void Obtener_IdExistente_DeberiaDevolverRelacionCorrecta()
        {
            // Arrange
            var rTecnicoTipoEspecialidad = new RTecnico_TipoEspecialidad();
            var idExistente = 1;

            // Act
            var relacion = rTecnicoTipoEspecialidad.Obtener(idExistente);

            // Assert
            
        }

        
    }
}
