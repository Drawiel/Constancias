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
        public static Academico CrearAcademico(AcademicoDTO academicoDto)
        {
            return new Academico
            {
                IdAcademico = academicoDto.IdAcademico,
                Nombre = academicoDto.Nombre,
                AreaAcademica = academicoDto.AreaAcademica,
                TipoContratacion = academicoDto.TipoContratacion,
                NumeroPersonal = academicoDto.NumeroPersonal,
                FechaContratacion = academicoDto.FechaContratacion,
                IdPrograma = academicoDto.IdPrograma
            };
        }

        public static ProductoAcademico CrearProductoAcademico(ProductoAcademicoDTO productoDto)
        {
            return new ProductoAcademico
            {
                IdProducto = productoDto.IdProducto,
                Titulo = productoDto.Titulo,
                FechaPublicacion = productoDto.FechaPublicacion,
                Tipo = productoDto.TipoProducto,
                TipoPublicacion = productoDto.TipoPublicacion,
                IdAcademico = productoDto.IdAcademico
            };
        }

        public static ProyectoCampo CrearProyectoCampo(ProyectoDeCampoDTO proyectoDto)
        {
            return new ProyectoCampo
            {
                IdProyectoCampo = proyectoDto.IdProyectoCampo,
                NombreProyecto = proyectoDto.Nombre,
                LugarRealizacion = proyectoDto.LugarRealizacion,
                Periodo = proyectoDto.Periodo,
                RolAcademico = proyectoDto.RolAcademico,
                IdAcademico = proyectoDto.IdAcademico
            };
        }

        public static Participacion CrearParticipacion(ParticipacionDTO participacionDto)
        {
            return new Participacion
            {
                IdParticipacion = participacionDto.IdParticipacion,
                PeriodoParticipacion = participacionDto.PeriodoParticipacion,
                TipoParticipacion = participacionDto.TipoParticipacion,
                IdPrograma = participacionDto.IdProgramaEducativo,
                IdAcademico = participacionDto.IdAcademico
            };
        }

        public static Constancia CrearConstancia(ConstanciaDTO constanciaDto)
        {
            return new Constancia
            {
                IdConstancia = constanciaDto.IdConstancia,
                FechaExpedicion = constanciaDto.FechaExpedicion,
                Tipo = constanciaDto.TipoConstancia,
                IdAcademico = constanciaDto.IdAcademico,
                Solicitante = constanciaDto.Solicitante,
            };
        }

        public static ProgramaEducativo CrearProgramaEducativo(ProgramaEducativoDTO programaDto)
        {
            return new ProgramaEducativo
            {
                IdPrograma = programaDto.IdProgramaEducativo,
                Nombre = programaDto.Nombre,
                Año = programaDto.Año,
                AreaAcademica = programaDto.AreaAcademica
            };
        }

        public static ExperienciaEducativa CrearExperienciaEducativa(ExperienciaEducativaDTO experienciaEducativa) 
        {
            return new ExperienciaEducativa
            {
                IdExperienciaEducativa = experienciaEducativa.IdExperienciaEducativa,
                Nombre = experienciaEducativa.Nombre,
                IdPrograma = experienciaEducativa.IdProgramaEducativo
            };
        }


        public static TrabajoRecepcional CrearTrabajoRecepcional(TrabajoRecepcionalDTO trabajoRecepcional)
        {
            return new TrabajoRecepcional
            {
                IdTrabajoRecepcional = trabajoRecepcional.IdTrabajoRecepcional,
                IdAcademico = trabajoRecepcional.IdAcademico,
                TipoTrabajo = trabajoRecepcional.TipoTrabajo,
                RolAcademico = trabajoRecepcional.RolAcademico,
                Titulo = trabajoRecepcional.Titulo,
                NombreEstudiante = trabajoRecepcional.NombreEstudiante,
                FechaPresentacion = trabajoRecepcional.FechaPublicacion
            };
        }








    }
}
