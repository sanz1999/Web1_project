using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR060_2019_Web_projekat.Models
{
    public class FitnessCenter
    {
        public FitnessCenter(int id, string name, Address address, DateTime openingDate, string ownerId, int feeYear, int feeMonth, int priceTrainig, int priceTrainingGroup, int priceTrainingPrivate)
        {
            Id = id;
            Name = name;
            Address = address;
            OpeningDate = openingDate;
            OwnerId = ownerId;
            FeeYear = feeYear;
            FeeMonth = feeMonth;
            PriceTrainig = priceTrainig;
            PriceTrainingGroup = priceTrainingGroup;
            PriceTrainingPrivate = priceTrainingPrivate;
            Exist = true;
        }

        public FitnessCenter(int id, string name, Address address, DateTime openingDate, string ownerId, int feeYear, int feeMonth, int priceTrainig, int priceTrainingGroup, int priceTrainingPrivate, bool exist)
        {
            Id = id;
            Name = name;
            Address = address;
            OpeningDate = openingDate;
            OwnerId = ownerId;
            FeeYear = feeYear;
            FeeMonth = feeMonth;
            PriceTrainig = priceTrainig;
            PriceTrainingGroup = priceTrainingGroup;
            PriceTrainingPrivate = priceTrainingPrivate;
            Exist = exist;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Address Address { get; set; }

        public DateTime OpeningDate { get; set; }

        public string OwnerId { get; set; }

        public int FeeYear { get; set; }

        public int FeeMonth { get; set; }

        public int PriceTrainig { get; set; }

        public int PriceTrainingGroup { get; set; }

        public int PriceTrainingPrivate { get; set; }

        public bool Exist { get; set; } 
    }
}