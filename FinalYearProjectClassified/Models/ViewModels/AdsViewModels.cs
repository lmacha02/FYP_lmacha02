using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalYearProjectClassified.Models.ViewModels.Ads
{
    public class DetailsViewModel
    {
        [Key]
        public int AdId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        //public bool IsFeatured { get; set; } ???
        public string PostCode { get; set; }
        public string ImageFileName { get; set; }

        public List<Image> Images { get; set; }

        //public bool IsDisabled { get; set; }
        //public DateTime? DisabledOn { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public ApplicationUser User { get; set; }
    }
}