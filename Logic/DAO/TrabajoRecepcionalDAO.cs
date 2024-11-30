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
                Console.WriteLine($"Error de SQL al agregar el trabajo recepcional: {ex.Message}");
                return -1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; 
            }
        }

        public int? ObtenerUltimoIdTrabajoRecepcional()
        {
            try
            {
                var ultimoTrabajo = _context.TrabajoRecepcional
                                            .OrderByDescending(t => t.IdTrabajoRecepcional)
                                            .FirstOrDefault();

                return ultimoTrabajo?.IdTrabajoRecepcional; 
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el último ID de trabajo recepcional: {ex.Message}");
                return -1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; 
            }
        }

        public List<string> ObtenerTrabajosRecepcionalesDeAcademico(int idAcademico) {
            try {
                var listaTrabajos = _context.TrabajoRecepcional.Where(t => (t.IdAcademico == idAcademico)).Select(t => t.Titulo).ToList();

                return listaTrabajos;
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
