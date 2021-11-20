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
    public partial class DSTaiKHoan_admin : Form
    {
        DataTable tbl_account;
        public DSTaiKHoan_admin()
        {
            InitializeComponent();
        }

        private void btn_themTK_admin_Click(object sender, EventArgs e)
        {
            Form_ThemTK_admin form_themtk = new Form_ThemTK_admin();
            form_themtk.StartPosition = FormStartPosition.CenterScreen;
            form_themtk.Show();
        }

        private void LoadData_DSTaiKhoan() // tải dữ liệu vào DataGridView
        {
            string sql = "SELECT TENDANGNHAP, MATKHAU, LOAIACC FROM ACCOUNT";
            tbl_account = Functions.GetDataToTable(sql);
            dGV_dstaikhoan_AD.DataSource = tbl_account;

            // set Font cho tên cột
            dGV_dstaikhoan_AD.Font = new Font("Time New Roman", 13);
            dGV_dstaikhoan_AD.Columns[0].HeaderText = "Tên Đăng Nhập";
            dGV_dstaikhoan_AD.Columns[1].HeaderText = "Mật Khẩu";
            dGV_dstaikhoan_AD.Columns[2].HeaderText = "Loại Tài Khoản";        

            // set Font cho dữ liệu hiển thị trong cột
            dGV_dstaikhoan_AD.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_dstaikhoan_AD.Columns[0].Width = 300;
            dGV_dstaikhoan_AD.Columns[1].Width = 300;
            dGV_dstaikhoan_AD.Columns[2].Width = 300;        

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_dstaikhoan_AD.AllowUserToAddRows = false;
            dGV_dstaikhoan_AD.EditMode = DataGridViewEditMode.EditProgrammatically;          
        }

        private void DSTaiKHoan_admin_Load(object sender, EventArgs e)
        {
            LoadData_DSTaiKhoan();
        }

        private void dGV_dstaikhoan_AD_Click(object sender, EventArgs e)
        {
            //Nếu không có dữ liệu
            if (tbl_account.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục 
            txtBox_tendangnhap_DSTK.Text = dGV_dstaikhoan_AD.CurrentRow.Cells["TENDANGNHAP"].Value.ToString();
            txtBox_matkhau_DSTK.Text = dGV_dstaikhoan_AD.CurrentRow.Cells["MATKHAU"].Value.ToString();
            txtBox_loaitaikhoan_DSTK.Text = dGV_dstaikhoan_AD.CurrentRow.Cells["LOAIACC"].Value.ToString();          
        }
    }
}
