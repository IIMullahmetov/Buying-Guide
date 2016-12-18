

namespace Buying_Guide.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class SPECIALIZATION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPECIALIZATION()
        {
            this.PRODUCTS = new HashSet<PRODUCTS>();
            this.SHOP_SPECIALIZATION = new HashSet<SHOP_SPECIALIZATION>();
        }
    
        public int ID { get; set; }
        public string SPECIALIZATION1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTS> PRODUCTS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SHOP_SPECIALIZATION> SHOP_SPECIALIZATION { get; set; }
    }
}
