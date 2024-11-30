using Logic.Clases;
using Logic.DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Runtime.Remoting.Contexts;

namespace Pruebas.AcademicoDAOTest
{
    [TestClass]
    public class RegistrarAcademicoTest
    {
        [TestMethod]
        public void AgregarAcademicoTest()
        {
            // Arrange
            AcademicoDAO academicoDAO = new AcademicoDAO();
            AcademicoDTO academicoDTO = new AcademicoDTO
            {
                Nombre = "Juan Pérez",
                AreaAcademica = "Economico - Administrativa",
                FechaContratacion = "22-10-2022",
                NumeroPersonal = "345676",
                IdPrograma = 1,
                Contrasena = "Contraseña123"
            };

            // Act
            int result = academicoDAO.AgregarAcademico(academicoDTO);

            // Assert
            Assert.AreEqual(1, result); // Se espera un 1 para indicar éxito
        }

        [TestMethod]
        public void AgregarAcademico_DuplicadoTest()
        {
            // Arrange
            AcademicoDAO academicoDAO = new AcademicoDAO();
            AcademicoDTO academicoDTO = new AcademicoDTO
            {
                Nombre = "Carlos López",
                AreaAcademica = "Matemáticas",
                FechaContratacion = "22-10-2022",
                NumeroPersonal = "345676", // Duplicado
                IdPrograma = 2,
                Contrasena = "Contraseña456"
            };

            // Act
            int result = academicoDAO.AgregarAcademico(academicoDTO);

            // Assert
            Assert.AreEqual(-3, result); // Se espera -3 indicando duplicidad
        }

        [TestMethod]
        public void AgregarAcademico_DatosInvalidosTest()
        {
            // Arrange
            AcademicoDAO academicoDAO = new AcademicoDAO();
            AcademicoDTO academicoDTO = new AcademicoDTO
            {
                Nombre = "", // Nombre vacío
                AreaAcademica = "Matemáticas",
                FechaContratacion = "22-10-2022",
                NumeroPersonal = "987654",
                IdPrograma = 3,
                Contrasena = "Contraseña123"
            };

            // Act
            int result = academicoDAO.AgregarAcademico(academicoDTO);

            // Assert
            Assert.AreEqual(-2, result); // Se espera -2 indicando error de validación
        }

        [TestMethod]
        public void AgregarAcademico_DbErrorTest()
        {
            // Arrange
            AcademicoDAO academicoDAO = new AcademicoDAO();
            AcademicoDTO academicoDTO = new AcademicoDTO
            {
                Nombre = "Ana Gómez",
                AreaAcademica = "Ciencias",
                FechaContratacion = "22-10-2022",
                NumeroPersonal = "67890",
                IdPrograma = 1,
                Contrasena = "Contraseña123"
            };

            // Simulando error en la base de datos
            Mock<IContexto> mockContexto = new Mock<IContexto>();
            mockContexto.Setup(c => c.SaveChanges()).Throws(new DbUpdateException("Error en la base de datos"));

            var academicoDAOConError = new AcademicoDAO(mockContexto.Object);

            // Act
            int result = academicoDAOConError.AgregarAcademico(academicoDTO);

            // Assert
            Assert.AreEqual(-1, result); // Se espera -1 indicando error de base de datos
        }

        [TestMethod]
        public void ObtenerIdAcademicoPorNumeroPersonalTest()
        {
            // Arrange
            AcademicoDAO academicoDAO = new AcademicoDAO();
            string numeroPersonal = "345676";

            // Act
            int? result = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal);

            // Assert
            Assert.IsTrue(result.HasValue); // Se espera un ID válido
        }

        [TestMethod]
        public void ObtenerIdAcademicoPorNumeroPersonal_NoExisteTest()
        {
            // Arrange
            AcademicoDAO academicoDAO = new AcademicoDAO();
            string numeroPersonal = "99999";

            // Act
            int? result = academicoDAO.ObtenerIdAcademicoPorNumeroPersonal(numeroPersonal);

            // Assert
            Assert.IsNull(result); // Se espera que no se encuentre
        }

