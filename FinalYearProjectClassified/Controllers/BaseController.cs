using FinalYearProjectClassified.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalYearProjectClassified.Controllers
{
    public abstract class BaseController : Controller
    {
        ApplicationDbContext _db;
        public ApplicationDbContext Database
        {
            get
            {
                if (this._db == null)
                    this._db = new ApplicationDbContext();

                return this._db;
            }
        }

        ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                if (this._userManager == null)
                    this._userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                return _userManager;
            }
        }


        ApplicationUser _currentUser;
        public ApplicationUser CurrentUser
        {
            get
            {
                if (this._currentUser == null && this.User.Identity.IsAuthenticated)
                    this._currentUser = UserManager.FindByNameAsync(User.Identity.Name).Result;

                return this._currentUser;
            }
        }
    }
}