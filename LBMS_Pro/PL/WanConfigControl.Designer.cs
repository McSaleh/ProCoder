namespace LBMS_Pro.PL
{
    partial class WanConfigControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtWanISP = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtWanISP
            // 
            this.txtWanISP.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtWanISP.Location = new System.Drawing.Point(15, 20);
            this.txtWanISP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtWanISP.Name = "txtWanISP";
            this.txtWanISP.Size = new System.Drawing.Size(187, 32);
            this.txtWanISP.TabIndex = 0;
            this.txtWanISP.Text = "ether";
            // 
            // WanConfigControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtWanISP);
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "WanConfigControl";
            this.Size = new System.Drawing.Size(709, 66);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWanISP;
    }
}
