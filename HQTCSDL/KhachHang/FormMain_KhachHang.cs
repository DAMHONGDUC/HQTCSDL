using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace HQTCSDL
{
    public partial class FormMain_KhachHang : Form
    {
        Thread t;
        public FormMain_KhachHang()
        {
            InitializeComponent();
        }

        // mở 1 form con
        private Form activeform = null;
        private void openChildForm(Form childForm)
        {
            if (activeform != null)
                activeform.Close();
            activeform = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm_KH.Controls.Add(childForm);
            panelChildForm_KH.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // xử lí chuyển màu khi click vào button
        private Button currentButton;
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = ColorTranslator.FromHtml("#4169E1");
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(39, 39, 58);
                    previousBtn.ForeColor = Color.Gainsboro;
                }
            }
        }

        // xử lí đăng xuất + đăng nhập lại
        public void open_FormDangNhap(object obj)
        {
            Application.Run(new Form_DangNhap());
        }

        private void btn_dangxuat_KH_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_FormDangNhap);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        // xử lí khi load form thì hiện tab tài khoản
        private void FormMain_KhachHang_Load(object sender, EventArgs e)
        {         
            btn_taikhoan_KH.PerformClick();
        }

        // xử lí thoát
        private void btn_thoat_KH_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // chức năng đặt hàng
        private void btn_dathang_KH_Click(object sender, EventArgs e)
        {
            openChildForm(new DSDoiTac_KH());
            ActivateButton(sender);
        }

        // chức năng xem đơn hàng của tôi
        private void btn_donhangcuatoi_KH_Click(object sender, EventArgs e)
        {
            openChildForm(new DS_DonHang_KH());
            ActivateButton(sender);
        }

        // chức năng giỏ hàng
        private void btn_giohang_KH_Click(object sender, EventArgs e)
        {
            openChildForm(new DatHang_KH());
            ActivateButton(sender);
        }

        // chức năng tài khoản
        private void btn_taikhoan_KH_Click_1(object sender, EventArgs e)
        {
            openChildForm(new ThongTinChiTiet_KH());
            ActivateButton(sender);
        }

        
    }
}
