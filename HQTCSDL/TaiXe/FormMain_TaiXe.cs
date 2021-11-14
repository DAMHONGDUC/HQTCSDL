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
    public partial class FormMain_TaiXe : Form
    {
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

        private void btn_DSdonhang_TX_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void btn_donhangdanhan_TX_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void btn_thongke_TK_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void btn_taikhoan_TX_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
        }

        private void btn_thoat_TX_Click(object sender, EventArgs e)
        {          
            this.Close();
        }
    }
}
