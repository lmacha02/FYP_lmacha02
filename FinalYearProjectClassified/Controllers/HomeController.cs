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


        public ActionResult Index(
            string keywords = "", 
            string postcode = "", 
            int sortBy = 0)
        {
           var model = new Models.Home.IndexViewModel();
            model.SortBy = (AdsOrder)sortBy;
            model.Keywords = keywords;
            model.PostCode = postcode;

            var ads = this._adRepository
                .GetNonFeaturedAds()
                .Where(x =>
                    (String.IsNullOrEmpty(keywords) ||
                    (!String.IsNullOrEmpty(keywords) &&
                            (
                                x.Name.Contains(keywords) ||
                                x.Description.Contains(keywords)
                            )
                        )
                    )
                )
                .Where(x =>
                    (String.IsNullOrEmpty(postcode) ||
                    (!String.IsNullOrEmpty(postcode) &&
                            (
                                x.PostCode.Contains(postcode) ||
                                x.PostCode.Contains(postcode)
                            )
                        )
                    )
                );

            switch (model.SortBy)
            {
                default:
                case AdsOrder.MostRecent:
                    ads = ads.OrderBy(x => x.CreatedOn);
                    break;
                case AdsOrder.PriceLowToHigh:
                    ads = ads.OrderBy(x => x.Price);
                    break;
                case AdsOrder.PriceHighToLow:
                    ads = ads.OrderByDescending(x => x.Price);
                    break;
            }

            model.Ads = ads.ToList();  

            return View(model);
        }


        [HttpPost]
        public ActionResult Index(Models.Home.IndexViewModel model)
        {
            return RedirectToAction("Index", new
            {
                keywords = model.Keywords,
                postcode = model.PostCode,
                sortBy = (int)model.SortBy
            });
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