using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Courier.Models.ViewModels
{
    public class QuotaDetail
    {
        [Required (ErrorMessage ="Enter Your Email")]
        [EmailAddress]
        public string Email
        {
            get; set;
        }
        
        [Required (ErrorMessage = "Enter country you are sending from")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Enter country you are sending to")]
        public string Destination { get; set; }

        [Display(Name ="Package Information")]
        [Required(ErrorMessage ="Provide a detailed information on the package")]
        public string packageInfo { get; set; }

    }

    public class join
    {
        public QuotaDetail quota{get; set; }
        public SupportV support { get; set; }

    }
}