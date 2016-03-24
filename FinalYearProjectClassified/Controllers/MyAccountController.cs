using FinalYearProjectClassified.Models;
using FinalYearProjectClassified.Models.MyAccount;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

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
        public ActionResult DetailsAd(int id)                   // TODO does it need the "?"
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
            model.Images = ad.Images.ToList();                   // TODO : Check if its correct!

            return View(model);
        }

        // GET: MyAccount/EditAd/
        public ActionResult EditAd(int? id)
        {
            var model = new EditAdViewModel();

            if (id.HasValue)
            {
                var ad = this._adRepository.FindById(id.Value);         // TODO - Add control for editting images

                model.AdId = id;
                model.Name = ad.Name;
                model.Description = ad.Description;
                model.Price = ad.Price;
                model.PostCode = ad.PostCode;
                model.Images = ad.Images;
            }
            return View(model);
        }

        // POST: MyAccount/EditAd
        [HttpPost]
        public ActionResult EditAd(EditAdViewModel model, int? id, IEnumerable<HttpPostedFileBase> files)
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

                // Images upload
                var imgDir = "~/Images";
                var noImgUploaded = true;
                ad.Images = new List<Image>();

                foreach (var file in files.Where(file => file != null && file.ContentLength > 0))
                {
                    noImgUploaded = false;

                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Server.MapPath(imgDir), fileName);
                    file.SaveAs(filePath);
                    fileName = Path.Combine(imgDir, fileName);
                    
                    ad.Images.Add(new Image() { FilePath = fileName });
                }
                if(noImgUploaded && !id.HasValue)
                {
                    //No image uploaded, display custom image (no image available pic)
                    ad.Images.Add(new Image() { FilePath = imgDir + "/noImage.jpg" });
                }

                var entity = this._adRepository.Save(ad);

                return RedirectToAction("EditAd");
            }
            return View(model);
        }

        public ActionResult DeleteImage(int id, int imgId)                     // TODO : finish!
        {
            //Delete image
            if (imgId > 0)
            {
                this._adRepository.DeleteImage(imgId);

                return RedirectToAction("EditAd", new{id = id});
            }
            return RedirectToAction("EditAd");
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
