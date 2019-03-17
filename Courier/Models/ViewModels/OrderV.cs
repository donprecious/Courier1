using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Courier.Models.DbModel;
using System.ComponentModel.DataAnnotations;

namespace Courier.Models.ViewModels
{
    public class OrderV
    {
        public int OrderID { get; set; }

        [Display(Name ="Package name")]
        [Required(ErrorMessage ="Enter package name")]
        public string Packagename { get; set; }

        [Required(ErrorMessage = "Describe your package")]
        public string Description { get; set; }
        
        public string weight { get; set; }
        public string height { get; set; }
        public string width { get; set; }
        public string userId { get; set; }
        public Location location { get; set; }
    }
}