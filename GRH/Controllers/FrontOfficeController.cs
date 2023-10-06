using GRH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace GRH.Controllers
{
    public class FrontOfficeController : Controller
    {
        public IActionResult Index()
        {
            SqlConnection co = new Connect().connectDB();
            Annonce annonces = new Annonce();
            var listAnnonce = annonces.GetAllAnnonces(co);

            @ViewBag.all = listAnnonce;
            Console.WriteLine("12");
            if(co != null)
            {
                co.Close();
            }
            Console.WriteLine(listAnnonce.Count);
            return View(listAnnonce);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Inscription()
        {
            return View();
        }

        public IActionResult Cv()
        {
            SqlConnection co = new Connect().connectDB();
            Critere critere = new Critere();
            SousCritere sousCritere = new SousCritere();

            if (co != null)
            {
                co.Close();
            }
            var listCritere = critere.GetAllCritere(null);
          //var listSousCritere = sousCritere.GetAllSousCriteres(co);



            @ViewBag.listCritere = listCritere;

            Console.WriteLine(listCritere);
         //   @ViewBag.listSousCritere = listSousCritere;
            return View();
        }
    }
}
