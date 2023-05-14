namespace LaptopShopMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHANVIEN")]
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            DONHANGs = new HashSet<DONHANG>();
            TAIKHOANQUANTRIs = new HashSet<TAIKHOANQUANTRI>();
            PHIEUNHAPHANGs = new HashSet<PHIEUNHAPHANG>();
        }

        [Key]
        public int MANHANVIEN { get; set; }

        [Required]
        [StringLength(30)]
        public string TENNHANVIEN { get; set; }

        [StringLength(256)]
        public string HINH { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYSINH { get; set; }

        [StringLength(100)]
        public string CCCD { get; set; }

        [StringLength(50)]
        public string CHUCVU { get; set; }

        [StringLength(100)]
        public string DIACHI { get; set; }

        [StringLength(100)]
        public string EMAIL { get; set; }

        [StringLength(13)]
        public string SDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANG> DONHANGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAIKHOANQUANTRI> TAIKHOANQUANTRIs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAPHANG> PHIEUNHAPHANGs { get; set; }
    }
}
