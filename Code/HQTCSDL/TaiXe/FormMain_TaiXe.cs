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
    public partial class FormMain_TaiXe : Form
    {
        Thread t;
        public FormMain_TaiXe()
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
            panelChildForm_TX.Controls.Add(childForm);
            panelChildForm_TX.Tag = childForm;
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

        private void btn_dangxuat_TX_Click(object sender, EventArgs e)
        {
            this.Close();
            t = new Thread(open_FormDangNhap);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        // xử lí khi load form thì hiện tab tài khoản
        private void FormMain_TaiXe_Load(object sender, EventArgs e)
        {
            btn_taikhoan_TX.PerformClick();
        }

        // xử lí thoát
        private void btn_thoat_TX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // chức năng xem DS đơn hàng
        private void btn_DSdonhang_TX_Click(object sender, EventArgs e)
        {
            openChildForm(new DS_DonHang_TX());
            ActivateButton(sender);
        }

        // chức năng xem đơn hàng đã nhận
        private void btn_donhangdanhan_TX_Click(object sender, EventArgs e)
        {
            openChildForm(new DonHangDaNhan_TX());
            ActivateButton(sender);
        }

        // chức năng thống kê
        private void btn_thongke_TK_Click(object sender, EventArgs e)
        {
            openChildForm(new ThongKe_TX());
            ActivateButton(sender);
        }

        // chức năng tài khoản
        private void btn_taikhoan_TX_Click(object sender, EventArgs e)
        {
            openChildForm(new ThongTinChiTiet_TX());
            ActivateButton(sender);
        }        
    }
}
