using QLQB_ChucNang_QLNhanVien_va_LichLamViec.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    public partial class QuanLyNhapHang : Form
    {
        bool sortAscending = true;
        private string maNV;
        private string tenNV;
        private string maQuyen;

        public QuanLyNhapHang(string maNV, string tenNV, string maQuyen)
        {
            InitializeComponent();
            this.maNV = maNV;
            this.tenNV = tenNV;
            this.maQuyen = maQuyen;
        }

        public QuanLyNhapHang() { InitializeComponent(); }

        private void QuanLyNhapHang_Load(object sender, EventArgs e)
        {
            cbThang.SelectedIndex = DateTime.Now.Month - 1;
            cbNam.SelectedItem = DateTime.Now.Year;

            // 🔹 Thiết lập DataGridView cho phép nhập liệu
            ConfigureDataGridView();

            LoadPhieuNhap();
            LoadCanhBao();
            LoadTongGiaTriNhap();


        }

        private void ConfigureDataGridView()
        {
            dgvPhieuNhap.AllowUserToAddRows = true;
            dgvPhieuNhap.ReadOnly = false;
            dgvPhieuNhap.EditMode = DataGridViewEditMode.EditOnEnter;

            // 🔹 Tạo các cột thủ công
            dgvPhieuNhap.Columns.Clear();
            dgvPhieuNhap.AutoGenerateColumns = false;

            // Mã phiếu nhập (ReadOnly)
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaPhieuNhap",
                HeaderText = "Mã Phiếu",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = { BackColor = System.Drawing.Color.LightGray }
            });

            // Ngày nhập (ReadOnly)
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayNhap",
                HeaderText = "Ngày Nhập",
                ReadOnly = true,
                Width = 100,
                DefaultCellStyle = { BackColor = System.Drawing.Color.LightGray }
            });

            // Tên món (Editable - ComboBox)
            DataGridViewComboBoxColumn colTenMon = new DataGridViewComboBoxColumn
            {
                Name = "TenMon",
                HeaderText = "Tên Món",
                Width = 200,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
            };
            LoadDanhSachMon(colTenMon);
            dgvPhieuNhap.Columns.Add(colTenMon);

            // Số lượng nhập (Editable)
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoLuongNhap",
                HeaderText = "SL Nhập",
                Width = 80
            });

            // Số lượng tồn trước (ReadOnly)
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoLuongTonTruoc",
                HeaderText = "Tồn Trước",
                ReadOnly = true,
                Width = 80,
                DefaultCellStyle = {
                    BackColor = System.Drawing.Color.LightYellow,
                    ForeColor = System.Drawing.Color.OrangeRed
                }
            });

            // Số lượng tồn sau (ReadOnly)
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoLuongTonSau",
                HeaderText = "Tồn Sau",
                ReadOnly = true,
                Width = 80,
                DefaultCellStyle = {
                    BackColor = System.Drawing.Color.LightGreen,
                    ForeColor = System.Drawing.Color.Green
                }
            });

            // Tổng tiền (Editable)
            dgvPhieuNhap.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TongTien",
                HeaderText = "Tổng Tiền",
                Width = 120
            });

            // 🔹 Đăng ký sự kiện
            dgvPhieuNhap.DefaultValuesNeeded += dgvPhieuNhap_DefaultValuesNeeded;
            dgvPhieuNhap.CellValueChanged += dgvPhieuNhap_CellValueChanged;
            dgvPhieuNhap.CurrentCellDirtyStateChanged += dgvPhieuNhap_CurrentCellDirtyStateChanged;
        }

        private void dgvPhieuNhap_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Commit ngay lập tức khi thay đổi ComboBox
            if (dgvPhieuNhap.IsCurrentCellDirty &&
                dgvPhieuNhap.CurrentCell is DataGridViewComboBoxCell)
            {
                dgvPhieuNhap.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void LoadDanhSachMon(DataGridViewComboBoxColumn column)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT TenMon FROM Kho ORDER BY TenMon", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        column.Items.Add(reader["TenMon"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách món:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPhieuNhap_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            // 🔹 Tự động điền Mã phiếu nhập
            string maMoi = TaoMaPhieuNhapMoi();
            e.Row.Cells["MaPhieuNhap"].Value = maMoi;

            // 🔹 Tự động điền Ngày nhập
            e.Row.Cells["NgayNhap"].Value = DateTime.Now.ToString("yyyy-MM-dd");

            // 🔹 Khởi tạo giá trị mặc định
            e.Row.Cells["SoLuongNhap"].Value = 0;
            e.Row.Cells["SoLuongTonTruoc"].Value = 0;
            e.Row.Cells["SoLuongTonSau"].Value = 0;
            e.Row.Cells["TongTien"].Value = 0;
        }

        private string TaoMaPhieuNhapMoi()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand(
                        @"SELECT TOP 1 MaPhieuNhap 
                          FROM PhieuNhap 
                          ORDER BY MaPhieuNhap DESC", conn);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string maCuoi = result.ToString();
                        // Lấy phần số sau "PN"
                        if (maCuoi.Length > 2)
                        {
                            int soThuTu = int.Parse(maCuoi.Substring(2)) + 1;
                            return "PN" + soThuTu.ToString("D3");
                        }
                    }

                    return "PN001";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo mã phiếu:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "PN" + DateTime.Now.Ticks % 1000;
            }
        }

        private void dgvPhieuNhap_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvPhieuNhap.Rows[e.RowIndex];

            // 🔹 Khi chọn Tên món
            if (e.ColumnIndex == dgvPhieuNhap.Columns["TenMon"].Index)
            {
                string tenMon = row.Cells["TenMon"].Value?.ToString();
                if (string.IsNullOrEmpty(tenMon)) return;

                try
                {
                    using (SqlConnection conn = DatabaseConnection.OpenConnection())
                    {
                        SqlCommand cmd = new SqlCommand(
                            "SELECT SoLuongTon FROM Kho WHERE TenMon = @TenMon", conn);
                        cmd.Parameters.AddWithValue("@TenMon", tenMon);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            int soLuongTon = Convert.ToInt32(result);
                            row.Cells["SoLuongTonTruoc"].Value = soLuongTon;

                            // Tính tồn sau nếu đã có số lượng nhập
                            int slNhap = 0;
                            if (int.TryParse(row.Cells["SoLuongNhap"].Value?.ToString(), out slNhap))
                            {
                                row.Cells["SoLuongTonSau"].Value = soLuongTon + slNhap;
                            }
                            else
                            {
                                row.Cells["SoLuongTonSau"].Value = soLuongTon;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lấy thông tin kho:\n" + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // 🔹 Khi nhập Số lượng nhập
            if (e.ColumnIndex == dgvPhieuNhap.Columns["SoLuongNhap"].Index)
            {
                if (int.TryParse(row.Cells["SoLuongNhap"].Value?.ToString(), out int slNhap) &&
                    int.TryParse(row.Cells["SoLuongTonTruoc"].Value?.ToString(), out int tonTruoc))
                {
                    row.Cells["SoLuongTonSau"].Value = tonTruoc + slNhap;
                }
            }
        }

        private void LoadPhieuNhap()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    string query = @"
                        SELECT 
                            p.MaPhieuNhap,
                            CONVERT(VARCHAR(10), p.NgayNhap, 120) AS NgayNhap,
                            k.TenMon,
                            ct.SoLuongNhap,
                            (k.SoLuongTon - ct.SoLuongNhap) AS SoLuongTonTruoc,
                            k.SoLuongTon AS SoLuongTonSau,
                            p.TongTien
                        FROM PhieuNhap p
                        JOIN Kho k ON p.MaMon = k.MaMon
                        JOIN ChiTietPN ct ON p.MaPhieuNhap = ct.MaPhieuNhap
                        ORDER BY p.NgayNhap DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // 🔹 Xóa dòng cũ (giữ dòng trống để nhập mới)
                    if (dgvPhieuNhap.Columns.Count > 0)
                    {
                        dgvPhieuNhap.Rows.Clear();
                    }

                    // 🔹 Load dữ liệu vào DataGridView
                    foreach (DataRow dr in dt.Rows)
                    {
                        int idx = dgvPhieuNhap.Rows.Add();
                        DataGridViewRow row = dgvPhieuNhap.Rows[idx];

                        row.Cells["MaPhieuNhap"].Value = dr["MaPhieuNhap"];
                        row.Cells["NgayNhap"].Value = dr["NgayNhap"];
                        row.Cells["TenMon"].Value = dr["TenMon"];
                        row.Cells["SoLuongNhap"].Value = dr["SoLuongNhap"];
                        row.Cells["SoLuongTonTruoc"].Value = dr["SoLuongTonTruoc"];
                        row.Cells["SoLuongTonSau"].Value = dr["SoLuongTonSau"];
                        row.Cells["TongTien"].Value = dr["TongTien"];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu phiếu nhập:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCanhBao()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.fn_KiemTraHangTonThap(50)", conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvCanhBao.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải cảnh báo tồn kho:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTongGiaTriNhap()
        {
            if (cbThang.SelectedItem == null || cbNam.SelectedItem == null) return;

            try
            {
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT dbo.fn_TongGiaTriNhapTheoThang(@Thang, @Nam)", conn);
                    cmd.Parameters.AddWithValue("@Thang", cbThang.SelectedItem);
                    cmd.Parameters.AddWithValue("@Nam", cbNam.SelectedItem);

                    var result = cmd.ExecuteScalar();
                    decimal giaTri = (result == DBNull.Value || result == null) ? 0 : Convert.ToDecimal(result);

                    lblTongGiaTri.Text = $"💰 Tổng giá trị nhập tháng {cbThang.Text}/{cbNam.Text}:\n\n{giaTri:N0} VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thống kê:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int soLuongThemMoi = 0;
            int soLuongBoQua = 0;

            try
            {
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    foreach (DataGridViewRow row in dgvPhieuNhap.Rows)
                    {
                        // 🔹 Bỏ qua dòng trống hoặc dòng mới
                        if (row.IsNewRow) continue;

                        string maPhieu = row.Cells["MaPhieuNhap"].Value?.ToString();
                        string tenMon = row.Cells["TenMon"].Value?.ToString();
                        string ngayNhapStr = row.Cells["NgayNhap"].Value?.ToString();
                        string slNhapStr = row.Cells["SoLuongNhap"].Value?.ToString();
                        string tongTienStr = row.Cells["TongTien"].Value?.ToString();

                        // 🔹 Kiểm tra dữ liệu đầy đủ
                        if (string.IsNullOrEmpty(maPhieu) ||
                            string.IsNullOrEmpty(tenMon) ||
                            string.IsNullOrEmpty(slNhapStr) ||
                            string.IsNullOrEmpty(tongTienStr))
                        {
                            soLuongBoQua++;
                            continue;
                        }

                        // 🔹 Kiểm tra xem phiếu đã tồn tại chưa
                        SqlCommand checkCmd = new SqlCommand(
                            "SELECT COUNT(*) FROM PhieuNhap WHERE MaPhieuNhap = @MaPhieu", conn);
                        checkCmd.Parameters.AddWithValue("@MaPhieu", maPhieu);
                        int exists = (int)checkCmd.ExecuteScalar();

                        if (exists > 0)
                        {
                            soLuongBoQua++;
                            continue; // Đã tồn tại, bỏ qua
                        }

                        // 🔹 Validate dữ liệu
                        if (!int.TryParse(slNhapStr, out int soLuongNhap) || soLuongNhap <= 0)
                        {
                            MessageBox.Show($"Số lượng nhập không hợp lệ ở phiếu {maPhieu}!",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }

                        if (!decimal.TryParse(tongTienStr, out decimal tongTien) || tongTien <= 0)
                        {
                            MessageBox.Show($"Tổng tiền không hợp lệ ở phiếu {maPhieu}!",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }

                        // 🔹 Tính đơn giá
                        decimal donGia = tongTien / soLuongNhap;

                        // 🔹 Lấy MaMon từ TenMon
                        string maMon = "";
                        SqlCommand getMonCmd = new SqlCommand(
                            "SELECT MaMon FROM Kho WHERE TenMon = @TenMon", conn);
                        getMonCmd.Parameters.AddWithValue("@TenMon", tenMon);
                        object result = getMonCmd.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show($"Món '{tenMon}' không tồn tại trong kho!",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                        maMon = result.ToString();

                        // 🔹 Parse ngày nhập
                        DateTime ngayNhap;
                        if (!DateTime.TryParse(ngayNhapStr, out ngayNhap))
                        {
                            ngayNhap = DateTime.Now;
                        }

                        // 🔹 Gọi Stored Procedure
                        SqlCommand cmd = new SqlCommand("sp_LapPhieuNhapHang", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaPhieuNhap", maPhieu);
                        cmd.Parameters.AddWithValue("@MaMon", maMon);
                        cmd.Parameters.AddWithValue("@NgayNhap", ngayNhap);
                        cmd.Parameters.AddWithValue("@SoLuongNhap", soLuongNhap);
                        cmd.Parameters.AddWithValue("@TenMonNhap", tenMon);
                        cmd.Parameters.AddWithValue("@DonGia", donGia);

                        cmd.ExecuteNonQuery();
                        soLuongThemMoi++;
                    }

                    // 🔹 Thông báo kết quả
                    string thongBao = $"✅ Hoàn tất lập phiếu nhập!\n\n";
                    thongBao += $"• Đã thêm: {soLuongThemMoi} phiếu\n";
                    if (soLuongBoQua > 0)
                        thongBao += $"• Đã bỏ qua: {soLuongBoQua} dòng";

                    MessageBox.Show(thongBao, "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadPhieuNhap();
                    LoadCanhBao();
                    LoadTongGiaTriNhap();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi lập phiếu nhập:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvPhieuNhap.SelectedRows.Count == 0)
            {
                MessageBox.Show("⚠️ Vui lòng chọn phiếu nhập cần xóa!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhieu = dgvPhieuNhap.SelectedRows[0].Cells["MaPhieuNhap"].Value?.ToString();
            if (string.IsNullOrEmpty(maPhieu)) return;

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa phiếu nhập '{maPhieu}'?\n\nHành động này không thể hoàn tác!",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No) return;

            try
            {
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_XoaPhieuNhap", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaPhieuNhap", maPhieu);
                    cmd.Parameters.AddWithValue("@LyDo", "Xóa thủ công từ hệ thống.");
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("✅ Đã xóa phiếu nhập thành công!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadPhieuNhap();
                    LoadCanhBao();
                    LoadTongGiaTriNhap();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi xóa phiếu nhập:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPhieuNhap();
                LoadCanhBao();
                LoadTongGiaTriNhap();
                MessageBox.Show("🔄 Đã làm mới dữ liệu thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi làm mới dữ liệu:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPhieuNhap_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Bỏ qua nếu không có dữ liệu
            if (dgvPhieuNhap.Rows.Count <= 1) return;

            try
            {
                string columnName = dgvPhieuNhap.Columns[e.ColumnIndex].Name;

                // Sắp xếp dựa trên cột được click
                var rows = dgvPhieuNhap.Rows.Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow)
                    .OrderBy(r => r.Cells[columnName].Value?.ToString())
                    .ToList();

                if (!sortAscending)
                    rows.Reverse();

                dgvPhieuNhap.Rows.Clear();
                foreach (var row in rows)
                {
                    dgvPhieuNhap.Rows.Add(row);
                }

                sortAscending = !sortAscending;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sắp xếp: " + ex.Message);
            }
        }
    }
}

