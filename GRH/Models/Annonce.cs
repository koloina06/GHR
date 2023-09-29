namespace GRH.Models
{
    public class Annonce
    {
        public int idAnnonce {  get; set; }
        public Postes postes { get; set; }
        public String descriptions { get; set; }
        public int volumeTaches { get; set; }
        public int volumeJourHomme { get; set; }

        int nombreRecrue;
        public DateTime dateAnnonce { get; set; }
        public int etat { get; set; }

        public void setNombreRecrue ()
        {
            int nombreRecrue = this.volumeTaches/this.nombreRecrue;
            this.nombreRecrue = nombreRecrue;
        }

        public int getNombreRecrue ()
        {
            return this.nombreRecrue;
        }

        public Annonce() { }

        public Annonce (int idAnnonce, Postes postes, string descriptions, int volumeTaches, int volumeJourHomme, DateTime dateAnnonce, int etat)
        {
            this.idAnnonce = idAnnonce;
            this.postes = postes;
            this.descriptions = descriptions;
            this.volumeTaches = volumeTaches;
            this.volumeJourHomme = volumeJourHomme;
            this.dateAnnonce = dateAnnonce;
            this.etat = etat;
        }
    }
}
