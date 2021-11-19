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
    public partial class DSTaiKHoan_admin : Form
    {
        public DSTaiKHoan_admin()
        {
            InitializeComponent();
        }

        private void btn_themTK_admin_Click(object sender, EventArgs e)
        {
            Form_ThemTK_admin form_themtk = new Form_ThemTK_admin();
            form_themtk.StartPosition = FormStartPosition.CenterScreen;
            form_themtk.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
