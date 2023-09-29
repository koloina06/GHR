namespace GRH.Models
{
    public class RH
    {
        public int idRh { get; set; }

        public String email { get; set; }

        public String mdp { get; set; }

        public RH()
        {

        }

        public RH(int idRh, string email, string mdp)
        {
            this.idRh = idRh;
            this.email = email;
            this.mdp = mdp;
        }
    }
}
