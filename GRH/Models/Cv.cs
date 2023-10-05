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
                co = Connect.connectDB();
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

        public int getNoteCv(SqlConnection co, int idCv,int idAnnonce)
        {
            if (co == null)
            {
                co = Connect.connectDB();
            }
            
            int note = 0;
            
            SqlCommand command = new SqlCommand("select sum(coeff) somme FROM detailsCV dc join cv on dc.idCv=cv.idCv JOIN critereCoef cc on dc.idSousCritere=cc.idSousCritere where cv.idAnnonce="+idAnnonce+" and cc.idAnnonce="+idAnnonce+" and cv.idAnnonce="+idAnnonce+"and cv.idCv="+idCv+" group by cv.idCv",co);
            
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["somme"]+"--haha--");
                note = int.Parse(reader["somme"].ToString());
            }
            reader.Close();
            return note;
         }

        public String[] getPDF(SqlConnection co, int idCv)
        {
            if (co == null)
            {
                co = Connect.connectDB();
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
                co = Connect.connectDB();
            }
            List<Cv> list = new List<Cv>();
            Cv cv1= new Cv();
            List<int> idCv= cv1.listeCvByAnnonce(co,idAnnonce);
            Clients client = new Clients();
            SousCritere sousCritere = new SousCritere();
            List<SousCritere> details= new List<SousCritere>();
            for(int i=0; i<idCv.Count; i++)
            {
                Cv cv = new Cv();
                cv.idCv= idCv[i];
                client= client.getClientAnnonceByCv(co, idAnnonce, idCv[i]);
                client.note = cv.getNoteCv(co, idCv[i],idAnnonce)+client.getNoteSexe(co,idAnnonce);
                
                cv.client= client;
                details= sousCritere.getDetailsByCv(co, idCv[i]);
                cv.sousCritere= details;
                String[] pdf = cv.getPDF(co, idCv[i]);
                cv.pdfDiplome = pdf[0];
                cv.pdfAttestation = pdf[1];
                list.Add(cv);
            }
            return list;
        }
        
        //Enregistrer les notes dans la table note
        public static void SaveNoteCv(SqlConnection co,int idAnnonce)
        {
            if (co == null)
            {
                co = Connect.connectDB();
            }
            List<Cv> list = new List<Cv>();
            Cv cv1= new Cv();
            List<int> idCv= cv1.listeCvByAnnonce(co,idAnnonce);
            Clients client = new Clients();
            SousCritere sousCritere = new SousCritere();
            List<SousCritere> details= new List<SousCritere>();
            for(int i=0; i<idCv.Count; i++)
            {
                Cv cv = new Cv();
                cv.idCv= idCv[i];
                client= client.getClientAnnonceByCv(co, idAnnonce, idCv[i]);
                client.note = cv.getNoteCv(co, idCv[i],idAnnonce)+client.getNoteSexe(co,idAnnonce);
                
                
                
                cv.client= client;
                details= sousCritere.getDetailsByCv(co, idCv[i]);
                cv.sousCritere= details;
                String[] pdf = cv.getPDF(co, idCv[i]);
                cv.pdfDiplome = pdf[0];
                cv.pdfAttestation = pdf[1];
                list.Add(cv);
            }
            
        }
        
    }
}
