using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HQTCSDL
{
    public partial class SanPham_DT : Form
    {
        DataTable tbl_DoiTac_SP;
        public SanPham_DT()
        {
            InitializeComponent();
        }
        private void LoadData_SP()//dữ liệu vào DataGridView
        {
            string sql = "SELECT SP.MASP,SP.MADT,SP.MASP,SP.TENSP," +
                "SP.SOLUONG,SP.CHINHANH, SP.GIABAN, CN.TENCHINHANH " +
                "FROM SANPHAM SP, CHINHANH CN " +
                "WHERE CN.MACHINHANH = SP.CHINHANH";
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
        }
    }
}
