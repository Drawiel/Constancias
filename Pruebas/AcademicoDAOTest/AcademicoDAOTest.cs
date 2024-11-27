using Logic.Clases;
using Logic.DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Pruebas.AcademicoDAOTest
{
    [TestClass]
    public class AcademicoDAOTest
    {
        [TestMethod]
        public void AgregarAcademicoTest()
        {
            AcademicoDAO academico = new AcademicoDAO();
            AcademicoDTO academicoDTO = new AcademicoDTO();
            academicoDTO.Nombre = "Juan Perez";
            academicoDTO.AreaAcademica = "Economico - Administrativa";
            academicoDTO.FechaContratacion = "22-10-2022";
            academicoDTO.NumeroPersonal = "345676";
            academicoDTO.IdPrograma = 1;
        }
    }
}
