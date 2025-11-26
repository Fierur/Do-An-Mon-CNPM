using QLQB_ChucNang_QLNhanVien_va_LichLamViec.Database;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    public partial class frmThongTinCaNhan : Form
    {
        public frmThongTinCaNhan()
        {
            InitializeComponent();
        }

        private void frmThongTinCaNhan_Load(object sender, EventArgs e)
        {
            LoadThongTinCaNhan();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadThongTinCaNhan()
        {
            try
            {
                // Sử dụng VIEW để load thông tin
                string query = "SELECT * FROM vw_ThongTinCaNhan";

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtMaNV.Text = reader["MaNV"].ToString();
                        txtTenNV.Text = reader["TenNV"].ToString();
                        txtChucVu.Text = reader["ChucVu"].ToString();
                        txtNgaySinh.Text = Convert.ToDateTime(reader["NgaySinh"]).ToString("dd/MM/yyyy");
                        txtGioiTinh.Text = reader["GioiTinh"].ToString();

                        // Format lương
                        decimal luong = Convert.ToDecimal(reader["LuongMoiGio"]);
                        txtLuongMoiGio.Text = luong.ToString("N0") + " VNĐ";

                        txtMaCa.Text = reader["MaCa"].ToString();

                        // Format giờ
                        if (reader["GioBD"] != DBNull.Value)
                        {
                            TimeSpan gioBD = (TimeSpan)reader["GioBD"];
                            txtGioBD.Text = gioBD.ToString(@"hh\:mm");
                        }
                        else
                        {
                            txtGioBD.Text = "";
                        }

                        if (reader["GioKT"] != DBNull.Value)
                        {
                            TimeSpan gioKT = (TimeSpan)reader["GioKT"];
                            txtGioKT.Text = gioKT.ToString(@"hh\:mm");
                        }
                        else
                        {
                            txtGioKT.Text = "";
                        }

                        txtQuyen.Text = reader["TenQuyen"].ToString();

                        // Cập nhật title với tên
                        lblTitle.Text = $"THÔNG TIN CÁ NHÂN - {reader["TenNV"]}";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin nhân viên!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}