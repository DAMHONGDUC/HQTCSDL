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
    public partial class DS_SanPham_KH : Form
    {
        public DS_SanPham_KH()
        {
            InitializeComponent();
        }

        private void btn_Back_SP_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Muangay_KH_xemSP_Click(object sender, EventArgs e)
        {
            DatHang_KH dathang_kh = new DatHang_KH();
            dathang_kh.Show();
        }
    }
}
