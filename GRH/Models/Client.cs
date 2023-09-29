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
}
