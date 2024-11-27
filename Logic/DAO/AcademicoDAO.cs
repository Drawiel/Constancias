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


    public class AcademicoDAO
    {
        private readonly ConstanciasEntities _context;

        /*public AcademicoDAO(ConstanciasEntities context)
        {
            _context = context;
        }
        */



        public int AgregarAcademico(AcademicoDTO academico)
        {
            var academicoDB = EntityFactory.CrearAcademico(academico);

            _context.Academico.Add(academicoDB);
            int registrosAfectados = _context.SaveChanges();

            // Retornar 1 si se registra correctamente
            return registrosAfectados > 0 ? 1 : 0;

        }

        public int? ObtenerIdAcademicoPorNumeroPersonal(string numeroPersonal)
        {
            var academico = _context.Academico
                                     .FirstOrDefault(a => a.NumeroPersonal == numeroPersonal);

            return academico?.IdAcademico;
        }

        public  int? ObtenerUltimoIdAcademico()
        {
            
                // Recupera el último académico basado en el orden descendente de IdAcademico
                var ultimoAcademico = _context.Academico
                                             .OrderByDescending(a => a.IdAcademico)
                                             .FirstOrDefault();

                return ultimoAcademico?.IdAcademico; // Devuelve null si no hay académicos en la tabla
          
        }



    }
}
    
