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
    public class TrabajoRecepcionalDAO {

        private readonly ConstanciasEntities _context;


        public int AgregarTrabajoRecepcional(TrabajoRecepcionalDTO trabajo)
        {
            var trabajoRecepcionalDB = EntityFactory.CrearTrabajoRecepcional(trabajo);
            _context.TrabajoRecepcional.Add(trabajoRecepcionalDB);

            // Guardar cambios en la base de datos
            int registrosAfectados = _context.SaveChanges();

            // Retornar 1 si se registra correctamente
            return registrosAfectados > 0 ? 1 : 0;

        }

        public  int? ObtenerUltimoIdTrabajoRecepcional()
        {
                var ultimoTrabajo = _context.TrabajoRecepcional
                                           .OrderByDescending(t => t.IdTrabajoRecepcional)
                                           .FirstOrDefault();

                return ultimoTrabajo?.IdTrabajoRecepcional; 
        }

    }
}
