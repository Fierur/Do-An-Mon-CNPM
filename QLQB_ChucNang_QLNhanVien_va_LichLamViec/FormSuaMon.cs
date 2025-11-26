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
    public partial class FormSuaMon : Form
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
}