// QLTV.GUI\frmReportReader.cs
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QLTV.BUS;

namespace QLTV.GUI
{
    public partial class frmReportReader : Form
    {
        private readonly DocGiaBUS busDocGia = new DocGiaBUS();

        public frmReportReader()
        {
            InitializeComponent();
        }

        private void frmReportReader_Load(object sender, EventArgs e)
        {
            cbbChon.Items.Add("Độc giả mượn nhiều sách nhất");
            cbbChon.Items.Add("Danh sách tất cả độc giả");
            cbbChon.Items.Add("Độc giả có sách quá hạn"); // <-- Tùy chọn mới
            cbbChon.SelectedIndex = 0;
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (cbbChon.SelectedItem == null) return;
            string reportType = cbbChon.SelectedItem.ToString();

            try
            {
                switch (reportType)
                {
                    case "Độc giả mượn nhiều sách nhất":
                        dgvDocGia.DataSource = busDocGia.ThongKeDocGiaMuonNhieuNhat();
                        ConfigureDataGridViewForTopReaders();
                        break;

                    case "Danh sách tất cả độc giả":
                        dgvDocGia.DataSource = busDocGia.LayDanhSachDocGia();
                        ConfigureDataGridViewForAllReaders();
                        break;

                    case "Độc giả có sách quá hạn": // <-- Case mới
                        dgvDocGia.DataSource = busDocGia.ThongKeDocGiaQuaHan();
                        ConfigureDataGridViewForOverdueReaders();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu thống kê: " + ex.Message, "Lỗi");
            }
        }

        private void ConfigureDataGridViewForTopReaders()
        {
            dgvDocGia.Columns["MaDocGia"].HeaderText = "Mã Độc Giả";
            dgvDocGia.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvDocGia.Columns["SoSachDaMuon"].HeaderText = "Số Sách Đã Mượn";
            dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ConfigureDataGridViewForAllReaders()
        {
            dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDocGia.Columns["MaDocGia"].HeaderText = "Mã Độc Giả";
            dgvDocGia.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvDocGia.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvDocGia.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvDocGia.Columns["Email"].HeaderText = "Email";
            dgvDocGia.Columns["PhieuMuon"].Visible = false;
            if (dgvDocGia.Columns.Contains("SoDienThoai"))
            {
                dgvDocGia.Columns["SoDienThoai"].Visible = false;
            }
        }

        // Hàm cấu hình mới
        private void ConfigureDataGridViewForOverdueReaders()
        {
            dgvDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDocGia.Columns["MaDocGia"].HeaderText = "Mã Độc Giả";
            dgvDocGia.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvDocGia.Columns["MaSach"].HeaderText = "Mã Sách";
            dgvDocGia.Columns["TenSach"].HeaderText = "Tên Sách Quá Hạn";
            dgvDocGia.Columns["NgayHenTra"].HeaderText = "Ngày Hẹn Trả";
            dgvDocGia.Columns["SoNgayQuaHan"].HeaderText = "Số Ngày Quá Hạn";
            if (dgvDocGia.Columns.Contains("SoDienThoai"))
            {
                dgvDocGia.Columns["SoDienThoai"].Visible = false;
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (dgvDocGia.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
            saveFileDialog.Title = "Lưu file thống kê";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToCsv(dgvDocGia, saveFileDialog.FileName);
                MessageBox.Show("Xuất file thành công!", "Thông báo");
            }
        }

        private void ExportToCsv(DataGridView dgv, string filePath)
        {
            var sb = new StringBuilder();
            var headers = dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible);
            sb.AppendLine(string.Join(",", headers.Select(column => $"\"{column.HeaderText}\"").ToArray()));

            foreach (DataGridViewRow row in dgv.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>().Where(c => c.OwningColumn.Visible);
                sb.AppendLine(string.Join(",", cells.Select(cell => $"\"{cell.Value}\"").ToArray()));
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}