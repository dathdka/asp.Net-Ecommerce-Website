namespace DemoStore4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonDH")]
    public partial class DonDH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonDH()
        {
            CT_DDH = new HashSet<CT_DDH>();
        }

        [Key]
        [Display(Name = "Mã đơn đặt hàng")]
        public int MaDDH { get; set; }

        [Required]
        [StringLength(128)]
        public string IDUser { get; set; }
        [Display(Name = "Thời gian đặt")]
        public DateTime ThoiGianDat { get; set; }
        [Display(Name = "Tổng tiền")]
        public double TongTien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_DDH> CT_DDH { get; set; }
    }
}
