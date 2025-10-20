// QLTV.GUI\frmType.cs
using System;
using System.Windows.Forms;
using QLTV.BUS;
using QLTV.DAL.Entities;

namespace QLTV.GUI
{
    public partial class frmType : Form
    {
        private readonly TheLoaiBUS busTheLoai = new TheLoaiBUS();
        private bool isAdding = false;

        public frmType()
        {
            InitializeComponent();
        }

        private void frmType_Load(object sender, EventArgs e)
        {
            LoadData();
            SetButtonAndFieldState(true);
        }

        private void LoadData()
        {
            dgvTheLoai.DataSource = busTheLoai.LayDanhSach();
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dgvTheLoai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTheLoai.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTheLoai.ReadOnly = true;
            dgvTheLoai.MultiSelect = false;

            dgvTheLoai.Columns["MaTheLoai"].HeaderText = "Mã Thể Loại";
            dgvTheLoai.Columns["TenTheLoai"].HeaderText = "Tên Thể Loại";
            dgvTheLoai.Columns["Sach"].Visible = false; // Ẩn cột danh sách Sách
        }

        private void SetButtonAndFieldState(bool viewState)
        {
            btnThem.Enabled = viewState;
            btnSua.Enabled = viewState;
            btnXoa.Enabled = viewState;
            btnLuu.Enabled = !viewState;
            // btnHuy.Enabled = !viewState; // Bật nếu có nút Hủy

            txtMaTL.ReadOnly = true; // Mã thể loại luôn không cho sửa
            txtTenTL.ReadOnly = viewState;
        }

        private void ClearFields()
        {
            txtMaTL.Clear();
            txtTenTL.Clear();
        }

        private void dgvTheLoai_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTheLoai.SelectedRows.Count > 0)
            {
                var selectedType = dgvTheLoai.SelectedRows[0].DataBoundItem as TheLoai;
                if (selectedType != null)
                {
                    txtMaTL.Text = selectedType.MaTheLoai.ToString();
                    txtTenTL.Text = selectedType.TenTheLoai;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            ClearFields();
            SetButtonAndFieldState(false);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvTheLoai.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn thể loại để sửa.", "Thông báo");
                return;
            }
            isAdding = false;
            SetButtonAndFieldState(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTheLoai.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn thể loại để xóa.", "Thông báo");
                return;
            }

            int maTL = int.Parse(txtMaTL.Text);
            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa thể loại '{txtTenTL.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    busTheLoai.XoaTheLoai(maTL);
                    LoadData();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: Không thể xóa thể loại đã được gán cho sách.", "Lỗi");
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenTL.Text))
            {
                MessageBox.Show("Tên thể loại không được để trống!", "Cảnh báo");
                return;
            }

            try
            {
                if (isAdding) // Trạng thái Thêm mới
                {
                    TheLoai newType = new TheLoai { TenTheLoai = txtTenTL.Text.Trim() };
                    busTheLoai.ThemTheLoai(newType);
                }
                else // Trạng thái Sửa
                {
                    TheLoai updatedType = new TheLoai
                    {
                        MaTheLoai = int.Parse(txtMaTL.Text),
                        TenTheLoai = txtTenTL.Text.Trim()
                    };
                    busTheLoai.SuaTheLoai(updatedType);
                }

                LoadData();
                SetButtonAndFieldState(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi");
            }
        }

        // Bạn cần thêm nút Hủy và gán sự kiện này cho nó
        private void btnHuy_Click(object sender, EventArgs e)
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