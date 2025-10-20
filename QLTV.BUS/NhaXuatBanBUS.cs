using QLTV.DAL;
using QLTV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLTV.BUS
{
    public class NhaXuatBanBUS
    {
        private readonly NhaXuatBanDAL _dal = new NhaXuatBanDAL();

        public List<NhaXuatBan> LayDanhSach() => _dal.LayDanhSach();

        public void ThemNXB(NhaXuatBan nxb)
        {
            if (_dal.LayDanhSach().Any(n => n.TenNXB.Equals(nxb.TenNXB, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("Tên nhà xuất bản đã tồn tại!");
            }
            _dal.Them(nxb);
        }

        public void SuaNXB(NhaXuatBan nxb)
        {
            _dal.Sua(nxb);
        }

        public void XoaNXB(int maNXB)
        {
            _dal.Xoa(maNXB);
        }
    }
}