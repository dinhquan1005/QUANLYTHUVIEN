// QLTV.BUS\SachBUS.cs
using QLTV.DAL;
using QLTV.DAL.Entities;
using QLTV.Entities;
using System.Collections.Generic;

namespace QLTV.BUS
{
    public class SachBUS
    {
        private readonly QLTV.DAL.SachDAL _dal = new QLTV.DAL.SachDAL();
        // QLTV.BUS\SachBUS.cs
        public SachBUS()
        {
        }
        public Sach ConvertToSach(SachView view)
        {
            return new Sach
            {
                MaSach = view.MaSach,
                TenSach = view.TenSach,
                NamXuatBan = view.NamXuatBan,
                MaTheLoai = view.MaTheLoai,
                MaTacGia = view.MaTacGia,
                MaNXB = view.MaNXB,
                SoLuong = view.SoLuong,
                MoTa = "" // hoặc giữ giá trị cũ nếu cần
            };
        }
        public bool KiemTraTonTai(string maSach)
        {
            return _dal.KiemTraTonTai(maSach);
        }
        public List<SachView> LayDanhSachSach() => _dal.LayTatCaSach();
        public List<SachView> TimKiemSach(string keyword) => _dal.TimKiemSach(keyword);
        public bool ThemSach(Sach sach) => _dal.Them(sach);
        public bool SuaSach(Sach sach) => _dal.Sua(sach);
        public bool XoaSach(string maSach) => _dal.Xoa(maSach);
        public List<SachThongKeDTO> ThongKeSachMuonNhieuNhat()
        {
            return _dal.ThongKeSachMuonNhieuNhat();
        }
        // Thêm phương thức này vào lớp SachBUS của bạn
        public List<SachThongKeDTO> ThongKeSachMuonTrongThang(int month, int year)
        {
            return _dal.ThongKeSachMuonTrongThang(month, year);
        }
        // Hàm tìm kiếm phải nhận 2 tham số: từ khóa và loại tìm kiếm
        public List<Sach> TimKiemSach(string keyword, string searchType)
        {
            return _dal.TimKiem(keyword, searchType);
        }
    }
}