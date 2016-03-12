using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalYearProjectClassified.Models.Home
{
    public class IndexViewModel
    {
        public string Keywords { get; set; }

        public string PostCode { get; set; }

        public AdsOrder SortBy { get; set; }

        public List<Ad> Ads { get; set; }
    }
}