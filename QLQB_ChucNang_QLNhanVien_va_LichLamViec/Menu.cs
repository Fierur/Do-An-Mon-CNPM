using QLQB_ChucNang_QLNhanVien_va_LichLamViec.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
            //btnXemPhieuNhap.MouseEnter += MenuButton_MouseEnter;
            //btnXemPhieuNhap.MouseLeave += MenuButton_MouseLeave;

            //this.Controls.Add(btnXemPhieuNhap);
        }
        private void frmMenu_Load(object sender, EventArgs e)
        {
            // Hiển thị tên nhân viên đang đăng nhập
            lblWelcome.Text = $"Xin chào, {SessionInfo.TenNV} ({SessionInfo.TenQuyen})";

            // Kiểm tra quyền và ẩn/hiện nút tương ứng
            CheckPermissions();
        }

        private void CheckPermissions()
        {
            // Nếu không phải quản lý, ẩn một số nút
            if (!SessionInfo.IsAdmin)
            {
                // Nhân viên thường chỉ được phép chấm công
                btnQLNhanVien.Visible = false;
                btnTinhLuong.Visible = false;
                btnCaLam.Visible = false;
                btnQLKho.Visible = false;
                btnXemPhieuNhap.Visible = false;

                // Có thể cho phép nhân viên xem bàn và hóa đơn
                // btnQLBanHoaDon.Enabled = true;
            }
            if (SessionInfo.MaQuyen == "Q04")
            {
                btnQLNhanVien.Visible = false;
                btnTinhLuong.Visible = false;
                btnCaLam.Visible = false;
                btnQLKho.Visible = false;
                btnQLBanHoaDon.Visible = false;
            }
            if (SessionInfo.MaQuyen == "Q05")
            {
                btnQLNhanVien.Visible = false;
                btnTinhLuong.Visible = false;
                btnCaLam.Visible = false;
                btnQLKho.Visible = false;
                btnQLBanHoaDon.Visible = false;
            }
            
        }

        #region Event Handlers cho các nút menu
        private void btnXemPhieuNhap_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    // Hiển thị form nhập mã phiếu nhập
            //    using (FormNhapMaPhieuNhap formNhapMa = new FormNhapMaPhieuNhap())
            //    {
            //        if (formNhapMa.ShowDialog() == DialogResult.OK)
            //        {
            //            string maPhieuNhap = formNhapMa.MaPhieuNhap;

            //            if (!string.IsNullOrEmpty(maPhieuNhap))
            //            {
            //                // Mở form chi tiết phiếu nhập
            //                FormChiTietPhieuNhap formCT = new FormChiTietPhieuNhap(
            //                    maPhieuNhap,
            //                    DatabaseConnection.ConnectionString);
            //                formCT.ShowDialog();
            //            }
            //            else
            //            {
            //                MessageBox.Show("Vui lòng nhập mã phiếu nhập!", "Cảnh báo",
            //                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi mở form chi tiết phiếu nhập: " + ex.Message, "Lỗi",
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        private void btnQLNhanVien_Click(object sender, EventArgs e)
        {
            // Mở form Main
            frmMain frm = new frmMain();
            frm.Show();
            this.Hide();

            frm.Shown += (s, args) => { frm.SwitchToTab("tabQuanLyNV"); };
            // Khi đóng form Main, hiển thị lại form Menu
            frm.FormClosed += (s, args) =>
        {
            this.Show();
        };
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            // Mở form Main và chuyển đến tab Chấm công
            frmMain frm = new frmMain();
            frm.Show();
            this.Hide();

            frm.Shown += (s, args) => { frm.SwitchToTab("tabChamCong"); };
            // Sau khi form load xong, chuyển tab
            frm.Load += (s, args) =>
            {
                frm.SwitchToTab("tabChamCong");
            };

            frm.FormClosed += (s, args) =>
            {
                this.Show();
            };
        }

        private void btnTinhLuong_Click(object sender, EventArgs e)
        {
            // Mở form Main và chuyển đến tab Tính lương
            frmMain frm = new frmMain();
            frm.Show();
            this.Hide();

            frm.Shown += (s, args) =>
            {
                frm.SwitchToTab("tabTinhLuong");
            };

            frm.FormClosed += (s, args) =>
            {
                this.Show();
            };
        }

        private void btnCaLam_Click(object sender, EventArgs e)
        {
            // Mở form Main và chuyển đến tab Lịch làm việc
            frmMain frm = new frmMain();
            frm.Show();
            this.Hide();

            frm.Shown += (s, args) =>
            {
                frm.SwitchToTab("tabLichLamViec");
            };

            frm.FormClosed += (s, args) =>
            {
                this.Show();
            };
        }

        private void btnQLBanHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở form Quản lý bàn & hóa đơn
                QuanLyHoaDon formQLBan = new QuanLyHoaDon();
                formQLBan.Show();
                this.Hide();

                formQLBan.FormClosed += (s, args) =>
                {
                    this.Show();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form Quản lý bàn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQLKho_Click(object sender, EventArgs e)
        {
            try
            {
                FormQuanLyKho formKho = new FormQuanLyKho();
                formKho.Show();
                this.Hide();

                formKho.FormClosed += (s, args) =>
                {
                    this.Show();
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form Quản lý kho: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Xóa session
                SessionInfo.Clear();
                DatabaseConnection.ClearConnection();

                // Mở lại form đăng nhập
                frmLogin loginForm = new frmLogin();
                loginForm.Show();

                // Đóng form menu
                this.Close();
            }
        }

        #endregion

        #region Hiệu ứng hover cho các nút

        private void MenuButton_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                // Phóng to nhẹ khi hover
                btn.Font = new Font(btn.Font.FontFamily, btn.Font.Size + 1, btn.Font.Style);

                // Tăng độ sáng màu nền
                btn.BackColor = ControlPaint.Light(btn.BackColor, 0.1f);
            }
        }

        private void MenuButton_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                // Khôi phục kích thước ban đầu
                btn.Font = new Font(btn.Font.FontFamily, 14F, FontStyle.Bold);

                // Khôi phục màu nền
                // Bạn có thể lưu màu gốc vào Tag của button để khôi phục chính xác
                if (btn == btnQLNhanVien)
                    btn.BackColor = Color.FromArgb(52, 152, 219);
                else if (btn == btnChamCong)
                    btn.BackColor = Color.FromArgb(46, 204, 113);
                else if (btn == btnTinhLuong)
                    btn.BackColor = Color.FromArgb(241, 196, 15);
                else if (btn == btnCaLam)
                    btn.BackColor = Color.FromArgb(155, 89, 182);
                else if (btn == btnQLBanHoaDon)
                    btn.BackColor = Color.FromArgb(230, 126, 34);
                else if (btn == btnQLKho)
                    btn.BackColor = Color.FromArgb(26, 188, 156);
            }
        }

        #endregion
    }
}
