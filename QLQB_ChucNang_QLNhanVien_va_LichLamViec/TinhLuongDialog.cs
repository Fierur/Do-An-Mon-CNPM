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
    public partial class frmTinhLuongDialog : Form
    {
        private string maNV;
        private string tenNV;

        private Label lblTitle;
        private Label lblThang, lblNam, lblThuongPhat;
        private NumericUpDown nudThang, nudNam, nudThuongPhat;
        private Button btnTinhLuong, btnHuy;
        private Panel pnlThongTin;
        private Label lblSoNgayLam, lblLuongGio;
        private TextBox txtSoNgayLam, txtLuongGio;

        public frmTinhLuongDialog(string maNV, string tenNV)
        {
            this.maNV = maNV;
            this.tenNV = tenNV;
            InitializeComponent();
            lblTitle.Text = $"TÍNH LƯƠNG - {maNV} - {tenNV}";
            // Gán các sự kiện (Events)
            nudThang.ValueChanged += (s, e) => LoadThongTinThang();
            nudNam.ValueChanged += (s, e) => LoadThongTinThang();

            // Nút Hủy
            btnHuy.Click += (s, e) => {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            // Gọi load lần đầu
            LoadThongTinThang();
        }
       
        private void frmTinhLuongDialog_Load(object sender, EventArgs e)
        {
            LoadThongTinThang();
        }

        private void LoadThongTinThang()
        {
            try
            {
                int thang = (int)nudThang.Value;
                int nam = (int)nudNam.Value;

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    // Số ngày làm
                    string querySoNgay = @"SELECT dbo.fn_TongNgayLamMotThang(@MaNV, @Thang, @Nam) AS SoNgay";
                    SqlCommand cmdSoNgay = new SqlCommand(querySoNgay, conn);
                    cmdSoNgay.Parameters.AddWithValue("@MaNV", maNV);
                    cmdSoNgay.Parameters.AddWithValue("@Thang", thang);
                    cmdSoNgay.Parameters.AddWithValue("@Nam", nam);

                    int soNgay = Convert.ToInt32(cmdSoNgay.ExecuteScalar());
                    txtSoNgayLam.Text = soNgay.ToString() + " ngày";

                    // Lương/giờ
                    string queryLuong = "SELECT LuongMoiGio FROM NhanVien WHERE MaNV = @MaNV";
                    SqlCommand cmdLuong = new SqlCommand(queryLuong, conn);
                    cmdLuong.Parameters.AddWithValue("@MaNV", maNV);

                    decimal luongGio = Convert.ToDecimal(cmdLuong.ExecuteScalar());
                    txtLuongGio.Text = luongGio.ToString("N0") + " VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTinhLuong_Click(object sender, EventArgs e)
        {
            try
            {
                int thang = (int)nudThang.Value;
                int nam = (int)nudNam.Value;
                decimal thuongPhat = nudThuongPhat.Value;

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_TinhLuongThang", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaNV", maNV);
                    cmd.Parameters.AddWithValue("@Thang", thang);
                    cmd.Parameters.AddWithValue("@Nam", nam);
                    cmd.Parameters.AddWithValue("@ThuongPhat", thuongPhat);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string thongBao = $"✓ TÍNH LƯƠNG THÀNH CÔNG!\n\n" +
                            $"━━━━━━━━━━━━━━━━━━━━━━\n" +
                            $"Nhân viên: {reader["TenNV"]}\n" +
                            $"Tháng: {thang}/{nam}\n" +
                            $"━━━━━━━━━━━━━━━━━━━━━━\n" +
                            $"Số ngày làm: {reader["SoNgayLam"]} ngày\n" +
                            $"Giờ/ngày: {reader["SoGioMotNgay"]} giờ (cố định)\n" +
                            $"Lương/giờ: {Convert.ToDecimal(reader["LuongMoiGio"]):N0} VNĐ\n" +
                            $"Thưởng/Phạt: {Convert.ToDecimal(reader["ThuongPhat"]):N0} VNĐ\n" +
                            $"━━━━━━━━━━━━━━━━━━━━━━\n" +
                            $"TỔNG LƯƠNG: {Convert.ToDecimal(reader["TongLuong"]):N0} VNĐ\n" +
                            $"━━━━━━━━━━━━━━━━━━━━━━";

                        MessageBox.Show(thongBao, "Kết quả tính lương",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
