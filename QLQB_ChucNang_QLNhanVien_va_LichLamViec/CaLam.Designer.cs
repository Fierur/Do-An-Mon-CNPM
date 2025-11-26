using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    partial class frmXemCaLam
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
            this.Text = "Ca Làm Việc";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterParent;

            // Panel Lọc
            pnlFilter = new Panel();
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Height = 60;
            pnlFilter.BackColor = Color.WhiteSmoke;

            Label lblLocNgay = new Label();
            lblLocNgay.Text = "Lọc theo ngày:";
            lblLocNgay.Location = new Point(20, 20);
            lblLocNgay.AutoSize = true;

            dtpLocNgay = new DateTimePicker();
            dtpLocNgay.Location = new Point(120, 17);
            dtpLocNgay.Width = 120;
            dtpLocNgay.Format = DateTimePickerFormat.Short;

            btnLocNgay = new Button();
            btnLocNgay.Text = "Lọc ngày";
            btnLocNgay.Location = new Point(250, 15);
            btnLocNgay.Size = new Size(90, 27);
            btnLocNgay.BackColor = Color.FromArgb(52, 152, 219);
            btnLocNgay.ForeColor = Color.White;
            btnLocNgay.FlatStyle = FlatStyle.Flat;
            btnLocNgay.Click += btnLocNgay_Click;

            Label lblLocThang = new Label();
            lblLocThang.Text = "Tháng:";
            lblLocThang.Location = new Point(370, 20);
            lblLocThang.AutoSize = true;

            nudLocThang = new NumericUpDown();
            nudLocThang.Location = new Point(430, 17);
            nudLocThang.Width = 60;
            nudLocThang.Minimum = 1;
            nudLocThang.Maximum = 12;
            nudLocThang.Value = DateTime.Now.Month;

            Label lblLocNam = new Label();
            lblLocNam.Text = "Năm:";
            lblLocNam.Location = new Point(510, 20);
            lblLocNam.AutoSize = true;

            nudLocNam = new NumericUpDown();
            nudLocNam.Location = new Point(550, 17);
            nudLocNam.Width = 80;
            nudLocNam.Minimum = 2020;
            nudLocNam.Maximum = 2100;
            nudLocNam.Value = DateTime.Now.Year;

            btnLocThang = new Button();
            btnLocThang.Text = "Lọc tháng";
            btnLocThang.Location = new Point(640, 15);
            btnLocThang.Size = new Size(90, 27);
            btnLocThang.BackColor = Color.FromArgb(52, 152, 219);
            btnLocThang.ForeColor = Color.White;
            btnLocThang.FlatStyle = FlatStyle.Flat;
            btnLocThang.Click += btnLocThang_Click;

            btnHuyLoc = new Button();
            btnHuyLoc.Text = "Hủy lọc";
            btnHuyLoc.Location = new Point(740, 15);
            btnHuyLoc.Size = new Size(90, 27);
            btnHuyLoc.BackColor = Color.FromArgb(189, 195, 199);
            btnHuyLoc.ForeColor = Color.White;
            btnHuyLoc.FlatStyle = FlatStyle.Flat;
            btnHuyLoc.Click += btnHuyLoc_Click;

            pnlFilter.Controls.AddRange(new Control[] {
                lblLocNgay, dtpLocNgay, btnLocNgay,
                lblLocThang, nudLocThang, lblLocNam, nudLocNam,
                btnLocThang, btnHuyLoc
            });

            // DataGridView Ca Làm
            Label lblCaLam = new Label();
            lblCaLam.Text = "DANH SÁCH CA LÀM VIỆC";
            lblCaLam.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblCaLam.Location = new Point(20, 70);
            lblCaLam.AutoSize = true;

            dgvCaLam = new DataGridView();
            dgvCaLam.Location = new Point(20, 100);
            dgvCaLam.Size = new Size(500, 250);
            dgvCaLam.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCaLam.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCaLam.ReadOnly = true;
            dgvCaLam.AllowUserToAddRows = false;
            dgvCaLam.SelectionChanged += dgvCaLam_SelectionChanged;

            // DataGridView Nhân Viên Ca
            Label lblNhanVienCa = new Label();
            lblNhanVienCa.Text = "DANH SÁCH NHÂN VIÊN THEO CA";
            lblNhanVienCa.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblNhanVienCa.Location = new Point(550, 70);
            lblNhanVienCa.AutoSize = true;

            dgvNhanVienCa = new DataGridView();
            dgvNhanVienCa.Location = new Point(550, 100);
            dgvNhanVienCa.Size = new Size(600, 520);
            dgvNhanVienCa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVienCa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVienCa.ReadOnly = true;
            dgvNhanVienCa.AllowUserToAddRows = false;

            // Thống kê
            Panel pnlThongKe = new Panel();
            pnlThongKe.Location = new Point(20, 360);
            pnlThongKe.Size = new Size(500, 260);
            pnlThongKe.BackColor = Color.WhiteSmoke;
            pnlThongKe.BorderStyle = BorderStyle.FixedSingle;

            Label lblThongKe = new Label();
            lblThongKe.Text = "THỐNG KÊ";
            lblThongKe.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblThongKe.Location = new Point(10, 10);
            lblThongKe.AutoSize = true;

            Label lblTongCa = new Label();
            lblTongCa.Name = "lblTongCa";
            lblTongCa.Text = "Tổng số ca: 0";
            lblTongCa.Font = new Font("Segoe UI", 10);
            lblTongCa.Location = new Point(20, 50);
            lblTongCa.AutoSize = true;

            Label lblTongNV = new Label();
            lblTongNV.Name = "lblTongNV";
            lblTongNV.Text = "Tổng số nhân viên: 0";
            lblTongNV.Font = new Font("Segoe UI", 10);
            lblTongNV.Location = new Point(20, 80);
            lblTongNV.AutoSize = true;

            pnlThongKe.Controls.AddRange(new Control[] {
                lblThongKe, lblTongCa, lblTongNV
            });

            this.Controls.AddRange(new Control[] {
                pnlFilter, lblCaLam, dgvCaLam,
                lblNhanVienCa, dgvNhanVienCa, pnlThongKe
            });

            this.Load += frmXemCaLam_Load;
        }

    }

    #endregion
}
