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
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class BaiBao
    {
        public BaiBao()
        {
            HinhAnh = "~/Content/img/bao.png";
        }
        [NotMapped]
        public HttpPostedFileBase UploadImage1 { get; set; }
        public int MaBB { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<int> MaTG { get; set; }
        public Nullable<int> MaDM { get; set; }
        public Nullable<int> MaTT { get; set; }
    
        public virtual Danhmuc Danhmuc { get; set; }
        public virtual TacGia TacGia { get; set; }
        public virtual TTBB TTBB { get; set; }
    }
}