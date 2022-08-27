using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR060_2019_Web_projekat.Models
{
    
    public class GroupTraining
    {
        public GroupTraining(int id, string name, int centerId, int duration, DateTime appointment, int capacity, List<User> visitors)
        {
            Id = id;
            Name = name;
            CenterId = centerId;
            Duration = duration;
            Appointment = appointment;
            Capacity = capacity;
            Visitors = visitors;
            Exist = true;
        }

        public GroupTraining(int id, string name, int centerId, int duration, DateTime appointment, int capacity, List<User> visitors, bool exist)
        {
            Id = id;
            Name = name;
            CenterId = centerId;
            Duration = duration;
            Appointment = appointment;
            Capacity = capacity;
            Visitors = visitors;
            Exist = exist;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int CenterId { get; set; }

        public int Duration { get; set; }

        public DateTime Appointment { get; set; }

        public int Capacity { get; set; }

        public List<User> Visitors { get; set; } 

        public bool Exist { get; set; } 
    }
}