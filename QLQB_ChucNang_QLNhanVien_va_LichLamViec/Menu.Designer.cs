using System;
using System.Drawing;
using System.Windows.Forms;
namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    partial class frmMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controls
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnLogout;

        // Menu buttons
        private System.Windows.Forms.Button btnQLNhanVien;
        private System.Windows.Forms.Button btnChamCong;
        private System.Windows.Forms.Button btnTinhLuong;
        private System.Windows.Forms.Button btnCaLam;
        private System.Windows.Forms.Button btnQLBanHoaDon;
        private System.Windows.Forms.Button btnQLKho;
        private System.Windows.Forms.Button btnXemPhieuNhap;
        private System.Windows.Forms.Button btnNhapHang;

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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblTitle1 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnQLNhanVien = new System.Windows.Forms.Button();
            this.btnChamCong = new System.Windows.Forms.Button();
            this.btnTinhLuong = new System.Windows.Forms.Button();
            this.btnCaLam = new System.Windows.Forms.Button();
            this.btnQLBanHoaDon = new System.Windows.Forms.Button();
            this.btnQLKho = new System.Windows.Forms.Button();
            this.btnNhapHang = new System.Windows.Forms.Button();
            this.btnXemPhieuNhap = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblWelcome);
            this.pnlHeader.Controls.Add(this.btnLogout);
            this.pnlHeader.Controls.Add(this.lblIcon);
            this.pnlHeader.Controls.Add(this.lblTitle1);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 100);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(80, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(511, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HỆ THỐNG QUẢN LÝ QUÁN BAR";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(35, 65);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(147, 21);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Xin chào, Nhân viên";
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(1040, 30);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(140, 45);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "🚪 Đăng xuất";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblIcon
            // 
            this.lblIcon.AutoSize = true;
            this.lblIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 28F);
            this.lblIcon.Location = new System.Drawing.Point(30, 18);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(74, 51);
            this.lblIcon.TabIndex = 3;
            this.lblIcon.Text = "🍹";
            // 
            // lblTitle1
            // 
            this.lblTitle1.Location = new System.Drawing.Point(0, 0);
            this.lblTitle1.Name = "lblTitle1";
            this.lblTitle1.Size = new System.Drawing.Size(100, 23);
            this.lblTitle1.TabIndex = 4;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlMain.Controls.Add(this.btnQLNhanVien);
            this.pnlMain.Controls.Add(this.btnChamCong);
            this.pnlMain.Controls.Add(this.btnTinhLuong);
            this.pnlMain.Controls.Add(this.btnCaLam);
            this.pnlMain.Controls.Add(this.btnQLBanHoaDon);
            this.pnlMain.Controls.Add(this.btnQLKho);
            this.pnlMain.Controls.Add(this.btnNhapHang);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 100);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(50);
            this.pnlMain.Size = new System.Drawing.Size(1200, 713);
            this.pnlMain.TabIndex = 1;
            // 
            // btnQLNhanVien
            // 
            this.btnQLNhanVien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnQLNhanVien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQLNhanVien.FlatAppearance.BorderSize = 0;
            this.btnQLNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQLNhanVien.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQLNhanVien.ForeColor = System.Drawing.Color.White;
            this.btnQLNhanVien.Location = new System.Drawing.Point(92, 46);
            this.btnQLNhanVien.Name = "btnQLNhanVien";
            this.btnQLNhanVien.Size = new System.Drawing.Size(280, 180);
            this.btnQLNhanVien.TabIndex = 0;
            this.btnQLNhanVien.Text = "👥\r\n\r\nQUẢN LÝ\r\nNHÂN VIÊN";
            this.btnQLNhanVien.UseVisualStyleBackColor = false;
            this.btnQLNhanVien.Click += new System.EventHandler(this.btnQLNhanVien_Click);
            this.btnQLNhanVien.MouseEnter += new System.EventHandler(this.MenuButton_MouseEnter);
            this.btnQLNhanVien.MouseLeave += new System.EventHandler(this.MenuButton_MouseLeave);
            // 
            // btnChamCong
            // 
            this.btnChamCong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnChamCong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChamCong.FlatAppearance.BorderSize = 0;
            this.btnChamCong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChamCong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChamCong.ForeColor = System.Drawing.Color.White;
            this.btnChamCong.Location = new System.Drawing.Point(452, 46);
            this.btnChamCong.Name = "btnChamCong";
            this.btnChamCong.Size = new System.Drawing.Size(280, 180);
            this.btnChamCong.TabIndex = 1;
            this.btnChamCong.Text = "✓\r\n\r\nCHẤM CÔNG";
            this.btnChamCong.UseVisualStyleBackColor = false;
            this.btnChamCong.Click += new System.EventHandler(this.btnChamCong_Click);
            this.btnChamCong.MouseEnter += new System.EventHandler(this.MenuButton_MouseEnter);
            this.btnChamCong.MouseLeave += new System.EventHandler(this.MenuButton_MouseLeave);
            // 
            // btnTinhLuong
            // 
            this.btnTinhLuong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnTinhLuong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTinhLuong.FlatAppearance.BorderSize = 0;
            this.btnTinhLuong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTinhLuong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTinhLuong.ForeColor = System.Drawing.Color.White;
            this.btnTinhLuong.Location = new System.Drawing.Point(812, 46);
            this.btnTinhLuong.Name = "btnTinhLuong";
            this.btnTinhLuong.Size = new System.Drawing.Size(280, 180);
            this.btnTinhLuong.TabIndex = 2;
            this.btnTinhLuong.Text = "💰\r\n\r\nTÍNH LƯƠNG";
            this.btnTinhLuong.UseVisualStyleBackColor = false;
            this.btnTinhLuong.Click += new System.EventHandler(this.btnTinhLuong_Click);
            this.btnTinhLuong.MouseEnter += new System.EventHandler(this.MenuButton_MouseEnter);
            this.btnTinhLuong.MouseLeave += new System.EventHandler(this.MenuButton_MouseLeave);
            // 
            // btnCaLam
            // 
            this.btnCaLam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnCaLam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCaLam.FlatAppearance.BorderSize = 0;
            this.btnCaLam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaLam.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaLam.ForeColor = System.Drawing.Color.White;
            this.btnCaLam.Location = new System.Drawing.Point(92, 286);
            this.btnCaLam.Name = "btnCaLam";
            this.btnCaLam.Size = new System.Drawing.Size(280, 180);
            this.btnCaLam.TabIndex = 3;
            this.btnCaLam.Text = "📅\r\n\r\nCA LÀM VIỆC";
            this.btnCaLam.UseVisualStyleBackColor = false;
            this.btnCaLam.Click += new System.EventHandler(this.btnCaLam_Click);
            this.btnCaLam.MouseEnter += new System.EventHandler(this.MenuButton_MouseEnter);
            this.btnCaLam.MouseLeave += new System.EventHandler(this.MenuButton_MouseLeave);
            // 
            // btnQLBanHoaDon
            // 
            this.btnQLBanHoaDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnQLBanHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQLBanHoaDon.FlatAppearance.BorderSize = 0;
            this.btnQLBanHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQLBanHoaDon.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQLBanHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnQLBanHoaDon.Location = new System.Drawing.Point(452, 286);
            this.btnQLBanHoaDon.Name = "btnQLBanHoaDon";
            this.btnQLBanHoaDon.Size = new System.Drawing.Size(280, 180);
            this.btnQLBanHoaDon.TabIndex = 4;
            this.btnQLBanHoaDon.Text = "🍽️\r\n\r\nBÀN - HÓA ĐƠN";
            this.btnQLBanHoaDon.UseVisualStyleBackColor = false;
            this.btnQLBanHoaDon.Click += new System.EventHandler(this.btnQLBanHoaDon_Click);
            this.btnQLBanHoaDon.MouseEnter += new System.EventHandler(this.MenuButton_MouseEnter);
            this.btnQLBanHoaDon.MouseLeave += new System.EventHandler(this.MenuButton_MouseLeave);
            // 
            // btnQLKho
            // 
            this.btnQLKho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.btnQLKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQLKho.FlatAppearance.BorderSize = 0;
            this.btnQLKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQLKho.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQLKho.ForeColor = System.Drawing.Color.White;
            this.btnQLKho.Location = new System.Drawing.Point(812, 286);
            this.btnQLKho.Name = "btnQLKho";
            this.btnQLKho.Size = new System.Drawing.Size(280, 180);
            this.btnQLKho.TabIndex = 5;
            this.btnQLKho.Text = "📦\r\n\r\nQUẢN LÝ KHO";
            this.btnQLKho.UseVisualStyleBackColor = false;
            this.btnQLKho.Click += new System.EventHandler(this.btnQLKho_Click);
            this.btnQLKho.MouseEnter += new System.EventHandler(this.MenuButton_MouseEnter);
            this.btnQLKho.MouseLeave += new System.EventHandler(this.MenuButton_MouseLeave);
            // 
            // btnNhapHang
            // 
            this.btnNhapHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(100)))), ((int)(((byte)(50)))));
            this.btnNhapHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNhapHang.FlatAppearance.BorderSize = 0;
            this.btnNhapHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhapHang.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhapHang.ForeColor = System.Drawing.Color.White;
            this.btnNhapHang.Location = new System.Drawing.Point(452, 516);
            this.btnNhapHang.Name = "btnNhapHang";
            this.btnNhapHang.Size = new System.Drawing.Size(280, 180);
            this.btnNhapHang.TabIndex = 7;
            this.btnNhapHang.Text = "📥\r\n\r\nNHẬP HÀNG";
            this.btnNhapHang.UseVisualStyleBackColor = false;
            this.btnNhapHang.Click += new System.EventHandler(this.btnNhapHang_Click);
            this.btnNhapHang.MouseEnter += new System.EventHandler(this.MenuButton_MouseEnter);
            this.btnNhapHang.MouseLeave += new System.EventHandler(this.MenuButton_MouseLeave);
            // 
            // btnXemPhieuNhap
            // 
            this.btnXemPhieuNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.btnXemPhieuNhap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXemPhieuNhap.FlatAppearance.BorderSize = 0;
            this.btnXemPhieuNhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemPhieuNhap.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXemPhieuNhap.ForeColor = System.Drawing.Color.White;
            this.btnXemPhieuNhap.Location = new System.Drawing.Point(460, 520);
            this.btnXemPhieuNhap.Name = "btnXemPhieuNhap";
            this.btnXemPhieuNhap.Size = new System.Drawing.Size(280, 180);
            this.btnXemPhieuNhap.TabIndex = 6;
            this.btnXemPhieuNhap.Text = "📋\r\n\r\nXEM PHIẾU NHẬP";
            this.btnXemPhieuNhap.UseVisualStyleBackColor = false;
            this.btnXemPhieuNhap.MouseEnter += new System.EventHandler(this.MenuButton_MouseEnter);
            this.btnXemPhieuNhap.MouseLeave += new System.EventHandler(this.MenuButton_MouseLeave);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 813);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu Chính - Quản Lý Quán Bar";
            this.Load += new System.EventHandler(this.frmMenu_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblIcon;
        private Label lblTitle1;
    }
}