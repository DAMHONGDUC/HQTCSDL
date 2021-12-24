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
    public partial class DonHang_DT : Form
    {
        DataTable tbl_DonHang_DT;
        public DonHang_DT()
        {
            InitializeComponent();
        }

        private void LoadData_CN() //dữ liệu vào DataGridView
        {
            DataTable tbl_DonHang_DT2 = new DataTable();

            // add các cột
            tbl_DonHang_DT2.Columns.Add("MADH", typeof(string));
            tbl_DonHang_DT2.Columns.Add("MASP", typeof(string));
            tbl_DonHang_DT2.Columns.Add("TENKH", typeof(string));
            tbl_DonHang_DT2.Columns.Add("SDT", typeof(string));
            tbl_DonHang_DT2.Columns.Add("NGAYKHNHAN", typeof(string));
            tbl_DonHang_DT2.Columns.Add("TENTX", typeof(string));
            tbl_DonHang_DT2.Columns.Add("SDTTX", typeof(string));

            tbl_DonHang_DT2.Rows.Add(new object[] { "DH001","SP001", "Nguyễn Chí Thanh", "0123456789",
            "20/11/2021","Hoàng Văn Chí","0472681938"});

            dGV_donhang_DT.DataSource = tbl_DonHang_DT2;

            // set Font cho tên cột
            dGV_donhang_DT.Font = new Font("Time New Roman", 13);
            dGV_donhang_DT.Columns[0].HeaderText = "Mã đơn hàng";
            dGV_donhang_DT.Columns[1].HeaderText = "Mã sản phẩm";
            dGV_donhang_DT.Columns[2].HeaderText = "Tên khách hàng";
            dGV_donhang_DT.Columns[3].HeaderText = "SĐT";
            dGV_donhang_DT.Columns[4].HeaderText = "Ngày KH nhận";
            dGV_donhang_DT.Columns[5].HeaderText = "Tên tài xế";
            dGV_donhang_DT.Columns[6].HeaderText = "SĐT";

            // set Font cho dữ liệu hiển thị trong cột
            dGV_donhang_DT.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_donhang_DT.Columns[0].Width = 150;
            dGV_donhang_DT.Columns[1].Width = 150;
            dGV_donhang_DT.Columns[2].Width = 150;
            dGV_donhang_DT.Columns[3].Width = 150;
            dGV_donhang_DT.Columns[4].Width = 150;
            dGV_donhang_DT.Columns[5].Width = 150;
            dGV_donhang_DT.Columns[6].Width = 150;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_donhang_DT.AllowUserToAddRows = false;
            dGV_donhang_DT.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void DonHang_DT_Load(object sender, EventArgs e)
        {
            LoadData_CN();
        }

        private void dGV_donhang_DT_Click(object sender, EventArgs e)
        {
            ////Nếu không có dữ liệu
            //if (tbl_DonHang_DT.Rows.Count == 0)
            //{
            //    MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //// set giá trị cho các mục            
            //txtBox_tenchinhanh_CN.Text = dGV_donhang_DT.CurrentRow.Cells["TENCHINHANH"].Value.ToString();
            //txtBox_diachi_CN.Text = dGV_donhang_DT.CurrentRow.Cells["DIACHI"].Value.ToString();
        }
    }
}
