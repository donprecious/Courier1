//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Courier.Models.DbModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class support
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public support()
        {
            this.Responses = new HashSet<Response>();
        }
    
        public int TicketID { get; set; }
        public string email { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> dateSpam { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Response> Responses { get; set; }
    }
}
