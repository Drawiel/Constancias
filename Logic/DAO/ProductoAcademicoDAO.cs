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
    public class ProductoAcademicoDAO {
        private readonly ConstanciasEntities _context;

       

        public int AgregarProductoAcademico(ProductoAcademicoDTO producto)
        {
                var productoAcademicoDB = EntityFactory.CrearProductoAcademico(producto);
                _context.ProductoAcademico.Add(productoAcademicoDB);
            int registrosAfectados = _context.SaveChanges();

            // Retornar 1 si se registra correctamente
            return registrosAfectados > 0 ? 1 : 0;
        }

        public  int? ObtenerUltimoIdProductoAcademico()
        {
            
                // Recupera el último producto académico basado en el orden descendente de IdProducto
                var ultimoProducto = _context.ProductoAcademico
                                            .OrderByDescending(p => p.IdProducto)
                                            .FirstOrDefault();

                return ultimoProducto?.IdProducto; // Devuelve null si no hay productos académicos registrados
            
        }





    }
}
