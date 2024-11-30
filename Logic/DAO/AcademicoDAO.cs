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


    public class AcademicoDAO
    {
        private readonly ConstanciasEntities _context;

        /*public AcademicoDAO(ConstanciasEntities context)
        {
            _context = context;
        }
        */



        public int AgregarAcademico(AcademicoDTO academico)
        {
            try
            {
                var academicoDB = EntityFactory.CrearAcademico(academico);

                _context.Academico.Add(academicoDB);
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
                Console.WriteLine($"Error de SQL al agregar el académico: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
            }
        }

        public int? ObtenerIdAcademicoPorNumeroPersonal(string numeroPersonal)
        {
            try
            {
                var academico = _context.Academico
                                        .FirstOrDefault(a => a.NumeroPersonal == numeroPersonal);

                return academico?.IdAcademico;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el ID del académico por número personal: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
            }
        }

        public int? ObtenerUltimoIdAcademico()
        {
            try
            {
                // Recupera el último académico basado en el orden descendente de IdAcademico
                var ultimoAcademico = _context.Academico
                                              .OrderByDescending(a => a.IdAcademico)
                                              .FirstOrDefault();

                return ultimoAcademico?.IdAcademico; // Devuelve null si no hay académicos en la tabla
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el último ID del académico: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
            }
        }

        public int ActualizarAcademico(AcademicoDTO academicoActualizado)
        {
            try
            {
                var academicoDB = _context.Academico.FirstOrDefault(a => a.IdAcademico == academicoActualizado.IdAcademico);

                if (academicoDB == null)
                {
                    Console.WriteLine("No se encontró al académico.");
                    return -1; // Código de error para "No encontrado"
                }

                // Actualizar los campos excepto contraseña y número de personal
                academicoDB.Nombre = academicoActualizado.Nombre;
                academicoDB.TipoContratacion = academicoActualizado.TipoContratacion;
                academicoDB.AreaAcademica = academicoActualizado.AreaAcademica;
                academicoDB.FechaContratacion = academicoActualizado.FechaContratacion;
                academicoDB.IdPrograma = academicoActualizado.IdPrograma;

                int registrosAfectados = _context.SaveChanges();
                return registrosAfectados > 0 ? 1 : 0; // Retornar 1 si se actualiza correctamente, 0 si no hay cambios
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el académico: {ex.Message}");
                return -2; // Código de error general
            }
        }

        public int ActualizarContraseñaYNombreAcademico(int idAcademico, string nuevaContraseña, string nuevoNombre)
        {
            try
            {
                // Buscar al académico por su ID
                var academicoDB = _context.Academico.FirstOrDefault(a => a.IdAcademico == idAcademico);

                if (academicoDB == null)
                {
                    Console.WriteLine("No se encontró al académico.");
                    return -1; // Código de error para "No encontrado"
                }

                // Actualizar la contraseña y el nombre
                academicoDB.Contrasena = nuevaContraseña;
                academicoDB.Nombre = nuevoNombre;

                // Guardar los cambios en la base de datos
                int registrosAfectados = _context.SaveChanges();
                return registrosAfectados > 0 ? 1 : 0; // Retornar 1 si se actualiza correctamente, 0 si no hay cambios
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la contraseña y el nombre del académico: {ex.Message}");
                return -2; // Código de error general
            }
        }

        public int AgregarAcademicoBasico(string nombre, string numeroPersonal, string contraseña)
        {
            try
            {
                // Verificar si ya existe un académico con el mismo número de personal
                var academicoExistente = _context.Academico.FirstOrDefault(a => a.NumeroPersonal == numeroPersonal);

                if (academicoExistente != null)
                {
                    Console.WriteLine("Ya existe un académico con este número de personal.");
                    return -3; // Código de error para duplicidad
                }

                // Crear una nueva instancia del académico
                Academico nuevoAcademico = new Academico
                {
                    Nombre = nombre,
                    NumeroPersonal = numeroPersonal,
                    Contrasena = contraseña // Si usas hashes para contraseñas, realiza el hash aquí
                };

                // Agregar el académico al contexto
                _context.Academico.Add(nuevoAcademico);

                // Guardar los cambios en la base de datos
                int registrosAfectados = _context.SaveChanges();

                return registrosAfectados > 0 ? 1 : 0; // Retornar 1 si se agrega correctamente, 0 si no hay cambios
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el académico: {ex.Message}");
                return -2; // Código de error general
            }
        }

        public int ValidarYRegistrarAcademico(AcademicoDTO academicoDTO)
        {
            try
            {
                // Verificar si el número personal ya está registrado
                int? idAcademicoExistente = ObtenerIdAcademicoPorNumeroPersonal(academicoDTO.NumeroPersonal);

                if (idAcademicoExistente.HasValue)
                {
                    // Si ya existe, actualizar el académico
                    int resultadoActualizacion = ActualizarAcademico(academicoDTO);
                    if (resultadoActualizacion == 1)
                    {
                        return 1; // Actualización exitosa
                    }
                    else
                    {
                        return -1; // Error al actualizar
                    }
                }
                else
                {
                    // Si no existe, registrar un nuevo académico
                    int resultadoRegistro = AgregarAcademico(academicoDTO);
                    if (resultadoRegistro == 1)
                    {
                        return 1; // Registro exitoso
                    }
                    else
                    {
                        return -1; // Error al registrar
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                // Código de error para duplicidad (2627 o 2601)
                return -3;
            }
            catch (SqlException ex)
            {
                // Código de error general de SQL
                Console.WriteLine($"Error de SQL: {ex.Message}");
                return -1;
            }
            catch (Exception ex)
            {
                // Código de error general
                Console.WriteLine($"Error general: {ex.Message}");
                return -2;
            }
        }

        public int? ObtenerIdProgramaDeAcademicoPorIdAcademico(int idAcademico) {
            try {
                var idPrograma = _context.Academico.Where(p => (p.IdAcademico == idAcademico)).Select(p => p.IdAcademico).FirstOrDefault();
                return idPrograma;
            } catch (SqlException ex) {
                Console.WriteLine(ex.Message);
                return null;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }











    }
}
    
