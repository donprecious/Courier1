using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Courier.Models.ViewModels
{
    public class LocationV
    {
        public int LocationID { get; set; }

        [Required (ErrorMessage ="Order ID not found") ]
        public int OrderID { get; set; }

        [Required (ErrorMessage ="where will your package go")]
        public string Destination { get; set; }


        


        [Required(ErrorMessage = "where is your package coming from")]
        public string Source { get; set; }

        public string CurrentLocation { get; set; }

        public decimal DestinationLatitude { get; set; }
        public decimal DestinationLogitude { get; set; }

        public decimal SourceLatitude { get; set; }
        public decimal SourceLogitude { get; set; }


        public decimal CurrentLatitude { get; set; }
        public decimal CurrentLogitude { get; set; }
    }
}