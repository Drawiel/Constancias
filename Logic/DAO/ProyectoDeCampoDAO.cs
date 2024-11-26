using DataAccess;
using Logic.Clases;
using Logic.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DAO {
    public class ProyectoDeCampoDAO {

        private readonly gestionconstanciasEntities _context;
        public ProyectoDeCampoDAO(gestionconstanciasEntities context) {
            _context = context;
        }

        public void AgregarProyectoCampo(ProyectoDeCampoDTO nuevoProyecto)
        {
            var proyectoDB = EntityFactory.CrearProyectoCampo(nuevoProyecto);
            _context.proyectocampoes.Add(proyectoDB);
            _context.SaveChanges();
        }
    }
}
