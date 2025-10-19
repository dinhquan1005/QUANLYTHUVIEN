// Program.cs
using QLTV.DAL.Entities;
using QLTV.GUI;
using System;
using System.Windows.Forms;

namespace QLTV
{
    internal static class Program
    {
        public static NguoiDung CurrentUser { get; set; }
        [STAThread]
        static void Main()

        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Hiển thị form đăng nhập trước
            frmLogin loginForm = new frmLogin();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Nếu đăng nhập thành công → mở form chính
                Application.Run(new frmMain());
            }
            else
            {
                // Người dùng nhấn Thoát hoặc đóng form đăng nhập → thoát ứng dụng
                return;
            }
        }
    }
}