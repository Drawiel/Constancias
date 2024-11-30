using DataAccess;
using Logic.Clases;
using Logic.Factories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DAO {
    public class ConstanciaDAO {
        private readonly ConstanciasEntities _context;

        public ConstanciaDAO() { _context = new ConstanciasEntities(); }

        public int SolicitarConstancia(ConstanciaDTO constanciaDTO) {
            try {
                var solicitudConstancia = EntityFactory.CrearConstancia(constanciaDTO);
                _context.Constancia.Add(solicitudConstancia);
                int registrosAfectados = _context.SaveChanges();
                return registrosAfectados;
            } catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601) {
                Console.WriteLine($"Error de duplicidad: {ex.Message}");
                return -3; // Código de error para duplicidad de valores únicos
            } catch (SqlException ex) {
                Console.WriteLine($"Error de SQL al agregar la solicitud de constancia: {ex.Message}");
                return -1; // Código de error general de SQL
            } catch (Exception ex) {
                Console.WriteLine($"Error general: {ex.Message}");
                return -2; // Código de error para excepciones generales
            }
        }
    }
}