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
        
        private void LoadData_DHDN()//dữ liệu vào DataGridView
        {
            string sql1 = "SELECT DH.MADH,KH.HOTEN,KH.SDT,DH.NGAYLAP,DH.PHIVANCHUYEN," +
                "DH.HINHTHUCTHANHTOAN,DH.SOLUONGSP,DH.DIACHIGH,DH.TONGPHISP,DH.TONGPHI " +
                "FROM DONHANG DH,KHACHHANG KH WHERE DH.TINHTRANG = 1 and DH.MAKH = KH.MAKH";
            string sql2 = "SELECT DH.MADH,KH.HOTEN,KH.SDT,DH.NGAYLAP," +
                "DH.PHIVANCHUYEN," +
                "DH.SOLUONGSP,DH.DIACHIGH,DH.TONGPHI " +
                "FROM DONHANG DH,KHACHHANG KH" +
                " WHERE DH.TINHTRANG = 1 and DH.MAKH = KH.MAKH";
            tbl_TaiXe_DHDN2 = Functions.GetDataToTable(sql2);
            dGV_TaiXe_DHDN.DataSource = tbl_TaiXe_DHDN2;

            // set Font cho tên cột
            dGV_TaiXe_DHDN.Font = new Font("Time New Roman", 13);
            dGV_TaiXe_DHDN.Columns[0].HeaderText = "Mã đơn hàng";
            dGV_TaiXe_DHDN.Columns[1].HeaderText = "Khách hàng";
            dGV_TaiXe_DHDN.Columns[2].HeaderText = "Số điện thoại";
            dGV_TaiXe_DHDN.Columns[3].HeaderText = "Ngày lập";
            dGV_TaiXe_DHDN.Columns[4].HeaderText = "Phí vận chuyển";
            dGV_TaiXe_DHDN.Columns[5].HeaderText = "Số lượng";
            dGV_TaiXe_DHDN.Columns[6].HeaderText = "Địa chỉ";
            dGV_TaiXe_DHDN.Columns[7].HeaderText = "Tổng phí";
            // set Font cho dữ liệu hiển thị trong cột
            dGV_TaiXe_DHDN.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_TaiXe_DHDN.Columns[0].Width = 140;
            dGV_TaiXe_DHDN.Columns[1].Width = 140;
            dGV_TaiXe_DHDN.Columns[2].Width = 140;
            dGV_TaiXe_DHDN.Columns[3].Width = 140;
            dGV_TaiXe_DHDN.Columns[4].Width = 160;
            dGV_TaiXe_DHDN.Columns[5].Width = 120;
            dGV_TaiXe_DHDN.Columns[6].Width = 100;
            dGV_TaiXe_DHDN.Columns[7].Width = 110;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_TaiXe_DHDN.AllowUserToAddRows = false;
            dGV_TaiXe_DHDN.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void DonHangDaNhan_TX_Load(object sender, EventArgs e)
        {
            LoadData_DHDN();
        }
    }
}
