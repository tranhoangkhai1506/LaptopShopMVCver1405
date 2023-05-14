namespace LaptopShopMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SANPHAM")]
    public class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            CHITIETDONHANGs = new HashSet<CHITIETDONHANG>();
            CHITIETPHIEUNHAPHANGs = new HashSet<CHITIETPHIEUNHAPHANG>();
            DANHGIAs = new HashSet<DANHGIA>();
            PHIEUBAOHANHs = new HashSet<PHIEUBAOHANH>();
        }

        [Key]
        public int MASANPHAM { get; set; }

        [Required]
        [StringLength(100)]
        public string TENSANPHAM { get; set; }

        public string MOTA { get; set; }

        [StringLength(100)]
        public string HINH { get; set; }

        [StringLength(100)]
        public string RAM { get; set; }

        [StringLength(100)]
        public string ROM { get; set; }

        [StringLength(100)]
        public string CPU { get; set; }

        [StringLength(100)]
        public string CARDMANHINH { get; set; }

        [StringLength(256)]
        public string THOIGIANBAOHANH { get; set; }

        [Column(TypeName = "money")]
        public decimal GIABAN { get; set; }

        [Column(TypeName = "money")]
        public decimal? GIANHAP { get; set; }

        [StringLength(30)]
        public string TINHTRANG { get; set; }

        public int SOLUONG { get; set; }

        public int? MATHUONGHIEU { get; set; }

        public int? MADANHMUC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONHANG> CHITIETDONHANGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETPHIEUNHAPHANG> CHITIETPHIEUNHAPHANGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANHGIA> DANHGIAs { get; set; }

        public virtual DANHMUC DANHMUC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUBAOHANH> PHIEUBAOHANHs { get; set; }

        public virtual THUONGHIEU THUONGHIEU { get; set; }
    }
}
