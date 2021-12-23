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

namespace HQTCSDL
{
    public partial class SanPham_DT : Form
    {
        DataTable tbl_DoiTac_SP;
        string MASP;
        string MADT;
        public SanPham_DT(string madt)
        {
            InitializeComponent();
            MADT = madt;
        }
        private void LoadData_SP()//dữ liệu vào DataGridView
        {
            string sql = "SELECT SP.MASP,SP.MADT,SP.MASP,SP.TENSP," +
                "SP.SOLUONG,SP.CHINHANH, SP.GIABAN, CN.TENCHINHANH " +
                "FROM SANPHAM SP, CHINHANH CN " +
                "WHERE CN.MACHINHANH = SP.CHINHANH " +
                "AND SP.MADT = '" + MADT + "'";
            tbl_DoiTac_SP = Functions.GetDataToTable(sql);
            dGV_DoiTac_SP.DataSource = tbl_DoiTac_SP;

            // set Font cho tên cột
            dGV_DoiTac_SP.Font = new Font("Time New Roman", 13);
            dGV_DoiTac_SP.Columns[0].HeaderText = "Mã sản phẩm";
            dGV_DoiTac_SP.Columns[1].HeaderText = "Mã đối tác";
            dGV_DoiTac_SP.Columns[2].HeaderText = "Mã sản phẩm";
            dGV_DoiTac_SP.Columns[3].HeaderText = "Tên sản phẩm";
            dGV_DoiTac_SP.Columns[4].HeaderText = "Số lượng";
            dGV_DoiTac_SP.Columns[5].HeaderText = "Chi nhánh";
            // set Font cho dữ liệu hiển thị trong cột
            dGV_DoiTac_SP.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_DoiTac_SP.Columns[0].Width = 150;
            dGV_DoiTac_SP.Columns[1].Width = 150;
            dGV_DoiTac_SP.Columns[2].Width = 200;
            dGV_DoiTac_SP.Columns[3].Width = 200;
            dGV_DoiTac_SP.Columns[4].Width = 150;
            dGV_DoiTac_SP.Columns[5].Width = 150;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_DoiTac_SP.AllowUserToAddRows = false;
            dGV_DoiTac_SP.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void SanPham_DT_Load(object sender, EventArgs e)
        {
            LoadData_SP();
        }

        private void dGV_DoiTac_SP_Click(object sender, EventArgs e)
        {
            //Nếu không có dữ liệu
            if (tbl_DoiTac_SP.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục            
            cbBox_chinhnhanh_SP.Text = dGV_DoiTac_SP.CurrentRow.Cells["TENCHINHANH"].Value.ToString();
            txtBox_tensanpham_SP.Text = dGV_DoiTac_SP.CurrentRow.Cells["TENSP"].Value.ToString();
            txtBox_soluong_SP.Text = dGV_DoiTac_SP.CurrentRow.Cells["SOLUONG"].Value.ToString();
            txtBox_giaban_SP.Text = dGV_DoiTac_SP.CurrentRow.Cells["GIABAN"].Value.ToString();
            MASP = dGV_DoiTac_SP.CurrentRow.Cells["MASP"].Value.ToString();
        }


        private void Run_SP_DT_UPDATE_GiASP(string madt, string masp, string giamoi)
        {
            SqlCommand cmd = new SqlCommand("DT_UPDATE_GiASP", Functions.Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // set kiểu dữ liệu
            cmd.Parameters.Add("@MASP", SqlDbType.VarChar, 15);
            cmd.Parameters.Add("@MADT", SqlDbType.VarChar, 15);
            cmd.Parameters.Add("@GIAMOI", SqlDbType.Decimal, 19);
            cmd.Parameters["@GIAMOI"].Precision = 19;
            cmd.Parameters["@GIAMOI"].Scale = 4;

            // set giá trị
            cmd.Parameters["@MASP"].Value = masp;
            cmd.Parameters["@MADT"].Value = madt;
            cmd.Parameters["@GIAMOI"].Value = giamoi;

            cmd.ExecuteNonQuery();
        }

        private void btn_capnhat_SP_DT_Click(object sender, EventArgs e)
        {
            // KT có nhập đầy đủ dữ liệu không
            if (cbBox_chinhnhanh_SP.Text.Trim().Length == 0 | txtBox_soluong_SP.Text.Trim().Length == 0
                | txtBox_tensanpham_SP.Text.Trim().Length == 0 | txtBox_giaban_SP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // xử lí update 
            Run_SP_DT_UPDATE_GiASP(MADT, MASP, txtBox_giaban_SP.Text.Trim().ToString());

            // thông báo
            MessageBox.Show("Cập nhật thành công!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);          

            LoadData_SP();
            txtBox_giaban_SP.Text = dGV_DoiTac_SP.CurrentRow.Cells["GIABAN"].Value.ToString();
        }
    }
}
