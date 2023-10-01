using System.Data;
using Microsoft.Data.SqlClient;

namespace GRH.Models
{
    public class Annonce
    {
        public int idAnnonce {  get; set; }
        public Postes postes { get; set; }
        public String descriptions { get; set; }
        public int volumeTaches { get; set; }
        public int volumeJourHomme { get; set; }

        int nombreRecrue;
        public DateTime dateAnnonce { get; set; }
        public int etat { get; set; }

        public void setNombreRecrue ()
        {
            int nombreRecrue = this.volumeTaches/this.nombreRecrue;
            this.nombreRecrue = nombreRecrue;
        }

        public int getNombreRecrue ()
        {
            return this.nombreRecrue;
        }

        public Annonce() { }

        public Annonce (int idAnnonce, Postes postes, string descriptions, int volumeTaches, int volumeJourHomme, DateTime dateAnnonce, int etat)
        {
            this.idAnnonce = idAnnonce;
            this.postes = postes;
            this.descriptions = descriptions;
            this.volumeTaches = volumeTaches;
            this.volumeJourHomme = volumeJourHomme;
            this.dateAnnonce = dateAnnonce;
            this.etat = etat;
        }

        
        public void Insert(SqlConnection connection)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection = Connect.connectDB();
                }

                string query = "INSERT INTO Annonce (idPoste, descriptions, volumeTache, volumeJourHomme, dateAnnonce, etat) " +
                               "VALUES ( @idPoste, @descriptions, @volumeTaches, @volumeJourHomme, @dateAnnonce, @etat)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                 
                    command.Parameters.AddWithValue("@idPoste", postes.idPoste); // Remplacez par la valeur appropriée
                    command.Parameters.AddWithValue("@descriptions", descriptions);
                    command.Parameters.AddWithValue("@volumeTaches", volumeTaches);
                    command.Parameters.AddWithValue("@volumeJourHomme", volumeJourHomme);
                    command.Parameters.AddWithValue("@dateAnnonce", dateAnnonce);
                    command.Parameters.AddWithValue("@etat", etat);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Insertion réussie !");
                    }
                    else
                    {
                        Console.WriteLine("Aucune ligne n'a été insérée.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'insertion : " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public static Annonce getLastAnnonce(SqlConnection connection)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection = Connect.connectDB();
                }

                string query = "SELECT TOP 1 * FROM Annonce ORDER BY dateAnnonce DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Créez et retournez l'objet Annonce à partir des données de la base de données
                        Annonce lastAnnonce = new Annonce
                        {
                            idAnnonce = (int)reader["idAnnonce"],
                            postes = new Postes { idPoste = (int)reader["idPoste"] }, // Vous devez ajuster la création du poste
                            descriptions = (string)reader["descriptions"],
                            volumeTaches = (int)reader["volumeTache"],
                            volumeJourHomme = (int)reader["volumeJourHomme"],
                            dateAnnonce = (DateTime)reader["dateAnnonce"],
                            etat = (int)reader["etat"]
                        };

                        return lastAnnonce;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la récupération de la dernière annonce : " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return null; // Retournez null si aucune annonce n'a été trouvée
        }

    }
}
