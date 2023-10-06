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

        public List<Annonce> GetAllAnnonces(SqlConnection co)
        {
            if (co == null)
            {
                Connect con = new Connect();
                co = con.connectDB();
            }
            List<Annonce> annonces = new List<Annonce>();
            
            try
            {
             
                string query = "SELECT * FROM Annonce";
                    using (SqlCommand command = new SqlCommand(query, co))
                    {
                        
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            
                            Annonce annonce = new Annonce
                            {
                              idAnnonce = (int)reader["idAnnonce"],
                              descriptions = (string)reader["descriptions"],
                              volumeJourHomme = (int)reader["volumeJourHomme"],
                              volumeTaches = (int)reader["volumeTache"],
                              postes = Postes.getPostebyAnnonce(null, (int)reader["idAnnonce"])                                
                            };

                        annonce.postes = annonce.postes ?? new Postes();
                        annonces.Add(annonce);
                        }
                    reader.Close();
                    }
                }
            
            catch (Exception ex)
            {
               
                Console.WriteLine("Une erreur s'est produite lors de la récupération des annonces : " + ex.Message);
            }
            return annonces;
        }

        //public Annonce getAnnonceById(SqlConnection co, int idAnnonce)
        //{
        //    if (co == null)
        //    {
        //        Connect new_co = new Connect();
        //        co = new_co.connectDB();
        //    }
        //    Annonce annonce = null;
        //    SqlCommand command = new SqlCommand("SELECT * FROM annonce WHERE IdAnnonce = " + idAnnonce + "", co);
        //    SqlDataReader reader = command.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        int id = (int)reader["idAnnonce"];
        //        string descriptions = (string)reader["descriptions"];
        //        int vt = (int)reader["volumeTache"];
        //        int vh = (int)reader["volumeJourHomme"];
        //        DateTime date = (DateTime)reader["dateAnnonce"];
        //        int etat = (int)reader["etat"];
        //        annonce = new Annonce(id, descriptions, vt, vh, date, etat);
        //    }
        //    reader.Close();
        //    return annonce;
        //}

        public Postes getPoste(SqlConnection co, int idAnnonce)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            Services service = new Services();
            Postes poste = new Postes();
            poste = Postes.getPostebyAnnonce(co, idAnnonce);
            poste.service = service.getServiceByPoste(co, poste.idPoste);
            return poste;
        }
    }
}
