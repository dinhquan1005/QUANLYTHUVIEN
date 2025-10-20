// QLTV.GUI\frmEmployee.cs
using System;
using System.Windows.Forms;
using QLTV.BUS;
using QLTV.DAL.Entities;

namespace QLTV.GUI
{
    public partial class frmEmployee : Form
    {
        private readonly NhanVienBUS busNhanVien = new NhanVienBUS();

        public frmEmployee()
        {
            InitializeComponent();
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboBox();

            // === PHẦN CODE PHÂN QUYỀN MỚI ===
            if (Program.CurrentUser != null &&
                !Program.CurrentUser.VaiTro.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                // Nếu người dùng không phải là Admin, vô hiệu hóa các nút chức năng
                DisableFeaturesForNonAdmin();
            }
            else
            {
                // Nếu là Admin, đặt trạng thái nút như bình thường
                SetButtonAndFieldState(true); // Trạng thái ban đầu: chỉ xem
            }
        }

        // Hàm mới để vô hiệu hóa chức năng cho người dùng không phải Admin
        private void DisableFeaturesForNonAdmin()
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            // btnHuy.Enabled = false; // Nếu có nút Hủy

            // Có thể khóa toàn bộ groupbox nhập liệu để chỉ cho phép xem
            groupBox1.Enabled = false;
        }

        private void LoadData()
        {
            dgvNhanVien.DataSource = busNhanVien.LayDanhSachNhanVien();
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVien.ReadOnly = true;
            dgvNhanVien.MultiSelect = false;

            dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
            // Sửa lại tên cột cho đúng với thuộc tính trong lớp NhanVien
            dgvNhanVien.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
        }

        private void LoadComboBox()
        {
            cbbGioiTinh.Items.Clear();
            cbbGioiTinh.Items.Add("Nam");
            cbbGioiTinh.Items.Add("Nữ");
            cbbGioiTinh.Items.Add("Khác");
        }

        private void SetButtonAndFieldState(bool viewState)
        {
            btnThem.Enabled = viewState;
            btnSua.Enabled = viewState;
            btnXoa.Enabled = viewState;
            btnLuu.Enabled = !viewState;
            // btnHuy.Enabled = !viewState;

            txtMaNV.ReadOnly = viewState;
            // Sửa lại tên control cho đúng
            txtTenNV.ReadOnly = viewState;
            dtpNgaySinh.Enabled = !viewState;
            cbbGioiTinh.Enabled = !viewState;
            txtDiaChi.ReadOnly = viewState;
            txtSDT.ReadOnly = viewState;
        }

        private void ClearFields()
        {
            txtMaNV.Clear();
            txtTenNV.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            cbbGioiTinh.SelectedIndex = -1;
            txtDiaChi.Clear();
            txtSDT.Clear();
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                var selectedEmployee = dgvNhanVien.SelectedRows[0].DataBoundItem as NhanVien;
                if (selectedEmployee != null)
                {
                    txtMaNV.Text = selectedEmployee.MaNV;
                    txtTenNV.Text = selectedEmployee.HoTen; // Sửa lại tên thuộc tính
                    dtpNgaySinh.Value = selectedEmployee.NgaySinh ?? DateTime.Now;
                    cbbGioiTinh.SelectedItem = selectedEmployee.GioiTinh;
                    txtDiaChi.Text = selectedEmployee.DiaChi;
                    txtSDT.Text = selectedEmployee.SoDienThoai;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearFields();
            SetButtonAndFieldState(false);
            txtMaNV.ReadOnly = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để sửa.", "Thông báo");
                return;
            }
            SetButtonAndFieldState(false);
            txtMaNV.ReadOnly = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // ... (code của bạn giữ nguyên)
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text) || string.IsNullOrWhiteSpace(txtTenNV.Text))
            {
                MessageBox.Show("Mã và Tên nhân viên không được để trống!", "Cảnh báo");
                return;
            }

            try
            {
                if (txtMaNV.ReadOnly == false) // THÊM MỚI
                {
                    var newUser = new NguoiDung
                    {
                        TenDangNhap = txtMaNV.Text.Trim(),
                        MatKhau = "123456",
                        HoTen = txtTenNV.Text.Trim(),
                        VaiTro = "NhanVien", // Vai trò mặc định khi tạo nhân viên mới
                        TrangThai = true
                    };

                    var newEmployee = new NhanVien
                    {
                        MaNV = txtMaNV.Text.Trim(),
                        HoTen = txtTenNV.Text.Trim(), // Sửa lại tên thuộc tính
                        NgaySinh = dtpNgaySinh.Value.Date,
                        GioiTinh = cbbGioiTinh.SelectedItem?.ToString(),
                        DiaChi = txtDiaChi.Text.Trim(),
                        SoDienThoai = txtSDT.Text.Trim()
                    };

                    busNhanVien.ThemNhanVienVaTaiKhoan(newEmployee, newUser);
                }
                else // SỬA
                {
                    var employeeToUpdate = new NhanVien
                    {
                        MaNV = txtMaNV.Text.Trim(),
                        HoTen = txtTenNV.Text.Trim(), // Sửa lại tên thuộc tính
                        NgaySinh = dtpNgaySinh.Value.Date,
                        GioiTinh = cbbGioiTinh.SelectedItem?.ToString(),
                        DiaChi = txtDiaChi.Text.Trim(),
                        SoDienThoai = txtSDT.Text.Trim()
                    };
                    busNhanVien.SuaNhanVien(employeeToUpdate);
                }

                LoadData();
                SetButtonAndFieldState(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}