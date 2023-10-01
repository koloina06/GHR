using Microsoft.Data.SqlClient;

namespace GRH.Models
{
    public class SousCritere
    {
        public int idSousCritere { get; set; }
        public Critere critere { get; set; }

        public String nomSousCritere { get; set; }
        public SousCritere() { }    

        public SousCritere(int idSousCritere, Critere critere, string nomSousCritere)
        {
            this.idSousCritere = idSousCritere;
            this.critere = critere;
            this.nomSousCritere = nomSousCritere;
        }

        public List<SousCritere> getDetailsByCv(SqlConnection co, int idCv)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            Critere critere = null;
            SousCritere sousCritere = null;
            List<SousCritere> list= new List<SousCritere>();
            SqlCommand command = new SqlCommand("SELECT * FROM v_detailcv where idCv=" + idCv + "", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int idCritere = (int)reader["idCritere"];
                string nomCritere = (string)reader["nomCritere"];
                critere= new Critere(idCritere, nomCritere);
                int idSousCritere = (int)reader["idSousCritere"];
                string nomSousCritere = (string)reader["nomSousCritere"];
                sousCritere = new SousCritere(idSousCritere, critere, nomSousCritere);
                list.Add(sousCritere);          
            }
            reader.Close();
            return list;
        }
    }
}
