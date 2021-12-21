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
    public partial class DS_SanPham_KH : Form
    {
        DataTable tbl_DSSP_KH;
        public DS_SanPham_KH()
        {
            InitializeComponent();
        }

        private void btn_Back_SP_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Muangay_KH_xemSP_Click(object sender, EventArgs e)
        {
            DatHang_KH dathang_kh = new DatHang_KH();
            dathang_kh.Show();
        }
        private void LoadData_DSSP() // tải dữ liệu vào DataGridView
        {
            string sql = "SELECT SP.TENSP, SP.SOLUONG, SP.GIABAN, CN.DIACHI " +
                " FROM SANPHAM SP, CHINHANH CN" +
                " WHERE SP.CHINHANH = CN.MACHINHANH" +
                " AND SP.MADT = CN.MADT" +
                " AND SP.MADT = 'DT001'";//'" + X + "'";//X(với X là mã của thằng khách hàng đang thao tác)";
            tbl_DSSP_KH = Functions.GetDataToTable(sql);
            dGv_KH_DSSP.DataSource = tbl_DSSP_KH;

            // set Font cho tên cột
            dGv_KH_DSSP.Font = new Font("Time New Roman", 13);
            dGv_KH_DSSP.Columns[0].HeaderText = "Tên Sản Phẩm";
            dGv_KH_DSSP.Columns[1].HeaderText = "Số lượng sản phẩm";
            dGv_KH_DSSP.Columns[2].HeaderText = "Giá bán";
            dGv_KH_DSSP.Columns[3].HeaderText = "Địa chỉ chi nhánh có hàng";

            // set Font cho dữ liệu hiển thị trong cột
            dGv_KH_DSSP.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGv_KH_DSSP.Columns[0].Width = 220;
            dGv_KH_DSSP.Columns[1].Width = 220;
            dGv_KH_DSSP.Columns[2].Width = 220;
            dGv_KH_DSSP.Columns[3].Width = 220;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGv_KH_DSSP.AllowUserToAddRows = false;
            dGv_KH_DSSP.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void DS_SanPham_KH_Load(object sender, EventArgs e)
        {
            LoadData_DSSP();
        }

        private void dGv_KH_DSSP_Click(object sender, EventArgs e)
        {
            //Nếu không có dữ liệu
            if (tbl_DSSP_KH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục 
            txtBox_TenSP_KH_xemSP.Text = dGv_KH_DSSP.CurrentRow.Cells["TENSP"].Value.ToString();
            txtBox_SOLUONG_KH_xemSP.Text = dGv_KH_DSSP.CurrentRow.Cells["SOLUONG"].Value.ToString();
            txtBox_DIACHI_KH_xemSP.Text = dGv_KH_DSSP.CurrentRow.Cells["DIACHI"].Value.ToString();
            txtBox_GIABAN_KH_xemSP.Text = dGv_KH_DSSP.CurrentRow.Cells["GIABAN"].Value.ToString();
        }
    }
}
