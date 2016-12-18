

namespace Buying_Guide.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class WORKING_HOURS
    {
        public int SHOP_ID { get; set; }
        public int DAY_OF_WEEK_ID { get; set; }
        public string OPEN_TIME { get; set; }
        public string CLOSE_TIME { get; set; }
        public int ID { get; set; }
    
        public virtual SHOP SHOP { get; set; }
        public virtual WEEK_DAYS WEEK_DAYS { get; set; }
    }
}
