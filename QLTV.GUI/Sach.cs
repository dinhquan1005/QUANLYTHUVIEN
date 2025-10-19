namespace QLTV.GUI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            MuonTra = new HashSet<MuonTra>();
        }

        [Key]
        [StringLength(10)]
        public string MaSach { get; set; }

        [Required]
        [StringLength(200)]
        public string TenSach { get; set; }

        [Required]
        [StringLength(100)]
        public string TacGia { get; set; }

        [Required]
        [StringLength(10)]
        public string MaTheLoai { get; set; }

        public int? NamXuatBan { get; set; }

        [StringLength(100)]
        public string NhaXuatBan { get; set; }

        public int SoLuong { get; set; }

        [StringLength(500)]
        public string MoTa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MuonTra> MuonTra { get; set; }

        public virtual TheLoai TheLoai { get; set; }
    }
}
