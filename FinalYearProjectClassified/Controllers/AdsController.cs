using FinalYearProjectClassified.Models;
using FinalYearProjectClassified.Models.ViewModels.Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalYearProjectClassified.Controllers
{
    public class AdsController : BaseController
    {
        AdRepository _adRepository;

        public AdsController()
        {
            this._adRepository = new AdRepository(this.Database);
        }

        // GET: Ad/Details/5
        public ActionResult Details(int? id)
        {
            var model = new DetailsViewModel();

            if (id.HasValue)
            {
                var ad = this._adRepository.FindById(id.Value);

                model.AdId = id.Value;
                model.Name = ad.Name;
                model.Description = ad.Description;
                model.Price = ad.Price;
                model.PostCode = ad.PostCode;
                model.ImageFileName = ad.ImageFileName;
                model.CreatedOn = ad.CreatedOn;
                model.User = ad.User;
            }
            else
            {
                ViewBag.Message = "Sorry! No Ad selected..";
            }


            //Display the google maps with the marker for the postcode


            return View (model);
        }

        
    }
}
