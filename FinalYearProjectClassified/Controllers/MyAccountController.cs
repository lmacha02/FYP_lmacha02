using FinalYearProjectClassified.Models;
using FinalYearProjectClassified.Models.MyAccount;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalYearProjectClassified.Controllers
{
    [Authorize]
    public class MyAccountController : BaseController
    {
        AdRepository _adRepository;

        public MyAccountController()
        {
            this._adRepository = new AdRepository(this.Database);
        }


        // GET: MyAccount
        public ActionResult Index()
        {
            var model = new Models.MyAccount.IndexViewModel();
            model.User = this.CurrentUser;

            model.Ads = this._adRepository
                .FindActiveByUserId(this.CurrentUser.Id)
                .ToList();

            return View(model);
        }

        // GET: MyAccount/Details/id
        public ActionResult DetailsAd(int id) //does it need the "?"
        {
            var model = new DetailsAdViewModel();
            model.User = this.CurrentUser;

            var ad = this._adRepository.FindById(id);

            model.AdId = id;
            model.Name = ad.Name;
            model.Description = ad.Description;
            model.Price = ad.Price;
            model.PostCode = ad.PostCode;
            model.CreatedOn = ad.CreatedOn;



            return View(model);
        }

        // GET: MyAccount/EditAd/
        public ActionResult EditAd(int? id)
        {
            var model = new EditAdViewModel();

            if (id.HasValue)
            {
                var ad = this._adRepository.FindById(id.Value);

                model.AdId = id;
                model.Name = ad.Name;
                model.Description = ad.Description;
                model.Price = ad.Price;
                model.PostCode = ad.PostCode;
            }

            return View(model);
        }

        // POST: MyAccount/EditAd
        [HttpPost]
        public ActionResult EditAd(EditAdViewModel model, int? id)
        {
            if (ModelState.IsValid)
            {
                var ad = new Ad();

                if (id.HasValue)
                {
                    ad = this._adRepository.FindById(id.Value);
                }
                else
                {
                    ad.CreatedOn = DateTime.Now;
                    ad.UserId = this.CurrentUser.Id;
                }

                ad.Name = model.Name;
                ad.Description = model.Description;
                ad.Price = model.Price;
                ad.PostCode = model.PostCode.ToUpper();

                this._adRepository.Save(ad);

                //return RedirectToAction("EditAd", new { id = ad.Id });
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult DisableAd(int id)
        {
            if (id > 0)
            {
                this._adRepository.Disable(id);
            }

            return RedirectToAction("Index");
        }

        
    }
}
