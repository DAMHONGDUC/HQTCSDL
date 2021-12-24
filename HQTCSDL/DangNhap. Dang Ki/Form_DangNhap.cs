using System;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace HQTCSDL
{
    public partial class Form_DangNhap : Form
    {
        public int user_type = -2;
        string MAACC;
        string LOAIACC;
        string tendangnhap;
        string matkhau;

        Thread t;
        public Form_DangNhap()
        {
            InitializeComponent();
        }


        private void resetvalue_DN()
        {
            txtBox_tendangnhap.Text = "";
            txtBox_matkhau.Text = "";
        }
    
        private void Form_Login_Load(object sender, EventArgs e)
        {
            //Mở kết nối
            //Functions.Connect(user_type);
            Functions.Connect(Functions.get_ConnectString(user_type));

            resetvalue_DN();         
        }

        private void Run_SP_DangNhap()
        {                  
            SqlCommand cmd = new SqlCommand("Sp_DangNhap", Functions.Con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // set kiểu dữ liệu
            cmd.Parameters.Add("@TENDANGNHAP", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@MATKHAU", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@MAACC", SqlDbType.VarChar, 15).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@LOAIACC", SqlDbType.Int).Direction = ParameterDirection.Output;

            // set giá trị
            cmd.Parameters["@TENDANGNHAP"].Value = tendangnhap;
            cmd.Parameters["@MATKHAU"].Value = matkhau;

            cmd.ExecuteNonQuery();

            MAACC = Convert.ToString(cmd.Parameters["@MAACC"].Value);
            LOAIACC = Convert.ToString(cmd.Parameters["@LOAIACC"].Value);
        }
     
        // xử lí đăng nhập
        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            tendangnhap = txtBox_tendangnhap.Text.Trim().ToString();
            matkhau = txtBox_matkhau.Text.Trim().ToString();

            // nếu chưa có dữ liệu 
            if (tendangnhap.Length == 0 | matkhau.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // chạy SP đăng nhập, lấy MAACC, LOAIACC
            Run_SP_DangNhap();
          
            // nếu tên đăng nhập hoặc mật khẩu sai
            if (LOAIACC.Length == 0)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //resetvalue_DN();
                return;
            }
           
            // chuyển loại acc sang int
            user_type = Int32.Parse(LOAIACC);

            // nếu acc này bị khóa
            if (user_type == -1)
            {
                MessageBox.Show("Tài khoản này đã bị khóa !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
          
            // ngắt kết nối vô danh
            Functions.Disconnect();
            
            // kết nối với database tương ứng với loại acc
            Functions.Connect(Functions.get_ConnectString(user_type));

            // mở giao diện tương ứng từng loại acc                 
            this.Close();
            t = new Thread(open_FormMain);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();           
        }

        // xử lí mở form tương ứng từng loại acc      
        public void open_FormMain(object obj)
        {
            switch (user_type)
            {
                case 0:
                    {
                        Application.Run(new FormMain_DoiTac(MAACC));
                        break;
                    }
                case 1:
                    {
                        Application.Run(new FormMain_KhachHang(MAACC));
                        break;
                    }
                case 2:
                    {
                        Application.Run(new FormMain_TaiXe());
                        break;
                    }
                case 3:
                    {
                        Application.Run(new FormMain_NhanVien(tendangnhap,matkhau));
                        break;
                    }
                case 4:
                    {
                        Application.Run(new FormMain_Admin());
                        break;
                    }
            }
        }

        // chuyển qua màn hình đăng kí
        private void btn_dangki_Click(object sender, EventArgs e)
        {           
            Form_DangKi form_signup = new Form_DangKi();
            form_signup.StartPosition = FormStartPosition.CenterParent;       
            form_signup.ShowDialog();
        }
          
        // loại bỏ sự kiện enter 
        private void txtBox_tendangnhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        // loại bỏ sự kiện enter 
        private void txtBox_matkhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
