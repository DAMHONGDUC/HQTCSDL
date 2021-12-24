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
        DataTable tbl_ChiNhanh_HD;
        public HopDong_DT()
        {
            InitializeComponent();
        }

        private void Reset_Data_HD()
        {
            dTP_ngaylap_HDDL.CustomFormat = " ";
            dTP_ngayketthuc_HDHL.CustomFormat = " ";
        }

        private string tinhtrang_HD(string s)
        {
            string kq = "Chưa duyệt";
            if (s.Equals("1")) kq = "Đã duyệt";
            else if (s.Equals("2")) kq = "Đã bị hủy";
            else if (s.Equals("3"))
            {
                textBox_thongbao_hopdong.Text = "Hợp đồng của bạn sắp hết hạn, bạn có muốn gia hạn không?";
                btn_giahan_HD.Enabled = true;
                kq = "Chờ gia hạn";
            }
            return kq;
        }

        private void LoadData_HD() // tải dữ liệu vào DataGridView
        {
            // xử lí lấy dữ liệu
            string sql = "SELECT HD.MAHD, DT.MADT, " +
                "DT.MASOTHUE, DT.NGUOIDAIDIEN, HD.DADUYET, " +
                "HD.NGAYLAP, HD.NGAYKETTHUC, DATEDIFF(DAY,HD.NGAYLAP,HD.NGAYKETTHUC) AS HIEULUC " +
                "FROM HOPDONG HD, DOITAC DT " +
                "WHERE HD.MADT = DT.MADT ";          
                     
            tbl_DoiTac_HD = Functions.GetDataToTable(sql);           
            dGV_DoiTac_HD.DataSource = tbl_DoiTac_HD;

            // set Font cho tên cột
            dGV_DoiTac_HD.Font = new Font("Time New Roman", 13);
            dGV_DoiTac_HD.Columns[0].HeaderText = "Mã hợp đồng";
            dGV_DoiTac_HD.Columns[1].HeaderText = "Mã đối tác";
            dGV_DoiTac_HD.Columns[2].HeaderText = "Mã số thuế";
            dGV_DoiTac_HD.Columns[3].HeaderText = "Người đại diện";
            dGV_DoiTac_HD.Columns[4].HeaderText = "Tình trạng";
            dGV_DoiTac_HD.Columns[5].HeaderText = "Ngày lập";
            dGV_DoiTac_HD.Columns[6].HeaderText = "Ngày kết thúc";


            // set Font cho dữ liệu hiển thị trong cột
            dGV_DoiTac_HD.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_DoiTac_HD.Columns[0].Width = 150;
            dGV_DoiTac_HD.Columns[1].Width = 150;
            dGV_DoiTac_HD.Columns[2].Width = 150;
            dGV_DoiTac_HD.Columns[3].Width = 150;
            dGV_DoiTac_HD.Columns[4].Width = 150;
            dGV_DoiTac_HD.Columns[5].Width = 150;
            dGV_DoiTac_HD.Columns[6].Width = 150;


            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_DoiTac_HD.AllowUserToAddRows = false;
            dGV_DoiTac_HD.EditMode = DataGridViewEditMode.EditProgrammatically;
        }


        private void HopDong_DT_Load(object sender, EventArgs e)
        {
            Reset_Data_HD();

            LoadData_HD();

            // ko cho người dùng nhấn nút gia hạn
            btn_giahan_HD.Enabled = false;
        }

        private void dGV_DoiTac_HD_Click(object sender, EventArgs e) // xử lí khi người dùng click vào 1 row trong dataGirdView
        {
            dTP_ngaylap_HDDL.CustomFormat = "yyyy-MM-dd";
            dTP_ngayketthuc_HDHL.CustomFormat = "yyyy-MM-dd";

            //Nếu không có dữ liệu
            if (tbl_DoiTac_HD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục    
            txtBox_masothue_HDDL.Text = dGV_DoiTac_HD.CurrentRow.Cells["MASOTHUE"].Value.ToString();
            txtBox_nguoidaidien_HDDL.Text = dGV_DoiTac_HD.CurrentRow.Cells["NGUOIDAIDIEN"].Value.ToString();
            txtBox_tinhtrang_HDDL.Text = tinhtrang_HD(dGV_DoiTac_HD.CurrentRow.Cells["DADUYET"].Value.ToString());
            dTP_ngaylap_HDDL.Text = dGV_DoiTac_HD.CurrentRow.Cells["NGAYLAP"].Value.ToString();
            dTP_ngayketthuc_HDHL.Text = dGV_DoiTac_HD.CurrentRow.Cells["NGAYKETTHUC"].Value.ToString();
            txtBox_thoigianhieuluc_HDDL.Text = dGV_DoiTac_HD.CurrentRow.Cells["HIEULUC"].Value.ToString();

            // tìm số chi nhánh ứng với hợp đồng được chọn
            string MAHD = dGV_DoiTac_HD.CurrentRow.Cells["MAHD"].Value.ToString();
            string sql = "SELECT CN.MACHINHANH, CN.TENCHINHANH " +
                "FROM CT_HOPDONG CTHD, CHINHANH CN " +
                "WHERE MAHD = '" + MAHD + "' " +
                "AND CTHD.MACHINHANH = CN.MACHINHANH";

            tbl_ChiNhanh_HD = Functions.GetDataToTable(sql);
            txtBox_sochinhanh_HDDL.Text = tbl_ChiNhanh_HD.Rows.Count.ToString();
        }

        private void btn_xemchinhanh_HD_Click(object sender, EventArgs e) // xử lí hiển thị ra tất cả các chi nhánh ứng với hợp đồng này
        {
            // TH nếu chưa chọn hợp đồng nào
            if (txtBox_sochinhanh_HDDL.Text.Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn hợp đồng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ChiNhanh_ThuocHopDong chinhanh_thuochopdong = new ChiNhanh_ThuocHopDong();
                chinhanh_thuochopdong.tbl_ChiNhanh_CNTHD = tbl_ChiNhanh_HD;
                chinhanh_thuochopdong.Show();
            }
            
        }
    }
}
