namespace GRH.Models
{
    public class CritereCoef
    {
        public int idCritereCoef { get; set; }
        public SousCritere sousCritere { get; set; }
        public int coef { get; set; }
        public Annonce annonce { get; set; }

        public CritereCoef() { }

        public CritereCoef (int idCritereCoef, SousCritere sousCritere, int coef, Annonce annonce)
        {
            this.idCritereCoef = idCritereCoef;
            this.sousCritere = sousCritere;
            this.coef = coef;
            this.annonce = annonce;
        }
    }
}
