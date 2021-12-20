﻿using System;
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
    public partial class ChiNhanh_DT : Form
    {
        DataTable tbl_DoiTac_CN;
        public ChiNhanh_DT()
        {
            InitializeComponent();
        }
        
        private void LoadData_CN()//dữ liệu vào DataGridView
        {
            string sql = "SELECT CN.TENCHINHANH, CN.DIACHI" +
                " FROM CHINHANH CN";
            tbl_DoiTac_CN = Functions.GetDataToTable(sql);
            dGV_DoiTac_CN.DataSource = tbl_DoiTac_CN;

            // set Font cho tên cột
            dGV_DoiTac_CN.Font = new Font("Time New Roman", 13);
            dGV_DoiTac_CN.Columns[0].HeaderText = "Tên chi nhánh";
            dGV_DoiTac_CN.Columns[1].HeaderText = "Địa chỉ";


            // set Font cho dữ liệu hiển thị trong cột
            dGV_DoiTac_CN.DefaultCellStyle.Font = new Font("Time New Roman", 12);

            // set kích thước cột
            dGV_DoiTac_CN.Columns[0].Width = 450;
            dGV_DoiTac_CN.Columns[1].Width = 450;

            //Không cho người dùng thêm dữ liệu trực tiếp
            dGV_DoiTac_CN.AllowUserToAddRows = false;
            dGV_DoiTac_CN.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ChiNhanh_DT_Load(object sender, EventArgs e)
        {
            LoadData_CN();
        }

        private void dGV_DoiTac_CN_Click(object sender, EventArgs e)
        {
            //Nếu không có dữ liệu
            if (tbl_DoiTac_CN.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // set giá trị cho các mục            
            txtBox_tenchinhanh_CN.Text = dGV_DoiTac_CN.CurrentRow.Cells["TENCHINHANH"].Value.ToString();
            txtBox_diachi_CN.Text = dGV_DoiTac_CN.CurrentRow.Cells["DIACHI"].Value.ToString();          
        }

        
    }
}
