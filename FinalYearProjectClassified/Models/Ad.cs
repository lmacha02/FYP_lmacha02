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

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        public decimal Price { get; set; }
        public string PostCode { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public bool IsDisabled { get; set; }
        public DateTime? DisabledOn { get; set; }

        public virtual List<Image> Images { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }

    //Repository functions specific to class Ad
    public sealed class AdRepository : BaseRepository<Ad>
    {
        public AdRepository(ApplicationDbContext context) : 
            base(context)
        { }

        protected override DbSet<Ad> Entities
        {
            get { return this._context.Ads; }
            
        }

        private DbSet<Image> Images
        {
            get { return this._context.Images; }

        }

        public IQueryable<Ad> GetAds(string keywords, string postcode, bool isFeatured)
        {
            return this.Find()
                .Where(x => x.IsFeatured.Equals(isFeatured))
                .Where(x => x.IsDisabled.Equals(false))
                .Where(x =>
                    (String.IsNullOrEmpty(keywords) || (!String.IsNullOrEmpty(keywords) &&
                            (
                                x.Name.Contains(keywords) ||
                                x.Description.Contains(keywords)
                            )
                        )
                    )
                )
                .Where(x =>
                    (String.IsNullOrEmpty(postcode) || (!String.IsNullOrEmpty(postcode) &&
                            (
                                x.PostCode.Contains(postcode) ||
                                x.PostCode.Contains(postcode)
                            )
                        )
                    )
                );
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

        public string DeleteImage(int ImageId)
        {
            if (ImageId > 0)
            {
                var img = this._context.Images.Find(ImageId);

                this._context.Images.Remove(img);
                this._context.SaveChanges();

                return "Image deleted";
            }
            return "Something went wrong!"; 
        }
    }
}