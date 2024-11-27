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



        public int? ObtenerIdProgramaPorNombre(string nombrePrograma)
        {
            try
            {
                var programa = _context.ProgramaEducativo
                                       .FirstOrDefault(p => p.Nombre == nombrePrograma);

                return programa?.IdPrograma; // Devuelve null si no encuentra el programa
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el ID del programa por nombre: {ex.Message}");
                return -1; // Código de error para problemas relacionados con SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general al obtener el ID del programa: {ex.Message}");
                return -2; // Código de error general
            }
        }

        public List<int> ObtenerIdsProgramas(List<string> nombresProgramas)
        {
            var listaIds = new List<int>();
            ProgramaEducativoDAO programaEducativoDAO = new ProgramaEducativoDAO();

            foreach (var nombre in nombresProgramas)
            {
                // Obtener el ID por nombre
                var id = programaEducativoDAO.ObtenerIdProgramaPorNombre(nombre);

                // Si el ID no es null, agregarlo a la lista
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


    }
}
