using QLTV.BUS;
using QLTV.DAL.Entities;
using QLTV.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QLTV.GUI
{
    public partial class frmBook : Form
    {
        private readonly SachBUS _bus = new SachBUS();
        private SachView _current;

        public frmBook()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgvSach.DataSource = _bus.LayDanhSachSach();
        }

        private void dgvSach_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSach.SelectedRows.Count > 0)
            {
                _current = dgvSach.SelectedRows[0].DataBoundItem as SachView;
                if (_current != null)
                {
                    txtMaSach.Text = _current.MaSach ?? "";
                    txtTenSach.Text = _current.TenSach ?? "";
                    txtNamXB.Text = _current.NamXuatBan?.ToString() ?? "";
                    txtMaTL.Text = _current.MaTheLoai?.ToString() ?? "";
                    txtMNXB.Text = _current.MaNXB?.ToString() ?? "";
                    txtMaTG.Text = _current.MaTacGia?.ToString() ?? "";
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maSach = txtMaSach.Text.Trim();
            string tenSach = txtTenSach.Text.Trim();

            if (string.IsNullOrWhiteSpace(maSach) || string.IsNullOrWhiteSpace(tenSach))
            {
                MessageBox.Show("Vui lòng nhập Mã sách và Tên sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🔍 Kiểm tra TRƯỚC khi tạo đối tượng: Mã sách đã tồn tại chưa?
            try
            {
                // Gọi BUS để kiểm tra tồn tại (bạn cần thêm phương thức này — xem bên dưới)
                if (_bus.KiemTraTonTai(maSach))
                {
                    MessageBox.Show("Mã sách đã tồn tại! Vui lòng nhập mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kiểm tra mã sách:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Chuyển đổi các trường số
            int? namXB = ParseInt(txtNamXB.Text);
            int? maTL = ParseInt(txtMaTL.Text);
            int? maTG = ParseInt(txtMaTG.Text);
            int? maNXB = ParseInt(txtMNXB.Text);

            var s = new Sach
            {
                MaSach = maSach,
                TenSach = tenSach,
                NamXuatBan = namXB,
                MaTheLoai = maTL,
                MaTacGia = maTG,
                MaNXB = maNXB,
                SoLuong = 0,
                MoTa = ""
            };

            try
            {
                if (_bus.ThemSach(s))
                {
                    MessageBox.Show("Thêm sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearFields();
                }
                else
                {
                    // Trường hợp dự phòng: nếu ThemSach trả về false
                    MessageBox.Show("Thêm thất bại! Mã sách đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống:\n{ex.InnerException?.Message ?? ex.Message}", "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method
        private int? ParseInt(string input)
        {
            return string.IsNullOrWhiteSpace(input) ? (int?)null : int.Parse(input);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_current == null)
            {
                MessageBox.Show("Vui lòng chọn sách cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chuyển đổi từ SachView sang Sach
            var sachToUpdate = _bus.ConvertToSach(_current);

            // Cập nhật các trường từ TextBox
            sachToUpdate.TenSach = txtTenSach.Text.Trim();

            int? namXB = ParseInt(txtNamXB.Text);
            if (namXB.HasValue) sachToUpdate.NamXuatBan = namXB;

            int? maTL = ParseInt(txtMaTL.Text);
            sachToUpdate.MaTheLoai = maTL;

            int? maNXB = ParseInt(txtMNXB.Text);
            sachToUpdate.MaNXB = maNXB;

            int? maTG = ParseInt(txtMaTG.Text);
            sachToUpdate.MaTacGia = maTG;

            try
            {
                if (_bus.SuaSach(sachToUpdate))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi CSDL:\n{ex.InnerException?.Message ?? ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_current == null)
            {
                MessageBox.Show("Vui lòng chọn sách cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Xác nhận xóa sách '{_current.TenSach}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_bus.XoaSach(_current.MaSach))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Không thể xóa! Sách đang được mượn hoặc không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("Dữ liệu đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearFields();

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTim.Text.Trim();
            dgvSach.DataSource = string.IsNullOrEmpty(keyword)
                ? _bus.LayDanhSachSach()
                : _bus.TimKiemSach(keyword);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTim.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                // Nếu không nhập gì → tải lại toàn bộ
                LoadData();
                return;
            }

            try
            {
                var ketQua = _bus.TimKiemSach(keyword);
                if (ketQua == null || ketQua.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy sách phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvSach.DataSource = new List<Sach>(); // Xóa trắng DataGridView
                }
                else
                {
                    dgvSach.DataSource = ketQua;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtMaSach.Clear();
            txtTenSach.Clear();
            txtNamXB.Clear();
            txtMaTL.Clear();
            txtMNXB.Clear();
            txtMaTG.Clear();
            _current = null;
        }
    }
}