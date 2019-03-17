using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Courier.Models.ViewModels
{
    public class ReceiptV
    {

        public int orderID { get; set; }
        
        [Required]
        [Display(Name ="Amount")]
        public decimal amount { get; set; }

    }
}