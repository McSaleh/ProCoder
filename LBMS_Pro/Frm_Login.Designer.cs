namespace LBMS_Pro
{
    partial class Frm_Login
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.BT_Login = new System.Windows.Forms.Button();
            this.TB_Port = new System.Windows.Forms.TextBox();
            this.TB_ip = new System.Windows.Forms.TextBox();
            this.RTB_Result = new System.Windows.Forms.RichTextBox();
            this.TB_OnLinDevisc = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.DGV_Prof = new System.Windows.Forms.DataGridView();
            this.BT_GetProfile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Prof)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(476, 14);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(53, 26);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "بورت";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // BT_Login
            // 
            this.BT_Login.BackColor = System.Drawing.Color.Goldenrod;
            this.BT_Login.FlatAppearance.BorderSize = 0;
            this.BT_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_Login.ForeColor = System.Drawing.Color.White;
            this.BT_Login.Location = new System.Drawing.Point(29, 6);
            this.BT_Login.Name = "BT_Login";
            this.BT_Login.Size = new System.Drawing.Size(91, 34);
            this.BT_Login.TabIndex = 1;
            this.BT_Login.Text = "دخول";
            this.BT_Login.UseVisualStyleBackColor = false;
            this.BT_Login.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TB_Port
            // 
            this.TB_Port.AutoCompleteCustomSource.AddRange(new string[] {
            "8271",
            "8278"});
            this.TB_Port.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.TB_Port.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.TB_Port.Location = new System.Drawing.Point(401, 12);
            this.TB_Port.MaxLength = 4;
            this.TB_Port.Name = "TB_Port";
            this.TB_Port.Size = new System.Drawing.Size(71, 30);
            this.TB_Port.TabIndex = 2;
            // 
            // TB_ip
            // 
            this.TB_ip.Location = new System.Drawing.Point(533, 12);
            this.TB_ip.MaxLength = 15;
            this.TB_ip.Name = "TB_ip";
            this.TB_ip.Size = new System.Drawing.Size(167, 30);
            this.TB_ip.TabIndex = 3;
            this.TB_ip.Text = "10.0.0.1";
            // 
            // RTB_Result
            // 
            this.RTB_Result.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTB_Result.Location = new System.Drawing.Point(12, 74);
            this.RTB_Result.Name = "RTB_Result";
            this.RTB_Result.Size = new System.Drawing.Size(555, 167);
            this.RTB_Result.TabIndex = 4;
            this.RTB_Result.Text = "";
            // 
            // TB_OnLinDevisc
            // 
            this.TB_OnLinDevisc.Location = new System.Drawing.Point(573, 308);
            this.TB_OnLinDevisc.Multiline = true;
            this.TB_OnLinDevisc.Name = "TB_OnLinDevisc";
            this.TB_OnLinDevisc.Size = new System.Drawing.Size(91, 68);
            this.TB_OnLinDevisc.TabIndex = 5;
            // 
            // listBox1
            // 
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 22;
            this.listBox1.Location = new System.Drawing.Point(573, 74);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(254, 178);
            this.listBox1.TabIndex = 6;
            // 
            // DGV_Prof
            // 
            this.DGV_Prof.AllowUserToAddRows = false;
            this.DGV_Prof.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DGV_Prof.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Prof.BackgroundColor = System.Drawing.Color.White;
            this.DGV_Prof.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGV_Prof.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Crimson;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Janna LT", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Goldenrod;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV_Prof.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGV_Prof.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Janna LT", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.BlueViolet;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV_Prof.DefaultCellStyle = dataGridViewCellStyle3;
            this.DGV_Prof.EnableHeadersVisualStyles = false;
            this.DGV_Prof.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DGV_Prof.Location = new System.Drawing.Point(12, 247);
            this.DGV_Prof.Name = "DGV_Prof";
            this.DGV_Prof.RightToLeft = System.Windows.Forms.RightToLeft.No;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.ForestGreen;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Janna LT", 9F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.IndianRed;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_Prof.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DGV_Prof.RowHeadersWidth = 24;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DGV_Prof.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.DGV_Prof.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.DGV_Prof.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Janna LT", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGV_Prof.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.DGV_Prof.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DGV_Prof.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.DGV_Prof.RowTemplate.ReadOnly = true;
            this.DGV_Prof.Size = new System.Drawing.Size(555, 140);
            this.DGV_Prof.TabIndex = 39;
            // 
            // BT_GetProfile
            // 
            this.BT_GetProfile.BackColor = System.Drawing.Color.Goldenrod;
            this.BT_GetProfile.FlatAppearance.BorderSize = 0;
            this.BT_GetProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BT_GetProfile.ForeColor = System.Drawing.Color.White;
            this.BT_GetProfile.Location = new System.Drawing.Point(573, 258);
            this.BT_GetProfile.Name = "BT_GetProfile";
            this.BT_GetProfile.Size = new System.Drawing.Size(183, 38);
            this.BT_GetProfile.TabIndex = 40;
            this.BT_GetProfile.Text = "جلب البروفايلات";
            this.BT_GetProfile.UseVisualStyleBackColor = false;
            this.BT_GetProfile.Click += new System.EventHandler(this.BT_GetProfile_Click);
            // 
            // Frm_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 388);
            this.Controls.Add(this.BT_GetProfile);
            this.Controls.Add(this.DGV_Prof);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.TB_OnLinDevisc);
            this.Controls.Add(this.RTB_Result);
            this.Controls.Add(this.TB_ip);
            this.Controls.Add(this.TB_Port);
            this.Controls.Add(this.BT_Login);
            this.Controls.Add(this.checkBox1);
            this.Font = new System.Drawing.Font("Janna LT", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Frm_Login";
            this.Text = "Frm_Login";
            this.Load += new System.EventHandler(this.Frm_Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Prof)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button BT_Login;
        private System.Windows.Forms.TextBox TB_Port;
        private System.Windows.Forms.TextBox TB_ip;
        private System.Windows.Forms.RichTextBox RTB_Result;
        private System.Windows.Forms.TextBox TB_OnLinDevisc;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.DataGridView DGV_Prof;
        private System.Windows.Forms.Button BT_GetProfile;
    }
}