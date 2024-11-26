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
        private readonly gestionconstanciasEntities _context;

        public AcademicoDAO(gestionconstanciasEntities context)
        {
            _context = context;
        }




        public void AgregarAcademico(AcademicoDTO academico)
        {
            var academicoDB = EntityFactory.CrearAcademico(academico);

            _context.academicoes.Add(academicoDB);
            _context.SaveChanges();

        }

        public int? ObtenerIdAcademicoPorNumeroPersonal(string numeroPersonal)
        {
            var academico = _context.academicoes
                                     .FirstOrDefault(a => a.numeroPersonal == numeroPersonal);

            return academico?.idAcademico;
        }

        public int? ObtenerIdRolPorNombre(string nombre)
        {
            var rol = _context.rolacademicoes
                               .Where(r => r.nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                               .Select(r => r.idRolAcademico)
                               .FirstOrDefault();

            return rol;
        }
    }
}
    
