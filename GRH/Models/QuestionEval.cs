namespace GRH.Models
{
    public class QuestionEval
    {
        int idQuestionEval { get; set; }
        Evaluation idEval { get; set; }
        String question { get; set; }
        Services idService { get; set; }

        public QuestionEval() { }

        public QuestionEval(int idQuestionEval, Evaluation idEval, String question, Services idService)
        {
            this.idQuestionEval = idQuestionEval;
            this.idEval = idEval;
            this.question = question;
            this.idService = idService;

        }
        
    }
}
