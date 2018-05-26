using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using k_means2;


namespace Course_Work_Vikhlyayeva_Kotvitskiy.Controllers
{
    public class SolverController : Controller
    {
        // GET: Solver
        public ActionResult Input()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Input(string Count, string method, string create)
        {
            int c;
            int[] clustering;
            Clusterizing cluster = new Clusterizing();

            c = int.Parse(Count);
            cluster.InitializeTuples(c, create);
            if (method == "k-means")
                clustering = cluster.Cluster();
            else if (method == "Forel")
                clustering = cluster.Cluster2();
            return View();
        }
    }
}