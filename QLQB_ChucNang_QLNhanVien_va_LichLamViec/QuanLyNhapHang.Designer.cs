namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    partial class QuanLyNhapHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvPhieuNhap;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblTongGiaTri;
        private System.Windows.Forms.ComboBox cbThang;
        private System.Windows.Forms.ComboBox cbNam;
        private System.Windows.Forms.DataGridView dgvCanhBao;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Label lblCanhBao;
        private System.Windows.Forms.Label lblChonThang;
        private System.Windows.Forms.Label lblChonNam;
        private System.Windows.Forms.GroupBox grpThaoTac;
        private System.Windows.Forms.GroupBox grpThongKe;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvPhieuNhap = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblTongGiaTri = new System.Windows.Forms.Label();
            this.cbThang = new System.Windows.Forms.ComboBox();
            this.cbNam = new System.Windows.Forms.ComboBox();
            this.dgvCanhBao = new System.Windows.Forms.DataGridView();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.lblCanhBao = new System.Windows.Forms.Label();
            this.lblChonThang = new System.Windows.Forms.Label();
            this.lblChonNam = new System.Windows.Forms.Label();
            this.grpThaoTac = new System.Windows.Forms.GroupBox();
            this.grpThongKe = new System.Windows.Forms.GroupBox();

            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuNhap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBao)).BeginInit();
            this.topPanel.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.grpThaoTac.SuspendLayout();
            this.grpThongKe.SuspendLayout();
            this.SuspendLayout();

            // 
            // topPanel - Header
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
            this.topPanel.Controls.Add(this.lblTitle);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1200, 80);
            this.topPanel.TabIndex = 0;

            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Gold;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1200, 80);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🏪 QUẢN LÝ NHẬP KHO - QUÁN BAR";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // leftPanel - Thao tác & Thống kê
            // 
            this.leftPanel.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.leftPanel.Controls.Add(this.grpThongKe);
            this.leftPanel.Controls.Add(this.grpThaoTac);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 80);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Padding = new System.Windows.Forms.Padding(15);
            this.leftPanel.Size = new System.Drawing.Size(280, 620);
            this.leftPanel.TabIndex = 1;

            // 
            // grpThaoTac - Nhóm thao tác
            // 
            this.grpThaoTac.Controls.Add(this.btnLamMoi);
            this.grpThaoTac.Controls.Add(this.btnXoa);
            this.grpThaoTac.Controls.Add(this.btnThem);
            this.grpThaoTac.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpThaoTac.ForeColor = System.Drawing.Color.Gold;
            this.grpThaoTac.Location = new System.Drawing.Point(15, 15);
            this.grpThaoTac.Name = "grpThaoTac";
            this.grpThaoTac.Size = new System.Drawing.Size(250, 220);
            this.grpThaoTac.TabIndex = 0;
            this.grpThaoTac.TabStop = false;
            this.grpThaoTac.Text = "⚙️ THAO TÁC";

            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.Gold;
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.Black;
            this.btnThem.Location = new System.Drawing.Point(20, 35);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(210, 50);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "➕ LẬP PHIẾU NHẬP";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatAppearance.BorderSize = 0;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(20, 95);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(210, 50);
            this.btnXoa.TabIndex = 1;
            this.btnXoa.Text = "🗑️ XÓA PHIẾU NHẬP";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(20, 155);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(210, 50);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "🔄 LÀM MỚI DỮ LIỆU";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // 
            // grpThongKe - Nhóm thống kê
            // 
            this.grpThongKe.Controls.Add(this.lblTongGiaTri);
            this.grpThongKe.Controls.Add(this.cbNam);
            this.grpThongKe.Controls.Add(this.lblChonNam);
            this.grpThongKe.Controls.Add(this.cbThang);
            this.grpThongKe.Controls.Add(this.lblChonThang);
            this.grpThongKe.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpThongKe.ForeColor = System.Drawing.Color.Gold;
            this.grpThongKe.Location = new System.Drawing.Point(15, 250);
            this.grpThongKe.Name = "grpThongKe";
            this.grpThongKe.Size = new System.Drawing.Size(250, 250);
            this.grpThongKe.TabIndex = 1;
            this.grpThongKe.TabStop = false;
            this.grpThongKe.Text = "📊 THỐNG KÊ NHẬP HÀNG";

            // 
            // lblChonThang
            // 
            this.lblChonThang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblChonThang.ForeColor = System.Drawing.Color.White;
            this.lblChonThang.Location = new System.Drawing.Point(20, 35);
            this.lblChonThang.Name = "lblChonThang";
            this.lblChonThang.Size = new System.Drawing.Size(60, 25);
            this.lblChonThang.TabIndex = 0;
            this.lblChonThang.Text = "Tháng:";
            this.lblChonThang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // cbThang
            // 
            this.cbThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbThang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbThang.FormattingEnabled = true;
            this.cbThang.Location = new System.Drawing.Point(85, 35);
            this.cbThang.Name = "cbThang";
            this.cbThang.Size = new System.Drawing.Size(145, 25);
            this.cbThang.TabIndex = 1;
            for (int i = 1; i <= 12; i++) this.cbThang.Items.Add(i);
            this.cbThang.SelectedIndexChanged += (s, e) => LoadTongGiaTriNhap();

            // 
            // lblChonNam
            // 
            this.lblChonNam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblChonNam.ForeColor = System.Drawing.Color.White;
            this.lblChonNam.Location = new System.Drawing.Point(20, 75);
            this.lblChonNam.Name = "lblChonNam";
            this.lblChonNam.Size = new System.Drawing.Size(60, 25);
            this.lblChonNam.TabIndex = 2;
            this.lblChonNam.Text = "Năm:";
            this.lblChonNam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // cbNam
            // 
            this.cbNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbNam.FormattingEnabled = true;
            this.cbNam.Location = new System.Drawing.Point(85, 75);
            this.cbNam.Name = "cbNam";
            this.cbNam.Size = new System.Drawing.Size(145, 25);
            this.cbNam.TabIndex = 3;
            for (int y = 2020; y <= System.DateTime.Now.Year + 1; y++) this.cbNam.Items.Add(y);
            this.cbNam.SelectedIndexChanged += (s, e) => LoadTongGiaTriNhap();

            // 
            // lblTongGiaTri
            // 
            this.lblTongGiaTri.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongGiaTri.ForeColor = System.Drawing.Color.Gold;
            this.lblTongGiaTri.Location = new System.Drawing.Point(20, 120);
            this.lblTongGiaTri.Name = "lblTongGiaTri";
            this.lblTongGiaTri.Size = new System.Drawing.Size(210, 110);
            this.lblTongGiaTri.TabIndex = 4;
            this.lblTongGiaTri.Text = "Tổng giá trị nhập:\n0 VNĐ";
            this.lblTongGiaTri.TextAlign = System.Drawing.ContentAlignment.TopLeft;

            // 
            // dgvPhieuNhap - Bảng chính ở giữa
            // 
            this.dgvPhieuNhap.AllowUserToAddRows = false;
            this.dgvPhieuNhap.AllowUserToDeleteRows = false;
            this.dgvPhieuNhap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhieuNhap.BackgroundColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.dgvPhieuNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPhieuNhap.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvPhieuNhap.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Gold;
            this.dgvPhieuNhap.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.dgvPhieuNhap.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvPhieuNhap.ColumnHeadersHeight = 40;
            this.dgvPhieuNhap.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvPhieuNhap.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Gold;
            this.dgvPhieuNhap.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvPhieuNhap.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvPhieuNhap.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvPhieuNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhieuNhap.EnableHeadersVisualStyles = false;
            this.dgvPhieuNhap.ForeColor = System.Drawing.Color.White;
            this.dgvPhieuNhap.GridColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.dgvPhieuNhap.Location = new System.Drawing.Point(280, 80);
            this.dgvPhieuNhap.MultiSelect = false;
            this.dgvPhieuNhap.Name = "dgvPhieuNhap";
            this.dgvPhieuNhap.ReadOnly = true;
            this.dgvPhieuNhap.RowTemplate.Height = 35;
            this.dgvPhieuNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPhieuNhap.Size = new System.Drawing.Size(920, 420);
            this.dgvPhieuNhap.TabIndex = 2;
            this.dgvPhieuNhap.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPhieuNhap_ColumnHeaderMouseClick);

            // 
            // bottomPanel - Cảnh báo tồn kho
            // 
            this.bottomPanel.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.bottomPanel.Controls.Add(this.dgvCanhBao);
            this.bottomPanel.Controls.Add(this.lblCanhBao);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(280, 500);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Padding = new System.Windows.Forms.Padding(15, 10, 15, 15);
            this.bottomPanel.Size = new System.Drawing.Size(920, 200);
            this.bottomPanel.TabIndex = 3;

            // 
            // lblCanhBao
            // 
            this.lblCanhBao.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCanhBao.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCanhBao.ForeColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.lblCanhBao.Location = new System.Drawing.Point(15, 10);
            this.lblCanhBao.Name = "lblCanhBao";
            this.lblCanhBao.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblCanhBao.Size = new System.Drawing.Size(890, 35);
            this.lblCanhBao.TabIndex = 0;
            this.lblCanhBao.Text = "⚠️ CẢNH BÁO TỒN KHO THẤP";
            this.lblCanhBao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // dgvCanhBao
            // 
            this.dgvCanhBao.AllowUserToAddRows = false;
            this.dgvCanhBao.AllowUserToDeleteRows = false;
            this.dgvCanhBao.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCanhBao.BackgroundColor = System.Drawing.Color.FromArgb(40, 40, 40);
            this.dgvCanhBao.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCanhBao.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.dgvCanhBao.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.dgvCanhBao.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvCanhBao.ColumnHeadersHeight = 35;
            this.dgvCanhBao.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvCanhBao.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.dgvCanhBao.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCanhBao.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvCanhBao.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvCanhBao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCanhBao.EnableHeadersVisualStyles = false;
            this.dgvCanhBao.ForeColor = System.Drawing.Color.White;
            this.dgvCanhBao.GridColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.dgvCanhBao.Location = new System.Drawing.Point(15, 45);
            this.dgvCanhBao.Name = "dgvCanhBao";
            this.dgvCanhBao.ReadOnly = true;
            this.dgvCanhBao.RowTemplate.Height = 30;
            this.dgvCanhBao.Size = new System.Drawing.Size(890, 140);
            this.dgvCanhBao.TabIndex = 1;

            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.dgvPhieuNhap);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.topPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý nhập kho - Quán Bar";
            this.Load += new System.EventHandler(this.QuanLyNhapHang_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvPhieuNhap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBao)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.leftPanel.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.grpThaoTac.ResumeLayout(false);
            this.grpThongKe.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}