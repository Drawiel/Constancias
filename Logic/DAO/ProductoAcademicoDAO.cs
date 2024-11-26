using DataAccess;
using Logic.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DAO {
    public class ProductoAcademicoDAO {
        private readonly gestionconstanciasEntities _context;
        public ProductoAcademicoDAO(gestionconstanciasEntities context) {
            _context = context;
        }

        public void AgregarProductoAcademico(ProductoAcademico producto)
        {
            
                _context.ProductosAcademicos.Add(producto);
                _context.SaveChanges();
            
        }


    }
}
