namespace GRH.Models
{
    public class ContratAvantage
    {
        int idContratAvantage { get; set; }
        Avantage idAvantage { get; set; }
        ContratEssai contrat { get; set; }

        public ContratAvantage() { }

        public ContratAvantage(int idContratAvantage, Avantage idAvantage, ContratEssai contrat) {
            this.idContratAvantage = idContratAvantage;
            this.idAvantage = idAvantage;
            this.contrat = contrat;
        }
    }
}
