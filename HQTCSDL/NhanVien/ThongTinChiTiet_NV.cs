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
    public partial class ThongTinChiTiet_NV : Form
    {
        bool is_changePass = false;      
        DataTable tbl_TTCT_NV;
        string TENDANGNHAP;
        string MATKHAU;
        string MAACC_TTCTNV;
        string MANV;

        public ThongTinChiTiet_NV(string tendangnhap, string matkhau)
        {
            InitializeComponent();           
            TENDANGNHAP = tendangnhap;
            MATKHAU = matkhau;

            string sql = "SELECT NV.MANV " +
                "FROM NHANVIEN NV, ACCOUNT A " +
                "WHERE A.TENDANGNHAP = '" + TENDANGNHAP + "' " +
                "AND A.MATKHAU = '" + MATKHAU + "' " +
                "AND A.MAACC = NV.MAACC";
            MANV = Functions.GetFieldValues(sql);
        }

        private void Init_value1()
        {
            txtBox_tendangnhap_TTCTNV.Enabled = false;
            txtBox_mk_TTCTNV.Enabled = false;
            txtBox_mkmoi_TTCTNV.Enabled = false;
            txtBox_xacnhanmkmoi_TTCTNV.Enabled = false;
            btn_show_xacnhanmkmoi_CTTTNV.Enabled = false;
            btn_show_mkmoi_CTTTNV.Enabled = false;
            btn_show_mk_CTTTNV.Enabled = false;
        }

        private void Init_value2()
        {
            txtBox_mk_TTCTNV.Enabled = true;
            txtBox_mkmoi_TTCTNV.Enabled = true;
            txtBox_xacnhanmkmoi_TTCTNV.Enabled = true;
            btn_show_xacnhanmkmoi_CTTTNV.Enabled = true;
            btn_show_mkmoi_CTTTNV.Enabled = true;
            btn_show_mk_CTTTNV.Enabled = true;
        }

        private void LoadData()
        {
            // xử lí lấy dữ liệu
            string sql = "SELECT A.TENDANGNHAP, A.MATKHAU, NV.TENNV, NV.DIACHI, NV.SDT, NV.EMAIL, A.MAACC " +               
                "FROM ACCOUNT A, NHANVIEN NV " +
                "WHERE A.TENDANGNHAP = '" + TENDANGNHAP + "' " +
                "AND A.MATKHAU =  '" + MATKHAU + "' " +
                "AND A.MAACC = NV.MAACC";

            tbl_TTCT_NV = Functions.GetDataToTable(sql);

            if (tbl_TTCT_NV.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else 
            {
                txtBox_tendangnhap_TTCTNV.Text = tbl_TTCT_NV.Rows[0].Field<string>(0).Trim();             
                txtBox_hoten_TTCTNV.Text = tbl_TTCT_NV.Rows[0].Field<string>(2).Trim();
                txtBox_diachi_TTCTNV.Text = tbl_TTCT_NV.Rows[0].Field<string>(3).Trim();
                txtBox_sdt_TTCTNV.Text = tbl_TTCT_NV.Rows[0].Field<string>(4).Trim();
                txtBox_email_TTCTNV.Text = tbl_TTCT_NV.Rows[0].Field<string>(5).Trim();
                MAACC_TTCTNV = tbl_TTCT_NV.Rows[0].Field<string>(6).Trim();
            }
        }

        private void ThongTinChiTiet_NV_Load(object sender, EventArgs e)
        {
            Init_value1();
            LoadData();
        }

        private void btn_capnhatmk_TTCTNV_Click(object sender, EventArgs e)
        {
            Init_value2();
            is_changePass = true;
        }

        private void Run_SP_NV_DoiMK(string MAACC_TTCTNV, string matkhaumoi)
        {
            SqlCommand cmd = new SqlCommand("Sp_NV_DoiMK", Functions.Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // set kiểu dữ liệu
            cmd.Parameters.Add("@MAACC", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@MATKHAU", SqlDbType.VarChar, 50);        

            // set giá trị
            cmd.Parameters["@MAACC"].Value = MAACC_TTCTNV;
            cmd.Parameters["@MATKHAU"].Value = matkhaumoi;

            cmd.ExecuteNonQuery();
        }

        private void Run_SP_NV_DoiThongTinTK(string manv, string tennv, string sdt, string diachi, string email)
        {
            SqlCommand cmd = new SqlCommand("Sp_NV_DoiThongTinTK", Functions.Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // set kiểu dữ liệu
            cmd.Parameters.Add("@MANV", SqlDbType.VarChar, 15);
            cmd.Parameters.Add("@TENNV", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 15);
            cmd.Parameters.Add("@DIACHI", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar, 50);         

            // set giá trị
            cmd.Parameters["@MANV"].Value = manv;
            cmd.Parameters["@TENNV"].Value = tennv;
            cmd.Parameters["@SDT"].Value = sdt;
            cmd.Parameters["@DIACHI"].Value = diachi; 
            cmd.Parameters["@EMAIL"].Value = email;

            cmd.ExecuteNonQuery();
        }

        private void btn_luu_TTCTNV_Click(object sender, EventArgs e)
        {
            // TH người dùng chưa nhập đầy đủ dữ liệu chưa
            if (txtBox_hoten_TTCTNV.Text.Trim().Length == 0 | txtBox_diachi_TTCTNV.Text.Trim().Length == 0 |
                txtBox_sdt_TTCTNV.Text.Trim().Length == 0 | txtBox_email_TTCTNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn cần phải nhập đầy đủ dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (is_changePass)
            {
                if (txtBox_mk_TTCTNV.Text.Trim().Length == 0 |
                    txtBox_mkmoi_TTCTNV.Text.Trim().Length == 0 | txtBox_xacnhanmkmoi_TTCTNV.Text.Trim().Length == 0 )
                {
                    MessageBox.Show("Bạn cần phải nhập đầy đủ dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!txtBox_mk_TTCTNV.Text.Trim().Equals(tbl_TTCT_NV.Rows[0].Field<string>(1).Trim()))
                {
                    MessageBox.Show("Sai mật khẩu hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!txtBox_mkmoi_TTCTNV.Text.Trim().Equals(txtBox_xacnhanmkmoi_TTCTNV.Text.Trim()))
                {
                    MessageBox.Show("Xác nhận mật khẩu chưa đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            // nếu đã thỏa hết các điều kiện ở trên
            try
            {
                string sql = "";
                if (is_changePass)
                    Run_SP_NV_DoiMK(MAACC_TTCTNV, txtBox_mkmoi_TTCTNV.Text.Trim());
               
                Run_SP_NV_DoiThongTinTK(MANV, txtBox_hoten_TTCTNV.Text.Trim(),
                    txtBox_sdt_TTCTNV.Text.Trim(), txtBox_diachi_TTCTNV.Text.Trim(),
                    txtBox_email_TTCTNV.Text.Trim());

                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtBox_mk_TTCTNV.Text = "";
                txtBox_mkmoi_TTCTNV.Text = "";
                txtBox_xacnhanmkmoi_TTCTNV.Text = "";
                Init_value1();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thông tin thất bại, mã lỗi: " + ex.Message); // This will display all the error in your statement.
            }        
        }

        private void btn_show_mk_CTTTNV_Click(object sender, EventArgs e)
        {
            if (btn_show_mk_CTTTNV.Text == "Show")
            {
                txtBox_mk_TTCTNV.UseSystemPasswordChar = false;
                btn_show_mk_CTTTNV.Text = "Hide";
            }
            else if (btn_show_mk_CTTTNV.Text == "Hide")
            {
                txtBox_mk_TTCTNV.UseSystemPasswordChar = true;
                btn_show_mk_CTTTNV.Text = "Show";
            }

        }

        private void btn_show_mkmoi_CTTTNV_Click(object sender, EventArgs e)
        {
            if (btn_show_mkmoi_CTTTNV.Text == "Show")
            {
                txtBox_mkmoi_TTCTNV.UseSystemPasswordChar = false;
                btn_show_mkmoi_CTTTNV.Text = "Hide";
            }
            else if (btn_show_mkmoi_CTTTNV.Text == "Hide")
            {
                txtBox_mkmoi_TTCTNV.UseSystemPasswordChar = true;
                btn_show_mkmoi_CTTTNV.Text = "Show";
            }
        }

        private void btn_show_xacnhanmkmoi_CTTTNV_Click(object sender, EventArgs e)
        {
            if (btn_show_xacnhanmkmoi_CTTTNV.Text == "Show")
            {
                txtBox_xacnhanmkmoi_TTCTNV.UseSystemPasswordChar = false;
                btn_show_xacnhanmkmoi_CTTTNV.Text = "Hide";
            }
            else if (btn_show_xacnhanmkmoi_CTTTNV.Text == "Hide")
            {
                txtBox_xacnhanmkmoi_TTCTNV.UseSystemPasswordChar = true;
                btn_show_xacnhanmkmoi_CTTTNV.Text = "Show";
            }
        }
    }
}
