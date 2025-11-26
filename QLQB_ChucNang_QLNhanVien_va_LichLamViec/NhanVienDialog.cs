using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QLQB_ChucNang_QLNhanVien_va_LichLamViec.Database;

namespace QLQB_ChucNang_QLNhanVien_va_LichLamViec
{
    public partial class frmNhanVienDialog : Form
    {
        public string MaNV { get; private set; }
        public string TenNV { get; private set; }
        public string ChucVu { get; private set; }
        public DateTime NgaySinh { get; private set; }
        public string GioiTinh { get; private set; }
        public string MatKhau { get; private set; }
        public decimal LuongMoiGio { get; private set; }
        public string MaQuyen { get; private set; }
        public string TrangThai { get; private set; }

        private bool isEditMode = false;
        private DataTable dtQuyen;
        private DataTable dtGioiTinh;

        public frmNhanVienDialog(bool isEdit = false, DataRow existingData = null)
        {
            InitializeComponent();
            isEditMode = isEdit;
            this.Text = isEdit ? "Sửa Thông Tin Nhân Viên" : "Thêm Nhân Viên Mới";

            if (isEdit && existingData != null)
            {
                LoadExistingData(existingData);
            }
            else
            {
                GenerateNewMaNV();
            }
        }

        

        private void frmNhanVienDialog_Load(object sender, EventArgs e)
        {
            try
            {
                LoadGioiTinh();
                LoadChucVu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateNewMaNV()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    string query = "SELECT TOP 1 MaNV FROM NhanVien ORDER BY MaNV DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string lastMaNV = result.ToString();
                        int number = int.Parse(lastMaNV.Substring(2));
                        txtMaNV.Text = "NV" + (number + 1).ToString("D2");
                    }
                    else
                    {
                        txtMaNV.Text = "NV01";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo mã NV: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaNV.Text = "NV01";
            }
        }

        private void LoadGioiTinh()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    string query = "SELECT DISTINCT GioiTinh FROM NhanVien WHERE GioiTinh IN (N'Nam', N'Nữ') ORDER BY GioiTinh;";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    dtGioiTinh = new DataTable();
                    adapter.Fill(dtGioiTinh);

                    cboGioiTinh.Items.Clear();
                    foreach (DataRow row in dtGioiTinh.Rows)
                    {
                        cboGioiTinh.Items.Add(row["GioiTinh"].ToString().Trim());
                    }

                    // Nếu không có dữ liệu, thêm mặc định
                    if (cboGioiTinh.Items.Count == 0)
                    {
                        cboGioiTinh.Items.Add("Nam");
                        cboGioiTinh.Items.Add("Nữ");
                    }

                    if (cboGioiTinh.Items.Count > 0)
                        cboGioiTinh.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                // Fallback nếu lỗi
                cboGioiTinh.Items.Clear();
                cboGioiTinh.Items.Add("Nam");
                cboGioiTinh.Items.Add("Nữ");
                cboGioiTinh.SelectedIndex = 0;
            }
        }

        private void LoadChucVu()
        {
            try
            {
                // Lấy danh sách chức vụ từ bảng NhanVien join Quyen
                using (SqlConnection conn = DatabaseConnection.OpenConnection())
                {
                    string query = @"SELECT DISTINCT n.MaQuyen, TenQuyen, ChucVu 
                                   FROM Quyen q 
                                   JOIN NhanVien n ON q.MaQuyen = n.MaQuyen 
                                   ORDER BY n.MaQuyen";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    dtQuyen = new DataTable();
                    adapter.Fill(dtQuyen);

                    cboChucVu.Items.Clear();

                    // Thêm các chức vụ từ database
                    foreach (DataRow row in dtQuyen.Rows)
                    {
                        cboChucVu.Items.Add(new
                        {
                            Text = row["ChucVu"].ToString(),
                            MaQuyen = row["MaQuyen"].ToString(),
                            TenQuyen = row["TenQuyen"].ToString()
                        });
                    }

                    cboChucVu.DisplayMember = "Text";

                    if (cboChucVu.Items.Count > 0)
                        cboChucVu.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load chức vụ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboChucVu.SelectedItem != null)
            {
                dynamic selectedItem = cboChucVu.SelectedItem;
                txtMaQuyen.Text = selectedItem.TenQuyen;
            }
            else
            {
                txtMaQuyen.Text = "";
            }
        }

        private void LoadExistingData(DataRow row)
        {
            try
            {
                txtMaNV.Text = row["MaNV"].ToString();
                txtTenNV.Text = row["TenNV"].ToString();
                dtpNgaySinh.Value = Convert.ToDateTime(row["NgaySinh"]);
                txtMatKhau.Text = row["MatKhau"].ToString();

                // Load after form load để đảm bảo các combobox đã được fill
                this.Load += (s, e) =>
                {
                    // Set giới tính
                    string gioiTinh = row["GioiTinh"].ToString().Trim();
                    for (int i = 0; i < cboGioiTinh.Items.Count; i++)
                    {
                        if (cboGioiTinh.Items[i].ToString() == gioiTinh)
                        {
                            cboGioiTinh.SelectedIndex = i;
                            break;
                        }
                    }

                    // Set lương
                    if (row.Table.Columns.Contains("LuongMoiGio"))
                        nudLuongMoiGio.Value = Convert.ToDecimal(row["LuongMoiGio"]);

                    // Set chức vụ dựa trên MaQuyen
                    if (row.Table.Columns.Contains("MaQuyen"))
                    {
                        string maQuyen = row["MaQuyen"].ToString();
                        for (int i = 0; i < cboChucVu.Items.Count; i++)
                        {
                            dynamic item = cboChucVu.Items[i];
                            if (item.MaQuyen == maQuyen)
                            {
                                cboChucVu.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    // Set trạng thái
                    if (row.Table.Columns.Contains("TrangThai"))
                    {
                        string trangThai = row["TrangThai"].ToString().Trim();
                        for (int i = 0; i < cboTrangThai.Items.Count; i++)
                        {
                            if (cboTrangThai.Items[i].ToString() == trangThai)
                            {
                                cboTrangThai.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenNV.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNV.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return false;
            }

            if (nudLuongMoiGio.Value <= 0)
            {
                MessageBox.Show("Vui lòng nhập lương/giờ hợp lệ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudLuongMoiGio.Focus();
                return false;
            }

            if (cboChucVu.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn chức vụ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboChucVu.Focus();
                return false;
            }

            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            MaNV = txtMaNV.Text.Trim();
            TenNV = txtTenNV.Text.Trim();
            NgaySinh = dtpNgaySinh.Value;
            GioiTinh = cboGioiTinh.SelectedItem.ToString();
            MatKhau = txtMatKhau.Text.Trim();
            LuongMoiGio = nudLuongMoiGio.Value;
            TrangThai = cboTrangThai.SelectedItem.ToString();

            // Lấy chức vụ và mã quyền từ item được chọn
            dynamic selectedItem = cboChucVu.SelectedItem;
            ChucVu = selectedItem.Text;
            MaQuyen = selectedItem.MaQuyen;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}