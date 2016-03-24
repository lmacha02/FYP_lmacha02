using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FinalYearProjectClassified.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        { }

        public DbSet<Ad> Ads { get; set; }

        public DbSet<Image> Images { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<ViewModels.Ads.DetailsViewModel> DetailsViewModels { get; set; }
    }
}