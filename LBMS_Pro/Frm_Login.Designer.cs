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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.BT_Login = new System.Windows.Forms.Button();
            this.TB_Port = new System.Windows.Forms.TextBox();
            this.TB_ip = new System.Windows.Forms.TextBox();
            this.RTB_Result = new System.Windows.Forms.RichTextBox();
            this.TB_OnLinDevisc = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(104, 14);
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
            this.BT_Login.Location = new System.Drawing.Point(29, 48);
            this.BT_Login.Name = "BT_Login";
            this.BT_Login.Size = new System.Drawing.Size(91, 34);
            this.BT_Login.TabIndex = 1;
            this.BT_Login.Text = "دخول";
            this.BT_Login.UseVisualStyleBackColor = false;
            this.BT_Login.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TB_Port
            // 
            this.TB_Port.Location = new System.Drawing.Point(29, 12);
            this.TB_Port.MaxLength = 4;
            this.TB_Port.Name = "TB_Port";
            this.TB_Port.Size = new System.Drawing.Size(71, 30);
            this.TB_Port.TabIndex = 2;
            // 
            // TB_ip
            // 
            this.TB_ip.Location = new System.Drawing.Point(161, 12);
            this.TB_ip.MaxLength = 15;
            this.TB_ip.Name = "TB_ip";
            this.TB_ip.Size = new System.Drawing.Size(167, 30);
            this.TB_ip.TabIndex = 3;
            this.TB_ip.Text = "10.0.0.1";
            // 
            // RTB_Result
            // 
            this.RTB_Result.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTB_Result.Location = new System.Drawing.Point(29, 163);
            this.RTB_Result.Name = "RTB_Result";
            this.RTB_Result.Size = new System.Drawing.Size(352, 64);
            this.RTB_Result.TabIndex = 4;
            this.RTB_Result.Text = "";
            // 
            // TB_OnLinDevisc
            // 
            this.TB_OnLinDevisc.Location = new System.Drawing.Point(29, 89);
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
            this.listBox1.Location = new System.Drawing.Point(127, 89);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(254, 68);
            this.listBox1.TabIndex = 6;
            // 
            // Frm_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 241);
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
    }
}