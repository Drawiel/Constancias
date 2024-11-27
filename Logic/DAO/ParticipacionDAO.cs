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



        public int AgregarParticipacion(ParticipacionDTO nuevaParticipacion)
        {
            try
            {
                var participacionDb = EntityFactory.CrearParticipacion(nuevaParticipacion);
                _context.Participacion.Add(participacionDb);
                int registrosAfectados = _context.SaveChanges();

                // Retornar 1 si se registra correctamente
                return registrosAfectados > 0 ? 1 : 0;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                Console.WriteLine($"Error de duplicidad: {ex.Message}");
                return -3; // Código de error para duplicidad de valores únicos
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al agregar la participación: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
            }
        }

        public int? ObtenerUltimoIdParticipacion()
        {
            try
            {
                // Recupera la última participación basada en el orden descendente de IdParticipacion
                var ultimaParticipacion = _context.Participacion
                                                  .OrderByDescending(p => p.IdParticipacion)
                                                  .FirstOrDefault();

                return ultimaParticipacion?.IdParticipacion; // Devuelve null si no hay participaciones registradas
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el último ID de participación: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
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




    }
}
