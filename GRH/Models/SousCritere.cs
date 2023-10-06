using Microsoft.Data.SqlClient;

namespace GRH.Models
{
    public class SousCritere
    {
        public int idSousCritere { get; set; }
        public Critere critere { get; set; }

        public String nomSousCritere { get; set; }
        public SousCritere() { }    

        public SousCritere(int idSousCritere, Critere critere, string nomSousCritere)
        {
            this.idSousCritere = idSousCritere;
            this.critere = critere;
            this.nomSousCritere = nomSousCritere;
        }

        public List<SousCritere> GetAllSousCriteres(SqlConnection co)
        {
            if (co == null)
            {
                Connect con = new Connect();
                co = con.connectDB();
            }
            List<SousCritere> sousCriteres= new List<SousCritere>();

            try
            {

                string query = "SELECT * FROM sousCritere";
                using (SqlCommand command = new SqlCommand(query, co))
                {

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Critere critere = new Critere();
                        SousCritere sousCritere = new SousCritere
                        {
                            
                            idSousCritere = (int)reader["idAnnonce"],
                            critere = critere.getCritereById(null,(int)reader["idCritere"]),
                            nomSousCritere = (String)reader["nomSousCritere"],
                           
                        };

                       
                        sousCriteres.Add(sousCritere);
                    }
                    reader.Close();
                }
            }

            catch (Exception ex)
            {

                Console.WriteLine("Une erreur s'est produite lors de la récupération des annonces : " + ex.Message);
            }
            return sousCriteres;
        }
    }
}
