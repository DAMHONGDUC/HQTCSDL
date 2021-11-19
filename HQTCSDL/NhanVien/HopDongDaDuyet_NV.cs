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
    public partial class HopDongChuaDuyet_NV : Form
    {
        DataTable tbl_HDDD;
        public HopDongChuaDuyet_NV()
        {
            InitializeComponent();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void LoadData_HDDD()//dữ liệu vào DataGridView
        {
            string sql = "SELECT HD.MAHD,HD.MADT,HD.SLCHINHANH, HD.PTHOAHONG,HD.NGAYLAP,HD.NGAYKETTHUC" +
                " FROM HOPDONG HD" +
                " WHERE HD.DADUYET = 1;";
            tbl_HDDD = Functions.GetDataToTable(sql);
            dGV_NhanVien_HDDD.DataSource = tbl_HDDD;

            // set Font cho tên cột
            dGV_NhanVien_HDDD.Font = new Font("Time New Roman", 13);
            dGV_NhanVien_HDDD.Columns[0].HeaderText = "Mã hợp đồng";
            dGV_NhanVien_HDDD.Columns[1].HeaderText = "Mã đối tác";
            dGV_NhanVien_HDDD.Columns[2].HeaderText = "Số lượng chi nhánh";
            dGV_NhanVien_HDDD.Columns[3].HeaderText = "Phần trăm hoa hồng";
            dGV_NhanVien_HDDD.Columns[4].HeaderText = "Ngày lập";
            dGV_NhanVien_HDDD.Columns[5].HeaderText = "Ngày kết thúc";
            // set Font cho dữ liệu hiển thị trong cột
            dGV_NhanVien_HDDD.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_NhanVien_HDDD.Columns[0].Width = 150;
            dGV_NhanVien_HDDD.Columns[1].Width = 150;
            dGV_NhanVien_HDDD.Columns[2].Width = 200;
            dGV_NhanVien_HDDD.Columns[3].Width = 200;
            dGV_NhanVien_HDDD.Columns[4].Width = 150;
            dGV_NhanVien_HDDD.Columns[5].Width = 150;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_NhanVien_HDDD.AllowUserToAddRows = false;
            dGV_NhanVien_HDDD.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
       

        private void HopDongChuaDuyet_NV_Load(object sender, EventArgs e)
        {
            LoadData_HDDD();
        }
    }
}
