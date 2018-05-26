using System.Web;
using System.Web.Mvc;

namespace Course_Work_Vikhlyayeva_Kotvitskiy
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
