using FinalYearProjectClassified.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FinalYearProjectClassified.Models
{
    [Table("Ads")]
    public class Ad : IDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //Add Data Annotations??

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        public decimal Price { get; set; }
        public string PostCode { get; set; }
        public string ImageFileName { get; set; }

        public DateTime CreatedOn { get; set; }
        public string UserId { get; set; }

        // name to display to the public ??

        public bool IsDisabled { get; set; }
        public DateTime? DisabledOn { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }

    //Repository with functions specific to class Ad
    public sealed class AdRepository : BaseRepository<Ad>
    {
        public AdRepository(ApplicationDbContext context) : 
            base(context)
        { }

        protected override DbSet<Ad> Entities
        {
            get { return this._context.Ads; }
        }


        //Get all Non Featured Ads              TODO: ensure only Active Ads are loaded
        public IQueryable<Ad> GetNonFeaturedAds()
        {
            return this.Find()
                .Where(x => x.IsFeatured.Equals(false));
        }

        //Get all Featured Ads                  TODO: ensure only Active Ads are loaded
        public IQueryable<Ad> GetFeaturedAds()
        {
            return this.Find()
                .Where(x => x.IsFeatured.Equals(true));
        }


        // Methods Bellow for signed-in users

        public IQueryable<Ad> FindByUserId(string userId)
        {
            return this.Find()
                .Where(x => x.UserId.Equals(userId));
        }

        public IQueryable<Ad> FindActiveByUserId(string userId)
        {
            return this.FindByUserId(userId)
                .Where(x => !x.IsDisabled);
        }

        public Ad Disable(int id)
        {
            var ad = this.FindById(id);
            return this.Disable(ad);
        }

        public Ad Disable(Ad entity)
        {
            entity.IsDisabled = true;
            entity.DisabledOn = DateTime.Now;

            return this.Save(entity);
        }
    }
}