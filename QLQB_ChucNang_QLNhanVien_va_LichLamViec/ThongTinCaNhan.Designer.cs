using System.Drawing;
using System.Windows.Forms;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    partial class frmThongTinCaNhan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private GroupBox grpThongTin;
        private Label lblMaNV, lblTenNV, lblChucVu, lblNgaySinh;
        private Label lblGioiTinh, lblLuongMoiGio, lblMaCa, lblQuyen;
        private TextBox txtMaNV, txtTenNV, txtChucVu, txtNgaySinh;
        private TextBox txtGioiTinh, txtLuongMoiGio, txtMaCa, txtQuyen;
        private Label lblGioBD, lblGioKT;
        private TextBox txtGioBD, txtGioKT;
        private Button btnDong;

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblMaNV = new System.Windows.Forms.Label();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.lblTenNV = new System.Windows.Forms.Label();
            this.txtTenNV = new System.Windows.Forms.TextBox();
            this.lblChucVu = new System.Windows.Forms.Label();
            this.txtChucVu = new System.Windows.Forms.TextBox();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.txtNgaySinh = new System.Windows.Forms.TextBox();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.txtGioiTinh = new System.Windows.Forms.TextBox();
            this.lblLuongMoiGio = new System.Windows.Forms.Label();
            this.txtLuongMoiGio = new System.Windows.Forms.TextBox();
            this.lblMaCa = new System.Windows.Forms.Label();
            this.txtMaCa = new System.Windows.Forms.TextBox();
            this.lblGioBD = new System.Windows.Forms.Label();
            this.txtGioBD = new System.Windows.Forms.TextBox();
            this.lblGioKT = new System.Windows.Forms.Label();
            this.txtGioKT = new System.Windows.Forms.TextBox();
            this.lblQuyen = new System.Windows.Forms.Label();
            this.txtQuyen = new System.Windows.Forms.TextBox();
            this.btnDong = new System.Windows.Forms.Button();
            this.grpThongTin.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(243, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THÔNG TIN CÁ NHÂN";
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.lblMaNV);
            this.grpThongTin.Controls.Add(this.txtMaNV);
            this.grpThongTin.Controls.Add(this.lblTenNV);
            this.grpThongTin.Controls.Add(this.txtTenNV);
            this.grpThongTin.Controls.Add(this.lblChucVu);
            this.grpThongTin.Controls.Add(this.txtChucVu);
            this.grpThongTin.Controls.Add(this.lblNgaySinh);
            this.grpThongTin.Controls.Add(this.txtNgaySinh);
            this.grpThongTin.Controls.Add(this.lblGioiTinh);
            this.grpThongTin.Controls.Add(this.txtGioiTinh);
            this.grpThongTin.Controls.Add(this.lblLuongMoiGio);
            this.grpThongTin.Controls.Add(this.txtLuongMoiGio);
            this.grpThongTin.Controls.Add(this.lblMaCa);
            this.grpThongTin.Controls.Add(this.txtMaCa);
            this.grpThongTin.Controls.Add(this.lblGioBD);
            this.grpThongTin.Controls.Add(this.txtGioBD);
            this.grpThongTin.Controls.Add(this.lblGioKT);
            this.grpThongTin.Controls.Add(this.txtGioKT);
            this.grpThongTin.Controls.Add(this.lblQuyen);
            this.grpThongTin.Controls.Add(this.txtQuyen);
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpThongTin.Location = new System.Drawing.Point(1, 53);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(442, 415);
            this.grpThongTin.TabIndex = 1;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Chi tiết";
            // 
            // lblMaNV
            // 
            this.lblMaNV.AutoSize = true;
            this.lblMaNV.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMaNV.Location = new System.Drawing.Point(20, 30);
            this.lblMaNV.Name = "lblMaNV";
            this.lblMaNV.Size = new System.Drawing.Size(55, 19);
            this.lblMaNV.TabIndex = 0;
            this.lblMaNV.Text = "Mã NV:";
            // 
            // txtMaNV
            // 
            this.txtMaNV.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMaNV.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaNV.Location = new System.Drawing.Point(150, 27);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.ReadOnly = true;
            this.txtMaNV.Size = new System.Drawing.Size(200, 25);
            this.txtMaNV.TabIndex = 1;
            // 
            // lblTenNV
            // 
            this.lblTenNV.AutoSize = true;
            this.lblTenNV.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTenNV.Location = new System.Drawing.Point(20, 70);
            this.lblTenNV.Name = "lblTenNV";
            this.lblTenNV.Size = new System.Drawing.Size(97, 19);
            this.lblTenNV.TabIndex = 2;
            this.lblTenNV.Text = "Tên nhân viên:";
            // 
            // txtTenNV
            // 
            this.txtTenNV.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTenNV.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenNV.Location = new System.Drawing.Point(150, 67);
            this.txtTenNV.Name = "txtTenNV";
            this.txtTenNV.ReadOnly = true;
            this.txtTenNV.Size = new System.Drawing.Size(200, 25);
            this.txtTenNV.TabIndex = 3;
            // 
            // lblChucVu
            // 
            this.lblChucVu.AutoSize = true;
            this.lblChucVu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblChucVu.Location = new System.Drawing.Point(20, 110);
            this.lblChucVu.Name = "lblChucVu";
            this.lblChucVu.Size = new System.Drawing.Size(62, 19);
            this.lblChucVu.TabIndex = 4;
            this.lblChucVu.Text = "Chức vụ:";
            // 
            // txtChucVu
            // 
            this.txtChucVu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtChucVu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtChucVu.Location = new System.Drawing.Point(150, 107);
            this.txtChucVu.Name = "txtChucVu";
            this.txtChucVu.ReadOnly = true;
            this.txtChucVu.Size = new System.Drawing.Size(200, 25);
            this.txtChucVu.TabIndex = 5;
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgaySinh.Location = new System.Drawing.Point(20, 150);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(73, 19);
            this.lblNgaySinh.TabIndex = 6;
            this.lblNgaySinh.Text = "Ngày sinh:";
            // 
            // txtNgaySinh
            // 
            this.txtNgaySinh.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNgaySinh.Location = new System.Drawing.Point(150, 147);
            this.txtNgaySinh.Name = "txtNgaySinh";
            this.txtNgaySinh.ReadOnly = true;
            this.txtNgaySinh.Size = new System.Drawing.Size(200, 25);
            this.txtNgaySinh.TabIndex = 7;
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.AutoSize = true;
            this.lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGioiTinh.Location = new System.Drawing.Point(20, 190);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(64, 19);
            this.lblGioiTinh.TabIndex = 8;
            this.lblGioiTinh.Text = "Giới tính:";
            // 
            // txtGioiTinh
            // 
            this.txtGioiTinh.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGioiTinh.Location = new System.Drawing.Point(150, 187);
            this.txtGioiTinh.Name = "txtGioiTinh";
            this.txtGioiTinh.ReadOnly = true;
            this.txtGioiTinh.Size = new System.Drawing.Size(200, 25);
            this.txtGioiTinh.TabIndex = 9;
            // 
            // lblLuongMoiGio
            // 
            this.lblLuongMoiGio.AutoSize = true;
            this.lblLuongMoiGio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLuongMoiGio.Location = new System.Drawing.Point(20, 230);
            this.lblLuongMoiGio.Name = "lblLuongMoiGio";
            this.lblLuongMoiGio.Size = new System.Drawing.Size(75, 19);
            this.lblLuongMoiGio.TabIndex = 10;
            this.lblLuongMoiGio.Text = "Lương/giờ:";
            // 
            // txtLuongMoiGio
            // 
            this.txtLuongMoiGio.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtLuongMoiGio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLuongMoiGio.Location = new System.Drawing.Point(150, 227);
            this.txtLuongMoiGio.Name = "txtLuongMoiGio";
            this.txtLuongMoiGio.ReadOnly = true;
            this.txtLuongMoiGio.Size = new System.Drawing.Size(200, 25);
            this.txtLuongMoiGio.TabIndex = 11;
            // 
            // lblMaCa
            // 
            this.lblMaCa.AutoSize = true;
            this.lblMaCa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMaCa.Location = new System.Drawing.Point(23, 321);
            this.lblMaCa.Name = "lblMaCa";
            this.lblMaCa.Size = new System.Drawing.Size(49, 19);
            this.lblMaCa.TabIndex = 12;
            this.lblMaCa.Text = "Mã ca:";
            // 
            // txtMaCa
            // 
            this.txtMaCa.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtMaCa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaCa.Location = new System.Drawing.Point(150, 318);
            this.txtMaCa.Name = "txtMaCa";
            this.txtMaCa.ReadOnly = true;
            this.txtMaCa.Size = new System.Drawing.Size(70, 25);
            this.txtMaCa.TabIndex = 13;
            // 
            // lblGioBD
            // 
            this.lblGioBD.AutoSize = true;
            this.lblGioBD.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGioBD.Location = new System.Drawing.Point(60, 362);
            this.lblGioBD.Name = "lblGioBD";
            this.lblGioBD.Size = new System.Drawing.Size(84, 19);
            this.lblGioBD.TabIndex = 14;
            this.lblGioBD.Text = "Giờ bắt đầu:";
            // 
            // txtGioBD
            // 
            this.txtGioBD.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtGioBD.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGioBD.Location = new System.Drawing.Point(150, 359);
            this.txtGioBD.Name = "txtGioBD";
            this.txtGioBD.ReadOnly = true;
            this.txtGioBD.Size = new System.Drawing.Size(80, 25);
            this.txtGioBD.TabIndex = 15;
            // 
            // lblGioKT
            // 
            this.lblGioKT.AutoSize = true;
            this.lblGioKT.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGioKT.Location = new System.Drawing.Point(236, 362);
            this.lblGioKT.Name = "lblGioKT";
            this.lblGioKT.Size = new System.Drawing.Size(87, 19);
            this.lblGioKT.TabIndex = 16;
            this.lblGioKT.Text = "Giờ kết thúc:";
            // 
            // txtGioKT
            // 
            this.txtGioKT.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtGioKT.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGioKT.Location = new System.Drawing.Point(329, 359);
            this.txtGioKT.Name = "txtGioKT";
            this.txtGioKT.ReadOnly = true;
            this.txtGioKT.Size = new System.Drawing.Size(80, 25);
            this.txtGioKT.TabIndex = 17;
            // 
            // lblQuyen
            // 
            this.lblQuyen.AutoSize = true;
            this.lblQuyen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblQuyen.Location = new System.Drawing.Point(20, 274);
            this.lblQuyen.Name = "lblQuyen";
            this.lblQuyen.Size = new System.Drawing.Size(53, 19);
            this.lblQuyen.TabIndex = 18;
            this.lblQuyen.Text = "Quyền:";
            // 
            // txtQuyen
            // 
            this.txtQuyen.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtQuyen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtQuyen.Location = new System.Drawing.Point(150, 271);
            this.txtQuyen.Name = "txtQuyen";
            this.txtQuyen.ReadOnly = true;
            this.txtQuyen.Size = new System.Drawing.Size(200, 25);
            this.txtQuyen.TabIndex = 19;
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(161, 487);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 40);
            this.btnDong.TabIndex = 2;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // frmThongTinCaNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(447, 550);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.btnDong);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThongTinCaNhan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thông Tin Cá Nhân";
            this.Load += new System.EventHandler(this.frmThongTinCaNhan_Load);
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
    }
}