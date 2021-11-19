using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HQTCSDL
{
    public partial class Form_DangNhap : Form
    {
        public int user_type = 4;
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
            Functions.Connect(Functions.get_ConnectString(user_type));

            resetvalue_DN();
            txtBox_matkhau.PasswordChar = '*';
        }
     
        // xử lí đăng nhập
        private void btn_dangnhap_Click(object sender, EventArgs e)
        {
            string tendangnhap = txtBox_tendangnhap.Text.Trim().ToString();
            string matkhau = txtBox_matkhau.Text.Trim().ToString();

            // nếu chưa có dữ liệu 
            if (tendangnhap.Length == 0 | matkhau.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // kiểm tra tên đăng nhập và mật khẩu
            string sql = "SELECT LOAIACC " +
                "FROM ACCOUNT " +
                "WHERE TENDANGNHAP = '" + tendangnhap + "' " +
                "AND MATKHAU = '" + matkhau + "'";
            String loaiacc = Functions.GetFieldValues(sql);

            // nếu tên đăng nhập hoặc mật khẩu sai
            if (loaiacc.Length == 0)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //resetvalue_DN();
                return;
            }
           
            // lấy loại acc
            user_type = Get_user_type(loaiacc);

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
                        Application.Run(new FormMain_DoiTac());
                        break;
                    }
                case 1:
                    {
                        Application.Run(new FormMain_KhachHang());
                        break;
                    }
                case 2:
                    {
                        Application.Run(new FormMain_NhanVien());
                        break;
                    }
                case 3:
                    {
                        Application.Run(new FormMain_TaiXe());
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

        // lấy loại user
        private int Get_user_type(string loaiacc)
        {
            int type = -2;
            
            switch (loaiacc)
            {
                case "-1":
                    {
                        type = -1;
                        break;
                    }

                case "0":
                    {
                        type = 0;
                        break;
                    }
                case "1":
                    {
                        type = 1;
                        break;
                    }
                case "2":
                    {
                        type = 2;
                        break;
                    }
                case "3":
                    {
                        type = 3;
                        break;
                    }
                case "4":
                    {
                        type = 4;
                        break;
                    }
            }
            return type;
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
