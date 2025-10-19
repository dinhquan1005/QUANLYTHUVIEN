// QLTV.DAL\Entities\MuonTra.cs
namespace QLTV.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PhieuMuon")] // ← Lưu ý: tên bảng là "PhieuMuon", KHÔNG phải "MuonTra"
    public partial class MuonTra // ← Bạn đặt tên class là "MuonTra", nhưng CSDL là "PhieuMuon"
    {
        public MuonTra()
        {
            ChiTietPhieuMuon = new HashSet<ChiTietPhieuMuon>();
        }

        [Key]
        public int MaPhieuMuon { get; set; }

        [Required, StringLength(10)]
        public string MaDocGia { get; set; }

        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayMuon { get; set; }

        [Column(TypeName = "date")]
        public DateTime HanTra { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        public virtual DocGia DocGia { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        public virtual ICollection<ChiTietPhieuMuon> ChiTietPhieuMuon { get; set; }
    }
}