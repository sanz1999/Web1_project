using PR060_2019_Web_projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PR060_2019_Web_projekat.Controllers
{
    public class UserController : ApiController
    {

        public string Get() {
            User user = HttpContext.Current.Session["user"] as User;

            if (user == null) {
                user = new User();
                user.UserName = "proba";
                HttpContext.Current.Session["user"] = user;
            }

            return user.UserName;
        }
    }
}
