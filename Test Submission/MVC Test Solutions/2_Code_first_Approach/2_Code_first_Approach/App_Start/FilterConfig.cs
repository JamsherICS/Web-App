using System.Web;
using System.Web.Mvc;

namespace _2_Code_first_Approach
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
