using GRH.Models;
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

        public IActionResult NoteCv()
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
            @ViewBag.allCv = listeTriee;
            return View("~/Views/Home/NoteCv.cshtml");
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
        
        

      
    }
}