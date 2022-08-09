using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PR060_2019_Web_projekat.Models
{
    enum Role
    {
        Posetilac,
        Trener,
        Vlasnik
    }
    public class Korisnik
    {

        private string korisnickoIme;
        private string lozinka;
        private string ime;
        private string prezime;
        private string pol;
        private string email;
        private DateTime datumRodjenja;
        private Role uloga;



    }
}