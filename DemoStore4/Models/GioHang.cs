namespace DemoStore4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GioHang")]
    public partial class GioHang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Mã sản phẩm")]
        public int MaSP { get; set; }

        [Key]
        [Column(Order = 1)]
        [Display(Name = "Mã người dùng")]
        public string Id { get; set; }

        [Display(Name = "Số lượng")]
        public int? SoLuongThem { get; set; }

        public virtual SanPham SanPham { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string TenSP;
        [Display(Name = "Đơn giá")]
        public double DonGia;
        [Display(Name = "Khuyến mãi")]
        public double KhuyenMai;
        [Display(Name = "Thành tiền")]
        public double GiaSauGiam;

        public string HinhAnh;

    }
}
