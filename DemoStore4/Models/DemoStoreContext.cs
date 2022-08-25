namespace DemoStore4.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DemoStoreContext : DbContext
    {
        public DemoStoreContext()
            : base("name=DemoStoreContext")
        {
        }

        public virtual DbSet<CT_DDH> CT_DDH { get; set; }
        public virtual DbSet<CT_Voucher> CT_Voucher { get; set; }
        public virtual DbSet<DonDH> DonDHs { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<LoaiSP> LoaiSPs { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonDH>()
                .HasMany(e => e.CT_DDH)
                .WithRequired(e => e.DonDH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiSP>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.LoaiSP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.CT_DDH)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.GioHangs)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Voucher>()
                .HasMany(e => e.CT_Voucher)
                .WithRequired(e => e.Voucher)
                .WillCascadeOnDelete(false);
        }
    }
}
