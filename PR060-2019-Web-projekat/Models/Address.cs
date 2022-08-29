using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR060_2019_Web_projekat.Models
{
    public class Address
    {
        public Address(string street, string city, int zIP)
        {
            Street = street;
            City = city;
            ZIP = zIP;
        }

        public string Street { get; set; }
        public string City { get; set; }
        public int ZIP { get; set; }
    }
}