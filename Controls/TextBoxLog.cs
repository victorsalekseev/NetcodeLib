using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Netcode.Controls
{
    public partial class TextBoxLog : UserControl
    {
        public TextBoxLog()
        {
            InitializeComponent();
            сохранитьToolStripMenuItem.Click += new EventHandler(сохранитьToolStripMenuItem_Click);
            toolStripMenuItemCopy.Click += new EventHandler(toolStripMenuItemCopy_Click);
            contextMenuStrip_log.Opening += new CancelEventHandler(contextMenuStrip_log_Opening);
        }

        void contextMenuStrip_log_Opening(object sender, CancelEventArgs e)
        {
            if (textBox_log.SelectionLength < 1)
            {
                toolStripMenuItemCopy.Enabled = false;
            }
            else
            {
                toolStripMenuItemCopy.Enabled = true;
            }
        }

        private string _textlog = string.Empty;
        public string TextLog
        {
            get { return _textlog;}
            set { _textlog=value;
            textBox_log.AppendText( _textlog);
            }
        }

        void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
             Clipboard.SetText(textBox_log.SelectedText);
        }

        void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    sw.Write(textBox_log.Text);
                    sw.Close();
                }
            }
        }
    }
}
