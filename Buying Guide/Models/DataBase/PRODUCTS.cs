//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

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
        public int COUNT { get; set; }
    
        public virtual SHOP SHOP { get; set; }
    }
}
