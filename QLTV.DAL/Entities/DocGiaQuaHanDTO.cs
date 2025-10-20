using System;

namespace QLTV.DAL.Entities
{
    public class DocGiaQuaHanDTO
    {
        public string MaDocGia { get; set; }
        public string HoTen { get; set; }
        public string MaSach { get; set; }
        public string TenSach { get; set; }
        public DateTime NgayHenTra { get; set; }
        public int SoNgayQuaHan { get; set; }
    }
}