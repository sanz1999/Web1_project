using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR060_2019_Web_projekat.Models
{
    
    public class User
    {

        public User() { }

        public User(int id, string firstName, string lastName, Enum_Gender gender, string eMail, DateTime birthDate, Enum_Role role, List<int> trainingVisitor, List<int> trainingTrainer, int centerIdWorking, List<int> ownedCenters, string userName, string password, bool exist)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            EMail = eMail;
            BirthDate = birthDate;
            Role = role;
            TrainingVisitor = trainingVisitor;
            TrainingTrainer = trainingTrainer;
            CenterIdWorking = centerIdWorking;
            OwnedCenters = ownedCenters;
            UserName = userName;
            Password = password;
            Exist = exist;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; } 

        public Enum_Gender Gender { get; set; }

        public string EMail { get; set; }

        public DateTime BirthDate { get; set; }

        public Enum_Role Role { get; set; }

        public List<int> TrainingVisitor { get; set; }

        public List<int> TrainingTrainer { get; set; }

        public int CenterIdWorking { get; set; }

        public List<int> OwnedCenters { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool Exist { get; set; }

    }
}