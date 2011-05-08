namespace Netcode.Controls
{
    partial class HtmlEditor
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
            this.Redaktor_tabControl = new System.Windows.Forms.TabControl();
            this.html_tabPage = new System.Windows.Forms.TabPage();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_open = new System.Windows.Forms.ToolStripSplitButton();
            this.какНеизвестныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.какВстроенныйОбъектToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tSBtn_print = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_undo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_redo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_cut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_copy = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_paste = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_bold = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_italic = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_underline = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.JustifyLeft = new System.Windows.Forms.ToolStripButton();
            this.JustifyRight = new System.Windows.Forms.ToolStripButton();
            this.JustifyCenter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_JustifyFull = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.InsertImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_link = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBox_fontsize = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton_nr_to_br = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_html = new System.Windows.Forms.ToolStripButton();
            this.view_tabPage = new System.Windows.Forms.TabPage();
            this.html_richTextBox = new System.Windows.Forms.RichTextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.Redaktor_tabControl.SuspendLayout();
            this.html_tabPage.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.view_tabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // Redaktor_tabControl
            // 
            this.Redaktor_tabControl.Controls.Add(this.html_tabPage);
            this.Redaktor_tabControl.Controls.Add(this.view_tabPage);
            this.Redaktor_tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Redaktor_tabControl.Location = new System.Drawing.Point(0, 0);
            this.Redaktor_tabControl.Name = "Redaktor_tabControl";
            this.Redaktor_tabControl.SelectedIndex = 0;
            this.Redaktor_tabControl.Size = new System.Drawing.Size(678, 393);
            this.Redaktor_tabControl.TabIndex = 6;
            // 
            // html_tabPage
            // 
            this.html_tabPage.Controls.Add(this.webBrowser);
            this.html_tabPage.Controls.Add(this.toolStrip);
            this.html_tabPage.Location = new System.Drawing.Point(4, 22);
            this.html_tabPage.Name = "html_tabPage";
            this.html_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.html_tabPage.Size = new System.Drawing.Size(670, 367);
            this.html_tabPage.TabIndex = 1;
            this.html_tabPage.Text = "Просмотр";
            this.html_tabPage.UseVisualStyleBackColor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(3, 28);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(664, 336);
            this.webBrowser.TabIndex = 14;
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_open,
            this.tSBtn_print,
            this.toolStripButton_undo,
            this.toolStripButton_redo,
            this.toolStripSeparator3,
            this.toolStripButton_cut,
            this.toolStripButton_copy,
            this.toolStripButton_paste,
            this.toolStripButton_bold,
            this.toolStripButton_italic,
            this.toolStripButton_underline,
            this.toolStripSeparator,
            this.JustifyLeft,
            this.JustifyRight,
            this.JustifyCenter,
            this.toolStripButton_JustifyFull,
            this.toolStripSeparator2,
            this.InsertImage,
            this.toolStripButton2,
            this.toolStripButton1,
            this.toolStripButton_link,
            this.toolStripSeparator1,
            this.toolStripComboBox_fontsize,
            this.toolStripButton_nr_to_br,
            this.toolStripButton_html});
            this.toolStrip.Location = new System.Drawing.Point(3, 3);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(664, 25);
            this.toolStrip.TabIndex = 13;
            // 
            // toolStripButton_open
            // 
            this.toolStripButton_open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_open.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.какНеизвестныйToolStripMenuItem,
            this.какВстроенныйОбъектToolStripMenuItem});
            this.toolStripButton_open.Image = global::Netcode.Common.Properties.Resources.document_into;
            this.toolStripButton_open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_open.Name = "toolStripButton_open";
            this.toolStripButton_open.Size = new System.Drawing.Size(32, 22);
            this.toolStripButton_open.Text = "Скопировать из файла";
            this.toolStripButton_open.ButtonClick += new System.EventHandler(this.toolStripButton_open_ButtonClick);
            // 
            // какНеизвестныйToolStripMenuItem
            // 
            this.какНеизвестныйToolStripMenuItem.Name = "какНеизвестныйToolStripMenuItem";
            this.какНеизвестныйToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.какНеизвестныйToolStripMenuItem.Text = "Неизвестный или Plain text";
            this.какНеизвестныйToolStripMenuItem.Click += new System.EventHandler(this.какНеизвестныйToolStripMenuItem_Click);
            // 
            // какВстроенныйОбъектToolStripMenuItem
            // 
            this.какВстроенныйОбъектToolStripMenuItem.Name = "какВстроенныйОбъектToolStripMenuItem";
            this.какВстроенныйОбъектToolStripMenuItem.Size = new System.Drawing.Size(221, 22);
            this.какВстроенныйОбъектToolStripMenuItem.Text = "Как встроенный объект";
            this.какВстроенныйОбъектToolStripMenuItem.Click += new System.EventHandler(this.какВстроенныйОбъектToolStripMenuItem_Click);
            // 
            // tSBtn_print
            // 
            this.tSBtn_print.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tSBtn_print.Image = global::Netcode.Common.Properties.Resources.PrintHS;
            this.tSBtn_print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tSBtn_print.Name = "tSBtn_print";
            this.tSBtn_print.Size = new System.Drawing.Size(23, 22);
            this.tSBtn_print.Text = "Печать";
            this.tSBtn_print.Click += new System.EventHandler(this.tSBtn_print_Click);
            // 
            // toolStripButton_undo
            // 
            this.toolStripButton_undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_undo.Image = global::Netcode.Common.Properties.Resources.undo;
            this.toolStripButton_undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_undo.Name = "toolStripButton_undo";
            this.toolStripButton_undo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_undo.Text = "Назад";
            this.toolStripButton_undo.Click += new System.EventHandler(this.toolStripButton_undo_Click);
            // 
            // toolStripButton_redo
            // 
            this.toolStripButton_redo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_redo.Image = global::Netcode.Common.Properties.Resources.redo;
            this.toolStripButton_redo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_redo.Name = "toolStripButton_redo";
            this.toolStripButton_redo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_redo.Text = "Вперед";
            this.toolStripButton_redo.Click += new System.EventHandler(this.toolStripButton_redo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_cut
            // 
            this.toolStripButton_cut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_cut.Image = global::Netcode.Common.Properties.Resources.cut;
            this.toolStripButton_cut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_cut.Name = "toolStripButton_cut";
            this.toolStripButton_cut.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_cut.Text = "Вырезать";
            this.toolStripButton_cut.Click += new System.EventHandler(this.toolStripButton_cut_Click);
            // 
            // toolStripButton_copy
            // 
            this.toolStripButton_copy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_copy.Image = global::Netcode.Common.Properties.Resources.copy;
            this.toolStripButton_copy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_copy.Name = "toolStripButton_copy";
            this.toolStripButton_copy.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_copy.Text = "Копировать";
            this.toolStripButton_copy.Click += new System.EventHandler(this.toolStripButton_copy_Click);
            // 
            // toolStripButton_paste
            // 
            this.toolStripButton_paste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_paste.Image = global::Netcode.Common.Properties.Resources.paste;
            this.toolStripButton_paste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_paste.Name = "toolStripButton_paste";
            this.toolStripButton_paste.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_paste.Text = "Вставить";
            this.toolStripButton_paste.Click += new System.EventHandler(this.toolStripButton_paste_Click);
            // 
            // toolStripButton_bold
            // 
            this.toolStripButton_bold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_bold.Image = global::Netcode.Common.Properties.Resources.text_bold;
            this.toolStripButton_bold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_bold.Name = "toolStripButton_bold";
            this.toolStripButton_bold.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_bold.Click += new System.EventHandler(this.toolStripButton_bold_Click);
            // 
            // toolStripButton_italic
            // 
            this.toolStripButton_italic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_italic.Image = global::Netcode.Common.Properties.Resources.text_italics;
            this.toolStripButton_italic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_italic.Name = "toolStripButton_italic";
            this.toolStripButton_italic.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_italic.Click += new System.EventHandler(this.toolStripButton_italic_Click);
            // 
            // toolStripButton_underline
            // 
            this.toolStripButton_underline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_underline.Image = global::Netcode.Common.Properties.Resources.text_underlined;
            this.toolStripButton_underline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_underline.Name = "toolStripButton_underline";
            this.toolStripButton_underline.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_underline.Click += new System.EventHandler(this.toolStripButton_underline_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // JustifyLeft
            // 
            this.JustifyLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.JustifyLeft.Image = global::Netcode.Common.Properties.Resources.JustifyLeft;
            this.JustifyLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.JustifyLeft.Name = "JustifyLeft";
            this.JustifyLeft.Size = new System.Drawing.Size(23, 22);
            this.JustifyLeft.Click += new System.EventHandler(this.JustifyLeft_Click);
            // 
            // JustifyRight
            // 
            this.JustifyRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.JustifyRight.Image = global::Netcode.Common.Properties.Resources.JustifyRight;
            this.JustifyRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.JustifyRight.Name = "JustifyRight";
            this.JustifyRight.Size = new System.Drawing.Size(23, 22);
            this.JustifyRight.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // JustifyCenter
            // 
            this.JustifyCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.JustifyCenter.Image = global::Netcode.Common.Properties.Resources.JustifyCenter;
            this.JustifyCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.JustifyCenter.Name = "JustifyCenter";
            this.JustifyCenter.Size = new System.Drawing.Size(23, 22);
            this.JustifyCenter.Click += new System.EventHandler(this.JustifyCenter_Click);
            // 
            // toolStripButton_JustifyFull
            // 
            this.toolStripButton_JustifyFull.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_JustifyFull.Image = global::Netcode.Common.Properties.Resources.JustifyFull;
            this.toolStripButton_JustifyFull.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_JustifyFull.Name = "toolStripButton_JustifyFull";
            this.toolStripButton_JustifyFull.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_JustifyFull.Click += new System.EventHandler(this.toolStripButton_JustifyFull_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // InsertImage
            // 
            this.InsertImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InsertImage.Image = global::Netcode.Common.Properties.Resources.InsertImage;
            this.InsertImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InsertImage.Name = "InsertImage";
            this.InsertImage.Size = new System.Drawing.Size(23, 22);
            this.InsertImage.Text = "Вставить картинку (путь до картинки - выделение в документе)";
            this.InsertImage.Click += new System.EventHandler(this.toolStripButton3_Click_1);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Netcode.Common.Properties.Resources.f1;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Цвет выделенного текста";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::Netcode.Common.Properties.Resources.g;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Цвет фона выделения";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton_link
            // 
            this.toolStripButton_link.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_link.Image = global::Netcode.Common.Properties.Resources.link_2;
            this.toolStripButton_link.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_link.Name = "toolStripButton_link";
            this.toolStripButton_link.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_link.Text = "Вставить ссылку";
            this.toolStripButton_link.Click += new System.EventHandler(this.toolStripButton_link_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripComboBox_fontsize
            // 
            this.toolStripComboBox_fontsize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox_fontsize.Items.AddRange(new object[] {
            "Font Size = 1",
            "Font Size = 2",
            "Font Size = 3",
            "Font Size = 4",
            "Font Size = 5",
            "Font Size = 6"});
            this.toolStripComboBox_fontsize.Name = "toolStripComboBox_fontsize";
            this.toolStripComboBox_fontsize.Size = new System.Drawing.Size(90, 25);
            this.toolStripComboBox_fontsize.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox_fontsize_SelectedIndexChanged);
            // 
            // toolStripButton_nr_to_br
            // 
            this.toolStripButton_nr_to_br.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_nr_to_br.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_nr_to_br.Name = "toolStripButton_nr_to_br";
            this.toolStripButton_nr_to_br.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_nr_to_br.Text = "Заменить перенос каретки тегом <BR>";
            this.toolStripButton_nr_to_br.Visible = false;
            this.toolStripButton_nr_to_br.Click += new System.EventHandler(this.toolStripButton_nr_to_br_Click);
            // 
            // toolStripButton_html
            // 
            this.toolStripButton_html.Image = global::Netcode.Common.Properties.Resources.document_into;
            this.toolStripButton_html.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_html.Name = "toolStripButton_html";
            this.toolStripButton_html.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_html.Visible = false;
            // 
            // view_tabPage
            // 
            this.view_tabPage.Controls.Add(this.html_richTextBox);
            this.view_tabPage.Location = new System.Drawing.Point(4, 22);
            this.view_tabPage.Name = "view_tabPage";
            this.view_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.view_tabPage.Size = new System.Drawing.Size(670, 367);
            this.view_tabPage.TabIndex = 2;
            this.view_tabPage.Text = "HTML View (Read only)";
            this.view_tabPage.UseVisualStyleBackColor = true;
            // 
            // html_richTextBox
            // 
            this.html_richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.html_richTextBox.Location = new System.Drawing.Point(3, 3);
            this.html_richTextBox.Name = "html_richTextBox";
            this.html_richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.html_richTextBox.Size = new System.Drawing.Size(664, 361);
            this.html_richTextBox.TabIndex = 0;
            this.html_richTextBox.Text = "";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Форматы html, doc, swf, pdf, odt и другие";
            // 
            // colorDialog
            // 
            this.colorDialog.FullOpen = true;
            // 
            // HtmlEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Redaktor_tabControl);
            this.Name = "HtmlEditor";
            this.Size = new System.Drawing.Size(678, 393);
            this.Redaktor_tabControl.ResumeLayout(false);
            this.html_tabPage.ResumeLayout(false);
            this.html_tabPage.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.view_tabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Redaktor_tabControl;
        private System.Windows.Forms.TabPage html_tabPage;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButton_html;
        private System.Windows.Forms.ToolStripSplitButton toolStripButton_open;
        private System.Windows.Forms.ToolStripMenuItem какНеизвестныйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem какВстроенныйОбъектToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton_undo;
        private System.Windows.Forms.ToolStripButton toolStripButton_redo;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_fontsize;
        private System.Windows.Forms.ToolStripButton toolStripButton_cut;
        private System.Windows.Forms.ToolStripButton toolStripButton_copy;
        private System.Windows.Forms.ToolStripButton toolStripButton_paste;
        private System.Windows.Forms.ToolStripButton toolStripButton_bold;
        private System.Windows.Forms.ToolStripButton toolStripButton_italic;
        private System.Windows.Forms.ToolStripButton toolStripButton_underline;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton toolStripButton_nr_to_br;
        private System.Windows.Forms.TabPage view_tabPage;
        private System.Windows.Forms.RichTextBox html_richTextBox;
        private System.Windows.Forms.ToolStripButton toolStripButton_link;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton JustifyRight;
        private System.Windows.Forms.ToolStripButton JustifyLeft;
        private System.Windows.Forms.ToolStripButton JustifyCenter;
        private System.Windows.Forms.ToolStripButton toolStripButton_JustifyFull;
        private System.Windows.Forms.ToolStripButton InsertImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tSBtn_print;

    }
}
