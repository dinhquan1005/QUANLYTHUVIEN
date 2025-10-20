using QLTV.DAL.Entities;
using System.Linq;

namespace QLTV.DAL
{
    public class NguoiDungDAL
    {
        public NguoiDung DangNhap(string user, string pass)
        {
            using (var db = new LibraryModel())
            {
                return db.NguoiDung
                         .FirstOrDefault(x => x.TenDangNhap == user
                                           && x.MatKhau == pass
                                           && x.TrangThai == true);
            }
        }

        // --- PHƯƠNG THỨC BỊ THIẾU BẠN CẦN THÊM VÀO ---
        public NguoiDung LayTheoId(string tenDangNhap)
        {
            using (var db = new LibraryModel())
            {
                // Find() là cách nhanh nhất để tìm một đối tượng bằng khóa chính
                return db.NguoiDung.Find(tenDangNhap);
            }
        }
        // ---------------------------------------------

        public bool Them(NguoiDung nd)
        {
            using (var db = new LibraryModel())
            {
                if (db.NguoiDung.Any(x => x.TenDangNhap == nd.TenDangNhap))
                    return false;

                db.NguoiDung.Add(nd);
                return db.SaveChanges() > 0;
            }
        }
    }
}