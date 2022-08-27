using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PR060_2019_Web_projekat.Models
{
    public class GroupTrainings
    {
        public List<GroupTraining> ListOfGroupTrainings { get; set; }

        public GroupTrainings(string path)
        {
            path = HostingEnvironment.MapPath(path);
            ListOfGroupTrainings = new List<GroupTraining>();

            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                ListOfGroupTrainings = JsonConvert.DeserializeObject<List<GroupTraining>>(json);
            }

        }
    }
}