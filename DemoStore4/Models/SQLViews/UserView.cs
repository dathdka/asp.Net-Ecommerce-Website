namespace DemoStore4.Models.SQLViews
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class UserView : DbContext
    {
        public UserView()
            : base("name=UserView")
        {
        }

        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
