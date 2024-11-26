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
        private readonly gestionconstanciasEntities _context;

        public ParticipacionDAO(gestionconstanciasEntities context)
        {
            _context = context;
        }

        public void AgregarParticipacion(ParticipacionDTO nuevaParticipacion)
        {
            var participacionDb = EntityFactory.CrearParticipacion(nuevaParticipacion);
            _context.participacions.Add(participacionDb);
            _context.SaveChanges();
        }

        public int? ObtenerIdPorNombre(string nombre)
        {
            var tipo = _context.tipoparticipacions
                                .FirstOrDefault(tp => tp.nombre == nombre);

            return tipo?.idTipoParticipacion;
        }

    }
}
