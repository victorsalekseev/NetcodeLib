using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace Netcode.Controls
{
    public partial class MessageList : UserControl
    {
        public delegate void OnSelectItem(ListViewItem lvi);
        /// <summary>
        /// При выборе итема
        /// </summary>
        public event OnSelectItem SelectItem;

        public System.Windows.Forms.ListView.ListViewItemCollection ListViewItems
        {
            get { return listView_trace.Items; }
        }

        public int ListViewItemCount
        {
            get { return listView_trace.Items.Count; }
        }

        public 
        string ar = string.Empty;
        Label _wait = new Label();

        public MessageList()
        {
            _wait.BackColor = System.Drawing.SystemColors.Window;
            _wait.AutoSize = true;
            _wait.Location = new System.Drawing.Point(this.Width / 2, this.Height / 2);
            _wait.Name = "_wait";
            _wait.Size = new System.Drawing.Size(35, 13);
            _wait.Text = string.Empty;
            _wait.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            _wait.Visible = false;
            this.Controls.Add(_wait);

            InitializeComponent();

            saveFileDialog.FileName = "Сообщения программы";
            this.Resize += new EventHandler(MessageList_Resize);
            сохранитьКакToolStripMenuItem.Click += new EventHandler(сохранитьКакToolStripMenuItem_Click);
            listView_trace.Click += new EventHandler(listView_trace_Click);
            //listView_trace.FindItemWithText
        }

        void listView_trace_Click(object sender, EventArgs e)
        {
            if (SelectItem != null)
            {
                if (listView_trace.Items.Count > 0 && listView_trace.SelectedItems.Count > 0)
                {
                    SelectItem.Invoke(listView_trace.SelectedItems[0]);
                }
            }
        }

        void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    sw.Write(ar);
                    sw.Close();
                }
            }
        }

        void MessageList_Resize(object sender, EventArgs e)
        {
            Комментарий.Width = this.Width - Время.Width - Событие.Width - 30;
        }

        /// <summary>
        /// Очистить список
        /// </summary>
        public void ClearItems()
        {
            ar = string.Empty;
            listView_trace.Items.Clear();
            GC.Collect();
        }

        /// <summary>
        /// Написать сообщение о добавлении
        /// </summary>
        /// <param name="Event">Событие</param>
        /// <param name="Comment">Комментарий</param>
        public void PrintMAdd(string Event, string Comment)
        {
            DateTime dt = DateTime.Now;
            string d_time = dt.ToLongTimeString() + " (" + dt.ToShortDateString() + ")";
            write_lview_message(d_time, Event, Comment, Color.LightGreen, 0, listView_trace, null);
        }

        /// <summary>
        /// Написать сообщение о добавлении
        /// </summary>
        /// <param name="DateTime">Дата и время</param>
        /// <param name="Event">Событие</param>
        /// <param name="Comment">Комментарий</param>
        public void PrintMAdd(string DateTime, string Event, string Comment, object tag)
        {
            write_lview_message(DateTime, Event, Comment, Color.LightGreen, 0, listView_trace, tag);
        }

        /// <summary>
        /// Написать сообщение о запуске
        /// </summary>
        /// <param name="Event">Событие</param>
        /// <param name="Comment">Комментарий</param>
        public void PrintMStart(string Event, string Comment)
        {
            DateTime dt = DateTime.Now;
            string d_time = dt.ToLongTimeString() + " (" + dt.ToShortDateString() + ")";
            write_lview_message(d_time, Event, Comment, Color.LightYellow, 1, listView_trace, null);
        }

        /// <summary>
        /// Написать сообщение о запуске
        /// </summary>
        /// <param name="DateTime">Дата и время</param>
        /// <param name="Event">Событие</param>
        /// <param name="Comment">Комментарий</param>
        public void PrintMStart(string DateTime, string Event, string Comment, object tag)
        {
            write_lview_message(DateTime, Event, Comment, Color.LightYellow, 1, listView_trace, tag);
        }

        /// <summary>
        /// Написать сообщение об остановке
        /// </summary>
        /// <param name="Event">Событие</param>
        /// <param name="Comment">Комментарий</param>
        public void PrintMStop(string Event, string Comment)
        {
            DateTime dt = DateTime.Now;
            string d_time = dt.ToLongTimeString() + " (" + dt.ToShortDateString() + ")";
            write_lview_message(d_time, Event, Comment, Color.GhostWhite, 2, listView_trace, null);
        }

        /// <summary>
        /// Написать сообщение об остановке
        /// </summary>
        /// <param name="DateTime">Дата и время</param>
        /// <param name="Event">Событие</param>
        /// <param name="Comment">Комментарий</param>
        public void PrintMStop(string DateTime, string Event, string Comment, object tag)
        {
            write_lview_message(DateTime, Event, Comment, Color.GhostWhite, 2, listView_trace, tag);
        }

        /// <summary>
        /// Написать сообщение об ошибке
        /// </summary>
        /// <param name="Event">Событие</param>
        /// <param name="Comment">Комментарий</param>
        public void PrintMError(string Event, string Comment)
        {
            DateTime dt = DateTime.Now;
            string d_time = dt.ToLongTimeString() + " (" + dt.ToShortDateString() + ")";
            write_lview_message(d_time, Event, Comment, Color.Red, 3, listView_trace, null);
        }

        /// <summary>
        /// Написать сообщение об ошибке
        /// </summary>
        /// <param name="DateTime">Дата и время</param>
        /// <param name="Event">Событие</param>
        /// <param name="Comment">Комментарий</param>
        public void PrintMError(string DateTime, string Event, string Comment, object tag)
        {
            write_lview_message(DateTime, Event, Comment, Color.Red, 3, listView_trace, tag);
        }

        /// <summary>
        /// Написать информационное сообщение
        /// </summary>
        /// <param name="Event">Событие</param>
        /// <param name="Comment">Комментарий</param>
        public void PrintMInfo(string Event, string Comment)
        {
            DateTime dt = DateTime.Now;
            string d_time = dt.ToLongTimeString() + " (" + dt.ToShortDateString() + ")";
            write_lview_message(d_time, Event, Comment, Color.GhostWhite, 4, listView_trace, null);
        }

        /// <summary>
        /// Написать информационное сообщение
        /// </summary>
        /// <param name="DateTime">Дата и время</param>
        /// <param name="Event">Событие</param>
        /// <param name="Comment">Комментарий</param>
        public void PrintMInfo(string DateTime, string Event, string Comment, Color cl, object tag)
        {
            write_lview_message(DateTime, Event, Comment, cl, 4, listView_trace, tag);
        }

        /// <summary>
        /// Написать сообщение об удачном завершении
        /// </summary>
        /// <param name="Event">Событие</param>
        /// <param name="Comment">Комментарий</param>
        public void PrintMSuccess(string Event, string Comment)
        {
            DateTime dt = DateTime.Now;
            string d_time = dt.ToLongTimeString() + " (" + dt.ToShortDateString() + ")";
            write_lview_message(d_time, Event, Comment, Color.DodgerBlue, 5, listView_trace, null);
        }

        /// <summary>
        /// Написать сообщение об удачном завершении
        /// </summary>
        /// <param name="DateTime">Дата и время</param>
        /// <param name="Event">Событие</param>
        /// <param name="Comment">Комментарий</param>
        public void PrintMSuccess(string DateTime, string Event, string Comment, object tag)
        {
            write_lview_message(DateTime, Event, Comment, Color.DodgerBlue, 5, listView_trace, tag);
        }

        private void write_lview_message(string datetime, string events, string commment, Color cl, int indx, ListView liv, object tag)
        {
            ListViewItem li = new ListViewItem(datetime);
            li.Tag = tag;
            li.Name = commment;
            li.SubItems.Add(events);
            li.SubItems.Add(commment);

            li.BackColor = cl;
            li.ImageIndex = indx;
            liv.Items.Add(li);
            liv.Items[liv.Items.Count - 1].EnsureVisible();
            ar += datetime + " | " + events + " | " + commment + Environment.NewLine;
        }

        public void BeginUpdate()
        {
            listView_trace.BeginUpdate();
            _wait.Text = "Загрузка...";
            _wait.Location = new System.Drawing.Point((this.Width/2)-(_wait.Width/2), (this.Height/2)-(_wait.Height/2));
            _wait.Visible = true;
        }

        public void EndUpdate()
        {
            listView_trace.EndUpdate();
            _wait.Location = new System.Drawing.Point(0, 0);
            _wait.Text = string.Empty;
            _wait.Visible = false;
        }

        public void Sort(SortOrder so)
        {
            listView_trace.Sorting = so;
            listView_trace.Sort();
        }
    }
}