        [TestMethod]
        public void ActualizarAcademicoTest()
        {
            // Arrange
            AcademicoDAO academicoDAO = new AcademicoDAO();
            AcademicoDTO academicoDTO = new AcademicoDTO
            {
                IdAcademico = 1,
                Nombre = "Juan Pérez",
                TipoContratacion = "Titular",
                AreaAcademica = "Matemáticas",
                FechaContratacion = "22-10-2022",
                IdPrograma = 1,
                Contrasena = "Contraseña123"
            };

            // Act
            int result = academicoDAO.ActualizarAcademico(academicoDTO);

            // Assert
            Assert.AreEqual(1, result); // Se espera un 1 para indicar éxito
        }

        [TestMethod]
        public void ActualizarAcademico_NoEncontradoTest()
        {
            // Arrange
            AcademicoDAO academicoDAO = new AcademicoDAO();
            AcademicoDTO academicoDTO = new AcademicoDTO
            {
                IdAcademico = 99999, // No existe
                Nombre = "Carlos López",
                TipoContratacion = "Adjunto",
                AreaAcademica = "Ciencias",
                FechaContratacion = "22-10-2022",
                IdPrograma = 2,
                Contrasena = "Contraseña456"
            };

            // Act
            int result = academicoDAO.ActualizarAcademico(academicoDTO);

            // Assert
            Assert.AreEqual(-1, result); // Se espera -1 indicando no encontrado
        }

        [TestMethod]
        public void ActualizarContraseñaYNombreAcademicoTest()
        {
            // Arrange
            AcademicoDAO academicoDAO = new AcademicoDAO();
            int idAcademico = 1;
            string nuevaContraseña = "NuevaContraseña123";
            string nuevoNombre = "Juan Pérez";

            // Act
            int result = academicoDAO.ActualizarContraseñaYNombreAcademico(idAcademico, nuevaContraseña, nuevoNombre);

            // Assert
            Assert.AreEqual(1, result); // Se espera un 1 para indicar éxito
        }

        [TestMethod]
        public void ActualizarContraseñaYNombreAcademico_NoEncontradoTest()
        {
            // Arrange
            AcademicoDAO academicoDAO = new AcademicoDAO();
            int idAcademico = 99999; // No existe
            string nuevaContraseña = "NuevaContraseña123";
            string nuevoNombre = "Carlos López";

            // Act
            int result = academicoDAO.ActualizarContraseñaYNombreAcademico(idAcademico, nuevaContraseña, nuevoNombre);

            // Assert
            Assert.AreEqual(-1, result); // Se espera -1 indicando no encontrado
        }

        [TestMethod]
        public void ValidarYRegistrarAcademico_NuevoTest()
        {
            // Arrange
            AcademicoDAO academicoDAO = new AcademicoDAO();
            AcademicoDTO academicoDTO = new AcademicoDTO
            {
                Nombre = "Ana Gómez",
                NumeroPersonal = "54321",
                Contrasena = "Contraseña123"
            };

            // Act
            int result = academicoDAO.ValidarYRegistrarAcademico(academicoDTO);

            // Assert
            Assert.AreEqual(1, result); // Se espera un 1 para indicar éxito
        }

        [TestMethod]
        public void ValidarYRegistrarAcademico_ActualizarTest()
        {
            // Arrange
            AcademicoDAO academicoDAO = new AcademicoDAO();
            AcademicoDTO academicoDTO = new AcademicoDTO
            {
                Nombre = "Carlos López",
                NumeroPersonal = "345676", // Ya existe
                Contrasena = "Contraseña456"
            };

            // Act
            int result = academicoDAO.ValidarYRegistrarAcademico(academicoDTO);

            // Assert
            Assert.AreEqual(1, result); // Se espera un 1 para indicar actualización exitosa
        }

        [TestMethod]
        public void AgregarAcademico_ValidarContraseñaYNombreTest()
        {
            // Arrange
            AcademicoDAO academicoDAO = new AcademicoDAO();
            AcademicoDTO academicoDTO = new AcademicoDTO
            {
                Nombre = "Juan Pérez",
                NumeroPersonal = "345676",
                Contrasena = "Contraseña123"
            };

            // Act
            int result = academicoDAO.AgregarAcademico(academicoDTO);

            // Assert
            Assert.AreEqual(1, result); // Se espera un 1 para indicar éxito
        }

