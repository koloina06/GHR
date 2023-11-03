namespace GRH.Models
{
    public class Evaluation
    {
        int idEvaluation { get; set; }
        DateTime dateEvaluation { get; set; }

        public Evaluation() { }

        public Evaluation(int idEvaluation, DateTime dateEvaluation) {
            this.idEvaluation = idEvaluation;
            this.dateEvaluation = dateEvaluation;
        }
    }
}
