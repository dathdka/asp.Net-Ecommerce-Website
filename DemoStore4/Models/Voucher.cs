namespace DemoStore4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Voucher")]
    public partial class Voucher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Voucher()
        {
            CT_Voucher = new HashSet<CT_Voucher>();
        }

        [Key]
        [Display(Name = "Mã khuyến mãi")]
        public int MaKM { get; set; }
        [Required(ErrorMessage = "Giá khuyến mãi không được để trống")]
        [Display(Name = "Giá khuyến mãi")]
        public double GiaKM { get; set; }
        [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày bắt đầu")]
        public DateTime NgayBD { get; set; }
        [Display(Name = "Ngày kết thúc")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
        public DateTime NgayKT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_Voucher> CT_Voucher { get; set; }
    }
}
