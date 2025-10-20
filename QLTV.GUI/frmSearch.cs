// QLTV.GUI\frmSearch.cs
using System;
using System.Windows.Forms;
using QLTV.BUS;

namespace QLTV.GUI
{
    public partial class frmSearch : Form
    {
        private readonly SachBUS busSach = new SachBUS();
        private readonly DocGiaBUS busDocGia = new DocGiaBUS();

        public frmSearch()
        {
            InitializeComponent();
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            // Thiết lập trạng thái ban đầu cho các radio button
            rdbMaSach.Checked = true;
            rdbMaDG.Checked = true;
        }

        private void btnTimSach_Click(object sender, EventArgs e)
        {
            string keyword = txtTimSach.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                ConfigureDgvForSach();
            }

            string searchType = rdbMaSach.Checked ? "MaSach" : "TenSach";
            var result = busSach.TimKiemSach(keyword, searchType);

            dgvTable.DataSource = result;
            ConfigureDgvForSach(); // Cấu hình DataGridView để hiển thị Sách

            if (result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy sách phù hợp.");
            }
        }

        private void btnTimDG_Click(object sender, EventArgs e)
        {
            string keyword = txtTmDG.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                ConfigureDgvForDocGia();
            }

            string searchType = rdbMaDG.Checked ? "MaDocGia" : "HoTen";
            var result = busDocGia.TimKiemDocGia(keyword, searchType);

            dgvTable.DataSource = result;
            ConfigureDgvForDocGia(); // Cấu hình DataGridView để hiển thị Độc giả

            if (result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy độc giả phù hợp.");
            }
        }

        private void ConfigureDgvForSach()
        {
            dgvTable.AutoGenerateColumns = false;
            dgvTable.Columns.Clear();

            dgvTable.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã Sách", DataPropertyName = "MaSach" });
            dgvTable.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên Sách", DataPropertyName = "TenSach" });
            dgvTable.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Số Lượng", DataPropertyName = "SoLuong" });
            dgvTable.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Năm XB", DataPropertyName = "NamXuatBan" });

            dgvTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigureDgvForDocGia()
        {
            dgvTable.AutoGenerateColumns = false;
            dgvTable.Columns.Clear();

            dgvTable.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã Độc Giả", DataPropertyName = "MaDocGia" });
            dgvTable.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Họ Tên", DataPropertyName = "HoTen" });
            dgvTable.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Ngày Sinh", DataPropertyName = "NgaySinh", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } });
            dgvTable.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Giới Tính", DataPropertyName = "GioiTinh" });
            dgvTable.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email" });

            dgvTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}