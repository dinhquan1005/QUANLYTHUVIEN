// QLTV.DAL\SachView.cs
namespace QLTV.DAL
{
    public class SachView
    {
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public string TacGia { get; set; }     // ← string
        public string TheLoai { get; set; }    // ← string
        public int SoLuong { get; set; }
        public int? NamXuatBan { get; set; }
    }
}