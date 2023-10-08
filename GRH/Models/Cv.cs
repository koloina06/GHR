namespace GRH.Models
{
    public class Cv
    {
        public int idCv {  get; set; }
        public Client client { get; set; }
        public SousCritere sousCritere { get; set; }   

        public String pdfDiplome { get; set; }

        public String pdfAttestation { get; set; }

        public Cv(int idCv, Client client, SousCritere sousCritere, string pdfDiplome, string pdfAttestation)
        {
            this.idCv = idCv;
            this.client = client;
            this.sousCritere = sousCritere;
            this.pdfDiplome = pdfDiplome;
            this.pdfAttestation = pdfAttestation;
        }

        public Cv() { }

        public static void InsertCv(SqlConnection co, int clientId, Cv newCv)
        {
            if (co == null)
            {
                Connect con = new Connect();
                co = con.connectDB();
            }

            try
            {
               
                SousCritere sousCritere = new SousCritere(); 
                

                string query = "INSERT INTO VotreTableCv (idClient, idSousCritere, pdfDiplome, pdfAttestation) " +
                               "VALUES (@idClient, @idSousCritere, @pdfDiplome, @pdfAttestation)";

                using (SqlCommand command = new SqlCommand(query, co))
                {
                    command.Parameters.AddWithValue("@idClient", clientId);
                    command.Parameters.AddWithValue("@idSousCritere", sousCritere.idSousCritere);
                    command.Parameters.AddWithValue("@pdfDiplome", newCv.pdfDiplome);
                    command.Parameters.AddWithValue("@pdfAttestation", newCv.pdfAttestation);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de l'ajout du CV : " + ex.Message);
            }
        }
    }


}
