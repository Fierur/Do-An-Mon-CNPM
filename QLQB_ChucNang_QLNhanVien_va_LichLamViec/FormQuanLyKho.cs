using Microsoft.VisualBasic;
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
    public partial class FormQuanLyKho : Form
    {
        private string username;
        private string password;
        private bool canEdit;

        public FormQuanLyKho(string username, string password, bool canEdit)
        {
            InitializeComponent(); // gọi file Designer
            this.username = username;
            this.password = password;
            this.canEdit = canEdit;
        }

        private string GetConnectionString()
        {
            return $@"Data Source=26.85.209.147;Initial Catalog=QuanLyQuanBar;User ID={username};Password={password}";
        }

        private void FormQuanLyKho_Load(object sender, EventArgs e)
        {
            LoadData();
            btnAdd.Enabled = btnEdit.Enabled = btnDelete.Enabled = canEdit;
        }

        private void LoadData()
        {
            try
            {
                dgvKho.Rows.Clear();
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    string query = "SELECT MaMon, TenMon, SoLuongTon, Gia FROM Kho";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dgvKho.Rows.Add(
                            reader["MaMon"].ToString(),
                            reader["TenMon"].ToString(),
                            Convert.ToInt32(reader["SoLuongTon"]),
                            Convert.ToDecimal(reader["Gia"])
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var themMon = new FormThemMon(GetConnectionString());
            if (themMon.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvKho.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn món để sửa!");
                return;
            }

            string maMon = dgvKho.SelectedRows[0].Cells[0].Value.ToString();
            string tenMon = dgvKho.SelectedRows[0].Cells[1].Value.ToString();
            int soLuong = Convert.ToInt32(dgvKho.SelectedRows[0].Cells[2].Value);
            decimal gia = Convert.ToDecimal(dgvKho.SelectedRows[0].Cells[3].Value);

            var suaMon = new FormSuaMon(maMon, tenMon, soLuong, gia, GetConnectionString());
            if (suaMon.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvKho.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn món để xóa!");
                return;
            }

            string maMon = dgvKho.SelectedRows[0].Cells[0].Value.ToString();

            if (MessageBox.Show($"Bạn có chắc muốn xóa món {maMon}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Kho WHERE MaMon=@MaMon", conn);
                        cmd.Parameters.AddWithValue("@MaMon", maMon);
                        cmd.ExecuteNonQuery();
                    }
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa món: " + ex.Message);
                }
            }
        }
    }
}