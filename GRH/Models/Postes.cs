using Microsoft.Data.SqlClient;

namespace GRH.Models
{
    public class Postes
    {

        public int idPoste { get; set; }

        public String nomPoste { get; set; }

        public Services service { get; set; }

        public Postes()
        {

        }

        public Postes(int idPoste, string nomPoste, Services service)
        {
            this.idPoste = idPoste;
            this.nomPoste = nomPoste;
            this.service = service;
        }

        public Postes(int idPoste, string nomPoste)
        {
            this.idPoste = idPoste;
            this.nomPoste = nomPoste;
        }

        public Postes getPostebyAnnonce(SqlConnection co,int idAnnonce)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            Postes poste = new Postes();
            Services service = null;
            SqlCommand command = new SqlCommand("SELECT * FROM v_posteAnnonce WHERE idAnnonce = " + idAnnonce + "", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int idPoste= (int)reader["idposte"];
                String nomPoste = (string)reader["nomPoste"];
                poste= new Postes(idPoste,nomPoste);
            }
            reader.Close();
            return poste;
        }
    }
}
