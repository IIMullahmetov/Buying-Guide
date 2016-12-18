
namespace Buying_Guide.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class SHOP_SPECIALIZATION
    {
        public int SHOP_ID { get; set; }
        public int SPECIALIZATION_ID { get; set; }
        public int ID { get; set; }
    
        public virtual SHOP SHOP { get; set; }
        public virtual SPECIALIZATION SPECIALIZATION { get; set; }
    }
}
