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

        public ProyectoDeCampoDAO()
        {
            _context = new ConstanciasEntities();
        }

        public int AgregarProyectoCampo(ProyectoDeCampoDTO nuevoProyecto)
        {
            try
            {
                var proyectoDB = EntityFactory.CrearProyectoCampo(nuevoProyecto);
                _context.ProyectoCampo.Add(proyectoDB);
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
                Console.WriteLine($"Error de SQL al agregar el proyecto de campo: {ex.Message}");
                return -1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; 
            }
        }

        public int? ObtenerUltimoIdProyectoCampo()
        {
            try
            {
                var ultimoProyecto = _context.ProyectoCampo
                                              .OrderByDescending(p => p.IdProyectoCampo)
                                              .FirstOrDefault();

                return ultimoProyecto?.IdProyectoCampo; 
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el último ID del proyecto de campo: {ex.Message}");
                return -1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; 
            }
        }

        public List<string> ObtenerProyectoDeCampoDeAcademico(int idAcademico) {
            try {
                var listaProyectos = _context.ProyectoCampo.Where(p => (p.IdAcademico == idAcademico)).Select(p => p.NombreProyecto).ToList();

                return listaProyectos;
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
