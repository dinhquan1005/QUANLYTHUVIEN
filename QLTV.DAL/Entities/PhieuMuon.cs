namespace QLTV.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuMuon")]
    public partial class PhieuMuon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuMuon()
        {
            ChiTietPhieuMuon = new HashSet<ChiTietPhieuMuon>();
        }

        [Key]
        public int MaPhieuMuon { get; set; }

        [Required]
        [StringLength(10)]
        public string MaDocGia { get; set; }

        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayMuon { get; set; }

        [Column(TypeName = "date")]
        public DateTime HanTra { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuMuon> ChiTietPhieuMuon { get; set; }

        public virtual DocGia DocGia { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
