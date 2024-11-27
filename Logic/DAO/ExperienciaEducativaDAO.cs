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
    public class ExperienciaEducativaDAO {
        private readonly ConstanciasEntities _context;

        public int AgregarExperiencia(ExperienciaEducativaDTO experienciaEducativa)
        {
            var experienciaDB = EntityFactory.CrearExperienciaEducativa(experienciaEducativa);

            _context.ExperienciaEducativa.Add(experienciaDB);
            int registrosAfectados = _context.SaveChanges();

            // Retornar 1 si se registra correctamente
            return registrosAfectados > 0 ? 1 : 0;

        }



        public int? ObtenerIdExperienciaPorNombre(string nombre)
        {
            var experienciaEducativa = _context.ExperienciaEducativa
                                         .FirstOrDefault(a => a.Nombre == nombre);

            return experienciaEducativa?.IdExperienciaEducativa;
        }

        public  int? ObtenerUltimoIdExperienciaEducativa()
        {
            
                // Recupera la última experiencia educativa basada en el orden descendente de IdExperienciaEducativa
                var ultimaExperiencia = _context.ExperienciaEducativa
                                               .OrderByDescending(e => e.IdExperienciaEducativa)
                                               .FirstOrDefault();

                return ultimaExperiencia?.IdExperienciaEducativa; // Devuelve null si no hay experiencias registradas
            
        }







    }
}
