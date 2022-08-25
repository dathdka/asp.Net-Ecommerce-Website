namespace DemoStore4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_Voucher
    {
        [Key]
        [Column(Order = 0)]
        public string IDUser { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Mã khuyến mãi")]
        public int MaKM { get; set; }


        public bool DaSD { get; set; }

        public virtual Voucher Voucher { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa;
    }
}
