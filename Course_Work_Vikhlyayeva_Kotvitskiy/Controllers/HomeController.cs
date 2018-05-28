using Course_Work_Vikhlyayeva_Kotvitskiy.Models;
using k_means2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Web.UI;

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

            //solve

            //Result r = new Result
            //{
            //    Str = "Hello"
            //};

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(string Count, string method, string create)
        {
            int c;
            int[] clustering = { };

            Clusterizing cluster = new Clusterizing();


            c = int.Parse(Count);
            cluster.InitializeTuples(c, create);
            if (method == "k-means")
                clustering = cluster.Cluster();
            else if (method == "Forel")
                clustering = cluster.Cluster2();
            ViewBag.Mass = clustering;

            return View();
        }
    }
}