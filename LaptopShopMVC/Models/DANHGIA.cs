namespace LaptopShopMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DANHGIA")]
    public partial class DANHGIA
    {
        [Key]
        public int MADANHGIA { get; set; }

        public int? MAKHACHHANG { get; set; }

        public int? MASANPHAM { get; set; }

        public string NOIDUNG { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
