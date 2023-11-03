namespace GRH.Models
{
    public class DemandeConge
    {
       int idDemandeConge { get; set; }
        Mpiasa idMpiasa { get; set; }
        DateTime debut { get; set; }
        DateTime fin { get; set; }
        Raison idRaison { get; set; }

        public DemandeConge() { }

        public DemandeConge(int idDemandeConge, Mpiasa idMpiasa, DateTime debut, DateTime fin, Raison idRaison) {
            this.idDemandeConge = idDemandeConge;
            this.idMpiasa = idMpiasa;
            this.debut = debut;
            this.fin = fin;
            this.idRaison = idRaison;
        
        }
    }
}
