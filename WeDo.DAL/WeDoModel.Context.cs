﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeDo.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class angelhackEntities : DbContext
    {
        public angelhackEntities()
            : base("name=angelhackEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<requestbid> requestbids { get; set; }
        public DbSet<requestnotification> requestnotifications { get; set; }
        public DbSet<request> requests { get; set; }
        public DbSet<requeststatu> requeststatus { get; set; }
        public DbSet<user> users { get; set; }
        public DbSet<usertype> usertypes { get; set; }
    }
}
