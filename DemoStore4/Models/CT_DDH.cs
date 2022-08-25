namespace DemoStore4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_DDH
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Display(Name ="Mã đơn  đặt hàng")]
        public int MaDDH { get; set; }

        [Display(Name = "Số lượng")]
        public int SLuong { get; set; }

        [Display(Name = "Thời gian nhận hàng")]
        public DateTime? ThoiGianNhan { get; set; }

        public virtual DonDH DonDH { get; set; }

        public virtual SanPham SanPham { get; set; }

        public String TenSP;

        public string HinhAnh;
    }
}
