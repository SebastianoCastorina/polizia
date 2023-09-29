using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PoliziaMunicipale.Models
{
    public class Verbale
    {
        public int Id { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string Agente { get; set; }
        public DateTime DataVerbale { get; set; }
        public double Importo { get; set; }
        public int PuntiTolti { get; set; }
        public int IdTrasgressore { get; set; }
        public int IdViolazione { get; set; }

        public Verbale() { }
        public Verbale(DateTime dataViolazione, string indirizzoViolazione, string agente, DateTime dataVerbale, double importo, int puntiTolti, int idTrasgressore, int idViolazione)
        {
            DataViolazione = dataViolazione;
            IndirizzoViolazione = indirizzoViolazione;
            Agente = agente;
            DataVerbale = dataVerbale;
            Importo = importo;
            PuntiTolti = puntiTolti;
            IdTrasgressore = idTrasgressore;
            IdViolazione = idViolazione;
        }
    }
}