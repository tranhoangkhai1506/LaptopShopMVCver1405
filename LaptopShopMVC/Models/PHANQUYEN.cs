namespace LaptopShopMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHANQUYEN")]
    public partial class PHANQUYEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHANQUYEN()
        {
            TAIKHOANQUANTRIs = new HashSet<TAIKHOANQUANTRI>();
        }

        [Key]
        public int MAPHANQUYEN { get; set; }

        [Required]
        [StringLength(30)]
        public string TENPHANQUYEN { get; set; }

        [Required]
        [StringLength(30)]
        public string QUYENHAN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAIKHOANQUANTRI> TAIKHOANQUANTRIs { get; set; }
    }
}
