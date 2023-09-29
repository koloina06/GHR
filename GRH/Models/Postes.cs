namespace GRH.Models
{
    public class Postes
    {

        public int idPoste { get; set; }

        public String nomPoste { get; set; }

        public Services service { get; set; }

        public Postes()
        {

        }

        public Postes(int idPoste, string nomPoste, Services service)
        {
            this.idPoste = idPoste;
            this.nomPoste = nomPoste;
            this.service = service;
        }
    }
}
