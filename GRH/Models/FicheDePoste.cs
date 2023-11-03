namespace GRH.Models
{
    public class FicheDePoste
    {
        int idFicheDePoste { get; set; }
        Client idClient { get; set; }
        Postes idPoste { get; set; }
        Mpiasa idMpiasa { get; set; }
        Annonce idAnnonce { get; set; }
        Contrat idContrat { get; set; }
        Cv idCv { get; set; }
        Mpiasa idMpiasaSuperieur { get; set; }

        public FicheDePoste() { }

        public FicheDePoste(int idFicheDePoste, Client idClient, Postes idPoste, Mpiasa idMpiasa, Annonce idAnnonce, Contrat idContrat, Cv idCv, Mpiasa idMpiasaSuperieur) {
            this.idFicheDePoste = idFicheDePoste;
            this.idPoste = idPoste;
            this.idMpiasa = idMpiasa;
            this.idAnnonce = idAnnonce;
            this.idContrat = idContrat;
            this.idCv = idCv;
            this.idMpiasaSuperieur = idMpiasaSuperieur;
        
        }
    }
}
