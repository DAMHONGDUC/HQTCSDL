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
    public partial class HopDong_DT : Form
    {
        DataTable tbl_DoiTac_HD;
        public HopDong_DT()
        {
            InitializeComponent();
        }
        private void LoadData_CN()//dữ liệu vào DataGridView
        {
            string sql = "SELECT HD.MADT,HD.MAHD,HD.NGAYLAP," +
                "HD.NGAYKETTHUC,HD.SLCHINHANH,HD.DADUYET " +
                "FROM HOPDONG HD WHERE HD.DADUYET = 1";
           
            tbl_DoiTac_HD = Functions.GetDataToTable(sql);           
            dGV_DoiTac_HD.DataSource = tbl_DoiTac_HD;

            // set Font cho tên cột
            dGV_DoiTac_HD.Font = new Font("Time New Roman", 13);
            dGV_DoiTac_HD.Columns[0].HeaderText = "Mã hợp đồng";
            dGV_DoiTac_HD.Columns[1].HeaderText = "Mã đối tác";
            dGV_DoiTac_HD.Columns[2].HeaderText = "Ngày lập";
            dGV_DoiTac_HD.Columns[3].HeaderText = "Ngày kết thúc";
            dGV_DoiTac_HD.Columns[4].HeaderText = "Số lượng chi nhánh";
            dGV_DoiTac_HD.Columns[5].HeaderText = "Đã duyệt";


            // set Font cho dữ liệu hiển thị trong cột
            dGV_DoiTac_HD.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_DoiTac_HD.Columns[0].Width = 200;
            dGV_DoiTac_HD.Columns[1].Width = 200;
            dGV_DoiTac_HD.Columns[2].Width = 200;
            dGV_DoiTac_HD.Columns[3].Width = 200;
            dGV_DoiTac_HD.Columns[4].Width = 200;
            dGV_DoiTac_HD.Columns[5].Width = 200;


            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_DoiTac_HD.AllowUserToAddRows = false;
            dGV_DoiTac_HD.EditMode = DataGridViewEditMode.EditProgrammatically;
        }


        private void HopDong_DT_Load(object sender, EventArgs e)
        {
            LoadData_CN();
        }

        private void dGV_DoiTac_HD_Click(object sender, EventArgs e)
        {
            //Nếu không có dữ liệu
            if (tbl_DoiTac_HD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục            
            txtBox_tinhtrang_HDDL.Text = dGV_DoiTac_HD.CurrentRow.Cells["DADUYET"].Value.ToString();
            txtBox_sochinhanh_HDDL.Text = dGV_DoiTac_HD.CurrentRow.Cells["SLCHINHANH"].Value.ToString();
            dTP_ngaylap_HDDL.Text = dGV_DoiTac_HD.CurrentRow.Cells["NGAYLAP"].Value.ToString();
            dTP_ngayketthuc_HDHL.Text = dGV_DoiTac_HD.CurrentRow.Cells["NGAYKETTHUC"].Value.ToString();
        }
    }
}
