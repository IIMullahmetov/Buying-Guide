﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Buying_Guide.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ORM : DbContext
    {
        public ORM()
            : base("name=ORM")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<OWN_FORMS> OWN_FORMS { get; set; }
        public virtual DbSet<PRODUCTS> PRODUCTS { get; set; }
        public virtual DbSet<SHOP> SHOP { get; set; }
        public virtual DbSet<SHOP_SPECIALIZATION> SHOP_SPECIALIZATION { get; set; }
        public virtual DbSet<SPECIALIZATION> SPECIALIZATION { get; set; }
        public virtual DbSet<WEEK_DAYS> WEEK_DAYS { get; set; }
        public virtual DbSet<WORKING_HOURS> WORKING_HOURS { get; set; }
    }
}