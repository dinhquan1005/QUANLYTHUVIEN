// QLTV.GUI\frmLogin.cs
using System;
using System.Windows.Forms;
using QLTV.BUS;        
using QLTV.DAL.Entities;

namespace QLTV.GUI
{
    public partial class frmLogin : Form
    {
        private readonly NguoiDungBUS _bus = new NguoiDungBUS();

        public frmLogin()
        {
            InitializeComponent();
            this.Text = "Đăng nhập hệ thống";
            this.AcceptButton = btnLogin;   // Enter = Đăng nhập
            this.CancelButton = btnExit;    // Esc = Thoát
            txtPassword.UseSystemPasswordChar = true;
        }

        // Trong frmLogin.cs - sự kiện btnLogin_Click
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus();
                return;
            }

            try
            {
                var user = _bus.DangNhap(username, password);
                if (user != null)
                {
                    Program.CurrentUser = user;
                    this.DialogResult = DialogResult.OK; // 👈 Quan trọng!
                    this.Close(); // Đóng form đăng nhập
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtLogin.SelectAll();
                    txtLogin.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Thoát: đặt DialogResult = Cancel
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}