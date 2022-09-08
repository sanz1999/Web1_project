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
    public class GroupTrainingsController : ApiController
    {
        [HttpGet]
        public List<GroupTraining> GetUpcomingForFC(int id)
        {
            GroupTrainings trainings = HttpContext.Current.Application["GroupTrainings"] as GroupTrainings;
            List<GroupTraining> selected = new List<GroupTraining>();
            foreach (GroupTraining gt in GroupTrainings.ListOfGroupTrainings)
            {
                if ((gt.CenterId == id) && gt.Exist && (gt.Appointment > DateTime.Now)) { selected.Add(gt); }
            }
            return selected;
        }

        [HttpPost]
        [Route("api/GroupTraining/SingleTraining")]
        public GroupTraining GetSingleGroupTraining([FromBody]GroupTraining gt) {
            GroupTrainings trainings = HttpContext.Current.Application["GroupTrainings"] as GroupTrainings;
             GroupTraining x = GroupTrainings.GetById(gt.Id);
                return x;
            
        }
        [HttpPost]
        [Route("api/GroupTraining/AddVisitor")]
        public IHttpActionResult AddVisitor([FromBody]PersonModel data)
        {//Username is user username, pass is training id
            GroupTrainings trainings = HttpContext.Current.Application["GroupTrainings"] as GroupTrainings;
            int id_user = (Users.GetByUsername(data.Username)).Id;
            int id_gt = Convert.ToInt32(data.Password);
            foreach (GroupTraining gt in GroupTrainings.ListOfGroupTrainings)
            {
                if (gt.Id == id_gt)
                {
                    if (gt.Visitors.Count >= gt.Capacity)
                    {
                        return BadRequest("Neuspesno, nema slobodnih mesta");
                    }
                    else if (gt.Visitors.Contains(id_user))
                    {
                        return BadRequest("Neuspesno, vec ste se prijavili");
                    }
                    else
                    {
                        GroupTrainings.AddVisitor(id_user, id_gt);
                        break;
                    }
                }
            }
            HttpContext.Current.Application["GroupTrainings"] = trainings;
            return Ok();
        }

        [HttpPost]
        [Route("api/GroupTraining/GetForVisitor")]
        public Dictionary<Tuple<int,string>,GroupTraining> GetForVisitor([FromBody]PersonModel data)
        {
            Dictionary<Tuple<int, string>, GroupTraining> selected = new Dictionary<Tuple<int, string>, GroupTraining>();
            FitnessCenters centers = HttpContext.Current.Application["FitnessCenters"] as FitnessCenters;

            User user = Users.GetByUsername(data.Username);
            foreach (int x in user.TrainingVisitor) {
                GroupTraining gt = GroupTrainings.GetById(x);
                if (gt.Appointment <= DateTime.Now)
                {
                    FitnessCenter fc = FitnessCenters.GetById(centers.ListOfFitnessCenters,gt.CenterId);
                    selected.Add(new Tuple<int, string>(gt.Id,fc.Name),gt);
                }
            }
            return selected;
        }
        [HttpPost]
        [Route("api/GroupTraining/GetPastForTrainer")]
        public Dictionary<Tuple<int, string>, GroupTraining> GetPastForTrainer([FromBody]PersonModel data)
        {
            Dictionary<Tuple<int, string>, GroupTraining> selected = new Dictionary<Tuple<int, string>, GroupTraining>();
            FitnessCenters centers = HttpContext.Current.Application["FitnessCenters"] as FitnessCenters;

            User user = Users.GetByUsername(data.Username);
            foreach (int x in user.TrainingTrainer)
            {
                GroupTraining gt = GroupTrainings.GetById(x);
                if ((gt.Appointment <= DateTime.Now) && gt.Exist)
                {
                    FitnessCenter fc = FitnessCenters.GetById(centers.ListOfFitnessCenters, gt.CenterId);
                    selected.Add(new Tuple<int, string>(gt.Id, fc.Name), gt);
                }
            }
            return selected;
        }
        [HttpPost]
        [Route("api/GroupTraining/GetUpcomingForTrainer")]
        public Dictionary<Tuple<int, string>, GroupTraining> GetUpcomingForTrainer([FromBody]PersonModel data)
        {
            Dictionary<Tuple<int, string>, GroupTraining> selected = new Dictionary<Tuple<int, string>, GroupTraining>();
            FitnessCenters centers = HttpContext.Current.Application["FitnessCenters"] as FitnessCenters;

            User user = Users.GetByUsername(data.Username);
            foreach (int x in user.TrainingTrainer)
            {
                GroupTraining gt = GroupTrainings.GetById(x);
                if ((gt.Appointment >= DateTime.Now )&& gt.Exist)
                {
                    FitnessCenter fc = FitnessCenters.GetById(centers.ListOfFitnessCenters, gt.CenterId);
                    selected.Add(new Tuple<int, string>(gt.Id, fc.Name), gt);
                }
            }
            return selected;
        }


        [HttpPut]
        [Route("api/GroupTraining/CreateTraining")]
        public IHttpActionResult CreateTraining([FromBody] GroupTraining gt) {
            User user = HttpContext.Current.Session["user"] as User;

            if (gt.Appointment < DateTime.Now.AddDays(3)) {
                return BadRequest("Termin novog treninga mora biti bar 3 dana unapred");
            }
            GroupTrainings trainings = HttpContext.Current.Application["GroupTrainings"] as GroupTrainings;
            gt.Id = GroupTrainings.ListOfGroupTrainings.Count() + 1;
            gt.Exist = true;
            gt.CenterId = user.CenterIdWorking;
            gt.Visitors = new List<int>();
            GroupTrainings.ListOfGroupTrainings.Add(gt);
            GroupTrainings.Save(GroupTrainings.ListOfGroupTrainings);
            HttpContext.Current.Application["GroupTrainings"] = trainings;
            return Ok();
        }

        [HttpPut]
        [Route("api/GroupTraining/ModifyTraining")]
        public IHttpActionResult ModifyTraining([FromBody] GroupTraining gt)
        {
            GroupTrainings trainings = HttpContext.Current.Application["GroupTrainings"] as GroupTrainings;
            GroupTraining x = GroupTrainings.GetById(gt.Id);
            int index = GroupTrainings.ListOfGroupTrainings.IndexOf(x);
            x.TrainingName = gt.TrainingName;
            x.Type = gt.Type;
            x.Duration = gt.Duration;
            x.Appointment = gt.Appointment;
            if (gt.Capacity < x.Visitors.Count()) {
                return BadRequest("Neuspesna izmena treninga, prijavljeni broj posetioca je veci od novog kapaciteta");
            }
            x.Capacity = gt.Capacity;
            GroupTrainings.ListOfGroupTrainings.RemoveAt(index);
            GroupTrainings.ListOfGroupTrainings.Insert(index, x);
            GroupTrainings.Save(GroupTrainings.ListOfGroupTrainings);
            HttpContext.Current.Application["GroupTrainings"] = trainings;
            return Ok();
        }


        [HttpDelete]
        [Route("api/GroupTraining/DeleteTraining")]
        public IHttpActionResult DeleteTraining([FromBody] GroupTraining gt) {
            
            GroupTrainings trainings = HttpContext.Current.Application["GroupTrainings"] as GroupTrainings;
            GroupTraining x = GroupTrainings.GetById(gt.Id);
            if (x.Visitors.Count > 0)
            {
                return BadRequest("Neuspesno, postoje prijavljeni posetioci");
            }
            else if (x.Appointment < DateTime.Now)
            {
                return BadRequest("Neuspesno, trening je vec odrzan");
            }
            else {
                int index = GroupTrainings.ListOfGroupTrainings.IndexOf(x);
                x.Exist = false;
                GroupTrainings.ListOfGroupTrainings.RemoveAt(index);
                GroupTrainings.ListOfGroupTrainings.Insert(index, x);
                GroupTrainings.Save(GroupTrainings.ListOfGroupTrainings);
                HttpContext.Current.Application["GroupTrainings"] = trainings;
                return Ok();
            }
                    
             
            
        }
    }
}
