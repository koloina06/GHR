namespace GRH.Models
{
    public class Raison
    {
        int idRaison { get; set; }
        String nom { get; set; }
        DateTime jour { get; set; }
        public Raison() { }

        public Raison(int idRaison, String nom, DateTime jour)
        {
            this.idRaison = idRaison;
            this.nom = nom;
            this.jour = jour;
        }
    }
}
