using QLTV.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace QLTV.DAL
{
    public class NhaXuatBanDAL
    {
        public List<NhaXuatBan> LayDanhSach()
        {
            using (var db = new LibraryModel()) { return db.NhaXuatBan.OrderBy(n => n.MaNXB).ToList(); }
        }

        public NhaXuatBan LayTheoId(int maNXB)
        {
            using (var db = new LibraryModel()) { return db.NhaXuatBan.Find(maNXB); }
        }

        public void Them(NhaXuatBan nxb)
        {
            using (var db = new LibraryModel())
            {
                db.NhaXuatBan.Add(nxb);
                db.SaveChanges();
            }
        }

        public void Sua(NhaXuatBan nxb)
        {
            using (var db = new LibraryModel())
            {
                db.Entry(nxb).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Xoa(int maNXB)
        {
            using (var db = new LibraryModel())
            {
                var nxb = db.NhaXuatBan.Find(maNXB);
                if (nxb != null)
                {
                    db.NhaXuatBan.Remove(nxb);
                    db.SaveChanges();
                }
            }
        }
    }
}