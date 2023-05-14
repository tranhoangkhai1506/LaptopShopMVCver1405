namespace LaptopShopMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAIKHOANKHACHHANG")]
    public partial class TAIKHOANKHACHHANG
    {
        [Key]
        [StringLength(50)]
        public string TENDANGNHAP { get; set; }

        [Required]
        [StringLength(50)]
        public string MATKHAU { get; set; }

        public int? MAKHACHHANG { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
