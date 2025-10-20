using QLTV.DAL;
using QLTV.DAL.Entities;
using System;
using System.Collections.Generic;

namespace QLTV.BUS
{
    public class DocGiaBUS
    {
        private readonly DocGiaDAL _dal = new DocGiaDAL();

        public List<DocGia> LayDanhSachDocGia() => _dal.LayTatCa();

        public void ThemDocGia(DocGia dg)
        {
            if (_dal.LayTheoId(dg.MaDocGia) != null)
            {
                throw new Exception("Mã độc giả đã tồn tại!");
            }
            _dal.Them(dg);
        }

        public void SuaDocGia(DocGia dg)
        {
            _dal.Sua(dg);
        }

        public void XoaDocGia(string ma)
        {
            _dal.Xoa(ma);
        }

        public DocGia LayTheoId(string maDocGia)
        {
            return _dal.LayTheoId(maDocGia);
        }

        // Thêm phương thức này vào lớp DocGiaBUS của bạn
        public List<DocGiaQuaHanDTO> ThongKeDocGiaQuaHan()
        {
            return _dal.ThongKeDocGiaQuaHan();
        }
        // Thêm phương thức này vào lớp DocGiaBUS của bạn
        public List<DocGiaThongKeDTO> ThongKeDocGiaMuonNhieuNhat()
        {
            return _dal.ThongKeDocGiaMuonNhieuNhat();
        }
        public List<DocGia> TimKiemDocGia(string keyword, string searchType)
        {
            return _dal.TimKiem(keyword, searchType);
        }
    }
}