//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace THTMDT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TTBB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TTBB()
        {
            this.BaiBaos = new HashSet<BaiBao>();
        }

        public List<TTBB> ListPD { get; set; }
        public int MaTT { get; set; }
        public string TenTT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiBao> BaiBaos { get; set; }
    }
}