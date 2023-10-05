using Microsoft.Data.SqlClient;

namespace GRH.Models
{
    public class Cv
    {
        public int idCv {  get; set; }
        public Clients client { get; set; }
        public List<SousCritere> sousCritere { get; set; }   

        public String pdfDiplome { get; set; }

        public String pdfAttestation { get; set; }

        public Annonce annonce { get; set; }

        public Cv(int idCv, Clients client, List<SousCritere> sousCritere, string pdfDiplome, string pdfAttestation, Annonce annonce)
        {
            this.idCv = idCv;
            this.client = client;
            this.sousCritere = sousCritere;
            this.pdfDiplome = pdfDiplome;
            this.pdfAttestation = pdfAttestation;
            this.annonce = annonce;
        }

        public Cv(int idCv, Clients client, List<SousCritere> sousCritere, string pdfDiplome, string pdfAttestation)
        {
            this.idCv = idCv;
            this.client = client;
            this.sousCritere = sousCritere;
            this.pdfDiplome = pdfDiplome;
            this.pdfAttestation = pdfAttestation;
        }

        public Cv() { }

        public List<int> listeCvByAnnonce(SqlConnection co,int idAnnonce)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            List<int> list = new List<int>();
            SqlCommand command = new SqlCommand("SELECT idCv FROM cv where idAnnonce=" + idAnnonce + " ", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add((int)reader["idCv"]);
            }
            reader.Close();
            return list;
        }

        public int getNoteCv(SqlConnection co, int idCv, int idAnnonce)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            int note = 0;
            String querry = "select sum(coef) somme from detailscv dc join cv on dc.idcv=cv.idcv join criterecoef cc on dc.idSousCritere=cc.idsouscritere where cv.idannonce=" + idAnnonce + " and cc.idannonce=" + idAnnonce + " and cv.idcv=" + idCv + " group by cv.idcv";
            SqlCommand command = new SqlCommand(querry, co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                note = (int)reader["somme"];
            }
            reader.Close();
            return note;
         }

        public String[] getPDF(SqlConnection co, int idCv)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            String[] pdf= new string[2];
            SqlCommand command = new SqlCommand("select pdfDiplome,pdfAttestation from cv where idCv=" + idCv + "", co);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                pdf[0] = (string)reader["pdfDiplome"];
                pdf[1] = (string)reader["pdfAttestation"];
            }
            reader.Close();
            return pdf;
        }

        public List<Cv> getCv(SqlConnection co,int idAnnonce)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            List<Cv> list = new List<Cv>();
            Cv cv1= new Cv();
            List<int> idCv= cv1.listeCvByAnnonce(co,idAnnonce);
            Clients client = new Clients();
            SousCritere sousCritere = new SousCritere();
            List<SousCritere> details= new List<SousCritere>();
            Annonce annonce= new Annonce();
            for(int i=0; i<idCv.Count; i++)
            {
                Cv cv = new Cv();
                cv.idCv= idCv[i];
                client= client.getClientAnnonceByCv(co, idAnnonce, idCv[i]);
                client.note= cv.getNoteCv(co, idCv[i], idAnnonce)+client.getNoteSexe(co,idAnnonce);
                cv.client= client;
                details= sousCritere.getDetailsByCv(co, idCv[i]);
                cv.sousCritere= details;
                String[] pdf = cv.getPDF(co, idCv[i]);
                cv.pdfDiplome = pdf[0];
                cv.pdfAttestation = pdf[1];
                cv.annonce = annonce.getAnnonce(co, idAnnonce);
                list.Add(cv);
            }
            return list;
        }

        public void insertNoteCV(SqlConnection co,int idCv, int note)
        {
            if (co == null)
            {
                Connect new_co = new Connect();
                co = new_co.connectDB();
            }
            String querry = "insert into note values(" + idCv + "," + note + ",0,0)";
            Console.WriteLine(querry);
            SqlCommand command = new SqlCommand(querry, co);
            command.ExecuteNonQuery();
        }
    }
}