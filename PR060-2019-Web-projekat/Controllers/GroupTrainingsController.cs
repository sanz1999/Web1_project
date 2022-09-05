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
    }
}
