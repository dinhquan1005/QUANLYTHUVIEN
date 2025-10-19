using QLTV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLTV.DAL
{
    public class PhieuMuonDAL
    {
        /// <summary>
        /// Lấy danh sách tất cả phiếu mượn (có thông tin độc giả)
        /// </summary>
        public List<PhieuMuon> LayTatCa()
        {
            using (var db = new LibraryModel())
            {
                return db.PhieuMuon
                         .Include("DocGia")
                         .ToList();
            }
        }

        /// <summary>
        /// Tạo mới một phiếu mượn và các chi tiết mượn sách
        /// </summary>
        public bool TaoPhieu(PhieuMuon phieu, List<string> danhSachMaSach)
        {
            using (var db = new LibraryModel())
            {
                // Kiểm tra số lượng sách đủ không
                foreach (var maSach in danhSachMaSach)
                {
                    var sach = db.Sach.FirstOrDefault(s => s.MaSach == maSach);
                    if (sach == null || sach.SoLuong <= 0)
                        return false; // Sách không tồn tại hoặc hết hàng
                }

                // Thêm phiếu mượn
                db.PhieuMuon.Add(phieu);
                db.SaveChanges(); // Có MaPhieuMuon rồi

                // Thêm chi tiết và giảm số lượng sách
                foreach (var maSach in danhSachMaSach)
                {
                    db.ChiTietPhieuMuon.Add(new ChiTietPhieuMuon
                    {
                        MaPhieuMuon = phieu.MaPhieuMuon,
                        MaSach = maSach
                    });

                    // Giảm số lượng sách
                    var sach = db.Sach.First(s => s.MaSach == maSach);
                    sach.SoLuong--;
                }

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Xác nhận trả một cuốn sách trong phiếu mượn
        /// </summary>
        public bool TraSach(int maPhieu, string maSach)
        {
            using (var db = new LibraryModel())
            {
                var chiTiet = db.ChiTietPhieuMuon
                                .FirstOrDefault(ct => ct.MaPhieuMuon == maPhieu && ct.MaSach == maSach);
                if (chiTiet == null || chiTiet.NgayTraThucTe.HasValue)
                    return false; // Đã trả rồi hoặc không tồn tại

                // Cập nhật ngày trả
                chiTiet.NgayTraThucTe = DateTime.Now;

                // Tăng lại số lượng sách
                var sach = db.Sach.First(s => s.MaSach == maSach);
                sach.SoLuong++;

                // Cập nhật trạng thái phiếu: nếu tất cả đã trả → "Đã trả"
                var phieu = db.PhieuMuon.First(p => p.MaPhieuMuon == maPhieu);
                bool conSachChuaTra = db.ChiTietPhieuMuon
                                       .Any(ct => ct.MaPhieuMuon == maPhieu && !ct.NgayTraThucTe.HasValue);
                phieu.TrangThai = conSachChuaTra ? "Đang mượn" : "Đã trả";

                return db.SaveChanges() > 0;
            }
        }

        /// <summary>
        /// Lấy danh sách chi tiết của một phiếu mượn
        /// </summary>
        public List<ChiTietPhieuMuon> LayChiTietTheoPhieu(int maPhieu)
        {
            using (var db = new LibraryModel())
            {
                return db.ChiTietPhieuMuon
                         .Include("Sach")
                         .Where(ct => ct.MaPhieuMuon == maPhieu)
                         .ToList();
            }
        }
    }
}