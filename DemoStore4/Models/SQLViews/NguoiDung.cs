namespace DemoStore4.Models.SQLViews
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        public string Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public int? Points { get; set; }

        public double TongTien;

        public int MaKM;

        public bool DaNhap;

        public int Diem;
    }
}
