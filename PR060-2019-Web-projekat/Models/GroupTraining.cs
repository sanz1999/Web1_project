using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR060_2019_Web_projekat.Models
{
    
    public class GroupTraining
    {
        

        public GroupTraining(int id,string trainingName, Enum_TrainingType type, int centerId, int duration, DateTime appointment, int capacity, List<int> visitors, bool exist)
        {
            Id = id;
            TrainingName = trainingName;
            Type = type;
            CenterId = centerId;
            Duration = duration;
            Appointment = appointment;
            Capacity = capacity;
            Visitors = visitors;
            Exist = exist;
        }

        public int Id { get; set; }

        public string TrainingName { get; set; }

        public Enum_TrainingType Type { get; set; }

        public int CenterId { get; set; }

        public int Duration { get; set; }

        public DateTime Appointment { get; set; }

        public int Capacity { get; set; }

        public List<int> Visitors { get; set; } 

        public bool Exist { get; set; } 
    }
}