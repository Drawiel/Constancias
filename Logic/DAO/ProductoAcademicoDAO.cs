using DataAccess;
using Logic.Clases;
using Logic.Factories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DAO {
    public class ProductoAcademicoDAO {
        private readonly ConstanciasEntities _context;



        public int AgregarProductoAcademico(ProductoAcademicoDTO producto)
        {
            try
            {
                var productoAcademicoDB = EntityFactory.CrearProductoAcademico(producto);
                _context.ProductoAcademico.Add(productoAcademicoDB);
                int registrosAfectados = _context.SaveChanges();

                // Retornar 1 si se registra correctamente
                return registrosAfectados > 0 ? 1 : 0;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                Console.WriteLine($"Error de duplicidad: {ex.Message}");
                return -3; // Código de error para duplicidad de valores únicos
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error de SQL al agregar el producto académico: {ex.Message}");
                return -1; // Código de error general de SQL
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
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






    }
}
