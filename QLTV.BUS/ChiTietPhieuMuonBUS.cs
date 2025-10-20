// QLTV.BUS\ChiTietPhieuMuonBUS.cs
using QLTV.DAL;
using QLTV.DAL.Entities;
using System;
using System.Collections.Generic;

namespace QLTV.BUS
{
    public class ChiTietPhieuMuonBUS
    {
        private readonly ChiTietPhieuMuonDAL _dalCT = new ChiTietPhieuMuonDAL();
        private readonly SachDAL _dalSach = new SachDAL(); // Cần để kiểm tra và cập nhật sách

        public List<ChiTietPhieuMuon> LayTheoMaPhieu(int maPM)
        {
            return _dalCT.LayTheoMaPhieu(maPM);
        }

        public void Them(ChiTietPhieuMuon ct)
        {
            // Logic nghiệp vụ: Kiểm tra sách trước khi cho mượn
            var sach = _dalSach.LayTheoId(ct.MaSach);
            if (sach == null)
            {
                throw new Exception("Mã sách không tồn tại!");
            }
            if (sach.SoLuong <= 0)
            {
                throw new Exception("Sách đã hết, không thể mượn.");
            }

            // Giảm số lượng sách đi 1
            sach.SoLuong--;
            _dalSach.Sua(sach);

            // Thêm vào chi tiết phiếu mượn
            _dalCT.Them(ct);
        }

        public void TraSach(int maPM, string maSach)
        {
            // Tất cả logic nghiệp vụ đã được chuyển xuống DAL để đảm bảo tính toàn vẹn (transaction)
            _dalCT.TraSach(maPM, maSach);
        }
    }
}