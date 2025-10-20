// QLTV.DAL\SachDAL.cs
using QLTV.DAL.Entities;
using QLTV.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace QLTV.DAL
{
    public class SachDAL
    {
        // QLTV.DAL\SachDAL.cs
        public List<SachView> LayTatCaSach()
        {
            using (var db = new LibraryModel())
            {
                return db.Sach
                          .Select(s => new SachView
                          {
                              MaSach = s.MaSach,
                              TenSach = s.TenSach,
                              NamXuatBan = s.NamXuatBan,
                              MaTheLoai = s.MaTheLoai,
                              MaTacGia = s.MaTacGia,
                              MaNXB = s.MaNXB,
                              SoLuong = s.SoLuong
                          })
                          .ToList();
            }
        }
        public bool KiemTraTonTai(string maSach)
        {
            using (var db = new LibraryModel())
            {
                return db.Sach.Any(s => s.MaSach == maSach);
            }
        }
        public List<SachView> TimKiemSach(string keyword)
        {
            using (var db = new LibraryModel())
            {
                return db.Sach
                          .Where(s => s.MaSach.Contains(keyword) || s.TenSach.Contains(keyword))
                          .Select(s => new SachView
                          {
                              MaSach = s.MaSach,
                              TenSach = s.TenSach,
                              NamXuatBan = s.NamXuatBan,
                              MaTheLoai = s.MaTheLoai,
                              MaTacGia = s.MaTacGia,
                              MaNXB = s.MaNXB,
                              SoLuong = s.SoLuong
                          })
                          .ToList();
            }
        }

        public Sach LayTheoId(string maSach)
        {
            using (var db = new LibraryModel())
            {
                // Find() là cách nhanh nhất để tìm một đối tượng bằng khóa chính
                return db.Sach.Find(maSach);
            }
        }
        // ---------------------------------------------
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
        // Thêm phương thức này vào lớp SachDAL của bạn
        public List<SachThongKeDTO> ThongKeSachMuonNhieuNhat()
        {
            using (var db = new LibraryModel())
            {
                var result = db.ChiTietPhieuMuon
                               .GroupBy(ct => ct.Sach) // Nhóm theo đối tượng Sách
                               .Select(group => new SachThongKeDTO
                               {
                                   MaSach = group.Key.MaSach,
                                   TenSach = group.Key.TenSach,
                                   SoLuotMuon = group.Count() // Đếm số lần xuất hiện
                               })
                               .OrderByDescending(s => s.SoLuotMuon) // Sắp xếp giảm dần
                               .ToList();
                return result;
            }
        }
        // Thêm phương thức này vào lớp SachDAL của bạn
        public List<SachThongKeDTO> ThongKeSachMuonTrongThang(int month, int year)
        {
            using (var db = new LibraryModel())
            {
                var result = db.ChiTietPhieuMuon
                               .Where(ct => ct.PhieuMuon.NgayMuon.Month == month && ct.PhieuMuon.NgayMuon.Year == year)
                               .GroupBy(ct => ct.Sach)
                               .Select(group => new SachThongKeDTO
                               {
                                   MaSach = group.Key.MaSach,
                                   TenSach = group.Key.TenSach,
                                   SoLuotMuon = group.Count()
                               })
                               .OrderByDescending(s => s.SoLuotMuon)
                               .ToList();
                return result;
            }
        }

        // Thêm/Cập nhật phương thức này trong lớp SachDAL
        public List<Sach> TimKiem(string keyword, string searchType)
        {
            using (var db = new LibraryModel())
            {
                IQueryable<Sach> query = db.Sach.Include(s => s.TheLoai)
                                                .Include(s => s.TacGia);

                if (searchType == "MaSach")
                {
                    query = query.Where(s => s.MaSach.Contains(keyword));
                }
                else // TenSach
                {
                    query = query.Where(s => s.TenSach.Contains(keyword));
                }
                return query.ToList();
            }
        }
    }
}