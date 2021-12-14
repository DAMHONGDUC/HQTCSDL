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
    public partial class HopDongDaDuyet_NV : Form
    {
        DataTable tbl_HDDD;
        public HopDongDaDuyet_NV()
        {
            InitializeComponent();
        }

        private void LoadData_HDDD()//dữ liệu vào DataGridView
        {
            string sql = "SELECT HD.MAHD, HD.MADT, DT.TENDT, DT.NGUOIDAIDIEN, " +
                "HD.SLCHINHANH, HD.PTHOAHONG, " +
                "HD.THOIHANHD, HD.NGAYLAP, HD.NGAYBATDAU, HD.NGAYKETTHUC, " +
                "DATEDIFF(DAY,HD.NGAYBATDAU,HD.NGAYKETTHUC) AS TGHIEULUC" +
                " FROM HOPDONG HD, DOITAC DT" +
                " WHERE HD.DADUYET = 1 " +
                "AND HD.MADT = DT.MADT";
            tbl_HDDD = Functions.GetDataToTable(sql);
            dGV_HDDD_NV.DataSource = tbl_HDDD;

            // set Font cho tên cột
            dGV_HDDD_NV.Font = new Font("Time New Roman", 13);
            dGV_HDDD_NV.Columns[0].HeaderText = "Mã hợp đồng";
            dGV_HDDD_NV.Columns[1].HeaderText = "Mã đối tác";
            dGV_HDDD_NV.Columns[2].HeaderText = "Số lượng chi nhánh";
            dGV_HDDD_NV.Columns[3].HeaderText = "Phần trăm hoa hồng";
            dGV_HDDD_NV.Columns[4].HeaderText = "Ngày lập";
            dGV_HDDD_NV.Columns[5].HeaderText = "Ngày kết thúc";
            // set Font cho dữ liệu hiển thị trong cột
            dGV_HDDD_NV.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_HDDD_NV.Columns[0].Width = 150;
            dGV_HDDD_NV.Columns[1].Width = 150;
            dGV_HDDD_NV.Columns[2].Width = 200;
            dGV_HDDD_NV.Columns[3].Width = 200;
            dGV_HDDD_NV.Columns[4].Width = 150;
            dGV_HDDD_NV.Columns[5].Width = 150;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_HDDD_NV.AllowUserToAddRows = false;
            dGV_HDDD_NV.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
  
        private void HopDongDaDuyet_NV_Load(object sender, EventArgs e)
        {
            LoadData_HDDD();
        }

        private void dGV_HDDD_NV_Click(object sender, EventArgs e) // xử lí khi click vào datagridview
        {
            //Nếu không có dữ liệu
            if (tbl_HDDD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục 
            dTP_ngaylap_HHDDNV.CustomFormat = "yyyy-MM-dd";
            dTP_ngaybd_HHDDNV.CustomFormat = "yyyy-MM-dd";
            dTP_ngaykt_HHDDNV.CustomFormat = "yyyy-MM-dd";

            txtBox_mahd_HHDDNV.Text = dGV_HDDD_NV.CurrentRow.Cells["MAHD"].Value.ToString();
            txtBox_slchinhanh_HHDDNV.Text = dGV_HDDD_NV.CurrentRow.Cells["SLCHINHANH"].Value.ToString();
            txtBox_madt_HHDDNV.Text = dGV_HDDD_NV.CurrentRow.Cells["MADT"].Value.ToString();
            txtBox_tendt_HHDDNV.Text = dGV_HDDD_NV.CurrentRow.Cells["TENDT"].Value.ToString();
            txtBox_nguoidaidien_HHDDNV.Text = dGV_HDDD_NV.CurrentRow.Cells["NGUOIDAIDIEN"].Value.ToString();
            txtBox_pthoahong_HHDDNV.Text = dGV_HDDD_NV.CurrentRow.Cells["PTHOAHONG"].Value.ToString();
            txtBox_thoihanhd_HHDDNV.Text = dGV_HDDD_NV.CurrentRow.Cells["THOIHANHD"].Value.ToString();
            dTP_ngaylap_HHDDNV.Text = dGV_HDDD_NV.CurrentRow.Cells["NGAYLAP"].Value.ToString();
            dTP_ngaybd_HHDDNV.Text = dGV_HDDD_NV.CurrentRow.Cells["NGAYBATDAU"].Value.ToString();
            dTP_ngaykt_HHDDNV.Text = dGV_HDDD_NV.CurrentRow.Cells["NGAYKETTHUC"].Value.ToString();
            txtBox_tghieuluc_HHDDNV.Text = dGV_HDDD_NV.CurrentRow.Cells["TGHIEULUC"].Value.ToString();
        }
    }
}
