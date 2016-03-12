using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalYearProjectClassified.Models
{
    public enum AdsOrder
    {
        [Display(Name = "Most Recent")]
        MostRecent = 0,

        [Display(Name = "Price High to Low")]
        PriceHighToLow = 2,

        [Display(Name = "Price Low To High")]
        PriceLowToHigh = 3
    }
}