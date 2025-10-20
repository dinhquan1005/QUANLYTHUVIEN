using QLTV.DAL;
using QLTV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLTV.BUS
{
    public class TheLoaiBUS
    {
        private readonly TheLoaiDAL _dal = new TheLoaiDAL();

        public List<TheLoai> LayDanhSach() => _dal.LayDanhSach();

        public void ThemTheLoai(TheLoai tl)
        {
            // Kiểm tra tên thể loại không được trùng
            if (_dal.LayDanhSach().Any(t => t.TenTheLoai.Equals(tl.TenTheLoai, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("Tên thể loại đã tồn tại!");
            }
            _dal.Them(tl);
        }

        public void SuaTheLoai(TheLoai tl)
        {
            _dal.Sua(tl);
        }

        public void XoaTheLoai(int maTL)
        {
            _dal.Xoa(maTL);
        }
    }
}