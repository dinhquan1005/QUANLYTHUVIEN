// QLTV.DAL\DocGiaDAL.cs
using QLTV.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace QLTV.DAL
{
    public class DocGiaDAL  // ← Phải là public class
    {
        public List<DocGia> LayTatCa()
        {
            using (var db = new LibraryModel())
            {
                return db.DocGia.ToList();
            }
        }

        public bool Them(DocGia dg)
        {
            using (var db = new LibraryModel())
            {
                db.DocGia.Add(dg);
                return db.SaveChanges() > 0;
            }
        }

        public bool Xoa(string maDocGia)
        {
            using (var db = new LibraryModel())
            {
                var dg = db.DocGia.FirstOrDefault(d => d.MaDocGia == maDocGia);
                if (dg == null) return false;
                db.DocGia.Remove(dg);
                return db.SaveChanges() > 0;
            }
        }
    }
}