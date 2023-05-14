namespace LaptopShopMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHITIETPHIEUNHAPHANG")]
    public partial class CHITIETPHIEUNHAPHANG
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MAPHIEUNHAPHANG { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MASANPHAM { get; set; }

        public short? SOLUONGNHAP { get; set; }

        [Column(TypeName = "money")]
        public decimal? GIA { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYLAP { get; set; }

        public virtual PHIEUNHAPHANG PHIEUNHAPHANG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
