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
            centers.ListOfFitnessCenters=centers.ListOfFitnessCenters.OrderBy(o=>o.Name).ToList();
            return centers.ListOfFitnessCenters;
        }
        [HttpGet]
        public FitnessCenter GetFitnessCenter(int id) {
      
            FitnessCenters centers = HttpContext.Current.Application["FitnessCenters"] as FitnessCenters;
            return FitnessCenters.GetById(centers.ListOfFitnessCenters,id);
        }
    }
}
