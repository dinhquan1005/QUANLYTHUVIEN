// QLTV.DAL\SachDAL.cs
using QLTV.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace QLTV.DAL
{
    public class SachDAL
    {
        public List<SachView> LayTatCa()
        {
            using (var db = new LibraryModel())
            {
                return db.Sach
                         .Select(s => new SachView
                         {
                             MaSach = s.MaSach,
                             TenSach = s.TenSach,
                             TacGia = s.TacGia != null ? s.TacGia.TenTacGia : "Chưa cập nhật",
                             TheLoai = s.TheLoai != null ? s.TheLoai.TenTheLoai : "Chưa cập nhật",
                             SoLuong = s.SoLuong,
                             NamXuatBan = s.NamXuatBan
                         })
                         .ToList();
            }
        }

        public bool Them(Sach sachMoi)
        {
            using (var db = new LibraryModel())
            {
                var sach = new Sach
                {
                    MaSach = sachMoi.MaSach,
                    TenSach = sachMoi.TenSach,
                    MaTheLoai = sachMoi.MaTheLoai,  
                    MaTacGia = sachMoi.MaTacGia,
                    MaNXB = sachMoi.MaNXB,
                    NamXuatBan = sachMoi.NamXuatBan,
                    SoLuong = sachMoi.SoLuong,
                    MoTa = sachMoi.MoTa
                };
                db.Sach.Add(sach);
                return db.SaveChanges() > 0;
            }
        }
        public bool Sua(Sach sach)
        {
            using (var db = new LibraryModel())
            {
                var existing = db.Sach.FirstOrDefault(s => s.MaSach == sach.MaSach);
                if (existing == null) return false;

                existing.TenSach = sach.TenSach;
                existing.MaTheLoai = sach.MaTheLoai;
                existing.MaTacGia = sach.MaTacGia;
                existing.MaNXB = sach.MaNXB;
                existing.NamXuatBan = sach.NamXuatBan;
                existing.SoLuong = sach.SoLuong;
                existing.MoTa = sach.MoTa;

                return db.SaveChanges() > 0;
            }
        }

        public bool Xoa(string maSach)
        {
            using (var db = new LibraryModel())
            {
                var sach = db.Sach.FirstOrDefault(s => s.MaSach == maSach);
                if (sach == null) return false;

                // Kiểm tra xem sách có đang được mượn không
                var dangMuon = db.ChiTietPhieuMuon
                                 .Any(ct => ct.MaSach == maSach && ct.NgayTraThucTe == null);
                if (dangMuon) return false;

                db.Sach.Remove(sach);
                return db.SaveChanges() > 0;
            }
        }
    }
}