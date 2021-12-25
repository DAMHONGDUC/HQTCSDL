using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace HQTCSDL
{
    public partial class HopDongChuaDuyet_NV : Form
    {
        string MANV;
        DataTable tbl_HDCD;
        string return_value_DuyetHD;
        string return_value_LoaiBoHD;
        public HopDongChuaDuyet_NV(string manv)
        {
            InitializeComponent();
            MANV = manv;
        }

        private void Reset_value()
        {
            dTP_ngaylap_HHCDNV.CustomFormat = "";
            txtBox_mahd_HHCDNV.Text = "";
            txtBox_slchinhanh_HHCDNV.Text = "";
            dTP_ngaylap_HHCDNV.Text = "";
            txtBox_madt_HHCDNV.Text = "";
            txtBox_tendt_HHCDNV.Text = "";
            txtBox_nguoidaidien_HHCDNV.Text = "";
            txtBox_pthoahong_HHCDNV.Text = "";
            txtBox_thoihanhd_HHCDNV.Text = "";
        }
        private string Get_ngayketthuc(string ngaybd, string thoihanhd) // set up ngày kết thúc dựa trên ngày bắt đầu và thời hạn hợp đồng
        {
            string kq = "";
            string[] elements = ngaybd.Split('-');
            string[] elements2 = elements[2].Split(' ');
            int year = Int32.Parse(elements[0]);         
            int year_end = year + Int32.Parse(thoihanhd);

            kq = elements[1] + "-" + elements2[0] + "-" + year_end.ToString();
            return kq;
        }

        private void LoadData_HDCD() //dữ liệu vào DataGridView
        {        
            string sql = "SELECT HD.MAHD, HD.MADT, DT.TENDT, DT.NGUOIDAIDIEN, " +
                "HD.SLCHINHANH, HD.PTHOAHONG, HD.NGAYLAP, " +
                "HD.THOIHANHD" +
                " FROM HOPDONG HD, DOITAC DT" +
                " WHERE HD.DADUYET = 0 " +
                "AND HD.MADT = DT.MADT";
            tbl_HDCD = Functions.GetDataToTable(sql);
            dGV_HDCD_NV.DataSource = tbl_HDCD;

            // set Font cho tên cột
            dGV_HDCD_NV.Font = new Font("Time New Roman", 13);
            dGV_HDCD_NV.Columns[0].HeaderText = "Mã hợp đồng";
            dGV_HDCD_NV.Columns[1].HeaderText = "Mã đối tác";
            dGV_HDCD_NV.Columns[2].HeaderText = "Tên đối tác";
            dGV_HDCD_NV.Columns[3].HeaderText = "Người đại diện";
            dGV_HDCD_NV.Columns[4].HeaderText = "Số lượng chi nhánh";
            dGV_HDCD_NV.Columns[5].HeaderText = "Phần trăm hoa hồng";
            dGV_HDCD_NV.Columns[6].HeaderText = "Ngày lập";
            dGV_HDCD_NV.Columns[7].HeaderText = "Thời hạn hợp đồng";

            // set Font cho dữ liệu hiển thị trong cột
            dGV_HDCD_NV.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_HDCD_NV.Columns[0].Width = 200;
            dGV_HDCD_NV.Columns[1].Width = 200;
            dGV_HDCD_NV.Columns[2].Width = 200;
            dGV_HDCD_NV.Columns[3].Width = 200;
            dGV_HDCD_NV.Columns[4].Width = 200;
            dGV_HDCD_NV.Columns[5].Width = 200;
            dGV_HDCD_NV.Columns[6].Width = 200;
            dGV_HDCD_NV.Columns[7].Width = 200;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_HDCD_NV.AllowUserToAddRows = false;
            dGV_HDCD_NV.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
    
        private void HopDongChuaDuyet_NV_Load(object sender, EventArgs e)
        {
            dTP_ngaylap_HHCDNV.CustomFormat = " ";
            LoadData_HDCD();
        }

        private void dGV_HDCD_NV_Click(object sender, EventArgs e) // xử lí khi click vào datagridview
        {
            //Nếu không có dữ liệu
            if (tbl_HDCD.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục 
            dTP_ngaylap_HHCDNV.CustomFormat = "yyyy-MM-dd";
            txtBox_mahd_HHCDNV.Text = dGV_HDCD_NV.CurrentRow.Cells["MAHD"].Value.ToString();
            txtBox_slchinhanh_HHCDNV.Text = dGV_HDCD_NV.CurrentRow.Cells["SLCHINHANH"].Value.ToString();
            dTP_ngaylap_HHCDNV.Text = dGV_HDCD_NV.CurrentRow.Cells["NGAYLAP"].Value.ToString();
            txtBox_madt_HHCDNV.Text = dGV_HDCD_NV.CurrentRow.Cells["MADT"].Value.ToString();
            txtBox_tendt_HHCDNV.Text = dGV_HDCD_NV.CurrentRow.Cells["TENDT"].Value.ToString();
            txtBox_nguoidaidien_HHCDNV.Text = dGV_HDCD_NV.CurrentRow.Cells["NGUOIDAIDIEN"].Value.ToString();
            txtBox_pthoahong_HHCDNV.Text = dGV_HDCD_NV.CurrentRow.Cells["PTHOAHONG"].Value.ToString();
            txtBox_thoihanhd_HHCDNV.Text = dGV_HDCD_NV.CurrentRow.Cells["THOIHANHD"].Value.ToString();
        }

        private void Run_SP_DuyetHopDong(string ngaybd, string ngaykt, string manv, string mahd)
        {
            SqlCommand cmd = new SqlCommand("Sp_NV_DuyetHopDong", Functions.Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // set kiểu dữ liệu
            cmd.Parameters.Add("@NGAYBATDAU", SqlDbType.Date);
            cmd.Parameters.Add("@NGAYKETTHUC", SqlDbType.Date);
            cmd.Parameters.Add("@MANV", SqlDbType.VarChar, 15);
            cmd.Parameters.Add("@MAHD", SqlDbType.VarChar, 15);

            var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            // set giá trị
            cmd.Parameters["@NGAYBATDAU"].Value = ngaybd;
            cmd.Parameters["@NGAYKETTHUC"].Value = ngaykt;
            cmd.Parameters["@MANV"].Value = manv;
            cmd.Parameters["@MAHD"].Value = mahd;

            cmd.ExecuteNonQuery();

            return_value_DuyetHD = returnParameter.Value.ToString();          
        }

        private void btn_duyethd_HHCDNV_Click(object sender, EventArgs e) // xử lí duyệt hợp đồng
        {
            // TH nếu chưa chọn hợp đồng nào 
            if (txtBox_mahd_HHCDNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn hợp đồng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // hỏi người dùng có muốn duyệt không
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn duyệt hợp đồng này không?" +
                "", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            // nếu đã thỏa hết các điều kiện 
            try
            {
                DateTime today = DateTime.Today; //yyyy - MM - dd                

                Run_SP_DuyetHopDong(today.ToString("MM/dd/yyyy"), Get_ngayketthuc(today.ToString(), txtBox_thoihanhd_HHCDNV.Text.Trim().ToString())
                    , MANV, txtBox_mahd_HHCDNV.Text.Trim().ToString());

                if (return_value_DuyetHD.Equals("0"))
                    MessageBox.Show("Duyệt hợp đồng thất bại!!!");
                else MessageBox.Show("Duyệt hợp đồng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LoadData_HDCD();
                Reset_value();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Duyệt hợp đồng Thất bại, mã lỗi: " + ex.Message); // This will display all the error in your statement.
            }
        }

        private void Run_SP_LoaiBoHopDong(string manv, string mahd)
        {
            SqlCommand cmd = new SqlCommand("Sp_NV_LoaiBoHopDong", Functions.Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;               

            // set kiểu dữ liệu        
            cmd.Parameters.Add("@MANV", SqlDbType.VarChar, 15);
            cmd.Parameters.Add("@MAHD", SqlDbType.VarChar, 15);

            var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;

            // set giá trị        
            cmd.Parameters["@MANV"].Value = manv;
            cmd.Parameters["@MAHD"].Value = mahd;

            cmd.ExecuteNonQuery();
          
            return_value_LoaiBoHD = returnParameter.Value.ToString();
        }

        private void btn_loaibohd_HHCDNV_Click(object sender, EventArgs e) // xử lí loại bỏ hợp đồng
        {
            // TH nếu chưa chọn hợp đồng nào 
            if (txtBox_mahd_HHCDNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn hợp đồng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // hỏi người dùng có muốn loại bỏ hợp đồng này không
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn loại bỏ hợp đồng này không?" +
                "", "Thông báo", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            // nếu đã thỏa hết các điều kiện 
            try
            {            
                Run_SP_LoaiBoHopDong(MANV, txtBox_mahd_HHCDNV.Text.Trim().ToString());

                if (return_value_LoaiBoHD.Equals("0"))
                    MessageBox.Show("Loại bỏ hợp đồng thất bại!!!");
                else MessageBox.Show("Loại bỏ hợp đồng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                LoadData_HDCD();
                Reset_value();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loại bỏ hợp đồng Thất bại, mã lỗi: " + ex.Message); // This will display all the error in your statement.
            }
        }
    }
}