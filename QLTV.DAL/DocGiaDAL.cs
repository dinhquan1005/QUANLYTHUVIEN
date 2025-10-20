using QLTV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity; // Thêm dòng này
using System.Linq;

namespace QLTV.DAL
{
    public class DocGiaDAL
    {
        public List<DocGia> LayTatCa()
        {
            using (var db = new LibraryModel())
            {
                return db.DocGia.ToList();
            }
        }

        public DocGia LayTheoId(string maDocGia)
        {
            using (var db = new LibraryModel())
            {
                return db.DocGia.Find(maDocGia);
            }
        }

        public void Them(DocGia dg)
        {
            using (var db = new LibraryModel())
            {
                db.DocGia.Add(dg);
                db.SaveChanges();
            }
        }

        public void Sua(DocGia dg)
        {
            using (var db = new LibraryModel())
            {
                db.Entry(dg).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Xoa(string maDocGia)
        {
            using (var db = new LibraryModel())
            {
                var dg = db.DocGia.Find(maDocGia);
                if (dg != null)
                {
                    db.DocGia.Remove(dg);
                    db.SaveChanges();
                }
            }
        }
        // Thêm phương thức này vào lớp DocGiaDAL của bạn
        public List<DocGiaQuaHanDTO> ThongKeDocGiaQuaHan()
        {
            using (var db = new LibraryModel())
            {
                DateTime today = DateTime.Now.Date;
                var result = db.ChiTietPhieuMuon
                               .Where(ct => ct.NgayTraThucTe == null && DbFunctions.TruncateTime(ct.PhieuMuon.HanTra) < today)
                               .Select(ct => new DocGiaQuaHanDTO
                               {
                                   MaDocGia = ct.PhieuMuon.DocGia.MaDocGia,
                                   HoTen = ct.PhieuMuon.DocGia.HoTen,
                                   MaSach = ct.MaSach,
                                   TenSach = ct.Sach.TenSach,
                                   NgayHenTra = ct.PhieuMuon.HanTra,
                                   SoNgayQuaHan = (int)DbFunctions.DiffDays(ct.PhieuMuon.HanTra, today)
                               })
                               .OrderBy(dg => dg.NgayHenTra) // Sắp xếp theo hạn trả cũ nhất
                               .ToList();
                return result;
            }
        }
        // Thêm phương thức này vào lớp DocGiaDAL của bạn
        public List<DocGiaThongKeDTO> ThongKeDocGiaMuonNhieuNhat()
        {
            using (var db = new LibraryModel())
            {
                var result = db.PhieuMuon
                               .GroupBy(p => p.DocGia) // Nhóm theo đối tượng Độc giả
                               .Select(group => new DocGiaThongKeDTO
                               {
                                   MaDocGia = group.Key.MaDocGia,
                                   HoTen = group.Key.HoTen,
                                   // Sum() số lượng sách trong ChiTietPhieuMuon của mỗi phiếu
                                   SoSachDaMuon = group.Sum(p => p.ChiTietPhieuMuon.Count())
                               })
                               .OrderByDescending(dg => dg.SoSachDaMuon) // Sắp xếp giảm dần
                               .ToList();
                return result;
            }
        }
        // Thêm phương thức này vào lớp DocGiaDAL
        public List<DocGia> TimKiem(string keyword, string searchType)
        {
            using (var db = new LibraryModel())
            {
                IQueryable<DocGia> query = db.DocGia;

                if (searchType == "MaDocGia")
                {
                    query = query.Where(dg => dg.MaDocGia.Contains(keyword));
                }
                else // HoTen
                {
                    query = query.Where(dg => dg.HoTen.Contains(keyword));
                }
                return query.ToList();
            }
        }
    }
}