        [TestMethod]
        public void AgregarParticipacionTest()
        {
            // Arrange
            ParticipacionDAO participacionDAO = new ParticipacionDAO();
            ParticipacionDTO participacionDTO = new ParticipacionDTO
            {
                Nombre = "Participación en curso de programación",
                TipoParticipacion = "Curso",
                PeriodoParticipacion = "2022",
                IdAcademico = 1,
                IdProgramaEducativo = 2
            };

            // Act
            int result = participacionDAO.AgregarParticipacion(participacionDTO);

            // Assert
            Assert.AreEqual(1, result); // Se espera un 1 para indicar éxito
        }

        [TestMethod]
        public void AgregarParticipacion_DuplicadoTest()
        {
            // Arrange
            ParticipacionDAO participacionDAO = new ParticipacionDAO();
            ParticipacionDTO participacionDTO = new ParticipacionDTO
            {
                Nombre = "Participación en curso de programación", // Duplicado
                TipoParticipacion = "Curso",
                PeriodoParticipacion = "2022",
                IdAcademico = 1,
                IdProgramaEducativo = 2
            };

            // Act
            int result = participacionDAO.AgregarParticipacion(participacionDTO);

            // Assert
            Assert.AreEqual(-3, result); // Se espera -3 indicando duplicidad
        }

        [TestMethod]
        public void ObtenerUltimoIdParticipacionTest()
        {
            // Arrange
            ParticipacionDAO participacionDAO = new ParticipacionDAO();

            // Act
            int? result = participacionDAO.ObtenerUltimoIdParticipacion();

            // Assert
            Assert.IsTrue(result.HasValue); // Se espera que el último ID sea válido
        }

        [TestMethod]
        public void ObtenerParticipacionesPorAcademicoYProgramaTest()
        {
            // Arrange
            ParticipacionDAO participacionDAO = new ParticipacionDAO();
            int idAcademico = 1;
            int idPrograma = 2;
            string tipoParticipacion = "Curso";
            string periodo = "2022";

            // Act
            var result = participacionDAO.ObtenerParticipacionesPorAcademicoYPrograma(idAcademico, idPrograma, tipoParticipacion, periodo);

            // Assert
            Assert.IsNotNull(result); // Se espera que la lista no sea nula
            Assert.IsTrue(result.Count > 0); // Se espera que al menos haya una participación
        }

        [TestMethod]
        public void AgregarProductoAcademicoTest()
        {
            // Arrange
            ProductoAcademicoDAO productoAcademicoDAO = new ProductoAcademicoDAO();
            ProductoAcademicoDTO productoAcademicoDTO = new ProductoAcademicoDTO
            {
                Titulo = "Tesis sobre Inteligencia Artificial",
                TipoProducto = "Investigación",
                FechaPublicacion = "01-01-2023",
                TipoPublicacion = "Revista",
                IdAcademico = 1
            };

            // Act
            int result = productoAcademicoDAO.AgregarProductoAcademico(productoAcademicoDTO);

            // Assert
            Assert.AreEqual(1, result); // Se espera un 1 para indicar éxito
        }

        [TestMethod]
        public void AgregarProductoAcademico_DatosInvalidosTest()
        {
            // Arrange
            ProductoAcademicoDAO productoAcademicoDAO = new ProductoAcademicoDAO();
            ProductoAcademicoDTO productoAcademicoDTO = new ProductoAcademicoDTO
            {
                Titulo = "", // Título vacío
                TipoProducto = "Investigación",
                FechaPublicacion = "01-01-2023",
                TipoPublicacion = "Revista",
                IdAcademico = 1
            };

            // Act
            int result = productoAcademicoDAO.AgregarProductoAcademico(productoAcademicoDTO);

            // Assert
            Assert.AreEqual(-3, result); // Se espera -3 para indicar error de validación
        }

        [TestMethod]
        public void ObtenerUltimoIdProductoAcademicoTest()
        {
            // Arrange
            ProductoAcademicoDAO productoAcademicoDAO = new ProductoAcademicoDAO();

            // Act
            int? result = productoAcademicoDAO.ObtenerUltimoIdProductoAcademico();

            // Assert
            Assert.IsTrue(result.HasValue); // Se espera que el último ID sea válido
        }

