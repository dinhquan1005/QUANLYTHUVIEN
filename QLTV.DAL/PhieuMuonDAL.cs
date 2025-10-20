using QLTV.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace QLTV.DAL
{
    public class PhieuMuonDAL
    {
        public List<PhieuMuon> LayDanhSach()
        {
            using (var db = new LibraryModel())
            {
                return db.PhieuMuon.Include(p => p.DocGia).ToList();
            }
        }

        public PhieuMuon Them(PhieuMuon pm)
        {
            using (var db = new LibraryModel())
            {
                var newPm = db.PhieuMuon.Add(pm);
                db.SaveChanges();
                return newPm; // Trả về đối tượng vừa thêm
            }
        }

        public void Sua(PhieuMuon pm)
        {
            using (var db = new LibraryModel())
            {
                db.Entry(pm).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Xoa(int maPM)
        {
            using (var db = new LibraryModel())
            {
                var pm = db.PhieuMuon.Include(p => p.ChiTietPhieuMuon)
                                     .FirstOrDefault(p => p.MaPhieuMuon == maPM);
                if (pm != null)
                {
                    // Xóa các chi tiết phiếu mượn trước
                    db.ChiTietPhieuMuon.RemoveRange(pm.ChiTietPhieuMuon);
                    // Xóa phiếu mượn sau
                    db.PhieuMuon.Remove(pm);
                    db.SaveChanges();
                }
            }
        }
        public void CapNhatTrangThai(int maPhieu, string trangThaiMoi)
        {
            using (var db = new LibraryModel()) // ← KHAI BÁO DB Ở ĐÂY!
            {
                var phieu = db.PhieuMuon.FirstOrDefault(p => p.MaPhieuMuon == maPhieu);
                if (phieu != null)
                {
                    phieu.TrangThai = trangThaiMoi;
                    db.SaveChanges(); // ← SaveChanges() hợp lệ vì db là DbContext
                }
            } // ← using tự động dispose db
        }
    }
}