namespace LaptopShopMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEUNHAPHANG")]
    public partial class PHIEUNHAPHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIEUNHAPHANG()
        {
            CHITIETPHIEUNHAPHANGs = new HashSet<CHITIETPHIEUNHAPHANG>();
        }

        [Key]
        public int MAPHIEUNHAPHANG { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYNHAPHANG { get; set; }

        [Column(TypeName = "money")]
        public decimal? TONGTIEN { get; set; }

        public int? MANHANVIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETPHIEUNHAPHANG> CHITIETPHIEUNHAPHANGs { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
