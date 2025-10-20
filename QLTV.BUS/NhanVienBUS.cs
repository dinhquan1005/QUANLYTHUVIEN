using QLTV.DAL;
using QLTV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLTV.BUS
{
    public class NhanVienBUS
    {
        private readonly NhanVienDAL _dalNV = new NhanVienDAL();
        private readonly NguoiDungDAL _dalND = new NguoiDungDAL();

        public List<NhanVien> LayDanhSachNhanVien() => _dalNV.LayDanhSach();

        public void SuaNhanVien(NhanVien nv)
        {
            _dalNV.Sua(nv);
        }

        public void XoaNhanVien(string ma)
        {
            _dalNV.Xoa(ma);
        }

        // Phương thức mới để thêm đồng bộ
        public void ThemNhanVienVaTaiKhoan(NhanVien nv, NguoiDung nd)
        {
            // ✅ KIỂM TRA QUAN TRỌNG NẰM Ở ĐÂY
            if (_dalND.LayTheoId(nd.TenDangNhap) != null)
            {
                // Nếu tài khoản đã tồn tại, ném ra lỗi để GUI bắt được
                throw new Exception("Tên đăng nhập (Mã nhân viên) đã tồn tại!");
            }

            // Nếu không trùng, mới gọi DAL để thêm
            _dalNV.ThemNhanVienVaTaiKhoan(nv, nd);
        }
    }
}