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
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Windows.Forms;
using System.IO;


namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    public partial class frmTongHopLuong : Form
    {
        
        public frmTongHopLuong()
        {
            InitializeComponent();

            // Event handlers
            this.Load += frmTongHopLuong_Load;
            btnLoc.Click += btnLoc_Click;
            btnDong.Click += btnDong_Click;
            btnXuatExcel.Click += btnXuatExcel_Click;
        }

        private void frmTongHopLuong_Load(object sender, EventArgs e)
        {
            // Set giá trị mặc định
            nudThang.Value = DateTime.Now.Month;
            nudNam.Value = DateTime.Now.Year;

            // Load dữ liệu
            LoadTongHopLuong((int)nudThang.Value, (int)nudNam.Value);
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadTongHopLuong((int)nudThang.Value, (int)nudNam.Value);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadTongHopLuong(int thang, int nam)
        {
            try
            {
                string query = @"SELECT 
                                    nv.MaNV,
                                    nv.TenNV,
                                    nv.ChucVu,
                                    ISNULL(bl.TongNgayLamMotThang, 0) AS [Số Ngày],
                                    nv.LuongMoiGio AS [Lương/Giờ],
                                    ISNULL(bl.ThuongPhat, 0) AS [Thưởng/Phạt],
                                    ISNULL(bl.TongLuong, 0) AS [Tổng Lương],
                                    bl.NgayTinhLuong AS [Ngày Tính]
                                FROM NhanVien nv
                                LEFT JOIN BangLuong bl ON nv.MaNV = bl.MaNV 
                                    AND bl.Thang = @Thang 
                                    AND bl.Nam = @Nam
                                WHERE nv.TrangThai = N'Đang làm' or nv.TrangThai = N'Tạm nghỉ'
                                ORDER BY bl.TongLuong DESC, nv.MaNV";

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Thang", thang);
                    cmd.Parameters.AddWithValue("@Nam", nam);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    adapter.Fill(dt);

                    dgvTongHop.DataSource = dt;

                    // Format columns
                    FormatColumns();

                    // Tính thống kê
                    CalculateStatistics(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatColumns()
        {
            if (dgvTongHop.Columns["Lương/Giờ"] != null)
            {
                dgvTongHop.Columns["Lương/Giờ"].DefaultCellStyle.Format = "N0";
                dgvTongHop.Columns["Lương/Giờ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvTongHop.Columns["Thưởng/Phạt"] != null)
            {
                dgvTongHop.Columns["Thưởng/Phạt"].DefaultCellStyle.Format = "N0";
                dgvTongHop.Columns["Thưởng/Phạt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvTongHop.Columns["Tổng Lương"] != null)
            {
                dgvTongHop.Columns["Tổng Lương"].DefaultCellStyle.Format = "N0";
                dgvTongHop.Columns["Tổng Lương"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTongHop.Columns["Tổng Lương"].DefaultCellStyle.Font = new System.Drawing.Font(dgvTongHop.Font, FontStyle.Bold);
            }
            if (dgvTongHop.Columns["Ngày Tính"] != null)
            {
                dgvTongHop.Columns["Ngày Tính"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            // Color rows
            foreach (DataGridViewRow row in dgvTongHop.Rows)
            {
                if (row.Cells["Tổng Lương"].Value != DBNull.Value)
                {
                    decimal tongLuong = Convert.ToDecimal(row.Cells["Tổng Lương"].Value);
                    if (tongLuong > 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(230, 247, 255);
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 245, 230);
                    }
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
                }
            }
        }

        private void CalculateStatistics(System.Data.DataTable dt)
        {
            int tongNV = 0;
            decimal tongLuong = 0;
            decimal luongTB = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["Tổng Lương"] != DBNull.Value)
                {
                    decimal tl = Convert.ToDecimal(row["Tổng Lương"]);
                    if (tl > 0)
                    {
                        tongNV++;
                        tongLuong += tl;
                    }
                }
            }

            if (tongNV > 0)
                luongTB = tongLuong / tongNV;

            lblTongNV.Text = $"👥 Tổng NV đã tính lương: {tongNV}";
            lblTongLuong.Text = $"💰 Tổng lương: {tongLuong:N0} VNĐ";
            lblLuongTB.Text = $"📊 Lương TB: {luongTB:N0} VNĐ";
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    string fileName = $"L{(int)nudThang.Value}{(int)nudNam.Value}";
                    saveFileDialog.FileName = fileName;
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.DefaultExt = "xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        XuatExcelInterop(saveFileDialog.FileName);
                        MessageBox.Show($"Xuất file Excel thành công!\nFile: {Path.GetFileName(saveFileDialog.FileName)}",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file Excel: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XuatExcelInterop(string filePath)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Workbook workbook = null;
            Worksheet worksheet = null;

            try
            {
                // Khởi tạo ứng dụng Excel
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                // Tạo workbook mới
                workbook = excelApp.Workbooks.Add(Missing.Value);
                worksheet = (Worksheet)workbook.ActiveSheet;
                worksheet.Name = "Tổng hợp lương";

                // Tiêu đề chính
                worksheet.Cells[1, 1] = "TỔNG HỢP LƯƠNG NHÂN VIÊN";
                Range titleRange = worksheet.Range["A1", "H1"];
                titleRange.Merge();
                titleRange.Font.Bold = true;
                titleRange.Font.Size = 16;
                titleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                titleRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                titleRange.Interior.Color = ColorTranslator.ToOle(Color.LightBlue);

                // Thông tin tháng/năm
                worksheet.Cells[2, 1] = $"Tháng: {(int)nudThang.Value} - Năm: {(int)nudNam.Value}";
                Range monthRange = worksheet.Range["A2", "H2"];
                monthRange.Merge();
                monthRange.Font.Bold = true;
                monthRange.Font.Size = 12;
                monthRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                // Ngày xuất
                worksheet.Cells[3, 1] = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}";
                Range dateRange = worksheet.Range["A3", "H3"];
                dateRange.Merge();
                dateRange.Font.Italic = true;
                dateRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                // Header row
                int headerRow = 5;
                string[] headers = { "Mã NV", "Tên NV", "Chức vụ", "Số Ngày", "Lương/Giờ", "Thưởng/Phạt", "Tổng Lương", "Ngày Tính" };

                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[headerRow, i + 1] = headers[i];
                    Range headerCell = worksheet.Cells[headerRow, i + 1];
                    headerCell.Font.Bold = true;
                    headerCell.Interior.Color = ColorTranslator.ToOle(Color.LightBlue);
                    headerCell.Borders.LineStyle = XlLineStyle.xlContinuous;
                    headerCell.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                }

                // Dữ liệu
                int row = headerRow + 1;
                decimal tongLuong = 0;
                int tongNV = 0;

                foreach (DataGridViewRow dgvRow in dgvTongHop.Rows)
                {
                    if (dgvRow.IsNewRow) continue;

                    for (int col = 0; col < dgvRow.Cells.Count; col++)
                    {
                        object cellValue = dgvRow.Cells[col].Value;
                        worksheet.Cells[row, col + 1] = cellValue;

                        Range cell = worksheet.Cells[row, col + 1];
                        cell.Borders.LineStyle = XlLineStyle.xlContinuous;

                        // Căn phải cho các cột số và định dạng số
                        if (col >= 3 && col <= 6) // Các cột số: Số Ngày, Lương/Giờ, Thưởng/Phạt, Tổng Lương
                        {
                            cell.HorizontalAlignment = XlHAlign.xlHAlignRight;

                            if (cellValue != null && decimal.TryParse(cellValue.ToString(), out decimal numValue))
                            {
                                cell.NumberFormat = "#,##0";

                                // Tính tổng lương
                                if (col == 6) // Cột Tổng Lương
                                {
                                    tongLuong += numValue;
                                    if (numValue > 0) tongNV++;
                                }
                            }
                        }
                        else
                        {
                            cell.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                        }

                        // Định dạng riêng cho cột Tổng Lương
                        if (col == 6 && cellValue != null && decimal.TryParse(cellValue.ToString(), out decimal luong))
                        {
                            if (luong > 0)
                            {
                                cell.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(230, 247, 255));
                            }
                            else
                            {
                                cell.Interior.Color = ColorTranslator.ToOle(Color.FromArgb(255, 245, 230));
                            }
                            cell.Font.Bold = true;
                        }
                    }
                    row++;
                }

                // Dòng tổng kết
                int totalRow = row + 1;
                worksheet.Cells[totalRow, 1] = "TỔNG KẾT";
                Range totalTitleRange = worksheet.Range[worksheet.Cells[totalRow, 1], worksheet.Cells[totalRow, 4]];
                totalTitleRange.Merge();
                totalTitleRange.Font.Bold = true;
                totalTitleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                totalTitleRange.Interior.Color = ColorTranslator.ToOle(Color.LightGreen);

                worksheet.Cells[totalRow, 5] = "Tổng NV:";
                worksheet.Cells[totalRow, 5].Font.Bold = true;
                worksheet.Cells[totalRow, 5].HorizontalAlignment = XlHAlign.xlHAlignRight;

                worksheet.Cells[totalRow, 6] = tongNV;
                worksheet.Cells[totalRow, 6].NumberFormat = "#,##0";

                worksheet.Cells[totalRow + 1, 5] = "Tổng lương:";
                worksheet.Cells[totalRow + 1, 5].Font.Bold = true;
                worksheet.Cells[totalRow + 1, 5].HorizontalAlignment = XlHAlign.xlHAlignRight;

                worksheet.Cells[totalRow + 1, 6] = tongLuong;
                worksheet.Cells[totalRow + 1, 6].NumberFormat = "#,##0";
                worksheet.Cells[totalRow + 1, 6].Font.Bold = true;
                worksheet.Cells[totalRow + 1, 6].Font.Color = ColorTranslator.ToOle(Color.Green);

                // Điều chỉnh độ rộng cột tự động
                worksheet.Columns.AutoFit();

                // Lưu file
                workbook.SaveAs(filePath, XlFileFormat.xlOpenXMLWorkbook);
            }
            finally
            {
                // Giải phóng tài nguyên
                if (workbook != null)
                {
                    workbook.Close(false, Missing.Value, Missing.Value);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                }

                // Dọn dẹp các đối tượng COM
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
