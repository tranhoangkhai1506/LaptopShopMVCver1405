namespace LaptopShopMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAIKHOANQUANTRI")]
    public partial class TAIKHOANQUANTRI
    {
        [Key]
        [StringLength(50)]
        public string TENDANGNHAP { get; set; }

        [Required]
        [StringLength(50)]
        public string MATKHAU { get; set; }

        public int? MANHANVIEN { get; set; }

        public int? MAPHANQUYEN { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }

        public virtual PHANQUYEN PHANQUYEN { get; set; }
    }
}
