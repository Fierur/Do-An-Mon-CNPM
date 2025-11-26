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
    public partial class FormChiTietPhieuNhap : Form
    {
        private string maPhieuNhap;
        private string connectionString;
        private DataGridView dgvChiTiet;

        public FormChiTietPhieuNhap(string maPhieuNhap, string connString)
        {
            this.maPhieuNhap = maPhieuNhap;
            this.connectionString = connString;
            if (string.IsNullOrEmpty(SessionInfo.MaNV))
            {
                MessageBox.Show("Chưa đăng nhập! Vui lòng đăng nhập lại.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            InitializeForm();
            LoadData();
        }

        private void InitializeForm()
        {
            this.Text = $"Chi Tiết Phiếu Nhập - User: {SessionInfo.MaNV} ({SessionInfo.TenQuyen})";
            this.Size = new Size(950, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            dgvChiTiet = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(900, 500),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvChiTiet.Columns.Add("TenMonNhap", "Tên Món Nhập");
            dgvChiTiet.Columns.Add("SoLuongNhap", "Số Lượng");
            dgvChiTiet.Columns.Add("DonGia", "Đơn Giá");
            dgvChiTiet.Columns.Add("ThanhTien", "Thành Tiền");

            Button btnDong = new Button { Text = "Đóng", Location = new Point(770, 540), Size = new Size(140, 40) };
            btnDong.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { dgvChiTiet, btnDong });
        }

        private void LoadData()
        {
            try
            {
                // Kiểm tra session trước khi load dữ liệu
                if (string.IsNullOrEmpty(SessionInfo.MaNV))
                {
                    MessageBox.Show("Phiên đăng nhập đã hết hạn!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }
                dgvChiTiet.Rows.Clear();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT TenMonNhap, SoLuongNhap, DonGia FROM ChiTietPN WHERE MaPhieuNhap=@MaPN";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaPN", maPhieuNhap);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        decimal tong = 0;
                        while (reader.Read())
                        {
                            int sl = Convert.ToInt32(reader["SoLuongNhap"]);
                            decimal dg = Convert.ToDecimal(reader["DonGia"]);
                            decimal tt = sl * dg;
                            tong += tt;
                            dgvChiTiet.Rows.Add(reader["TenMonNhap"], sl, dg, tt);
                        }
                        reader.Close();

                        int rowIndex = dgvChiTiet.Rows.Add("", "", "TỔNG CỘNG:", tong);
                        dgvChiTiet.Rows[rowIndex].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Xử lý lỗi session bị kill
                if (sqlEx.Message.Contains("session is in the kill state") ||
                    sqlEx.Message.Contains("Login failed"))
                {
                    MessageBox.Show(
                        "Phiên đăng nhập của bạn đã hết hạn.\n" +
                        "Vui lòng đăng nhập lại.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Quay về form login
                    ForceLogout();
                }
                else
                {
                    MessageBox.Show("Lỗi tải chi tiết: " + sqlEx.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ForceLogout()
        {
            try
            {
                SessionInfo.Clear();
                DatabaseConnection.ClearConnection();

                this.Hide();

                frmLogin loginForm = new frmLogin();
                loginForm.FormClosed += (s, args) =>
                {
                    this.Close();
                    this.Dispose();
                };
                loginForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng xuất: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}