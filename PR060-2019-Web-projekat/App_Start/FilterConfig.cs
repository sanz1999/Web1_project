using System.Web;
using System.Web.Mvc;

namespace PR060_2019_Web_projekat
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
