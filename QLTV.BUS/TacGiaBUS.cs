using QLTV.DAL;
using QLTV.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLTV.BUS
{
    public class TacGiaBUS
    {
        private readonly TacGiaDAL _dal = new TacGiaDAL();

        public List<TacGia> LayDanhSach() => _dal.LayDanhSach();

        public void ThemTacGia(TacGia tg)
        {
            if (_dal.LayDanhSach().Any(t => t.TenTacGia.Equals(tg.TenTacGia, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception("Tên tác giả đã tồn tại!");
            }
            _dal.Them(tg);
        }

        public void SuaTacGia(TacGia tg)
        {
            _dal.Sua(tg);
        }

        public void XoaTacGia(int maTG)
        {
            _dal.Xoa(maTG);
        }
    }
}