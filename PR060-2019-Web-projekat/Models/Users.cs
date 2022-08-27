using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PR060_2019_Web_projekat.Models
{
    public class Users
    {
        public List<User> ListOfUsers { get; set; }

        public Users(string path) {
            path = HostingEnvironment.MapPath(path);
            ListOfUsers = new List<User>();

            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                ListOfUsers = JsonConvert.DeserializeObject<List<User>>(json);
            }

        }

    }
}