// QLTV.GUI\frmRegister.cs
using System;
using System.Windows.Forms;
using QLTV.BUS;
using QLTV.DAL.Entities;

namespace QLTV.GUI
{
    public partial class frmRegister : Form
    {
        private readonly NguoiDungBUS busNguoiDung = new NguoiDungBUS();
        private readonly Form parentForm; // Biến để lưu lại form cha (frmLogin)

        // Chỉ sử dụng một hàm khởi tạo duy nhất, bắt buộc phải có form cha
        public frmRegister(Form parent)
        {
            InitializeComponent();
            this.parentForm = parent; // Lưu lại tham chiếu của form cha
            this.Text = "Đăng ký tài khoản";
            this.AcceptButton = btnRegister;
        }

        // QLTV.GUI\frmRegister.cs — chỉ cần giữ nguyên, nhưng đảm bảo dùng BUS mới
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            try
            {
                string user = txtUser.Text.Trim();
                string pass = txtPassword.Text;
                string confirmPass = txtconfirmPass.Text;

                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(confirmPass))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (pass != confirmPass)
                {
                    MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var nd = new NguoiDung
                {
                    TenDangNhap = user,
                    MatKhau = pass,
                    HoTen = "Người dùng mới", // Có thể thêm TextBox cho họ tên sau
                    VaiTro = "ThuThu",
                    TrangThai = true
                };

                if (busNguoiDung.Them(nd))
                {
                    MessageBox.Show("Đăng ký thành công! Vui lòng quay lại để đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đăng ký thất bại! Tài khoản đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            // Nút này chỉ có một nhiệm vụ là đóng form hiện tại
            this.Close();
        }

        private void frmRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Khi form này đóng, tự động hiển thị lại form cha (frmLogin) đã bị ẩn
            parentForm?.Show();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkShowPassword.Checked;
            txtPassword.UseSystemPasswordChar = !isChecked;
            txtconfirmPass.UseSystemPasswordChar = !isChecked;
        }
    }
}