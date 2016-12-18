
namespace Buying_Guide.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class OWN_FORMS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OWN_FORMS()
        {
            this.SHOP = new HashSet<SHOP>();
        }
    
        public int ID { get; set; }
        public string OWN_FORMS1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SHOP> SHOP { get; set; }
    }
}
