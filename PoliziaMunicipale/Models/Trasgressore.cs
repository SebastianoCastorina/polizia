using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PoliziaMunicipale.Models
{
    public class Trasgressore
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int CAP { get; set; }
        public string CF { get; set; }

        public Trasgressore() { }
        public Trasgressore(string surname, string name, string address, string city, int cAP, string cF)
        {
            Surname = surname;
            Name = name;
            Address = address;
            City = city;
            CAP = cAP;
            CF = cF;
        }
    }
}