using QLTV.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace QLTV.DAL
{
    public class NhanVienDAL
    {
        public List<NhanVien> LayDanhSach()
        {
            using (var db = new LibraryModel()) { return db.NhanVien.ToList(); }
        }

        public NhanVien LayTheoId(string maNV)
        {
            using (var db = new LibraryModel()) { return db.NhanVien.Find(maNV); }
        }

        public void Sua(NhanVien nv)
        {
            using (var db = new LibraryModel())
            {
                db.Entry(nv).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Xoa(string maNV)
        {
            using (var db = new LibraryModel())
            {
                var nv = db.NhanVien.Find(maNV);
                if (nv != null)
                {
                    db.NhanVien.Remove(nv);
                    db.SaveChanges();
                }
            }
        }

        // Phương thức mới để thêm đồng bộ
        public void ThemNhanVienVaTaiKhoan(NhanVien nv, NguoiDung nd)
        {
            using (var db = new LibraryModel())
            {
                // Thêm NguoiDung (tài khoản) trước
                db.NguoiDung.Add(nd);
                // Thêm NhanVien (hồ sơ) sau
                db.NhanVien.Add(nv);

                // Lưu cả hai thay đổi cùng lúc
                db.SaveChanges();
            }
        }
    }
}