namespace GRH.Models
{
    public class ReponseEval
    {
        int idReponseEval { get; set; }
        Fiche idFiche { get; set; }
        Question idQuestion { get; set; }
        String reponse { get; set; }
        public ReponseEval() { }

        public ReponseEval(int idReponseEval, Fiche idFiche,Question idQuestion, String reponse)
        {
            this.idReponseEval = idReponseEval;
            this.idFiche = idFiche;
            this.idQuestion = idQuestion;
            this.reponse = reponse;

        }
    }
}
