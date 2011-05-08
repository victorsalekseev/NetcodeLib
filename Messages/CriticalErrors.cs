using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Netcode.Messages
{

    public class CriticalErrors : Form
    {
        Messages ms = new Messages();
        ToolStrip toolStrip= new ToolStrip();
        ToolStripButton toolStripButtonSave = new ToolStripButton();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        ListView lv = new ListView();

        public CriticalErrors()
        {
            this.Size = new Size(600, 197);
            this.Text = "Критическая ошибка";
            this.Name = "CriticalErrors";
            TopMost = true;
            Opacity = 100;
            ControlBox = true;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;

            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripButtonSave});
            toolStrip.Location = new System.Drawing.Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new System.Drawing.Size(292, 25);
            toolStrip.TabIndex = 0;
            toolStrip.Text = "Панель меню";
            toolStrip.Dock = DockStyle.Top;
            // 
            // toolStripButtonSave
            // 
            toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonSave.Image = global::Netcode.Common.Properties.Resources.save_16;
            toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonSave.Name = "toolStripButtonSave";
            toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            toolStripButtonSave.Text = "Сохранить";
            toolStripButtonSave.Click += new EventHandler(toolStripButtonSave_Click);

            lv.Name = "lb";
            lv.Dock = DockStyle.Fill;
            this.Controls.Add(lv);
            this.Controls.Add(toolStrip);

            lv.BackColor = Color.Red;
            lv.ForeColor = Color.White;
            lv.View = View.Details;
            lv.GridLines = true;
            lv.MultiSelect = false;
            lv.Columns.Add("Время", "Время", 134);
            lv.Columns.Add("Тип", "Тип", 40);
            lv.Columns.Add("Комментарий", "Комментарий", 394);
        }

        void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                        foreach (ListViewItem li in lv.Items)
                        {
                            for (int i = 0; i < li.SubItems.Count; i++)
                            {
                                sw.Write(li.SubItems[i].Text + "|");
                            }
                            sw.Write(Environment.NewLine);
                        }
                        sw.Close();
                    }
                }
            }
            catch { }
        }

        public void PrintError(string class_error, string error)
        {
            if (Application.OpenForms["CriticalErrors"] != null)
            {
                ListView lbc = (ListView)Application.OpenForms["CriticalErrors"].Controls["lb"];
                ms.write_lview_message(class_error, error, Color.Red, 0, lbc);
            }
            else
            {
                this.Show();
                ms.write_lview_message(class_error, error, Color.Red, 0, lv);
            }
        }

        private void InitializeComponent()
        {


        }
    }
}
