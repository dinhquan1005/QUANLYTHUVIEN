// QLTV.GUI\frmWriter.cs
using System;
using System.Windows.Forms;
using QLTV.BUS;
using QLTV.DAL.Entities;

namespace QLTV.GUI
{
    public partial class frmWriter : Form
    {
        private readonly TacGiaBUS busTacGia = new TacGiaBUS();
        private bool isAdding = false;

        public frmWriter()
        {
            InitializeComponent();
        }

        private void frmWriter_Load(object sender, EventArgs e)
        {
            LoadData();
            SetButtonAndFieldState(true);
        }

        private void LoadData()
        {
            dgvTacGia.DataSource = busTacGia.LayDanhSach();
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dgvTacGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTacGia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTacGia.ReadOnly = true;
            dgvTacGia.MultiSelect = false;

            dgvTacGia.Columns["MaTacGia"].HeaderText = "Mã Tác Giả";
            dgvTacGia.Columns["TenTacGia"].HeaderText = "Tên Tác Giả";
            dgvTacGia.Columns["Sach"].Visible = false;
        }

        private void SetButtonAndFieldState(bool viewState)
        {
            btnThem.Enabled = viewState;
            btnSua.Enabled = viewState;
            btnXoa.Enabled = viewState;
            btnLuu.Enabled = !viewState;
            // btnHuy.Enabled = !viewState;

            txtTacGia.ReadOnly = true; // Mã tác giả luôn không cho sửa
            txtTenTG.ReadOnly = viewState;
        }

        private void ClearFields()
        {
            txtTacGia.Clear();
            txtTenTG.Clear();
        }

        private void dgvTacGia_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTacGia.SelectedRows.Count > 0)
            {
                var selectedWriter = dgvTacGia.SelectedRows[0].DataBoundItem as TacGia;
                if (selectedWriter != null)
                {
                    txtTacGia.Text = selectedWriter.MaTacGia.ToString();
                    txtTenTG.Text = selectedWriter.TenTacGia;
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
            if (dgvTacGia.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tác giả để sửa.", "Thông báo");
                return;
            }
            isAdding = false;
            SetButtonAndFieldState(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTacGia.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tác giả để xóa.", "Thông báo");
                return;
            }

            int maTG = int.Parse(txtTacGia.Text);
            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa tác giả '{txtTenTG.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    busTacGia.XoaTacGia(maTG);
                    LoadData();
                    ClearFields();
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi khi xóa: Không thể xóa tác giả đã có sách trong thư viện.", "Lỗi");
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenTG.Text))
            {
                MessageBox.Show("Tên tác giả không được để trống!", "Cảnh báo");
                return;
            }

            try
            {
                if (isAdding) // Trạng thái Thêm mới
                {
                    TacGia newWriter = new TacGia { TenTacGia = txtTenTG.Text.Trim() };
                    busTacGia.ThemTacGia(newWriter);
                }
                else // Trạng thái Sửa
                {
                    TacGia updatedWriter = new TacGia
                    {
                        MaTacGia = int.Parse(txtTacGia.Text),
                        TenTacGia = txtTenTG.Text.Trim()
                    };
                    busTacGia.SuaTacGia(updatedWriter);
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