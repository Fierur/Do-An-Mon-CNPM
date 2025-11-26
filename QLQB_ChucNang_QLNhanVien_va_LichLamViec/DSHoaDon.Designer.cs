namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    partial class DSHoaDon
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblMaBan = new System.Windows.Forms.Label();
            this.txtMaBan = new System.Windows.Forms.TextBox();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.pnlThongKe = new System.Windows.Forms.Panel();
            this.lblTongHoaDon = new System.Windows.Forms.Label();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.grpFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            this.pnlThongKe.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dgvLichSu);
            this.pnlMain.Controls.Add(this.pnlThongKe);
            this.pnlMain.Controls.Add(this.grpFilter);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.btnXuatExcel);
            this.grpFilter.Controls.Add(this.btnLamMoi);
            this.grpFilter.Controls.Add(this.btnTimKiem);
            this.grpFilter.Controls.Add(this.txtTenKH);
            this.grpFilter.Controls.Add(this.lblTenKH);
            this.grpFilter.Controls.Add(this.txtMaBan);
            this.grpFilter.Controls.Add(this.lblMaBan);
            this.grpFilter.Controls.Add(this.dtpDenNgay);
            this.grpFilter.Controls.Add(this.lblDenNgay);
            this.grpFilter.Controls.Add(this.dtpTuNgay);
            this.grpFilter.Controls.Add(this.lblTuNgay);
            this.grpFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFilter.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.grpFilter.Location = new System.Drawing.Point(0, 0);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(1200, 120);
            this.grpFilter.TabIndex = 0;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "BỘ LỌC TÌM KIẾM";
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTuNgay.Location = new System.Drawing.Point(20, 35);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(59, 15);
            this.lblTuNgay.TabIndex = 0;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Font = new System.Drawing.Font("Arial", 9F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(100, 32);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(150, 21);
            this.dtpTuNgay.TabIndex = 1;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblDenNgay.Location = new System.Drawing.Point(270, 35);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(66, 15);
            this.lblDenNgay.TabIndex = 2;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Font = new System.Drawing.Font("Arial", 9F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(350, 32);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(150, 21);
            this.dtpDenNgay.TabIndex = 3;
            // 
            // lblMaBan
            // 
            this.lblMaBan.AutoSize = true;
            this.lblMaBan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaBan.Location = new System.Drawing.Point(520, 35);
            this.lblMaBan.Name = "lblMaBan";
            this.lblMaBan.Size = new System.Drawing.Size(51, 15);
            this.lblMaBan.TabIndex = 4;
            this.lblMaBan.Text = "Mã bàn:";
            // 
            // txtMaBan
            // 
            this.txtMaBan.Font = new System.Drawing.Font("Arial", 9F);
            this.txtMaBan.Location = new System.Drawing.Point(580, 32);
            this.txtMaBan.Name = "txtMaBan";
            this.txtMaBan.Size = new System.Drawing.Size(120, 21);
            this.txtMaBan.TabIndex = 5;
            // 
            // lblTenKH
            // 
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblTenKH.Location = new System.Drawing.Point(20, 75);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(50, 15);
            this.lblTenKH.TabIndex = 6;
            this.lblTenKH.Text = "Tên KH:";
            // 
            // txtTenKH
            // 
            this.txtTenKH.Font = new System.Drawing.Font("Arial", 9F);
            this.txtTenKH.Location = new System.Drawing.Point(100, 72);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(250, 21);
            this.txtTenKH.TabIndex = 7;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.SteelBlue;
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(380, 70);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 30);
            this.btnTimKiem.TabIndex = 8;
            this.btnTimKiem.Text = "🔍 Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.Gray;
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(490, 70);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 30);
            this.btnLamMoi.TabIndex = 9;
            this.btnLamMoi.Text = "🔄 Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.Color.SeaGreen;
            this.btnXuatExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.btnXuatExcel.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel.Location = new System.Drawing.Point(600, 70);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(100, 30);
            this.btnXuatExcel.TabIndex = 10;
            this.btnXuatExcel.Text = "📊 Xuất Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // dgvLichSu
            // 
            this.dgvLichSu.AllowUserToAddRows = false;
            this.dgvLichSu.AllowUserToDeleteRows = false;
            this.dgvLichSu.BackgroundColor = System.Drawing.Color.White;
            this.dgvLichSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLichSu.Location = new System.Drawing.Point(0, 120);
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.ReadOnly = true;
            this.dgvLichSu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSu.Size = new System.Drawing.Size(1200, 520);
            this.dgvLichSu.TabIndex = 1;
            this.dgvLichSu.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLichSu_CellDoubleClick);
            // 
            // pnlThongKe
            // 
            this.pnlThongKe.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlThongKe.Controls.Add(this.lblTongDoanhThu);
            this.pnlThongKe.Controls.Add(this.lblTongHoaDon);
            this.pnlThongKe.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlThongKe.Location = new System.Drawing.Point(0, 640);
            this.pnlThongKe.Name = "pnlThongKe";
            this.pnlThongKe.Size = new System.Drawing.Size(1200, 60);
            this.pnlThongKe.TabIndex = 2;
            // 
            // lblTongHoaDon
            // 
            this.lblTongHoaDon.AutoSize = true;
            this.lblTongHoaDon.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongHoaDon.Location = new System.Drawing.Point(20, 20);
            this.lblTongHoaDon.Name = "lblTongHoaDon";
            this.lblTongHoaDon.Size = new System.Drawing.Size(140, 18);
            this.lblTongHoaDon.TabIndex = 0;
            this.lblTongHoaDon.Text = "Tổng hóa đơn: 0";
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.AutoSize = true;
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongDoanhThu.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTongDoanhThu.Location = new System.Drawing.Point(400, 20);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(180, 18);
            this.lblTongDoanhThu.TabIndex = 1;
            this.lblTongDoanhThu.Text = "Tổng doanh thu: 0 đ";
            // 
            // LichSuBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.Name = "LichSuBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LỊCH SỬ BÀN - HÓA ĐƠN";
            this.Load += new System.EventHandler(this.LichSuBan_Load);
            this.pnlMain.ResumeLayout(false);
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            this.pnlThongKe.ResumeLayout(false);
            this.pnlThongKe.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label lblMaBan;
        private System.Windows.Forms.TextBox txtMaBan;
        private System.Windows.Forms.Label lblTenKH;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.DataGridView dgvLichSu;
        private System.Windows.Forms.Panel pnlThongKe;
        private System.Windows.Forms.Label lblTongHoaDon;
        private System.Windows.Forms.Label lblTongDoanhThu;
    }
}