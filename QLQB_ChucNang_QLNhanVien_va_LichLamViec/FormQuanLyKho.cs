using Microsoft.VisualBasic;
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
    public partial class FormQuanLyKho : Form
    {
        //private string username;
        //private string password;
        private bool canEdit;

        public FormQuanLyKho()/*(string username, string password, bool canEdit)*/
        {
            InitializeComponent(); // gọi file Designer
                                   //this.username = username;
                                   //this.password = password;
                                   //this.canEdit = SessionInfo.IsAdmin;
            //KIỂM TRA ĐĂNG NHẬP NGAY KHI KHỞI TẠO
            if (string.IsNullOrEmpty(SessionInfo.MaNV))
            {
                MessageBox.Show("Chưa đăng nhập! Vui lòng đăng nhập lại.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private string GetConnectionString()
        {
            return DatabaseConnection.GetConnection().ConnectionString;
        }

        private void FormQuanLyKho_Load(object sender, EventArgs e)
        {
            //LoadData();
            //btnAdd.Enabled = btnEdit.Enabled = btnDelete.Enabled = canEdit;
            try
            {
                //Kiểm tra session trước khi load
                if (string.IsNullOrEmpty(SessionInfo.MaNV))
                {
                    MessageBox.Show("Phiên đăng nhập đã hết hạn!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                LoadData();

                //SỬ DỤNG SessionInfo.IsAdmin thay vì biến canEdit
                btnAdd.Enabled = SessionInfo.IsAdmin;
                btnEdit.Enabled = SessionInfo.IsAdmin;
                btnDelete.Enabled = SessionInfo.IsAdmin;

                //Hiển thị thông tin user đang đăng nhập
                this.Text = $"QUẢN LÝ KHO - User: {SessionInfo.MaNV} ({SessionInfo.TenQuyen})";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo form: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            //try
            //{
            //    dgvKho.Rows.Clear();
            //    using (SqlConnection conn = DatabaseConnection.OpenConnection())
            //    {
            //        string query = "SELECT MaMon, TenMon, SoLuongTon, Gia FROM Kho";
            //        SqlCommand cmd = new SqlCommand(query, conn);
            //        SqlDataReader reader = cmd.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            dgvKho.Rows.Add(
            //                reader["MaMon"].ToString(),
            //                reader["TenMon"].ToString(),
            //                Convert.ToInt32(reader["SoLuongTon"]),
            //                Convert.ToDecimal(reader["Gia"])
            //            );
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            //}
            try
            {
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    string query = "SELECT MaMon, TenMon, SoLuongTon, Gia FROM Kho";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvKho.AutoGenerateColumns = true;
                    dgvKho.DataSource = dt;
                }
            }
            catch (SqlException sqlEx)
            {
                //Xử lý lỗi session bị kill
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
                    MessageBox.Show("Lỗi tải dữ liệu: " + sqlEx.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //var themMon = new FormThemMon(DatabaseConnection.GetConnection().ConnectionString);
            //if (themMon.ShowDialog() == DialogResult.OK)
            //    LoadData();
            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Chỉ quản lý mới có quyền thêm món!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var themMon = new FormThemMon(DatabaseConnection.GetConnection().ConnectionString);
                if (themMon.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //if (dgvKho.SelectedRows.Count == 0)
            //{
            //    MessageBox.Show("Chọn món để sửa!");
            //    return;
            //}

            //string maMon = dgvKho.SelectedRows[0].Cells[0].Value.ToString();
            //string tenMon = dgvKho.SelectedRows[0].Cells[1].Value.ToString();
            //int soLuong = Convert.ToInt32(dgvKho.SelectedRows[0].Cells[2].Value);
            //decimal gia = Convert.ToDecimal(dgvKho.SelectedRows[0].Cells[3].Value);

            //var suaMon = new FormSuaMon(maMon, tenMon, soLuong, gia, DatabaseConnection.GetConnection().ConnectionString);
            //if (suaMon.ShowDialog() == DialogResult.OK)
            //    LoadData();
            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Chỉ quản lý mới có quyền sửa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvKho.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn món để sửa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string maMon = dgvKho.SelectedRows[0].Cells[0].Value.ToString();
                string tenMon = dgvKho.SelectedRows[0].Cells[1].Value.ToString();
                int soLuong = Convert.ToInt32(dgvKho.SelectedRows[0].Cells[2].Value);
                decimal gia = Convert.ToDecimal(dgvKho.SelectedRows[0].Cells[3].Value);

                var suaMon = new FormSuaMon(maMon, tenMon, soLuong, gia,
                    DatabaseConnection.GetConnection().ConnectionString);

                if (suaMon.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //if (dgvKho.SelectedRows.Count == 0)
            //{
            //    MessageBox.Show("Chọn món để xóa!");
            //    return;
            //}

            //string maMon = dgvKho.SelectedRows[0].Cells[0].Value.ToString();

            //if (MessageBox.Show($"Bạn có chắc muốn xóa món {maMon}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    try
            //    {
            //        using (SqlConnection conn = DatabaseConnection.OpenConnection())
            //        {
            //            conn.Open();
            //            SqlCommand cmd = new SqlCommand("DELETE FROM Kho WHERE MaMon=@MaMon", conn);
            //            cmd.Parameters.AddWithValue("@MaMon", maMon);
            //            cmd.ExecuteNonQuery();
            //        }
            //        LoadData();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("Lỗi xóa món: " + ex.Message);
            //    }
            //}

            if (!SessionInfo.IsAdmin)
            {
                MessageBox.Show("Chỉ quản lý mới có quyền xóa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvKho.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn món để xóa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maMon = dgvKho.SelectedRows[0].Cells[0].Value.ToString();

            if (MessageBox.Show($"Bạn có chắc muốn xóa món {maMon}?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseConnection.OpenConnection())
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM Kho WHERE MaMon=@MaMon", conn);
                        cmd.Parameters.AddWithValue("@MaMon", maMon);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Xóa thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa món: " + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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