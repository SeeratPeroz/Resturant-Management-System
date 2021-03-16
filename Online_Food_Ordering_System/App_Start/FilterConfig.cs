using Online_Food_Ordering_System.Models;
using System.Web;
using System.Web.Mvc;

namespace Online_Food_Ordering_System
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogAttribute());
        }
    }
}
