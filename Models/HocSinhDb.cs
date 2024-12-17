using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TX2.Models
{
    public partial class HocSinhDb : DbContext
    {
        public HocSinhDb()
            : base("name=HocSinhDB")
        {
        }

        public virtual DbSet<HocSinh> HocSinh { get; set; }
        public virtual DbSet<LopHoc> LopHoc { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
