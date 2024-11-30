using DataAccess;
using Logic.Clases;
using Logic.Factories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DAO {


    public class AcademicoDAO
    {
        private readonly ConstanciasEntities _context;

        public AcademicoDAO()
        {
            _context = new ConstanciasEntities();
        }




        public int AgregarAcademico(AcademicoDTO academico)
        {
            try
            {
                var academicoDB = EntityFactory.CrearAcademico(academico);

                _context.Academico.Add(academicoDB);

                _context.SaveChanges();

                return 1; 
            }
            catch (DbEntityValidationException dbEx)
            {
                StringBuilder errorMessage = new StringBuilder();
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage.AppendLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }

                Console.WriteLine(errorMessage.ToString()); 
                return -2; 
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error al actualizar la base de datos: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.StackTrace}");
                    
                }
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -1; 
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
                return -1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; 
            }
        }

        public int? ObtenerUltimoIdAcademico()
        {
            try
            {
                var ultimoAcademico = _context.Academico
                                              .OrderByDescending(a => a.IdAcademico)
                                              .FirstOrDefault();

                return ultimoAcademico?.IdAcademico; 
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el último ID del académico: {ex.Message}");
                return -1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; 
            }
        }

        public int ActualizarAcademico(AcademicoDTO academicoActualizado)
        {
            try
            {
                var academicoDB = _context.Academico.FirstOrDefault(a => a.NumeroPersonal == academicoActualizado.NumeroPersonal);

                if (academicoDB == null)
                {
                    Console.WriteLine("No se encontró al académico.");
                    return -1; 
                }

                academicoDB.Nombre = academicoActualizado.Nombre;
                academicoDB.TipoContratacion = academicoActualizado.TipoContratacion;
                academicoDB.AreaAcademica = academicoActualizado.AreaAcademica;
                academicoDB.FechaContratacion = academicoActualizado.FechaContratacion;
                academicoDB.IdPrograma = academicoActualizado.IdPrograma;

                int registrosAfectados = _context.SaveChanges();
                return registrosAfectados > 0 ? 1 : 0; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar el académico: {ex.Message}");
                return -2; 
            }
        }

        public int ActualizarContraseñaYNombreAcademico(int idAcademico, string nuevaContraseña)
        {
            try
            {
                var academicoDB = _context.Academico.FirstOrDefault(a => a.IdAcademico == idAcademico);

                if (academicoDB == null)
                {
                    Console.WriteLine("No se encontró al académico.");
                    return -1; 
                }

                academicoDB.Contrasena = nuevaContraseña;
                
                

                int registrosAfectados = _context.SaveChanges();
                return registrosAfectados > 0 ? 1 : 0; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la contraseña y el nombre del académico: {ex.Message}");
                return -2; 
            }
        }

        public bool ValidarContraseña(int idAcademico, string contrasenaIngresada)
        {
            try
            {
                var academico = _context.Academico
                                        .FirstOrDefault(a => a.IdAcademico == idAcademico);

                if (academico == null)
                {
                    Console.WriteLine("Académico no encontrado.");
                    return false;
                }

                if (!string.IsNullOrEmpty(academico.Contrasena))
                {
                    if (academico.Contrasena == contrasenaIngresada)
                    {
                        return true; 
                    }
                    else
                    {
                        Console.WriteLine("Contraseña incorrecta.");
                        return false; 
                    }
                }
                else
                {
                    Console.WriteLine("Este académico no tiene contraseña registrada.");
                    return false; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al validar la contraseña: {ex.Message}");
                return false; 
            }
        }

        public string ObtenerNombreAcademicoPorNumeroPersonal(string numeroPersonal)
        {
            try
            {
                var academico = _context.Academico
                                        .FirstOrDefault(a => a.NumeroPersonal == numeroPersonal);

                if (academico != null)
                {
                    return academico.Nombre;
                }
                else
                {
                    Console.WriteLine("Académico no encontrado.");
                    return string.Empty; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el nombre del académico: {ex.Message}");
                return string.Empty; 
            }
        }



        public int AgregarAcademicoBasico( string numeroPersonal, string contraseña)
        {
            try
            {
                var academicoExistente = _context.Academico.FirstOrDefault(a => a.NumeroPersonal == numeroPersonal);

                if (academicoExistente != null)
                {
                    Console.WriteLine("Ya existe un académico con este número de personal.");
                    return -3; 
                }

                Academico nuevoAcademico = new Academico
                {
                    
                    NumeroPersonal = numeroPersonal,
                    Contrasena = contraseña,
                    
                };

                _context.Academico.Add(nuevoAcademico);

                int registrosAfectados = _context.SaveChanges();

                return registrosAfectados > 0 ? 1 : 0; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar el académico: {ex.Message}");
                return -2; 
            }
        }

        public int ValidarYRegistrarAcademico(AcademicoDTO academicoDTO)
        {
            try
            {
                var academicoExistente = _context.Academico.FirstOrDefault(a => a.NumeroPersonal == academicoDTO.NumeroPersonal);

                if (academicoExistente != null)
                {
                    if (!academicoExistente.Nombre.Equals(academicoDTO.Nombre, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Error: El número personal ya está registrado con un nombre diferente.");
                        return -4; 
                    }

                    int resultadoActualizacion = ActualizarAcademico(academicoDTO);
                    if (resultadoActualizacion == 1)
                    {
                        return 1; 
                    }
                    else
                    {
                        return -1; 
                    }
                }
                else
                {
                    int resultadoRegistro = AgregarAcademico(academicoDTO);
                    if (resultadoRegistro == 1)
                    {
                        return 1; 
                    }
                    else
                    {
                        return -1; 
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                Console.WriteLine($"Error de duplicidad: {ex.Message}");
                return -3;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL: {ex.Message}");
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2;
            }
        }

        public int BuscarAcademicoPorNumeroPersonal(string numeroPersonal)
        {
            try
            {
                var academico = _context.Academico
                                        .FirstOrDefault(a => a.NumeroPersonal == numeroPersonal);

                if (academico == null)
                {
                    return 0; 
                }

                if (!string.IsNullOrEmpty(academico.Contrasena))
                {
                    return 1; 
                }

                return 2; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar al académico: {ex.Message}");
                return -1; 
            }
        }

        public int? ObtenerIdProgramaDeAcademicoPorIdAcademico(int idAcademico)
        {
            try
            {
                var idPrograma = _context.Academico.Where(p => (p.IdAcademico == idAcademico)).Select(p => p.IdAcademico).FirstOrDefault();
                return idPrograma;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }



    }
}
    
