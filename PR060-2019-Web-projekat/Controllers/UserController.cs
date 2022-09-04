using Newtonsoft.Json;
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
        public PersonModel GetOwnerName(int id) {
            Users users = HttpContext.Current.Application["Users"] as Users;
            User user = Users.GetById(id);
            return new PersonModel() { Username = user.FirstName + " " +user.LastName,Password = "" };
        }
        [HttpPost]
        [Route("api/User/CompleteUser")]
        public User CompleteUser([FromBody]Login_data login) {
            User user = HttpContext.Current.Session["user"] as User;
            return user;
        }
    }
}
