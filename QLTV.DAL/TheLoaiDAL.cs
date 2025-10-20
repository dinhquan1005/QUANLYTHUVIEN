using QLTV.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace QLTV.DAL
{
    public class TheLoaiDAL
    {
        public List<TheLoai> LayDanhSach()
        {
            using (var db = new LibraryModel())
            {
                // Thêm .OrderBy() để sắp xếp kết quả trả về
                return db.TheLoai.OrderBy(tl => tl.MaTheLoai).ToList();
            }
        }

        public TheLoai LayTheoId(int maTL)
        {
            using (var db = new LibraryModel()) { return db.TheLoai.Find(maTL); }
        }

        public void Them(TheLoai tl)
        {
            using (var db = new LibraryModel())
            {
                db.TheLoai.Add(tl);
                db.SaveChanges();
            }
        }

        public void Sua(TheLoai tl)
        {
            using (var db = new LibraryModel())
            {
                db.Entry(tl).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Xoa(int maTL)
        {
            using (var db = new LibraryModel())
            {
                var tl = db.TheLoai.Find(maTL);
                if (tl != null)
                {
                    db.TheLoai.Remove(tl);
                    db.SaveChanges();
                }
            }
        }
    }
}