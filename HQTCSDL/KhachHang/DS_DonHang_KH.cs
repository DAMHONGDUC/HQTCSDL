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
    public partial class DS_DonHang_KH : Form
    {
        DataTable tbl_DSDonhang_KH;
        public DS_DonHang_KH()
        {
            InitializeComponent();
        }

        private void LoadData_DSDonhang() // tải dữ liệu vào DataGridView
        {
            string sql = "SELECT SP.TENSP, DH.SOLUONGSP, DH.NGAYLAP, DH.TONGPHI, DH.TINHTRANG, DH. PHIVANCHUYEN, DH.HINHTHUCTHANHTOAN, DH. TONGPHISP, CT.SOLUONG, CT.THANHTIEN" +
                " FROM SANPHAM SP, DONHANG DH, CT_DONHANG CT" +
                " WHERE DH.MADH = CT.MADH" +
                " AND CT.MASP = SP.MASP" +
                " AND DH.MAKH = 'KH001'";//'" + X + "'";//X(với X là mã của thằng khách hàng đang thao tác)";
            tbl_DSDonhang_KH = Functions.GetDataToTable(sql);
            dGv_KH_DSDonhang.DataSource = tbl_DSDonhang_KH;

            // set Font cho tên cột
            dGv_KH_DSDonhang.Font = new Font("Time New Roman", 13);
            dGv_KH_DSDonhang.Columns[0].HeaderText = "Tên Sản Phẩm";
            dGv_KH_DSDonhang.Columns[1].HeaderText = "Số lượng sản phẩm đơn hàng";
            dGv_KH_DSDonhang.Columns[2].HeaderText = "Ngày mua";
            dGv_KH_DSDonhang.Columns[3].HeaderText = "Tổng tiền";
            dGv_KH_DSDonhang.Columns[4].HeaderText = "Tình trạng đơn hàng";
            dGv_KH_DSDonhang.Columns[5].HeaderText = "Phí vận chuyển";
            dGv_KH_DSDonhang.Columns[6].HeaderText = "Hình thức thanh toán";
            dGv_KH_DSDonhang.Columns[7].HeaderText = "Tổng phí sản phẩm";
            dGv_KH_DSDonhang.Columns[8].HeaderText = "Số lượng sản phẩm chi tiết";
            dGv_KH_DSDonhang.Columns[9].HeaderText = "Thành tiền sản phẩm";

            // set Font cho dữ liệu hiển thị trong cột
            dGv_KH_DSDonhang.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGv_KH_DSDonhang.Columns[0].Width = 220;
            dGv_KH_DSDonhang.Columns[1].Width = 220;
            dGv_KH_DSDonhang.Columns[2].Width = 220;
            dGv_KH_DSDonhang.Columns[3].Width = 220;
            dGv_KH_DSDonhang.Columns[4].Width = 220;
            dGv_KH_DSDonhang.Columns[5].Width = 220;
            dGv_KH_DSDonhang.Columns[6].Width = 220;
            dGv_KH_DSDonhang.Columns[7].Width = 220;
            dGv_KH_DSDonhang.Columns[8].Width = 220;
            dGv_KH_DSDonhang.Columns[9].Width = 220;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGv_KH_DSDonhang.AllowUserToAddRows = false;
            dGv_KH_DSDonhang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void DS_DonHang_KH_Load(object sender, EventArgs e)
        {
            LoadData_DSDonhang();
        }


        private void dGv_KH_DSDonhang_Click(object sender, EventArgs e)
        {
            if (tbl_DSDonhang_KH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục 
            txtBox_TenSP_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["TENSP"].Value.ToString();
            txtBox_GIABAN_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["GIABAN"].Value.ToString();
            txtBox_SL_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["SOLUONG"].Value.ToString();
            txtBox_ThanhTien_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["THANHTIEN"].Value.ToString();
            txtBox_HTTT_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["HINHTHUCTHANHTOAN"].Value.ToString();
            txtBox_TongTien_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["TONGPHI"].Value.ToString();
            txtBox_PhiVanChuyen_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["PHIVANCHUYEN"].Value.ToString();
            txtBox_DIACHI_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["DIACHIGH"].Value.ToString();
            dTp_NGAYLAP_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["NGAYLAP"].Value.ToString();
            txtBox_TTDH_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["TINHTRANG"].Value.ToString();
        }
    }
  
}
