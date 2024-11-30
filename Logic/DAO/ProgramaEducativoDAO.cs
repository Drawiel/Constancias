using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DAO {
    public class ProgramaEducativoDAO
    {

        private readonly ConstanciasEntities _context;

        public ProgramaEducativoDAO()
        {
            _context = new ConstanciasEntities();
        }

        public int? ObtenerIdProgramaPorNombre(string nombrePrograma)
        {
            try
            {
                var programa = _context.ProgramaEducativo
                                       .FirstOrDefault(p => p.Nombre == nombrePrograma);

                return programa?.IdPrograma; 
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el ID del programa por nombre: {ex.Message}");
                return -1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general al obtener el ID del programa: {ex.Message}");
                return -2; 
            }
        }

        public List<int> ObtenerIdsProgramas(List<string> nombresProgramas)
        {
            var listaIds = new List<int>();
            ProgramaEducativoDAO programaEducativoDAO = new ProgramaEducativoDAO();

            foreach (var nombre in nombresProgramas)
            {
                var id = programaEducativoDAO.ObtenerIdProgramaPorNombre(nombre);

                if (id.HasValue)
                {
                    listaIds.Add(id.Value);
                }
                else
                {
                    Console.WriteLine($"No se encontró el programa educativo con nombre: {nombre}");
                }
            }

            return listaIds;
        }

        public string ObtenerNombreProgramaPorIdPrograma(int idPrograma) {
            try {
                var nombrePrograma = _context.ProgramaEducativo.Where(p => (p.IdPrograma == idPrograma)).Select(p => p.Nombre).FirstOrDefault();
                return nombrePrograma;
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
