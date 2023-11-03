namespace GRH.Models
{
    public class Fiche
    {
        int idFiche { get; set; }
        Evaluation idEval { get; set; }
        String remarques { get; set; }
        Client idClient { get; set; }

        public Fiche() { }
        public Fiche(int idFiche, Evaluation idEval, String remarques, Client idClient) {
            this.idFiche = idFiche;
            this.idEval = idEval;
            this.remarques = remarques;
            this.idClient = idClient;
        }
    }
}
