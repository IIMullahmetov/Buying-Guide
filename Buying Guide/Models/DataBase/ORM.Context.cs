
namespace Buying_Guide.Models.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
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
        public virtual DbSet<HOURS> HOURS { get; set; }
        public virtual DbSet<SPECIALIZATION_VIEW> SPECIALIZATION_VIEW { get; set; }
    
        public virtual int insertSpec(Nullable<int> shopId, Nullable<int> specId)
        {
            var shopIdParameter = shopId.HasValue ?
                new ObjectParameter("shopId", shopId) :
                new ObjectParameter("shopId", typeof(int));
    
            var specIdParameter = specId.HasValue ?
                new ObjectParameter("specId", specId) :
                new ObjectParameter("specId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("insertSpec", shopIdParameter, specIdParameter);
        }
    
        public virtual int insertWorkHours(Nullable<int> dayId, Nullable<int> shopId, string open, string close)
        {
            var dayIdParameter = dayId.HasValue ?
                new ObjectParameter("dayId", dayId) :
                new ObjectParameter("dayId", typeof(int));
    
            var shopIdParameter = shopId.HasValue ?
                new ObjectParameter("shopId", shopId) :
                new ObjectParameter("shopId", typeof(int));
    
            var openParameter = open != null ?
                new ObjectParameter("open", open) :
                new ObjectParameter("open", typeof(string));
    
            var closeParameter = close != null ?
                new ObjectParameter("close", close) :
                new ObjectParameter("close", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("insertWorkHours", dayIdParameter, shopIdParameter, openParameter, closeParameter);
        }
    }
}
