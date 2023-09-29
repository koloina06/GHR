namespace GRH.Models
{
    public class Reponse
    {
        public int idReponse { get; set; }
        public Question question { get; set; }
        public String nom { get; set; }
        public int etat { get; set; }

        public Reponse() { }
        public Reponse(int idReponse, Question question, string nom, int etat)
        {
            this.idReponse = idReponse;
            this.question = question;
            this.nom = nom;
            this.etat = etat;
        }
    }
}
