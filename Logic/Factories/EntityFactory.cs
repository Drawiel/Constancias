using DataAccess;
using Logic.Clases;
using Logic.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Factories
{
    public class EntityFactory
    {
        public static academico CrearAcademico(AcademicoDTO academicoDto)
        {
            return new academico
            {
                nombre = academicoDto.Nombre,
                idAreaAcademica = academicoDto.IdAreaAcademica,
                idTipoPersonal = academicoDto.IdTipoPersonal,
                numeroPersonal = academicoDto.NumeroPersonal,
                fechaContratacion = academicoDto.FechaContratacion
            };
        }

        public static ProductoAcademico CrearProductoAcademico(ProductoAcademicoDTO productoDto)
        {
            return new ProductoAcademico
            {
                idProducto = productoDto.IdProducto,
                titulo = productoDto.Titulo,
                fechaPublicacion = productoDto.FechaPublicacion,
                idTipoProducto = productoDto.IdTipoProducto,
                idTipoPublicacion = productoDto.IdTipoPublicacion,
                idAcademico = productoDto.IdAcademico
            };
        }

        public static proyectocampo CrearProyectoCampo(ProyectoDeCampoDTO proyectoDto)
        {
            return new proyectocampo
            {
                idProyectoCampo = proyectoDto.IdProyectoCampo,
                nombre = proyectoDto.Nombre,
                lugarRealizacion = proyectoDto.LugarRealizacion,
                idPeriodo = proyectoDto.IdPeriodo,
                idRolAcademico = proyectoDto.IdRolAcademico,
                idAcademico = proyectoDto.IdAcademico
            };
        }

        public static participacion CrearParticipacion(ParticipacionDTO participacionDto)
        {
            return new participacion
            {
                idProyectoCampo = participacionDto.IdProyectoCampo,
                nombre = participacionDto.Nombre,
                idPeriodo = participacionDto.IdPeriodo,
                idTipoParticipacion = participacionDto.IdTipoParticipacion,
                idProgramaEducativo = participacionDto.IdProgramaEducativo,
                idAcademico = participacionDto.IdAcademico
            };
        }

        public static constancia CrearConstancia(ConstanciaDTO constanciaDto)
        {
            return new constancia
            {
                idConstancia = constanciaDto.IdConstancia,
                fechaExpedicion = constanciaDto.FechaExpedicion,
                idTipoConstancia = constanciaDto.IdTipoConstancia,
                idAcademico = constanciaDto.IdAcademico,
                firmaElectronica = constanciaDto.FirmaElectronica
            };
        }

        public static programaeducativo CrearProgramaEducativo(ProgramaEducativoDTO programaDto)
        {
            return new programaeducativo
            {
                idProgramaEducativo = programaDto.IdProgramaEducativo,
                nombre = programaDto.Nombre,
                fecha = programaDto.Fecha,
                idAreaAcademica = programaDto.IdAreaAcademica
            };
        }







    }
}
