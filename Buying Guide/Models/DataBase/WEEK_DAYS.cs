

namespace Buying_Guide.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class WEEK_DAYS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WEEK_DAYS()
        {
            this.WORKING_HOURS = new HashSet<WORKING_HOURS>();
        }
    
        public int ID { get; set; }
        public string DAY_OF_WEEK { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WORKING_HOURS> WORKING_HOURS { get; set; }
    }
}
