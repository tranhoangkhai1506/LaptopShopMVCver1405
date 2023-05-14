namespace LaptopShopMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEUBAOHANH")]
    public partial class PHIEUBAOHANH
    {
        [Key]
        public int MAPHIEUBAOHANH { get; set; }

        public int? MAKHACHHANG { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYBATDAU { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYKETTHUC { get; set; }

        public int? MASANPHAM { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
