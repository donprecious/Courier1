﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CourierEntities : DbContext
    {
        public CourierEntities()
            : base("name=CourierEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CurrentLocation> CurrentLocations { get; set; }
        public virtual DbSet<destination> destinations { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Quota> Quotas { get; set; }
        public virtual DbSet<Response> Responses { get; set; }
        public virtual DbSet<ResponseReply> ResponseReplies { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<support> supports { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
    }
}
