namespace GRH.Models
{
    public class Cv
    {
        public int idCv {  get; set; }
        public Client client { get; set; }
        public SousCritere sousCritere { get; set; }   

        public String pdfDiplome { get; set; }

        public String pdfAttestation { get; set; }

        public Cv(int idCv, Client client, SousCritere sousCritere, string pdfDiplome, string pdfAttestation)
        {
            this.idCv = idCv;
            this.client = client;
            this.sousCritere = sousCritere;
            this.pdfDiplome = pdfDiplome;
            this.pdfAttestation = pdfAttestation;
        }

        public Cv() { }
    }


}
