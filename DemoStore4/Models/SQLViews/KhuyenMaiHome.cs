namespace DemoStore4.Models.SQLViews
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhuyenMaiHome")]
    public partial class KhuyenMaiHome
    {
        [Key]
        [Column(Order = 0)]
        public int MaSP { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaLSP { get; set; }

        [Key]
        [Column(Order = 2)]
        public string TenSP { get; set; }

        [Key]
        [Column(Order = 3)]
        public double DonGia { get; set; }

        [Key]
        [Column(Order = 4)]
        public double KhuyenMai { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(255)]
        public string MoTa { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(255)]
        public string HinhAnh { get; set; }

        [Key]
        [Column(Order = 7)]
        public double GiaSauGiam { get; set; }
    }
}
