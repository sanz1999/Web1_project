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
        public static FitnessCenter GetById(List<FitnessCenter> centers, int id)
        {
            return centers.Find(o => o.Id == id);
        }
        internal static void Save(List<FitnessCenter> ListOfCenters)
        {
            string path = "~/App_Data/FitnessCenters.json";
            path = HostingEnvironment.MapPath(path);
            string json = JsonConvert.SerializeObject(ListOfCenters, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(json);
            }
        }

    }
}