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
    
    public partial class UserCredential
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
    
        public virtual User User { get; set; }
    }
}
