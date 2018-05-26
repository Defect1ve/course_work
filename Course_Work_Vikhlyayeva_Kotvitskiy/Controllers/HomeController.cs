using Course_Work_Vikhlyayeva_Kotvitskiy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            //solve
            int[] coordinates = null; ;
            int[] clasters = null; ;

            Result r = new Result(coordinates, clasters);

            return View("Result", r);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}