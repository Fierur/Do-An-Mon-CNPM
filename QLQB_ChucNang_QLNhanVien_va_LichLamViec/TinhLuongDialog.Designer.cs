using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    partial class frmTinhLuongDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmTinhLuongDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "frmTinhLuongDialog";
            this.Text = "TinhLuongDialog";
            this.ResumeLayout(false);
            this.Text = "Tính Lương Nhân Viên";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            // Title
            lblTitle = new Label();
            //lblTitle.Text = $"TÍNH LƯƠNG - {maNV} - {tenNV}";
            lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(41, 128, 185);
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;

            // Panel thông tin
            pnlThongTin = new Panel();
            pnlThongTin.Location = new Point(20, 60);
            pnlThongTin.Size = new Size(440, 120);
            pnlThongTin.BackColor = Color.FromArgb(236, 240, 241);
            pnlThongTin.BorderStyle = BorderStyle.FixedSingle;

            Label lblInfoTitle = new Label();
            lblInfoTitle.Text = "📊 THÔNG TIN HIỆN TẠI";
            lblInfoTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblInfoTitle.Location = new Point(10, 10);
            lblInfoTitle.AutoSize = true;

            lblSoNgayLam = new Label();
            lblSoNgayLam.Text = "Số ngày làm tháng hiện tại:";
            lblSoNgayLam.Location = new Point(20, 45);
            lblSoNgayLam.AutoSize = true;

            txtSoNgayLam = new TextBox();
            txtSoNgayLam.Location = new Point(230, 42);
            txtSoNgayLam.Width = 180;
            txtSoNgayLam.ReadOnly = true;
            txtSoNgayLam.BackColor = Color.WhiteSmoke;

            lblLuongGio = new Label();
            lblLuongGio.Text = "Lương/giờ:";
            lblLuongGio.Location = new Point(20, 80);
            lblLuongGio.AutoSize = true;

            txtLuongGio = new TextBox();
            txtLuongGio.Location = new Point(230, 77);
            txtLuongGio.Width = 180;
            txtLuongGio.ReadOnly = true;
            txtLuongGio.BackColor = Color.WhiteSmoke;

            pnlThongTin.Controls.AddRange(new Control[] {
                lblInfoTitle, lblSoNgayLam, txtSoNgayLam,
                lblLuongGio, txtLuongGio
            });

            // Chọn tháng/năm
            lblThang = new Label();
            lblThang.Text = "Tháng:";
            lblThang.Location = new Point(40, 200);
            lblThang.AutoSize = true;

            nudThang = new NumericUpDown();
            nudThang.Location = new Point(150, 197);
            nudThang.Width = 70;
            nudThang.Minimum = 1;
            nudThang.Maximum = 12;
            nudThang.Value = DateTime.Now.Month;
            //nudThang.ValueChanged += (s, e) => LoadThongTinThang();

            lblNam = new Label();
            lblNam.Text = "Năm:";
            lblNam.Location = new Point(250, 200);
            lblNam.AutoSize = true;

            nudNam = new NumericUpDown();
            nudNam.Location = new Point(310, 197);
            nudNam.Width = 90;
            nudNam.Minimum = 2020;
            nudNam.Maximum = 2100;
            nudNam.Value = DateTime.Now.Year;
            //nudNam.ValueChanged += (s, e) => LoadThongTinThang();

            // Thưởng/Phạt
            lblThuongPhat = new Label();
            lblThuongPhat.Text = "Thưởng/Phạt (VNĐ):";
            lblThuongPhat.Location = new Point(40, 250);
            lblThuongPhat.AutoSize = true;

            nudThuongPhat = new NumericUpDown();
            nudThuongPhat.Location = new Point(150, 247);
            nudThuongPhat.Width = 250;
            nudThuongPhat.Minimum = -10000000;
            nudThuongPhat.Maximum = 10000000;
            nudThuongPhat.Value = 0;
            nudThuongPhat.Increment = 50000;
            nudThuongPhat.ThousandsSeparator = true;

            // Note
            Label lblNote = new Label();
            lblNote.Text = "💡 Công thức: Tổng lương = Số ngày làm × 8 giờ × Lương/giờ + Thưởng/Phạt";
            lblNote.Location = new Point(40, 290);
            lblNote.Size = new Size(400, 40);
            lblNote.ForeColor = Color.FromArgb(52, 152, 219);
            lblNote.Font = new Font("Segoe UI", 9, FontStyle.Italic);

            // Buttons
            btnTinhLuong = new Button();
            btnTinhLuong.Text = "💰 Tính Lương";
            btnTinhLuong.Location = new Point(100, 350);
            btnTinhLuong.Size = new Size(130, 40);
            btnTinhLuong.BackColor = Color.FromArgb(46, 204, 113);
            btnTinhLuong.ForeColor = Color.White;
            btnTinhLuong.FlatStyle = FlatStyle.Flat;
            btnTinhLuong.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnTinhLuong.Cursor = Cursors.Hand;
            btnTinhLuong.Click += btnTinhLuong_Click;

            btnHuy = new Button();
            btnHuy.Text = "Hủy";
            btnHuy.Location = new Point(250, 350);
            btnHuy.Size = new Size(130, 40);
            btnHuy.BackColor = Color.FromArgb(189, 195, 199);
            btnHuy.ForeColor = Color.White;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnHuy.Cursor = Cursors.Hand;
            //btnHuy.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Controls.AddRange(new Control[] {
                lblTitle, pnlThongTin,
                lblThang, nudThang, lblNam, nudNam,
                lblThuongPhat, nudThuongPhat,
                lblNote, btnTinhLuong, btnHuy
            });

            this.Load += frmTinhLuongDialog_Load;
        }

        #endregion
    }
}