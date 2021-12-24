using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HQTCSDL
{
    public partial class DS_DonHang_TX : Form
    {
        DataTable tbl_TaiXe_DSDH;
        public DS_DonHang_TX()
        {
            InitializeComponent();
        }

        private void LoadData_DSDH()//dữ liệu vào DataGridView
        {
            string sql = "SELECT DH.MADH, KH.HOTEN, KH.SDT, DH.NGAYLAP, DH.PHIVANCHUYEN, " +
                "DH.SOLUONGSP,DH.DIACHIGH, DH.TONGPHISP, DH.TONGPHI, DH.HINHTHUCTHANHTOAN " +
                "FROM DONHANG DH,KHACHHANG KH WHERE DH.TINHTRANG = 0 and DH.MAKH = KH.MAKH";

            
            tbl_TaiXe_DSDH = Functions.GetDataToTable(sql);
            dGV_TaiXe_DSDH.DataSource = tbl_TaiXe_DSDH;


            
            // set Font cho tên cột
            dGV_TaiXe_DSDH.Font = new Font("Time New Roman", 13);
            dGV_TaiXe_DSDH.Columns[0].HeaderText = "Mã đơn hàng";
            dGV_TaiXe_DSDH.Columns[1].HeaderText = "Khách hàng";
            dGV_TaiXe_DSDH.Columns[2].HeaderText = "Số điện thoại";
            dGV_TaiXe_DSDH.Columns[3].HeaderText = "Ngày lập";
            dGV_TaiXe_DSDH.Columns[4].HeaderText = "Phí vận chuyển";
            dGV_TaiXe_DSDH.Columns[5].HeaderText = "Số lượng";
            dGV_TaiXe_DSDH.Columns[6].HeaderText = "Địa chỉ giao hàng";
            dGV_TaiXe_DSDH.Columns[7].HeaderText = "Tổng phí SP";
            dGV_TaiXe_DSDH.Columns[8].HeaderText = "Tổng";
            dGV_TaiXe_DSDH.Columns[9].HeaderText = "Hình thức thanh toán";
            // set Font cho dữ liệu hiển thị trong cột
            dGV_TaiXe_DSDH.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_TaiXe_DSDH.Columns[0].Width = 140;
            dGV_TaiXe_DSDH.Columns[1].Width = 140;
            dGV_TaiXe_DSDH.Columns[2].Width = 140;
            dGV_TaiXe_DSDH.Columns[3].Width = 140;
            dGV_TaiXe_DSDH.Columns[4].Width = 180;
            dGV_TaiXe_DSDH.Columns[5].Width = 120;
            dGV_TaiXe_DSDH.Columns[6].Width = 200;
            dGV_TaiXe_DSDH.Columns[7].Width = 170;
            dGV_TaiXe_DSDH.Columns[8].Width = 110;
            dGV_TaiXe_DSDH.Columns[9].Width = 200;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_TaiXe_DSDH.AllowUserToAddRows = false;
            dGV_TaiXe_DSDH.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void DS_DonHang_TX_Load(object sender, System.EventArgs e)
        {
            LoadData_DSDH();
        }

        private void dGV_TaiXe_DSDH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Nếu không có dữ liệu
            if (tbl_TaiXe_DSDH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục            
            txtBox_maDH_DSDH.Text = dGV_TaiXe_DSDH.CurrentRow.Cells["MADH"].Value.ToString();
            txtBox_tenKH_DSDH.Text = dGV_TaiXe_DSDH.CurrentRow.Cells["HOTEN"].Value.ToString();
            txtBox_SDT_DSDH.Text = dGV_TaiXe_DSDH.CurrentRow.Cells["SDT"].Value.ToString();
            textBox_NgayLap_DSDH.Text = dGV_TaiXe_DSDH.CurrentRow.Cells["NGAYLAP"].Value.ToString();
            textBox_PhiVanChuyen_DSDH.Text = dGV_TaiXe_DSDH.CurrentRow.Cells["PHIVANCHUYEN"].Value.ToString();
            txtBox_SLSP_DSDH.Text = dGV_TaiXe_DSDH.CurrentRow.Cells["SOLUONGSP"].Value.ToString();
            txtBox_DiaChiGH_DSDH.Text = dGV_TaiXe_DSDH.CurrentRow.Cells["DIACHIGH"].Value.ToString();
            textBox_PhiSanPham_DSDH.Text = dGV_TaiXe_DSDH.CurrentRow.Cells["TONGPHISP"].Value.ToString();
            textBox_TongPhi_DSDH.Text = dGV_TaiXe_DSDH.CurrentRow.Cells["TONGPHI"].Value.ToString();
            string temp = dGV_TaiXe_DSDH.CurrentRow.Cells["HINHTHUCTHANHTOAN"].Value.ToString();



            switch (temp)
            {
                case "0":
                    textBox_HTTT_DSDH.Text = "Tiền mặt";
                    break;
                case "1":
                    textBox_HTTT_DSDH.Text = "Tiền mặt";
                    break;
                case "2":
                    textBox_HTTT_DSDH.Text = "Tiền mặt";
                    break;

                default:
                    break;
            }
        }
    }
}

        