using Microsoft.Data.SqlClient;

namespace GRH.Models
{
    public class Critere
    {
        public int idCritere { get; set; }
        public String nomCritere { get; set; }

        public Critere() { }

        public Critere(int idCritere, string nomCritere)
        {
            this.idCritere = idCritere;
            this.nomCritere = nomCritere;
        }

        public List<Critere> GetAllCritere(SqlConnection co)
        {
            if (co == null)
            {
                Connect con = new Connect();
                co = con.connectDB();
            }
            List<Critere> criteres = new List<Critere>();

            try
            {

                string query = "SELECT * FROM Critere";
                using (SqlCommand command = new SqlCommand(query, co))
                {

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        Critere critere = new Critere
                        {
                            idCritere = (int)reader["idCritere"],
                            nomCritere = (string)reader["nomCritere"],
                            
                        };

                       
                        criteres.Add(critere);
                    }
                    reader.Close();
                }
            }

            catch (Exception ex)
            {

                Console.WriteLine("Une erreur s'est produite lors de la récupération des Criteres : " + ex.Message);
            }
            return criteres;
        }

        public  Critere getCritereById(SqlConnection co, int idCritere)
        {
            Critere critere = null;

            try
            {
                if (co == null)
                {
                    Connect new_co = new Connect();
                    co = new_co.connectDB();
                }

                String sql = "SELECT * FROM critere WHERE idCritere = @idCritere";
                SqlCommand command = new SqlCommand(sql, co);
                command.Parameters.AddWithValue("@idCritere", idCritere);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    

                    int idcriteres = (int)reader["idPoste"];
                    String nomCritere = (string)reader["nomCritere"];
                   


                    critere = new Critere(idcriteres, nomCritere);
                  
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while getting Poste by Annonce: " + ex.Message);
            }

            return critere;
        }
    }
}
