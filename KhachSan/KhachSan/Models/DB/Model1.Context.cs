﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KhachSan.Models.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KSEntities : DbContext
    {
        public KSEntities()
            : base("name=KSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_booking> tbl_booking { get; set; }
        public virtual DbSet<tbl_booking_status> tbl_booking_status { get; set; }
        public virtual DbSet<tbl_payment> tbl_payment { get; set; }
        public virtual DbSet<tbl_payment_type> tbl_payment_type { get; set; }
        public virtual DbSet<tbl_room> tbl_room { get; set; }
        public virtual DbSet<tbl_room_type> tbl_room_type { get; set; }
        public virtual DbSet<tbl_user> tbl_user { get; set; }
        public virtual DbSet<tbl_user_level> tbl_user_level { get; set; }
    }
}
