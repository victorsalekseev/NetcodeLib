using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Netcode.Messages
{

    public class HauptMessages : Form
    {
        Messages ms = new Messages();
        ListView lv = new ListView();
        public HauptMessages()
        {
            this.Size = new Size(600, 197);
            this.Text = "Сообщение";
            this.Name = "HauptMessages";
            TopMost = true;
            Opacity = 100;
            ControlBox = true;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;

            lv.Name = "lb";
            lv.Dock = DockStyle.Fill;
            this.Controls.Add(lv);

            lv.BackColor = Color.BlueViolet;
            lv.ForeColor = Color.White;
            lv.View = View.Details;
            lv.GridLines = true;
            lv.MultiSelect = false;
            lv.Columns.Add("Время", "Время", 134);
            lv.Columns.Add("Тип", "Тип", 40);
            lv.Columns.Add("Комментарий", "Комментарий", 394);
        }

        public void PrintMessage(string class_message, string message)
        {
            if (Application.OpenForms["HauptMessages"] != null)
            {
                ListView lbc = (ListView)Application.OpenForms["HauptMessages"].Controls["lb"];
                ms.write_lview_message(class_message, message, Color.BlueViolet, 0, lbc);
            }
            else
            {
                this.Show();
                ms.write_lview_message(class_message, message, Color.BlueViolet, 0, lv);
            }

            if (!Regex.IsMatch(message, "ERROR", RegexOptions.IgnoreCase | RegexOptions.Singleline))
            {
                Timer t = new Timer();
                t.Interval = 3500;
                t.Enabled = true;
                t.Tick += new EventHandler(t_Tick);                
            }
        }

        void t_Tick(object sender, EventArgs e)
        {
            this.Close();
            Timer t = (Timer)sender;
            t.Enabled = false;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CriticalErrors
            // 
            this.ClientSize = new System.Drawing.Size(292, 84);
            this.Name = "HauptMessages";
            this.ResumeLayout(false);

        }
    }
}
