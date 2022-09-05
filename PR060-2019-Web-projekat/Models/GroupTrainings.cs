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
        public static List<GroupTraining> ListOfGroupTrainings { get; set; }

        public GroupTrainings(string path)
        {
            path = HostingEnvironment.MapPath(path);
            GroupTrainings.ListOfGroupTrainings = new List<GroupTraining>();

            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                ListOfGroupTrainings = JsonConvert.DeserializeObject<List<GroupTraining>>(json);
            }

        }

        public static void AddVisitor(int user_id,int id) {
            GroupTraining training = GroupTrainings.GetById(id);
            int training_index = ListOfGroupTrainings.IndexOf(training);
            training.Visitors.Add(user_id);
            ListOfGroupTrainings.RemoveAt(training_index);
            ListOfGroupTrainings.Insert(training_index, training);
            Users.AddTrening(user_id, id);
            Save(ListOfGroupTrainings);
            return;
        }

        public static GroupTraining GetById(int id)
        {
            return ListOfGroupTrainings.Find(x => x.Id == id);
        }

        internal static void Save(List<GroupTraining> ListOfUsers)
        {
            string path = "~/App_Data/GroupTrainings.json";
            path = HostingEnvironment.MapPath(path);
            string json = JsonConvert.SerializeObject(ListOfUsers, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(json);
            }
        }
    }
}