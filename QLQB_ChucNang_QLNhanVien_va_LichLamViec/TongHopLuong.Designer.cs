using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    partial class frmTongHopLuong
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblFilterTitle = new System.Windows.Forms.Label();
            this.lblThang = new System.Windows.Forms.Label();
            this.nudThang = new System.Windows.Forms.NumericUpDown();
            this.lblNam = new System.Windows.Forms.Label();
            this.nudNam = new System.Windows.Forms.NumericUpDown();
            this.btnLoc = new System.Windows.Forms.Button();
            this.dgvTongHop = new System.Windows.Forms.DataGridView();
            this.pnlThongKe = new System.Windows.Forms.Panel();
            this.lblTongNV = new System.Windows.Forms.Label();
            this.lblTongLuong = new System.Windows.Forms.Label();
            this.lblLuongTB = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTongHop)).BeginInit();
            this.pnlThongKe.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(380, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 TỔNG HỢP LƯƠNG NHÂN VIÊN";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.lblFilterTitle);
            this.pnlFilter.Controls.Add(this.lblThang);
            this.pnlFilter.Controls.Add(this.nudThang);
            this.pnlFilter.Controls.Add(this.lblNam);
            this.pnlFilter.Controls.Add(this.nudNam);
            this.pnlFilter.Controls.Add(this.btnLoc);
            this.pnlFilter.Location = new System.Drawing.Point(30, 70);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1130, 70);
            this.pnlFilter.TabIndex = 1;
            // 
            // lblFilterTitle
            // 
            this.lblFilterTitle.AutoSize = true;
            this.lblFilterTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblFilterTitle.Location = new System.Drawing.Point(15, 10);
            this.lblFilterTitle.Name = "lblFilterTitle";
            this.lblFilterTitle.Size = new System.Drawing.Size(88, 20);
            this.lblFilterTitle.TabIndex = 0;
            this.lblFilterTitle.Text = "🔍 BỘ LỌC";
            // 
            // lblThang
            // 
            this.lblThang.AutoSize = true;
            this.lblThang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblThang.Location = new System.Drawing.Point(30, 40);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(50, 19);
            this.lblThang.TabIndex = 1;
            this.lblThang.Text = "Tháng:";
            // 
            // nudThang
            // 
            this.nudThang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudThang.Location = new System.Drawing.Point(90, 37);
            this.nudThang.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            this.nudThang.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudThang.Name = "nudThang";
            this.nudThang.Size = new System.Drawing.Size(70, 25);
            this.nudThang.TabIndex = 2;
            this.nudThang.Value = new decimal(new int[] { 11, 0, 0, 0 });
            // 
            // lblNam
            // 
            this.lblNam.AutoSize = true;
            this.lblNam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNam.Location = new System.Drawing.Point(180, 40);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(41, 19);
            this.lblNam.TabIndex = 3;
            this.lblNam.Text = "Năm:";
            // 
            // nudNam
            // 
            this.nudNam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudNam.Location = new System.Drawing.Point(225, 37);
            this.nudNam.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            this.nudNam.Minimum = new decimal(new int[] { 2020, 0, 0, 0 });
            this.nudNam.Name = "nudNam";
            this.nudNam.Size = new System.Drawing.Size(90, 25);
            this.nudNam.TabIndex = 4;
            this.nudNam.Value = new decimal(new int[] { 2025, 0, 0, 0 });
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnLoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(340, 32);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(100, 30);
            this.btnLoc.TabIndex = 5;
            this.btnLoc.Text = "🔍 Lọc";
            this.btnLoc.UseVisualStyleBackColor = false;
            // 
            // dgvTongHop
            // 
            this.dgvTongHop.AllowUserToAddRows = false;
            this.dgvTongHop.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTongHop.ColumnHeadersHeight = 40;
            this.dgvTongHop.Location = new System.Drawing.Point(30, 160);
            this.dgvTongHop.Name = "dgvTongHop";
            this.dgvTongHop.ReadOnly = true;
            this.dgvTongHop.RowTemplate.Height = 35;
            this.dgvTongHop.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTongHop.Size = new System.Drawing.Size(1130, 380);
            this.dgvTongHop.TabIndex = 2;
            // 
            // pnlThongKe
            // 
            this.pnlThongKe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlThongKe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlThongKe.Controls.Add(this.lblTongNV);
            this.pnlThongKe.Controls.Add(this.lblTongLuong);
            this.pnlThongKe.Controls.Add(this.lblLuongTB);
            this.pnlThongKe.Location = new System.Drawing.Point(30, 560);
            this.pnlThongKe.Name = "pnlThongKe";
            this.pnlThongKe.Size = new System.Drawing.Size(1130, 60);
            this.pnlThongKe.TabIndex = 3;
            // 
            // lblTongNV
            // 
            this.lblTongNV.AutoSize = true;
            this.lblTongNV.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongNV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTongNV.Location = new System.Drawing.Point(20, 20);
            this.lblTongNV.Name = "lblTongNV";
            this.lblTongNV.Size = new System.Drawing.Size(88, 20);
            this.lblTongNV.TabIndex = 0;
            this.lblTongNV.Text = "Tổng NV: 0";
            // 
            // lblTongLuong
            // 
            this.lblTongLuong.AutoSize = true;
            this.lblTongLuong.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblTongLuong.Location = new System.Drawing.Point(350, 20);
            this.lblTongLuong.Name = "lblTongLuong";
            this.lblTongLuong.Size = new System.Drawing.Size(144, 20);
            this.lblTongLuong.TabIndex = 1;
            this.lblTongLuong.Text = "Tổng lương: 0 VNĐ";
            // 
            // lblLuongTB
            // 
            this.lblLuongTB.AutoSize = true;
            this.lblLuongTB.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblLuongTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.lblLuongTB.Location = new System.Drawing.Point(750, 20);
            this.lblLuongTB.Name = "lblLuongTB";
            this.lblLuongTB.Size = new System.Drawing.Size(131, 20);
            this.lblLuongTB.TabIndex = 2;
            this.lblLuongTB.Text = "Lương TB: 0 VNĐ";
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.btnDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(1040, 630);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 35);
            this.btnDong.TabIndex = 5;
            this.btnDong.Text = "✖ Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnXuatExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnXuatExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXuatExcel.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel.Location = new System.Drawing.Point(900, 630);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(120, 35);
            this.btnXuatExcel.TabIndex = 6;
            this.btnXuatExcel.Text = "📊 Xuất Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            // 
            // frmTongHopLuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.dgvTongHop);
            this.Controls.Add(this.pnlThongKe);
            this.Controls.Add(this.btnDong);
            this.Name = "frmTongHopLuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tổng Hợp Lương Nhân Viên";
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTongHop)).EndInit();
            this.pnlThongKe.ResumeLayout(false);
            this.pnlThongKe.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblFilterTitle;
        public Label lblTongLuong;
        private Button btnXuatExcel;
        private Panel pnlFilter;
        private Label lblThang, lblNam, lblTitle;
        private NumericUpDown nudThang, nudNam;
        private Button btnLoc, btnDong;
        private DataGridView dgvTongHop;
        private Panel pnlThongKe;
        private Label lblTongNV, lblLuongTB;
    }
}