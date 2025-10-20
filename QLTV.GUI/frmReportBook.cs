// QLTV.GUI\frmReportBook.cs
using QLTV.BUS;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLTV.GUI
{
    public partial class frmReportBook : Form
    {
        private readonly SachBUS busSach = new SachBUS();

        public frmReportBook()
        {
            InitializeComponent();
        }

        private void frmReportBook_Load(object sender, EventArgs e)
        {
            // Thêm các tùy chọn thống kê
            cbbChon.Items.Add("Sách được mượn nhiều nhất");
            cbbChon.Items.Add("Tất cả sách");
            cbbChon.Items.Add("Sách mượn trong tháng");
            cbbChon.SelectedIndex = 0;

            // Cấu hình DateTimePicker để chỉ chọn tháng/năm
            dtpThangNam.Format = DateTimePickerFormat.Custom;
            dtpThangNam.CustomFormat = "MM/yyyy";
            dtpThangNam.ShowUpDown = true;
        }

        private void cbbChon_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ẩn/hiện DateTimePicker tùy theo lựa chọn
            if (cbbChon.SelectedItem.ToString() == "Sách mượn trong tháng")
            {
                dtpThangNam.Visible = true;
            }
            else
            {
                dtpThangNam.Visible = false;
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (cbbChon.SelectedItem == null) return;
            string reportType = cbbChon.SelectedItem.ToString();

            try
            {
                switch (reportType)
                {
                    case "Sách được mượn nhiều nhất":
                        // 1. Gán DataSource TRƯỚC
                        dgvSach.DataSource = busSach.ThongKeSachMuonNhieuNhat();
                        // 2. Gọi hàm Configure SAU
                        ConfigureDataGridViewForMostBorrowed();
                        break;

                    case "Tất cả sách":
                        // 1. Gán DataSource TRƯỚC
                        dgvSach.DataSource = busSach.LayDanhSachSach();
                        // 2. Gọi hàm Configure SAU
                        ConfigureDataGridViewForAllBooks();
                        break;
                    case "Sách mượn trong tháng":
                        int month = dtpThangNam.Value.Month;
                        int year = dtpThangNam.Value.Year;
                        dgvSach.DataSource = busSach.ThongKeSachMuonTrongThang(month, year);
                        ConfigureDataGridViewForMostBorrowed();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu thống kê: " + ex.Message, "Lỗi");
            }
        }

        private void ConfigureDataGridViewForMostBorrowed()
        {
            dgvSach.Columns["MaSach"].HeaderText = "Mã Sách";
            dgvSach.Columns["TenSach"].HeaderText = "Tên Sách";
            dgvSach.Columns["SoLuotMuon"].HeaderText = "Số Lượt Mượn";
            dgvSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigureDataGridViewForAllBooks()
        {
            // 1. Ngăn DataGridView tự động tạo cột từ DataSource
            dgvSach.AutoGenerateColumns = false;
            // 2. Xóa các cột đã có để tránh bị trùng lặp khi hàm được gọi lại
            dgvSach.Columns.Clear();

            // 3. Thêm các cột bạn muốn hiển thị theo cách thủ công

            // Cột Mã Sách
            dgvSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaSach",                // Tên kỹ thuật của cột
                HeaderText = "Mã Sách",         // Tên hiển thị trên giao diện
                DataPropertyName = "MaSach"     // Tên thuộc tính trong lớp Sach để lấy dữ liệu
            });

            // Cột Tên Sách
            dgvSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenSach",
                HeaderText = "Tên Sách",
                DataPropertyName = "TenSach"
            });

            // Cột Số Lượng
            dgvSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoLuong",
                HeaderText = "Số Lượng Tồn",
                DataPropertyName = "SoLuong"
            });

            // Cột Năm Xuất Bản
            dgvSach.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NamXuatBan",
                HeaderText = "Năm XB",
                DataPropertyName = "NamXuatBan"
            });

            // 4. Cấu hình các thuộc tính chung cho DataGridView để có trải nghiệm tốt hơn
            dgvSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSach.ReadOnly = true;
            dgvSach.MultiSelect = false;
            dgvSach.AllowUserToAddRows = false;
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (dgvSach.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
            saveFileDialog.Title = "Lưu file thống kê";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToCsv(dgvSach, saveFileDialog.FileName);
                MessageBox.Show("Xuất file thành công!", "Thông báo");
            }
        }

        private void ExportToCsv(DataGridView dgv, string filePath)
        {
            var sb = new StringBuilder();

            // Thêm header
            var headers = dgv.Columns.Cast<DataGridViewColumn>();
            sb.AppendLine(string.Join(",", headers.Select(column => $"\"{column.HeaderText}\"").ToArray()));

            // Thêm các dòng
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>();
                sb.AppendLine(string.Join(",", cells.Select(cell => $"\"{cell.Value}\"").ToArray()));
            }

            // Ghi file với encoding UTF-8 để hỗ trợ tiếng Việt
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}