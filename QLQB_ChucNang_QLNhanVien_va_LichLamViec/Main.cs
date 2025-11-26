using QLQB_ChucNang_QLNhanVien_va_LichLamViec.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    public partial class frmMain : Form
    {
        private DataTable dtNhanVien;
        private DataTable dtChamCong;
        private DataTable dtTinhLuong;
        private DataTable dtLichLamViec;
        private DataTable dtQuyen;
        private DataTable dtGioiTinh;
        private bool isDataChanged = false;
        private Button btnThongTinCaNhan;
        private Panel pnlMenuHeader;
        private Label lblMenuWelcome;

        // Controls cho tab Quản lý nhân viên
        private Panel pnlNVInput;
        private TextBox txtMaNV, txtTenNV, txtMatKhau;
        private DateTimePicker dtpNgaySinh;
        private ComboBox cboGioiTinh, cboChucVu, cboTrangThai;
        private NumericUpDown nudLuongMoiGio;
        private TextBox txtMaQuyen;

        // Controls cho tab Lịch làm việc
        private DataGridView dgvCaLam, dgvNhanVienCa;
        private DateTimePicker dtpLocNgay;
        private NumericUpDown nudLocThang, nudLocNam;
        private Button btnLocNgay, btnLocThang, btnHuyLocCa;
        private Panel pnlThongKeCa;
        private Button btnLLV_ThemCa, btnLLV_XoaCa, btnLLV_SuaCa, btnLLV_SaveCa, btnLLV_CancelCa;
        private Button btnLLV_ThemNVCa, btnLLV_XoaNVCa;
        private TextBox txtMaCaNew, txtGioBDNew, txtGioKTNew;
        private ComboBox cboNhanVienCaLam, cboCaLamChon;
        private SplitContainer splitContainer;
        private DateTimePicker dtpGioBD;
        private DateTimePicker dtpGioKT;

        private DataGridView dgvDanhSachNV_Luong;
        private DataGridView dgvBangLuongNV;
        private SplitContainer splitContainerLuong;
        private Button btnTinhLuongNV;
        private Button btnXoaLuongNV;
        private Button btnTongHopLuong;
        private Label lblThongTinNV;
        private Panel pnlCaInput;
        public frmMain()
        {
            InitializeComponent();
            InitializeTabControls();
            InitializeMenuHeader();
        }

        private void InitializeMenuHeader()
        {
            // Tạo panel header cho tab menu
            pnlMenuHeader = new Panel();
            pnlMenuHeader.Dock = DockStyle.Top;
            pnlMenuHeader.Height = 70;
            pnlMenuHeader.BackColor = Color.FromArgb(41, 128, 185);

            lblMenuWelcome = new Label();
            lblMenuWelcome.AutoSize = true;
            lblMenuWelcome.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblMenuWelcome.ForeColor = Color.White;
            lblMenuWelcome.Location = new Point(20, 20);

            btnThongTinCaNhan = new Button();
            btnThongTinCaNhan.Text = "👤 Thông tin";
            btnThongTinCaNhan.Size = new Size(130, 40);
            btnThongTinCaNhan.BackColor = Color.FromArgb(52, 152, 219);
            btnThongTinCaNhan.ForeColor = Color.White;
            btnThongTinCaNhan.FlatStyle = FlatStyle.Flat;
            btnThongTinCaNhan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnThongTinCaNhan.Cursor = Cursors.Hand;
            btnThongTinCaNhan.Click += btnThongTinCaNhan_Click;

            Button btnLogoutMenu = new Button();
            btnLogoutMenu.Text = "Đăng xuất";
            btnLogoutMenu.Size = new Size(130, 40);
            btnLogoutMenu.BackColor = Color.FromArgb(231, 76, 60);
            btnLogoutMenu.ForeColor = Color.White;
            btnLogoutMenu.FlatStyle = FlatStyle.Flat;
            btnLogoutMenu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogoutMenu.Cursor = Cursors.Hand;
            btnLogoutMenu.Click += btnLogout_Click;

            pnlMenuHeader.Controls.Add(lblMenuWelcome);
            pnlMenuHeader.Controls.Add(btnThongTinCaNhan);
            pnlMenuHeader.Controls.Add(btnLogoutMenu);

            pnlMenuHeader.Resize += (s, e) =>
            {
                btnLogoutMenu.Location = new Point(pnlMenuHeader.Width - 150, 15);
                btnThongTinCaNhan.Location = new Point(pnlMenuHeader.Width - 300, 15);
            };
        }
        public void SwitchToTab(string tabName)
        {
            foreach (TabPage tab in tabControl.TabPages)
            {
                if (tab.Name == tabName)
                {
                    tabControl.SelectedTab = tab;
                    return;
                }
            }
        }
        private void ForceLogoutAndRelogin(string message)
        {
            try
            {
                // Hiển thị thông báo
                MessageBox.Show(
                    message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Xóa session và đóng kết nối
                SessionInfo.Clear();
                DatabaseConnection.ClearConnection();

                // Ẩn form hiện tại
                this.Hide();

                // Mở lại form đăng nhập
                frmLogin loginForm = new frmLogin();
                loginForm.FormClosed += (s, args) =>
                {
                    // Đóng hoàn toàn form Main hiện tại
                    this.Close();
                    this.Dispose();
                };
                loginForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng xuất: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        private void btnThongTinCaNhan_Click(object sender, EventArgs e)
        {
            ShowThongTinCaNhan();
        }

        private void ShowThongTinCaNhan()
        {
            try
            {
                string info = "";

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    // Dùng VIEW cho tất cả user
                    string query = "SELECT * FROM vw_ThongTinCaNhan";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string gioBD = reader["GioBD"] != DBNull.Value ?
                            ((TimeSpan)reader["GioBD"]).ToString(@"hh\:mm") : "Chưa có";
                        string gioKT = reader["GioKT"] != DBNull.Value ?
                            ((TimeSpan)reader["GioKT"]).ToString(@"hh\:mm") : "Chưa có";


                        info = $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n" +
                               $"           THÔNG TIN CÁ NHÂN\n" +
                               $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n" +
                               $"  👤 Mã NV:            {reader["MaNV"]}\n" +
                               $"  📝 Tên:               {reader["TenNV"]}\n" +
                               $"  💼 Chức vụ:          {reader["ChucVu"]}\n" +
                               $"  🎂 Ngày sinh:      {Convert.ToDateTime(reader["NgaySinh"]):dd/MM/yyyy}\n" +
                               $"  ⚧  Giới tính:         {reader["GioiTinh"]}\n" +
                               $"  💰 Lương/giờ:       {Convert.ToDecimal(reader["LuongMoiGio"]):N0} VNĐ\n" +
                               $"  🔑 Quyền:            {reader["TenQuyen"]}\n\n" +
                               $"  📅 Ca làm việc:    {(reader["MaCa"] != DBNull.Value ? reader["MaCa"].ToString() : "Chưa có")}\n" +
                               $"  ⏰ Giờ:                 {gioBD} - {gioKT}\n" +
                               $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";
                    }
                    reader.Close();

                    // Lấy thông tin lương nếu có
                    string queryLuong = @"SELECT TOP 1 * FROM vw_BangLuongCaNhan
                                            ORDER BY Nam DESC, Thang DESC";
                    SqlCommand cmdLuong = new SqlCommand(queryLuong, conn);
                    cmdLuong.Parameters.AddWithValue("@MaNV", SessionInfo.MaNV);
                    SqlDataReader readerLuong = cmdLuong.ExecuteReader();

                    if (readerLuong.HasRows)
                    {
                        readerLuong.Read();
                        info += $"\n\n━━━━━━ THÔNG TIN LƯƠNG GẦN NHẤT ━━━━━━\n" +
                                $"  📊 Mã lương:        {readerLuong["MaLuong"]}\n" +
                                $"  📅 Số ngày làm:    {readerLuong["TongNgayLamMotThang"]} ngày\n" +
                                $"  💵 Thưởng/Phạt:   {Convert.ToDecimal(readerLuong["ThuongPhat"] == DBNull.Value ? 0 : readerLuong["ThuongPhat"]):N0} VNĐ\n" +
                                $"  💰 Tổng lương:     {Convert.ToDecimal(readerLuong["TongLuong"] == DBNull.Value ? 0 : readerLuong["TongLuong"]):N0} VNĐ\n" +
                                $"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";
                    }

                    readerLuong.Close();
                }

                MessageBox.Show(info, "Thông tin cá nhân", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            splitContainer.SplitterDistance = 500;
            lblMenuWelcome.Text = $"Chào mừng, {SessionInfo.TenNV}";

            // Xóa pnlHeader khỏi form chính
            if (this.Controls.Contains(pnlHeader))
            {
                this.Controls.Remove(pnlHeader);
            }

            // Thêm header vào tab Menu - HIỂN THỊ CHO TẤT CẢ
            if (!tabMenu.Controls.Contains(pnlMenuHeader))
            {
                tabMenu.Controls.Add(pnlMenuHeader);
                pnlMenuHeader.BringToFront();
            }

            // Kiểm tra quyền và điều chỉnh UI
            if (!SessionInfo.IsAdmin)
            {
                // Ẩn các nút menu không cần thiết
                btnMenuQLNV.Visible = false;
                btnMenuTinhLuong.Visible = false;
                btnMenuLichLV.Visible = false;

                // Căn giữa nút Chấm công
                CenterMenuButton();

                // Xóa các tab không được phép
                tabControl.TabPages.Remove(tabQuanLyNV);
                tabControl.TabPages.Remove(tabTinhLuong);
                tabControl.TabPages.Remove(tabLichLamViec);

                // Mặc định hiển thị tab Menu
                tabControl.SelectedTab = tabMenu;
            }
            else
            {
                tabControl.SelectedTab = tabMenu;
            }

            CheckPermissions();
        }

        private void CenterMenuButton()
        {
            // Căn giữa nút Chấm công khi chỉ còn 1 nút
            if (pnlMenuMain != null && btnMenuChamCong != null)
            {
                int centerX = (pnlMenuMain.Width - btnMenuChamCong.Width) / 2;
                int centerY = (pnlMenuMain.Height - btnMenuChamCong.Height) / 2;
                btnMenuChamCong.Location = new Point(centerX, centerY);

                // Resize event để nút luôn ở giữa khi resize form
                pnlMenuMain.Resize += (s, e) =>
                {
                    if (!SessionInfo.IsAdmin && btnMenuChamCong.Visible)
                    {
                        centerX = (pnlMenuMain.Width - btnMenuChamCong.Width) / 2;
                        centerY = (pnlMenuMain.Height - btnMenuChamCong.Height) / 2;
                        btnMenuChamCong.Location = new Point(centerX, centerY);
                    }
                };
            }
        }


        private void btnTrovemenu_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            frm.Visible = true;
            this.Hide();
        }
        private void CheckPermissions()
        {
            // Hàm này giờ chỉ để kiểm tra, UI đã được xử lý ở frmMain_Load
            if (!SessionInfo.IsAdmin)
            {
                // Log hoặc xử lý thêm nếu cần
                System.Diagnostics.Debug.WriteLine($"Nhân viên {SessionInfo.MaNV} - Quyền hạn chế");
            }
        }

        private void InitializeTabControls()
        {
            InitializeQLNVTab();
            InitializeChamCongTab();
            InitializeTinhLuongTab();
            InitializeLichLamViecTab();
        }

        #region Menu Navigation
        private void btnMenuQLNV_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabQuanLyNV;
        }

        private void btnMenuChamCong_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabChamCong;
        }

        private void btnMenuTinhLuong_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabTinhLuong;
        }

        private void btnMenuLichLV_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabLichLamViec;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SessionInfo.Clear();
                DatabaseConnection.ClearConnection();
                this.Close();
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabQuanLyNV)
                LoadNhanVienData();
            else if (tabControl.SelectedTab == tabChamCong)
                LoadChamCongData();
            else if (tabControl.SelectedTab == tabTinhLuong)
                LoadTinhLuongData();
            else if (tabControl.SelectedTab == tabLichLamViec)
                LoadLichLamViecData();
        }
        #endregion

        #region Quản Lý Nhân Viên

        private void InitializeQLNVTab()
        {
            pnlNVInput = new Panel();
            pnlNVInput.Dock = DockStyle.Top;
            pnlNVInput.Height = 180; // Giảm từ 280 xuống 180
            pnlNVInput.BackColor = Color.FromArgb(245, 245, 245);
            pnlNVInput.Padding = new Padding(10);

            // Column 1
            Label lblMaNV = new Label { Text = "Mã NV: *", Location = new Point(20, 15), AutoSize = true };
            txtMaNV = new TextBox { Location = new Point(120, 12), Width = 150, ReadOnly = false };

            Label lblTenNV = new Label { Text = "Tên NV: *", Location = new Point(290, 15), AutoSize = true };
            txtTenNV = new TextBox { Location = new Point(380, 12), Width = 200 };

            Label lblChucVu = new Label { Text = "Chức vụ: *", Location = new Point(600, 15), AutoSize = true };
            cboChucVu = new ComboBox { Location = new Point(690, 12), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cboChucVu.SelectedIndexChanged += (s, ev) =>
            {
                if (cboChucVu.SelectedItem != null)
                {
                    dynamic item = cboChucVu.SelectedItem;
                    txtMaQuyen.Text = item.TenQuyen;
                }
            };

            // Column 2
            Label lblNgaySinh = new Label { Text = "Ngày sinh: *", Location = new Point(20, 55), AutoSize = true };
            dtpNgaySinh = new DateTimePicker { Location = new Point(120, 52), Width = 150, Format = DateTimePickerFormat.Short, MaxDate = DateTime.Now.AddYears(-17) };

            Label lblGioiTinh = new Label { Text = "Giới tính: *", Location = new Point(290, 55), AutoSize = true };
            cboGioiTinh = new ComboBox { Location = new Point(380, 52), Width = 100, DropDownStyle = ComboBoxStyle.DropDownList };
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ" });
            cboGioiTinh.SelectedIndex = 0;

            Label lblTrangThai = new Label { Text = "Trạng thái: *", Location = new Point(500, 55), AutoSize = true };
            cboTrangThai = new ComboBox { Location = new Point(600, 52), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            cboTrangThai.Items.AddRange(new object[] { "Đang làm", "Tạm nghỉ", "Nghỉ việc" });
            cboTrangThai.SelectedIndex = 0;

            Label lblQuyen = new Label { Text = "Quyền:", Location = new Point(770, 55), AutoSize = true };
            txtMaQuyen = new TextBox { Location = new Point(830, 52), Width = 150, ReadOnly = true, BackColor = Color.WhiteSmoke };

            // Column 3
            Label lblMatKhau = new Label { Text = "Mật khẩu: *", Location = new Point(20, 95), AutoSize = true };
            txtMatKhau = new TextBox { Location = new Point(120, 92), Width = 200 };

            Label lblLuong = new Label { Text = "Lương/giờ: *", Location = new Point(340, 95), AutoSize = true };
            nudLuongMoiGio = new NumericUpDown
            {
                Location = new Point(440, 92),
                Width = 150,
                Minimum = 10000,
                Maximum = 1000000,
                Value = 50000,
                Increment = 5000,
                ThousandsSeparator = true
            };

            Button btnGenMaNV = new Button
            {
                Text = "🔄 Tạo mã",
                Location = new Point(20, 135),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnGenMaNV.Click += (s, ev) => GenerateNewMaNV();

            pnlNVInput.Controls.AddRange(new Control[] {
                lblMaNV, txtMaNV, lblTenNV, txtTenNV, lblChucVu, cboChucVu,
                lblNgaySinh, dtpNgaySinh, lblGioiTinh, cboGioiTinh, lblTrangThai, cboTrangThai,
                lblMatKhau, txtMatKhau, lblLuong, nudLuongMoiGio, lblQuyen, txtMaQuyen,
                btnGenMaNV
            });

            dgvNhanVien = new DataGridView();
            dgvNhanVien.Dock = DockStyle.Fill;
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVien.MultiSelect = false;
            dgvNhanVien.ReadOnly = true;
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.RowTemplate.Height = 30;
            dgvNhanVien.SelectionChanged += dgvNhanVien_SelectionChanged;
            dgvNhanVien.CellFormatting += dgvNhanVien_CellFormatting;

            pnlQLNV = new Panel();
            pnlQLNV.Dock = DockStyle.Bottom;
            pnlQLNV.Height = 60;
            pnlQLNV.BackColor = Color.WhiteSmoke;

            btnQLNV_Them = CreateButton("➕ Thêm", 20, Color.FromArgb(46, 204, 113));
            btnQLNV_Sua = CreateButton("✏️ Sửa", 140, Color.FromArgb(241, 196, 15));
            btnQLNV_Xoa = CreateButton("🗑️ Xóa", 260, Color.FromArgb(231, 76, 60));
            btnQLNV_Save = CreateButton("💾 Lưu", 380, Color.FromArgb(52, 152, 219));
            btnQLNV_Cancel = CreateButton("↶ Hủy", 500, Color.FromArgb(149, 165, 166));
            btnQLNV_BackMenu = CreateButton("← Menu", 620, Color.FromArgb(52, 73, 94));

            pnlQLNV.Controls.AddRange(new Control[] {
                btnQLNV_Them, btnQLNV_Xoa, btnQLNV_Sua,
                btnQLNV_Save, btnQLNV_Cancel, btnQLNV_BackMenu
            });

            tabQuanLyNV.Controls.Add(dgvNhanVien);
            tabQuanLyNV.Controls.Add(pnlNVInput);
            tabQuanLyNV.Controls.Add(pnlQLNV);

            btnQLNV_Them.Click += btnQLNV_Them_Click;
            btnQLNV_Xoa.Click += btnQLNV_Xoa_Click;
            btnQLNV_Sua.Click += btnQLNV_Sua_Click;
            btnQLNV_Save.Click += btnQLNV_Save_Click;
            btnQLNV_Cancel.Click += btnQLNV_Cancel_Click;
            btnQLNV_BackMenu.Click += (s, ev) => tabControl.SelectedTab = tabMenu;

            LoadChucVuData();
        }

        private void dgvNhanVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvNhanVien.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string trangThai = e.Value.ToString().Trim();
                switch (trangThai)
                {
                    case "Đang làm":
                        e.CellStyle.BackColor = Color.LightGreen;
                        e.CellStyle.ForeColor = Color.DarkGreen;
                        e.CellStyle.Font = new Font(dgvNhanVien.Font, FontStyle.Bold);
                        break;
                    case "Tạm nghỉ":
                        e.CellStyle.BackColor = Color.LightYellow;
                        e.CellStyle.ForeColor = Color.DarkOrange;
                        break;
                    case "Nghỉ việc":
                        e.CellStyle.BackColor = Color.LightCoral;
                        e.CellStyle.ForeColor = Color.DarkRed;
                        break;
                }
            }
            if (dgvNhanVien.Columns[e.ColumnIndex].Name == "GioiTinh" && e.Value != null)
            {
                e.Value = e.Value.ToString().Trim();
                e.FormattingApplied = true;
            }
        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0 && !isDataChanged)
            {
                DataGridViewRow row = dgvNhanVien.SelectedRows[0];
                txtMaNV.Text = row.Cells["MaNV"].Value?.ToString();
                txtTenNV.Text = row.Cells["TenNV"].Value?.ToString();
                txtMatKhau.Text = row.Cells["MatKhau"].Value?.ToString();

                if (row.Cells["NgaySinh"].Value != null)
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);

                if (row.Cells["GioiTinh"].Value != null)
                {
                    string gt = row.Cells["GioiTinh"].Value.ToString().Trim();
                    cboGioiTinh.SelectedIndex = cboGioiTinh.FindStringExact(gt);
                }

                if (row.Cells["LuongMoiGio"].Value != null)
                    nudLuongMoiGio.Value = Convert.ToDecimal(row.Cells["LuongMoiGio"].Value);

                if (row.Cells["MaQuyen"].Value != null)
                {
                    string maQuyen = row.Cells["MaQuyen"].Value.ToString();
                    for (int i = 0; i < cboChucVu.Items.Count; i++)
                    {
                        dynamic item = cboChucVu.Items[i];
                        if (item.MaQuyen == maQuyen)
                        {
                            cboChucVu.SelectedIndex = i;
                            break;
                        }
                    }
                }

                if (row.Cells["TrangThai"].Value != null)
                {
                    string tt = row.Cells["TrangThai"].Value.ToString().Trim();
                    cboTrangThai.SelectedIndex = cboTrangThai.FindStringExact(tt);
                }
            }
        }

        private void LoadChucVuData()
        {
            if (!SessionInfo.IsAdmin)
                return;
            try
            {
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    string query = @"SELECT DISTINCT n.MaQuyen, TenQuyen, ChucVu 
                                   FROM Quyen q 
                                   JOIN NhanVien n ON q.MaQuyen = n.MaQuyen 
                                   ORDER BY n.MaQuyen";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    dtQuyen = new DataTable();
                    adapter.Fill(dtQuyen);

                    cboChucVu.Items.Clear();
                    foreach (DataRow row in dtQuyen.Rows)
                    {
                        cboChucVu.Items.Add(new
                        {
                            Text = row["ChucVu"].ToString(),
                            MaQuyen = row["MaQuyen"].ToString(),
                            TenQuyen = row["TenQuyen"].ToString()
                        });
                    }

                    cboChucVu.DisplayMember = "Text";
                    if (cboChucVu.Items.Count > 0)
                        cboChucVu.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load chức vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateNewMaNV()
        {
            try
            {
                string lastMaNV = "";
                int maxNumber = 0;

                // Tìm mã NV lớn nhất trong DataGridView (bao gồm cả chưa lưu)
                foreach (DataGridViewRow row in dgvNhanVien.Rows)
                {
                    if (row.IsNewRow) continue; // Bỏ qua dòng mới

                    string maNV = row.Cells["MaNV"].Value?.ToString();
                    if (!string.IsNullOrEmpty(maNV) && maNV.StartsWith("NV"))
                    {
                        if (int.TryParse(maNV.Substring(2), out int number))
                        {
                            if (number > maxNumber)
                            {
                                maxNumber = number;
                                lastMaNV = maNV;
                            }
                        }
                    }
                }

                // Nếu không tìm thấy mã nào, tạo mã đầu tiên
                if (maxNumber == 0)
                {
                    txtMaNV.Text = "NV01";
                }
                else
                {
                    // Tạo mã mới bằng cách tăng số lên 1
                    txtMaNV.Text = "NV" + (maxNumber + 1).ToString("D2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaNV.Text = "NV01";
            }
        }

        private void LoadNhanVienData()
        {
            // Chỉ load khi là quản lý
            if (!SessionInfo.IsAdmin)
            {
                // Clear data để tránh hiển thị dữ liệu cũ
                if (dtNhanVien != null)
                    dtNhanVien.Clear();
                return;
            }
            try
            {
                string query = @"SELECT MaNV, TenNV, ChucVu, NgaySinh, GioiTinh, MatKhau, 
                                LuongMoiGio, MaQuyen, TrangThai 
                                FROM NhanVien 
                                ORDER BY MaNV";

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    dtNhanVien = new DataTable();
                    adapter.Fill(dtNhanVien);
                    foreach (DataRow row in dtNhanVien.Rows)
                    {
                        if (row["GioiTinh"] != null)
                        {
                            row["GioiTinh"] = row["GioiTinh"].ToString().Trim();
                        }
                    }
                    dgvNhanVien.DataSource = dtNhanVien;

                    if (dgvNhanVien.Columns["NgaySinh"] != null)
                        dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    if (dgvNhanVien.Columns["LuongMoiGio"] != null)
                    {
                        dgvNhanVien.Columns["LuongMoiGio"].DefaultCellStyle.Format = "N0";
                        dgvNhanVien.Columns["LuongMoiGio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }

                isDataChanged = false;
            }
            catch (SqlException sqlEx)
            {
                // KIỂM TRA NẾU LÀ LỖI SESSION BỊ KILL
                if (sqlEx.Message.Contains("session is in the kill state") ||
                    sqlEx.Message.Contains("Login failed"))
                {
                    ForceLogoutAndRelogin(
                        "Phiên đăng nhập của bạn đã hết hạn hoặc bị ngắt kết nối.\n\n" +
                        "Vui lòng đăng nhập lại."
                    );
                }
                else
                {
                    MessageBox.Show("Lỗi tải dữ liệu: " + sqlEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateNhanVienInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng tạo mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenNV.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNV.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }
            if (cboChucVu.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn chức vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnQLNV_Them_Click(object sender, EventArgs e)
        {
            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Bạn không có quyền thêm nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboGioiTinh.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtMatKhau.Text.Trim() == null)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtMaNV.Text.Trim() == null)
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateNhanVienInput()) return;

            dynamic selectedItem = cboChucVu.SelectedItem;

            DataRow newRow = dtNhanVien.NewRow();
            newRow["MaNV"] = txtMaNV.Text.Trim();
            newRow["TenNV"] = txtTenNV.Text.Trim();
            newRow["ChucVu"] = selectedItem.Text;
            newRow["NgaySinh"] = dtpNgaySinh.Value;
            newRow["GioiTinh"] = cboGioiTinh.SelectedItem.ToString();
            newRow["MatKhau"] = txtMatKhau.Text.Trim();
            newRow["LuongMoiGio"] = nudLuongMoiGio.Value;
            newRow["MaQuyen"] = selectedItem.MaQuyen;
            newRow["TrangThai"] = cboTrangThai.SelectedItem.ToString();

            dtNhanVien.Rows.Add(newRow);
            isDataChanged = true;

            MessageBox.Show("✓ Đã thêm nhân viên!\nNhấn 'Lưu' để cập nhật database.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearNhanVienInput();
            GenerateNewMaNV();
        }

        private void btnQLNV_Xoa_Click(object sender, EventArgs e)
        {
            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Bạn không có quyền xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                string maNV = dgvNhanVien.SelectedRows[0].Cells["MaNV"].Value.ToString();
                string tenNV = dgvNhanVien.SelectedRows[0].Cells["TenNV"].Value.ToString();

                if (MessageBox.Show($"Xóa nhân viên:\n{maNV} - {tenNV}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dgvNhanVien.Rows.RemoveAt(dgvNhanVien.SelectedRows[0].Index);
                    isDataChanged = true;
                    MessageBox.Show("✓ Đã xóa! Nhấn 'Lưu' để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearNhanVienInput();
                }
            }
        }

        private void btnQLNV_Sua_Click(object sender, EventArgs e)
        {
            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Bạn không có quyền sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                if (!ValidateNhanVienInput()) return;

                DataRow selectedRow = ((DataRowView)dgvNhanVien.SelectedRows[0].DataBoundItem).Row;
                dynamic selectedItem = cboChucVu.SelectedItem;

                selectedRow["TenNV"] = txtTenNV.Text.Trim();
                selectedRow["ChucVu"] = selectedItem.Text;
                selectedRow["NgaySinh"] = dtpNgaySinh.Value;
                selectedRow["GioiTinh"] = cboGioiTinh.SelectedItem.ToString();
                selectedRow["MatKhau"] = txtMatKhau.Text.Trim();
                selectedRow["LuongMoiGio"] = nudLuongMoiGio.Value;
                selectedRow["MaQuyen"] = selectedItem.MaQuyen;
                selectedRow["TrangThai"] = cboTrangThai.SelectedItem.ToString();

                isDataChanged = true;
                dgvNhanVien.Refresh();
                MessageBox.Show("✓ Đã cập nhật! Nhấn 'Lưu' để lưu vào database.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnQLNV_Save_Click(object sender, EventArgs e)
        {
            if (!isDataChanged)
            {
                MessageBox.Show("Không có thay đổi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Lưu các thay đổi?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int successCount = 0;
                int errorCount = 0;
                string errorMessages = "";
                bool currentUserPasswordChanged = false; // Flag để kiểm tra đổi mật khẩu tài khoản hiện tại

                try
                {
                    using (SqlConnection conn = DatabaseConnection.OpenConnection())
                    {
                        DataTable changes = dtNhanVien.GetChanges();
                        DataTable deletedRows = dtNhanVien.GetChanges(DataRowState.Deleted);

                        // Xử lý thêm mới và sửa đổi
                        if (changes != null)
                        {
                            foreach (DataRow row in changes.Rows)
                            {
                                try
                                {
                                    if (row.RowState == DataRowState.Added)
                                    {
                                        SqlCommand cmd = new SqlCommand("sp_ThemNhanVienMoi", conn);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@MaNV", row["MaNV"]);
                                        cmd.Parameters.AddWithValue("@TenNV", row["TenNV"]);
                                        cmd.Parameters.AddWithValue("@ChucVu", row["ChucVu"]);
                                        cmd.Parameters.AddWithValue("@NgaySinh", row["NgaySinh"]);
                                        cmd.Parameters.AddWithValue("@GioiTinh", row["GioiTinh"]);
                                        cmd.Parameters.AddWithValue("@LuongMoiGio", row["LuongMoiGio"]);
                                        cmd.Parameters.AddWithValue("@MatKhau", row["MatKhau"]);
                                        cmd.Parameters.AddWithValue("@MaQuyen", row["MaQuyen"]);
                                        cmd.Parameters.AddWithValue("@TrangThai", row["TrangThai"]);
                                        cmd.ExecuteNonQuery();

                                        try
                                        {
                                            SqlCommand cmdAccount = new SqlCommand("sp_TaoTaiKhoanNhanVien", conn);
                                            cmdAccount.CommandType = CommandType.StoredProcedure;
                                            cmdAccount.Parameters.AddWithValue("@MaNV", row["MaNV"]);
                                            cmdAccount.ExecuteNonQuery();
                                        }
                                        catch { }

                                        successCount++;
                                    }
                                    else if (row.RowState == DataRowState.Modified)
                                    {
                                        string maNV = row["MaNV"].ToString();
                                        string matKhauMoi = row["MatKhau"].ToString();
                                        string matKhauCu = row["MatKhau", DataRowVersion.Original].ToString();
                                        bool matKhauThayDoi = matKhauMoi != matKhauCu;

                                        // *** KIỂM TRA NẾU ĐANG ĐỔI MẬT KHẨU CỦA CHÍNH MÌNH ***
                                        if (matKhauThayDoi && maNV == SessionInfo.MaNV)
                                        {
                                            currentUserPasswordChanged = true;
                                        }

                                        // Nếu mật khẩu thay đổi, gọi procedure đổi mật khẩu
                                        if (matKhauThayDoi)
                                        {
                                            try
                                            {
                                                SqlCommand cmdDoiMK = new SqlCommand("sp_DoiMatKhau", conn);
                                                cmdDoiMK.CommandType = CommandType.StoredProcedure;
                                                cmdDoiMK.Parameters.AddWithValue("@MaNV", maNV);
                                                cmdDoiMK.Parameters.AddWithValue("@MatKhauMoi", matKhauMoi);
                                                cmdDoiMK.ExecuteNonQuery();
                                            }
                                            catch (SqlException sqlExMK)
                                            {
                                                errorMessages += $"\n- {maNV} (Đổi MK): {sqlExMK.Message}";
                                            }
                                        }

                                        // Cập nhật thông tin khác
                                        string updateQuery = @"UPDATE NhanVien SET 
                                    TenNV=@TenNV, ChucVu=@ChucVu, NgaySinh=@NgaySinh,
                                    GioiTinh=@GioiTinh, 
                                    LuongMoiGio=@LuongMoiGio, MaQuyen=@MaQuyen, TrangThai=@TrangThai";

                                        if (!matKhauThayDoi)
                                        {
                                            updateQuery += ", MatKhau=@MatKhau";
                                        }

                                        updateQuery += " WHERE MaNV=@MaNV";

                                        SqlCommand cmd = new SqlCommand(updateQuery, conn);
                                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                                        cmd.Parameters.AddWithValue("@TenNV", row["TenNV"]);
                                        cmd.Parameters.AddWithValue("@ChucVu", row["ChucVu"]);
                                        cmd.Parameters.AddWithValue("@NgaySinh", row["NgaySinh"]);
                                        cmd.Parameters.AddWithValue("@GioiTinh", row["GioiTinh"]);
                                        cmd.Parameters.AddWithValue("@LuongMoiGio", row["LuongMoiGio"]);
                                        cmd.Parameters.AddWithValue("@MaQuyen", row["MaQuyen"]);
                                        cmd.Parameters.AddWithValue("@TrangThai", row["TrangThai"]);

                                        if (!matKhauThayDoi)
                                        {
                                            cmd.Parameters.AddWithValue("@MatKhau", row["MatKhau"]);
                                        }

                                        cmd.ExecuteNonQuery();
                                        successCount++;
                                    }
                                }
                                catch (SqlException sqlEx)
                                {
                                    errorCount++;
                                    errorMessages += $"\n- {row["MaNV"]}: {sqlEx.Message}";
                                }
                            }
                        }

                        // Xử lý xóa
                        if (deletedRows != null)
                        {
                            foreach (DataRow row in deletedRows.Rows)
                            {
                                try
                                {
                                    string maNV = row["MaNV", DataRowVersion.Original].ToString();
                                    SqlCommand cmd = new SqlCommand("sp_XoaNhanVien", conn);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@MaNV", maNV);
                                    cmd.ExecuteNonQuery();
                                    successCount++;
                                }
                                catch (SqlException sqlEx)
                                {
                                    errorCount++;
                                    string maNV = row["MaNV", DataRowVersion.Original].ToString();
                                    errorMessages += $"\n- {maNV}: {sqlEx.Message}";
                                }
                            }
                        }
                    } // Đóng connection ở đây

                    // Hiển thị thông báo kết quả
                    if (errorCount == 0)
                        MessageBox.Show($"✓ Lưu thành công! ({successCount} bản ghi)", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show($"Hoàn tất với lỗi:\n- Thành công: {successCount}\n- Lỗi: {errorCount}{errorMessages}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    isDataChanged = false;
                    dtNhanVien.AcceptChanges();

                    // *** KIỂM TRA VÀ ĐĂNG XUẤT NẾU ĐỔI MẬT KHẨU CỦA CHÍNH MÌNH ***
                    if (currentUserPasswordChanged)
                    {
                        // Gọi hàm logout toàn cục NGAY LẬP TỨC, KHÔNG GỌI LoadNhanVienData()
                        ForceLogoutAndRelogin(
                            "✓ Đã lưu thành công!\n\n" +
                            "Bạn đã thay đổi mật khẩu của chính mình.\n\n" +
                            "Hệ thống sẽ tự động đăng xuất.\n" +
                            "Vui lòng đăng nhập lại với mật khẩu mới."
                        );
                        return; // QUAN TRỌNG: Dừng ngay tại đây, không chạy code phía dưới
                    }

                    // Chỉ reload dữ liệu nếu KHÔNG đổi mật khẩu của chính mình
                    LoadNhanVienData();
                    ClearNhanVienInput();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnQLNV_Cancel_Click(object sender, EventArgs e)
        {
            if (isDataChanged)
            {
                if (MessageBox.Show("Hủy thay đổi?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    LoadNhanVienData();
                    ClearNhanVienInput();
                }
            }
            else
            {
                ClearNhanVienInput();
            }
        }

        private void ClearNhanVienInput()
        {
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtMatKhau.Clear();
            DateTime macDinh = DateTime.Now.AddYears(-18);
            if (macDinh < dtpNgaySinh.MinDate)
                macDinh = dtpNgaySinh.MinDate;
            dtpNgaySinh.Value = macDinh;
            cboGioiTinh.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            nudLuongMoiGio.Value = 50000;
            if (cboChucVu.Items.Count > 0)
                cboChucVu.SelectedIndex = 0;
        }
        #endregion

        #region Chấm Công
        private void InitializeChamCongTab()
        {
            Panel pnlCCInput = new Panel();
            pnlCCInput.Dock = DockStyle.Top;
            pnlCCInput.Height = 80;
            pnlCCInput.BackColor = Color.FromArgb(245, 245, 245);

            Label lblNhanVien = new Label();
            lblNhanVien.Text = "Nhân viên:";
            lblNhanVien.Location = new Point(20, 25);
            lblNhanVien.AutoSize = true;

            cboNhanVienCC = new ComboBox();
            cboNhanVienCC.Location = new Point(120, 22);
            cboNhanVienCC.Width = 200;
            cboNhanVienCC.DropDownStyle = ComboBoxStyle.DropDownList;

            Label lblNgay = new Label();
            lblNgay.Text = "Ngày:";
            lblNgay.Location = new Point(350, 25);
            lblNgay.AutoSize = true;

            dtpChamCong = new DateTimePicker();
            dtpChamCong.Location = new Point(410, 22);
            dtpChamCong.Width = 200;
            dtpChamCong.Format = DateTimePickerFormat.Short;

            Label lblLocThang = new Label();
            lblLocThang.Text = "Lọc tháng:";
            lblLocThang.Location = new Point(650, 25);
            lblLocThang.AutoSize = true;

            NumericUpDown nudLocThang = new NumericUpDown();
            nudLocThang.Name = "nudLocThang";
            nudLocThang.Location = new Point(730, 22);
            nudLocThang.Width = 60;
            nudLocThang.Minimum = 1;
            nudLocThang.Maximum = 12;
            nudLocThang.Value = DateTime.Now.Month;

            Label lblLocNam = new Label();
            lblLocNam.Text = "Năm:";
            lblLocNam.Location = new Point(810, 25);
            lblLocNam.AutoSize = true;

            NumericUpDown nudLocNam = new NumericUpDown();
            nudLocNam.Name = "nudLocNam";
            nudLocNam.Location = new Point(860, 22);
            nudLocNam.Width = 80;
            nudLocNam.Minimum = 2020;
            nudLocNam.Maximum = 2100;
            nudLocNam.Value = DateTime.Now.Year;

            Button btnLocChamCong = new Button();
            btnLocChamCong.Text = "Lọc";
            btnLocChamCong.Location = new Point(960, 20);
            btnLocChamCong.Size = new Size(80, 27);
            btnLocChamCong.BackColor = Color.FromArgb(52, 152, 219);
            btnLocChamCong.ForeColor = Color.White;
            btnLocChamCong.FlatStyle = FlatStyle.Flat;
            btnLocChamCong.Click += (s, ev) =>
            {
                LoadChamCongDataFiltered((int)nudLocThang.Value, (int)nudLocNam.Value);
            };

            Button btnHuyLoc = new Button();
            btnHuyLoc.Text = "Hủy lọc";
            btnHuyLoc.Location = new Point(1050, 20);
            btnHuyLoc.Size = new Size(80, 27);
            btnHuyLoc.BackColor = Color.FromArgb(189, 195, 199);
            btnHuyLoc.ForeColor = Color.White;
            btnHuyLoc.FlatStyle = FlatStyle.Flat;
            btnHuyLoc.Click += (s, ev) => LoadChamCongData();

            if (SessionInfo.IsAdmin)
            {
                pnlCCInput.Controls.AddRange(new Control[] {
                    lblNhanVien, cboNhanVienCC, lblNgay, dtpChamCong,
                    lblLocThang, nudLocThang, lblLocNam, nudLocNam,
                    btnLocChamCong, btnHuyLoc
                });
            }
            else
            {
                pnlCCInput.Controls.AddRange(new Control[] {
                    lblNgay, dtpChamCong,
                    lblLocThang, nudLocThang, lblLocNam, nudLocNam,
                    btnLocChamCong, btnHuyLoc
                });
            }

            pnlChamCong = new Panel();
            pnlChamCong.Dock = DockStyle.Bottom;
            pnlChamCong.Height = 60;
            pnlChamCong.BackColor = Color.WhiteSmoke;

            btnCC_Them = CreateButton("✓ Chấm công", 20, Color.FromArgb(46, 204, 113));
            btnCC_Xoa = CreateButton("🗑️ Xóa", 140, Color.FromArgb(231, 76, 60));
            btnCC_BackMenu = CreateButton("← Menu", 620, Color.FromArgb(52, 73, 94));

            if (SessionInfo.IsAdmin)
            {
                pnlChamCong.Controls.AddRange(new Control[] { btnCC_Them, btnCC_Xoa, btnCC_BackMenu });
            }
            else
            {
                // Nhân viên thường chỉ có nút chấm công và nút quay về menu
                Button btnBackMenuNV = CreateButton("← Menu", 140, Color.FromArgb(52, 73, 94));
                btnBackMenuNV.Click += (s, ev) => tabControl.SelectedTab = tabMenu;

                pnlChamCong.Controls.AddRange(new Control[] { btnCC_Them, btnBackMenuNV });
            }

            dgvChamCong = new DataGridView();
            dgvChamCong.Dock = DockStyle.Fill;
            dgvChamCong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChamCong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChamCong.ReadOnly = true;
            dgvChamCong.AllowUserToAddRows = false;
            dgvChamCong.RowTemplate.Height = 30;

            tabChamCong.Controls.Add(dgvChamCong);
            tabChamCong.Controls.Add(pnlCCInput);
            tabChamCong.Controls.Add(pnlChamCong);

            btnCC_Them.Click += btnCC_Them_Click;
            btnCC_Xoa.Click += btnCC_Xoa_Click;
            btnCC_BackMenu.Click += (s, ev) => tabControl.SelectedTab = tabMenu;
        }

        private void LoadChamCongData()
        {
            try
            {
                string query;

                if (!SessionInfo.IsAdmin)
                {
                    query = @"SELECT NgayDiLam, MaNV, TenNV, ChucVu, SoGioLam
                             FROM vw_ChamCongCaNhan
                             ORDER BY NgayDiLam DESC";
                }
                else
                {
                    query = @"SELECT cc.NgayDiLam, cc.MaNV, nv.TenNV, nv.ChucVu, 8 AS SoGioLam
                             FROM ChamCong cc
                             INNER JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                             ORDER BY cc.NgayDiLam DESC";
                }

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    dtChamCong = new DataTable();
                    adapter.Fill(dtChamCong);
                    dgvChamCong.DataSource = dtChamCong;

                    if (dgvChamCong.Columns["NgayDiLam"] != null)
                        dgvChamCong.Columns["NgayDiLam"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    if (dgvChamCong.Columns["SoGioLam"] != null)
                        dgvChamCong.Columns["SoGioLam"].HeaderText = "Giờ Làm (8h/ngày)";

                    if (SessionInfo.IsAdmin && cboNhanVienCC != null)
                    {
                        SqlCommand cmd = new SqlCommand("SELECT MaNV, TenNV FROM NhanVien ORDER BY MaNV", conn);
                        SqlDataReader reader = cmd.ExecuteReader();
                        cboNhanVienCC.Items.Clear();
                        while (reader.Read())
                        {
                            cboNhanVienCC.Items.Add(new
                            {
                                Text = $"{reader["MaNV"]} - {reader["TenNV"]}",
                                Value = reader["MaNV"].ToString()
                            });
                        }
                        cboNhanVienCC.DisplayMember = "Text";
                        cboNhanVienCC.ValueMember = "Value";
                    }
                }

                isDataChanged = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChamCongDataFiltered(int thang, int nam)
        {
            try
            {
                string query;

                if (!SessionInfo.IsAdmin)
                {
                    query = @"SELECT NgayDiLam, MaNV, TenNV, ChucVu, SoGioLam
                             FROM vw_ChamCongCaNhan
                             WHERE MONTH(NgayDiLam)=@Thang AND YEAR(NgayDiLam)=@Nam
                             ORDER BY NgayDiLam DESC";
                }
                else
                {
                    query = @"SELECT cc.NgayDiLam, cc.MaNV, nv.TenNV, nv.ChucVu, 8 AS SoGioLam
                             FROM ChamCong cc
                             INNER JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                             WHERE MONTH(cc.NgayDiLam)=@Thang AND YEAR(cc.NgayDiLam)=@Nam
                             ORDER BY cc.NgayDiLam DESC";
                }

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@Thang", thang);
                    adapter.SelectCommand.Parameters.AddWithValue("@Nam", nam);

                    dtChamCong = new DataTable();
                    adapter.Fill(dtChamCong);
                    dgvChamCong.DataSource = dtChamCong;

                    if (dgvChamCong.Columns["NgayDiLam"] != null)
                        dgvChamCong.Columns["NgayDiLam"].DefaultCellStyle.Format = "dd/MM/yyyy";

                    if (dgvChamCong.Columns["SoGioLam"] != null)
                        dgvChamCong.Columns["SoGioLam"].HeaderText = "Giờ Làm (8h/ngày)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lọc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCC_Them_Click(object sender, EventArgs e)
        {
            string maNV;
            DateTime ngay = dtpChamCong.Value.Date;

            if (!SessionInfo.IsAdmin)
            {
                // Nhân viên thường chỉ chấm công ngày hiện tại
                if (ngay != DateTime.Now.Date)
                {
                    MessageBox.Show("Nhân viên chỉ được chấm công cho ngày hiện tại!\nNếu cần chấm công bù, vui lòng liên hệ quản lý.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                maNV = SessionInfo.MaNV;
            }
            else
            {
                // Quản lý có thể chấm công cho bất kỳ nhân viên nào và bất kỳ ngày nào
                if (cboNhanVienCC.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                dynamic selectedItem = cboNhanVienCC.SelectedItem;
                maNV = selectedItem.Value;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlCommand checkCmd = new SqlCommand(
                        "SELECT COUNT(*) FROM ChamCong WHERE MaNV=@MaNV AND NgayDiLam=@Ngay", conn);
                    checkCmd.Parameters.AddWithValue("@MaNV", maNV);
                    checkCmd.Parameters.AddWithValue("@Ngay", ngay);

                    int count = (int)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Đã chấm công ngày này!\n(Một ngày chỉ chấm công 1 lần = 8 giờ)",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    SqlCommand cmd = new SqlCommand(
                        "INSERT INTO ChamCong (NgayDiLam, MaNV) VALUES (@Ngay, @MaNV)", conn);
                    cmd.Parameters.AddWithValue("@Ngay", ngay);
                    cmd.Parameters.AddWithValue("@MaNV", maNV);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("✓ Chấm công thành công!\n(Tính 8 giờ làm việc)",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadChamCongData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCC_Xoa_Click(object sender, EventArgs e)
        {
            if (dgvChamCong.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Xóa bản ghi chấm công này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string maNV = dgvChamCong.SelectedRows[0].Cells["MaNV"].Value.ToString();
                        DateTime ngay = Convert.ToDateTime(dgvChamCong.SelectedRows[0].Cells["NgayDiLam"].Value);

                        using (SqlConnection conn = DatabaseConnection.OpenConnection())
                        {
                            SqlCommand cmd = new SqlCommand(
                                "DELETE FROM ChamCong WHERE MaNV=@MaNV AND NgayDiLam=@Ngay", conn);
                            cmd.Parameters.AddWithValue("@MaNV", maNV);
                            cmd.Parameters.AddWithValue("@Ngay", ngay);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("✓ Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadChamCongData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        #region Tính Lương

        private void InitializeTinhLuongTab()
        {
            splitContainerLuong = new SplitContainer();
            splitContainerLuong.Dock = DockStyle.Fill;
            splitContainerLuong.Orientation = Orientation.Vertical;
            splitContainerLuong.SplitterDistance = 48;
            splitContainerLuong.SplitterWidth = 4;
            splitContainerLuong.IsSplitterFixed = false;

            // ============ PANEL TRÁI - DANH SÁCH NHÂN VIÊN ============
            Panel pnlLeft = new Panel();
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.BackColor = Color.White;

            Label lblDanhSachNV = new Label();
            lblDanhSachNV.Text = "👥 DANH SÁCH NHÂN VIÊN";
            lblDanhSachNV.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblDanhSachNV.ForeColor = Color.FromArgb(52, 73, 94);
            lblDanhSachNV.Location = new Point(20, 20);
            lblDanhSachNV.AutoSize = true;

            dgvDanhSachNV_Luong = new DataGridView();
            dgvDanhSachNV_Luong.Location = new Point(20, 55);
            dgvDanhSachNV_Luong.Size = new Size(355, 480);
            dgvDanhSachNV_Luong.RowHeadersVisible = false;
            dgvDanhSachNV_Luong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDanhSachNV_Luong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSachNV_Luong.ReadOnly = true;
            dgvDanhSachNV_Luong.AllowUserToAddRows = false;
            dgvDanhSachNV_Luong.RowTemplate.Height = 35;
            dgvDanhSachNV_Luong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDanhSachNV_Luong.SelectionChanged += dgvDanhSachNV_Luong_SelectionChanged;

            pnlLeft.Controls.AddRange(new Control[] { lblDanhSachNV, dgvDanhSachNV_Luong });
            splitContainerLuong.Panel1.Controls.Add(pnlLeft);

            // ============ PANEL PHẢI - BẢNG LƯƠNG NHÂN VIÊN ============
            Panel pnlRight = new Panel();
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.BackColor = Color.White;

            lblThongTinNV = new Label();
            lblThongTinNV.Text = "💰 LƯƠNG NHÂN VIÊN";
            lblThongTinNV.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblThongTinNV.ForeColor = Color.FromArgb(52, 73, 94);
            lblThongTinNV.Location = new Point(20, 20);
            lblThongTinNV.AutoSize = true;

            dgvBangLuongNV = new DataGridView();
            dgvBangLuongNV.Location = new Point(20, 55);
            dgvBangLuongNV.Size = new Size(745, 480);
            dgvBangLuongNV.RowHeadersVisible = false;
            dgvBangLuongNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBangLuongNV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBangLuongNV.ReadOnly = true;
            dgvBangLuongNV.AllowUserToAddRows = false;
            dgvBangLuongNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvBangLuongNV.RowTemplate.Height = 35;

            pnlRight.Controls.AddRange(new Control[] { lblThongTinNV, dgvBangLuongNV });
            splitContainerLuong.Panel2.Controls.Add(pnlRight);

            // ============ PANEL BOTTOM - BUTTONS ============
            pnlTinhLuong = new Panel();
            pnlTinhLuong.Dock = DockStyle.Bottom;
            pnlTinhLuong.Height = 70;
            pnlTinhLuong.BackColor = Color.FromArgb(245, 245, 245);

            btnTinhLuongNV = CreateButton("💰 Tính lương", 20, Color.FromArgb(241, 196, 15));
            btnXoaLuongNV = CreateButton("🗑️ Xóa", 150, Color.FromArgb(231, 76, 60));
            btnTongHopLuong = CreateButton("📊 Tổng hợp", 280, Color.FromArgb(155, 89, 182));
            btnTL_BackMenu = CreateButton("← Menu", 620, Color.FromArgb(52, 73, 94));

            pnlTinhLuong.Controls.AddRange(new Control[] {
                btnTinhLuongNV, btnXoaLuongNV, btnTongHopLuong, btnTL_BackMenu
            });

            // Add controls to tab
            tabTinhLuong.Controls.Add(splitContainerLuong);
            tabTinhLuong.Controls.Add(pnlTinhLuong);

            // Event handlers
            btnTinhLuongNV.Click += btnTinhLuongNV_Click;
            btnXoaLuongNV.Click += btnXoaLuongNV_Click;
            btnTongHopLuong.Click += btnTongHopLuong_Click;
            btnTL_BackMenu.Click += (s, ev) => tabControl.SelectedTab = tabMenu;
        }

        private void LoadTinhLuongData()
        {
            if (!SessionInfo.IsAdmin)
            {
                if (dgvDanhSachNV_Luong != null)
                    dgvDanhSachNV_Luong.DataSource = null;
                if (dgvBangLuongNV != null)
                    dgvBangLuongNV.DataSource = null;
                return;
            }

            try
            {
                string query = @"SELECT MaNV, TenNV, GioiTinh, ChucVu, TrangThai
                        FROM NhanVien 
                        WHERE TrangThai = N'Đang làm' or TrangThai = N'Tạm nghỉ'
                        ORDER BY MaNV";

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvDanhSachNV_Luong.DataSource = dt;

                    // Format columns
                    if (dgvDanhSachNV_Luong.Columns["TrangThai"] != null)
                        dgvDanhSachNV_Luong.Columns["TrangThai"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhân viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvDanhSachNV_Luong_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDanhSachNV_Luong.SelectedRows.Count > 0)
            {
                string maNV = dgvDanhSachNV_Luong.SelectedRows[0].Cells["MaNV"].Value.ToString();
                string tenNV = dgvDanhSachNV_Luong.SelectedRows[0].Cells["TenNV"].Value.ToString();

                lblThongTinNV.Text = $"💰 LƯƠNG NHÂN VIÊN - {maNV} - {tenNV}";

                LoadBangLuongNhanVien(maNV);
            }
        }
        private void LoadBangLuongNhanVien(string maNV)
        {
            try
            {
                string query = @"SELECT 
                            bl.MaLuong,
                            bl.Thang,
                            bl.Nam,
                            bl.TongNgayLamMotThang AS [Số Ngày],
                            nv.LuongMoiGio AS [Lương/Giờ],
                            bl.ThuongPhat AS [Thưởng/Phạt],
                            bl.TongLuong AS [Tổng Lương],
                            bl.NgayTinhLuong AS [Ngày Tính]
                        FROM BangLuong bl
                        INNER JOIN NhanVien nv ON bl.MaNV = nv.MaNV
                        WHERE bl.MaNV = @MaNV
                        ORDER BY bl.Nam DESC, bl.Thang DESC";

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaNV", maNV);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvBangLuongNV.DataSource = dt;

                    // Format columns
                    FormatLuongColumns(dgvBangLuongNV);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải bảng lương: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormatLuongColumns(DataGridView dgv)
        {
            if (dgv.Columns["Lương/Giờ"] != null)
            {
                dgv.Columns["Lương/Giờ"].DefaultCellStyle.Format = "N0";
                dgv.Columns["Lương/Giờ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgv.Columns["Thưởng/Phạt"] != null)
            {
                dgv.Columns["Thưởng/Phạt"].DefaultCellStyle.Format = "N0";
                dgv.Columns["Thưởng/Phạt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgv.Columns["Tổng Lương"] != null)
            {
                dgv.Columns["Tổng Lương"].DefaultCellStyle.Format = "N0";
                dgv.Columns["Tổng Lương"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns["Tổng Lương"].DefaultCellStyle.BackColor = Color.LightGreen;
                dgv.Columns["Tổng Lương"].DefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
            }
            if (dgv.Columns["Ngày Tính"] != null)
            {
                dgv.Columns["Ngày Tính"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
        }
        private void btnTinhLuongNV_Click(object sender, EventArgs e)
        {
            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Chỉ quản lý mới có quyền tính lương!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvDanhSachNV_Luong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maNV = dgvDanhSachNV_Luong.SelectedRows[0].Cells["MaNV"].Value.ToString();
            string tenNV = dgvDanhSachNV_Luong.SelectedRows[0].Cells["TenNV"].Value.ToString();

            // Mở dialog tính lương
            frmTinhLuongDialog dialog = new frmTinhLuongDialog(maNV, tenNV);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Refresh lại bảng lương
                LoadBangLuongNhanVien(maNV);
            }
        }
        private void btnXoaLuongNV_Click(object sender, EventArgs e)
        {
            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Chỉ quản lý mới có quyền xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvBangLuongNV.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn bản ghi lương cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLuong = dgvBangLuongNV.SelectedRows[0].Cells["MaLuong"].Value.ToString();
            int thang = Convert.ToInt32(dgvBangLuongNV.SelectedRows[0].Cells["Thang"].Value);
            int nam = Convert.ToInt32(dgvBangLuongNV.SelectedRows[0].Cells["Nam"].Value);

            if (MessageBox.Show($"Xóa bảng lương tháng {thang}/{nam}?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseConnection.OpenConnection())
                    {
                        SqlCommand cmdUpdate = new SqlCommand(
                            "UPDATE NhanVien SET MaLuong=NULL WHERE MaLuong=@MaLuong", conn);
                        cmdUpdate.Parameters.AddWithValue("@MaLuong", maLuong);
                        cmdUpdate.ExecuteNonQuery();

                        SqlCommand cmdDelete = new SqlCommand(
                            "DELETE FROM BangLuong WHERE MaLuong=@MaLuong", conn);
                        cmdDelete.Parameters.AddWithValue("@MaLuong", maLuong);
                        cmdDelete.ExecuteNonQuery();
                    }

                    MessageBox.Show("✓ Xóa thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh
                    if (dgvDanhSachNV_Luong.SelectedRows.Count > 0)
                    {
                        string maNV = dgvDanhSachNV_Luong.SelectedRows[0].Cells["MaNV"].Value.ToString();
                        LoadBangLuongNhanVien(maNV);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTongHopLuong_Click(object sender, EventArgs e)
        {
            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Chỉ quản lý mới có quyền xem tổng hợp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmTongHopLuong frmTongHop = new frmTongHopLuong();
            frmTongHop.ShowDialog();
        }

        #endregion

        #region Lịch Làm Việc
        private bool isEditingCa = false;
        private string editingMaCaCu = "";


        private void LoadLichLamViecData()
        {
            try
            {
                string query = @"SELECT MaCa, 
                        CONVERT(VARCHAR(8), GioBD, 108) AS GioBatDau,
                        CONVERT(VARCHAR(8), GioKT, 108) AS GioKetThuc
                        FROM LichLamViec
                        ORDER BY GioBD";

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    dtLichLamViec = new DataTable();
                    adapter.Fill(dtLichLamViec);
                    dgvCaLam.DataSource = dtLichLamViec;

                    // Load combo nhân viên
                    SqlCommand cmdNV = new SqlCommand("SELECT MaNV, TenNV FROM NhanVien WHERE TrangThai = N'Đang làm' ORDER BY MaNV", conn);
                    SqlDataReader readerNV = cmdNV.ExecuteReader();
                    cboNhanVienCaLam.Items.Clear();
                    while (readerNV.Read())
                    {
                        cboNhanVienCaLam.Items.Add(new
                        {
                            Text = $"{readerNV["MaNV"]} - {readerNV["TenNV"]}",
                            Value = readerNV["MaNV"].ToString()
                        });
                    }
                    cboNhanVienCaLam.DisplayMember = "Text";
                    cboNhanVienCaLam.ValueMember = "Value";
                    readerNV.Close();

                    // Load combo ca
                    SqlCommand cmdCa = new SqlCommand("SELECT MaCa FROM LichLamViec ORDER BY MaCa", conn);
                    SqlDataReader readerCa = cmdCa.ExecuteReader();
                    cboCaLamChon.Items.Clear();
                    while (readerCa.Read())
                    {
                        cboCaLamChon.Items.Add(readerCa["MaCa"].ToString());
                    }
                    readerCa.Close();

                    UpdateThongKeCa(dtLichLamViec.Rows.Count, 0);
                }

                isDataChanged = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvCaLam_SelectionChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu không có dòng nào được chọn thì thoát
            if (dgvCaLam.SelectedRows.Count == 0)
                return;

            //// Nếu đang có thay đổi chưa lưu, hỏi người dùng có muốn hủy thay đổi không
            //if (isDataChanged)
            //{
            //    var result = MessageBox.Show("Bạn có thay đổi chưa lưu. Bạn có muốn hủy thay đổi và xem dữ liệu mới không?",
            //                                "Cảnh báo",
            //                                MessageBoxButtons.YesNo,
            //                                MessageBoxIcon.Warning);
            //    if (result == DialogResult.No)
            //    {
            //        // Nếu người dùng không muốn hủy, thì không làm gì
            //        return;
            //    }
            //    // Nếu người dùng đồng ý hủy, thì đặt isDataChanged = false và tiếp tục
            //    isDataChanged = false;
            //}

            // Cập nhật dữ liệu từ dòng được chọn
            string maCa = dgvCaLam.SelectedRows[0].Cells["MaCa"].Value.ToString();
            txtMaCaNew.Text = maCa;

            string gioBD = dgvCaLam.SelectedRows[0].Cells["GioBatDau"].Value.ToString();
            string gioKT = dgvCaLam.SelectedRows[0].Cells["GioKetThuc"].Value.ToString();

            // Cập nhật DateTimePicker với định dạng HH:mm:ss
            if (TimeSpan.TryParse(gioBD, out TimeSpan timeBD))
            {
                dtpGioBD.Value = DateTime.Today.Add(timeBD);
            }
            if (TimeSpan.TryParse(gioKT, out TimeSpan timeKT))
            {
                dtpGioKT.Value = DateTime.Today.Add(timeKT);
            }

            LoadNhanVienTheoCa(maCa);
        }
        private void LoadNhanVienTheoCa(string maCa, DateTime? ngay = null, int? thang = null, int? nam = null)
        {
            try
            {
                string query = @"SELECT DISTINCT nv.MaNV, nv.TenNV, nv.MaCa, nv.ChucVu
                        FROM NhanVien nv
                        WHERE nv.MaCa = @MaCa AND nv.TrangThai = N'Đang làm'";

                if (ngay.HasValue)
                {
                    query = @"SELECT DISTINCT nv.MaNV, nv.TenNV, nv.MaCa, nv.ChucVu, cc.NgayDiLam
                    FROM NhanVien nv
                    INNER JOIN ChamCong cc ON nv.MaNV = cc.MaNV
                    WHERE nv.MaCa = @MaCa AND cc.NgayDiLam = @Ngay";
                }
                else if (thang.HasValue && nam.HasValue)
                {
                    query = @"SELECT DISTINCT nv.MaNV, nv.TenNV, nv.MaCa, nv.ChucVu, 
                    COUNT(cc.NgayDiLam) AS SoNgayLam
                    FROM NhanVien nv
                    LEFT JOIN ChamCong cc ON nv.MaNV = cc.MaNV 
                        AND MONTH(cc.NgayDiLam) = @Thang 
                        AND YEAR(cc.NgayDiLam) = @Nam
                    WHERE nv.MaCa = @MaCa
                    GROUP BY nv.MaNV, nv.TenNV, nv.MaCa, nv.ChucVu";
                }

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaCa", maCa);

                    if (ngay.HasValue)
                        cmd.Parameters.AddWithValue("@Ngay", ngay.Value.Date);

                    if (thang.HasValue && nam.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@Thang", thang.Value);
                        cmd.Parameters.AddWithValue("@Nam", nam.Value);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvNhanVienCa.DataSource = dt;

                    UpdateThongKeCa(dgvCaLam.Rows.Count, dt.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLocNgay_Click(object sender, EventArgs e)
        {
            if (dgvCaLam.SelectedRows.Count > 0)
            {
                string maCa = dgvCaLam.SelectedRows[0].Cells["MaCa"].Value.ToString();
                LoadNhanVienTheoCa(maCa, dtpLocNgay.Value.Date);
            }
        }
        private void btnLocThang_Click(object sender, EventArgs e)
        {
            if (dgvCaLam.SelectedRows.Count > 0)
            {
                string maCa = dgvCaLam.SelectedRows[0].Cells["MaCa"].Value.ToString();
                LoadNhanVienTheoCa(maCa, null, (int)nudLocThang.Value, (int)nudLocNam.Value);
            }
        }
        private void btnHuyLocCa_Click(object sender, EventArgs e)
        {
            if (dgvCaLam.SelectedRows.Count > 0)
            {
                string maCa = dgvCaLam.SelectedRows[0].Cells["MaCa"].Value.ToString();
                LoadNhanVienTheoCa(maCa);
            }
        }
        private void UpdateThongKeCa(int tongCa, int tongNV)
        {
            var lblTongCa = pnlThongKeCa.Controls.Find("lblTongCa", false).FirstOrDefault() as Label;
            var lblTongNV = pnlThongKeCa.Controls.Find("lblTongNV", false).FirstOrDefault() as Label;

            if (lblTongCa != null)
                lblTongCa.Text = $"📊 Tổng số ca: {tongCa}";

            if (lblTongNV != null)
                lblTongNV.Text = $"👥 Tổng số nhân viên: {tongNV}";
        }


        private void btnLLV_ThemCa_Click(object sender, EventArgs e)
        {
            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Chỉ quản lý mới có quyền thêm ca!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMaCaNew.Text))
            {
                MessageBox.Show("Vui lòng nhập mã ca!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kiểm tra trùng mã ca trong database
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlCommand checkCmd = new SqlCommand(
                        "SELECT COUNT(*) FROM LichLamViec WHERE MaCa = @MaCa", conn);
                    checkCmd.Parameters.AddWithValue("@MaCa", txtMaCaNew.Text.Trim());
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Mã ca đã tồn tại trong database!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Sử dụng định dạng HH:mm:ss
                string gioBD = dtpGioBD.Value.ToString("HH:mm:ss");
                string gioKT = dtpGioKT.Value.ToString("HH:mm:ss");

                // Thêm vào DataTable chính
                DataRow newRow = dtLichLamViec.NewRow();
                newRow["MaCa"] = txtMaCaNew.Text.Trim();
                newRow["GioBatDau"] = gioBD;
                newRow["GioKetThuc"] = gioKT;
                dtLichLamViec.Rows.Add(newRow);

                isDataChanged = true;

                MessageBox.Show("✓ Đã thêm ca vào danh sách chờ!\nNhấn 'Lưu ca' để lưu vào database.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearCaInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLLV_SuaCa_Click(object sender, EventArgs e)
        {
            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Chỉ quản lý mới có quyền sửa ca!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvCaLam.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ca cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maCaCu = dgvCaLam.SelectedRows[0].Cells["MaCa"].Value.ToString();

                // Sử dụng định dạng HH:mm:ss
                string gioBD = dtpGioBD.Value.ToString("HH:mm:ss");
                string gioKT = dtpGioKT.Value.ToString("HH:mm:ss");

                // Cập nhật trong DataTable chính
                DataRow[] rows = dtLichLamViec.Select($"MaCa = '{maCaCu}'");
                if (rows.Length > 0)
                {
                    rows[0]["GioBatDau"] = gioBD;
                    rows[0]["GioKetThuc"] = gioKT;
                    isDataChanged = true;

                    MessageBox.Show("✓ Đã cập nhật ca trong danh sách chờ!\nNhấn 'Lưu ca' để lưu vào database.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ClearCaInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLLV_XoaCa_Click(object sender, EventArgs e)
        {
            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Chỉ quản lý mới có quyền xóa ca!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvCaLam.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ca cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maCa = dgvCaLam.SelectedRows[0].Cells["MaCa"].Value.ToString();

            if (MessageBox.Show($"Xóa ca {maCa}?\n(Nhấn 'Lưu ca' để áp dụng thay đổi)",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DataRow[] rows = dtLichLamViec.Select($"MaCa = '{maCa}'");
                    if (rows.Length > 0)
                    {
                        rows[0].Delete();
                        isDataChanged = true;

                        MessageBox.Show("✓ Đã đánh dấu xóa ca!\nNhấn 'Lưu ca' để áp dụng.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    ClearCaInput();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnLLV_SaveCa_Click(object sender, EventArgs e)
        {
            if (!isDataChanged)
            {
                MessageBox.Show("Không có thay đổi nào!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Lưu tất cả thay đổi ca làm việc vào database?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            int successCount = 0;
            int errorCount = 0;
            string errorMessages = "";

            try
            {
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    // Lấy các thay đổi từ DataTable
                    DataTable changes = dtLichLamViec.GetChanges();

                    if (changes != null)
                    {
                        foreach (DataRow row in changes.Rows)
                        {
                            try
                            {
                                if (row.RowState == DataRowState.Added)
                                {
                                    // Thêm mới
                                    string insertQuery = @"INSERT INTO LichLamViec (MaCa, GioBD, GioKT) 
                                         VALUES (@MaCa, @GioBD, @GioKT)";
                                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                                    cmd.Parameters.AddWithValue("@MaCa", row["MaCa"]);
                                    cmd.Parameters.AddWithValue("@GioBD", TimeSpan.Parse(row["GioBatDau"].ToString()));
                                    cmd.Parameters.AddWithValue("@GioKT", TimeSpan.Parse(row["GioKetThuc"].ToString()));
                                    cmd.ExecuteNonQuery();
                                    successCount++;
                                }
                                else if (row.RowState == DataRowState.Modified)
                                {
                                    // Cập nhật
                                    string updateQuery = @"UPDATE LichLamViec 
                                         SET GioBD = @GioBD, GioKT = @GioKT 
                                         WHERE MaCa = @MaCa";
                                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                                    cmd.Parameters.AddWithValue("@MaCa", row["MaCa"]);
                                    cmd.Parameters.AddWithValue("@GioBD", TimeSpan.Parse(row["GioBatDau"].ToString()));
                                    cmd.Parameters.AddWithValue("@GioKT", TimeSpan.Parse(row["GioKetThuc"].ToString()));
                                    cmd.ExecuteNonQuery();
                                    successCount++;
                                }
                            }
                            catch (SqlException sqlEx)
                            {
                                errorCount++;
                                errorMessages += $"\n- {row["MaCa"]}: {sqlEx.Message}";
                            }
                        }
                    }

                    // Xử lý các dòng bị xóa
                    DataTable deletedRows = dtLichLamViec.GetChanges(DataRowState.Deleted);
                    if (deletedRows != null)
                    {
                        foreach (DataRow row in deletedRows.Rows)
                        {
                            try
                            {
                                string maCa = row["MaCa", DataRowVersion.Original].ToString();

                                // Xóa ca của nhân viên trước
                                string updateQuery = "UPDATE NhanVien SET MaCa = NULL WHERE MaCa = @MaCa";
                                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                                updateCmd.Parameters.AddWithValue("@MaCa", maCa);
                                updateCmd.ExecuteNonQuery();

                                // Xóa ca
                                string deleteQuery = "DELETE FROM LichLamViec WHERE MaCa = @MaCa";
                                SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                                cmd.Parameters.AddWithValue("@MaCa", maCa);
                                cmd.ExecuteNonQuery();
                                successCount++;
                            }
                            catch (SqlException sqlEx)
                            {
                                errorCount++;
                                string maCa = row["MaCa", DataRowVersion.Original].ToString();
                                errorMessages += $"\n- {maCa}: {sqlEx.Message}";
                            }
                        }
                    }
                }

                if (errorCount == 0)
                {
                    MessageBox.Show($"✓ Lưu thành công! ({successCount} thay đổi)",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Hoàn tất với lỗi:\n- Thành công: {successCount}\n- Lỗi: {errorCount}{errorMessages}",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Reset và reload dữ liệu
                isDataChanged = false;
                dtLichLamViec.AcceptChanges();
                LoadLichLamViecData();
                ClearCaInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLLV_CancelCa_Click(object sender, EventArgs e)
        {
            try
            {
                if (isDataChanged)
                {
                    if (MessageBox.Show("Hủy tất cả thay đổi chưa lưu?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Hủy thay đổi bằng cách reload từ database
                        isDataChanged = false;

                        // Kiểm tra dtLichLamViec trước khi gọi RejectChanges
                        if (dtLichLamViec != null)
                        {
                            dtLichLamViec.RejectChanges();
                        }

                        LoadLichLamViecData();
                        ClearCaInput();

                        MessageBox.Show("✓ Đã hủy thay đổi!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    ClearCaInput();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hủy thay đổi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLLV_ThemNVCa_Click(object sender, EventArgs e)
        {
            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Chỉ quản lý mới có quyền gán ca cho nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboNhanVienCaLam.SelectedItem == null || cboCaLamChon.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên và ca làm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                dynamic selectedNV = cboNhanVienCaLam.SelectedItem;
                string maNV = selectedNV.Value;
                string maCa = cboCaLamChon.SelectedItem.ToString();

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    string updateQuery = "UPDATE NhanVien SET MaCa = @MaCa WHERE MaNV = @MaNV";
                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@MaCa", maCa);
                    cmd.Parameters.AddWithValue("@MaNV", maNV);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("✓ Gán ca làm việc cho nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (dgvCaLam.SelectedRows.Count > 0)
                    {
                        string currentCa = dgvCaLam.SelectedRows[0].Cells["MaCa"].Value.ToString();
                        LoadNhanVienTheoCa(currentCa);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLLV_XoaNVCa_Click(object sender, EventArgs e)
        {
            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Chỉ quản lý mới có quyền bỏ ca nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvNhanVienCa.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần bỏ ca!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maNV = dgvNhanVienCa.SelectedRows[0].Cells["MaNV"].Value.ToString();
            string tenNV = dgvNhanVienCa.SelectedRows[0].Cells["TenNV"].Value.ToString();

            if (MessageBox.Show($"Bỏ ca làm của nhân viên {tenNV}?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseConnection.OpenConnection())
                    {
                        string updateQuery = "UPDATE NhanVien SET MaCa = NULL WHERE MaNV = @MaNV";
                        SqlCommand cmd = new SqlCommand(updateQuery, conn);
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("✓ Bỏ ca làm việc thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (dgvCaLam.SelectedRows.Count > 0)
                        {
                            string currentCa = dgvCaLam.SelectedRows[0].Cells["MaCa"].Value.ToString();
                            LoadNhanVienTheoCa(currentCa);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearCaInput()
        {
            try
            {
                // Kiểm tra null trước khi sử dụng các control
                if (txtMaCaNew != null && !txtMaCaNew.IsDisposed)
                    txtMaCaNew.Clear();

                if (dtpGioBD != null && !dtpGioBD.IsDisposed)
                    dtpGioBD.Value = DateTime.Today.AddHours(13);

                if (dtpGioKT != null && !dtpGioKT.IsDisposed)
                    dtpGioKT.Value = DateTime.Today.AddHours(21);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nhưng không hiển thị cho người dùng
                System.Diagnostics.Debug.WriteLine("Lỗi trong ClearCaInput: " + ex.Message);
            }
        }
        #endregion

        #region Helper Methods
        private Button CreateButton(string text, int x, Color? bgColor = null)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(x, 10);
            btn.Size = new Size(110, 40);
            btn.BackColor = bgColor ?? Color.FromArgb(52, 152, 219);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            return btn;
        }
        #endregion
    }

    public static class SessionInfo
    {
        public static string MaNV { get; set; }
        public static string TenNV { get; set; }
        public static string MaQuyen { get; set; }
        public static string TenQuyen { get; set; }
        public static bool IsAdmin { get; set; }

        public static void Clear()
        {
            MaNV = null;
            TenNV = null;
            MaQuyen = null;
            TenQuyen = null;
            IsAdmin = false;
        }
    }
}