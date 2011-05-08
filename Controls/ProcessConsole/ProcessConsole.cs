using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Netcode.Controls
{
    public partial class ProcessConsole : UserControl
    {
        public delegate void AddTextBoxDelegate(string text);
        AddTextBoxDelegate addText;

        public ProcessConsole()
        {
            InitializeComponent();
            addText = new AddTextBoxDelegate(AddTextBoxText);
        }

        private void AddTextBoxText(string text)
        {
            if (!textBox_log.InvokeRequired)
            {
                textBox_log.AppendText(text + Environment.NewLine + Environment.NewLine);
            }
            else
            {
                textBox_log.Invoke(addText, new object[] { text });
            }
        }

        /// <summary>
        /// Запуск консольного приложения и отображение его вывода на текстбокс
        /// </summary>
        /// <param name="FileName">Файл пприложения</param>
        /// <param name="Arguments">Строка аргументов</param>
        public void StartProcessCounsole(string FileName, string Arguments)
        {
            Process p = new Process();
            p.StartInfo.FileName = FileName;
            p.StartInfo.Arguments = Arguments;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            p.StartInfo.StandardErrorEncoding = Encoding.GetEncoding(866);

            p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
            p.Exited += new EventHandler(p_Exited);
            p.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            try
            {
                if (p.Start())
                {
                    p.WaitForExit();
                    p.BeginOutputReadLine();
                    p.BeginErrorReadLine();
                    AddTextBoxText("===================================================");
                    AddTextBoxText("Запуск процесса " + p.StartInfo.FileName + " " + p.StartInfo.Arguments);
                }
                else
                {
                    AddTextBoxText("Процесс не запущен " + p.StartInfo.FileName + " " + p.StartInfo.Arguments);
                }
            }
            catch (Exception ex)
            {
                AddTextBoxText("ОШИБКА: " + ex.Message);
            }
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                AddTextBoxText(e.Data);
            }
        }

        void p_Exited(object sender, EventArgs e)
        {
            AddTextBoxText("Процесс завершен.");
        }

        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                AddTextBoxText("ОШИБКА: " + e.Data);
            }
        }
    }
}
