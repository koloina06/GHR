namespace GRH.Models
{
    public class SousCritere
    {
        public int idSousCritere { get; set; }
        public Critere critere { get; set; }

        public String nomSousCritere { get; set; }
        public SousCritere() { }    

        public SousCritere(int idSousCritere, Critere critere, string nomSousCritere)
        {
            this.idSousCritere = idSousCritere;
            this.critere = critere;
            this.nomSousCritere = nomSousCritere;
        }
    }
}
