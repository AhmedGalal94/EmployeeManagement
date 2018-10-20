using EmployeeManagement.Web.Filters;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // handle All thorwn Exceptions with Logging 
            filters.Add(new ExceptionFilter());
        }
    }
}
