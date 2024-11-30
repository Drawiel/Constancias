using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess {
    using System;
    using System.Data.SqlClient;

    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "data source=marla\\SQLEXPRESS;initial catalog=Constancias;persist security info=True;user id=SGCDBUSER;password=passwordSGC;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Intenta abrir la conexión
                    Console.WriteLine("Conexión exitosa!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error de conexión: " + ex.Message);
                }
            }
        }
    }

}
