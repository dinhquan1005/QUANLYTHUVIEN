// QLTV.GUI\frmMain.cs
using System;
using System.Linq;
using System.Windows.Forms;

namespace QLTV.GUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = false; // Tắt chế độ MDI để dùng Panel hiệu quả

            // Hiển thị thông tin người dùng đã đăng nhập
            // Giả sử bạn có một Label tên là lblUserInfo trên form
            if (Program.CurrentUser != null)
            {
                // lblUserInfo.Text = $"Chào mừng: {Program.CurrentUser.HoTen} ({Program.CurrentUser.VaiTro})";
            }
        }

        #region Xử lý sự kiện chung (Đăng xuất & Thoát)

        // Sự kiện này có thể gán cho nút hoặc menu item "Đăng xuất"
        private void mnuLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Application.Restart() là cách an toàn và sạch sẽ nhất để đăng xuất.
                // Nó sẽ đóng hoàn toàn ứng dụng hiện tại và khởi động lại một tiến trình mới,
                // bắt đầu lại từ frmLogin.
                Application.Restart();
            }
        }

        // Sự kiện này có thể gán cho nút hoặc menu item "Thoát"
        private void mnuExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Đảm bảo ứng dụng thoát hoàn toàn khi form Main bị đóng (nhấn nút X)
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Hàm quản lý việc mở Form con

        /// <summary>
        /// Phương thức chung và hiệu quả để mở một form con bên trong Panel.
        /// Chỉ tạo form một lần duy nhất và tái sử dụng cho các lần sau.
        /// </summary>
        /// <typeparam name="T">Kiểu của Form cần mở (ví dụ: frmBook)</typeparam>
        private void OpenChildForm<T>() where T : Form, new()
        {
            // 1. Tìm trong pnlContent xem form đã được tạo và thêm vào chưa
            var form = pnlContent.Controls.OfType<T>().FirstOrDefault();

            if (form == null)
            {
                // 2. Nếu chưa có, tạo mới một lần duy nhất
                form = new T
                {
                    TopLevel = false, // Không phải là cửa sổ cấp cao nhất
                    FormBorderStyle = FormBorderStyle.None, // Bỏ viền cửa sổ
                    Dock = DockStyle.Fill // Lấp đầy Panel
                };
                pnlContent.Controls.Add(form); // Thêm vào Panel
                pnlContent.Tag = form;
                form.Show();
                form.BringToFront(); // Đưa form lên trên cùng
            }
            else
            {
                // 3. Nếu đã có, chỉ cần đưa nó lên trên cùng
                form.BringToFront();
            }
        }

        #endregion

        #region Sự kiện Click các Menu Item để mở Form tương ứng

        private void mnuBookManagement_Click(object sender, EventArgs e)
        {
            OpenChildForm<frmBook>();
        }

        private void mnuReaderManagement_Click(object sender, EventArgs e)
        {
            OpenChildForm<frmReader>();
        }

        private void mnuBorrowReturn_Click(object sender, EventArgs e)
        {
            OpenChildForm<frmBorrow>();
        }

        private void mnuEmployeeManagement_Click(object sender, EventArgs e)
        {
            OpenChildForm<frmEmployee>();
        }



        private void mnuPublisherManagement_Click(object sender, EventArgs e)
        {
            OpenChildForm<frmPublisher>();
        }

        private void mnuWriterManagement_Click(object sender, EventArgs e)
        {
            // Giả sử bạn có form tên frmWriter
            // OpenChildForm<frmWriter>();
        }

        private void mnuTypeManagement_Click(object sender, EventArgs e)
        {
            // Giả sử bạn có form tên frmType
            // OpenChildForm<frmType>();
        }

        private void mnuSearch_Click(object sender, EventArgs e)
        {
            OpenChildForm<frmSearch>();
        }

        private void mnuReportBook_Click(object sender, EventArgs e)
        {
            OpenChildForm<frmReportBook>();
        }

        private void mnuReportReader_Click(object sender, EventArgs e)
        {
            OpenChildForm<frmReportReader>();
        }

        private void mnuRules_Click(object sender, EventArgs e)
        {
            OpenChildForm<frmRules>();
        }

        #endregion
    }
}