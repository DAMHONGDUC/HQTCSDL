using System;
using System.Windows.Forms;

namespace HQTCSDL
{
    public partial class Form_DangKi : Form
    {
        public Form_DangKi()
        {
            InitializeComponent();
        }

        // xử lí quay lại
        private void btn_quaylai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // xử lí đăng kí
        private void btn_dangki_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đăng kí thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        // xử lí cho user chỉ được chọn 1 loại account
        private void cB_DT_CheckedChanged(object sender, EventArgs e)
        {          
            if (cB_DT.Checked == true)
            {
                cB_KH.Checked = false;
                cB_TX.Checked = false;
            }
        }

        private void cB_KH_CheckedChanged(object sender, EventArgs e)
        {
            if (cB_KH.Checked == true)
            {
                cB_DT.Checked = false;
                cB_TX.Checked = false;
            }
        }

        private void cB_TX_CheckedChanged(object sender, EventArgs e)
        {
            if (cB_TX.Checked == true)
            {
                cB_KH.Checked = false;
                cB_DT.Checked = false;
            }
        }
    }
}
