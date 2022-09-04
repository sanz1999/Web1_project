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
        public IHttpActionResult Post([FromBody]User person)
        {
            Users users = HttpContext.Current.Application["Users"] as Users;

            person.Exist = true;
            person.CenterIdWorking = -1;
            person.Id = Users.ListOfUsers.Count;
            person.OwnedCenters = new List<int>();
            person.TrainingTrainer = new List<int>();
            person.TrainingVisitor = new List<int>();

            if (Users.FindByMail(person.EMail)) {
                return BadRequest("Prosledjeni e-mail je vec u upotrebi");
            }
            else if(Users.ExistByUsername(person.UserName)){
                return BadRequest("Korisnicko ime je zauzeto");
            }
            Users.Add(person);
            HttpContext.Current.Application["Users"] = users;
            return Ok();

        }
        [HttpPut]
        [Route("api/registration/update")]
        public IHttpActionResult Put([FromBody]User person) {
            Users users = HttpContext.Current.Application["Users"] as Users;
            User updated = Users.GetById(person.Id);
            foreach (User x in Users.ListOfUsers) {
                if (x.Id != person.Id) {
                    if (x.EMail == person.EMail) {
                        return BadRequest("Prosledjeni e-mail je vec u upotrebi");
                    }else if (x.UserName == person.UserName)
                    {
                        return BadRequest("Korisnicko ime je zauzeto");
                    }
                }
            }
            updated.UserName = person.UserName;
            updated.LastName = person.LastName;
            updated.FirstName = person.FirstName;
            updated.BirthDate = person.BirthDate;
            updated.Gender = person.Gender;
            updated.Password = person.Password;
            updated.EMail = person.EMail;
          
            Users.Update(updated);
            HttpContext.Current.Application["Users"] = users;
            return Ok();
        }
        
        
     
     

    }
}
