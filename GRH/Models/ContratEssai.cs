namespace GRH.Models
{
    public class ContratEssai
    {
        int idContratEssat { get; set; }
        int idClient { get; set; }
        String adresse { get; set; }
        DateTime debut { get; set; }
        DateTime fin { get; set; }
        double salaire { get; set; }
        int idRh { get; set; }

        public ContratEssai() { }

        public ContratEssai(int idContratEssai,int idClient, String adresse, DateTime debut, DateTime fin, double salaire, int idRh)
        {
            this.idContratEssat = idContratEssat;
            this.idClient = idClient;
            this.adresse = adresse;
            this.debut = debut;
            this.fin = fin;
            this.salaire = salaire;
            this.idRh = idRh;

        }
    }
}
