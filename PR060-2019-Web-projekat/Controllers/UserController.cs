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
            return new PersonModel() { Username = user.FirstName + " " + user.LastName, Password = "" };
        }
        [HttpPost]
        [Route("api/User/CompleteUser")]
        public User CompleteUser([FromBody]Login_data login) {
            User user = HttpContext.Current.Session["user"] as User;
            return user;
        }
        [HttpPost]
        [Route("api/User/VisitorsForTraining")]
        public List<User> VisitorsForTraining([FromBody] GroupTraining gt) {
            GroupTrainings trainings = HttpContext.Current.Application["GroupTrainings"] as GroupTrainings;
            Users users = HttpContext.Current.Application["Users"] as Users;
            GroupTraining gtx = GroupTrainings.GetById(gt.Id);
            List<User> selected_users = new List<User>();
            foreach (int x in gtx.Visitors) {
                selected_users.Add(Users.GetById(x));
            }
            return selected_users;
        }
        [HttpPost]
        [Route("api/User/GetTrainersForO")]
        public List<User> GetTrainersForO([FromBody] User user) {
            Users users = HttpContext.Current.Application["Users"] as Users;
            FitnessCenters centers = HttpContext.Current.Application["FitnessCenters"] as FitnessCenters;
            List<User> trainers = new List<User>();
            User owner = Users.GetByUsername(user.UserName);
            List<FitnessCenter> fcs = new List<FitnessCenter>();
            foreach (FitnessCenter x in centers.ListOfFitnessCenters) {
                if (x.OwnerId == owner.Id) {
                    fcs.Add(x);
                }
            }
            foreach (FitnessCenter x in fcs) {
                foreach (User y in Users.ListOfUsers) {
                    if (x.Id == y.CenterIdWorking) {
                        trainers.Add(y);
                    }
                }
            }
            return trainers; 
        }
        [HttpPost]
        [Route("api/User/BanTrainer")]
        public IHttpActionResult BanTrainer([FromBody] User user)
        {
            Users users = HttpContext.Current.Application["Users"] as Users;
            User trainer = Users.GetById(user.Id);
            int index = Users.ListOfUsers.IndexOf(trainer);
            trainer.Exist = false;
            Users.ListOfUsers.RemoveAt(index);
            Users.ListOfUsers.Insert(index, trainer);
            Users.Save(Users.ListOfUsers);
            return Ok();
        }
    }
}
