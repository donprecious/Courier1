using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Courier.Models.ViewModels
{
    public class SupportV
    {

        public int TicketID { get; set; }

        [EmailAddress]
        [Required (ErrorMessage ="Enter Email")]
        public string email { get; set; }

        [Required (ErrorMessage ="Enter Message" )]
        public string Message{get; set;}

    }

    public class supportReply
    {
        public int TicketID { get; set; }

     
                [Required(ErrorMessage = "Enter Message")]
        public string Message { get; set; }
    }
}