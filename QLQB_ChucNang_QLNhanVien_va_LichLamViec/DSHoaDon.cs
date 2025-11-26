using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QLQB_ChucNang_QLNhanVien_va_LichLamViec.Database;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    public partial class DSHoaDon: Form
    {
        public DSHoaDon()
        {
            InitializeComponent();
        }

        private void LichSuBan_Load(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đăng nhập
                if (string.IsNullOrEmpty(SessionInfo.MaNV))
                {
                    MessageBox.Show("Chưa đăng nhập! Vui lòng đăng nhập lại.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Thiết lập ngày mặc định (30 ngày trước đến hôm nay)
                dtpTuNgay.Value = DateTime.Now.AddDays(-30);
                dtpDenNgay.Value = DateTime.Now;

                // Load dữ liệu ban đầu
                LoadLichSuBan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo form: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region LOAD DỮ LIỆU

        private void LoadLichSuBan()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    // Query lấy lịch sử bàn với thông tin đầy đủ
                    string query = @"
                        SELECT 
                            ls.MaBan AS 'Mã Bàn',
                            ls.NgayThanhToan AS 'Ngày Thanh Toán',
                            ls.ThoiDiemTT AS 'Thời Điểm',
                            b.HinhThucTT AS 'Hình Thức TT',
                            b.TongTien AS 'Tổng Tiền',
                            nv.TenNV AS 'Nhân Viên',
                            ISNULL(kh.TenKH, N'Khách lẻ') AS 'Tên Khách Hàng',
                            ISNULL(kh.SDT, '') AS 'SĐT'
                        FROM LichSuBan ls
                        INNER JOIN Ban b ON ls.MaBan = b.MaBan
                        LEFT JOIN NhanVien nv ON b.MaNV = nv.MaNV
                        LEFT JOIN DatBan db ON b.MaDatBan = db.MaDatBan
                        LEFT JOIN KhachHang kh ON db.MaKH = kh.MaKH
                        WHERE ls.NgayThanhToan BETWEEN @TuNgay AND @DenNgay";

                    // Thêm điều kiện lọc nếu có
                    if (!string.IsNullOrWhiteSpace(txtMaBan.Text))
                    {
                        query += " AND ls.MaBan LIKE @MaBan";
                    }

                    if (!string.IsNullOrWhiteSpace(txtTenKH.Text))
                    {
                        query += " AND kh.TenKH LIKE @TenKH";
                    }

                    query += " ORDER BY ls.NgayThanhToan DESC, ls.ThoiDiemTT DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TuNgay", dtpTuNgay.Value.Date);
                    cmd.Parameters.AddWithValue("@DenNgay", dtpDenNgay.Value.Date);

                    if (!string.IsNullOrWhiteSpace(txtMaBan.Text))
                    {
                        cmd.Parameters.AddWithValue("@MaBan", "%" + txtMaBan.Text.Trim() + "%");
                    }

                    if (!string.IsNullOrWhiteSpace(txtTenKH.Text))
                    {
                        cmd.Parameters.AddWithValue("@TenKH", "%" + txtTenKH.Text.Trim() + "%");
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvLichSu.DataSource = dt;

                    // Định dạng DataGridView
                    FormatDataGridView();

                    // Cập nhật thống kê
                    CapNhatThongKe(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load lịch sử: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvLichSu.Columns.Count == 0) return;

            // Thiết lập độ rộng cột
            dgvLichSu.Columns["Mã Bàn"].Width = 80;
            dgvLichSu.Columns["Ngày Thanh Toán"].Width = 120;
            dgvLichSu.Columns["Thời Điểm"].Width = 80;
            dgvLichSu.Columns["Hình Thức TT"].Width = 120;
            dgvLichSu.Columns["Tổng Tiền"].Width = 120;
            dgvLichSu.Columns["Nhân Viên"].Width = 150;
            dgvLichSu.Columns["Tên Khách Hàng"].Width = 180;
            dgvLichSu.Columns["SĐT"].Width = 120;

            // Định dạng tiền
            dgvLichSu.Columns["Tổng Tiền"].DefaultCellStyle.Format = "N0";
            dgvLichSu.Columns["Tổng Tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Định dạng ngày
            dgvLichSu.Columns["Ngày Thanh Toán"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvLichSu.Columns["Ngày Thanh Toán"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Định dạng thời điểm
            dgvLichSu.Columns["Thời Điểm"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Căn giữa mã bàn
            dgvLichSu.Columns["Mã Bàn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void CapNhatThongKe(DataTable dt)
        {
            int tongHoaDon = dt.Rows.Count;
            decimal tongDoanhThu = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["Tổng Tiền"] != DBNull.Value)
                {
                    tongDoanhThu += Convert.ToDecimal(row["Tổng Tiền"]);
                }
            }

            lblTongHoaDon.Text = $"Tổng hóa đơn: {tongHoaDon}";
            lblTongDoanhThu.Text = $"Tổng doanh thu: {tongDoanhThu:N0} đ";
        }

        #endregion

        #region SỰ KIỆN BUTTON

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Validate ngày
            if (dtpTuNgay.Value > dtpDenNgay.Value)
            {
                MessageBox.Show("Từ ngày không được lớn hơn đến ngày!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadLichSuBan();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // Reset các bộ lọc
            dtpTuNgay.Value = DateTime.Now.AddDays(-30);
            dtpDenNgay.Value = DateTime.Now;
            txtMaBan.Clear();
            txtTenKH.Clear();

            // Load lại dữ liệu
            LoadLichSuBan();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                FileName = $"LichSuBan_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                XuatExcel(saveFileDialog.FileName);
            }
        }

        #endregion

        #region XUẤT EXCEL

        private void XuatExcel(string filePath)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook workbook = excelApp.Workbooks.Add();
                Worksheet worksheet = workbook.ActiveSheet;

                // Tiêu đề
                worksheet.Cells[1, 1] = "LỊCH SỬ HÓA ĐƠN - QUÁN BAR SÀI GÒN";
                Range titleRange = worksheet.Range["A1:H1"];
                titleRange.Merge();
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                // Thông tin lọc
                worksheet.Cells[2, 1] = $"Từ ngày: {dtpTuNgay.Value:dd/MM/yyyy} - Đến ngày: {dtpDenNgay.Value:dd/MM/yyyy}";
                worksheet.Cells[3, 1] = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}";

                // Tiêu đề cột
                int colIndex = 1;
                int rowIndex = 5;

                foreach (DataGridViewColumn col in dgvLichSu.Columns)
                {
                    worksheet.Cells[rowIndex, colIndex] = col.HeaderText;
                    colIndex++;
                }

                // Định dạng tiêu đề cột
                Range headerRange = worksheet.Range[$"A{rowIndex}:H{rowIndex}"];
                headerRange.Font.Bold = true;
                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);

                // Dữ liệu
                rowIndex++;
                decimal tongDoanhThu = 0;

                foreach (DataGridViewRow row in dgvLichSu.Rows)
                {
                    if (row.IsNewRow) continue;

                    colIndex = 1;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        worksheet.Cells[rowIndex, colIndex] = cell.Value?.ToString() ?? "";
                        colIndex++;
                    }

                    // Tính tổng
                    if (row.Cells["Tổng Tiền"].Value != DBNull.Value)
                    {
                        tongDoanhThu += Convert.ToDecimal(row.Cells["Tổng Tiền"].Value);
                    }

                    rowIndex++;
                }

                // Tổng cộng
                worksheet.Cells[rowIndex + 1, 4] = "TỔNG CỘNG:";
                worksheet.Cells[rowIndex + 1, 5] = tongDoanhThu;

                Range totalLabelRange = worksheet.Cells[rowIndex + 1, 4];
                totalLabelRange.Font.Bold = true;
                totalLabelRange.HorizontalAlignment = XlHAlign.xlHAlignRight;

                Range totalValueRange = worksheet.Cells[rowIndex + 1, 5];
                totalValueRange.Font.Bold = true;
                totalValueRange.NumberFormat = "#,##0";

                // Định dạng cột tiền
                Range moneyRange = worksheet.Range[$"E6:E{rowIndex}"];
                moneyRange.NumberFormat = "#,##0";

                // Điều chỉnh độ rộng cột
                worksheet.Columns[1].ColumnWidth = 12;  // Mã Bàn
                worksheet.Columns[2].ColumnWidth = 15;  // Ngày TT
                worksheet.Columns[3].ColumnWidth = 12;  // Thời điểm
                worksheet.Columns[4].ColumnWidth = 15;  // Hình thức TT
                worksheet.Columns[5].ColumnWidth = 15;  // Tổng tiền
                worksheet.Columns[6].ColumnWidth = 20;  // Nhân viên
                worksheet.Columns[7].ColumnWidth = 25;  // Tên KH
                worksheet.Columns[8].ColumnWidth = 15;  // SĐT

                // Lưu file
                workbook.SaveAs(filePath);
                workbook.Close();
                excelApp.Quit();

                // Giải phóng COM objects
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                MessageBox.Show("Xuất Excel thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Mở file
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region XEM CHI TIẾT

        private void dgvLichSu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                string maBan = dgvLichSu.Rows[e.RowIndex].Cells["Mã Bàn"].Value.ToString();
                DateTime ngayTT = Convert.ToDateTime(dgvLichSu.Rows[e.RowIndex].Cells["Ngày Thanh Toán"].Value);

                XemChiTietHoaDon(maBan, ngayTT);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xem chi tiết: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XemChiTietHoaDon(string maBan, DateTime ngayTT)
        {
            // Tạo form hiển thị chi tiết
            Form formChiTiet = new Form
            {
                Text = $"CHI TIẾT HÓA ĐƠN - BÀN {maBan} - {ngayTT:dd/MM/yyyy}",
                Size = new System.Drawing.Size(700, 500),
                StartPosition = FormStartPosition.CenterScreen
            };

            DataGridView dgvChiTiet = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = System.Drawing.Color.White,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            formChiTiet.Controls.Add(dgvChiTiet);

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    // Query lấy chi tiết món đã order (từ lịch sử)
                    // Lưu ý: Vì đã thanh toán nên ChiTietHD có thể đã bị xóa
                    // Cần có bảng lưu chi tiết hoặc query từ dữ liệu còn lại
                    string query = @"
                        SELECT 
                            k.TenMon AS 'Tên Món',
                            ct.SoLuong AS 'Số Lượng',
                            cng.GiaMoi AS 'Đơn Giá',
                            (ct.SoLuong * cng.GiaMoi) AS 'Thành Tiền'
                        FROM ChiTietHD ct
                        INNER JOIN Kho k ON ct.MaMon = k.MaMon
                        INNER JOIN (
                            SELECT MaMon, GiaMoi,
                                   ROW_NUMBER() OVER (PARTITION BY MaMon ORDER BY NgayCapNhat DESC) as rn
                            FROM CapNhatGia
                        ) cng ON ct.MaMon = cng.MaMon AND cng.rn = 1
                        WHERE ct.MaBan = @MaBan";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaBan", maBan);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvChiTiet.DataSource = dt;

                    if (dgvChiTiet.Columns.Count > 0)
                    {
                        dgvChiTiet.Columns["Tên Món"].Width = 250;
                        dgvChiTiet.Columns["Số Lượng"].Width = 100;
                        dgvChiTiet.Columns["Đơn Giá"].Width = 120;
                        dgvChiTiet.Columns["Thành Tiền"].Width = 120;

                        dgvChiTiet.Columns["Đơn Giá"].DefaultCellStyle.Format = "N0";
                        dgvChiTiet.Columns["Thành Tiền"].DefaultCellStyle.Format = "N0";
                        dgvChiTiet.Columns["Đơn Giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvChiTiet.Columns["Thành Tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy chi tiết hóa đơn.\n(Dữ liệu có thể đã bị xóa sau thanh toán)",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load chi tiết: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            formChiTiet.ShowDialog();
        }

        #endregion
    }
}