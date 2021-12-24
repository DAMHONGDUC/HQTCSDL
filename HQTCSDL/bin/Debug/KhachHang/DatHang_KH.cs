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
    public partial class DatHang_KH : Form
    {
        
        private string MAACC;
        private string maDT;
        private string maSP;
        private string tenSP;
        private string giaBan;
        private string soluongmua;
        public DatHang_KH(string MAACC,string maDT, string maSP, string tenSP, string giaBan, string soluongmua)
        {
            this.MAACC = MAACC;
            this.maDT = maDT;
            this.maSP = maSP;
            this.tenSP = tenSP;
            this.giaBan = giaBan;
            this.soluongmua = soluongmua;
            InitializeComponent();
        }
      
        private void btn_Back_DH_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void DatHang_KH_Load(object sender, EventArgs e)
        {
            txtBox_TenSP_KH_dh.Text = tenSP;
            txtBox_GIABAN_KH_dh.Text = giaBan;
            textBox_SL_KH_MH.Text = soluongmua;
            float temp = Int32.Parse(soluongmua) * float.Parse(giaBan);
            txtBox_TONGTIEN_KH_dh.Text = temp.ToString();

            string sql = "SELECT KH.DIACHI FROM KHACHHANG KH WHERE KH.MAACC = '" + MAACC + "'";
            string diachi = Functions.GetFieldValues(sql);
            comboBox_HTTT_KH_dh.Items.Add("Tiền mặt");
            comboBox_HTTT_KH_dh.Items.Add("Ví điện tử");
            comboBox_HTTT_KH_dh.Items.Add("Thẻ ngân hàng");
            textBox_DiChi_DH_KH.Text = diachi;
            txtBox_PHIVANCHUYEN_KH_dh.Text = "30000";

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_DH_Click(object sender, EventArgs e)
        {
            int count = 0;
            string sql1 = "SELECT COUNT(*) FROM DONHANG";
            string temp = Functions.GetFieldValues(sql1);
            count = Int32.Parse(temp) + 1;
            string maDH;
            if(count < 10)
            {
                maDH = "DH00" + count;
            }
            else if(count < 100)
            {
                maDH = "DH0" + count;
            }
            else
            {
                maDH = "DH" + count;
            }

            
            if(comboBox_HTTT_KH_dh.Text.Trim().Length == 0 || textBox_DiChi_DH_KH.Text.Trim().Length ==0
                || textBox_SL_KH_MH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ dữ liệu !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DateTime now = DateTime.Now;
            string httt="";
            if(comboBox_HTTT_KH_dh.Text.Trim().ToString() == "Tiền mặt")
            {
                httt = "0";
            }
            if(comboBox_HTTT_KH_dh.Text.Trim().ToString() == "Ví điện tử")
            {
                httt = "1";
            }
            if(comboBox_HTTT_KH_dh.Text.Trim().ToString() == "Thẻ ngân hàng")
            {
                httt = "2";
            }
            float tongtien = float.Parse(txtBox_TONGTIEN_KH_dh.Text.ToString()) + float.Parse(txtBox_PHIVANCHUYEN_KH_dh.Text.ToString());
            string sql2 = "INSERT INTO DONHANG(MADH,MADT,MAKH,SOLUONGSP,HINHTHUCTHANHTOAN, DIACHIGH, NGAYLAP, TONGPHISP, PHIVANCHUYEN, TONGPHI, TINHTRANG) " +
                "VALUES('" + maDH + "',"
                + "'" + maDT + "',"
                + "'" + MAACC + "',"
                + textBox_SL_KH_MH.Text.Trim().ToString() + ","
                + httt.ToString() + ","
                + "N'" + textBox_DiChi_DH_KH.Text.Trim().ToString() + "',"
                + "'" + now.ToString() + "',"
                + txtBox_TONGTIEN_KH_dh.Text.ToString() + ","
                + txtBox_PHIVANCHUYEN_KH_dh.Text.ToString() + ","
                + tongtien + ","
                + "0" + ")";
            Functions.RunSQL(sql2);

            
            string slq3 = "INSERT INTO CT_DONHANG(MADT, MADH, MASP, SOLUONG, THANHTIEN) "
                + "VALUES('" + maDT + "','" + maDH +"','" + maSP +"',"
                + textBox_SL_KH_MH.Text.Trim().ToString() + ","
                + txtBox_TONGTIEN_KH_dh.Text.ToString() + ")";

            Functions.RunSQL(slq3);

            MessageBox.Show("Đặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string sql4 = "Sp_KH_MUASP '" + maSP + "'," + textBox_SL_KH_MH.Text.Trim().ToString();
            Functions.RunSQL(sql4);
            this.Close();
        }
    }
}
