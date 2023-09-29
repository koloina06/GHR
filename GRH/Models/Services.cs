namespace GRH.Models
{
    public class Services
    {
        public int idService { get; set; }
        public String nomService { get; set; }
        public String email { get; set; }
        public String mdp { get; set; }

        public Services()
        {

        }

        public Services(int idService, string nomService, string email, string mdp)
        {
            this.idService = idService;
            this.nomService = nomService;
            this.email = email;
            this.mdp = mdp;
        }
    }
}
