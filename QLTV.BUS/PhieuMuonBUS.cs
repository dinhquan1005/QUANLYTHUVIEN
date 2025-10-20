// QLTV.BUS\PhieuMuonBUS.cs
using QLTV.DAL;
using QLTV.DAL.Entities;
using System;
using System.Collections.Generic;

namespace QLTV.BUS
{
    public class PhieuMuonBUS
    {
        private readonly PhieuMuonDAL _dal = new PhieuMuonDAL();

        public List<PhieuMuon> LayDanhSach()
        {
            return _dal.LayDanhSach();
        }

        public PhieuMuon Them(PhieuMuon pm)
        {
            // Có thể thêm các logic kiểm tra ở đây
            // Ví dụ: kiểm tra xem độc giả có bị phạt không...
            if (string.IsNullOrEmpty(pm.MaDocGia) || string.IsNullOrEmpty(pm.TenDangNhap))
            {
                throw new Exception("Mã độc giả và người lập phiếu không được để trống.");
            }
            return _dal.Them(pm);
        }

        public void Sua(PhieuMuon pm)
        {
            _dal.Sua(pm);
        }

        public void Xoa(int maPM)
        {
            _dal.Xoa(maPM);
        }
        public void CapNhatTrangThai(int maPhieu, string trangThaiMoi)
        {
            _dal.CapNhatTrangThai(maPhieu, trangThaiMoi);
        }
    }
}