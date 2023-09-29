using PoliziaMunicipale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoliziaMunicipale.Controllers
{
    public class ViolazioneController : Controller
    {
        // GET: Violazione
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Violazione v)
        {
             if(ModelState.IsValid)
            {
                DB.AggiungiViolazione(v.Description);
                return RedirectToAction("Index","Home");
            } else return View();
        }
    }
}