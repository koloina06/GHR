namespace GRH.Models
{
    public class Question
    {
        public int idQuestion { get; set; }
        public String nom { get; set; }
        public int coef { get; set; }
        public Services services { get; set; }

        public Question() { }

        public Question (int idQuestion, string nom, int coef, Services services)
        {
            this.idQuestion = idQuestion;
            this.nom = nom;
            this.coef = coef;
            this.services = services;
        }
    }
}
