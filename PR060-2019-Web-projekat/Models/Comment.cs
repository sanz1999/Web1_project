using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR060_2019_Web_projekat.Models
{
    public class Comment
    {
        

       

        public int Id { get; set; }

        public int CenterId { get; set; }

        public string Username { get; set; }  

        public string Content { get; set; }

        public int RatingGrade { get; set; }

        public bool Approved { get; set; }


        public void Accept() {
            this.Approved = true;
        }

        public void Decline() {
            this.Approved = false;
        }
    }
}