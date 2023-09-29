using PoliziaMunicipale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliziaMunicipale.Controllers
{
    public class TrasgressoreController : Controller
    {
        // GET: Trasgressore
       public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Trasgressore t)
        {
            if(ModelState.IsValid)
            {
                DB.AggiungiTrasgressore(t.Surname, t.Name, t.Address, t.City, t.CAP, t.CF);
                return RedirectToAction("Index","Home");
            } else return View();
        }
    }
}