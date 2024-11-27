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
    public class ParticipacionDAO {
        private readonly ConstanciasEntities _context;

       

        public int AgregarParticipacion(ParticipacionDTO nuevaParticipacion)
        {
            var participacionDb = EntityFactory.CrearParticipacion(nuevaParticipacion);
            _context.Participacion.Add(participacionDb);
            int registrosAfectados = _context.SaveChanges();

            // Retornar 1 si se registra correctamente
            return registrosAfectados > 0 ? 1 : 0;
        }

        public  int? ObtenerUltimoIdParticipacion()
        {
            
                // Recupera la última participación basada en el orden descendente de IdParticipacion
                var ultimaParticipacion = _context.Participacion
                                                 .OrderByDescending(p => p.IdParticipacion)
                                                 .FirstOrDefault();

                return ultimaParticipacion?.IdParticipacion; // Devuelve null si no hay participaciones registradas
            
        }


    }
}
