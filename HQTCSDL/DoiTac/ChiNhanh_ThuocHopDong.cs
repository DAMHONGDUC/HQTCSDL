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
    public partial class ChiNhanh_ThuocHopDong : Form
    {
        public DataTable tbl_ChiNhanh_CNTHD;
        public ChiNhanh_ThuocHopDong()
        {
            InitializeComponent();
        }

        private void Load_Data_CNTH()
        {
            dGV_chinhanhthuochopdong.DataSource = tbl_ChiNhanh_CNTHD;

            // set Font cho tên cột
            dGV_chinhanhthuochopdong.Font = new Font("Time New Roman", 13);
            dGV_chinhanhthuochopdong.Columns[0].HeaderText = "Mã chi nhánh";
            dGV_chinhanhthuochopdong.Columns[1].HeaderText = "Tên chi nhánh";

            // set Font cho dữ liệu hiển thị trong cột
            dGV_chinhanhthuochopdong.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_chinhanhthuochopdong.Columns[0].Width = 285;
            dGV_chinhanhthuochopdong.Columns[1].Width = 290;
        }

        private void ChiNhanh_ThuocHopDong_Load(object sender, EventArgs e)
        {
            Load_Data_CNTH();
        }
    }
}
