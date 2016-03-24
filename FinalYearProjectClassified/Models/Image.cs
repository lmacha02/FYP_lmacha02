using FinalYearProjectClassified.Models.Repositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FinalYearProjectClassified.Models
{
    [Table("Images")]
    public class Image : IDbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool IsMainImage { get; set; }

        public string FilePath { get; set; }

        public int AdId { get; set; }

        [ForeignKey("AdId")]
        public virtual Ad Ad { get; set; }
    }


    // Repository methods for Image
    //public sealed class ImageRepository : BaseRepository<Image>
    //{
    //    public ImageRepository(ApplicationDbContext context) :
    //        base(context)
    //    { }

    //    protected override DbSet<Image> Entities
    //    {
    //        get { return this._context.Images; }
    //    }

    //    public IQueryable<Image> FindByAdId(int AdId)
    //    {
    //        return this.Find()
    //            .Where(x => x.AdId.Equals(AdId));
    //    }
    //}
}