// QLTV.BLL\NguoiDungBUS.cs
using QLTV.DAL;
using QLTV.DAL.Entities;

namespace QLTV.BUS
{
    public class NguoiDungBUS
    {
        private readonly NguoiDungDAL dal = new NguoiDungDAL();

        public NguoiDung DangNhap(string user, string pass)
        {
            return dal.DangNhap(user, pass);
        }

        public bool Them(NguoiDung nd)
        {
            return dal.Them(nd);
        }
    }
}