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
using System.Data;

namespace HQTCSDL
{
    public partial class ChiNhanh_DT : Form
    {
        DataTable tbl_DoiTac_CN;
        string dt;
        public ChiNhanh_DT(string madt)
        {
            InitializeComponent();
            dt = madt;
        }
        
        private void LoadData_CN()//dữ liệu vào DataGridView
        {
            string sql = "SELECT CN.TENCHINHANH, CN.DIACHI" +
                " FROM CHINHANH CN WHERE CN.MADT = " + "'" + dt + "'";
            tbl_DoiTac_CN = Functions.GetDataToTable(sql);
            dGV_DoiTac_CN.DataSource = tbl_DoiTac_CN;

            // set Font cho tên cột
            dGV_DoiTac_CN.Font = new Font("Time New Roman", 13);
            dGV_DoiTac_CN.Columns[0].HeaderText = "Tên chi nhánh";
            dGV_DoiTac_CN.Columns[1].HeaderText = "Địa chỉ";


            // set Font cho dữ liệu hiển thị trong cột
            dGV_DoiTac_CN.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_DoiTac_CN.Columns[0].Width = 450;
            dGV_DoiTac_CN.Columns[1].Width = 450;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_DoiTac_CN.AllowUserToAddRows = false;
            dGV_DoiTac_CN.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ChiNhanh_DT_Load(object sender, EventArgs e)
        {
            LoadData_CN();
        }



        private void btn_DT_ThemCN_Click(object sender, EventArgs e)
        {
            txtBox_tenchinhanh_CN.Text = "";
            txtBox_diachi_CN.Text = "";
        }

        private void btn_DT_LuuCN_Click(object sender, EventArgs e)
        {
            // TH người dùng chưa nhập đầy đủ dữ liệu chưa
            if (txtBox_tenchinhanh_CN.Text.Trim().Length == 0 | txtBox_diachi_CN.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn cần phải nhập đầy đủ dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // nếu đã thỏa hết các điều kiện ở trên
            try
            {
                string sql = "SELECT TOP 1 MACHINHANH FROM CHINHANH ORDER BY MACHINHANH DESC";
                string macn = Functions.GetFieldValues(sql);
                string[] elements = macn.Split('N');
                int maso = Int32.Parse(elements[1]) + 1;
                macn = "CN" + maso.ToString();

                SqlCommand cmd = new SqlCommand("sp_DT_ThemChiNhanh", Functions.Con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // set kiểu dữ liệu
                cmd.Parameters.Add("@MADT", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@MACHINHANH", SqlDbType.VarChar, 15);
                cmd.Parameters.Add("@DIACHI", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@TEN", SqlDbType.NVarChar, 50);
        
                // set giá trị

                cmd.Parameters["@MADT"].Value = dt.Trim();
                cmd.Parameters["@MACHINHANH"].Value = macn.Trim();
                cmd.Parameters["@DIACHI"].Value = txtBox_diachi_CN.Text.Trim();
                cmd.Parameters["@TEN"].Value = txtBox_tenchinhanh_CN.Text.Trim();

                cmd.ExecuteNonQuery();

                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtBox_tenchinhanh_CN.Text = "";
                txtBox_diachi_CN.Text = "";
                LoadData_CN();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thông tin thất bại, mã lỗi: " + ex.Message); // This will display all the error in your statement.
            }
        }

        private void dGV_DoiTac_CN_Click(object sender, EventArgs e)
        {
            if (tbl_DoiTac_CN.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục            
            txtBox_tenchinhanh_CN.Text = dGV_DoiTac_CN.CurrentRow.Cells["TENCHINHANH"].Value.ToString();
            txtBox_diachi_CN.Text = dGV_DoiTac_CN.CurrentRow.Cells["DIACHI"].Value.ToString();

        }
    }
}
