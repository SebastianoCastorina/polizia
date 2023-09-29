using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PoliziaMunicipale.Models
{
    public class Violazione
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Violazione() { }

        public Violazione(string description)
        {
            Description = description;
        }
    }
}