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

        private void btn_XemSP_KH_Click(object sender, EventArgs e)
        {
            DS_SanPham_KH ds_sanpham = new DS_SanPham_KH();
            ds_sanpham.Show();
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
            dGv_KH_DSDT.Columns[3].Width = 230;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGv_KH_DSDT.AllowUserToAddRows = false;
            dGv_KH_DSDT.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void DSDoiTac_KH_Load(object sender, EventArgs e)
        {
            LoadData_DSDT();
        }

        private void dGv_KH_DSDT_Click(object sender, EventArgs e)
        {
            if (tbl_DSDoitac_KH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục 
            txtBox_TenDT_KH_xemDT.Text = dGv_KH_DSDT.CurrentRow.Cells["TENDT"].Value.ToString();
            txtBox_DiaChi_KH_xemDT.Text = dGv_KH_DSDT.CurrentRow.Cells["DIACHI"].Value.ToString();
            txtBox_ChiNhanh_KH_xemDT.Text = dGv_KH_DSDT.CurrentRow.Cells["SOCHINHANH"].Value.ToString();
            txtBox_LoaiHang_KH_xemDT.Text = dGv_KH_DSDT.CurrentRow.Cells["LOAIHANG"].Value.ToString();

        }
    }
}
