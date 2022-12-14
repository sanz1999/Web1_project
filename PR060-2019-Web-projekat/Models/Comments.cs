using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PR060_2019_Web_projekat.Models
{
    public class Comments
    {
        public List<Comment> ListOfComments { get; set; }
       
        public Comments(string path)
        {
            path = HostingEnvironment.MapPath(path);
            ListOfComments = new List<Comment>();

            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                ListOfComments = JsonConvert.DeserializeObject<List<Comment>>(json);
            }

        }
        public static void Save(List<Comment> comments) {
            string path = "~/App_Data/Comments.json";
            path = HostingEnvironment.MapPath(path);
            string json = JsonConvert.SerializeObject(comments, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(json);
            }
        }
    }
}