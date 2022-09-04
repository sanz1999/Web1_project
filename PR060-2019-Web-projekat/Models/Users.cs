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
        public static List<User> ListOfUsers { get; set; }

        public Users(string path) {
            path = HostingEnvironment.MapPath(path);
            ListOfUsers = new List<User>();

            using (StreamReader sr = new StreamReader(path))
            {
                string json = sr.ReadToEnd();
                ListOfUsers = JsonConvert.DeserializeObject<List<User>>(json);
            }

        }
        public static User GetByUsername(string username)
        {
            return ListOfUsers.Find(x => x.UserName == username);
        }
        public static User GetById(int id)
        {
            return ListOfUsers.Find(x => x.Id == id);
        }
        public static bool ExistByUsername(string username)
        {
            return ListOfUsers.Exists(x => x.UserName == username);
        }

        public static bool FindByMail(  string mail)
        {
            return ListOfUsers.Exists(x => x.EMail == mail);
        }
        public static User Add( User user)
        {
            ListOfUsers.Add(user);
            Save(ListOfUsers);
            return user;
        }

        public static User AddTrening( string username, int id)
        {
            //GetById(ListOfUsers,username).TreninziPosetilac.Add(id);

           Save(ListOfUsers);

            return GetByUsername(username);
        }

        public static User Update( User user)
        {
            ListOfUsers.Remove(GetById( user.Id));
            ListOfUsers.Add(user);
            Save(ListOfUsers);
            return user;
        }
        //JSON OPERATIONS
        internal static void Save(List<User> ListOfUsers)
        {
            string path = "~/App_Data/Users.json";
            path = HostingEnvironment.MapPath(path);
            string json = JsonConvert.SerializeObject(ListOfUsers, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(json);
            }
        }

    }
}