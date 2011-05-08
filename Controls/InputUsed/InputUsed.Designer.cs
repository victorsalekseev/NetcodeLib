namespace Netcode.Controls
{
    partial class InputUsed
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputUsed));
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageInput = new System.Windows.Forms.TabPage();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.tabPageUsed = new System.Windows.Forms.TabPage();
            this.textBoxUsed = new System.Windows.Forms.TextBox();
            this.toolStripHead = new System.Windows.Forms.ToolStrip();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageInput.SuspendLayout();
            this.tabPageUsed.SuspendLayout();
            this.toolStripHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.tabControl);
            this.groupBox.Controls.Add(this.toolStripHead);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(356, 255);
            this.groupBox.TabIndex = 38;
            this.groupBox.TabStop = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageInput);
            this.tabControl.Controls.Add(this.tabPageUsed);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(3, 41);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(350, 211);
            this.tabControl.TabIndex = 3;
            // 
            // tabPageInput
            // 
            this.tabPageInput.Controls.Add(this.textBoxInput);
            this.tabPageInput.Location = new System.Drawing.Point(4, 22);
            this.tabPageInput.Name = "tabPageInput";
            this.tabPageInput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInput.Size = new System.Drawing.Size(342, 185);
            this.tabPageInput.TabIndex = 0;
            this.tabPageInput.Text = "Новые";
            this.tabPageInput.UseVisualStyleBackColor = true;
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(3, 3);
            this.textBoxInput.Multiline = true;
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInput.Size = new System.Drawing.Size(331, 172);
            this.textBoxInput.TabIndex = 35;
            // 
            // tabPageUsed
            // 
            this.tabPageUsed.Controls.Add(this.textBoxUsed);
            this.tabPageUsed.Location = new System.Drawing.Point(4, 22);
            this.tabPageUsed.Name = "tabPageUsed";
            this.tabPageUsed.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUsed.Size = new System.Drawing.Size(342, 185);
            this.tabPageUsed.TabIndex = 1;
            this.tabPageUsed.Text = "Использованные";
            this.tabPageUsed.UseVisualStyleBackColor = true;
            // 
            // textBoxUsed
            // 
            this.textBoxUsed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxUsed.Location = new System.Drawing.Point(3, 3);
            this.textBoxUsed.Multiline = true;
            this.textBoxUsed.Name = "textBoxUsed";
            this.textBoxUsed.ReadOnly = true;
            this.textBoxUsed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxUsed.Size = new System.Drawing.Size(336, 179);
            this.textBoxUsed.TabIndex = 36;
            // 
            // toolStripHead
            // 
            this.toolStripHead.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator});
            this.toolStripHead.Location = new System.Drawing.Point(3, 16);
            this.toolStripHead.Name = "toolStripHead";
            this.toolStripHead.Size = new System.Drawing.Size(350, 25);
            this.toolStripHead.TabIndex = 2;
            this.toolStripHead.Text = "toolStrip1";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.openToolStripButton.Text = "&Открыть";
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.saveToolStripButton.Text = "&Сохранить";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            // 
            // InputUsed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "InputUsed";
            this.Size = new System.Drawing.Size(356, 255);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageInput.ResumeLayout(false);
            this.tabPageInput.PerformLayout();
            this.tabPageUsed.ResumeLayout(false);
            this.tabPageUsed.PerformLayout();
            this.toolStripHead.ResumeLayout(false);
            this.toolStripHead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageInput;
        private System.Windows.Forms.TabPage tabPageUsed;
        private System.Windows.Forms.TextBox textBoxUsed;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.ToolStrip toolStripHead;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;

    }
}
