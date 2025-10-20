// QLTV.GUI\frmPublisher.cs
using System;
using System.Windows.Forms;
using QLTV.BUS;
using QLTV.DAL.Entities;

namespace QLTV.GUI
{
    public partial class frmPublisher : Form
    {
        private readonly NhaXuatBanBUS busNXB = new NhaXuatBanBUS();
        private bool isAdding = false;

        public frmPublisher()
        {
            InitializeComponent();
        }

        private void frmPublisher_Load(object sender, EventArgs e)
        {
            LoadData();
            SetButtonAndFieldState(true);
        }

        private void LoadData()
        {
            dgvNXB.DataSource = busNXB.LayDanhSach();
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dgvNXB.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNXB.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNXB.ReadOnly = true;
            dgvNXB.MultiSelect = false;

            dgvNXB.Columns["MaNXB"].HeaderText = "Mã NXB";
            dgvNXB.Columns["TenNXB"].HeaderText = "Tên Nhà Xuất Bản";
            dgvNXB.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvNXB.Columns["Sach"].Visible = false;
            dgvNXB.Columns["SoDienThoai"].Visible = false;
        }

        private void SetButtonAndFieldState(bool viewState)
        {
            btnThem.Enabled = viewState;
            btnSua.Enabled = viewState;
            btnXoa.Enabled = viewState;
            btnLuu.Enabled = !viewState;
            // btnHuy.Enabled = !viewState;

            txtMNXB.ReadOnly = true; // Mã NXB luôn không cho sửa
            txtTenNXB.ReadOnly = viewState;
            txtDiaChi.ReadOnly = viewState;
        }

        private void ClearFields()
        {
            txtMNXB.Clear();
            txtTenNXB.Clear();
            txtDiaChi.Clear();
        }

        private void dgvNXB_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNXB.SelectedRows.Count > 0)
            {
                var selectedPublisher = dgvNXB.SelectedRows[0].DataBoundItem as NhaXuatBan;
                if (selectedPublisher != null)
                {
                    txtMNXB.Text = selectedPublisher.MaNXB.ToString();
                    txtTenNXB.Text = selectedPublisher.TenNXB;
                    txtDiaChi.Text = selectedPublisher.DiaChi;
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
            if (dgvNXB.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhà xuất bản để sửa.", "Thông báo");
                return;
            }
            isAdding = false;
            SetButtonAndFieldState(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNXB.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhà xuất bản để xóa.", "Thông báo");
                return;
            }

            int maNXB = int.Parse(txtMNXB.Text);
            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa NXB '{txtTenNXB.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    busNXB.XoaNXB(maNXB);
                    LoadData();
                    ClearFields();
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi khi xóa: Không thể xóa NXB đã có sách trong thư viện.", "Lỗi");
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenNXB.Text))
            {
                MessageBox.Show("Tên nhà xuất bản không được để trống!", "Cảnh báo");
                return;
            }

            try
            {
                if (isAdding) // Trạng thái Thêm mới
                {
                    NhaXuatBan newPublisher = new NhaXuatBan
                    {
                        TenNXB = txtTenNXB.Text.Trim(),
                        DiaChi = txtDiaChi.Text.Trim()
                    };
                    busNXB.ThemNXB(newPublisher);
                }
                else // Trạng thái Sửa
                {
                    NhaXuatBan updatedPublisher = new NhaXuatBan
                    {
                        MaNXB = int.Parse(txtMNXB.Text),
                        TenNXB = txtTenNXB.Text.Trim(),
                        DiaChi = txtDiaChi.Text.Trim()
                    };
                    busNXB.SuaNXB(updatedPublisher);
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