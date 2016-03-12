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


        public ActionResult Index(string keywords = "", string postcode = "")
        {
            var model = new Models.Home.IndexViewModel();

            model.Ads = this._adRepository
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
                )
                .ToList();


            //TEST
            model.FilterListItems = new List<FilterListItem> {
                new FilterListItem { Text = "Price High to Low", Value = "value 1" },
                new FilterListItem { Text = "Price Low to High", Value = "value 2" },
                new FilterListItem { Text = "Price High to Low", Value = "value 3" },
                new FilterListItem { Text = "Price High to Low", Value = "value 4" }
                };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(Models.Home.IndexViewModel model)
        {
            return RedirectToAction("Index", new
            {
                keywords = model.Keywords,
                postcode = model.PostCode
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