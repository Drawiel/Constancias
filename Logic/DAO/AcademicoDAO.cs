using DataAccess;
using Logic.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
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




        public void AgregarAcademico(academico academico)
        {

            _context.academicoes.Add(academico);
            _context.SaveChanges();

        }
    }
    }
