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

                // Retornar 1 si se registra correctamente
                return registrosAfectados > 0 ? 1 : 0;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                // Manejo de duplicados en la base de datos
                Console.WriteLine($"Error de duplicidad: {ex.Message}");
                return -3; // Código de error para duplicidad de valores únicos
            }
            catch (SqlException ex)
            {
                // Manejo de errores generales de SQL
                Console.WriteLine($"Error de SQL al agregar la experiencia educativa: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                // Manejo de cualquier otro tipo de error
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

        public int ObtenerORegistrarExperiencia(ExperienciaEducativaDTO experienciaEducativa)
        {
            try
            {
                // Intentar obtener el ID de la experiencia educativa por nombre
                int? idExistente = ObtenerIdExperienciaPorNombre(experienciaEducativa.Nombre);

                if (idExistente.HasValue)
                {
                    // Si ya existe, devolver 2 para indicar que ya está registrada
                    return 2;
                }

                // Si no existe, intentar registrarla
                int resultado = AgregarExperiencia(experienciaEducativa);
                return resultado; // 1 si se registró correctamente, o el código de error correspondiente
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
            }
        }


        public string ObtenerNombrePorId(int idExperiencia)
        {
            try
            {
                // Buscar la experiencia educativa por ID
                var experienciaEducativa = _context.ExperienciaEducativa
                                                   .FirstOrDefault(e => e.IdExperienciaEducativa == idExperiencia);

                // Devolver el nombre si se encuentra
                return experienciaEducativa?.Nombre ?? "ID no encontrado";
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el nombre de la experiencia educativa por ID: {ex.Message}");
                return "Error de base de datos"; // Mensaje de error general
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return "Error general"; // Mensaje de error general
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











    }
}
