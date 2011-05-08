namespace Netcode.Controls
{
    partial class MSQGrid
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
            this.panel_head = new System.Windows.Forms.Panel();
            this.lb_page_size = new System.Windows.Forms.Label();
            this.cB_per_page = new System.Windows.Forms.ComboBox();
            this.lb_info = new System.Windows.Forms.Label();
            this.panel_down = new System.Windows.Forms.Panel();
            this.btn_fwd = new System.Windows.Forms.Button();
            this.btn_prev = new System.Windows.Forms.Button();
            this.dataGridView_db = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip_m = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_head.SuspendLayout();
            this.panel_down.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_db)).BeginInit();
            this.contextMenuStrip_m.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_head
            // 
            this.panel_head.Controls.Add(this.lb_page_size);
            this.panel_head.Controls.Add(this.cB_per_page);
            this.panel_head.Controls.Add(this.lb_info);
            this.panel_head.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_head.Location = new System.Drawing.Point(0, 0);
            this.panel_head.Name = "panel_head";
            this.panel_head.Size = new System.Drawing.Size(192, 46);
            this.panel_head.TabIndex = 0;
            // 
            // lb_page_size
            // 
            this.lb_page_size.AutoSize = true;
            this.lb_page_size.Location = new System.Drawing.Point(3, 6);
            this.lb_page_size.Name = "lb_page_size";
            this.lb_page_size.Size = new System.Drawing.Size(118, 13);
            this.lb_page_size.TabIndex = 1;
            this.lb_page_size.Text = "Записей на странице:";
            // 
            // cB_per_page
            // 
            this.cB_per_page.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_per_page.FormattingEnabled = true;
            this.cB_per_page.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "100",
            "500",
            "1000"});
            this.cB_per_page.Location = new System.Drawing.Point(127, 3);
            this.cB_per_page.Name = "cB_per_page";
            this.cB_per_page.Size = new System.Drawing.Size(61, 21);
            this.cB_per_page.TabIndex = 0;
            // 
            // lb_info
            // 
            this.lb_info.AutoSize = true;
            this.lb_info.Location = new System.Drawing.Point(3, 29);
            this.lb_info.Name = "lb_info";
            this.lb_info.Size = new System.Drawing.Size(29, 13);
            this.lb_info.TabIndex = 0;
            this.lb_info.Text = "label";
            // 
            // panel_down
            // 
            this.panel_down.Controls.Add(this.btn_fwd);
            this.panel_down.Controls.Add(this.btn_prev);
            this.panel_down.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_down.Location = new System.Drawing.Point(0, 365);
            this.panel_down.Name = "panel_down";
            this.panel_down.Size = new System.Drawing.Size(192, 29);
            this.panel_down.TabIndex = 13;
            // 
            // btn_fwd
            // 
            this.btn_fwd.Location = new System.Drawing.Point(104, 3);
            this.btn_fwd.Name = "btn_fwd";
            this.btn_fwd.Size = new System.Drawing.Size(85, 23);
            this.btn_fwd.TabIndex = 1;
            this.btn_fwd.Text = ">>";
            this.btn_fwd.UseVisualStyleBackColor = true;
            // 
            // btn_prev
            // 
            this.btn_prev.Location = new System.Drawing.Point(3, 3);
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(85, 23);
            this.btn_prev.TabIndex = 0;
            this.btn_prev.Text = "<<";
            this.btn_prev.UseVisualStyleBackColor = true;
            // 
            // dataGridView_db
            // 
            this.dataGridView_db.AllowUserToAddRows = false;
            this.dataGridView_db.AllowUserToDeleteRows = false;
            this.dataGridView_db.AllowUserToResizeRows = false;
            this.dataGridView_db.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_db.ContextMenuStrip = this.contextMenuStrip_m;
            this.dataGridView_db.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_db.Location = new System.Drawing.Point(0, 46);
            this.dataGridView_db.MultiSelect = false;
            this.dataGridView_db.Name = "dataGridView_db";
            this.dataGridView_db.ReadOnly = true;
            this.dataGridView_db.RowHeadersWidth = 23;
            this.dataGridView_db.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_db.Size = new System.Drawing.Size(192, 319);
            this.dataGridView_db.TabIndex = 0;
            this.dataGridView_db.DoubleClick += new System.EventHandler(this.dataGridView_db_DoubleClick);
            this.dataGridView_db.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_db_RowsRemoved);
            this.dataGridView_db.SelectionChanged += new System.EventHandler(this.dataGridView_db_SelectionChanged);
            // 
            // contextMenuStrip_m
            // 
            this.contextMenuStrip_m.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.изменитьToolStripMenuItem,
            this.удалитьToolStripMenuItem});
            this.contextMenuStrip_m.Name = "contextMenuStrip_m";
            this.contextMenuStrip_m.Size = new System.Drawing.Size(136, 70);
            // 
            // добавитьToolStripMenuItem
            // 
            this.добавитьToolStripMenuItem.Image = global::Netcode.Common.Properties.Resources.add;
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.добавитьToolStripMenuItem.Text = "Добавить";
            // 
            // изменитьToolStripMenuItem
            // 
            this.изменитьToolStripMenuItem.Image = global::Netcode.Common.Properties.Resources.edit;
            this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
            this.изменитьToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.изменитьToolStripMenuItem.Text = "Изменить";
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Image = global::Netcode.Common.Properties.Resources.delete;
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Visible = false;
            // 
            // MSQGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView_db);
            this.Controls.Add(this.panel_down);
            this.Controls.Add(this.panel_head);
            this.Name = "MSQGrid";
            this.Size = new System.Drawing.Size(192, 394);
            this.panel_head.ResumeLayout(false);
            this.panel_head.PerformLayout();
            this.panel_down.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_db)).EndInit();
            this.contextMenuStrip_m.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_head;
        private System.Windows.Forms.Label lb_page_size;
        private System.Windows.Forms.ComboBox cB_per_page;
        private System.Windows.Forms.Label lb_info;
        private System.Windows.Forms.Panel panel_down;
        private System.Windows.Forms.Button btn_fwd;
        private System.Windows.Forms.Button btn_prev;
        private System.Windows.Forms.DataGridView dataGridView_db;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_m;
        private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
    }
}
