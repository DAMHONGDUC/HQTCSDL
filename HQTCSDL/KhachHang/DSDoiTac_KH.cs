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
    public partial class DSDoiTac_KH : Form
    {
        public DSDoiTac_KH()
        {
            InitializeComponent();
        }

        private void btn_XemSP_KH_Click(object sender, EventArgs e)
        {
            DS_SanPham_KH ds_sanpham = new DS_SanPham_KH();
            ds_sanpham.Show();
        }
    }
}
