using Course_Work_Vikhlyayeva_Kotvitskiy.Models;
using k_means2;
using System;
using System.Web.Mvc;

namespace Course_Work_Vikhlyayeva_Kotvitskiy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Solve(FormCollection fc)
        {
            string CountStr = fc.GetValue("inputsm").AttemptedValue; 
            string algorithm = fc.GetValue("distribution").AttemptedValue; 

            int count = Convert.ToInt32(CountStr is null ? "0" : CountStr);


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string Vertex, string method, string create)
        {
            int c;
            int[] clustering = { };

            Clusterizing cluster = new Clusterizing();

            if (Vertex.Length != 0)
            {
                c = int.Parse(Vertex);
                cluster.InitializeTuples(c, create);
                if (method == "k-means")
                    clustering = cluster.Cluster();
                else if (method == "Forel")
                    clustering = cluster.Cluster2();
                ViewBag.Mass = clustering;
            }
            return View("Contact");
        }
    }
}