namespace QLTV.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuMuon")]
    public partial class ChiTietPhieuMuon
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaPhieuMuon { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaSach { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTraThucTe { get; set; }

        public decimal? TienPhat { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }

        public virtual PhieuMuon PhieuMuon { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
