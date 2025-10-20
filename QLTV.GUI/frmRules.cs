// QLTV.GUI\frmRules.cs
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace QLTV.GUI
{
    public partial class frmRules : Form
    {
        public frmRules()
        {
            InitializeComponent();
            this.Load += frmRules_Load;
        }

        private void frmRules_Load(object sender, EventArgs e)
        {
            RichTextBox rtbRules = new RichTextBox();
            rtbRules.Dock = DockStyle.Fill;
            rtbRules.ReadOnly = true;
            rtbRules.BorderStyle = BorderStyle.None;
            rtbRules.Font = new Font("Segoe UI", 11f);
            this.Controls.Add(rtbRules);

            try
            {
                // Đường dẫn đến file text trong thư mục chạy chương trình
                string filePath = "NoiQuy.txt";

                if (File.Exists(filePath))
                {
                    // Đọc file với bảng mã UTF-8
                    rtbRules.Text = File.ReadAllText(filePath, Encoding.UTF8);
                }
                else
                {
                    rtbRules.Text = "Không tìm thấy file quy định (NoiQuy.txt).";
                }
            }
            catch (Exception ex)
            {
                rtbRules.Text = "Lỗi khi tải quy định: " + ex.Message;
            }
        }
    }
}