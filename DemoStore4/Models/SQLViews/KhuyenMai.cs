namespace DemoStore4.Models.SQLViews
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhuyenMai")]
    public partial class KhuyenMai
    {
        [Key]
        [Column(Order = 0)]
        public int MaSP { get; set; }

        [Key]
        [Column(Order = 1)]
        public string TenSP { get; set; }

        [Key]
        [Column(Order = 2)]
        public double DonGia { get; set; }

        [Key]
        [Column("KhuyenMai", Order = 3)]
        public double KhuyenMai1 { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string MoTa { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(255)]
        public string HinhAnh { get; set; }

        [Key]
        [Column(Order = 6)]
        public double GiaSauGiam { get; set; }

        public bool DaDangNhap = false;

        public bool DaThemVao = false;
    }
}
