namespace LaptopShopMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KHACHHANG")]
    public partial class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            DANHGIAs = new HashSet<DANHGIA>();
            DONHANGs = new HashSet<DONHANG>();
            TAIKHOANKHACHHANGs = new HashSet<TAIKHOANKHACHHANG>();
            PHIEUBAOHANHs = new HashSet<PHIEUBAOHANH>();
        }

        [Key]
        public int MAKHACHHANG { get; set; }

        [Required]
        [StringLength(50)]
        public string TENKHACHHANG { get; set; }

        [StringLength(256)]
        public string EMAIL { get; set; }

        [StringLength(100)]
        public string CCCD { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYSINH { get; set; }

        [StringLength(100)]
        public string DIACHI { get; set; }

        [StringLength(13)]
        public string SDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DANHGIA> DANHGIAs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANG> DONHANGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAIKHOANKHACHHANG> TAIKHOANKHACHHANGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUBAOHANH> PHIEUBAOHANHs { get; set; }
    }
}
