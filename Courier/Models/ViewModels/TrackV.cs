using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Courier.Models.ViewModels
{
    public class TrackV
    {

        [Display(Name ="Tracking Number")]
        //[Required(ErrorMessage ="Enter Your Trackng No")]
        public string TrackID { get; set; }

        public string Source { get; set; }
        public string Destination { get; set; }
        public string CurrentLocation { get; set; }


        public string packageName { get; set; }
        
        

        public decimal CurrentLatitude { get; set; }
        public decimal CurrentLogitude { get; set; }
    

        public decimal DestinationLatitude { get; set; }
        public decimal DestinationLogitude { get; set; }

        public decimal SourceLatitude { get; set; }
        public decimal SourceLogitude { get; set; }
    }
}