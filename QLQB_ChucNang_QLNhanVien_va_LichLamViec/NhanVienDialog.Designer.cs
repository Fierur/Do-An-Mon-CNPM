using System;
using System.Drawing;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    partial class frmNhanVienDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.TextBox txtTenNV;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.NumericUpDown nudLuongMoiGio;
        private System.Windows.Forms.ComboBox cboChucVu;
        private System.Windows.Forms.TextBox txtMaQuyen;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMaNV;
        private System.Windows.Forms.Label lblTenNV;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.Label lblLuongMoiGio;
        private System.Windows.Forms.Label lblChucVu;
        private System.Windows.Forms.Label lblMaQuyen;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblTrangThai;

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
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.txtTenNV = new System.Windows.Forms.TextBox();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.nudLuongMoiGio = new System.Windows.Forms.NumericUpDown();
            this.cboChucVu = new System.Windows.Forms.ComboBox();
            this.txtMaQuyen = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMaNV = new System.Windows.Forms.Label();
            this.lblTenNV = new System.Windows.Forms.Label();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.lblLuongMoiGio = new System.Windows.Forms.Label();
            this.lblChucVu = new System.Windows.Forms.Label();
            this.lblMaQuyen = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudLuongMoiGio)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMaNV
            // 
            this.txtMaNV.BackColor = System.Drawing.Color.LightGray;
            this.txtMaNV.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaNV.Location = new System.Drawing.Point(170, 27);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(240, 25);
            this.txtMaNV.TabIndex = 1;
            // 
            // txtTenNV
            // 
            this.txtTenNV.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenNV.Location = new System.Drawing.Point(170, 67);
            this.txtTenNV.Name = "txtTenNV";
            this.txtTenNV.Size = new System.Drawing.Size(240, 25);
            this.txtTenNV.TabIndex = 3;
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new System.Drawing.Point(170, 107);
            this.dtpNgaySinh.MaxDate = new System.DateTime(2007, 11, 2, 15, 12, 51, 10);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(240, 25);
            this.dtpNgaySinh.TabIndex = 5;
            this.dtpNgaySinh.Value = new System.DateTime(2000, 11, 2, 15, 12, 51, 10);
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboGioiTinh.FormattingEnabled = true;
            this.cboGioiTinh.Location = new System.Drawing.Point(170, 147);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(240, 25);
            this.cboGioiTinh.TabIndex = 7;
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMatKhau.Location = new System.Drawing.Point(170, 187);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(240, 25);
            this.txtMatKhau.TabIndex = 9;
            // 
            // nudLuongMoiGio
            // 
            this.nudLuongMoiGio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudLuongMoiGio.Increment = new decimal(new int[] { 1000, 0, 0, 0});
            this.nudLuongMoiGio.Location = new System.Drawing.Point(170, 227);
            this.nudLuongMoiGio.Maximum = new decimal(new int[] { 1000000, 0, 0, 0});
            this.nudLuongMoiGio.Name = "nudLuongMoiGio";
            this.nudLuongMoiGio.Size = new System.Drawing.Size(240, 25);
            this.nudLuongMoiGio.TabIndex = 11;
            this.nudLuongMoiGio.ThousandsSeparator = true;
            this.nudLuongMoiGio.Value = new decimal(new int[] { 50000, 0, 0, 0});
            // 
            // cboChucVu
            // 
            this.cboChucVu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChucVu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboChucVu.FormattingEnabled = true;
            this.cboChucVu.Location = new System.Drawing.Point(170, 267);
            this.cboChucVu.Name = "cboChucVu";
            this.cboChucVu.Size = new System.Drawing.Size(240, 25);
            this.cboChucVu.TabIndex = 13;
            this.cboChucVu.SelectedIndexChanged += new System.EventHandler(this.cboChucVu_SelectedIndexChanged);
            // 
            // txtMaQuyen
            // 
            this.txtMaQuyen.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMaQuyen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaQuyen.Location = new System.Drawing.Point(170, 307);
            this.txtMaQuyen.Name = "txtMaQuyen";
            this.txtMaQuyen.ReadOnly = true;
            this.txtMaQuyen.Size = new System.Drawing.Size(240, 25);
            this.txtMaQuyen.TabIndex = 15;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(46, 430);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 40);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(285, 430);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // lblMaNV
            // 
            this.lblMaNV.AutoSize = true;
            this.lblMaNV.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMaNV.Location = new System.Drawing.Point(30, 30);
            this.lblMaNV.Name = "lblMaNV";
            this.lblMaNV.Size = new System.Drawing.Size(55, 19);
            this.lblMaNV.TabIndex = 0;
            this.lblMaNV.Text = "Mã NV:";
            // 
            // lblTenNV
            // 
            this.lblTenNV.AutoSize = true;
            this.lblTenNV.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTenNV.Location = new System.Drawing.Point(30, 70);
            this.lblTenNV.Name = "lblTenNV";
            this.lblTenNV.Size = new System.Drawing.Size(66, 19);
            this.lblTenNV.TabIndex = 2;
            this.lblTenNV.Text = "Tên NV: *";
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgaySinh.Location = new System.Drawing.Point(30, 110);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(83, 19);
            this.lblNgaySinh.TabIndex = 4;
            this.lblNgaySinh.Text = "Ngày sinh: *";
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGioiTinh.Location = new System.Drawing.Point(30, 150);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(74, 19);
            this.lblGioiTinh.TabIndex = 6;
            this.lblGioiTinh.Text = "Giới tính: *";
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMatKhau.Location = new System.Drawing.Point(30, 190);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(81, 19);
            this.lblMatKhau.TabIndex = 8;
            this.lblMatKhau.Text = "Mật khẩu: *";
            // 
            // lblLuongMoiGio
            // 
            this.lblLuongMoiGio.AutoSize = true;
            this.lblLuongMoiGio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLuongMoiGio.Location = new System.Drawing.Point(30, 230);
            this.lblLuongMoiGio.Name = "lblLuongMoiGio";
            this.lblLuongMoiGio.Size = new System.Drawing.Size(126, 19);
            this.lblLuongMoiGio.TabIndex = 10;
            this.lblLuongMoiGio.Text = "Lương/giờ (VNĐ): *";
            // 
            // lblChucVu
            // 
            this.lblChucVu.AutoSize = true;
            this.lblChucVu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblChucVu.Location = new System.Drawing.Point(30, 270);
            this.lblChucVu.Name = "lblChucVu";
            this.lblChucVu.Size = new System.Drawing.Size(72, 19);
            this.lblChucVu.TabIndex = 12;
            this.lblChucVu.Text = "Chức vụ: *";
            // 
            // lblMaQuyen
            // 
            this.lblMaQuyen.AutoSize = true;
            this.lblMaQuyen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMaQuyen.Location = new System.Drawing.Point(30, 310);
            this.lblMaQuyen.Name = "lblMaQuyen";
            this.lblMaQuyen.Size = new System.Drawing.Size(63, 19);
            this.lblMaQuyen.TabIndex = 14;
            this.lblMaQuyen.Text = "Quyền: *";
            // 
            // lblTrangThai
            // 
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrangThai.Location = new System.Drawing.Point(30, 350);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(80, 19);
            this.lblTrangThai.TabIndex = 18;
            this.lblTrangThai.Text = "Trạng thái: *";

            // 
            // cboTrangThai
            // 
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(170, 347);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(240, 25);
            this.cboTrangThai.TabIndex = 19;

            this.cboTrangThai.Items.AddRange(new object[]
                {
                    "Đang làm",
                    "Tạm nghỉ",
                    "Nghỉ việc"
                });
            this.cboTrangThai.SelectedIndex = 0;
            // 
            // frmNhanVienDialog
            // 
            this.AcceptButton = this.btnOK;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(450, 499);
            this.Controls.Add(this.lblMaNV);
            this.Controls.Add(this.txtMaNV);
            this.Controls.Add(this.lblTenNV);
            this.Controls.Add(this.txtTenNV);
            this.Controls.Add(this.lblNgaySinh);
            this.Controls.Add(this.dtpNgaySinh);
            this.Controls.Add(this.lblGioiTinh);
            this.Controls.Add(this.cboGioiTinh);
            this.Controls.Add(this.lblMatKhau);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.lblLuongMoiGio);
            this.Controls.Add(this.nudLuongMoiGio);
            this.Controls.Add(this.lblChucVu);
            this.Controls.Add(this.cboChucVu);
            this.Controls.Add(this.lblMaQuyen);
            this.Controls.Add(this.txtMaQuyen);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.cboTrangThai);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNhanVienDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmNhanVienDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudLuongMoiGio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}