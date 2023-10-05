using Azure.Core;
using GRH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;

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
            return View();
        }

        public IActionResult NoteCv()
        {
            SqlConnection co = null;
            Cv cv = new Cv();
            var list = cv.getCv(co, 1);
            var listeTriee = list.OrderByDescending(cv => cv.client.note).ToList();
            if (co != null)
            {
                co.Close();
            }
            return View(listeTriee);
        }

        public IActionResult PassageTest()
        {
            SqlConnection co = null;
            Cv cv = new Cv();
            Clients client = new Clients();
            Annonce annonce = new Annonce();
            String dateTest= Request.Form["dateTest"];
            int nbrCv = int.Parse(Request.Form["nbrTest"]);
            var list = cv.getCv(co, 1);
            var listeTriee = list.OrderByDescending(cv => cv.client.note).ToList();
            var listePassage = listeTriee.Take(nbrCv).ToList();
            annonce.cloturerAnnonce(co, listePassage[0].annonce.idAnnonce);
            foreach (var item in listeTriee)
            {
                cv.insertNoteCV(co, item.idCv, item.client.note);
            }
            foreach (var item in listePassage)
            {
                client.passageTest(co, dateTest, item.client.idClient);
            }
            var date = DateTime.Parse(dateTest);
            ViewBag.date = date;
            if (co != null)
            {
                co.Close();
            }
            return View(listePassage);
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
      

public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}