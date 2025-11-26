using System;
using System.Data;
using System.Data.SqlClient;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec.Database
{
    public class DatabaseConnection
    {                                       //26.71.28.188\\MSSQL16SERVER
        private static string serverName = "26.71.28.188";
        private static string databaseName = "QuanLyQuanBar";
        private static string connectionString;
        private static SqlConnection currentConnection = null;

        /// <summary>
        /// Dùng thông tin user + password từ form để đăng nhập
        /// </summary>
        public static bool TestLogin(string username, string password, out string errorMessage, out string tenNV, out string maQuyen, out string tenQuyen, out bool isQuanLy)
        {
            errorMessage = "";
            tenNV = "";
            maQuyen = "";
            tenQuyen = "";
            isQuanLy = false;

            try
            {
                // Tạo chuỗi kết nối động từ tài khoản người dùng nhập
                string loginConnectionString =
                    $"Data Source={serverName};Initial Catalog={databaseName};User ID={username};Password={password};";

                using (SqlConnection conn = new SqlConnection(loginConnectionString))
                {
                    conn.Open();

                    // Kiểm tra xem user có tồn tại trong bảng NhanVien không
                    string query = "SELECT TenNV, n.MaQuyen, TenQuyen FROM NhanVien n join Quyen q ON q.MaQuyen = n.MaQuyen WHERE MaNV =  @MaNV";
                    string query1 = "select* from vw_ThongTinCaNhan v \r\njoin Quyen q on v.MaQuyen = q.MaQuyen";
                    using (SqlCommand cmd = new SqlCommand(query1, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNV", username);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                tenNV = reader["TenNV"].ToString();
                                maQuyen = reader["MaQuyen"].ToString();
                                tenQuyen = reader["TenQuyen"].ToString();
                            }
                            else
                            {
                                errorMessage = "Tài khoản không tồn tại trong hệ thống!";
                                return false;
                            }
                        }
                    }

                    // Kiểm tra user có thuộc Role_QuanLy không
                    string roleQuery = "SELECT IS_MEMBER('Role_QuanLy') AS IsQuanLy";
                    using (SqlCommand cmdRole = new SqlCommand(roleQuery, conn))
                    {
                        object result = cmdRole.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            isQuanLy = Convert.ToInt32(result) == 1;
                        }
                    }

                    // Lưu chuỗi kết nối hiện tại cho session
                    connectionString = loginConnectionString;
                    return true;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 18456)
                    errorMessage = "Tên đăng nhập hoặc mật khẩu không đúng!";
                else
                {
                    errorMessage = "Lỗi kết nối: " + ex.Message;
                }
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi: " + ex.Message;
                return false;
            }
        }

        public static SqlConnection GetConnection()
        {
            if (currentConnection == null || currentConnection.State == ConnectionState.Closed)
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Chưa đăng nhập! Vui lòng đăng nhập lại.");
                }
                currentConnection = new SqlConnection(connectionString);
            }
            return currentConnection;
        }

        public static SqlConnection OpenConnection()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Chưa đăng nhập! Vui lòng đăng nhập lại.");
            }
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        public static void CloseConnection()
        {
            if (currentConnection != null && currentConnection.State == ConnectionState.Open)
                currentConnection.Close();
        }

        public static void ClearConnection()
        {
            CloseConnection();
            connectionString = null;
            currentConnection = null;
        }
    }
}