        [TestMethod]
        public void AgregarProyectoCampoTest()
        {
            // Arrange
            ProyectoDeCampoDAO proyectoDeCampoDAO = new ProyectoDeCampoDAO();
            ProyectoDeCampoDTO proyectoDeCampoDTO = new ProyectoDeCampoDTO
            {
                Nombre = "Proyecto Educativo en Línea",
                LugarRealizacion = "Plataforma Virtual",
                Periodo = "2023",
                RolAcademico = "Profesor",
                IdAcademico = 1
            };

            // Act
            int result = proyectoDeCampoDAO.AgregarProyectoCampo(proyectoDeCampoDTO);

            // Assert
            Assert.AreEqual(1, result); // Se espera un 1 para indicar éxito
        }

        [TestMethod]
        public void AgregarProyectoCampo_DuplicadoTest()
        {
            // Arrange
            ProyectoDeCampoDAO proyectoDeCampoDAO = new ProyectoDeCampoDAO();
            ProyectoDeCampoDTO proyectoDeCampoDTO = new ProyectoDeCampoDTO
            {
                Nombre = "Proyecto Educativo en Línea", // Duplicado
                LugarRealizacion = "Plataforma Virtual",
                Periodo = "2023",
                RolAcademico = "Profesor",
                IdAcademico = 1
            };

            // Act
            int result = proyectoDeCampoDAO.AgregarProyectoCampo(proyectoDeCampoDTO);

            // Assert
            Assert.AreEqual(-3, result); // Se espera -3 indicando duplicado
        }

        [TestMethod]
        public void ObtenerUltimoIdProyectoCampoTest()
        {
            // Arrange
            ProyectoDeCampoDAO proyectoDeCampoDAO = new ProyectoDeCampoDAO();

            // Act
            int? result = proyectoDeCampoDAO.ObtenerUltimoIdProyectoCampo();

            // Assert
            Assert.IsTrue(result.HasValue); // Se espera que el último ID sea válido
        }

        [TestMethod]
        public void AgregarTrabajoRecepcionalTest()
        {
            // Arrange
            TrabajoRecepcionalDAO trabajoRecepcionalDAO = new TrabajoRecepcionalDAO();
            TrabajoRecepcionalDTO trabajoRecepcionalDTO = new TrabajoRecepcionalDTO
            {
                NombreEstudiante = "José Martínez",
                Titulo = "Estudio sobre Big Data",
                FechaPublicacion = "01-01-2023",
                TipoTrabajo = "Investigación",
                RolAcademico = "Asesor",
                IdAcademico = 1
            };

            // Act
            int result = trabajoRecepcionalDAO.AgregarTrabajoRecepcional(trabajoRecepcionalDTO);

            // Assert
            Assert.AreEqual(1, result); // Se espera un 1 para indicar éxito
        }

        [TestMethod]
        public void AgregarTrabajoRecepcional_DuplicadoTest()
        {
            // Arrange
            TrabajoRecepcionalDAO trabajoRecepcionalDAO = new TrabajoRecepcionalDAO();
            TrabajoRecepcionalDTO trabajoRecepcionalDTO = new TrabajoRecepcionalDTO
            {
                NombreEstudiante = "José Martínez", // Duplicado
                Titulo = "Estudio sobre Big Data",
                FechaPublicacion = "01-01-2023",
                TipoTrabajo = "Investigación",
                RolAcademico = "Asesor",
                IdAcademico = 1
            };

            // Act
            int result = trabajoRecepcionalDAO.AgregarTrabajoRecepcional(trabajoRecepcionalDTO);

            // Assert
            Assert.AreEqual(-3, result); // Se espera -3 indicando duplicado
        }

        [TestMethod]
        public void ObtenerUltimoIdTrabajoRecepcionalTest()
        {
            // Arrange
            TrabajoRecepcionalDAO trabajoRecepcionalDAO = new TrabajoRecepcionalDAO();

            // Act
            int? result = trabajoRecepcionalDAO.ObtenerUltimoIdTrabajoRecepcional();

            // Assert
            Assert.IsTrue(result.HasValue); // Se espera que el último ID sea válido
        }






















    }



}
