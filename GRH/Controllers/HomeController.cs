﻿using GRH.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Data.SqlClient;


namespace GRH.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            SqlConnection con = Connect.connectDB();
            List<SousCritere> allS = SousCritere.getByIdCritere(con,1);
            @ViewBag.allS = allS;
            @ViewBag.c = con;
            return View("~/Views/Home/Index.cshtml");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Choice()
        {
            SqlConnection co = Connect.connectDB();
            Cv cv = new Cv();

            int idAnnonce = int.Parse(Request.Query["idAnnonce"]);
            var list = cv.getCv(co, idAnnonce);
            
            var listeTriee = list.OrderByDescending(cv => cv.client.note).ToList();
            if (co != null)
            {
                co.Close();
            }
            
            Console.WriteLine(listeTriee);
            @ViewBag.idAnnonce = idAnnonce;
            return View("~/Views/Home/ChoiceDetails.cshtml");
        }
        public IActionResult NoteCv()
        {
            SqlConnection co = Connect.connectDB();
            Cv cv = new Cv();

            int idAnnonce = int.Parse(Request.Query["idAnnonce"]);
            
            
            
            var list = cv.getCv(co, idAnnonce);
            Console.WriteLine("eto");
            var listeTriee = list.OrderByDescending(cv => cv.client.note).ToList();
          

            Annonce an = Annonce.getAnnonceById(co,idAnnonce);

            
            Console.WriteLine(an.descriptions);
            @ViewBag.allCv = listeTriee;
            @ViewBag.ann = an;
            
            if (co != null)
            {
                co.Close();
            }
            return View("~/Views/Home/NoteCv.cshtml");
            
        }
        
        [HttpPost]
        public IActionResult PassageTest()
        {
            int idAnnonce = int.Parse(Request.Form["idAnnonce"]);
            SqlConnection co =  Connect.connectDB();
            Cv cv = new Cv();
            Clients client = new Clients();
            Annonce annonce = new Annonce();
            String dateTest= Request.Form["dateTest"];
            
            DateTime dateTimeValue;
          
            double nbrCvs = double.Parse(Request.Form["nbrTest"]);
            int nbrCv = (int)Math.Round(nbrCvs);
            var list = cv.getCv(co, idAnnonce);
            Console.WriteLine(1);
            var listeTriee = list.OrderByDescending(cv => cv.client.note).ToList();
            var listePassage = listeTriee.Take(nbrCv).ToList();
            Console.WriteLine(2);
            Console.WriteLine(listePassage.Count+" j j");
           
            annonce.cloturerAnnonce(co, idAnnonce);
            
            foreach (var item in listeTriee)
            {
               cv.insertNoteCV(co, item.idCv, item.client.note);
            }
            foreach (var item in listePassage)
            {
                if (DateTime.TryParse(dateTest, out dateTimeValue))
                {
                    client.passageTest(co, dateTimeValue, item.client.idClient,idAnnonce);
                }
                
            }
            
            Annonce a = Annonce.getAnnonceById(co,idAnnonce);
            
            var date = DateTime.Parse(dateTest);
            ViewBag.date = date;
            @ViewBag.idAnnonce= idAnnonce;
            @ViewBag.idService = a.postes.service.idService;
            
            Console.WriteLine(a.postes.service.nomService+"hahaha");
            if (co != null)
            {
                co.Close();
            }
            return View(listePassage);
        }
        
     

        public IActionResult NoteTest()
        {
            SqlConnection co = Connect.connectDB();
            Cv cv = new Cv();

            int idAnnonce = int.Parse(Request.Query["idAnnonce"]);
            
            
            var list = cv.getCvTest(co, idAnnonce);
            Console.WriteLine("eto");
            var listeTriee = list.OrderByDescending(cv => cv.client.note).ToList();
          

            Annonce an = Annonce.getAnnonceById(co,idAnnonce);

            
            Console.WriteLine(an.descriptions);
            @ViewBag.allCv = listeTriee;
            @ViewBag.ann = an;
            
            if (co != null)
            {
                co.Close();
            }
            return View("~/Views/Home/NoteTeste.cshtml");
        }
        
        public IActionResult NoteEntretien()
        {
            SqlConnection co = Connect.connectDB();
            Cv cv = new Cv();

            int idAnnonce = int.Parse(Request.Query["idAnnonce"]);
            
            
            var list = cv.getCvEntretien(co, idAnnonce);
            Console.WriteLine("eto");
            var listeTriee = list.OrderByDescending(cv => cv.client.note).ToList();
          

            Annonce an = Annonce.getAnnonceById(co,idAnnonce);

            
            Console.WriteLine(an.descriptions);
            @ViewBag.allCv = listeTriee;
            @ViewBag.ann = an;
            
            if (co != null)
            {
                co.Close();
            }
            return View("~/Views/Home/NoteEntretien.cshtml");
        }
        public IActionResult getAllAnnonce()
        {
            SqlConnection con = Connect.connectDB();   

            int act = int.Parse(Request.Query["act"]);
            
            List<Annonce> getAll = Annonce.getAllAnnonce(con);

            var res = getAll;
            
            Annonce an = new Annonce();

            if (act == 0)
            {
                res = getAll.Where(a => a.etat == 0).ToList();
            }else if (act == 1)
            {
                res = getAll.Where(a => a.etat == 1).ToList();
            }
            else
            {
                res = getAll;
            }
            @ViewBag.allA = res;

           
            
            return View("~/Views/Home/AllAnnonce.cshtml");
        }
        
        public IActionResult QCM()
        {
            SqlConnection co = null;
            int idService = int.Parse(Request.Form["idService"]);
            int idAnnonce = int.Parse(Request.Form["idAnnonce"]);
            Question question = new Question();
            var list= question.questionsByService(co, idService);
            ViewBag.idAnnonce = idAnnonce;
            ViewBag.idService = idService;
            if (co != null)
            {
                co.Close();
            }
            return View(list);
        }

        public IActionResult InsertionQCM() 
        {
            SqlConnection co = null;
            Question question = new Question();
            if (Request.Form.ContainsKey("idService"))
            {
                int idAnnonce = int.Parse(Request.Form["idAnnonce"]);
                int idService = int.Parse(Request.Form["idService"]);
                List<Question> list = question.questionsByService(co, idService);
                List<Question> randomList = question.getRandomQuestions(list, 5);
                foreach (var item in randomList)
                {
                    question.insertQuestionTest(co, item.idQuestion, idAnnonce);
                }
            }
            else
            {
                int idAnnonce = int.Parse(Request.Form["idAnnonce"]);

                String[] id = Request.Form["questions"];

                int[] idQuestion = new int[id.Length];
                for (int i = 0; i < id.Length; i++)
                {
                    idQuestion[i] = int.Parse(id[i]);
                }
                foreach (var item in idQuestion)
                {
                    question.insertQuestionTest(co, item, idAnnonce);
                }
            }
            if (co != null)
            {
                co.Close();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Test()
        {
            SqlConnection co = null;
            Question question = new Question();
            Reponse reponse = new Reponse();
            var questions = question.questionsByAnnonce(co, 1);
            var reponses = reponse.getReponses(co, questions);
            Console.WriteLine(reponses[0].Count);
            QuestionReponse qcm = new QuestionReponse(questions,reponses);
            if (co != null)
            {
                co.Close();
            }
            return View(qcm);
        }
        [HttpPost]
        public IActionResult PassageEntretien()
        {
            int idAnnonce = int.Parse(Request.Form["idAnnonce"]);
            SqlConnection co =  Connect.connectDB();
            Cv cv = new Cv();
           
          
            double nbrCvs = double.Parse(Request.Form["nbrTest"]); 
            int nbrCv = (int)Math.Round(nbrCvs);
            
            var list = cv.getCvTest(co, idAnnonce);
            
          
            var listeTriee = list.OrderByDescending(cv => cv.client.note).ToList();
            var listePassage = listeTriee.Take(nbrCv).ToList();
            
            Annonce a = Annonce.getAnnonceById(co,idAnnonce);
            
            @ViewBag.idAnnonce= idAnnonce;
            @ViewBag.nbr = nbrCv;
            @ViewBag.listePassage = listePassage;
            
            Console.WriteLine(a.postes.service.nomService+"hahaha");
            if (co != null)
            {
                co.Close();
            }
            return View(listePassage);
        }

        [HttpPost]
        public IActionResult saveEntretien()
        {
           
            SqlConnection co = Connect.connectDB();
            int idAnnonce = int.Parse(Request.Form["idAnnonce"]);
            int nbr = int.Parse(Request.Form["nbr"]);
            Console.WriteLine("1 etoo ann="+idAnnonce);
            Cv cv = new Cv();
            
            var list = cv.getCvTest(co, idAnnonce);
            Console.WriteLine(nbr+" atoo");
            
            var listeTriee = list.OrderByDescending(cv => cv.client.note).ToList();
            var listePassage = listeTriee.Take(nbr).ToList();
            
           

            DateTime dateTimeValue;
            Console.WriteLine(listePassage.Count+" atyyy");
            foreach (Cv c in listePassage)
            {
                String dateEntretien= Request.Form[c.idCv.ToString()];
                if (DateTime.TryParse(dateEntretien, out dateTimeValue))
                {
                    c.changeEtatToEntretien(idAnnonce,dateTimeValue,co);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public IActionResult saveNoteEntretien()
        {
           
            SqlConnection co = Connect.connectDB();
            int idCv = int.Parse(Request.Form["idCv"]);
            int note = int.Parse(Request.Form["note"]);
            
            Clients.updateNoteEntretien(idCv,note,co);
            
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult toAddContratEssai()
        {
            SqlConnection con = Connect.connectDB();
            int idClient = int.Parse(Request.Query["idClient"]);
            int idAnnonce = int.Parse(Request.Query["idAnnonce"]);


            int idRh = Int32.Parse(HttpContext.Session.GetString("sess"));

            List<Avantage> allAvantages = Avantage.getAll(con);

           // Clients c = Clients.getClientsById(idClient, con);
            //RH rh = RH.GetById(idRh,con);

            @ViewBag.idClient = idClient;
            @ViewBag.idAnnonce = idAnnonce;
            @ViewBag.allAvantage = allAvantages;
            
            return View("~/Views/Home/AddContratEssai.cshtml");
        }
        
        [HttpPost]
        public IActionResult AddContratEssai()
        {
            SqlConnection con = Connect.connectDB();
            int idClient = int.Parse(Request.Form["idClient"]);
            int idAnnonce = int.Parse(Request.Form["idAnnonce"]);
            
            int idRh = Int32.Parse(HttpContext.Session.GetString("sess"));

            Clients c = Clients.getClientsById(idClient, con);
            RH rh = RH.GetById(idRh,con);
            con = Connect.connectDB();
            Annonce an = Annonce.getAnnonceById(con,idAnnonce);
            
            con = Connect.connectDB();
            TypeContrat t = TypeContrat.getById(1,con);

            String addresse = Request.Form["addresse"];
                
            String debutS = Request.Form["debut"];
            DateTime debut;
            String finS = Request.Form["fin"];
            DateTime fin;
            

            double salaire = double.Parse(Request.Form["salaire"]);
            
            if (DateTime.TryParse(debutS, out debut) && DateTime.TryParse(finS, out fin)) 
            {
                
                Console.WriteLine(c.nom);
                Console.WriteLine(rh.email);
                Console.WriteLine(an.postes.nomPoste);
                
                
                Contrat cont = new Contrat(1,c,addresse,debut,fin,salaire,rh,an.postes,t);
                cont.save(con);
                
                List<Avantage> allAvantage = Avantage.getAll(con);

                Contrat contrat = Contrat.getLastContrat(con);
                
                foreach (Avantage av in allAvantage)
                {
                    int a = av.idAvantage;
                    if(int.TryParse(Request.Form[av.idAvantage.ToString()],out a))
                    {
                          ContratAvantage.insert(av.idAvantage,contrat.idContrat,con);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult toListeContrat()
        {
            SqlConnection con = Connect.connectDB();

            int idType = int.Parse(Request.Query["idType"]);
            
            List<Contrat> all = new List<Contrat>();
            all = Contrat.getAllByType(idType,con);

            @ViewBag.allContrat = all;

            return View("~/Views/Home/ListeContratEssai.cshtml");

        }

        public IActionResult toAnotherContrat()
        {
            SqlConnection con = Connect.connectDB();

            int idContrat = int.Parse(Request.Query["idContrat"]);

            Contrat contrat = Contrat.getById(idContrat,con);

            List<TypeContrat> allType = TypeContrat.getAll(con);
            List<Avantage> allAvantages = Avantage.getAll(con);
            
            @ViewBag.idClient = contrat.clients.idClient;
            @ViewBag.idPoste = contrat.poste.idPoste;
            @ViewBag.allAvantage = allAvantages;
            @ViewBag.allType = allType;
            
            return View("~/Views/Home/AddAnotherContrat.cshtml");
        }
        [HttpPost]
        public IActionResult AddAnotherContrat()
        {
            SqlConnection con = Connect.connectDB();
            int idClient = int.Parse(Request.Form["idClient"]);
            int idPoste = int.Parse(Request.Form["idPoste"]);
            
            int idRh = Int32.Parse(HttpContext.Session.GetString("sess"));
            
            int idType = int.Parse(Request.Form["idTypeContrat"]);

            Clients c = Clients.getClientsById(idClient, con);
            RH rh = RH.GetById(idRh,con);
            con = Connect.connectDB();
            Postes p = Postes.getById(idPoste, con);
            
            con = Connect.connectDB();
            TypeContrat t = TypeContrat.getById(idType,con);

            String addresse = Request.Form["addresse"];
                
            String debutS = Request.Form["debut"];
            DateTime debut;
            String finS = Request.Form["fin"];
            DateTime fin;

            double salaire = double.Parse(Request.Form["salaire"]);
            
            Mpiasa mp = new Mpiasa(1,c,DateTime.Now,DateTime.Now,p);
            mp.saveMpiasa(con);
            
            if (DateTime.TryParse(debutS, out debut) ) 
            {
                if (t.idTypeContrat == 3)
                {
                    DateTime temp = new DateTime();
                    Contrat cont = new Contrat(1,c,addresse,debut,temp,salaire,rh,p,t); 
                    cont.save(con);
                }else
                {
                    if (DateTime.TryParse(finS, out fin))
                    {
                        Contrat cont = new Contrat(1,c,addresse,debut,fin,salaire,rh,p,t); 
                        cont.save(con);
                    }
                }
                
                List<Avantage> allAvantage = Avantage.getAll(con);
                Contrat contrat = Contrat.getLastContrat(con);
                
                foreach (Avantage av in allAvantage)
                {
                    int a = av.idAvantage;
                    if(int.TryParse(Request.Form[av.idAvantage.ToString()],out a))
                    {
                        ContratAvantage.insert(av.idAvantage,contrat.idContrat,con);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult toListMpiasa()
        {
            SqlConnection con = Connect.connectDB();
            List<Mpiasa> allMpiasa = Mpiasa.getAll(con);
            @ViewBag.allMpiasa = allMpiasa;

            return View("~/Views/Home/ListeMpiasa.cshtml");
        }

        public IActionResult getAllDemandeConge()
        {
            SqlConnection con = Connect.connectDB();

            List<DemandeConge> allDm = DemandeConge.getAllEnCours(con);


            @ViewBag.allDm = allDm;

            return View("~/Views/Home/ListeDmConge.cshtml");
        }
      
    }
}