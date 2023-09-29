namespace GRH.Models
{
    public class Critere
    {
        public int idCritere { get; set; }
        public String nomCritere { get; set; }

        public Critere() { }

        public Critere(int idCritere, string nomCritere)
        {
            this.idCritere = idCritere;
            this.nomCritere = nomCritere;
        }
    }
}
