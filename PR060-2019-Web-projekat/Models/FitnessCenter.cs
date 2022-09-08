using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR060_2019_Web_projekat.Models
{
    public class FitnessCenter
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public Address Addresss { get; set; }

        public DateTime OpeningDate { get; set; }

        public int OwnerId { get; set; }

        public int FeeYear { get; set; }

        public int FeeMonth { get; set; }

        public int PriceTrainig { get; set; }

        public int PriceTrainingGroup { get; set; }

        public int PriceTrainingPrivate { get; set; }

        public bool Exist { get; set; } 
    }
}