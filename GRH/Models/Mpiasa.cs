namespace GRH.Models
{
    public class Mpiasa
    {
        int idMpiasa { get; set; }
        Client idClient { get; set; }

        public Mpiasa() { }
        public Mpiasa(int idMpiasa, Client idClient) {
            this.idMpiasa = idMpiasa;
            this.idClient = idClient;
        }

    }
}
