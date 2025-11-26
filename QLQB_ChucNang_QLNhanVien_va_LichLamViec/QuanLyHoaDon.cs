using Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic;
using QLQB_ChucNang_QLNhanVien_va_LichLamViec.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using DataTable = System.Data.DataTable;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    public partial class QuanLyHoaDon : Form
    {
        //private string connectionString;
        private string maBanHienTai = "";
        private decimal tongTien = 0;
        //private string MaNV = "";
        //private string MaQuyen = "";
        //private bool IsAdmin = false;

        // Biến cho in ấn
        private PrintDocument printDocument;
        private DataTable dataHoaDon;
        private string tenKhachHangIn = "";
        private string sdtKhachHangIn = "";
        private string tenNhanVienIn = "";

        public QuanLyHoaDon()/*(string userName, string userRole)*/
        {
            InitializeComponent();
            //connectionString = "Data Source=26.71.28.188\\MSSQL16SERVER;Initial Catalog=QuanLyQuanBar";
            //// Lấy thông tin từ SessionInfo
            //currentUser  = SessionInfo.MaNV;        // Hoặc SessionInfo.TenNV tùy nhu cầu
            //maQuyen  = SessionInfo.SessionInfo.MaQuyen;
            //isQuanLy  = SessionInfo.IsAdmin;        // Hoặc (SessionInfo.SessionInfo.MaQuyen == "Q01")
            if (string.IsNullOrEmpty(SessionInfo.MaNV))
            {
                MessageBox.Show("Chưa đăng nhập! Vui lòng đăng nhập lại.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
        }

        //private string GetConnectionString()
        //{
        //    // Kiểm tra xem đã đăng nhập chưa
        //    if (string.IsNullOrEmpty(SessionInfo.MaNV))
        //    {
        //        throw new Exception("Chưa đăng nhập! Vui lòng đăng nhập lại.");
        //    }

        //    // Sử dụng connection string từ DatabaseConnection
        //    using (var conn = DatabaseConnection.GetConnection())
        //    {
        //        return conn.ConnectionString;
        //    }
        //}

        private void FormQuanLyBan_Load_1(object sender, EventArgs e)
        {
            try
            {
                // KIỂM TRA ĐĂNG NHẬP TRƯỚC
                if (string.IsNullOrEmpty(SessionInfo.MaNV))
                {
                    MessageBox.Show("Chưa đăng nhập! Vui lòng đăng nhập lại.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Kiểm tra kết nối và dữ liệu
                KiemTraKetNoiVaDuLieu();

                // HIỂN THỊ THÔNG TIN USER
                string tenQuyen = SessionInfo.MaQuyen == "Q01" ? "Quản lý" :
                             SessionInfo.MaQuyen == "Q02" ? "Phục vụ" :
                             SessionInfo.MaQuyen == "Q03" ? "Thu ngân" : "Nhân viên";

                this.Text = $"QUẢN LÝ QUÁN BAR - {tenQuyen} - User: {SessionInfo.MaNV}";

                PhanQuyenTheoUser();
                LoadComboBoxTrangThai();
                LoadDanhSachBan();
                LoadDanhSachMon();
                LoadComboBoxNhanVien();
                LoadComboBoxKhachHang();
                LamMoiForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo form: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KiemTraKetNoiVaDuLieu()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    // Kiểm tra bàn có dữ liệu không
                    string checkBan = "SELECT COUNT(*) FROM Ban";
                    SqlCommand cmdBan = new SqlCommand(checkBan, conn);
                    int soBan = (int)cmdBan.ExecuteScalar();

                    // Kiểm tra món có dữ liệu không
                    string checkMon = "SELECT COUNT(*) FROM Kho WHERE SoLuongTon > 0";
                    SqlCommand cmdMon = new SqlCommand(checkMon, conn);
                    int soMon = (int)cmdMon.ExecuteScalar();

                    MessageBox.Show($"Kết nối thành công!\nSố bàn: {soBan}\nSố món: {soMon}",
                        "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối database: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        #region PHÂN QUYỀN
        private void PhanQuyenTheoUser()
        {
            // DEBUG - Xem quyền hiện tại
            System.Diagnostics.Debug.WriteLine($"Phân quyền: User={SessionInfo.MaNV}, Quyền={SessionInfo.MaQuyen}");

            // --- QUYỀN QUẢN LÝ (Q01) - Full quyền ---
            if (SessionInfo.MaQuyen == "Q01" || SessionInfo.IsAdmin)
            {
                btnThemMoi.Enabled = true;
                btnXoaBan.Enabled = true;
                btnQuanLyKH.Enabled = true;
                cboNhanVien.Enabled = true;

                btnThemMon.Visible = true;
                btnXoaMon.Visible = true;
                btnThanhToan.Visible = true;
                btnInHoaDon.Visible = true;
                btnXuatExcel.Visible = true;
                btnDatBan.Visible = true;
                btnGopBan.Visible = true;
                btnThongKe.Visible = true;
                btnCapNhat.Visible = true;
            }
            // --- QUYỀN PHỤC VỤ (Q02) ---
            else if (SessionInfo.MaQuyen == "Q02")
            {
                btnThemMoi.Enabled = false;
                btnXoaBan.Enabled = false;
                btnQuanLyKH.Enabled = false;
                cboNhanVien.Enabled = false;
                btnQuanLyKH.Enabled = true;
                btnThemMon.Visible = true;
                btnXoaMon.Visible = true;

                // ẨN CÁC NÚT KHÔNG CẦN
                btnThanhToan.Visible = false;
                btnInHoaDon.Visible = false;
                btnXuatExcel.Visible = false;
                btnDatBan.Visible = false;
                btnGopBan.Visible = false;
                btnThongKe.Visible = false;

                try
                {
                    cboNhanVien.SelectedValue = SessionInfo.MaNV;
                }
                catch { }
            }
            // --- QUYỀN THU NGÂN (Q03) ---
            else if (SessionInfo.MaQuyen == "Q03")
            {
                btnThemMoi.Enabled = true;
                btnXoaBan.Visible = false;
                btnQuanLyKH.Visible = true;
                cboNhanVien.Enabled = false;

                // ẨN thêm/xóa món
                btnThemMon.Visible = false;
                btnThemMon.Enabled = false;
                btnXoaMon.Visible = false;
                btnThemMon.Enabled = false;

                // HIỆN thanh toán, in, xuất
                btnThanhToan.Enabled = true;
                btnThanhToan.Visible = true;
                btnInHoaDon.Visible = true;
                btnXuatExcel.Visible = false;
                btnThongKe.Visible = true;

                // ẨN các nút khác
                btnDatBan.Visible = false;
                btnGopBan.Visible = false;
                btnThongKe.Visible = true;
                btnThongKe.Visible = false;
                btnCapNhat.Visible = true;
            }
            // --- QUYỀN KHÁC ---
            else
            {
                btnThemMoi.Enabled = false;
                btnXoaBan.Enabled = false;
                btnQuanLyKH.Enabled = false;
                cboNhanVien.Enabled = false;

                btnThemMon.Visible = false;
                btnXoaMon.Visible = false;
                btnThanhToan.Visible = false;
                btnInHoaDon.Visible = false;
                btnXuatExcel.Visible = false;
                btnDatBan.Visible = false;
                btnGopBan.Visible = false;
                btnThongKe.Visible = false;
                btnCapNhat.Visible = false;
            }
        }
        #endregion

        #region LOAD DỮ LIỆU
        private void LoadComboBox()
        {
            LoadComboBoxNhanVien();
            LoadComboBoxKhachHang();
            LoadComboBoxTrangThai();
        }

        private void LoadComboBoxNhanVien()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    var query = @"SELECT MaNV, TenNV FROM NhanVien 
                                WHERE MaQuyen IN ('Q01','Q02','Q03') ORDER BY TenNV";
                    var query1 = @"SELECT * FROM vw_ThongTinCaNhan WHERE MaQuyen IN ('Q01','Q02','Q03') ORDER BY TenNV";
                    var da = new SqlDataAdapter(query1, conn);
                    var dt = new DataTable();
                    da.Fill(dt);

                    cboNhanVien.DataSource = dt;
                    cboNhanVien.DisplayMember = "TenNV";
                    cboNhanVien.ValueMember = "MaNV";
                    cboNhanVien.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhân viên: " + ex.Message);
            }
        }

        private void LoadComboBoxKhachHang()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    var query = "SELECT MaKH, TenKH, SDT FROM KhachHang ORDER BY TenKH";
                    var da = new SqlDataAdapter(query, conn);
                    var dt = new DataTable();
                    da.Fill(dt);

                    cboKhachHang.DataSource = dt;
                    cboKhachHang.DisplayMember = "TenKH";
                    cboKhachHang.ValueMember = "MaKH";
                    cboKhachHang.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải khách hàng: " + ex.Message);
            }
        }

        private void LoadComboBoxTrangThai()
        {
            cboTrangThai.Items.AddRange(new object[] {
                //"Trống", "Có khách", "Đã đặt", "Đã thanh toán", "Đang dọn"
            });
        }

        private void LoadDanhSachBan()
        {
            try
            {
                flpTatCa.Controls.Clear();
                flpTrong.Controls.Clear();
                flpCoKhach.Controls.Clear();

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    // Đảm bảo kết nối đang mở
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    string query = "SELECT MaBan, TrangThaiBan FROM Ban ORDER BY MaBan";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    int countTatCa = 0, countTrong = 0, countCoKhach = 0;

                    while (reader.Read())
                    {
                        string maBan = reader["MaBan"].ToString().Trim();
                        string trangThai = reader["TrangThaiBan"].ToString().Trim();

                        // DEBUG: In ra console để kiểm tra
                        System.Diagnostics.Debug.WriteLine($"Đọc được bàn: {maBan} - Trạng thái: {trangThai}");

                        // Tạo button cho tab "Tất cả"
                        System.Windows.Forms.Button btnTatCa = TaoBanButton(maBan, trangThai);
                        btnTatCa.Click += BtnBan_Click;
                        flpTatCa.Controls.Add(btnTatCa);
                        countTatCa++;

                        // Phân loại theo trạng thái
                        if (trangThai == "Trống")
                        {
                            System.Windows.Forms.Button btnTrong = TaoBanButton(maBan, trangThai);
                            btnTrong.Click += BtnBan_Click;
                            flpTrong.Controls.Add(btnTrong);
                            countTrong++;
                        }
                        else if (trangThai == "Có khách")
                        {
                            System.Windows.Forms.Button btnCoKhach = TaoBanButton(maBan, trangThai);
                            btnCoKhach.Click += BtnBan_Click;
                            flpCoKhach.Controls.Add(btnCoKhach);
                            countCoKhach++;
                        }
                    }
                    reader.Close();

                    // DEBUG
                    MessageBox.Show($"Đã load:\n- Tất cả: {countTatCa} bàn\n- Trống: {countTrong} bàn\n- Có khách: {countCoKhach} bàn");

                    // Hiển thị thông báo nếu không có dữ liệu
                    if (countTatCa == 0)
                    {
                        ThemLabelKhongCoDuLieu(flpTatCa, "Không có bàn nào");
                    }
                    if (countTrong == 0)
                    {
                        ThemLabelKhongCoDuLieu(flpTrong, "Không có bàn trống");
                    }
                    if (countCoKhach == 0)
                    {
                        ThemLabelKhongCoDuLieu(flpCoKhach, "Không có bàn có khách");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách bàn: " + ex.Message + "\n\n" + ex.StackTrace,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ThemLabelKhongCoDuLieu(FlowLayoutPanel panel, string message)
        {
            // SỬA LỖI Ở ĐÂY: Sử dụng System.Windows.Forms.Label
            System.Windows.Forms.Label lbl = new System.Windows.Forms.Label(); // Đây mới là Label đúng
            lbl.Text = message;
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Arial", 10, FontStyle.Italic);
            lbl.ForeColor = Color.Gray;
            lbl.Margin = new Padding(10);
            panel.Controls.Add(lbl);
        }

        private System.Windows.Forms.Button TaoBanButton(string maBan, string trangThai)
        {
            var btn = new System.Windows.Forms.Button
            {
                Text = $"{maBan}\n({trangThai})",
                Size = new Size(80, 65),
                Margin = new Padding(3),
                Font = new System.Drawing.Font("Arial", 9, FontStyle.Bold),
                Tag = maBan,
                FlatStyle = FlatStyle.Flat,
                BackColor = GetColorByStatus(trangThai)
            };

            btn.FlatAppearance.BorderColor = Color.Blue;
            btn.FlatAppearance.BorderSize = 0;

            return btn;
        }

        private Color GetColorByStatus(string trangThai)
        {
            switch (trangThai)
            {
                case "Trống": return Color.LightGreen;
                case "Có khách": return Color.LightCoral;
                case "Đã đặt": return Color.Orange;
                case "Đang dọn": return Color.Yellow;
                case "Đã thanh toán": return Color.LightBlue;
                default: return Color.White;
            }
        }

        private void LoadDanhSachMon()
        {
            try
            {
                flpChucNang.Controls.Clear();

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    // Đảm bảo kết nối đang mở
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    string query = @"SELECT k.MaMon, k.TenMon, 
                           (SELECT TOP 1 GiaMoi FROM CapNhatGia 
                            WHERE MaMon = k.MaMon 
                            ORDER BY NgayCapNhat DESC, SoLanCapNhatTrongNgay DESC) AS Gia
                           FROM Kho k
                           WHERE k.SoLuongTon > 0
                           ORDER BY k.TenMon";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    int count = 0;

                    while (reader.Read())
                    {
                        string maMon = reader["MaMon"].ToString().Trim();
                        string tenMon = reader["TenMon"].ToString().Trim();

                        decimal giaMon = 0;
                        if (reader["Gia"] != DBNull.Value)
                        {
                            giaMon = Convert.ToDecimal(reader["Gia"]);
                        }

                        System.Windows.Forms.Button btnMon = new System.Windows.Forms.Button();
                        btnMon.Text = tenMon + "\n" + giaMon.ToString("N0") + " đ";
                        btnMon.Size = new Size(115, 60);
                        btnMon.Margin = new Padding(3);
                        btnMon.Font = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
                        btnMon.BackColor = Color.LightBlue;
                        btnMon.ForeColor = Color.Black;
                        btnMon.Tag = maMon;
                        btnMon.Click += BtnMon_Click;

                        flpChucNang.Controls.Add(btnMon);
                        count++;
                    }

                    reader.Close();

                    if (count == 0)
                    {
                        // SỬA LỖI Ở ĐÂY: Sử dụng System.Windows.Forms.Label
                        System.Windows.Forms.Label lblEmpty = new System.Windows.Forms.Label(); // Đây mới là Label đúng
                        lblEmpty.Text = "Chưa có món nào trong menu";
                        lblEmpty.AutoSize = true;
                        lblEmpty.Font = new System.Drawing.Font("Arial", 12, FontStyle.Italic);
                        lblEmpty.ForeColor = Color.Gray;
                        lblEmpty.Margin = new Padding(10);
                        flpChucNang.Controls.Add(lblEmpty);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải món: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //private void LoadThongTinBan(string maBan)
        //{
        //    try
        //    {
        //        using (var conn = DatabaseConnection.GetConnection())
        //        {
        //            if (conn.State != ConnectionState.Open)
        //                conn.Open();
        //            var query = @"SELECT b.MaBan, b.TrangThaiBan, b.MaNV, db.MaKH,
        //            db.NgayDat, kh.TenKH, kh.SDT, b.TongTien, b.NgayThanhToan
        //            FROM Ban b
        //            LEFT JOIN DatBan db ON b.MaDatBan = db.MaDatBan
        //            LEFT JOIN KhachHang kh ON db.MaKH = kh.MaKH
        //            WHERE b.MaBan = @MaBan";

        //            var cmd = new SqlCommand(query, conn);
        //            cmd.Parameters.AddWithValue("@MaBan", maBan);
        //            var reader = cmd.ExecuteReader();

        //            if (reader.Read())
        //            {
        //                txtMaBan.Text = reader["MaBan"].ToString();

        //                // Cập nhật combobox trạng thái
        //                var trangThai = reader["TrangThaiBan"].ToString();
        //                cboTrangThai.SelectedItem = trangThai;

        //                // Hiển thị thông báo nếu đã thanh toán
        //                if (trangThai == "Đã thanh toán")
        //                {
        //                    MessageBox.Show("Bàn này đã thanh toán!", "Thông báo",
        //                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                }

        //                // ... phần còn lại giữ nguyên
        //            }
        //            reader.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi tải thông tin bàn: " + ex.Message);
        //    }
        //}
        private void LoadThongTinBan(string maBan)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    var query = @"SELECT b.MaBan, b.TrangThaiBan, b.MaNV, db.MaKH, db.NgayDat, kh.TenKH, kh.SDT, b.TongTien, b.NgayThanhToan 
                        FROM Ban b 
                        LEFT JOIN DatBan db ON b.MaDatBan = db.MaDatBan 
                        LEFT JOIN KhachHang kh ON db.MaKH = kh.MaKH 
                        WHERE b.MaBan = @MaBan";

                    var cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaBan", maBan);

                    // Sử dụng using để đảm bảo reader được đóng đúng cách
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Đọc tất cả dữ liệu cần thiết trước khi đóng reader
                            string maBanValue = reader["MaBan"].ToString();
                            string trangThai = reader["TrangThaiBan"].ToString();
                            object maNVValue = reader["MaNV"];
                            object maKHValue = reader["MaKH"];
                            object ngayDatValue = reader["NgayDat"];
                            object tenKHValue = reader["TenKH"];
                            object sdtValue = reader["SDT"];
                            object tongTienValue = reader["TongTien"];

                            // Đóng reader trước khi thao tác với controls
                            reader.Close();

                            // Gán giá trị vào controls
                            txtMaBan.Text = maBanValue;
                            cboTrangThai.SelectedItem = trangThai;

                            if (ngayDatValue != DBNull.Value)
                            {
                                dtpNgayDat.Value = Convert.ToDateTime(ngayDatValue);
                            }

                            if (maKHValue != DBNull.Value)
                            {
                                string maKH = maKHValue.ToString();
                                cboKhachHang.SelectedValue = maKH;

                                string tenKH = tenKHValue?.ToString() ?? "Khách lẻ";
                                string sdt = sdtValue?.ToString() ?? "";
                                txtSDT.Text = sdt;

                                //HIỂN THỊ THÔNG TIN KHÁCH HÀNG KHI BÀN "ĐÃ ĐẶT" 
                                if (trangThai == "Đã đặt" || trangThai == "Có khách")
                                {
                                    string thongTinKH = $"Khách hàng: {tenKH}\n" +
                                                        $"SĐT: {sdt}\n" +
                                                        $"Ngày đặt: {dtpNgayDat.Value:dd/MM/yyyy}";
                                    MessageBox.Show(thongTinKH, $"THÔNG TIN ĐẶT BÀN - {maBan}",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            // Hiển thị nhân viên phụ trách 
                            if (maNVValue != DBNull.Value)
                            {
                                cboNhanVien.SelectedValue = maNVValue.ToString();
                            }

                            // Hiển thị tổng tiền 
                            if (tongTienValue != DBNull.Value)
                            {
                                decimal tongTien = Convert.ToDecimal(tongTienValue);
                                txtTongTien.Text = tongTien.ToString("N0") + " đ";
                            }

                            // Hiển thị thông báo nếu đã thanh toán 
                            if (trangThai == "Đã thanh toán")
                            {
                                MessageBox.Show("Bàn này đã thanh toán!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin bàn: " + ex.Message);
            }
        }        
        #endregion

        #region XỬ LÝ SỰ KIỆN BÀN VÀ MÓN
        private void BtnBan_Click(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.Button btn)
            {
                maBanHienTai = btn.Tag.ToString();
                lblBanDangChon.Text = "Bàn đang chọn: " + maBanHienTai;

                LoadChiTietHoaDon(maBanHienTai);
                LoadThongTinBan(maBanHienTai);

                // Highlight button được chọn
                foreach (Control c in flpTatCa.Controls.OfType<System.Windows.Forms.Button>())
                    ((System.Windows.Forms.Button)c).FlatAppearance.BorderSize = 0;

                btn.FlatAppearance.BorderSize = 3;
                btn.FlatAppearance.BorderColor = Color.Blue;
            }
        }

        private void BtnMon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maBanHienTai))
            {
                MessageBox.Show("Vui lòng chọn bàn trước!");
                return;
            }

            if (sender is System.Windows.Forms.Button btn)
            {
                var maMon = btn.Tag.ToString();
                ThemMonVaoHoaDon(maMon);
            }
        }

        private void ThemMonVaoHoaDon(string maMon)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    //if (conn.State == ConnectionState.Closed)
                    //    conn.Open();
                    var cmd = new SqlCommand("sp_ThemMonVaoHoaDon", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@MaBan", maBanHienTai);
                    cmd.Parameters.AddWithValue("@MaMon", maMon);
                    cmd.Parameters.AddWithValue("@SoLuong", 1);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Cập nhật giao diện
                    LoadChiTietHoaDon(maBanHienTai);
                    LoadDanhSachBan();

                    MessageBox.Show("Đã thêm món thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm món: " + ex.Message);
            }
        }

        private void LoadChiTietHoaDon(string maBan)
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    // Load tổng tiền
                    var cmdTongTien = new SqlCommand("SELECT dbo.fn_TinhTongTienBan(@MaBan)", conn);
                    cmdTongTien.Parameters.AddWithValue("@MaBan", maBan);
                    var result = cmdTongTien.ExecuteScalar();
                    tongTien = result != DBNull.Value ? (decimal)result : 0;

                    // XÓA DÒNG NÀY: dgvHoaDon.Rows.Clear(); // ← ĐÂY LÀ NGUYÊN NHÂN GÂY LỖI

                    // Query chi tiết hóa đơn
                    var query = @"SELECT 
                k.TenMon AS 'Tên Món', 
                ct.SoLuong AS 'Số Lượng', 
                (SELECT TOP 1 GiaMoi FROM CapNhatGia 
                 WHERE MaMon = ct.MaMon 
                 ORDER BY NgayCapNhat DESC, SoLanCapNhatTrongNgay DESC) AS 'Đơn Giá',
                (ct.SoLuong * (SELECT TOP 1 GiaMoi FROM CapNhatGia 
                               WHERE MaMon = ct.MaMon 
                               ORDER BY NgayCapNhat DESC, SoLanCapNhatTrongNgay DESC)) AS 'Thành Tiền'
            FROM ChiTietHD ct
            INNER JOIN Kho k ON ct.MaMon = k.MaMon
            WHERE ct.MaBan = @MaBan";

                    var da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@MaBan", maBan);
                    var dt = new DataTable();
                    da.Fill(dt);

                    // Gán DataTable trực tiếp cho DataGridView
                    dgvHoaDon.DataSource = dt;

                    // Cập nhật tổng tiền
                    txtTongTien.Text = tongTien.ToString("N0") + " đ";
                    lblBanDangChon.Text = $"Bàn đang chọn: {maBan} - Tổng tiền: {tongTien:N0} đ";

                    // Định dạng DataGridView
                    if (dgvHoaDon.Columns.Count > 0)
                    {
                        dgvHoaDon.Columns["Tên Món"].Width = 200;
                        dgvHoaDon.Columns["Số Lượng"].Width = 80;
                        dgvHoaDon.Columns["Đơn Giá"].Width = 120;
                        dgvHoaDon.Columns["Thành Tiền"].Width = 120;

                        // Định dạng cột tiền
                        dgvHoaDon.Columns["Đơn Giá"].DefaultCellStyle.Format = "N0";
                        dgvHoaDon.Columns["Thành Tiền"].DefaultCellStyle.Format = "N0";
                        dgvHoaDon.Columns["Đơn Giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvHoaDon.Columns["Thành Tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải hóa đơn: " + ex.Message + "\n\n" + ex.StackTrace);
            }
        }
        #endregion

        #region CHỨC NĂNG CHÍNH
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maBanHienTai))
            {
                MessageBox.Show("Vui lòng chọn bàn!");
                return;
            }

            // Vô hiệu hóa nút để tránh click nhiều lần
            btnThanhToan.Enabled = false;

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    // Sử dụng stored procedure để thanh toán
                    var cmd = new SqlCommand("sp_ThanhToanHoaDon", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@MaBan", maBanHienTai);
                    cmd.Parameters.AddWithValue("@HinhThucTT", "Tiền mặt");

                    // Thực thi và lấy kết quả
                    var result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                    {
                        MessageBox.Show("Bàn này đã thanh toán rồi!", "Thông báo");
                        btnThanhToan.Enabled = true;
                        return;
                    }

                    decimal tongTienThanhToan = (decimal)result;

                    MessageBox.Show($"Thanh toán thành công!\nTổng tiền: {tongTienThanhToan:N0} đ",
                                  "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật giao diện NGAY LẬP TỨC
                    LoadDanhSachBan();
                    LoadChiTietHoaDon(maBanHienTai); // Load lại để xem trạng thái mới
                    LoadThongTinBan(maBanHienTai);   // Load thông tin bàn mới

                    // Làm mới form sau 1 giây
                    Timer timer = new Timer();
                    timer.Interval = 1000;
                    timer.Tick += (s, args) =>
                    {
                        LamMoiForm();
                        timer.Stop();
                    };
                    timer.Start();
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == -1) // Mã lỗi từ stored procedure
                {
                    MessageBox.Show("Bàn này đã thanh toán rồi!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (sqlEx.Message.Contains("PRIMARY KEY") && sqlEx.Message.Contains("LichSuBan"))
                {
                    MessageBox.Show("Giao dịch đã được thực hiện. Đang cập nhật giao diện...",
                                  "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Vẫn cập nhật giao diện dù có lỗi duplicate
                    LoadDanhSachBan();
                    LoadThongTinBan(maBanHienTai);
                }
                else
                {
                    MessageBox.Show("Lỗi thanh toán: " + sqlEx.Message, "Lỗi",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message, "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Kích hoạt lại nút sau 3 giây
                Timer timer = new Timer();
                timer.Interval = 3000;
                timer.Tick += (s, args) =>
                {
                    btnThanhToan.Enabled = true;
                    timer.Stop();
                };
                timer.Start();
            }
        }

        private void btnDatBan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maBanHienTai))
            {
                MessageBox.Show("Vui lòng chọn bàn!");
                return;
            }

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    var query = @"UPDATE Ban SET TrangThaiBan = N'Đã đặt', MaNV = @MaNV 
                                WHERE MaBan = @MaBan";

                    var cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaBan", maBanHienTai);
                    cmd.Parameters.AddWithValue("@MaNV", cboNhanVien.SelectedValue ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Đặt bàn thành công!");
                    LoadDanhSachBan();
                    LoadThongTinBan(maBanHienTai);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đặt bàn: " + ex.Message);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maBanHienTai))
            {
                MessageBox.Show("Vui lòng chọn bàn!");
                return;
            }

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    var query = @"UPDATE Ban SET TrangThaiBan = @TrangThai, MaNV = @MaNV 
                                WHERE MaBan = @MaBan";

                    var cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaBan", maBanHienTai);
                    cmd.Parameters.AddWithValue("@TrangThai", cboTrangThai.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@MaNV", cboNhanVien.SelectedValue ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cập nhật thành công!");
                    LoadDanhSachBan();
                    LoadThongTinBan(maBanHienTai);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }

        private void btnXoaBan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maBanHienTai))
            {
                MessageBox.Show("Vui lòng chọn bàn cần xóa!");
                return;
            }

            // KIỂM TRA QUYỀN TRỰC TIẾP TỪ DATABASE
            bool coQuyenXoa = false;
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    var checkQuery = @"SELECT COUNT(*) 
                              FROM NhanVien 
                              WHERE MaNV = @MaNV AND MaQuyen = 'Q01'";
                    var checkQuery1 = @"SELECT * FROM vw_ThongTinCaNhan WHERE MaNV = @MaNV AND MaQuyen = 'Q01'";
                    var cmd = new SqlCommand(checkQuery1, conn);
                    cmd.Parameters.AddWithValue("@MaNV", SessionInfo.MaNV);
                    coQuyenXoa = (int)cmd.ExecuteScalar() > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra quyền: " + ex.Message);
                return;
            }

            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Chỉ quản lý mới có quyền xóa bàn!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc muốn xóa bàn {maBanHienTai}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = DatabaseConnection.OpenConnection())
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        // Kiểm tra trạng thái bàn
                        var checkTT = new SqlCommand("SELECT TrangThaiBan FROM Ban WHERE MaBan = @MaBan", conn);
                        checkTT.Parameters.AddWithValue("@MaBan", maBanHienTai);
                        var trangThai = checkTT.ExecuteScalar()?.ToString();

                        if (trangThai == "Có khách" || trangThai == "Đã đặt")
                        {
                            MessageBox.Show($"Không thể xóa bàn đang {trangThai.ToLower()}!");
                            return;
                        }

                        // Kiểm tra có món trong hóa đơn không
                        var checkHD = new SqlCommand("SELECT COUNT(*) FROM ChiTietHD WHERE MaBan = @MaBan", conn);
                        checkHD.Parameters.AddWithValue("@MaBan", maBanHienTai);
                        int soMon = (int)checkHD.ExecuteScalar();

                        if (soMon > 0)
                        {
                            var confirm = MessageBox.Show(
                                $"Bàn có {soMon} món trong hóa đơn. Xóa toàn bộ?",
                                "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                            if (confirm == DialogResult.No) return;

                            // Xóa chi tiết hóa đơn
                            var delHD = new SqlCommand("DELETE FROM ChiTietHD WHERE MaBan = @MaBan", conn);
                            delHD.Parameters.AddWithValue("@MaBan", maBanHienTai);
                            delHD.ExecuteNonQuery();
                        }

                        // Xóa lịch sử bàn
                        var delLS = new SqlCommand("DELETE FROM LichSuBan WHERE MaBan = @MaBan", conn);
                        delLS.Parameters.AddWithValue("@MaBan", maBanHienTai);
                        delLS.ExecuteNonQuery();

                        // Xóa liên kết đặt bàn
                        var updateDB = new SqlCommand("UPDATE Ban SET MaDatBan = NULL WHERE MaBan = @MaBan", conn);
                        updateDB.Parameters.AddWithValue("@MaBan", maBanHienTai);
                        updateDB.ExecuteNonQuery();

                        // Xóa bàn
                        var delBan = new SqlCommand("DELETE FROM Ban WHERE MaBan = @MaBan", conn);
                        delBan.Parameters.AddWithValue("@MaBan", maBanHienTai);
                        delBan.ExecuteNonQuery();

                        MessageBox.Show("Xóa bàn thành công!");
                        LamMoiForm();
                        LoadDanhSachBan();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa bàn: " + ex.Message);
                }
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Chỉ quản lý mới có quyền thêm bàn!");
                return;
            }

            var maBanMoi = Microsoft.VisualBasic.Interaction.InputBox("Nhập mã bàn mới:", "Thêm bàn", "");
            if (string.IsNullOrWhiteSpace(maBanMoi)) return;

            try
            {
                using (var conn = DatabaseConnection.OpenConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    var query = "INSERT INTO Ban (MaBan, TrangThaiBan) VALUES (@MaBan, N'Trống')";
                    var cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaBan", maBanMoi.Trim());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Thêm bàn thành công!");
                    LoadDanhSachBan();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm bàn: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDanhSachBan();
            LoadDanhSachMon();
            LamMoiForm();
        }
        #endregion

        #region IN HÓA ĐƠN VÀ XUẤT EXCEL


        private void LoadDataForPrinting()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    // SỬA QUERY - Lấy giá mới nhất từ CapNhatGia
                    var query = @"SELECT 
                k.TenMon, 
                ct.SoLuong, 
                (SELECT TOP 1 GiaMoi FROM CapNhatGia 
                 WHERE MaMon = ct.MaMon 
                 ORDER BY NgayCapNhat DESC, SoLanCapNhatTrongNgay DESC) AS DonGia,
                (ct.SoLuong * (SELECT TOP 1 GiaMoi FROM CapNhatGia 
                               WHERE MaMon = ct.MaMon 
                               ORDER BY NgayCapNhat DESC, SoLanCapNhatTrongNgay DESC)) AS ThanhTien,
                kh.TenKH, 
                kh.SDT, 
                nv.TenNV
            FROM ChiTietHD ct
            INNER JOIN Kho k ON ct.MaMon = k.MaMon
            INNER JOIN Ban b ON ct.MaBan = b.MaBan
            LEFT JOIN DatBan db ON b.MaDatBan = db.MaDatBan
            LEFT JOIN KhachHang kh ON db.MaKH = kh.MaKH
            LEFT JOIN NhanVien nv ON b.MaNV = nv.MaNV
            WHERE ct.MaBan = @MaBan";

                    var da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@MaBan", maBanHienTai);
                    dataHoaDon = new DataTable();
                    da.Fill(dataHoaDon);

                    if (dataHoaDon.Rows.Count > 0)
                    {
                        tenKhachHangIn = dataHoaDon.Rows[0]["TenKH"]?.ToString() ?? "Khách lẻ";
                        sdtKhachHangIn = dataHoaDon.Rows[0]["SDT"]?.ToString() ?? "";
                        tenNhanVienIn = dataHoaDon.Rows[0]["TenNV"]?.ToString() ?? "";
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu hóa đơn để in!", "Thông báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu in: " + ex.Message);
            }
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            var g = e.Graphics;
            var fontTieuDe = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
            var fontThongTin = new System.Drawing.Font("Arial", 10);
            var fontMon = new System.Drawing.Font("Arial", 9);
            var fontTong = new System.Drawing.Font("Arial", 11, FontStyle.Bold);

            var yPos = 50f;
            var leftMargin = 50f;
            var rightMargin = 550f;

            // Tiêu đề
            g.DrawString("HÓA ĐƠN THANH TOÁN", fontTieuDe, Brushes.Black, leftMargin + 100, yPos);
            yPos += 40;

            // Thông tin cửa hàng
            g.DrawString("QUÁN BAR SÀI GÒN", fontThongTin, Brushes.Black, leftMargin, yPos);
            yPos += 20;
            g.DrawString("Địa chỉ: 123 Nguyễn Văn Linh, Quận 7, TP.HCM", fontThongTin, Brushes.Black, leftMargin, yPos);
            yPos += 20;
            g.DrawString("Điện thoại: 028 1234 5678", fontThongTin, Brushes.Black, leftMargin, yPos);
            yPos += 30;

            // Thông tin hóa đơn
            g.DrawString($"Mã bàn: {maBanHienTai}", fontThongTin, Brushes.Black, leftMargin, yPos);
            g.DrawString($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm}", fontThongTin, Brushes.Black, rightMargin - 150, yPos);
            yPos += 20;
            g.DrawString($"Khách hàng: {tenKhachHangIn}", fontThongTin, Brushes.Black, leftMargin, yPos);
            yPos += 20;
            g.DrawString($"SĐT: {sdtKhachHangIn}", fontThongTin, Brushes.Black, leftMargin, yPos);
            yPos += 20;
            g.DrawString($"Nhân viên: {tenNhanVienIn}", fontThongTin, Brushes.Black, leftMargin, yPos);
            yPos += 30;

            // Tiêu đề bảng
            g.DrawString("Tên món", fontThongTin, Brushes.Black, leftMargin, yPos);
            g.DrawString("SL", fontThongTin, Brushes.Black, leftMargin + 200, yPos);
            g.DrawString("Đơn giá", fontThongTin, Brushes.Black, leftMargin + 250, yPos);
            g.DrawString("Thành tiền", fontThongTin, Brushes.Black, rightMargin - 100, yPos);
            yPos += 25;

            // Đường kẻ ngang
            g.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos);
            yPos += 10;

            // Chi tiết món
            decimal tongTienIn = 0;
            foreach (DataRow row in dataHoaDon.Rows)
            {
                var tenMon = row["TenMon"].ToString();
                var soLuong = Convert.ToInt32(row["SoLuong"]);
                var donGia = Convert.ToDecimal(row["DonGia"]);
                var thanhTien = Convert.ToDecimal(row["ThanhTien"]);

                if (tenMon.Length > 25) tenMon = tenMon.Substring(0, 25) + "...";

                g.DrawString(tenMon, fontMon, Brushes.Black, leftMargin, yPos);
                g.DrawString(soLuong.ToString(), fontMon, Brushes.Black, leftMargin + 200, yPos);
                g.DrawString(donGia.ToString("N0") + " đ", fontMon, Brushes.Black, leftMargin + 250, yPos);
                g.DrawString(thanhTien.ToString("N0") + " đ", fontMon, Brushes.Black, rightMargin - 100, yPos);

                yPos += 20;
                tongTienIn += thanhTien;
            }

            // Tổng tiền
            yPos += 10;
            g.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos);
            yPos += 20;

            g.DrawString("TỔNG TIỀN:", fontTong, Brushes.Black, rightMargin - 150, yPos);
            g.DrawString(tongTienIn.ToString("N0") + " đ", fontTong, Brushes.Black, rightMargin - 100, yPos);
            yPos += 30;

            // Lời cảm ơn
            g.DrawString("Cảm ơn quý khách!", fontThongTin, Brushes.Black, leftMargin + 150, yPos);
            yPos += 20;
            g.DrawString("Hẹn gặp lại!", fontThongTin, Brushes.Black, leftMargin + 170, yPos);
        }



        private void ExportToExcel(string filePath)
        {
            try
            {
                // Kiểm tra dữ liệu
                if (dataHoaDon == null || dataHoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất Excel!", "Thông báo");
                    return;
                }

                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workbook = excelApp.Workbooks.Add();
                var worksheet = workbook.ActiveSheet;

                // Tiêu đề
                worksheet.Cells[1, 1] = "HÓA ĐƠN THANH TOÁN - QUÁN BAR SÀI GÒN";
                var titleRange = worksheet.Range["A1:E1"];
                titleRange.Merge();
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Thông tin hóa đơn
                worksheet.Cells[3, 1] = $"Mã bàn: {maBanHienTai}";
                worksheet.Cells[3, 4] = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}";
                worksheet.Cells[4, 1] = $"Khách hàng: {tenKhachHangIn}";
                worksheet.Cells[5, 1] = $"SĐT: {sdtKhachHangIn}";
                worksheet.Cells[6, 1] = $"Nhân viên: {tenNhanVienIn}";

                // Tiêu đề cột
                worksheet.Cells[8, 1] = "STT";
                worksheet.Cells[8, 2] = "Tên Món";
                worksheet.Cells[8, 3] = "Số Lượng";
                worksheet.Cells[8, 4] = "Đơn Giá";
                worksheet.Cells[8, 5] = "Thành Tiền";

                // Định dạng tiêu đề cột
                var headerRange = worksheet.Range["A8:E8"];
                headerRange.Font.Bold = true;
                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

                // Dữ liệu
                var rowIndex = 9;
                decimal tongTienExcel = 0;
                var stt = 1;

                foreach (DataRow row in dataHoaDon.Rows)
                {
                    worksheet.Cells[rowIndex, 1] = stt;
                    worksheet.Cells[rowIndex, 2] = row["TenMon"].ToString();
                    worksheet.Cells[rowIndex, 3] = Convert.ToInt32(row["SoLuong"]);
                    worksheet.Cells[rowIndex, 4] = Convert.ToDecimal(row["DonGia"]);
                    worksheet.Cells[rowIndex, 5] = Convert.ToDecimal(row["ThanhTien"]);

                    tongTienExcel += Convert.ToDecimal(row["ThanhTien"]);
                    rowIndex++;
                    stt++;
                }

                // Tổng tiền
                worksheet.Cells[rowIndex + 1, 4] = "TỔNG TIỀN:";
                worksheet.Cells[rowIndex + 1, 5] = tongTienExcel;

                // Định dạng tiền
                var moneyRange = worksheet.Range[$"D9:E{rowIndex + 1}"];
                moneyRange.NumberFormat = "#,##0";

                // Định dạng cột
                worksheet.Columns[1].ColumnWidth = 8;
                worksheet.Columns[2].ColumnWidth = 30;
                worksheet.Columns[3].ColumnWidth = 12;
                worksheet.Columns[4].ColumnWidth = 15;
                worksheet.Columns[5].ColumnWidth = 15;

                // Lưu file
                workbook.SaveAs(filePath);
                workbook.Close();
                excelApp.Quit();

                // Giải phóng COM objects
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                MessageBox.Show("Xuất Excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
        }
        #endregion

        #region QUẢN LÝ KHÁCH HÀNG
        private void btnQuanLyKH_Click(object sender, EventArgs e)
        {
            // Toggle hiển thị
            bool hienThi = !pnlKhachHang.Visible;

            pnlKhachHang.Visible = hienThi;
            pnlKhachHangTop.Visible = hienThi;
            dgvKhachHang.Visible = hienThi;  // ← Cũng nên toggle DataGridView

            if (hienThi)
            {
                LoadKhachHang();
                btnQuanLyKH.BackColor = Color.DarkGoldenrod;

                // Đưa focus vào textbox tên khách hàng
                txtTenKH.Focus();
            }
            else
            {
                btnQuanLyKH.BackColor = Color.Goldenrod;
                ClearKhachHangForm();
            }
        }

        private void LoadKhachHang()
        {
            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    var query = "SELECT MaKH, TenKH, SDT FROM KhachHang ORDER BY TenKH";
                    var da = new SqlDataAdapter(query, conn);
                    var dt = new DataTable();
                    da.Fill(dt);

                    dgvKhachHang.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load khách hàng: " + ex.Message);
            }
        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKH.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!");
                return;
            }

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    var query = "INSERT INTO KhachHang(TenKH, SDT) VALUES(@Ten, @SDT)";
                    var cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Ten", txtTenKH.Text.Trim());
                    cmd.Parameters.AddWithValue("@SDT", txtSDTKH.Text.Trim());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Thêm khách hàng thành công!");
                    LoadKhachHang();
                    ClearKhachHangForm();
                    LoadComboBoxKhachHang();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm khách hàng: " + ex.Message);
            }
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!");
                return;
            }

            try
            {
                using (var conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    var query = "UPDATE KhachHang SET TenKH=@Ten, SDT=@SDT WHERE MaKH=@Ma";
                    var cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Ma", txtMaKH.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ten", txtTenKH.Text.Trim());
                    cmd.Parameters.AddWithValue("@SDT", txtSDTKH.Text.Trim());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cập nhật khách hàng thành công!");
                    LoadKhachHang();
                    ClearKhachHangForm();
                    LoadComboBoxKhachHang();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật khách hàng: " + ex.Message);
            }
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!");
                return;
            }

            var result = MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (var conn = DatabaseConnection.GetConnection())
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        var query = "DELETE FROM KhachHang WHERE MaKH=@Ma";
                        var cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Ma", txtMaKH.Text.Trim());
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Xóa khách hàng thành công!");
                        LoadKhachHang();
                        ClearKhachHangForm();
                        LoadComboBoxKhachHang();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa khách hàng: " + ex.Message);
                }
            }
        }

        private void ClearKhachHangForm()
        {
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtSDTKH.Clear();
            txtTimKiemKH.Clear();
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvKhachHang.Rows[e.RowIndex];
                txtMaKH.Text = row.Cells["MaKH"].Value?.ToString() ?? "";
                txtTenKH.Text = row.Cells["TenKH"].Value?.ToString() ?? "";
                txtSDTKH.Text = row.Cells["SDT"].Value?.ToString() ?? "";
            }
        }

        private void btnTimKiemKH_Click(object sender, EventArgs e)
        {
            var tuKhoa = txtTimKiemKH.Text.Trim();
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                try
                {
                    using (var conn = DatabaseConnection.GetConnection())
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        var query = "SELECT MaKH, TenKH, SDT FROM KhachHang WHERE TenKH LIKE @TuKhoa OR SDT LIKE @TuKhoa ORDER BY TenKH";
                        var da = new SqlDataAdapter(query, conn);
                        da.SelectCommand.Parameters.AddWithValue("@TuKhoa", $"%{tuKhoa}%");
                        var dt = new DataTable();
                        da.Fill(dt);

                        dgvKhachHang.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tìm kiếm khách hàng: " + ex.Message);
                }
            }
            else
            {
                LoadKhachHang();
            }
        }

        private void btnLamMoiKH_Click(object sender, EventArgs e)
        {
            ClearKhachHangForm();
            LoadKhachHang();
        }
        #endregion

        #region HELPER METHODS
        private void LamMoiForm()
        {
            maBanHienTai = "";
            tongTien = 0;
            txtMaBan.Clear();
            txtSDT.Clear();
            txtTongTien.Clear();
            lblBanDangChon.Text = "Bàn đang chọn: Chưa chọn";
            cboTrangThai.SelectedIndex = -1;
            cboNhanVien.SelectedIndex = -1;
            cboKhachHang.SelectedIndex = -1;
            dtpNgayDat.Value = DateTime.Now;
            dgvHoaDon.DataSource = null;

            if (SessionInfo.MaQuyen == "Q02")
            {
                try
                {
                    cboNhanVien.SelectedValue = SessionInfo.MaNV;
                }
                catch
                {
                    // Bỏ qua nếu không set được
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            var tuKhoa = txtTimKiem.Text.Trim();
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                // Tìm kiếm bàn
                foreach (Control c in flpTatCa.Controls.OfType<System.Windows.Forms.Button>())
                {
                    var btn = (System.Windows.Forms.Button)c;
                    if (btn.Text.Contains(tuKhoa))
                    {
                        btn.BackColor = Color.Yellow;
                    }
                    else
                    {
                        btn.BackColor = GetColorByStatus(btn.Text.Split('\n')[1].Trim('(', ')'));
                    }
                }
            }
            else
            {
                LoadDanhSachBan();
            }
        }

        private void cboKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedValue != null)
            {
                try
                {
                    using (var conn = DatabaseConnection.GetConnection())
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        var query = "SELECT SDT FROM KhachHang WHERE MaKH = @MaKH";
                        var cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@MaKH", cboKhachHang.SelectedValue);
                        var result = cmd.ExecuteScalar();
                        txtSDT.Text = result?.ToString() ?? "";
                    }
                }
                catch { }
            }
        }

        private void btnGopBan_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng gộp bàn đang phát triển!");
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            //        // Tạo form thống kê đơn giản
            //        Form formThongKe = new Form();
            //        formThongKe.Text = "THỐNG KÊ DOANH THU";
            //        formThongKe.Size = new System.Drawing.Size(700, 500);
            //        formThongKe.StartPosition = FormStartPosition.CenterScreen;
            //        formThongKe.MaximizeBox = false;
            //        formThongKe.FormBorderStyle = FormBorderStyle.FixedDialog;

            //        // Tạo controls
            //        DateTimePicker dtpTuNgay = new DateTimePicker();
            //        DateTimePicker dtpDenNgay = new DateTimePicker();
            //        System.Windows.Forms.Button btnThongKe = new System.Windows.Forms.Button();
            //        System.Windows.Forms.Button btnXuatExcel = new System.Windows.Forms.Button();
            //        System.Windows.Forms.Button btnDong = new System.Windows.Forms.Button();
            //        DataGridView dgvThongKe = new DataGridView();
            //        System.Windows.Forms.Label lblTongDoanhThu = new System.Windows.Forms.Label();
            //        System.Windows.Forms.Label label1 = new System.Windows.Forms.Label();
            //        System.Windows.Forms.Label label2 = new System.Windows.Forms.Label();

            //        // Đặt vị trí và kích thước
            //        dtpTuNgay.Location = new System.Drawing.Point(80, 20);
            //        dtpTuNgay.Size = new System.Drawing.Size(120, 20);
            //        dtpTuNgay.Value = DateTime.Now.AddDays(-7);

            //        dtpDenNgay.Location = new System.Drawing.Point(280, 20);
            //        dtpDenNgay.Size = new System.Drawing.Size(120, 20);
            //        dtpDenNgay.Value = DateTime.Now;

            //        btnThongKe.Location = new System.Drawing.Point(420, 18);
            //        btnThongKe.Size = new System.Drawing.Size(80, 25);
            //        btnThongKe.Text = "Thống kê";
            //        btnThongKe.BackColor = System.Drawing.Color.LightBlue;

            //        btnXuatExcel.Location = new System.Drawing.Point(510, 18);
            //        btnXuatExcel.Size = new System.Drawing.Size(80, 25);
            //        btnXuatExcel.Text = "Xuất Excel";
            //        btnXuatExcel.BackColor = System.Drawing.Color.LightGreen;

            //        btnDong.Location = new System.Drawing.Point(600, 18);
            //        btnDong.Size = new System.Drawing.Size(60, 25);
            //        btnDong.Text = "Đóng";
            //        btnDong.BackColor = System.Drawing.Color.LightCoral;

            //        label1.Location = new System.Drawing.Point(20, 23);
            //        label1.Size = new System.Drawing.Size(60, 20);
            //        label1.Text = "Từ ngày:";

            //        label2.Location = new System.Drawing.Point(220, 23);
            //        label2.Size = new System.Drawing.Size(60, 20);
            //        label2.Text = "Đến ngày:";

            //        dgvThongKe.Location = new System.Drawing.Point(20, 60);
            //        dgvThongKe.Size = new System.Drawing.Size(650, 350);
            //        dgvThongKe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //        dgvThongKe.ReadOnly = true;

            //        lblTongDoanhThu.Location = new System.Drawing.Point(20, 420);
            //        lblTongDoanhThu.Size = new System.Drawing.Size(400, 30);
            //        lblTongDoanhThu.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            //        lblTongDoanhThu.ForeColor = System.Drawing.Color.Red;

            //        // Thêm controls vào form
            //        formThongKe.Controls.AddRange(new Control[] {
            //    dtpTuNgay, dtpDenNgay, btnThongKe, btnXuatExcel, btnDong,
            //    dgvThongKe, lblTongDoanhThu, label1, label2
            //});

            //        // Sự kiện thống kê
            //        btnThongKe.Click += (s, ev) =>
            //        {
            //            LoadThongKeDoanhThu(dtpTuNgay.Value, dtpDenNgay.Value, dgvThongKe, lblTongDoanhThu);
            //        };

            //        // Sự kiện xuất Excel
            //        btnXuatExcel.Click += (s, ev) =>
            //        {
            //            XuatExcelThongKe(dgvThongKe, dtpTuNgay.Value, dtpDenNgay.Value);
            //        };

            //        // Sự kiện đóng form
            //        btnDong.Click += (s, ev) =>
            //        {
            //            formThongKe.Close();
            //        };

            //        // Load dữ liệu ban đầu
            //        LoadThongKeDoanhThu(dtpTuNgay.Value, dtpDenNgay.Value, dgvThongKe, lblTongDoanhThu);

            //        formThongKe.ShowDialog();

            try
            {
                //Kiểm tra quyền(nếu cần)
                 if (!SessionInfo.IsAdmin && SessionInfo.MaQuyen != "Q03")
                {
                    MessageBox.Show("Bạn không có quyền xem lịch sử!", "Thông báo");
                    return;
                }

                // Mở form Lịch Sử Bàn
                DSHoaDon formLichSu = new DSHoaDon();
                formLichSu.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở form lịch sử: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadThongKeDoanhThu(DateTime tuNgay, DateTime denNgay, DataGridView dgv, System.Windows.Forms.Label lblTong)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ThongKeDoanhThu", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                    cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Hiển thị lưới
                    dgv.DataSource = dt;

                    // Tính tổng doanh thu
                    decimal tongDoanhThu = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        tongDoanhThu += Convert.ToDecimal(row["TongDoanhThu"]);
                    }

                    lblTong.Text = $"TỔNG DOANH THU: {tongDoanhThu:N0} đ";

                    // Định dạng DataGridView
                    if (dgv.Columns.Count > 0)
                    {
                        dgv.Columns["Ngay"].HeaderText = "NGÀY";
                        dgv.Columns["SoHoaDon"].HeaderText = "SỐ HÓA ĐƠN";
                        dgv.Columns["TongDoanhThu"].HeaderText = "TỔNG DOANH THU";

                        dgv.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                        dgv.Columns["TongDoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thống kê: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XuatExcelThongKe(DataGridView dgv, DateTime tuNgay, DateTime denNgay)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo");
                return;
            }

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.FileName = $"ThongKeDoanhThu_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportThongKeToExcel(dgv, saveFileDialog.FileName, tuNgay, denNgay);
                    MessageBox.Show("Xuất Excel thành công!", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi");
            }
        }

        private void ExportThongKeToExcel(DataGridView dgv, string filePath, DateTime tuNgay, DateTime denNgay)
        {
            try
            {
                var excelApp = new Microsoft.Office.Interop.Excel.Application();
                var workbook = excelApp.Workbooks.Add();
                var worksheet = workbook.ActiveSheet;

                // Tiêu đề
                worksheet.Cells[1, 1] = "THỐNG KÊ DOANH THU - QUÁN BAR SÀI GÒN";
                var titleRange = worksheet.Range["A1:C1"];
                titleRange.Merge();
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Thông tin thời gian
                worksheet.Cells[2, 1] = $"Từ ngày: {tuNgay:dd/MM/yyyy} đến {denNgay:dd/MM/yyyy}";
                worksheet.Cells[3, 1] = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}";

                // Tiêu đề cột
                worksheet.Cells[5, 1] = "Ngày";
                worksheet.Cells[5, 2] = "Số hóa đơn";
                worksheet.Cells[5, 3] = "Tổng doanh thu (VNĐ)";

                // Định dạng tiêu đề cột
                var headerRange = worksheet.Range["A5:C5"];
                headerRange.Font.Bold = true;
                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

                // Dữ liệu
                var rowIndex = 6;
                decimal tongDoanhThu = 0;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;

                    worksheet.Cells[rowIndex, 1] = Convert.ToDateTime(row.Cells["Ngay"].Value).ToString("dd/MM/yyyy");
                    worksheet.Cells[rowIndex, 2] = row.Cells["SoHoaDon"].Value;
                    worksheet.Cells[rowIndex, 3] = Convert.ToDecimal(row.Cells["TongDoanhThu"].Value);

                    tongDoanhThu += Convert.ToDecimal(row.Cells["TongDoanhThu"].Value);
                    rowIndex++;
                }

                // Tổng doanh thu
                worksheet.Cells[rowIndex + 1, 2] = "TỔNG DOANH THU:";
                worksheet.Cells[rowIndex + 1, 3] = tongDoanhThu;

                // Định dạng tiền
                var moneyRange = worksheet.Range[$"C6:C{rowIndex + 1}"];
                moneyRange.NumberFormat = "#,##0";

                // Định dạng cột
                worksheet.Columns[1].ColumnWidth = 15;
                worksheet.Columns[2].ColumnWidth = 15;
                worksheet.Columns[3].ColumnWidth = 20;

                // Lưu file
                workbook.SaveAs(filePath);
                workbook.Close();
                excelApp.Quit();

                // Giải phóng COM objects
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xuất Excel: " + ex.Message);
            }
        }

        private void btnThemMon_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nhấn vào món trong thực đơn để thêm!");
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(maBanHienTai))
            {
                MessageBox.Show("Vui lòng chọn bàn trước!", "Thông báo");
                return;
            }

            if (dgvHoaDon.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn món cần xóa!", "Thông báo");
                return;
            }

            var result = MessageBox.Show("Bạn có chắc muốn xóa món này?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Lấy tên món từ dòng được chọn
                    string tenMon = dgvHoaDon.SelectedRows[0].Cells["Tên Món"].Value.ToString();

                    using (var conn = DatabaseConnection.GetConnection())
                    {
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();

                        // Lấy MaMon từ TenMon
                        var getMaMon = new SqlCommand("SELECT MaMon FROM Kho WHERE TenMon = @TenMon", conn);
                        getMaMon.Parameters.AddWithValue("@TenMon", tenMon);
                        var maMon = getMaMon.ExecuteScalar()?.ToString();

                        if (string.IsNullOrEmpty(maMon))
                        {
                            MessageBox.Show("Không tìm thấy món này!");
                            return;
                        }

                        // Lấy số lượng trước khi xóa để hoàn trả kho
                        var getSoLuong = new SqlCommand(
                            "SELECT SoLuong FROM ChiTietHD WHERE MaBan = @MaBan AND MaMon = @MaMon", conn);
                        getSoLuong.Parameters.AddWithValue("@MaBan", maBanHienTai);
                        getSoLuong.Parameters.AddWithValue("@MaMon", maMon);
                        var soLuong = Convert.ToInt32(getSoLuong.ExecuteScalar());

                        // Xóa món khỏi hóa đơn
                        var deleteCmd = new SqlCommand(
                            "DELETE FROM ChiTietHD WHERE MaBan = @MaBan AND MaMon = @MaMon", conn);
                        deleteCmd.Parameters.AddWithValue("@MaBan", maBanHienTai);
                        deleteCmd.Parameters.AddWithValue("@MaMon", maMon);
                        deleteCmd.ExecuteNonQuery();

                        // Hoàn trả kho (vì trigger đã trừ khi thêm món)
                        var updateKho = new SqlCommand(
                            "UPDATE Kho SET SoLuongTon = SoLuongTon + @SoLuong WHERE MaMon = @MaMon", conn);
                        updateKho.Parameters.AddWithValue("@SoLuong", soLuong);
                        updateKho.Parameters.AddWithValue("@MaMon", maMon);
                        updateKho.ExecuteNonQuery();

                        MessageBox.Show("Đã xóa món thành công!");
                        LoadChiTietHoaDon(maBanHienTai);
                        LoadDanhSachBan();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa món: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
        private bool KiemTraDuLieuHoaDon()
        {
            if (string.IsNullOrEmpty(maBanHienTai))
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo");
                return false;
            }

            if (tongTien <= 0)
            {
                MessageBox.Show("Bàn chưa có món nào!", "Thông báo");
                return false;
            }

            return true;
        }

        // Sửa lại các hàm in và xuất Excel
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieuHoaDon()) return;

            try
            {
                LoadDataForPrinting();

                // Kiểm tra lại dữ liệu sau khi load
                if (dataHoaDon == null || dataHoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để in!", "Thông báo");
                    return;
                }

                var printDialog = new PrintDialog { Document = printDocument };
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                    MessageBox.Show("In hóa đơn thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi in hóa đơn: " + ex.Message);
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieuHoaDon()) return;

            try
            {
                // Load dữ liệu trước khi xuất
                LoadDataForPrinting();

                // Kiểm tra lại dữ liệu sau khi load
                if (dataHoaDon == null || dataHoaDon.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất Excel!", "Thông báo");
                    return;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    FileName = $"HoaDon_{maBanHienTai}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToExcel(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
        }

        #region EVENT HANDLERS TRỐNG
        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtTimKiem_TextChanged(object sender, EventArgs e) { }
        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtMaBan_TextChanged(object sender, EventArgs e) { }
        private void txtSDT_TextChanged(object sender, EventArgs e) { }
        private void dtpNgayDat_ValueChanged(object sender, EventArgs e) { }
        private void flpChucNang_Paint(object sender, PaintEventArgs e) { }
        private void pnlLeft_Paint(object sender, PaintEventArgs e) { }
        private void pnlCenter_Paint(object sender, PaintEventArgs e) { }
        private void pnlRight_Paint(object sender, PaintEventArgs e) { }
        private void flpTatCa_Paint(object sender, PaintEventArgs e) { }
        private void txtMaKH_TextChanged(object sender, EventArgs e) { }
        private void grpMenu_Enter(object sender, EventArgs e) { }
        #endregion
    }
}