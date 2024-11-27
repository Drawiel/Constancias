using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DAO {
    public class ProgramaEducativoDAO
    {

        private readonly ConstanciasEntities _context;  

        

        public int? ObtenerIdProgramaPorNombre(string nombrePrograma)
        {
           
                var programa = _context.ProgramaEducativo
                                      .FirstOrDefault(p => p.Nombre == nombrePrograma);

                return programa?.IdPrograma; 
            
        }
    }
}
