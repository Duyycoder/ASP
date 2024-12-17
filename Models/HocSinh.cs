namespace TX2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HocSinh")]
    public partial class HocSinh
    {
        [Key]
        [StringLength(10)]
        public string sbd { get; set; }

        [StringLength(50)]
        public string hoten { get; set; }

        [StringLength(50)]
        public string anhduthi { get; set; }

        public int? malop { get; set; }

        public double? diemthi { get; set; }

        public virtual LopHoc LopHoc { get; set; }
    }
}
