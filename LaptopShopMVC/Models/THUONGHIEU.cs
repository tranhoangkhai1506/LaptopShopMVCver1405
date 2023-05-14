namespace LaptopShopMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("THUONGHIEU")]
    public partial class THUONGHIEU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public THUONGHIEU()
        {
            SANPHAMs = new HashSet<SANPHAM>();
        }

        [Key]
        public int MATHUONGHIEU { get; set; }

        [Required]
        [StringLength(50)]
        public string TENTHUONGHIEU { get; set; }

        [StringLength(50)]
        public string XUATXU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
