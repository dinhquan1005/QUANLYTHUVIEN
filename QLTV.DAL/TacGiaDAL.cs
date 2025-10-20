using QLTV.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace QLTV.DAL
{
    public class TacGiaDAL
    {
        public List<TacGia> LayDanhSach()
        {
            using (var db = new LibraryModel()) { return db.TacGia.OrderBy(t => t.MaTacGia).ToList(); }
        }

        public TacGia LayTheoId(int maTG)
        {
            using (var db = new LibraryModel()) { return db.TacGia.Find(maTG); }
        }

        public void Them(TacGia tg)
        {
            using (var db = new LibraryModel())
            {
                db.TacGia.Add(tg);
                db.SaveChanges();
            }
        }

        public void Sua(TacGia tg)
        {
            using (var db = new LibraryModel())
            {
                db.Entry(tg).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Xoa(int maTG)
        {
            using (var db = new LibraryModel())
            {
                var tg = db.TacGia.Find(maTG);
                if (tg != null)
                {
                    db.TacGia.Remove(tg);
                    db.SaveChanges();
                }
            }
        }
    }
}