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
using System.Windows.Controls.Primitives;

namespace Logic.DAO {
    public class ExperienciaEducativaDAO {
        private readonly ConstanciasEntities _context;
        
        public ExperienciaEducativaDAO()
        {
            _context = new ConstanciasEntities();
        }

        public int AgregarExperiencia(ExperienciaEducativaDTO experienciaEducativa)
        {
            try
            {
                var experienciaDB = EntityFactory.CrearExperienciaEducativa(experienciaEducativa);

                _context.ExperienciaEducativa.Add(experienciaDB);
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
                Console.WriteLine($"Error de SQL al agregar la experiencia educativa: {ex.Message}");
                return -1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; 
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
                return -1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; 
            }
        }

        public int ObtenerORegistrarExperiencia(ExperienciaEducativaDTO experienciaEducativa)
        {
            try
            {
                int? idExistente = ObtenerIdExperienciaPorNombre(experienciaEducativa.Nombre);

                if (idExistente.HasValue)
                {
                    return 2;
                }

                int resultado = AgregarExperiencia(experienciaEducativa);
                return resultado; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; 
            }
        }


        public string ObtenerNombrePorId(int idExperiencia)
        {
            try
            {
                var experienciaEducativa = _context.ExperienciaEducativa
                                                   .FirstOrDefault(e => e.IdExperienciaEducativa == idExperiencia);

                return experienciaEducativa?.Nombre ?? "ID no encontrado";
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el nombre de la experiencia educativa por ID: {ex.Message}");
                return "Error de base de datos"; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return "Error general"; 
            }
        }



        public int? ObtenerUltimoIdExperienciaEducativa()
        {
            try
            {
                var ultimaExperiencia = _context.ExperienciaEducativa
                                                .OrderByDescending(e => e.IdExperienciaEducativa)
                                                .FirstOrDefault();

                return ultimaExperiencia?.IdExperienciaEducativa; 
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el último ID de experiencia educativa: {ex.Message}");
                return -1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; 
            }
        }

         public int RegistrarRelacionAcademicoExperiencia(int idAcademico, int idExperienciaEducativa)
         {
             try
             {
                 
                 var academico = _context.Academico
                                         .FirstOrDefault(a => a.IdAcademico == idAcademico);

                 var experiencia = _context.ExperienciaEducativa
                                           .FirstOrDefault(e => e.IdExperienciaEducativa == idExperienciaEducativa);

                 if (academico == null || experiencia == null)
                 {
                     return -1; 
                 }

                 academico.ExperienciaEducativa.Add(experiencia);

                 return _context.SaveChanges(); 
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"Error al registrar relación: {ex.Message}");
                 return -2; 
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
