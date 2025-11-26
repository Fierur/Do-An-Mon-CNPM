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
    public partial class FormThemMon : Form
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
}
