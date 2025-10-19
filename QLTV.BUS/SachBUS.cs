// QLTV.BUS\SachBUS.cs
using QLTV.DAL;
using QLTV.DAL.Entities;
using System.Collections.Generic;

namespace QLTV.BUS
{
    public class SachBUS
    {
        private readonly QLTV.DAL.SachDAL _dal = new QLTV.DAL.SachDAL();

        public List<SachView> LayDanhSachSach()
        {
            return new SachDAL().LayTatCa();
        }
        public bool ThemSach(Sach sach) => _dal.Them(sach);
        public bool SuaSach(Sach sach) => _dal.Sua(sach);
        public bool XoaSach(string maSach) => _dal.Xoa(maSach);
    }
}