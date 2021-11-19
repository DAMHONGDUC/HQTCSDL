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
        DataTable tbl_HDCD;
        public HopDongDaDuyet_NV()
        {
            InitializeComponent();
        }

        private void LoadData_HDCD()//dữ liệu vào DataGridView
        {
            string sql = "SELECT HD.MAHD,HD.MADT,HD.SLCHINHANH, HD.PTHOAHONG,HD.NGAYLAP" +
                " FROM HOPDONG HD" +
                " WHERE HD.DADUYET = 0;";
            tbl_HDCD = Functions.GetDataToTable(sql);
            dGV_NhanVien_HDCD.DataSource = tbl_HDCD;

            // set Font cho tên cột
            dGV_NhanVien_HDCD.Font = new Font("Time New Roman", 13);
            dGV_NhanVien_HDCD.Columns[0].HeaderText = "Mã hợp đồng";
            dGV_NhanVien_HDCD.Columns[1].HeaderText = "Mã đối tác";
            dGV_NhanVien_HDCD.Columns[2].HeaderText = "Số lượng chi nhánh";
            dGV_NhanVien_HDCD.Columns[3].HeaderText = "Phần trăm hoa hồng";
            dGV_NhanVien_HDCD.Columns[4].HeaderText = "Ngày lập";

            // set Font cho dữ liệu hiển thị trong cột
            dGV_NhanVien_HDCD.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_NhanVien_HDCD.Columns[0].Width = 300;
            dGV_NhanVien_HDCD.Columns[1].Width = 300;
            dGV_NhanVien_HDCD.Columns[2].Width = 300;
            dGV_NhanVien_HDCD.Columns[3].Width = 300;
            dGV_NhanVien_HDCD.Columns[4].Width = 300;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_NhanVien_HDCD.AllowUserToAddRows = false;
            dGV_NhanVien_HDCD.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void HopDongDaDuyet_NV_Load(object sender, EventArgs e)
        {
            LoadData_HDCD();
        }
    }
}
