using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace Netcode.Messages
{
    class BallonTips
    {
        private void ShowErrMitSound(string mess, string wavfile)
        {
            Netcode.Sound.Sound.SoundHelp sh = new Netcode.Sound.Sound.SoundHelp();
            sh.PlayWAV(wavfile, false, true);
            SetBalloonTip(500, "Внимание", mess, System.Drawing.SystemIcons.Asterisk, ToolTipIcon.Error, true, 2000);
        }

        #region BallonTip
        /// <summary>
        /// BallonTip
        /// </summary>
        /// <param name="ms">Таймаут, миллисекунд</param>
        /// <param name="title">Заголовок подсказки</param>
        /// <param name="text">Текст подсказки</param>
        /// <param name="sys_icon">Системная иконка сообщения</param>
        /// <param name="tip_icon">Иконка в трее</param>
        /// <param name="visible">Зарезервированный параметр</param>
        /// <param name="hide_ms">Время скрыия подсказки, миллисекунд</param>
        public void SetBalloonTip(int ms, string title, string text, Icon sys_icon, ToolTipIcon tip_icon, bool visible, int hide_ms)
        {
            NotifyIcon nic = new NotifyIcon();
            nic.Icon = sys_icon;
            nic.Visible = visible;
            nic.ShowBalloonTip(ms, title, text, tip_icon);
            Thread.Sleep(hide_ms);
            nic.Visible = false;
        }

        #endregion

    }
}
