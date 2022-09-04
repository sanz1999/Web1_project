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
    public class LoginController : ApiController
    {
        public Login_data Get()
        {
            User user = HttpContext.Current.Session["user"] as User;
            if (user == null)
            {
                user = new User();
                user.Role = Enum_Role.Undefined;
                HttpContext.Current.Session["user"] = user as User;
                return new Login_data(Enum_Role.Undefined, "", true); ;
            }

            return new Login_data(user.Role, user.UserName, true);



        }
        [HttpPost]
        [Route("api/login/loggin")]
        public Login_data Post([FromBody]PersonModel person)
        {
            Users users = HttpContext.Current.Application["Users"] as Users;
            foreach (User user in Users.ListOfUsers) {
                if (person.Username == user.UserName && person.Password == user.Password) {
                    HttpContext.Current.Session["user"] = user;
                    return new Login_data(user.Role, user.UserName, true);
                }
            }
            return  new Login_data(Enum_Role.Undefined, "", false);


        }


        [HttpGet]
        [Route("api/login/logout")]
        public IHttpActionResult SignOut() {
            try
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session["user"] = null;
                

            }
            catch {
                return Content(HttpStatusCode.NotAcceptable, "Signout not working");
                
            }

            return Ok();
        }
    }
}
