using Microsoft.Data.SqlClient;

namespace GRH.Models
{
    public class Connect
    {
        public SqlConnection connectDB()
        {

            var datasource = @".\sqlexpress";
            var database = "GRH";

            string connString = @"Data Source=" + datasource + ";Initial Catalog="
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
