namespace DemoStore4.Models.SQLViews
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DemoStoreView : DbContext
    {
        public DemoStoreView()
            : base("name=DemoStoreView")
        {
        }

        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<KhuyenMaiHome> KhuyenMaiHomes { get; set; }
        public virtual DbSet<ViewSanPham> ViewSanPhams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KhuyenMai>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<KhuyenMaiHome>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<ViewSanPham>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);
        }
    }
}
