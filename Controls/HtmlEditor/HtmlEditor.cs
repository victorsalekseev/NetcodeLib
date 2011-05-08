using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using Netcode.Text.EPocalipse.IFilter;

namespace Netcode.Controls
{
    public partial class HtmlEditor : UserControl
    {
                
        public HtmlEditor()
        {
            InitializeComponent();
            webBrowser.DocumentText = "<html><head><title></title></head><body> </body></html>";
            toolStripComboBox_fontsize.Text = "Font Size = 2";
            webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);
            Redaktor_tabControl.SelectedIndexChanged += new EventHandler(Redaktor_tabControl_SelectedIndexChanged);
        }

        string _selected_file = string.Empty;
        public string Selected_Currency_File
        {
            get { return _selected_file; }
            set { value = _selected_file; }
        }

        Encoding _default_encoding = Encoding.Default;
        public Encoding DefaultEncoding
        {
            get { return _default_encoding; }
            set { value = _default_encoding; }
        }

        public string HtmlText
        {
            get { return webBrowser.Document.Body.InnerHtml; }
            set { webBrowser.DocumentText = value; }
        }

        public void Navigate(string urlString)
        {
            webBrowser.Navigate(urlString);
            Application.DoEvents();
        }

        public void EditModeOn()
        {
            try
            {
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                object empty = null;
                doc.ExecCommand("EditMode", false, empty); 		// This turns the control into an editor
            }
            catch { }
        }

        void Redaktor_tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (this.Redaktor_tabControl.SelectedIndex)
                {
                    case 1:	// Code View
                        {
                            HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                            if (doc != null)
                                this.html_richTextBox.Text = doc.Body.InnerHtml;
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (NullReferenceException)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return;
            }
        }

        void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //EditModeOn();
        }

        private void toolStripButton_open_ButtonClick(object sender, EventArgs e)
        {
            toolStripButton_open.DropDown.Show(Cursor.Position);
        }

        private void toolStripButton_bold_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                object empty = null;
                // Bold
                if (doc != null)
                doc.ExecCommand("Bold", false, empty);
            }
            catch { }
        }

        private void toolStripButton_italic_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                object empty = null;
                // Italic
                if (doc != null)
                doc.ExecCommand("Italic", false, empty);
            }
            catch { }
        }

        private void toolStripButton_underline_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                object empty = null;
                // Underline
                if (doc != null)
                doc.ExecCommand("Underline", false, empty);
            }
            catch { }
        }

        private void toolStripButton_paste_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                object empty = null;
                // Paste
                if (doc != null)
                doc.ExecCommand("Paste", false, empty);
            }
            catch { }
        }

        private void toolStripButton_copy_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                object empty = null;
                // Copy
                if (doc != null)
                doc.ExecCommand("Copy", false, empty);
            }
            catch { }
        }

        private void toolStripButton_cut_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                object empty = null;
                // Cut
                if (doc != null)
                doc.ExecCommand("Cut", false, empty);
            }
            catch { }
        }

        private void какНеизвестныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ifilt(false);
        }

        private void какВстроенныйОбъектToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ifilt(true);
        }

        public delegate void OnSelectFile(string FileName);
        public event OnSelectFile SelectFile;
        private void ifilt(bool asEMBED)
        {
            _selected_file = string.Empty;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (asEMBED)
                    {
                        _selected_file = this.openFileDialog.FileName;
                        webBrowser.Navigate(_selected_file);
                    }
                    else
                    {
                        try
                        {
                            TextReader reader = new FilterReader(openFileDialog.FileName);
                            using (reader)
                            {
                                string tmp_file = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), DateTime.Now.Ticks.ToString() + ".html");
                                using (StreamWriter sw = new StreamWriter(tmp_file, false, DefaultEncoding))
                                {
                                    sw.Write(Regex.Replace(reader.ReadToEnd().Replace(Environment.NewLine, "<br>"), "\n", "<BR>" + Environment.NewLine, RegexOptions.Singleline));
                                    sw.Close();
                                }
                                ////object missing = System.Reflection.Missing.Value;
                                ////object tmp = (object)tmp_file;
                                _selected_file = tmp_file;
                                webBrowser.Navigate(tmp_file);
                                ////this.axWebBrowser1.Navigate2(ref tmp, ref missing, ref missing, ref missing, ref missing);
                            }
                        }
                        catch (ArgumentException)
                        {
                            MessageBox.Show("Данный тип файлов не поддерживается");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    return;
                }

            }
            SelectFile.Invoke(_selected_file);
            ////EditModeOn();
        }

        private void toolStripButton_undo_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                object empty = null;
                // Undo
                if (doc != null)
                doc.ExecCommand("Undo", false, empty);
            }
            catch { }
        }

        private void toolStripButton_redo_Click(object sender, EventArgs e)
        {
            try
            {
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                object empty = null;
                // Undo
                if (doc != null)
                doc.ExecCommand("Redo", false, empty);
            }
            catch { }
        }

        private void toolStripComboBox_fontsize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                if(doc != null)
                doc.ExecCommand("FontSize", false, toolStripComboBox_fontsize.Text.Substring(toolStripComboBox_fontsize.Text.Length - 1));
            }
            catch { }
        }

        private void toolStripButton_link_Click(object sender, EventArgs e)
        {
            try
            {
                object empty = null;
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                if (doc != null)
                doc.ExecCommand("CreateLink", false, empty);
            }
            catch { }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SetColor("BackColor");
        }

        private void SetColor(string command)
        {
            try
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                    string[] colors = new string[] {Conversion.Hex(colorDialog.Color.R),
                                                    Conversion.Hex(colorDialog.Color.G),
                                                    Conversion.Hex(colorDialog.Color.B)};

                    for (int i = 0; i < colors.Length; i++)
                    {
                        if (colors[i].Length == 1)
                            colors[i] = "0" + colors[i];
                    }
                    if (doc != null)
                    doc.ExecCommand(command, false, string.Format("#{0}{1}{2}", colors[0], colors[1], colors[2]));
                }
            }
            catch { }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SetColor("ForeColor");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                object empty = null;
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                if (doc != null)
                doc.ExecCommand("JustifyRight", false, empty);
            }
            catch { }
        }

        private void JustifyLeft_Click(object sender, EventArgs e)
        {
            try
            {
                object empty = null;
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                if (doc != null)
                doc.ExecCommand("JustifyLeft", false, empty);
            }
            catch { }
        }

        private void JustifyCenter_Click(object sender, EventArgs e)
        {
            try
            {
                object empty = null;
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                if (doc != null)
                doc.ExecCommand("JustifyCenter", false, empty);
            }
            catch { }
        }

        private void toolStripButton_JustifyFull_Click(object sender, EventArgs e)
        {
            try
            {
                object empty = null;
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                doc.ExecCommand("JustifyFull", false, empty);
            }
            catch { }
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            try
            {
                object empty = null;
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                if (doc != null)
                doc.ExecCommand("InsertImage", false, empty);
            }
            catch { }
        }

        private void toolStripButton_nr_to_br_Click(object sender, EventArgs e)
        {
            html_richTextBox.Text = Regex.Replace(html_richTextBox.Text, "\n", "<BR>" + Environment.NewLine, RegexOptions.Singleline);
        }

        private void tSBtn_print_Click(object sender, EventArgs e)
        {
            try
            {
                object empty = null;
                HtmlDocument doc = (HtmlDocument)this.webBrowser.Document;
                if (doc != null)
                    doc.ExecCommand("Print", false, empty);
            }
            catch { }
        }
    }
}