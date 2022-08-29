using PR060_2019_Web_projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PR060_2019_Web_projekat
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            

            Users listOfUsers = new Users("~/App_Data/Users.json");
            HttpContext.Current.Application["Users"] = listOfUsers as Users;

            Comments listOfComments = new Comments("~/App_Data/Comments.json");
            HttpContext.Current.Application["Comments"] = listOfComments as Comments;

            GroupTrainings listOfGroupTrainings = new GroupTrainings("~/App_Data/Comments.json");
            HttpContext.Current.Application["GroupTrainings"] = listOfGroupTrainings as GroupTrainings;

            FitnessCenters listOfFitnessCenters = new FitnessCenters("~/App_Data/FitnessCenters.json");
            HttpContext.Current.Application["FitnessCenters"] = listOfFitnessCenters as FitnessCenters;

            User user = new User();
            HttpContext.Current.Application["user"] = user as User;


        }
    }
}
