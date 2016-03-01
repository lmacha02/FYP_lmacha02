using FinalYearProjectClassified.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalYearProjectClassified.Controllers
{
    public class HomeController : BaseController

    {
        AdRepository _adRepository;

        public HomeController()
        {
            this._adRepository = new AdRepository(this.Database);
        }


        public ActionResult Index()
        {
            var model = new Models.Home.IndexViewModel();
            
            model.Ads = this._adRepository
                .GetNonFeaturedAds()
                .ToList();

            

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}