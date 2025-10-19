namespace QLTV.GUI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MuonTra")]
    public partial class MuonTra
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string MaDocGia { get; set; }

        [Required]
        [StringLength(10)]
        public string MaSach { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayMuon { get; set; }

        [Column(TypeName = "date")]
        public DateTime HanTra { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayTra { get; set; }

        [StringLength(20)]
        public string TinhTrang { get; set; }

        [StringLength(255)]
        public string GhiChu { get; set; }

        public virtual DocGia DocGia { get; set; }

        public virtual Sach Sach { get; set; }
    }
}
