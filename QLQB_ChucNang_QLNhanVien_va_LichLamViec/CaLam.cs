using QLQB_ChucNang_QLNhanVien_va_LichLamViec.Database;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    public partial class frmXemCaLam : Form
    {
        private DataGridView dgvCaLam;
        private DataGridView dgvNhanVienCa;
        private Panel pnlFilter;
        private DateTimePicker dtpLocNgay;
        private NumericUpDown nudLocThang;
        private NumericUpDown nudLocNam;
        private Button btnLocNgay;
        private Button btnLocThang;
        private Button btnHuyLoc;

        public frmXemCaLam()
        {
            InitializeComponent();
        }



        private void frmXemCaLam_Load(object sender, EventArgs e)
        {
            LoadCaLamViec();
        }

        private void LoadCaLamViec()
        {
            try
            {
                string query = @"SELECT MaCa, 
                                CONVERT(VARCHAR(5), GioBD, 108) AS GioBatDau,
                                CONVERT(VARCHAR(5), GioKT, 108) AS GioKetThuc
                                FROM LichLamViec
                                ORDER BY GioBD";

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvCaLam.DataSource = dt;

                    UpdateThongKe(dt.Rows.Count, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải ca làm việc: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCaLam_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCaLam.SelectedRows.Count > 0)
            {
                string maCa = dgvCaLam.SelectedRows[0].Cells["MaCa"].Value.ToString();
                LoadNhanVienTheoCa(maCa);
            }
        }

        private void LoadNhanVienTheoCa(string maCa, DateTime? ngay = null, int? thang = null, int? nam = null)
        {
            try
            {
                string query = @"SELECT DISTINCT nv.MaNV, nv.TenNV, nv.MaCa, nv.ChucVu
                                FROM NhanVien nv
                                WHERE nv.MaCa = @MaCa";

                // Nếu có bộ lọc ngày
                if (ngay.HasValue)
                {
                    query = @"SELECT DISTINCT nv.MaNV, nv.TenNV, nv.MaCa, nv.ChucVu, cc.NgayDiLam
                            FROM NhanVien nv
                            INNER JOIN ChamCong cc ON nv.MaNV = cc.MaNV
                            WHERE nv.MaCa = @MaCa AND cc.NgayDiLam = @Ngay";
                }
                // Nếu có bộ lọc tháng/năm
                else if (thang.HasValue && nam.HasValue)
                {
                    query = @"SELECT DISTINCT nv.MaNV, nv.TenNV, nv.MaCa, nv.ChucVu, 
                            COUNT(cc.NgayDiLam) AS SoNgayLam
                            FROM NhanVien nv
                            LEFT JOIN ChamCong cc ON nv.MaNV = cc.MaNV 
                                AND MONTH(cc.NgayDiLam) = @Thang 
                                AND YEAR(cc.NgayDiLam) = @Nam
                            WHERE nv.MaCa = @MaCa
                            GROUP BY nv.MaNV, nv.TenNV, nv.MaCa, nv.ChucVu";
                }

                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaCa", maCa);

                    if (ngay.HasValue)
                        cmd.Parameters.AddWithValue("@Ngay", ngay.Value.Date);

                    if (thang.HasValue && nam.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@Thang", thang.Value);
                        cmd.Parameters.AddWithValue("@Nam", nam.Value);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvNhanVienCa.DataSource = dt;

                    UpdateThongKe(dgvCaLam.Rows.Count, dt.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhân viên: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLocNgay_Click(object sender, EventArgs e)
        {
            if (dgvCaLam.SelectedRows.Count > 0)
            {
                string maCa = dgvCaLam.SelectedRows[0].Cells["MaCa"].Value.ToString();
                LoadNhanVienTheoCa(maCa, dtpLocNgay.Value.Date);
            }
        }

        private void btnLocThang_Click(object sender, EventArgs e)
        {
            if (dgvCaLam.SelectedRows.Count > 0)
            {
                string maCa = dgvCaLam.SelectedRows[0].Cells["MaCa"].Value.ToString();
                LoadNhanVienTheoCa(maCa, null, (int)nudLocThang.Value, (int)nudLocNam.Value);
            }
        }

        private void btnHuyLoc_Click(object sender, EventArgs e)
        {
            if (dgvCaLam.SelectedRows.Count > 0)
            {
                string maCa = dgvCaLam.SelectedRows[0].Cells["MaCa"].Value.ToString();
                LoadNhanVienTheoCa(maCa);
            }
        }

        private void UpdateThongKe(int tongCa, int tongNV)
        {
            var lblTongCa = this.Controls.Find("lblTongCa", true).FirstOrDefault() as Label;
            var lblTongNV = this.Controls.Find("lblTongNV", true).FirstOrDefault() as Label;

            if (lblTongCa != null)
                lblTongCa.Text = $"Tổng số ca: {tongCa}";

            if (lblTongNV != null)
                lblTongNV.Text = $"Tổng số nhân viên: {tongNV}";
        }
    }
}