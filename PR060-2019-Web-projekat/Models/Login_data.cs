using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR060_2019_Web_projekat.Models
{
    public class Login_data
    {
        public Login_data(Enum_Role role, string username, bool authenticated)
        {
            Role = role;
            Username = username;
            Authenticated = authenticated;
        }

        public Enum_Role Role { get; set; }
        public string Username { get; set; }
        public bool Authenticated { get; set; }
    }
}