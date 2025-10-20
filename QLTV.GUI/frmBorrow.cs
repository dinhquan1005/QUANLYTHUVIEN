// QLTV.GUI\frmBorrow.cs
using System;
using System.Windows.Forms;
using QLTV.BUS;
using QLTV.DAL.Entities;

namespace QLTV.GUI
{
    public partial class frmBorrow : Form
    {
        private readonly PhieuMuonBUS busPhieuMuon = new PhieuMuonBUS();
        private readonly ChiTietPhieuMuonBUS busChiTiet = new ChiTietPhieuMuonBUS();
        private readonly DocGiaBUS busDocGia = new DocGiaBUS();

        public frmBorrow()
        {
            InitializeComponent();
        }

        private void frmBorrow_Load(object sender, EventArgs e)
        {
            LoadPhieuMuonData();
            SetChiTietControlsEnabled(false);
            txtMaNV.ReadOnly = true;
            if (Program.CurrentUser != null)
            {
                txtMaNV.Text = Program.CurrentUser.TenDangNhap;
            }
            txtMaPhieu.ReadOnly = true;
            txtMaPM.ReadOnly = true;
        }

        #region Tab 1: Lập phiếu mượn
        private void LoadPhieuMuonData()
        {
            dgvPhieu.DataSource = busPhieuMuon.LayDanhSach();
            ConfigurePhieuMuonDGV();
        }

        private void ConfigurePhieuMuonDGV()
        {
            dgvPhieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhieu.ReadOnly = true;
            dgvPhieu.MultiSelect = false;

            dgvPhieu.Columns["MaPhieuMuon"].HeaderText = "Mã Phiếu";
            dgvPhieu.Columns["MaDocGia"].HeaderText = "Mã ĐG";
            dgvPhieu.Columns["TenDangNhap"].HeaderText = "Người Lập";
            dgvPhieu.Columns["NgayMuon"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPhieu.Columns["HanTra"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvPhieu.Columns["TrangThai"].HeaderText = "Trạng Thái";

            dgvPhieu.Columns["DocGia"].Visible = false;
            dgvPhieu.Columns["NguoiDung"].Visible = false;
            dgvPhieu.Columns["ChiTietPhieuMuon"].Visible = false;
        }

        private void dgvPhieu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhieu.SelectedRows.Count > 0)
            {
                var selectedPhieu = dgvPhieu.SelectedRows[0].DataBoundItem as PhieuMuon;
                if (selectedPhieu != null)
                {
                    txtMaPhieu.Text = selectedPhieu.MaPhieuMuon.ToString();
                    txtMaDG.Text = selectedPhieu.MaDocGia;
                    txtMaNV.Text = selectedPhieu.TenDangNhap;

                    LoadChiTietData(selectedPhieu.MaPhieuMuon);
                    SetChiTietControlsEnabled(true);
                }
            }
        }
        private void btnThemPhieuMuon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDG.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã độc giả.", "Thông báo");
                return;
            }

            try
            {
                // 1. Kiểm tra Mã độc giả có tồn tại không
                if (busDocGia.LayTheoId(txtMaDG.Text.Trim()) == null)
                {
                    MessageBox.Show("Mã độc giả không tồn tại!", "Lỗi");
                    return;
                }

                // 2. Tạo đối tượng phiếu mượn mới
                var phieuMoi = new PhieuMuon
                {
                    MaDocGia = txtMaDG.Text.Trim(),
                    TenDangNhap = Program.CurrentUser?.TenDangNhap,
                    NgayMuon = DateTime.Now.Date,
                    HanTra = DateTime.Now.Date.AddDays(14),
                    TrangThai = "Đang mượn"
                };

                // 3. Gọi BUS để thêm và nhận lại đối tượng đã có ID
                var pmVuaThem = busPhieuMuon.Them(phieuMoi);

                // 4. Nếu người dùng có nhập mã sách, thêm sách đó vào chi tiết phiếu
                if (!string.IsNullOrWhiteSpace(txtMS.Text) && pmVuaThem != null)
                {
                    var chiTiet = new ChiTietPhieuMuon
                    {
                        MaPhieuMuon = pmVuaThem.MaPhieuMuon, // Lấy ID chính xác từ đối tượng trả về
                        MaSach = txtMS.Text.Trim()
                    };
                    busChiTiet.Them(chiTiet);
                }

                // 5. Tải lại dữ liệu và thông báo thành công
                LoadPhieuMuonData(); // Sửa lại tên hàm cho đúng
                MessageBox.Show("Tạo phiếu mượn thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                // 6. Bắt và hiển thị lỗi chi tiết
                MessageBox.Show("Lỗi khi tạo phiếu mượn: " + ex.Message, "Lỗi");
            }
        }
        #endregion

