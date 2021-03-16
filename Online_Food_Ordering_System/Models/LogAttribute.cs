using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Online_Food_Ordering_System.Models
{
    public class LogAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);

        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        private void Log(string methodName, RouteData routedata)
        {
            var controlname = routedata.Values["controller"].ToString();
            var actionName = routedata.Values["action"].ToString();
            var mess = "Method " + actionName + " Action name " + actionName + "Cotroller " + controlname + "  " + DateTime.Now;
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/Log/log.txt"), mess + Environment.NewLine);

        }
    }
}