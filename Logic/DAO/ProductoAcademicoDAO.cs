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
        private readonly gestionconstanciasEntities _context;

        public ProductoAcademicoDAO(gestionconstanciasEntities context) {
            _context = context;
        }

        public void AgregarProductoAcademico(ProductoAcademicoDTO producto)
        {
                var productoAcademicoDB = EntityFactory.CrearProductoAcademico(producto);
                _context.productoacademicoes.Add(productoAcademicoDB);
                _context.SaveChanges();
        }

        public int? ObtenerIdParticipacionPorNombre(string nombre)
        {
            var tipo = _context.tipoparticipacions
                                .FirstOrDefault(tp => tp.nombre == nombre);

            return tipo?.idTipoParticipacion;
        }

        public int? ObtenerIdProductoPorNombre(string nombre)
        {
            var tipoProducto = _context.tipoproductoes
                                       .Where(tp => tp.nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase))
                                       .Select(tp => tp.idTipoProducto)
                                       .FirstOrDefault();

            return tipoProducto;
        }




    }
}
