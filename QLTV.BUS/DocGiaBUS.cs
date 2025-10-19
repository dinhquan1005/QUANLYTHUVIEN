// QLTV.BUS\DocGiaBUS.cs
using QLTV.DAL.Entities;
using System.Collections.Generic;

namespace QLTV.BUS
{
    public class DocGiaBUS
    {
        private readonly QLTV.DAL.DocGiaDAL _dal = new QLTV.DAL.DocGiaDAL();

        public List<DocGia> LayDanhSachDocGia() => _dal.LayTatCa();
        public bool ThemDocGia(DocGia dg) => _dal.Them(dg);
        public bool XoaDocGia(string ma) => _dal.Xoa(ma);
    }
}