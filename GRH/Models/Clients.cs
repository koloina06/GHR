using Microsoft.Data.SqlClient;

namespace GRH.Models
{
    public class Clients
    {
        public int idClient {  get; set; }

        public String nom { get; set; }

        public String prenoms { get; set; }

        public DateTime dtn { get; set; }

        public String email { get; set; }  

        public String mdp { get; set; }

        public String adresse { get; set; }
        
        public String sexe;

        public int note { get; set; }
        public void setSexe(int sexe)
        {
            if(sexe == 0)
            {
                this.sexe = "Homme";
            }
            if(sexe == 1)
            {
                this.sexe = "Femme";
            }
        }

        public String getSexe()
        {
            return this.sexe;
        }

        public Clients() { }

        public Clients (int idClient, string nom, string prenoms, DateTime dtn, string email, string mdp, string adresse,int sexe)
        {
            this.idClient = idClient;
            this.nom = nom;
            this.prenoms = prenoms;
            this.dtn = dtn;
            this.email = email;
            this.mdp = mdp;
            this.adresse = adresse;
            this.setSexe(sexe);
        }
        public Clients getClientAnnonceByCv(SqlConnection co, int idAnnonce, int idCv)
        {
            if (co == null)
            {
                co = Connect.connectDB();
            }
            Clients client = null;
            SqlCommand command = new SqlCommand("SELECT * FROM v_clientAnnonce where idAnnonce="+idAnnonce+" and idCv="+idCv+"", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int idClient = (int)reader["idClient"];
                string nom = (string)reader["nom"];
                string prenom = (string)reader["prenom"];
                DateTime dtn = (DateTime)reader["dtn"];
                string email = (string)reader["email"];
                string mdp = (string)reader["mdp"];
                string adresse = (string)reader["addresse"];
                int sexe = (int)reader["sexe"];
                client= new Clients(idClient,nom, prenom, dtn,email, mdp,adresse,sexe);
            }
            reader.Close();
            return client;
        }

        public int getNoteSexe(SqlConnection co, int idAnnonce)
        {
            if (co == null)
            {
                co = Connect.connectDB();
            }
            int note = 0;
                SqlCommand command = new SqlCommand("select * from v_notesexe where nomSousCritere='"+this.getSexe()+"' and idAnnonce="+idAnnonce+"",co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                note = int.Parse(reader["coeff"].ToString());
            }
            reader.Close();
            return note;
        }

        public void passageTest(SqlConnection co, String dateTest,int idClient)
        {
            if (co == null)
            {
                co = Connect.connectDB();
            }
            String querry = "update etatClient set datetest='" + dateTest + "',etat=1 where idClient=" + idClient + "";
            Console.WriteLine(querry);
            //SqlCommand command = new SqlCommand(querry, co);
            //command.ExecuteNonQuery();
        }
    }
}
