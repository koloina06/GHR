namespace GRH.Models
{
    public class Client
    {
        public int idClient {  get; set; }

        public String nom { get; set; }

        public String prenoms { get; set; }

        public DateTime dtn { get; set; }

        public String email { get; set; }  

        public String mdp { get; set; }

        public String adresse { get; set; }
        public int genre { get; set; }

        public Client() { }

        public Client (int idClient, string nom, string prenoms, DateTime dtn, string email, string mdp, string adresse)
        {
            this.idClient = idClient;
            this.nom = nom;
            this.prenoms = prenoms;
            this.dtn = dtn;
            this.email = email;
            this.mdp = mdp;
            this.adresse = adresse;
        }
    }

    public void InsertClient(SqlConnection con)
    {
     
            if (con == null)
            {
                Connect co = new Connect();
                con = co.connectDB();
            }

            string query = "INSERT INTO Client (nom, prenoms, dtn, email, mdp,sexe, adresse) " +
                           "VALUES (@nom, @prenoms, @dtn, @email, @mdp, @adresse)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nom", this.nom);
                command.Parameters.AddWithValue("@prenoms", this.prenoms);
                command.Parameters.AddWithValue("@dtn", this.dtn);
                command.Parameters.AddWithValue("@email", this.email);
                command.Parameters.AddWithValue("@mdp", this.mdp);
                command.Parameters.AddWithValue("@adresse", this.adresse);
                command.Parameters.AddWithValue("@sexe", this.genre);


            command.ExecuteNonQuery();
            }
        con.Close();
        }
}
