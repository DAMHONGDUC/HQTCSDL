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
    public partial class DonHangDaNhan_TX : Form
    {
        DataTable tbl_TaiXe_DHDN1;
        DataTable tbl_TaiXe_DHDN2;

        public DonHangDaNhan_TX()
        {
            InitializeComponent();
        }

        private void LoadData_CTHD()//dữ liệu vào DataGridView
        {
            // đọc lần 1
            string sql = "SELECT DH.MADH,KH.HOTEN,KH.SDT,DH.NGAYLAP," +
                "DH.PHIVANCHUYEN" +
                " WHERE DH.TINHTRANG = 0 and DH.MAKH = KH.MAKH";
            tbl_TaiXe_DHDN1 = Functions.GetDataToTable(sql);

            // đọc lần 2
            sql = "SELECT DH.HINHTHUCTHANHTOAN," +
                "DH.SOLUONGSP,DH.DIACHIGH,DH.TONGPHISP,DH.TONGPHI " +
                "FROM DONHANG DH,KHACHHANG KH" +
                " WHERE DH.TINHTRANG = 0 and DH.MAKH = KH.MAKH";
            tbl_TaiXe_DHDN2 = Functions.GetDataToTable(sql);

            dGV_TaiXe_DHDN.DataSource = tbl_TaiXe_DHDN1;

            // set Font cho tên cột
            dGV_TaiXe_DHDN.Font = new Font("Time New Roman", 13);
            dGV_TaiXe_DHDN.Columns[0].HeaderText = "Mã đơn hàng";
            dGV_TaiXe_DHDN.Columns[1].HeaderText = "Họ tên khách hàng";
            dGV_TaiXe_DHDN.Columns[2].HeaderText = "Số điện thoại";
            dGV_TaiXe_DHDN.Columns[3].HeaderText = "Ngày lập";
            dGV_TaiXe_DHDN.Columns[4].HeaderText = "Phí vận chuyển";
            //dGV_TaiXe_DHDN.Columns[5].HeaderText = "Hình thức thanh toán";
            //dGV_TaiXe_DHDN.Columns[6].HeaderText = "Số lượng";
            //dGV_TaiXe_DHDN.Columns[7].HeaderText = "Địa chỉ";
            //dGV_TaiXe_DHDN.Columns[8].HeaderText = "Tổng phí sản phẩm";
            //dGV_TaiXe_DHDN.Columns[9].HeaderText = "Tổng phí";
            // set Font cho dữ liệu hiển thị trong cột
            dGV_TaiXe_DHDN.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_TaiXe_DHDN.Columns[0].Width = 140;
            dGV_TaiXe_DHDN.Columns[1].Width = 100;
            dGV_TaiXe_DHDN.Columns[2].Width = 120;
            dGV_TaiXe_DHDN.Columns[3].Width = 100;
            dGV_TaiXe_DHDN.Columns[4].Width = 100;
            //dGV_TaiXe_DHDN.Columns[5].Width = 120;
            //dGV_TaiXe_DHDN.Columns[6].Width = 100;
            //dGV_TaiXe_DHDN.Columns[7].Width = 100;
            //dGV_TaiXe_DHDN.Columns[8].Width = 120;
            //dGV_TaiXe_DHDN.Columns[9].Width = 100;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_TaiXe_DHDN.AllowUserToAddRows = false;
            dGV_TaiXe_DHDN.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void DonHangDaNhan_TX_Load(object sender, EventArgs e)
        {
            LoadData_CTHD();
        }
    }
}
