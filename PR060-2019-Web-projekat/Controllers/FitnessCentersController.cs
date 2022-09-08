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
    public class FitnessCentersController : ApiController
    {
        public List<FitnessCenter> Get()
        {
            FitnessCenters centers = HttpContext.Current.Application["FitnessCenters"]  as FitnessCenters;
            List<FitnessCenter> selected = centers.ListOfFitnessCenters.OrderBy(o => o.Name).ToList();
            selected.RemoveAll(o => !o.Exist);
            
            return selected;
        }
        [HttpGet]
        public FitnessCenter GetFitnessCenter(int id) {
      
            FitnessCenters centers = HttpContext.Current.Application["FitnessCenters"] as FitnessCenters;
            return FitnessCenters.GetById(centers.ListOfFitnessCenters,id);
        }
        [HttpPost]
        [Route("api/FitnessCenters/FcForOwner")]
        public List<FitnessCenter> FcForOwner([FromBody]User person)
        {
            FitnessCenters centers = HttpContext.Current.Application["FitnessCenters"] as FitnessCenters;
            List<FitnessCenter> selected = new List<FitnessCenter>();
            User owner = Users.GetByUsername(person.UserName);
            foreach (FitnessCenter x in centers.ListOfFitnessCenters) {
                if ((x.OwnerId == owner.Id)&&x.Exist) {
                    selected.Add(x);

                }
            }
            return selected;

        }
        [HttpPost]
        [Route("api/FitnessCenters/GetSingle")]
        public FitnessCenter GetSingle([FromBody] User fc)
        {
            FitnessCenters centers = HttpContext.Current.Application["FitnessCenters"] as FitnessCenters;
            return FitnessCenters.GetById(centers.ListOfFitnessCenters,fc.Id);

        }
        [HttpPost]
        [Route("api/FitnessCenters/CreateCenter")]
        public IHttpActionResult CreateCenter([FromBody] FitnessCenter nfc) {
            FitnessCenters centers = HttpContext.Current.Application["FitnessCenters"] as FitnessCenters;
            User user = HttpContext.Current.Session["user"] as User;
            nfc.Id = centers.ListOfFitnessCenters.Count + 1;
            nfc.Exist = true;
            nfc.Addresss = new Address("ULICA 1","NEKI GRAD",12312) ;
            nfc.OwnerId = user.Id;
            centers.ListOfFitnessCenters.Add(nfc);
            FitnessCenters.Save(centers.ListOfFitnessCenters);
            HttpContext.Current.Application["FitnessCenters"] = centers;
            return Ok();
        }
        [HttpPost]
        [Route("api/FitnessCenters/ModifyCenter")]
        public IHttpActionResult ModifyCenter([FromBody] FitnessCenter nfc)
        {
            FitnessCenters centers = HttpContext.Current.Application["FitnessCenters"] as FitnessCenters;
            FitnessCenter fc = FitnessCenters.GetById(centers.ListOfFitnessCenters,nfc.Id);
            int index = centers.ListOfFitnessCenters.IndexOf(fc);
            centers.ListOfFitnessCenters.RemoveAt(index);
            nfc.Exist = fc.Exist;
            nfc.Addresss = fc.Addresss;
            nfc.OwnerId = fc.OwnerId;
            centers.ListOfFitnessCenters.Insert(index, nfc);
            FitnessCenters.Save(centers.ListOfFitnessCenters);
            HttpContext.Current.Application["FitnessCenters"] = centers;

            return Ok();
        }
        [HttpDelete]
        [Route("api/FitnessCenters/DeleteCenter")]
        public IHttpActionResult DeleteCenter([FromBody] FitnessCenter nfc)
        {
            FitnessCenters centers = HttpContext.Current.Application["FitnessCenters"] as FitnessCenters;
            GroupTrainings trainigs = HttpContext.Current.Application["GroupTrainings"] as GroupTrainings;
            FitnessCenter fc = FitnessCenters.GetById(centers.ListOfFitnessCenters,nfc.Id);
            foreach (GroupTraining gt in GroupTrainings.ListOfGroupTrainings) {
                if ((gt.CenterId == fc.Id) && (gt.Visitors.Count > 0) && (gt.Appointment>DateTime.Now)) {
                    return BadRequest("Ne mozete da obrisete centar, postoje buduci treninzi na koje je neko prijavljen");
                }
            }
            int index = centers.ListOfFitnessCenters.IndexOf(fc);
            fc.Exist = false;
            centers.ListOfFitnessCenters.RemoveAt(index);
            centers.ListOfFitnessCenters.Insert(index, fc);
            HttpContext.Current.Application["FitnessCenters"] = centers;
            FitnessCenters.Save(centers.ListOfFitnessCenters);
            return Ok();
        }
    }
}
