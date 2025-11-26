using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.TabPage tabMenu;

        // Menu chính
        private System.Windows.Forms.Panel pnlMenuMain;
        private System.Windows.Forms.Button btnMenuQLNV;
        private System.Windows.Forms.Button btnMenuChamCong;
        private System.Windows.Forms.Button btnMenuTinhLuong;
        private System.Windows.Forms.Button btnMenuLichLV;

        // Tab Quản lý nhân viên
        private System.Windows.Forms.DataGridView dgvNhanVien;
        private System.Windows.Forms.Panel pnlQLNV;
        private System.Windows.Forms.Button btnQLNV_Them;
        private System.Windows.Forms.Button btnQLNV_Xoa;
        private System.Windows.Forms.Button btnQLNV_Sua;
        private System.Windows.Forms.Button btnQLNV_Save;
        private System.Windows.Forms.Button btnQLNV_Cancel;
        private System.Windows.Forms.Button btnQLNV_BackMenu;

        // Tab Chấm công
        private System.Windows.Forms.DataGridView dgvChamCong;
        private System.Windows.Forms.Panel pnlChamCong;
        private System.Windows.Forms.Button btnCC_Them;
        private System.Windows.Forms.Button btnCC_Xoa;
        private System.Windows.Forms.Button btnCC_Sua;
        private System.Windows.Forms.Button btnCC_Save;
        private System.Windows.Forms.Button btnCC_Cancel;
        private System.Windows.Forms.Button btnCC_BackMenu;
        private System.Windows.Forms.DateTimePicker dtpChamCong;
        private System.Windows.Forms.ComboBox cboNhanVienCC;

        // Tab Tính lương
        private System.Windows.Forms.DataGridView dgvTinhLuong;
        private System.Windows.Forms.Panel pnlTinhLuong;
        private System.Windows.Forms.Button btnTL_TinhLuong;
        private System.Windows.Forms.Button btnTL_Xoa;
        private System.Windows.Forms.Button btnTL_Save;
        private System.Windows.Forms.Button btnTL_Cancel;
        private System.Windows.Forms.Button btnTL_BackMenu;
        private System.Windows.Forms.ComboBox cboNhanVienTL;
        private System.Windows.Forms.NumericUpDown nudThang;
        private System.Windows.Forms.NumericUpDown nudNam;
        private System.Windows.Forms.NumericUpDown nudThuongPhat;

        // Tab Lịch làm việc
        private System.Windows.Forms.DataGridView dgvLichLamViec;
        private System.Windows.Forms.Panel pnlLichLV;
        private System.Windows.Forms.Button btnLLV_Them;
        private System.Windows.Forms.Button btnLLV_Xoa;
        private System.Windows.Forms.Button btnLLV_Sua;
        private System.Windows.Forms.Button btnLLV_Save;
        private System.Windows.Forms.Button btnLLV_Cancel;
        private System.Windows.Forms.Button btnLLV_BackMenu;
        
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
        /// 
        //private void InitializeLichLamViecTab()
        //{
        //    splitContainer = new SplitContainer();  
        //    splitContainer.Dock = DockStyle.Fill;
        //    splitContainer.Orientation = Orientation.Vertical;

        //    splitContainer.SplitterDistance = 350;     
        //    splitContainer.SplitterWidth = 4;          
        //    splitContainer.IsSplitterFixed = false;    
        //    // Panel Left - Ca làm việc
        //    Panel pnlLeft = new Panel();
        //    pnlLeft.Dock = DockStyle.Fill;

        //    Label lblCaLam = new Label();
        //    lblCaLam.Text = "📋 DANH SÁCH CA LÀM VIỆC";
        //    lblCaLam.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        //    lblCaLam.ForeColor = Color.FromArgb(52, 73, 94);
        //    lblCaLam.Location = new Point(20, 20);
        //    lblCaLam.AutoSize = true;

        //    dgvCaLam = new DataGridView();
        //    dgvCaLam.Location = new Point(20, 45); 
        //    dgvCaLam.Size = new Size(450, 200);
        //    dgvCaLam.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //    dgvCaLam.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    dgvCaLam.ReadOnly = true;
        //    dgvCaLam.AllowUserToAddRows = false;
        //    dgvCaLam.RowTemplate.Height = 30;
        //    dgvCaLam.SelectionChanged += dgvCaLam_SelectionChanged;

        //    // Panel input ca làm mới
        //    Panel pnlCaInput = new Panel();
        //    pnlCaInput.Location = new Point(20, 250);
        //    pnlCaInput.Size = new Size(450, 120);
        //    pnlCaInput.BackColor = Color.FromArgb(236, 240, 241);
        //    pnlCaInput.BorderStyle = BorderStyle.FixedSingle;

        //    Label lblCaInputTitle = new Label();
        //    lblCaInputTitle.Text = "🕐 THÊM/SỬA CA LÀM";
        //    lblCaInputTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        //    lblCaInputTitle.Location = new Point(10, 10);
        //    lblCaInputTitle.AutoSize = true;

        //    Label lblMaCa = new Label();
        //    lblMaCa.Text = "Mã ca:";
        //    lblMaCa.Location = new Point(15, 45);
        //    lblMaCa.AutoSize = true;

        //    txtMaCaNew = new TextBox();
        //    txtMaCaNew.Location = new Point(80, 42);
        //    txtMaCaNew.Width = 80;

        //    Label lblGioBD = new Label();
        //    lblGioBD.Text = "Giờ BĐ:";
        //    lblGioBD.Location = new Point(180, 45);
        //    lblGioBD.AutoSize = true;

        //    txtGioBDNew = new TextBox();
        //    txtGioBDNew.Location = new Point(250, 42);
        //    txtGioBDNew.Width = 80;

        //    //
        //    DateTimePicker dtpGioBD = new DateTimePicker();
        //    dtpGioBD.Location = new Point(250, 42);
        //    dtpGioBD.Width = 80;
        //    dtpGioBD.Format = DateTimePickerFormat.Custom;
        //    dtpGioBD.CustomFormat = "HH:mm:ss";
        //    dtpGioBD.ShowUpDown = true;
        //    dtpGioBD.Value = DateTime.Today;
        //    //
        //    txtGioBDNew.ForeColor = Color.Gray;
        //    txtGioBDNew.Text = "HH:mm:ss";
        //    txtGioBDNew.Enter += (s, ev) => {
        //        if (txtGioBDNew.Text == "HH:mm:ss")
        //        {
        //            txtGioBDNew.Text = "";
        //            txtGioBDNew.ForeColor = Color.Black;
        //        }
        //    };
        //    txtGioBDNew.Leave += (s, ev) => {
        //        if (string.IsNullOrWhiteSpace(txtGioBDNew.Text))
        //        {
        //            txtGioBDNew.Text = "HH:mm:ss";
        //            txtGioBDNew.ForeColor = Color.Gray;
        //        }
        //    };

        //    Label lblGioKT = new Label();
        //    lblGioKT.Text = "Giờ KT:";
        //    lblGioKT.Location = new Point(15, 80);
        //    lblGioKT.AutoSize = true;

        //    txtGioKTNew = new TextBox();
        //    txtGioKTNew.Location = new Point(80, 77);
        //    txtGioKTNew.Width = 80;
        //    //
        //    DateTimePicker dtpGioKT = new DateTimePicker();
        //    dtpGioKT.Location = new Point(80, 77);
        //    dtpGioKT.Width = 80;
        //    dtpGioKT.Format = DateTimePickerFormat.Custom;
        //    dtpGioKT.CustomFormat = "HH:mm:ss";
        //    dtpGioKT.ShowUpDown = true;
        //    dtpGioKT.Value = DateTime.Today;
        //    //
        //    txtGioKTNew.ForeColor = Color.Gray;
        //    txtGioKTNew.Text = "HH:mm:ss";
        //    txtGioKTNew.Enter += (s, ev) => {
        //        if (txtGioKTNew.Text == "HH:mm:ss")
        //        {
        //            txtGioKTNew.Text = "";
        //            txtGioKTNew.ForeColor = Color.Black;
        //        }
        //    };
        //    txtGioKTNew.Leave += (s, ev) => {
        //        if (string.IsNullOrWhiteSpace(txtGioKTNew.Text))
        //        {
        //            txtGioKTNew.Text = "HH:mm:ss";
        //            txtGioKTNew.ForeColor = Color.Gray;
        //        }
        //    };

        //    pnlCaInput.Controls.AddRange(new Control[] {
        //        lblCaInputTitle, lblMaCa, txtMaCaNew, lblGioBD, dtpGioBD,
        //        lblGioKT, dtpGioKT
        //    });

        //    // Panel thống kê
        //    pnlThongKeCa = new Panel();
        //    pnlThongKeCa.Location = new Point(20, 375);
        //    pnlThongKeCa.Size = new Size(450, 100);
        //    pnlThongKeCa.BackColor = Color.FromArgb(236, 240, 241);
        //    pnlThongKeCa.BorderStyle = BorderStyle.FixedSingle;

        //    Label lblThongKeTitle = new Label();
        //    lblThongKeTitle.Text = "📊 THỐNG KÊ";
        //    lblThongKeTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        //    lblThongKeTitle.ForeColor = Color.FromArgb(52, 73, 94);
        //    lblThongKeTitle.Location = new Point(10, 10);
        //    lblThongKeTitle.AutoSize = true;

        //    Label lblTongCa = new Label();
        //    lblTongCa.Name = "lblTongCa";
        //    lblTongCa.Text = "Tổng số ca: 0";
        //    lblTongCa.Font = new Font("Segoe UI", 10);
        //    lblTongCa.Location = new Point(20, 45);
        //    lblTongCa.AutoSize = true;

        //    Label lblTongNV = new Label();
        //    lblTongNV.Name = "lblTongNV";
        //    lblTongNV.Text = "Tổng số nhân viên: 0";
        //    lblTongNV.Font = new Font("Segoe UI", 10);
        //    lblTongNV.Location = new Point(20, 70);
        //    lblTongNV.AutoSize = true;

        //    pnlThongKeCa.Controls.AddRange(new Control[] { lblThongKeTitle, lblTongCa, lblTongNV });

        //    pnlLeft.Controls.AddRange(new Control[] { lblCaLam, dgvCaLam, pnlCaInput, pnlThongKeCa });
        //    splitContainer.Panel1.Controls.Add(pnlLeft);

        //    // Panel Right - Nhân viên theo ca
        //    Panel pnlRight = new Panel();
        //    pnlRight.Dock = DockStyle.Fill;

        //    Label lblNhanVienCa = new Label();
        //    lblNhanVienCa.Text = "👥 NHÂN VIÊN THEO CA";
        //    lblNhanVienCa.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        //    lblNhanVienCa.ForeColor = Color.FromArgb(52, 73, 94);
        //    lblNhanVienCa.Location = new Point(20, 20);
        //    lblNhanVienCa.AutoSize = true;

        //    dgvNhanVienCa = new DataGridView();
        //    dgvNhanVienCa.Location = new Point(20, 45);
        //    dgvNhanVienCa.Size = new Size(580, 325);
        //    dgvNhanVienCa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //    dgvNhanVienCa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //    dgvNhanVienCa.ReadOnly = true;
        //    dgvNhanVienCa.AllowUserToAddRows = false;
        //    dgvNhanVienCa.RowTemplate.Height = 30;

        //    // Panel điều chỉnh nhân viên ca
        //    Panel pnlNVCaInput = new Panel();
        //    pnlNVCaInput.Location = new Point(20, 375);
        //    pnlNVCaInput.Size = new Size(580, 80);
        //    pnlNVCaInput.BackColor = Color.FromArgb(236, 240, 241);
        //    pnlNVCaInput.BorderStyle = BorderStyle.FixedSingle;

        //    Label lblNVCaTitle = new Label();
        //    lblNVCaTitle.Text = "👤 ĐIỀU CHỈNH CA LÀM NHÂN VIÊN";
        //    lblNVCaTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        //    lblNVCaTitle.Location = new Point(10, 10);
        //    lblNVCaTitle.AutoSize = true;

        //    Label lblNVChon = new Label();
        //    lblNVChon.Text = "Nhân viên:";
        //    lblNVChon.Location = new Point(15, 45);
        //    lblNVChon.AutoSize = true;

        //    cboNhanVienCaLam = new ComboBox();
        //    cboNhanVienCaLam.Location = new Point(100, 42);
        //    cboNhanVienCaLam.Width = 200;
        //    cboNhanVienCaLam.DropDownStyle = ComboBoxStyle.DropDownList;

        //    Label lblCaChon = new Label();
        //    lblCaChon.Text = "Ca làm:";
        //    lblCaChon.Location = new Point(320, 45);
        //    lblCaChon.AutoSize = true;

        //    cboCaLamChon = new ComboBox();
        //    cboCaLamChon.Location = new Point(380, 42);
        //    cboCaLamChon.Width = 100;
        //    cboCaLamChon.DropDownStyle = ComboBoxStyle.DropDownList;

        //    pnlNVCaInput.Controls.AddRange(new Control[] {
        //        lblNVCaTitle, lblNVChon, cboNhanVienCaLam, lblCaChon, cboCaLamChon
        //    });

        //    pnlRight.Controls.AddRange(new Control[] { lblNhanVienCa, dgvNhanVienCa, pnlNVCaInput });
        //    splitContainer.Panel2.Controls.Add(pnlRight);

        //    // Panel Filter
        //    Panel pnlFilter = new Panel();
        //    pnlFilter.Dock = DockStyle.Top;
        //    pnlFilter.Height = 70;
        //    pnlFilter.BackColor = Color.FromArgb(245, 245, 245);

        //    Label lblLocNgay = new Label();
        //    lblLocNgay.Text = "🗓️ Lọc theo ngày:";
        //    lblLocNgay.Location = new Point(20, 25);
        //    lblLocNgay.AutoSize = true;
        //    lblLocNgay.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        //    dtpLocNgay = new DateTimePicker();
        //    dtpLocNgay.Location = new Point(140, 22);
        //    dtpLocNgay.Width = 120;
        //    dtpLocNgay.Format = DateTimePickerFormat.Short;

        //    btnLocNgay = new Button();
        //    btnLocNgay.Text = "Lọc ngày";
        //    btnLocNgay.Location = new Point(270, 20);
        //    btnLocNgay.Size = new Size(90, 27);
        //    btnLocNgay.BackColor = Color.FromArgb(52, 152, 219);
        //    btnLocNgay.ForeColor = Color.White;
        //    btnLocNgay.FlatStyle = FlatStyle.Flat;
        //    btnLocNgay.Cursor = Cursors.Hand;
        //    btnLocNgay.Click += btnLocNgay_Click;

        //    Label lblLocThang = new Label();
        //    lblLocThang.Text = "📅 Tháng:";
        //    lblLocThang.Location = new Point(390, 25);
        //    lblLocThang.AutoSize = true;
        //    lblLocThang.Font = new Font("Segoe UI", 9, FontStyle.Bold);

        //    nudLocThang = new NumericUpDown();
        //    nudLocThang.Location = new Point(460, 22);
        //    nudLocThang.Width = 60;
        //    nudLocThang.Minimum = 1;
        //    nudLocThang.Maximum = 12;
        //    nudLocThang.Value = DateTime.Now.Month;

        //    Label lblLocNam = new Label();
        //    lblLocNam.Text = "Năm:";
        //    lblLocNam.Location = new Point(540, 25);
        //    lblLocNam.AutoSize = true;

        //    nudLocNam = new NumericUpDown();
        //    nudLocNam.Location = new Point(585, 22);
        //    nudLocNam.Width = 80;
        //    nudLocNam.Minimum = 2020;
        //    nudLocNam.Maximum = 2100;
        //    nudLocNam.Value = DateTime.Now.Year;

        //    btnLocThang = new Button();
        //    btnLocThang.Text = "Lọc tháng";
        //    btnLocThang.Location = new Point(675, 20);
        //    btnLocThang.Size = new Size(90, 27);
        //    btnLocThang.BackColor = Color.FromArgb(52, 152, 219);
        //    btnLocThang.ForeColor = Color.White;
        //    btnLocThang.FlatStyle = FlatStyle.Flat;
        //    btnLocThang.Cursor = Cursors.Hand;
        //    btnLocThang.Click += btnLocThang_Click;

        //    btnHuyLocCa = new Button();
        //    btnHuyLocCa.Text = "Hủy lọc";
        //    btnHuyLocCa.Location = new Point(775, 20);
        //    btnHuyLocCa.Size = new Size(90, 27);
        //    btnHuyLocCa.BackColor = Color.FromArgb(189, 195, 199);
        //    btnHuyLocCa.ForeColor = Color.White;
        //    btnHuyLocCa.FlatStyle = FlatStyle.Flat;
        //    btnHuyLocCa.Cursor = Cursors.Hand;
        //    btnHuyLocCa.Click += btnHuyLocCa_Click;

        //    pnlFilter.Controls.AddRange(new Control[] {
        //        lblLocNgay, dtpLocNgay, btnLocNgay,
        //        lblLocThang, nudLocThang, lblLocNam, nudLocNam,
        //        btnLocThang, btnHuyLocCa
        //    });

        //    // Panel Bottom
        //    pnlLichLV = new Panel();
        //    pnlLichLV.Dock = DockStyle.Bottom;
        //    pnlLichLV.Height = 60;
        //    pnlLichLV.BackColor = Color.WhiteSmoke;

        //    btnLLV_ThemCa = CreateButton("➕ Thêm ca", 20, Color.FromArgb(46, 204, 113));
        //    btnLLV_SuaCa = CreateButton("✏️ Sửa ca", 140, Color.FromArgb(241, 196, 15));
        //    btnLLV_XoaCa = CreateButton("🗑️ Xóa ca", 260, Color.FromArgb(231, 76, 60));
        //    btnLLV_SaveCa = CreateButton("💾 Lưu ca", 380, Color.FromArgb(52, 152, 219));
        //    btnLLV_CancelCa = CreateButton("↶ Hủy", 500, Color.FromArgb(149, 165, 166));
        //    btnLLV_ThemNVCa = CreateButton("👤+ Gán NV", 620, Color.FromArgb(155, 89, 182));
        //    btnLLV_XoaNVCa = CreateButton("👤✖ Bỏ NV", 740, Color.FromArgb(192, 57, 43));
        //    btnLLV_BackMenu = CreateButton("← Menu", 860, Color.FromArgb(52, 73, 94));

        //    pnlLichLV.Controls.AddRange(new Control[] {
        //        btnLLV_ThemCa, btnLLV_SuaCa, btnLLV_XoaCa, btnLLV_SaveCa, btnLLV_CancelCa,
        //        btnLLV_ThemNVCa, btnLLV_XoaNVCa, btnLLV_BackMenu
        //    });

        //    tabLichLamViec.Controls.Add(splitContainer);
        //    tabLichLamViec.Controls.Add(pnlFilter);
        //    tabLichLamViec.Controls.Add(pnlLichLV);

        //    btnLLV_ThemCa.Click += btnLLV_ThemCa_Click;
        //    btnLLV_SuaCa.Click += btnLLV_SuaCa_Click;
        //    btnLLV_XoaCa.Click += btnLLV_XoaCa_Click;
        //    btnLLV_SaveCa.Click += btnLLV_SaveCa_Click;
        //    btnLLV_CancelCa.Click += btnLLV_CancelCa_Click;
        //    btnLLV_BackMenu.Click += (s, ev) => tabControl.SelectedTab = tabMenu;
        //}
        private void InitializeLichLamViecTab()
        {
            splitContainer = new SplitContainer();
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Orientation = Orientation.Vertical;
            splitContainer.SplitterDistance = 350;
            splitContainer.SplitterWidth = 4;
            splitContainer.IsSplitterFixed = false;

            // ============ PANEL TRÁI ============
            Panel pnlLeft = new Panel();
            pnlLeft.Dock = DockStyle.Fill;

            Label lblCaLam = new Label();
            lblCaLam.Text = "📋 DANH SÁCH CA LÀM VIỆC";
            lblCaLam.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblCaLam.ForeColor = Color.FromArgb(52, 73, 94);
            lblCaLam.Location = new Point(20, 20);
            lblCaLam.AutoSize = true;

            dgvCaLam = new DataGridView();
            dgvCaLam.Location = new Point(20, 45);
            dgvCaLam.Size = new Size(450, 200);
            dgvCaLam.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCaLam.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCaLam.ReadOnly = true;
            dgvCaLam.AllowUserToAddRows = false;
            dgvCaLam.RowHeadersVisible = false;
            dgvCaLam.RowTemplate.Height = 30;
            dgvCaLam.SelectionChanged += dgvCaLam_SelectionChanged;

            // ============ PANEL INPUT CA LÀM ============
            pnlCaInput = new Panel();
            pnlCaInput.Location = new Point(20, 250);
            pnlCaInput.Size = new Size(450, 120);
            pnlCaInput.BackColor = Color.FromArgb(236, 240, 241);
            pnlCaInput.BorderStyle = BorderStyle.FixedSingle;

            Label lblCaInputTitle = new Label();
            lblCaInputTitle.Text = "🕐 THÊM/SỬA CA LÀM";
            lblCaInputTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblCaInputTitle.Location = new Point(10, 10);
            lblCaInputTitle.AutoSize = true;

            Label lblMaCa = new Label();
            lblMaCa.Text = "Mã ca:";
            lblMaCa.Location = new Point(15, 45);
            lblMaCa.AutoSize = true;

            txtMaCaNew = new TextBox();
            txtMaCaNew.Location = new Point(80, 42);
            txtMaCaNew.Width = 80;

            Label lblGioBD = new Label();
            lblGioBD.Text = "Giờ BĐ:";
            lblGioBD.Location = new Point(180, 45);
            lblGioBD.AutoSize = true;

            dtpGioBD = new DateTimePicker();
            dtpGioBD.Location = new Point(250, 42);
            dtpGioBD.Width = 100;
            dtpGioBD.Format = DateTimePickerFormat.Custom;
            dtpGioBD.CustomFormat = "HH:mm:ss";
            dtpGioBD.ShowUpDown = true;
            dtpGioBD.Value = DateTime.Today.AddHours(13);

            Label lblGioKT = new Label();
            lblGioKT.Text = "Giờ KT:";
            lblGioKT.Location = new Point(15, 80);
            lblGioKT.AutoSize = true;

            dtpGioKT = new DateTimePicker();
            dtpGioKT.Location = new Point(80, 77);
            dtpGioKT.Width = 100;
            dtpGioKT.Format = DateTimePickerFormat.Custom;
            dtpGioKT.CustomFormat = "HH:mm:ss";
            dtpGioKT.ShowUpDown = true;
            dtpGioKT.Value = DateTime.Today.AddHours(21);

            pnlCaInput.Controls.AddRange(new Control[] {
        lblCaInputTitle, lblMaCa, txtMaCaNew, lblGioBD, dtpGioBD,
        lblGioKT, dtpGioKT
    });

            // ============ PANEL THỐNG KÊ ============
            pnlThongKeCa = new Panel();
            pnlThongKeCa.Location = new Point(20, 375);
            pnlThongKeCa.Size = new Size(450, 100);
            pnlThongKeCa.BackColor = Color.FromArgb(236, 240, 241);
            pnlThongKeCa.BorderStyle = BorderStyle.FixedSingle;

            Label lblThongKeTitle = new Label();
            lblThongKeTitle.Text = "📊 THỐNG KÊ";
            lblThongKeTitle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblThongKeTitle.ForeColor = Color.FromArgb(52, 73, 94);
            lblThongKeTitle.Location = new Point(10, 10);
            lblThongKeTitle.AutoSize = true;

            Label lblTongCa = new Label();
            lblTongCa.Name = "lblTongCa";
            lblTongCa.Text = "Tổng số ca: 0";
            lblTongCa.Font = new Font("Segoe UI", 10);
            lblTongCa.Location = new Point(20, 45);
            lblTongCa.AutoSize = true;

            Label lblTongNV = new Label();
            lblTongNV.Name = "lblTongNV";
            lblTongNV.Text = "Tổng số nhân viên: 0";
            lblTongNV.Font = new Font("Segoe UI", 10);
            lblTongNV.Location = new Point(20, 70);
            lblTongNV.AutoSize = true;

            pnlThongKeCa.Controls.AddRange(new Control[] { lblThongKeTitle, lblTongCa, lblTongNV });

            pnlLeft.Controls.AddRange(new Control[] { lblCaLam, dgvCaLam, pnlCaInput, pnlThongKeCa });
            splitContainer.Panel1.Controls.Add(pnlLeft);

            // ============ PANEL PHẢI ============
            Panel pnlRight = new Panel();
            pnlRight.Dock = DockStyle.Fill;

            Label lblNhanVienCa = new Label();
            lblNhanVienCa.Text = "👥 NHÂN VIÊN THEO CA";
            lblNhanVienCa.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblNhanVienCa.ForeColor = Color.FromArgb(52, 73, 94);
            lblNhanVienCa.Location = new Point(20, 20);
            lblNhanVienCa.AutoSize = true;

            dgvNhanVienCa = new DataGridView();
            dgvNhanVienCa.Location = new Point(20, 45);
            dgvNhanVienCa.Size = new Size(580, 325);
            dgvNhanVienCa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVienCa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVienCa.ReadOnly = true;
            dgvNhanVienCa.AllowUserToAddRows = false;
            dgvNhanVienCa.RowHeadersVisible = false;
            dgvNhanVienCa.RowTemplate.Height = 30;

            // ============ PANEL ĐIỀU CHỈNH NV CA ============
            Panel pnlNVCaInput = new Panel();
            pnlNVCaInput.Location = new Point(20, 375);
            pnlNVCaInput.Size = new Size(580, 80);
            pnlNVCaInput.BackColor = Color.FromArgb(236, 240, 241);
            pnlNVCaInput.BorderStyle = BorderStyle.FixedSingle;

            Label lblNVCaTitle = new Label();
            lblNVCaTitle.Text = "👤 ĐIỀU CHỈNH CA LÀM NHÂN VIÊN";
            lblNVCaTitle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblNVCaTitle.Location = new Point(10, 10);
            lblNVCaTitle.AutoSize = true;

            Label lblNVChon = new Label();
            lblNVChon.Text = "Nhân viên:";
            lblNVChon.Location = new Point(15, 45);
            lblNVChon.AutoSize = true;

            cboNhanVienCaLam = new ComboBox();
            cboNhanVienCaLam.Location = new Point(100, 42);
            cboNhanVienCaLam.Width = 200;
            cboNhanVienCaLam.DropDownStyle = ComboBoxStyle.DropDownList;

            Label lblCaChon = new Label();
            lblCaChon.Text = "Ca làm:";
            lblCaChon.Location = new Point(320, 45);
            lblCaChon.AutoSize = true;

            cboCaLamChon = new ComboBox();
            cboCaLamChon.Location = new Point(380, 42);
            cboCaLamChon.Width = 100;
            cboCaLamChon.DropDownStyle = ComboBoxStyle.DropDownList;

            pnlNVCaInput.Controls.AddRange(new Control[] {
        lblNVCaTitle, lblNVChon, cboNhanVienCaLam, lblCaChon, cboCaLamChon
    });

            pnlRight.Controls.AddRange(new Control[] { lblNhanVienCa, dgvNhanVienCa, pnlNVCaInput });
            splitContainer.Panel2.Controls.Add(pnlRight);

            // ============ PANEL FILTER ============
            Panel pnlFilter = new Panel();
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Height = 70;
            pnlFilter.BackColor = Color.FromArgb(245, 245, 245);

            Label lblLocNgay = new Label();
            lblLocNgay.Text = "🗓️ Lọc theo ngày:";
            lblLocNgay.Location = new Point(20, 25);
            lblLocNgay.AutoSize = true;
            lblLocNgay.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            dtpLocNgay = new DateTimePicker();
            dtpLocNgay.Location = new Point(140, 22);
            dtpLocNgay.Width = 120;
            dtpLocNgay.Format = DateTimePickerFormat.Short;

            btnLocNgay = new Button();
            btnLocNgay.Text = "Lọc ngày";
            btnLocNgay.Location = new Point(270, 20);
            btnLocNgay.Size = new Size(90, 27);
            btnLocNgay.BackColor = Color.FromArgb(52, 152, 219);
            btnLocNgay.ForeColor = Color.White;
            btnLocNgay.FlatStyle = FlatStyle.Flat;
            btnLocNgay.Cursor = Cursors.Hand;
            btnLocNgay.Click += btnLocNgay_Click;

            Label lblLocThang = new Label();
            lblLocThang.Text = "📅 Tháng:";
            lblLocThang.Location = new Point(390, 25);
            lblLocThang.AutoSize = true;
            lblLocThang.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            nudLocThang = new NumericUpDown();
            nudLocThang.Location = new Point(460, 22);
            nudLocThang.Width = 60;
            nudLocThang.Minimum = 1;
            nudLocThang.Maximum = 12;
            nudLocThang.Value = DateTime.Now.Month;

            Label lblLocNam = new Label();
            lblLocNam.Text = "Năm:";
            lblLocNam.Location = new Point(540, 25);
            lblLocNam.AutoSize = true;

            nudLocNam = new NumericUpDown();
            nudLocNam.Location = new Point(585, 22);
            nudLocNam.Width = 80;
            nudLocNam.Minimum = 2020;
            nudLocNam.Maximum = 2100;
            nudLocNam.Value = DateTime.Now.Year;

            btnLocThang = new Button();
            btnLocThang.Text = "Lọc tháng";
            btnLocThang.Location = new Point(675, 20);
            btnLocThang.Size = new Size(90, 27);
            btnLocThang.BackColor = Color.FromArgb(52, 152, 219);
            btnLocThang.ForeColor = Color.White;
            btnLocThang.FlatStyle = FlatStyle.Flat;
            btnLocThang.Cursor = Cursors.Hand;
            btnLocThang.Click += btnLocThang_Click;

            btnHuyLocCa = new Button();
            btnHuyLocCa.Text = "Hủy lọc";
            btnHuyLocCa.Location = new Point(775, 20);
            btnHuyLocCa.Size = new Size(90, 27);
            btnHuyLocCa.BackColor = Color.FromArgb(189, 195, 199);
            btnHuyLocCa.ForeColor = Color.White;
            btnHuyLocCa.FlatStyle = FlatStyle.Flat;
            btnHuyLocCa.Cursor = Cursors.Hand;
            btnHuyLocCa.Click += btnHuyLocCa_Click;

            pnlFilter.Controls.AddRange(new Control[] {
        lblLocNgay, dtpLocNgay, btnLocNgay,
        lblLocThang, nudLocThang, lblLocNam, nudLocNam,
        btnLocThang, btnHuyLocCa
    });

            // ============ PANEL BOTTOM ============
            pnlLichLV = new Panel();
            pnlLichLV.Dock = DockStyle.Bottom;
            pnlLichLV.Height = 60;
            pnlLichLV.BackColor = Color.WhiteSmoke;

            btnLLV_ThemCa = CreateButton("➕ Thêm ca", 20, Color.FromArgb(46, 204, 113));
            btnLLV_SuaCa = CreateButton("✏️ Sửa ca", 140, Color.FromArgb(241, 196, 15));
            btnLLV_XoaCa = CreateButton("🗑️ Xóa ca", 260, Color.FromArgb(231, 76, 60));
            btnLLV_SaveCa = CreateButton("💾 Lưu ca", 380, Color.FromArgb(52, 152, 219));
            btnLLV_CancelCa = CreateButton("↶ Hủy", 500, Color.FromArgb(149, 165, 166));
            btnLLV_ThemNVCa = CreateButton("👤+ Gán NV", 620, Color.FromArgb(155, 89, 182));
            btnLLV_XoaNVCa = CreateButton("👤✖ Bỏ NV", 740, Color.FromArgb(192, 57, 43));
            btnLLV_BackMenu = CreateButton("← Menu", 860, Color.FromArgb(52, 73, 94));

            pnlLichLV.Controls.AddRange(new Control[] {
        btnLLV_ThemCa, btnLLV_SuaCa, btnLLV_XoaCa, btnLLV_SaveCa, btnLLV_CancelCa,
        btnLLV_ThemNVCa, btnLLV_XoaNVCa, btnLLV_BackMenu
    });

            // ============ ADD TO TAB ============
            tabLichLamViec.Controls.Add(splitContainer);
            tabLichLamViec.Controls.Add(pnlFilter);
            tabLichLamViec.Controls.Add(pnlLichLV);

            // ============ EVENT HANDLERS ============
            btnLLV_ThemCa.Click += btnLLV_ThemCa_Click;
            btnLLV_SuaCa.Click += btnLLV_SuaCa_Click;
            btnLLV_XoaCa.Click += btnLLV_XoaCa_Click;
            btnLLV_SaveCa.Click += btnLLV_SaveCa_Click;
            btnLLV_CancelCa.Click += btnLLV_CancelCa_Click;
            btnLLV_ThemNVCa.Click += btnLLV_ThemNVCa_Click;
            btnLLV_XoaNVCa.Click += btnLLV_XoaNVCa_Click;
            btnLLV_BackMenu.Click += (s, ev) => tabControl.SelectedTab = tabMenu;
        }
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabMenu = new System.Windows.Forms.TabPage();
            this.pnlMenuMain = new System.Windows.Forms.Panel();
            this.btnTrovemenu = new System.Windows.Forms.Button();
            this.btnMenuQLNV = new System.Windows.Forms.Button();
            this.btnMenuChamCong = new System.Windows.Forms.Button();
            this.btnMenuTinhLuong = new System.Windows.Forms.Button();
            this.btnMenuLichLV = new System.Windows.Forms.Button();
            this.tabQuanLyNV = new System.Windows.Forms.TabPage();
            this.tabChamCong = new System.Windows.Forms.TabPage();
            this.tabTinhLuong = new System.Windows.Forms.TabPage();
            this.tabLichLamViec = new System.Windows.Forms.TabPage();
            this.pnlHeader.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabMenu.SuspendLayout();
            this.pnlMenuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlHeader.Controls.Add(this.btnLogout);
            this.pnlHeader.Controls.Add(this.lblWelcome);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1144, 70);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(1002, 18);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(130, 40);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Đăng xuất";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(20, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(145, 30);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Chào mừng, ";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabMenu);
            this.tabControl.Controls.Add(this.tabQuanLyNV);
            this.tabControl.Controls.Add(this.tabChamCong);
            this.tabControl.Controls.Add(this.tabTinhLuong);
            this.tabControl.Controls.Add(this.tabLichLamViec);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 70);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1144, 630);
            this.tabControl.TabIndex = 1;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabMenu
            // 
            this.tabMenu.Controls.Add(this.pnlMenuMain);
            this.tabMenu.Location = new System.Drawing.Point(4, 26);
            this.tabMenu.Name = "tabMenu";
            this.tabMenu.Padding = new System.Windows.Forms.Padding(3);
            this.tabMenu.Size = new System.Drawing.Size(1136, 600);
            this.tabMenu.TabIndex = 0;
            this.tabMenu.Text = "Menu Chính";
            this.tabMenu.UseVisualStyleBackColor = true;
            // 
            // pnlMenuMain
            // 
            this.pnlMenuMain.Controls.Add(this.btnTrovemenu);
            this.pnlMenuMain.Controls.Add(this.btnMenuQLNV);
            this.pnlMenuMain.Controls.Add(this.btnMenuChamCong);
            this.pnlMenuMain.Controls.Add(this.btnMenuTinhLuong);
            this.pnlMenuMain.Controls.Add(this.btnMenuLichLV);
            this.pnlMenuMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMenuMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMenuMain.Name = "pnlMenuMain";
            this.pnlMenuMain.Size = new System.Drawing.Size(1130, 594);
            this.pnlMenuMain.TabIndex = 0;
            // 
            // btnTrovemenu
            // 
            this.btnTrovemenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.btnTrovemenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrovemenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrovemenu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTrovemenu.ForeColor = System.Drawing.Color.White;
            this.btnTrovemenu.Location = new System.Drawing.Point(1013, 566);
            this.btnTrovemenu.Name = "btnTrovemenu";
            this.btnTrovemenu.Size = new System.Drawing.Size(120, 35);
            this.btnTrovemenu.TabIndex = 6;
            this.btnTrovemenu.Text = "Trở về menu";
            this.btnTrovemenu.UseVisualStyleBackColor = false;
            this.btnTrovemenu.Click += new System.EventHandler(this.btnTrovemenu_Click);
            // 
            // btnMenuQLNV
            // 
            this.btnMenuQLNV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnMenuQLNV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuQLNV.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnMenuQLNV.ForeColor = System.Drawing.Color.White;
            this.btnMenuQLNV.Location = new System.Drawing.Point(211, 49);
            this.btnMenuQLNV.Name = "btnMenuQLNV";
            this.btnMenuQLNV.Size = new System.Drawing.Size(200, 200);
            this.btnMenuQLNV.TabIndex = 0;
            this.btnMenuQLNV.Text = "👥\r\n\r\nQuản Lý\r\nNhân Viên";
            this.btnMenuQLNV.UseVisualStyleBackColor = false;
            this.btnMenuQLNV.Click += new System.EventHandler(this.btnMenuQLNV_Click);
            // 
            // btnMenuChamCong
            // 
            this.btnMenuChamCong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnMenuChamCong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuChamCong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnMenuChamCong.ForeColor = System.Drawing.Color.White;
            this.btnMenuChamCong.Location = new System.Drawing.Point(726, 49);
            this.btnMenuChamCong.Name = "btnMenuChamCong";
            this.btnMenuChamCong.Size = new System.Drawing.Size(200, 200);
            this.btnMenuChamCong.TabIndex = 1;
            this.btnMenuChamCong.Text = "✓\r\n\r\nChấm Công";
            this.btnMenuChamCong.UseVisualStyleBackColor = false;
            this.btnMenuChamCong.Click += new System.EventHandler(this.btnMenuChamCong_Click);
            // 
            // btnMenuTinhLuong
            // 
            this.btnMenuTinhLuong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnMenuTinhLuong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuTinhLuong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnMenuTinhLuong.ForeColor = System.Drawing.Color.White;
            this.btnMenuTinhLuong.Location = new System.Drawing.Point(726, 298);
            this.btnMenuTinhLuong.Name = "btnMenuTinhLuong";
            this.btnMenuTinhLuong.Size = new System.Drawing.Size(200, 200);
            this.btnMenuTinhLuong.TabIndex = 2;
            this.btnMenuTinhLuong.Text = "💰\r\n\r\nTính Lương";
            this.btnMenuTinhLuong.UseVisualStyleBackColor = false;
            this.btnMenuTinhLuong.Click += new System.EventHandler(this.btnMenuTinhLuong_Click);
            // 
            // btnMenuLichLV
            // 
            this.btnMenuLichLV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnMenuLichLV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuLichLV.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnMenuLichLV.ForeColor = System.Drawing.Color.White;
            this.btnMenuLichLV.Location = new System.Drawing.Point(211, 298);
            this.btnMenuLichLV.Name = "btnMenuLichLV";
            this.btnMenuLichLV.Size = new System.Drawing.Size(200, 200);
            this.btnMenuLichLV.TabIndex = 3;
            this.btnMenuLichLV.Text = "📅\r\n\r\nLịch Làm Việc";
            this.btnMenuLichLV.UseVisualStyleBackColor = false;
            this.btnMenuLichLV.Click += new System.EventHandler(this.btnMenuLichLV_Click);
            // 
            // tabQuanLyNV
            // 
            this.tabQuanLyNV.Location = new System.Drawing.Point(4, 26);
            this.tabQuanLyNV.Name = "tabQuanLyNV";
            this.tabQuanLyNV.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuanLyNV.Size = new System.Drawing.Size(1136, 600);
            this.tabQuanLyNV.TabIndex = 1;
            this.tabQuanLyNV.Text = "Quản Lý Nhân Viên";
            this.tabQuanLyNV.UseVisualStyleBackColor = true;
            // 
            // tabChamCong
            // 
            this.tabChamCong.Location = new System.Drawing.Point(4, 26);
            this.tabChamCong.Name = "tabChamCong";
            this.tabChamCong.Size = new System.Drawing.Size(1136, 600);
            this.tabChamCong.TabIndex = 2;
            this.tabChamCong.Text = "Chấm Công";
            this.tabChamCong.UseVisualStyleBackColor = true;
            // 
            // tabTinhLuong
            // 
            this.tabTinhLuong.Location = new System.Drawing.Point(4, 26);
            this.tabTinhLuong.Name = "tabTinhLuong";
            this.tabTinhLuong.Size = new System.Drawing.Size(1136, 600);
            this.tabTinhLuong.TabIndex = 3;
            this.tabTinhLuong.Text = "Tính Lương";
            this.tabTinhLuong.UseVisualStyleBackColor = true;
            // 
            // tabLichLamViec
            // 
            this.tabLichLamViec.Location = new System.Drawing.Point(4, 26);
            this.tabLichLamViec.Name = "tabLichLamViec";
            this.tabLichLamViec.Size = new System.Drawing.Size(1136, 600);
            this.tabLichLamViec.TabIndex = 4;
            this.tabLichLamViec.Text = "Lịch Làm Việc";
            this.tabLichLamViec.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 700);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlHeader);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ Thống Quản Lý Bar";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabMenu.ResumeLayout(false);
            this.pnlMenuMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnTrovemenu;
        public TabControl tabControl;
        public TabPage tabQuanLyNV;
        public TabPage tabChamCong;
        public TabPage tabTinhLuong;
        public TabPage tabLichLamViec;
    }
}