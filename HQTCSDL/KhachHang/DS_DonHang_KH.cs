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
        private string maacc;
        public DS_DonHang_KH(string tendangnhap)
        {
            this.maacc = tendangnhap;
            InitializeComponent();
            
        }

        private void LoadData_DSDonhang() // tải dữ liệu vào DataGridView
        {
            string sql = "SELECT DH.MADH, DH.SOLUONGSP,DH.DIACHIGH, DH.PHIVANCHUYEN," +
                " DH.TONGPHI, DH.HINHTHUCTHANHTOAN, DH.NGAYLAP, DH.TINHTRANG " +
                "FROM DONHANG DH " +
                "WHERE DH.MAKH = '"+maacc+"'";
                
                
            tbl_DSDonhang_KH = Functions.GetDataToTable(sql);
            dGv_KH_DSDonhang.DataSource = tbl_DSDonhang_KH;

            // set Font cho tên cột
            dGv_KH_DSDonhang.Font = new Font("Time New Roman", 13);
            dGv_KH_DSDonhang.Columns[0].HeaderText = "Mã đơn hàng";
            dGv_KH_DSDonhang.Columns[1].HeaderText = "Số lượng sản phẩm đơn hàng";
            dGv_KH_DSDonhang.Columns[2].HeaderText = "Địa chỉ giao hàng";
            dGv_KH_DSDonhang.Columns[3].HeaderText = "Phí vận chuyển";
            dGv_KH_DSDonhang.Columns[4].HeaderText = "Tổng phí";
            dGv_KH_DSDonhang.Columns[5].HeaderText = "Hình thức thanh toán";
            dGv_KH_DSDonhang.Columns[6].HeaderText = "Ngày lập";
            dGv_KH_DSDonhang.Columns[7].HeaderText = "Tình trạng";
            

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
            txtBox_MaDH_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["MADH"].Value.ToString();
            txtBox_SL_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["SOLUONGSP"].Value.ToString();
            txtBox_DIACHI_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["DIACHIGH"].Value.ToString();
            txtBox_PhiVanChuyen_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["PHIVANCHUYEN"].Value.ToString();
            txtBox_TongTien_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["TONGPHI"].Value.ToString();
            string httt = "";
            if(dGv_KH_DSDonhang.CurrentRow.Cells["HINHTHUCTHANHTOAN"].Value.ToString() == "0")
            {
                httt = "Tiền mặt";
            }
            if (dGv_KH_DSDonhang.CurrentRow.Cells["HINHTHUCTHANHTOAN"].Value.ToString() == "1")
            {
                httt = "Ví điện tử";
            }
            if (dGv_KH_DSDonhang.CurrentRow.Cells["HINHTHUCTHANHTOAN"].Value.ToString() == "2")
            {
                httt = "Thẻ ngân hàng";
            }
            txtBox_HTTT_KH_xemDH.Text = httt;
            dTp_NGAYLAP_KH_xemDH.Text = dGv_KH_DSDonhang.CurrentRow.Cells["NGAYLAP"].Value.ToString();
            string tinhtrang = "";
            if(dGv_KH_DSDonhang.CurrentRow.Cells["TINHTRANG"].Value.ToString() == "0")
            {
                tinhtrang = "Chưa nhận";
            }
            if (dGv_KH_DSDonhang.CurrentRow.Cells["TINHTRANG"].Value.ToString() == "1")
            {
                tinhtrang = "Đã nhận";
            }
            if (dGv_KH_DSDonhang.CurrentRow.Cells["TINHTRANG"].Value.ToString() == "2")
            {
                tinhtrang = "Đang giao";
            }

            if (dGv_KH_DSDonhang.CurrentRow.Cells["TINHTRANG"].Value.ToString() == "3")
            {
                tinhtrang = "Đã giao";
            }
            if (dGv_KH_DSDonhang.CurrentRow.Cells["TINHTRANG"].Value.ToString() == "4")
            {
                tinhtrang = "Giao chưa thành công";
            }
            txtBox_TTDH_KH_xemDH.Text = tinhtrang;
            
        }
    }
  
}
