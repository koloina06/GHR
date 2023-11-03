namespace GRH.Models
{
    public class Contrat
    {
        int idContrat { get; set; }
        TypeContrat idType { get; set; }
        public Contrat() { }
        public Contrat(int idContrat, TypeContrat idType)
        {
            this.idContrat = idContrat;
            this.idType = idType;
        }
    }
}
