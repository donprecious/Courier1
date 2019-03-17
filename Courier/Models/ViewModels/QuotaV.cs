using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Courier.Models.DbModel;

namespace Courier.Models.ViewModels
{
    public class QuotaV
    {

    }

    public class QuotaList
    {
        public List<Response> Responses { get; set; }
    }
    
    public class QuotaReplyV
    {
       
        public int QuotaID { get; set; }

        [Display (Name = "Message")]
        public string QuotaMessage { get; set; }

    }

}