using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PR060_2019_Web_projekat.Models
{
    public class FitnessCenters
    {
        public List<FitnessCenter> ListOfFitnessCenters { get; set; }

        public FitnessCenters(string path)
        {
            path = HostingEnvironment.MapPath(path);
            ListOfFitnessCenters = new List<FitnessCenter>();

            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                ListOfFitnessCenters = JsonConvert.DeserializeObject<List<FitnessCenter>>(json);
            }

        }
    }
}