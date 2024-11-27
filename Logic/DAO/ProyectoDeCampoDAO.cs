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
    public class ProyectoDeCampoDAO {

        private readonly ConstanciasEntities _context;


        public int AgregarProyectoCampo(ProyectoDeCampoDTO nuevoProyecto)
        {
            try
            {
                var proyectoDB = EntityFactory.CrearProyectoCampo(nuevoProyecto);
                _context.ProyectoCampo.Add(proyectoDB);
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
                Console.WriteLine($"Error de SQL al agregar el proyecto de campo: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
            }
        }

        public int? ObtenerUltimoIdProyectoCampo()
        {
            try
            {
                // Recupera el último proyecto de campo basado en el orden descendente de IdProyectoCampo
                var ultimoProyecto = _context.ProyectoCampo
                                              .OrderByDescending(p => p.IdProyectoCampo)
                                              .FirstOrDefault();

                return ultimoProyecto?.IdProyectoCampo; // Devuelve null si no hay proyectos de campo registrados
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el último ID del proyecto de campo: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
            }
        }


    }
}
