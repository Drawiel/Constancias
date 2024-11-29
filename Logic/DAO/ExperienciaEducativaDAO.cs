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
    public class ExperienciaEducativaDAO {
        private readonly ConstanciasEntities _context;

        public int AgregarExperiencia(ExperienciaEducativaDTO experienciaEducativa)
        {
            try
            {
                var experienciaDB = EntityFactory.CrearExperienciaEducativa(experienciaEducativa);

                _context.ExperienciaEducativa.Add(experienciaDB);
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
                Console.WriteLine($"Error de SQL al agregar la experiencia educativa: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
            }
        }

        public int? ObtenerIdExperienciaPorNombre(string nombre)
        {
            try
            {
                var experienciaEducativa = _context.ExperienciaEducativa
                                                   .FirstOrDefault(a => a.Nombre == nombre);

                return experienciaEducativa?.IdExperienciaEducativa;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el ID de la experiencia educativa por nombre: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
            }
        }

        public int? ObtenerUltimoIdExperienciaEducativa()
        {
            try
            {
                // Recupera la última experiencia educativa basada en el orden descendente de IdExperienciaEducativa
                var ultimaExperiencia = _context.ExperienciaEducativa
                                                .OrderByDescending(e => e.IdExperienciaEducativa)
                                                .FirstOrDefault();

                return ultimaExperiencia?.IdExperienciaEducativa; // Devuelve null si no hay experiencias registradas
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el último ID de experiencia educativa: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
            }
        }

         public int RegistrarRelacionAcademicoExperiencia(int idAcademico, int idExperienciaEducativa)
         {
             try
             {
                 // Recuperar las entidades
                 var academico = _context.Academico
                                         .FirstOrDefault(a => a.IdAcademico == idAcademico);

                 var experiencia = _context.ExperienciaEducativa
                                           .FirstOrDefault(e => e.IdExperienciaEducativa == idExperienciaEducativa);

                 // Verificar que existan
                 if (academico == null || experiencia == null)
                 {
                     return -1; // Código de error: uno de los registros no existe
                 }

                 // Agregar la relación
                 academico.ExperienciaEducativa.Add(experiencia);

                 // Guardar los cambios
                 return _context.SaveChanges(); // Retorna el número de registros afectados
             }
             catch (Exception ex)
             {
                 // Manejo de errores (puedes agregar manejo específico de excepciones)
                 Console.WriteLine($"Error al registrar relación: {ex.Message}");
                 return -2; // Código de error genérico
             }
         }

        /*public int RegistrarRelacionesAcademicoExperiencias(int idAcademico, List<int> listaIdsExperiencias)
        {
            try
            {
                // Recuperar el académico de la base de datos
                var academico = _context.Academico
                                        .Include(a => a.ExperienciaEducativa) // Incluir la relación existente
                                        .FirstOrDefault(a => a.IdAcademico == idAcademico);

                if (academico == null)
                {
                    return -1; // Código de error: El académico no existe
                }

                foreach (var idExperiencia in listaIdsExperiencias)
                {
                    // Recuperar cada experiencia educativa
                    var experiencia = _context.ExperienciaEducativa
                                              .FirstOrDefault(e => e.IdExperienciaEducativa == idExperiencia);

                    if (experiencia != null && !academico.ExperienciaEducativa.Contains(experiencia))
                    {
                        // Agregar la experiencia educativa al académico si no existe
                        academico.ExperienciaEducativa.Add(experiencia);
                    }
                }

                // Guardar los cambios en la base de datos
                return _context.SaveChanges(); // Retorna el número de registros afectados
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine($"Error al registrar relaciones: {ex.Message}");
                return -2; // Código de error genérico
            }
        }*/

        public List<string> ObtenerNombreExperienciaEducativaPorIdPrograma(int idPrograma) {
            try {
                var listaExperienciasEnPrograma = _context.ExperienciaEducativa.Where(p => (p.IdPrograma == idPrograma)).Select(p => p.Nombre).ToList();

                return listaExperienciasEnPrograma;
            } catch (SqlException ex) {
                Console.WriteLine("Error de SQL al obtener los productos academicos del academico");
                Console.WriteLine(ex.Message);
                return new List<string>();
            } catch (Exception ex) {
                Console.WriteLine($"Error general: {ex.Message}");
                return new List<string>();
            }
        }











    }
}
