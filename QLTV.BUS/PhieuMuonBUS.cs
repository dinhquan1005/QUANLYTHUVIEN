// QLTV.BUS\PhieuMuonBUS.cs
using QLTV.DAL.Entities;
using System.Collections.Generic;

namespace QLTV.BUS
{
    public class PhieuMuonBUS
    {
        private readonly QLTV.DAL.PhieuMuonDAL _dal = new QLTV.DAL.PhieuMuonDAL();

        public List<PhieuMuon> LayDanhSachPhieu() => _dal.LayTatCa();
        public bool TaoPhieu(PhieuMuon phieu, List<string> dsMaSach) => _dal.TaoPhieu(phieu, dsMaSach);
        public bool TraSach(int maPhieu, string maSach) => _dal.TraSach(maPhieu, maSach);
    }
}