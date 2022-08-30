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
    public class RegistrationController : ApiController
    {
        [HttpPost]
        [Route("api/registration")]
        public HttpResponseMessage Post([FromBody]User person)
        {
            Users users = HttpContext.Current.Application["Users"] as Users;

            person.Exist = true;
            person.CenterIdWorking = -1;
            person.Id = Users.ListOfUsers.Count;
            person.OwnedCenters = new List<int>();
            person.TrainingTrainer = new List<int>();
            person.TrainingVisitor = new List<int>();

            if (Users.FindByMail(person.EMail)) {
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable) { Content = new StringContent("Zauzet mail")};
            }
            else if(Users.ExistByUsername(person.UserName)){
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("Zauzeto ime") };
            }
            Users.ListOfUsers.Add(person);
            Users.Save(Users.ListOfUsers);
            HttpContext.Current.Application["Users"] = users;
            return new HttpResponseMessage(HttpStatusCode.Created); 

        }
        
        
     
     

    }
}
