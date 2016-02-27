using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalYearProjectClassified.Models.MyAccount
{
    public class IndexViewModel
    {
        public List<Ad> Ads { get; set; }
        public ApplicationUser User { get; set; }
    }

    public class EditAdViewModel
    {
        public int? AdId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string PostCode { get; set; }
        
        
        
        //public string ImageFileName { get; set; }

        //update this value when Ad is changed or add new field eg. EditedOn?
        //public DateTime CreatedOn { get; set; }

    }

    public class DetailsAdViewModel
    {
        public int AdId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        //public bool IsFeatured { get; set; } ???
        public string PostCode { get; set; }
        //public string ImageFileName { get; set; }

        //public bool IsDisabled { get; set; }
        //public DateTime? DisabledOn { get; set; }

        public DateTime CreatedOn { get; set; }
        public decimal Price { get; set; }

        public ApplicationUser User { get; set; }


    }
}