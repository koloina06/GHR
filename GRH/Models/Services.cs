using Microsoft.Data.SqlClient;

namespace GRH.Models
{
    public class Services
    {
        public int idService { get; set; }
        public String nomService { get; set; }
        public String email { get; set; }
        public String mdp { get; set; }

        public Services()
        {

        }

        public Services(int idService, string nomService, string email, string mdp)
        {
            this.idService = idService;
            this.nomService = nomService;
            this.email = email;
            this.mdp = mdp;
        }

        public Services getServiceById(SqlConnection co, int idService)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            Services service = new Services();
            SqlCommand command = new SqlCommand("SELECT * FROM services WHERE idService = " + idService + "", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = (int)reader["idService"];
                string nomService = (string)reader["nomService"];
                string email = (string)reader["email"];
                string mdp = (string)reader["mdp"];
                service = new Services(id, nomService, email, mdp);
            }
            reader.Close();
            return service;
        }

        public Services getServiceByPoste(SqlConnection co, int idPoste)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            Services service = null;
            SqlCommand command = new SqlCommand("SELECT * FROM v_servicePoste WHERE idPoste = " + idPoste + "", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int idService = (int)reader["idService"];
                string nomService = (string)reader["nomService"];
                string email = (string)reader["email"];
                string mdp = (string)reader["mdp"];
                service = new Services(idService, nomService, email, mdp);
            }
            reader.Close();
            return service;
        }
    }
}
