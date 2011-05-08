using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Netcode.Messages
{
    /// <summary>
    /// Иконка в трее. Объявить новый класс
    /// и передать в него экземпляр формы и контекстного меню.
    /// Закрывать форму только функцией CloseApp()
    /// </summary>
    public class NIcon
    {
        public delegate void OnMouseClick_NoForm();
        /// <summary>
        /// Вызывается при клике без формы
        /// </summary>
        public event OnMouseClick_NoForm MouseClick_NoForm;

        NotifyIcon ni = new NotifyIcon();
        bool is_allw_closing = false;
        bool isRedIcon = false;//Красная иконка?
        public bool isStrobeIcon = false;//мигает иконка?
        Form _mForm = null;
        ContextMenuStrip cms_noform;
        Timer TimerStrobeIcon = new Timer();

        public Form mForm
        {
            get { return _mForm; }
            set { _mForm = value; }
        }

        public string IconMessageText
        {
            get { return ni.Text; }
            set {
                if (value.Length > 63)
                {
                    ni.Text = value.Substring(0, 63);
                }
                else
                {
                    ni.Text = value;
                }
            }
        }

        public Icon IconImg
        {
            get { return ni.Icon; }
            set { ni.Icon = value; }
        }

        public NIcon()
        {
            cms_noform = new ContextMenuStrip();
            cms_noform.Items.Add("Закрыть", null, new EventHandler(CloseApp_NoForm));

            ni.ContextMenuStrip = cms_noform;
            ni.Icon = global::Netcode.Common.Properties.Resources.IconBlue;
            ni.Visible = true;
            ni.Text = Application.ProductName;
            ni.MouseClick += new MouseEventHandler(ni_MouseClick_NoForm);
        }

        #region --- Если класс запущен без параметров, действуют только эти функции
        void CloseApp_NoForm(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрыть программу?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                is_allw_closing = true;
                ni.Visible = false;
                Application.Exit();
            }
        }

        void ni_MouseClick_NoForm(object sender, MouseEventArgs e)
        {
            if (MouseClick_NoForm != null && e.Button == MouseButtons.Left)
            {
                MouseClick_NoForm.Invoke();
            }
        }

        #endregion

        public NIcon(ContextMenuStrip cms, Form frm)
        {
            mForm = frm;
            ni.Icon = global::Netcode.Common.Properties.Resources.IconBlue;
            ni.Visible = true;
            ni.Text = Application.ProductName;
            ni.ContextMenuStrip = cms;
            ni.MouseClick += new MouseEventHandler(ni_MouseClick);
            _mForm.FormClosing += new FormClosingEventHandler(_mForm_FormClosing);
        }

        /// <summary>
        /// Мигание иконкой
        /// </summary>
        /// <param name="interval">Интервал, миллисекунд</param>
        public void StartStrobeIcon(int interval)
        {
            TimerStrobeIcon.Interval = interval;
            TimerStrobeIcon.Tick += new EventHandler(TimerStrobeIcon_Tick);
            TimerStrobeIcon.Enabled = true;
            TimerStrobeIcon.Start();
            isStrobeIcon = true;
        }

        public void StopStrobeIcon()
        {
            TimerStrobeIcon.Tick -= new EventHandler(TimerStrobeIcon_Tick);
            TimerStrobeIcon.Enabled = false;
            TimerStrobeIcon.Stop();
            ni.Icon = global::Netcode.Common.Properties.Resources.IconBlue;
            isStrobeIcon = false;
        }

        void TimerStrobeIcon_Tick(object sender, EventArgs e)
        {
            if (isRedIcon)
            {
                ni.Icon = global::Netcode.Common.Properties.Resources.IconBlue;
            }
            else
            {
                ni.Icon = global::Netcode.Common.Properties.Resources.IconRed;
            }

            if (isRedIcon)
            {
                isRedIcon = false;
            }
            else
            {
                isRedIcon = true;
            }
        }
        public void CloseApp()
        {
            if (MessageBox.Show("Закрыть программу?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                is_allw_closing = true;
                ni.Visible = false;
                _mForm.Close();
            }
        }

        void _mForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!is_allw_closing)
            {
                e.Cancel = true;
                _mForm.Visible = false;
            }
        }

        void ni_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ChangeVisiblePosition();
            }
        }

        public void ChangeVisiblePosition()
        {
            if (!_mForm.Visible)
            {
                _mForm.Visible = true;
                _mForm.WindowState = FormWindowState.Normal;
            }
            else
            {
                _mForm.Visible = false;
            }
        }
    }
}
