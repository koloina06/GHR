using Microsoft.Data.SqlClient;

namespace GRH.Models
{
    public class Connect
    {
        public static SqlConnection connectDB()
        {

            var datasource = @".\sqlexpress";
            var database = "grh";

            string connString = @"Data Source=TOAVINA;Initial Catalog="
                        + database + ";Persist Security Info=True; Trusted_Connection=True; TrustServerCertificate=True";

            SqlConnection conn = new SqlConnection(connString);
            
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            return conn;
        }
    }
}
