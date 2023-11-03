namespace GRH.Models
{
    public class Avantage
    {
        int idAvantage { get; set; }
        String nom { get; set; }

        public Avantage() { }

        public Avantage(int idAvantage, String nom){
            this.idAvantage = idAvantage;
            this.nom = nom;

         }
    }
}
