using DataAccess;
using Logic.Clases;
using Logic.Factories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DAO {
    public class ParticipacionDAO {
        private readonly ConstanciasEntities _context;

        public ParticipacionDAO()
        {
            _context = new ConstanciasEntities();
        }



        public int AgregarParticipacion(ParticipacionDTO nuevaParticipacion)
        {
            try
            {
                var participacionDb = EntityFactory.CrearParticipacion(nuevaParticipacion);
                _context.Participacion.Add(participacionDb);
                int registrosAfectados = _context.SaveChanges();

              
                return registrosAfectados > 0 ? 1 : 0;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                Console.WriteLine($"Error de duplicidad: {ex.Message}");
                return -3; 
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al agregar la participación: {ex.Message}");
                return -1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; 
            }
        }

        public int? ObtenerUltimoIdParticipacion()
        {
            try
            {
                var ultimaParticipacion = _context.Participacion
                                                  .OrderByDescending(p => p.IdParticipacion)
                                                  .FirstOrDefault();

                return ultimaParticipacion?.IdParticipacion; 
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el último ID de participación: {ex.Message}");
                return -1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; 
            }
        }

        public List<Participacion> ObtenerParticipacionesPorAcademicoYPrograma(int idAcademico, int idPrograma, string tipoParticipacion, string periodo)
        {
            return _context.Participacion
                           .Where(p => p.IdAcademico == idAcademico &&
                                       p.IdPrograma == idPrograma &&
                                       p.TipoParticipacion == tipoParticipacion &&
                                       p.PeriodoParticipacion == periodo)
                           .ToList();
        }

        public List<int?> ObtenerIdParticipacionesActualizacionPorAcademico(int idAcademico) {
            try {
                var participacionesActualizacion = _context.Participacion.Where(p => (p.IdAcademico == idAcademico) && (p.TipoParticipacion == "Actualización")).Select(p => p.IdPrograma).ToList();

                return participacionesActualizacion;
            } catch (SqlException ex) {
                Console.WriteLine("Error de SQL al obtener los productos academicos del academico");
                Console.WriteLine(ex.Message);
                return new List<int?>();
            } catch (Exception ex) {
                Console.WriteLine($"Error general: {ex.Message}");
                return new List<int?>();
            }
        }

        public List<int?> ObtenerIdParticipacionesCertificacionPorAcademico(int idAcademico) {
            try {
                var participacionesActualizacion = _context.Participacion.Where(p => (p.IdAcademico == idAcademico) && (p.TipoParticipacion == "Certificación")).Select(p => p.IdPrograma).ToList();

                return participacionesActualizacion;
            } catch (SqlException ex) {
                Console.WriteLine("Error de SQL al obtener los productos academicos del academico");
                Console.WriteLine(ex.Message);
                return new List<int?>();
            } catch (Exception ex) {
                Console.WriteLine($"Error general: {ex.Message}");
                return new List<int?>();
            }
        }




    }
}
