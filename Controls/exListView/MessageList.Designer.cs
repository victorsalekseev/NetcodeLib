namespace Netcode.Controls
{
    partial class MessageList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageList));
            this.listView_trace = new System.Windows.Forms.ListView();
            this.Время = new System.Windows.Forms.ColumnHeader();
            this.Событие = new System.Windows.Forms.ColumnHeader();
            this.Комментарий = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView_trace
            // 
            this.listView_trace.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Время,
            this.Событие,
            this.Комментарий});
            this.listView_trace.ContextMenuStrip = this.contextMenuStrip;
            this.listView_trace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_trace.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView_trace.FullRowSelect = true;
            this.listView_trace.GridLines = true;
            this.listView_trace.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_trace.LargeImageList = this.imageList;
            this.listView_trace.Location = new System.Drawing.Point(0, 0);
            this.listView_trace.MultiSelect = false;
            this.listView_trace.Name = "listView_trace";
            this.listView_trace.Size = new System.Drawing.Size(691, 127);
            this.listView_trace.SmallImageList = this.imageList;
            this.listView_trace.TabIndex = 22;
            this.listView_trace.UseCompatibleStateImageBehavior = false;
            this.listView_trace.View = System.Windows.Forms.View.Details;
            // 
            // Время
            // 
            this.Время.Text = "Время";
            this.Время.Width = 200;
            // 
            // Событие
            // 
            this.Событие.Text = "Событие";
            this.Событие.Width = 200;
            // 
            // Комментарий
            // 
            this.Комментарий.Text = "Комментарий";
            this.Комментарий.Width = 250;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьКакToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(174, 26);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Image = global::Netcode.Common.Properties.Resources.save_16;
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как...";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "edit_add.png");
            this.imageList.Images.SetKeyName(1, "player_play.png");
            this.imageList.Images.SetKeyName(2, "player_stop.png");
            this.imageList.Images.SetKeyName(3, "no.png");
            this.imageList.Images.SetKeyName(4, "info.png");
            this.imageList.Images.SetKeyName(5, "camera_test.png");
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            // 
            // MessageList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView_trace);
            this.Name = "MessageList";
            this.Size = new System.Drawing.Size(691, 127);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView_trace;
        private System.Windows.Forms.ColumnHeader Время;
        private System.Windows.Forms.ColumnHeader Событие;
        private System.Windows.Forms.ColumnHeader Комментарий;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}
