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
            foreach (GroupTraining gt in trainings.ListOfGroupTrainings) {
                if ((gt.CenterId == id) && gt.Exist && (gt.Appointment>DateTime.Now)) { selected.Add(gt); }
            }
            return selected;
        }
    }
}
