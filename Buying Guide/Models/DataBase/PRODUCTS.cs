
namespace Buying_Guide.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUCTS
    {
        public int ID { get; set; }
        public string PRODUCT { get; set; }
        public string DESCRIPTION { get; set; }
        public int SHOP_ID { get; set; }
        public decimal PRICE { get; set; }
        public string IMAGE { get; set; }
        public bool IS_EXISTS { get; set; }
        public int COUNT { get; set; }
        public int CATEGORY { get; set; }
    
        public virtual SPECIALIZATION SPECIALIZATION { get; set; }
        public virtual SHOP SHOP { get; set; }
    }
}
