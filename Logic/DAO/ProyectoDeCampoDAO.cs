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

        private readonly ConstanciasEntities _context;
        

        public int AgregarProyectoCampo(ProyectoDeCampoDTO nuevoProyecto)
        {
            var proyectoDB = EntityFactory.CrearProyectoCampo(nuevoProyecto);
            _context.ProyectoCampo.Add(proyectoDB);
            int registrosAfectados = _context.SaveChanges();

            // Retornar 1 si se registra correctamente
            return registrosAfectados > 0 ? 1 : 0;
        }

        public  int? ObtenerUltimoIdProyectoCampo()
        {
            
                // Recupera el último proyecto de campo basado en el orden descendente de IdProyectoCampo
                var ultimoProyecto = _context.ProyectoCampo
                                             .OrderByDescending(p => p.IdProyectoCampo)
                                             .FirstOrDefault();

                return ultimoProyecto?.IdProyectoCampo; // Devuelve null si no hay proyectos de campo registrados
            
        }

    }
}
