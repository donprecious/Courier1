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
    
    public partial class Response
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Response()
        {
            this.ResponseReplies = new HashSet<ResponseReply>();
        }
    
        public int ResponseID { get; set; }
        public Nullable<int> QuotaID { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> dateSpam { get; set; }
        public string status { get; set; }
        public Nullable<int> TicketID { get; set; }
    
        public virtual Quota Quota { get; set; }
        public virtual support support { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResponseReply> ResponseReplies { get; set; }
    }
}
