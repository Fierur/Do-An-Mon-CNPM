using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
            InitializeForm();
            LoadData();
        }

        private void InitializeForm()
        {
            this.Text = "Chi Tiết Phiếu Nhập";
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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết: " + ex.Message);
            }
        }
    }
}