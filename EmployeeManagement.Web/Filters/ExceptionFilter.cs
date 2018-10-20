using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagement.Web.Filters
{
    public class ExceptionFilter : HandleErrorAttribute
    {
        private static readonly Logger Logger = LogManager.GetLogger("FileLogger");

        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                Exception ex = filterContext.Exception;
                filterContext.ExceptionHandled = true;
                Logger.Log(LogLevel.Error, filterContext.Exception, filterContext.Exception.Message, null);

                var action = filterContext.RouteData.Values["action"].ToString();
                var type = filterContext.Controller.GetType();
                var method = type.GetMethods().Where(x => x.Name == action).FirstOrDefault();
                if (method != null)
                {
                    var returnType = method.ReturnType;
                    if (returnType == typeof(JsonResult))
                    {
                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        filterContext.Result = new JsonResult()
                        {
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                            Data = "An error occurred while processing your request"

                        };
                    }
                    else
                    {
                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        filterContext.Result = new ViewResult()
                        {
                            ViewName = "Error"
                        };
                    }
                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = "Error"
                    };
                }
            }
        }
    }
}