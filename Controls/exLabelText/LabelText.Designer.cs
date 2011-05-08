namespace Netcode.Controls
{
    partial class LTextBox
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
            this.components = new System.ComponentModel.Container();
            this.eP = new System.Windows.Forms.ErrorProvider(this.components);
            this.sC = new System.Windows.Forms.SplitContainer();
            this.lb_name = new System.Windows.Forms.Label();
            this.tb_text = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.eP)).BeginInit();
            this.sC.Panel1.SuspendLayout();
            this.sC.Panel2.SuspendLayout();
            this.sC.SuspendLayout();
            this.SuspendLayout();
            // 
            // eP
            // 
            this.eP.ContainerControl = this.sC;
            // 
            // sC
            // 
            this.sC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sC.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sC.IsSplitterFixed = true;
            this.sC.Location = new System.Drawing.Point(0, 0);
            this.sC.Name = "sC";
            // 
            // sC.Panel1
            // 
            this.sC.Panel1.Controls.Add(this.lb_name);
            this.sC.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // sC.Panel2
            // 
            this.sC.Panel2.Controls.Add(this.tb_text);
            this.sC.Size = new System.Drawing.Size(267, 25);
            this.sC.SplitterDistance = 54;
            this.sC.TabIndex = 9;
            // 
            // lb_name
            // 
            this.lb_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_name.AutoSize = true;
            this.lb_name.Location = new System.Drawing.Point(3, 6);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(47, 13);
            this.lb_name.TabIndex = 7;
            this.lb_name.Text = "lb_name";
            this.lb_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tb_text
            // 
            this.tb_text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_text.Location = new System.Drawing.Point(3, 3);
            this.tb_text.Name = "tb_text";
            this.tb_text.Size = new System.Drawing.Size(185, 20);
            this.tb_text.TabIndex = 8;
            this.tb_text.TextChanged += new System.EventHandler(this.tb_text_TextChanged);
            // 
            // Label_text
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sC);
            this.Name = "Label_text";
            this.Size = new System.Drawing.Size(267, 25);
            this.Load += new System.EventHandler(this.label_text_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eP)).EndInit();
            this.sC.Panel1.ResumeLayout(false);
            this.sC.Panel1.PerformLayout();
            this.sC.Panel2.ResumeLayout(false);
            this.sC.Panel2.PerformLayout();
            this.sC.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider eP;
        private System.Windows.Forms.SplitContainer sC;
        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.TextBox tb_text;

    }
}
