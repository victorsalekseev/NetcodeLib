using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace Netcode.Controls
{
    public class DateTimePickerUpdate : DateTimePicker
    {
        public DateTimePickerUpdate()
        {
            this.Resize += new EventHandler(DateTimePickerUpdate_Resize);
            Button b = new Button();
            b.Name = "button_reload";
            b.Size = new Size(this.Height-3, this.Height - 7);
            b.BackColor = System.Drawing.SystemColors.Control;
            b.FlatStyle = FlatStyle.Popup;
            b.Location = new Point(this.Width - 40, 2);
            b.Click += new EventHandler(b_Click);
            b.BackgroundImageLayout = ImageLayout.Zoom;
            b.BackgroundImage = global::Netcode.Common.Properties.Resources.refresh;
            this.Controls.Add(b);
        }

        void DateTimePickerUpdate_Resize(object sender, EventArgs e)
        {
            this.Controls["button_reload"].Location = new Point(this.Width - 40, 2);
        }

        public delegate void OnBeginUpdate(object DataValue);
        public event OnBeginUpdate BeginsUpdate;

        void b_Click(object sender, EventArgs e)
        {
            DateTime i = this.Value;
            try
            {
                BeginsUpdate.Invoke(i);
                this.Value = i;
            }
            catch (NullReferenceException)
            {
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            this.Focus();
        }
    }
}
