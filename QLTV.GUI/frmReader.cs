// QLTV.GUI\frmReader.cs
using System;
using System.Windows.Forms;
using QLTV.BUS;
using QLTV.DAL.Entities;

namespace QLTV.GUI
{
    public partial class frmReader : Form
    {
        private readonly DocGiaBUS busDocGia = new DocGiaBUS();

        public frmReader()
        {
            InitializeComponent();
        }

        private void frmReader_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComboBox();
            SetButtonAndFieldState(true);
        }

        private void LoadData()
        {
            dgvDocGia.DataSource = busDocGia.LayDanhSachDocGia();
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDocGia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDocGia.ReadOnly = true;
            dgvDocGia.MultiSelect = false;

            // Đặt tên cột và ẩn các cột không cần thiết
            dgvDocGia.Columns["MaDocGia"].HeaderText = "Mã ĐG";
            dgvDocGia.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvDocGia.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvDocGia.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvDocGia.Columns["PhieuMuon"].Visible = false;
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
            btnHuy.Enabled = !viewState;

            txtMaDG.ReadOnly = viewState;
            txtTenDG.ReadOnly = viewState;
            dtpNgaySinh.Enabled = !viewState;
            cbbGioiTinh.Enabled = !viewState;
            txtDiaChi.ReadOnly = viewState;
            txtEmail.ReadOnly = viewState;
        }

        private void ClearFields()
        {
            txtMaDG.Clear();
            txtTenDG.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            cbbGioiTinh.SelectedIndex = -1;
            txtDiaChi.Clear();
            txtEmail.Clear();
        }

        private void dgvDocGia_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDocGia.SelectedRows.Count > 0)
            {
                var selectedReader = dgvDocGia.SelectedRows[0].DataBoundItem as DocGia;
                if (selectedReader != null)
                {
                    txtMaDG.Text = selectedReader.MaDocGia;
                    txtTenDG.Text = selectedReader.HoTen;
                    dtpNgaySinh.Value = selectedReader.NgaySinh ?? DateTime.Now;
                    cbbGioiTinh.SelectedItem = selectedReader.GioiTinh;
                    txtDiaChi.Text = selectedReader.DiaChi;
                    txtEmail.Text = selectedReader.Email;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ClearFields();
            SetButtonAndFieldState(false);
            txtMaDG.ReadOnly = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn độc giả để sửa.", "Thông báo");
                return;
            }
            SetButtonAndFieldState(false);
            txtMaDG.ReadOnly = true; // Không cho phép sửa Mã
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn độc giả để xóa.", "Thông báo");
                return;
            }

            string maDG = txtMaDG.Text;
            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa độc giả '{maDG}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    busDocGia.XoaDocGia(maDG);
                    LoadData();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDG.Text) || string.IsNullOrWhiteSpace(txtTenDG.Text))
            {
                MessageBox.Show("Mã và Tên độc giả không được để trống!", "Cảnh báo");
                return;
            }

            try
            {
                DocGia dg = new DocGia
                {
                    MaDocGia = txtMaDG.Text.Trim(),
                    HoTen = txtTenDG.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value.Date,
                    GioiTinh = cbbGioiTinh.SelectedItem?.ToString(),
                    DiaChi = txtDiaChi.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };

                if (txtMaDG.ReadOnly == false) // Trạng thái Thêm mới
                {
                    busDocGia.ThemDocGia(dg);
                    LoadData();
                    ClearFields();
                }
                else // Trạng thái Sửa
                {
                    busDocGia.SuaDocGia(dg);
                    LoadData();
                    ClearFields();
                }

                LoadData();
                SetButtonAndFieldState(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi");
            }
        }

        private void btnHuy_Click(object sender, EventArgs e) // Bạn cần thêm nút này
        {
            ClearFields();
            SetButtonAndFieldState(true);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}