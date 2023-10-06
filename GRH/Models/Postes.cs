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
        public Postes(int idPoste, string nomPoste)
        {
            this.idPoste = idPoste;
            this.nomPoste = nomPoste;
        }
        public Postes(int idPoste, string nomPoste, Services service)
        {
            this.idPoste = idPoste;
            this.nomPoste = nomPoste;
            this.service = service;
        }


        //public static Postes getPostebyAnnonce(SqlConnection co, int idAnnonce)
        //{
        //    if (co == null)
        //    {
        //        Connect new_co = new Connect();
        //        co = new_co.connectDB();
        //    }
        //    Postes poste = new Postes();
        //    Services service = null;

        //    String sql = "SELECT * FROM v_posteAnnonce WHERE idAnnonce = " + idAnnonce;
        //    Console.WriteLine(sql);
        //    SqlCommand command = new SqlCommand(sql, co);
        //    SqlDataReader reader = command.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        int idPoste = (int)reader["idPoste"];
        //        String nomPoste = (string)reader["nomPoste"];
        //        poste = new Postes(idPoste, nomPoste);
        //    }
        //    reader.Close();
        //    return poste;
        //}


        public static Postes getPostebyAnnonce(SqlConnection co, int idAnnonce)
        {
            Postes poste = null;

            try
            {
                if (co == null)
                {
                    Connect new_co = new Connect();
                    co = new_co.connectDB();
                }

                String sql = "SELECT * FROM v_posteAnnonce WHERE idAnnonce = @idAnnonce";
                SqlCommand command = new SqlCommand(sql, co);
                command.Parameters.AddWithValue("@idAnnonce", idAnnonce);

                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    Services services = new Services();

                    int idPoste = (int)reader["idPoste"];
                    String nomPoste = (string)reader["nomPoste"];
                    services = services.getServiceByPoste(null, idPoste);


                    poste = new Postes(idPoste, nomPoste);
                    poste.service = services;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while getting Poste by Annonce: " + ex.Message);
            }

            return poste;
        }

    }
}
