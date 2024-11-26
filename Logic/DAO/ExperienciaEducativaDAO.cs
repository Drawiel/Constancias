using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DAO {
    public class ExperienciaEducativaDAO {
        private readonly gestionconstanciasEntities _context;

        public ExperienciaEducativaDAO(gestionconstanciasEntities context) {
            _context = context;
            
        }

        public int? ObtenerIdExperienciaPorNombre(string nombre)
        {
            var areaAcademica = _context.areaacademicas
                                         .FirstOrDefault(a => a.nombre == nombre);

            return areaAcademica?.idAreaAcademica;
        }

        




    }
}
