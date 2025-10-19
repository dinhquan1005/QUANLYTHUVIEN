// QLTV.DAL\NguoiDungDAL.cs
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
        public bool Them(NguoiDung nd)
        {
            using (var db = new LibraryModel())
            {
                // Kiểm tra tài khoản đã tồn tại chưa
                if (db.NguoiDung.Any(x => x.TenDangNhap == nd.TenDangNhap))
                    return false;

                db.NguoiDung.Add(nd);
                return db.SaveChanges() > 0;
            }
        }
    }
}