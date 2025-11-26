using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    public partial class FormPhu : Form
    {
        private string connectionString;

        public FormPhu(string connString)
        {
            this.connectionString = connString;
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Text = "Quản lý các chức năng phụ";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            Button btnThemMon = new Button
            {
                Text = "Thêm Món",
                Location = new Point(50, 50),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnThemMon.FlatAppearance.BorderSize = 0;
            btnThemMon.Click += (s, e) =>
            {
                var themMon = new FormThemMon(connectionString);
                themMon.ShowDialog();
            };

            Button btnSuaMon = new Button
            {
                Text = "Sửa Món",
                Location = new Point(50, 110),
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(241, 196, 15),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSuaMon.FlatAppearance.BorderSize = 0;
            btnSuaMon.Click += (s, e) =>
            {
                string maMon = Microsoft.VisualBasic.Interaction.InputBox("Nhập mã món cần sửa:", "Sửa Món", "");
                if (string.IsNullOrWhiteSpace(maMon)) return;

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT TenMon, SoLuongTon, Gia FROM Kho WHERE MaMon=@MaMon";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaMon", maMon.Trim().ToUpper());
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                string tenMon = reader["TenMon"].ToString();
                                int soLuong = Convert.ToInt32(reader["SoLuongTon"]);
                                decimal gia = Convert.ToDecimal(reader["Gia"]);
                                reader.Close();

                                var suaMon = new FormSuaMon(maMon, tenMon, soLuong, gia, connectionString);
                                suaMon.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Mã món không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            Button btnChiTietPN = new Button
            {
                Text = "Chi Tiết Phiếu Nhập",
                Location = new Point(50, 170),
                Size = new Size(180, 40),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnChiTietPN.FlatAppearance.BorderSize = 0;
            btnChiTietPN.Click += (s, e) =>
            {
                string maPN = Microsoft.VisualBasic.Interaction.InputBox("Nhập mã phiếu nhập:", "Chi Tiết Phiếu Nhập", "");
                if (string.IsNullOrWhiteSpace(maPN)) return;

                var chiTietPN = new FormChiTietPhieuNhap(maPN.Trim().ToUpper(), connectionString);
                chiTietPN.ShowDialog();
            };

            this.Controls.AddRange(new Control[] { btnThemMon, btnSuaMon, btnChiTietPN });
        }

        // ========== FORM THÊM MÓN ==========
        public class FormThemMon : Form
        {
            private TextBox txtMaMon, txtTenMon, txtGia;
            private string connectionString;

            public FormThemMon(string connString)
            {
                this.connectionString = connString;
                InitializeForm();
            }

            private void InitializeForm()
            {
                this.Text = "Thêm Món";
                this.Size = new Size(500, 420);
                this.StartPosition = FormStartPosition.CenterParent;
                this.BackColor = Color.White;

                Label lblMa = new Label { Text = "Mã Món:", Location = new Point(50, 50), AutoSize = true };
                txtMaMon = new TextBox { Location = new Point(150, 47), Size = new Size(280, 25) };

                Label lblTen = new Label { Text = "Tên Món:", Location = new Point(50, 100), AutoSize = true };
                txtTenMon = new TextBox { Location = new Point(150, 97), Size = new Size(280, 25) };

                Label lblGia = new Label { Text = "Giá:", Location = new Point(50, 150), AutoSize = true };
                txtGia = new TextBox { Location = new Point(150, 147), Size = new Size(280, 25), Text = "0" };

                Button btnLuu = new Button { Text = "💾 Lưu", Location = new Point(150, 220), Size = new Size(120, 40) };
                btnLuu.Click += BtnLuu_Click;

                Button btnHuy = new Button { Text = "❌ Hủy", Location = new Point(280, 220), Size = new Size(120, 40) };
                btnHuy.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

                this.Controls.AddRange(new Control[] { lblMa, txtMaMon, lblTen, txtTenMon, lblGia, txtGia, btnLuu, btnHuy });
            }

            private void BtnLuu_Click(object sender, EventArgs e)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(txtMaMon.Text) ||
                        string.IsNullOrWhiteSpace(txtTenMon.Text) ||
                        string.IsNullOrWhiteSpace(txtGia.Text))
                    {
                        MessageBox.Show("Nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    decimal gia;
                    if (!decimal.TryParse(txtGia.Text, out gia) || gia < 0)
                    {
                        MessageBox.Show("Giá phải là số không âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO Kho(MaMon, TenMon, SoLuongTon, Gia) VALUES(@Ma,@Ten,0,@Gia)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Ma", txtMaMon.Text.Trim().ToUpper());
                            cmd.Parameters.AddWithValue("@Ten", txtTenMon.Text.Trim());
                            cmd.Parameters.AddWithValue("@Gia", gia);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Thêm món thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ========== FORM SỬA MÓN ==========
        public class FormSuaMon : Form
        {
            private TextBox txtMaMon, txtTenMon, txtSoLuong, txtGia;
            private string connectionString;
            private string maMon;

            public FormSuaMon(string maMon, string tenMon, int soLuong, decimal gia, string connString)
            {
                this.maMon = maMon;
                this.connectionString = connString;
                InitializeForm(maMon, tenMon, soLuong, gia);
            }

            private void InitializeForm(string maMon, string tenMon, int soLuong, decimal gia)
            {
                this.Text = "Sửa Món";
                this.Size = new Size(500, 400);
                this.StartPosition = FormStartPosition.CenterParent;
                this.BackColor = Color.White;

                Label lblMa = new Label { Text = "Mã Món:", Location = new Point(50, 50), AutoSize = true };
                txtMaMon = new TextBox { Location = new Point(150, 47), Size = new Size(280, 25), Text = maMon, ReadOnly = true };

                Label lblTen = new Label { Text = "Tên Món:", Location = new Point(50, 100), AutoSize = true };
                txtTenMon = new TextBox { Location = new Point(150, 97), Size = new Size(280, 25), Text = tenMon };

                Label lblSL = new Label { Text = "Số lượng:", Location = new Point(50, 150), AutoSize = true };
                txtSoLuong = new TextBox { Location = new Point(150, 147), Size = new Size(280, 25), Text = soLuong.ToString() };

                Label lblGia = new Label { Text = "Giá:", Location = new Point(50, 200), AutoSize = true };
                txtGia = new TextBox { Location = new Point(150, 197), Size = new Size(280, 25), Text = gia.ToString() };

                Button btnCapNhat = new Button { Text = "💾 Cập nhật", Location = new Point(150, 250), Size = new Size(120, 40) };
                btnCapNhat.Click += BtnCapNhat_Click;

                Button btnHuy = new Button { Text = "❌ Hủy", Location = new Point(280, 250), Size = new Size(120, 40) };
                btnHuy.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

                this.Controls.AddRange(new Control[] { lblMa, txtMaMon, lblTen, txtTenMon, lblSL, txtSoLuong, lblGia, txtGia, btnCapNhat, btnHuy });
            }

            private void BtnCapNhat_Click(object sender, EventArgs e)
            {
                try
                {
                    int sl;
                    decimal gia;
                    if (!int.TryParse(txtSoLuong.Text, out sl) || sl < 0 || !decimal.TryParse(txtGia.Text, out gia) || gia < 0)
                    {
                        MessageBox.Show("Số lượng và giá phải hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE Kho SET TenMon=@Ten, SoLuongTon=@SL, Gia=@Gia WHERE MaMon=@Ma";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Ma", maMon);
                            cmd.Parameters.AddWithValue("@Ten", txtTenMon.Text.Trim());
                            cmd.Parameters.AddWithValue("@SL", sl);
                            cmd.Parameters.AddWithValue("@Gia", gia);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ========== FORM CHI TIẾT PHIẾU NHẬP ==========
        public class FormChiTietPhieuNhap : Form
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
}