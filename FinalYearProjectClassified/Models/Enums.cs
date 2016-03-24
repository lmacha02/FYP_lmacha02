using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalYearProjectClassified.Models
{
    public enum AdsOrder
    {
        [Display(Name = "Most Recent First")]
        MostRecent = 0,

        [Display(Name = "Price High to Low")]
        PriceHighToLow = 2,

        [Display(Name = "Price Low To High")]
        PriceLowToHigh = 3
    }

    public enum AdsCategory
    {
        [Display(Name = "All")]
        All = 0,

        [Display(Name = "Electronics")]
        Electronics = 1,

        [Display(Name = "Services")]
        Services = 2,

        [Display(Name = "Vehicles")]
        Vehicles = 3,

        [Display(Name = "Pets")]
        Pets = 4,

        [Display(Name = "Other")]
        Other = 5
    }
}