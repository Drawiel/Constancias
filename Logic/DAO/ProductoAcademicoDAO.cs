using DataAccess;
using Logic.Clases;
using Logic.Factories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DAO {
    public class ProductoAcademicoDAO {
        private readonly ConstanciasEntities _context;

        public ProductoAcademicoDAO()
        {
            _context = new ConstanciasEntities();
        }

        public int AgregarProductoAcademico(ProductoAcademicoDTO producto)
        {
            try
            {
                // Validar datos requeridos
                if (string.IsNullOrEmpty(producto.Titulo) || string.IsNullOrEmpty(producto.TipoProducto))
                {
                    Console.WriteLine("El título y el tipo de producto son obligatorios.");
                    return -3; // Código para datos inválidos
                }

                var productoAcademicoDB = EntityFactory.CrearProductoAcademico(producto);

                // Agregar al contexto
                _context.ProductoAcademico.Add(productoAcademicoDB);

                // Guardar cambios en la base de datos
                int registrosAfectados = _context.SaveChanges();
                return registrosAfectados > 0 ? 1 : 0; // Retorna 1 si se guarda correctamente
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error en la base de datos: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.StackTrace}");
                }
                return -1; // Código de error general de base de datos
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error general
            }
        }


        public int? ObtenerUltimoIdProductoAcademico()
        {
            try
            {
                // Recupera el último producto académico basado en el orden descendente de IdProducto
                var ultimoProducto = _context.ProductoAcademico
                                              .OrderByDescending(p => p.IdProducto)
                                              .FirstOrDefault();

                return ultimoProducto?.IdProducto; // Devuelve null si no hay productos académicos registrados
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al obtener el último ID de producto académico: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
            }
        }

        public List<string> ObtenerProductosAcademicosDeAcademico(int idAcademico) {
            try {
                var listaProductos = _context.ProductoAcademico.Where(p => (p.IdAcademico == idAcademico)).Select(p => p.Titulo).ToList();

                return listaProductos;
            } catch (SqlException ex) {
                Console.WriteLine("Error de SQL al obtener los productos academicos del academico");
                return new List<string>();
            } catch (Exception ex) {
                Console.WriteLine($"Error general: {ex.Message}");
                return new List<string>();
            }
        }






    }
}