        #region Tab 2: Chi tiết mượn/trả
        private void LoadChiTietData(int maPhieu)
        {
            txtMaPM.Text = maPhieu.ToString();
            dgvBorrow.DataSource = busChiTiet.LayTheoMaPhieu(maPhieu);
            ConfigureChiTietDGV();
        }

        private void ConfigureChiTietDGV()
        {
            dgvBorrow.AutoGenerateColumns = false;
            dgvBorrow.Columns.Clear();

            // Thêm các cột thủ công
            dgvBorrow.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaSach", HeaderText = "Mã Sách", DataPropertyName = "MaSach" });
            dgvBorrow.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenSach",
                HeaderText = "Tên Sách",
                ReadOnly = true
            }); dgvBorrow.Columns.Add(new DataGridViewTextBoxColumn { Name = "NgayTraThucTe", HeaderText = "Ngày Trả", DataPropertyName = "NgayTraThucTe", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });

            dgvBorrow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvBorrow_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBorrow.SelectedRows.Count > 0)
            {
                var selectedChiTiet = dgvBorrow.SelectedRows[0].DataBoundItem as ChiTietPhieuMuon;
                if (selectedChiTiet != null)
                {
                    txtMaSach.Text = selectedChiTiet.MaSach;
                    txtTinhTrang.Text = selectedChiTiet.GhiChu;

                    btnTra.Enabled = selectedChiTiet.NgayTraThucTe == null;
                }
            }
        }

        private void btnThemSach_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPM.Text) || string.IsNullOrWhiteSpace(txtMaSach.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã sách để thêm vào phiếu.", "Thông báo");
                return;
            }
            try
            {
                var chiTiet = new ChiTietPhieuMuon
                {
                    MaPhieuMuon = int.Parse(txtMaPM.Text),
                    MaSach = txtMaSach.Text.Trim(),
                    GhiChu = txtTinhTrang.Text
                };
                busChiTiet.Them(chiTiet);
                LoadChiTietData(chiTiet.MaPhieuMuon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sách: " + ex.Message, "Lỗi");
            }
        }

        // QLTV.GUI\frmBorrow.cs
        private void btnTra_Click(object sender, EventArgs e)
        {
            if (dgvBorrow.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sách để trả.", "Thông báo");
                return;
            }

            string maSach = dgvBorrow.SelectedRows[0].Cells["MaSach"].Value.ToString();
            int maPM = int.Parse(txtMaPM.Text);

            if (MessageBox.Show($"Xác nhận trả sách '{maSach}'?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    // Chỉ cần gọi một hàm duy nhất
                    busChiTiet.TraSach(maPM, maSach);

                    MessageBox.Show("Trả sách thành công!", "Thông báo");

                    // Tải lại dữ liệu cho cả hai DataGridView để thấy sự thay đổi
                    LoadChiTietData(maPM);    // Tải lại chi tiết phiếu (sẽ thấy ngày trả)
                    LoadPhieuMuonData();    // Tải lại phiếu chính (có thể thấy trạng thái đổi thành "Đã trả")
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi trả sách: " + ex.Message, "Lỗi");
                }
            }
        }

        private void SetChiTietControlsEnabled(bool isEnabled)
        {
            groupBox5.Enabled = isEnabled;
        }
        #endregion

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dgvBorrow_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var chiTiet = dgvBorrow.Rows[e.RowIndex].DataBoundItem as ChiTietPhieuMuon;
            if (chiTiet == null) return;

            string columnName = dgvBorrow.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "TenSach":
                    e.Value = chiTiet.Sach?.TenSach ?? "(Không có thông tin sách)";
                    e.FormattingApplied = true;
                    break;
            }
        }
    }
}