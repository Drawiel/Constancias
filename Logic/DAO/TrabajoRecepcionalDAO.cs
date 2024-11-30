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
    public class TrabajoRecepcionalDAO {

        private readonly ConstanciasEntities _context;

        public TrabajoRecepcionalDAO()
        {
            _context = new ConstanciasEntities();
        }

        public int AgregarTrabajoRecepcional(TrabajoRecepcionalDTO trabajo)
        {
            try
            {
                var trabajoRecepcionalDB = EntityFactory.CrearTrabajoRecepcional(trabajo);
                _context.TrabajoRecepcional.Add(trabajoRecepcionalDB);

                // Guardar cambios en la base de datos
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
                Console.WriteLine($"Error de SQL al agregar el trabajo recepcional: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
            }
        }

        public int? ObtenerUltimoIdTrabajoRecepcional()
        {
            try
            {
                var ultimoTrabajo = _context.TrabajoRecepcional
                                            .OrderByDescending(t => t.IdTrabajoRecepcional)
                                            .FirstOrDefault();

                return ultimoTrabajo?.IdTrabajoRecepcional; // Devuelve null si no hay registros
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el último ID de trabajo recepcional: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
            }
        }

        public List<string> ObtenerTrabajosRecepcionalesDeAcademico(int idAcademico) {
            try {
                var listaTrabajos = _context.TrabajoRecepcional.Where(t => (t.IdAcademico == idAcademico)).Select(t => t.Titulo).ToList();

                return listaTrabajos;
            } catch (SqlException ex) {
                Console.WriteLine("Error de SQL al obtener los productos academicos del academico");
                return new List<string>();
            } catch (Exception ex) {
                Console.WriteLine($"Error general: {ex.Message}");
                return new List<string>();
            }
        }




    }
}
