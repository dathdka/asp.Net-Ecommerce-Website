namespace DemoStore4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            CT_DDH = new HashSet<CT_DDH>();
            GioHangs = new HashSet<GioHang>();
        }

        [Key]
        public int MaSP { get; set; }
        [Required(ErrorMessage = "Mã loại sản phẩm không được để trống")]
        [Display(Name = "Mã loại sản phẩm")]
        public int MaLSP { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(128)]
        [Display(Name = "Tên sản phẩm")]
        public string TenSP { get; set; }
        [Required(ErrorMessage = "Đơn giá không được để trống")]
        [Display(Name = "Đơn giá")]
        public double DonGia { get; set; }
        [Required(ErrorMessage = "Số lượng sản phẩm không được để trống")]
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }
        [Required(ErrorMessage = "Khuyến mãi không được để trống")]
        [Display(Name = "Khuyến mãi")]
        public double KhuyenMai { get; set; }

        [Required(ErrorMessage = "Mô tả sản phẩm không được để trống")]
        [StringLength(255)]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        [Required(ErrorMessage = "Hình ảnh không được để trống")]
        [StringLength(255)]
        [Display(Name = "Hình Ảnh")]
        public string HinhAnh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_DDH> CT_DDH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GioHang> GioHangs { get; set; }

        public virtual LoaiSP LoaiSP { get; set; }

        public List<LoaiSP> ListLoaiSP = new List<LoaiSP>();


    }
}
