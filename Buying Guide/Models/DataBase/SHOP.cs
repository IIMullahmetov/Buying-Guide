
namespace Buying_Guide.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class SHOP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SHOP()
        {
            this.PRODUCTS = new HashSet<PRODUCTS>();
            this.WORKING_HOURS = new HashSet<WORKING_HOURS>();
            this.SHOP_SPECIALIZATION = new HashSet<SHOP_SPECIALIZATION>();
        }
    
        public int ID { get; set; }
        public string SHOP1 { get; set; }
        public string PHONE { get; set; }
        public string ADDRESS { get; set; }
        public int OWN_FORM_ID { get; set; }
        public string IMAGE { get; set; }
    
        public virtual OWN_FORMS OWN_FORMS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCTS> PRODUCTS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WORKING_HOURS> WORKING_HOURS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SHOP_SPECIALIZATION> SHOP_SPECIALIZATION { get; set; }
    }
}
