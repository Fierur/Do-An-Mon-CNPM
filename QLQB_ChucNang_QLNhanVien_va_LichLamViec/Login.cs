using QLQB_ChucNang_QLNhanVien_va_LichLamViec.Database;
using System;
using System.Windows.Forms;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Focus vào textbox username
            txtUsername.Focus();

            // Test data cho developer (comment dòng này khi deploy)
             txtUsername.Text = "NV01";
             txtPassword.Text = "nva123";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformLogin();
                e.Handled = true;
            }
        }

        private void PerformLogin()
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Hiển thị loading
            this.Cursor = Cursors.WaitCursor;
            btnLogin.Enabled = false;
            btnLogin.Text = "Đang đăng nhập...";

            try
            {
                string errorMessage;
                string tenNV, maQuyen, tenQuyen;
                bool isQuanLy;

                // Kiểm tra đăng nhập
                bool loginSuccess = DatabaseConnection.TestLogin(
                    txtUsername.Text.Trim(),
                    txtPassword.Text,
                    out errorMessage,
                    out tenNV,
                    out maQuyen,
                    out tenQuyen,
                    out isQuanLy
                );

                if (loginSuccess)
                {
                    // Lưu thông tin session
                    SessionInfo.MaNV = txtUsername.Text.Trim();
                    SessionInfo.TenNV = tenNV;
                    SessionInfo.MaQuyen = maQuyen;
                    SessionInfo.TenQuyen = tenQuyen;
                    SessionInfo.IsAdmin = isQuanLy; // Kiểm tra theo Role_QuanLy


                    // Hiển thị thông báo thành công
                    //MessageBox.Show(
                    //    $"Đăng nhập thành công!\n\n" +
                    //    $"Xin chào: {tenNV}\n" +
                    //    $"Mã NV: {SessionInfo.MaNV}\n" +
                    //    $"Quyền: {tenQuyen}",
                    //    "Thành công",
                    //    MessageBoxButtons.OK,
                    //    MessageBoxIcon.Information
                    //);

                    this.Hide();
                    frmMain mainForm = new frmMain();
                    mainForm.FormClosed += (s, args) => this.Close();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show(errorMessage, "Đăng nhập thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi không xác định:\n{ex.Message}\n\n" +
                    $"Chi tiết: {ex.StackTrace}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnLogin.Enabled = true;
                btnLogin.Text = "ĐĂNG NHẬP";
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Bạn có chắc muốn thoát ứng dụng?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void ChkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}