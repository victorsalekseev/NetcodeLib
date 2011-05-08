namespace Netcode.Common.Print
{
    partial class SimpleRaport
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
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.PageSettingstoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.PrintSettingstoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.exporttoolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.dataGridView_tmp = new System.Windows.Forms.DataGridView();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_tmp)).BeginInit();
            this.SuspendLayout();
            // 
            // pageSetupDialog
            // 
            this.pageSetupDialog.Document = this.printDocument;
            this.pageSetupDialog.EnableMetric = true;
            // 
            // printDialog
            // 
            this.printDialog.Document = this.printDocument;
            this.printDialog.UseEXDialog = true;
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PageSettingstoolStripButton,
            this.PrintSettingstoolStripButton,
            this.exporttoolStripButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(874, 31);
            this.toolStrip.TabIndex = 2;
            // 
            // PageSettingstoolStripButton
            // 
            this.PageSettingstoolStripButton.Image = global::Netcode.Common.Properties.Resources.EditPageSett;
            this.PageSettingstoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PageSettingstoolStripButton.Name = "PageSettingstoolStripButton";
            this.PageSettingstoolStripButton.Size = new System.Drawing.Size(141, 28);
            this.PageSettingstoolStripButton.Text = "Настройки страницы";
            this.PageSettingstoolStripButton.Visible = false;
            // 
            // PrintSettingstoolStripButton
            // 
            this.PrintSettingstoolStripButton.Image = global::Netcode.Common.Properties.Resources.print_sett;
            this.PrintSettingstoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintSettingstoolStripButton.Name = "PrintSettingstoolStripButton";
            this.PrintSettingstoolStripButton.Size = new System.Drawing.Size(84, 28);
            this.PrintSettingstoolStripButton.Text = "Печатать";
            // 
            // exporttoolStripButton
            // 
            this.exporttoolStripButton.Image = global::Netcode.Common.Properties.Resources.convert_to_xls;
            this.exporttoolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exporttoolStripButton.Name = "exporttoolStripButton";
            this.exporttoolStripButton.Size = new System.Drawing.Size(138, 28);
            this.exporttoolStripButton.Text = "Экспорт в Excell CSV";
            // 
            // printPreviewControl
            // 
            this.printPreviewControl.AutoZoom = false;
            this.printPreviewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printPreviewControl.Document = this.printDocument;
            this.printPreviewControl.Location = new System.Drawing.Point(0, 31);
            this.printPreviewControl.Name = "printPreviewControl";
            this.printPreviewControl.Size = new System.Drawing.Size(874, 461);
            this.printPreviewControl.TabIndex = 3;
            this.printPreviewControl.Zoom = 1;
            // 
            // dataGridView_tmp
            // 
            this.dataGridView_tmp.AllowUserToAddRows = false;
            this.dataGridView_tmp.AllowUserToDeleteRows = false;
            this.dataGridView_tmp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_tmp.Location = new System.Drawing.Point(656, 8);
            this.dataGridView_tmp.Name = "dataGridView_tmp";
            this.dataGridView_tmp.Size = new System.Drawing.Size(35, 12);
            this.dataGridView_tmp.TabIndex = 4;
            this.dataGridView_tmp.Visible = false;
            // 
            // SimpleRaport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 492);
            this.Controls.Add(this.dataGridView_tmp);
            this.Controls.Add(this.printPreviewControl);
            this.Controls.Add(this.toolStrip);
            this.Name = "SimpleRaport";
            this.Text = "Печать отчета";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_tmp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.PrintPreviewControl printPreviewControl;
        private System.Windows.Forms.ToolStripButton PageSettingstoolStripButton;
        private System.Windows.Forms.ToolStripButton PrintSettingstoolStripButton;
        private System.Windows.Forms.DataGridView dataGridView_tmp;
        private System.Windows.Forms.ToolStripButton exporttoolStripButton;
    }
}