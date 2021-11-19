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
    public partial class ThongKe_TX : Form
    {
        DataTable tbl_TaiXe_TK;
        public ThongKe_TX()
        {
            InitializeComponent();
        }
        private void LoadData_ThongKe()//dữ liệu vào DataGridView
        {
            string sql = "SELECT DH.MADH,DH.PHIVANCHUYEN" +
                " FROM  DONHANG DH" +
                " WHERE DH.TINHTRANG = 1";

            
            tbl_TaiXe_TK = Functions.GetDataToTable(sql);
            dGV_TaiXe_TK.DataSource = tbl_TaiXe_TK;

            // set Font cho tên cột
            dGV_TaiXe_TK.Font = new Font("Time New Roman", 13);
            dGV_TaiXe_TK.Columns[0].HeaderText = "Mã đơn hàng";
            dGV_TaiXe_TK.Columns[1].HeaderText = "Phí vận chuyển";

            // set Font cho dữ liệu hiển thị trong cột
            dGV_TaiXe_TK.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_TaiXe_TK.Columns[0].Width = 500;
            dGV_TaiXe_TK.Columns[1].Width = 500;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_TaiXe_TK.AllowUserToAddRows = false;
            dGV_TaiXe_TK.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ThongKe_TX_Load(object sender, EventArgs e)
        {
            LoadData_ThongKe();
            
        }
    }
}
