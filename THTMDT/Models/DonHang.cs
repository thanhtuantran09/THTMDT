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
    
    public partial class DonHang
    {
        public int MaDH { get; set; }
        public Nullable<System.DateTime> NgayDat { get; set; }
        public Nullable<decimal> TongTien { get; set; }
        public Nullable<int> IDTrangThaiDH { get; set; }
        public Nullable<int> MaDoiTac { get; set; }
    
        public virtual DoiTac DoiTac { get; set; }
        public virtual TTDH TTDH { get; set; }
    }
}