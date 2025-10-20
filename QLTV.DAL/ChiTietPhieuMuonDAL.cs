using QLTV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace QLTV.DAL
{
    public class ChiTietPhieuMuonDAL
    {
        public List<ChiTietPhieuMuon> LayTheoMaPhieu(int maPM)
        {
            using (var db = new LibraryModel())
            {
                return db.ChiTietPhieuMuon
                         .Include(ct => ct.Sach)     
                         .Include(ct => ct.PhieuMuon)  
                         .Where(ct => ct.MaPhieuMuon == maPM)
                         .ToList();
            }
        }

        public void Them(ChiTietPhieuMuon ct)
        {
            using (var db = new LibraryModel())
            {
                db.ChiTietPhieuMuon.Add(ct);
                db.SaveChanges();
            }
        }

        public void TraSach(int maPhieuMuon, string maSach)
        {
            // Sử dụng một DbContext duy nhất cho tất cả các thao tác
            using (var db = new LibraryModel())
            {
                // 1. Tìm chi tiết phiếu mượn cần trả
                var chiTiet = db.ChiTietPhieuMuon.Find(maPhieuMuon, maSach);
                if (chiTiet == null || chiTiet.NgayTraThucTe != null)
                {
                    throw new Exception("Sách này không có trong phiếu mượn hoặc đã được trả từ trước.");
                }

                // 2. Cập nhật ngày trả thực tế cho cuốn sách
                chiTiet.NgayTraThucTe = DateTime.Now;

                // 3. Tìm và tăng lại số lượng của cuốn sách trong kho
                var sach = db.Sach.Find(maSach);
                if (sach != null)
                {
                    sach.SoLuong++;
                }

                // 4. Kiểm tra xem còn cuốn sách nào trong phiếu chưa được trả không
                bool conSachChuaTra = db.ChiTietPhieuMuon
                                         .Any(ct => ct.MaPhieuMuon == maPhieuMuon && ct.NgayTraThucTe == null);

                // 5. Nếu không còn sách nào chưa trả, cập nhật trạng thái của phiếu mượn chính
                if (!conSachChuaTra)
                {
                    var phieuMuon = db.PhieuMuon.Find(maPhieuMuon);
                    if (phieuMuon != null)
                    {
                        phieuMuon.TrangThai = "Đã trả";
                    }
                }

                // 6. Lưu tất cả các thay đổi trên vào CSDL trong một lần duy nhất
                db.SaveChanges();
            }
        }
    }
}