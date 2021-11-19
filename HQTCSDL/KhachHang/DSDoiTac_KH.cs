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
    public partial class DSDoiTac_KH : Form
    {
        DataTable tbl_DSDoitac_KH;
        public DSDoiTac_KH()
        {
            InitializeComponent();
        }

        private void LoadData_DSDT() // tải dữ liệu vào DataGridView
        {
            string sql = "SELECT TENDT, DIACHI, SOCHINHANH, LOAIHANG" +
                " FROM DOITAC";
            tbl_DSDoitac_KH = Functions.GetDataToTable(sql);
            dGv_KH_DSDT.DataSource = tbl_DSDoitac_KH;

            // set Font cho tên cột
            dGv_KH_DSDT.Font = new Font("Time New Roman", 13);
            dGv_KH_DSDT.Columns[0].HeaderText = "Tên Đối Tác";
            dGv_KH_DSDT.Columns[1].HeaderText = "Địa chỉ đối tác";
            dGv_KH_DSDT.Columns[2].HeaderText = "Số lượng chi nhánh";
            dGv_KH_DSDT.Columns[3].HeaderText = "Loại hàng cung cấp";

            // set Font cho dữ liệu hiển thị trong cột
            dGv_KH_DSDT.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGv_KH_DSDT.Columns[0].Width = 220;
            dGv_KH_DSDT.Columns[1].Width = 220;
            dGv_KH_DSDT.Columns[2].Width = 220;
            dGv_KH_DSDT.Columns[3].Width = 220;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGv_KH_DSDT.AllowUserToAddRows = false;
            dGv_KH_DSDT.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btn_XemSP_KH_Click(object sender, EventArgs e)
        {
            DS_SanPham_KH ds_sanpham = new DS_SanPham_KH();
            ds_sanpham.Show();
        }

        private void DSDoiTac_KH_Load(object sender, EventArgs e)
        {
            LoadData_DSDT();
        }


    }
}
