namespace HQTCSDL
{
    partial class ChiNhanh_ThuocHopDong
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dGV_chinhanhthuochopdong = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_chinhanhthuochopdong)).BeginInit();
            this.SuspendLayout();
            // 
            // dGV_chinhanhthuochopdong
            // 
            this.dGV_chinhanhthuochopdong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_chinhanhthuochopdong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGV_chinhanhthuochopdong.Location = new System.Drawing.Point(0, 0);
            this.dGV_chinhanhthuochopdong.Name = "dGV_chinhanhthuochopdong";
            this.dGV_chinhanhthuochopdong.RowHeadersWidth = 51;
            this.dGV_chinhanhthuochopdong.RowTemplate.Height = 24;
            this.dGV_chinhanhthuochopdong.Size = new System.Drawing.Size(838, 462);
            this.dGV_chinhanhthuochopdong.TabIndex = 7;
            // 
            // ChiNhanh_ThuocHopDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 462);
            this.Controls.Add(this.dGV_chinhanhthuochopdong);
            this.Name = "ChiNhanh_ThuocHopDong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ChiNhanh_ThuocHopDong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_chinhanhthuochopdong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dGV_chinhanhthuochopdong;
    }
}