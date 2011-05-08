using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Netcode.Controls
{
    public partial class InputUsed : UserControl
    {
        public InputUsed()
        {
            InitializeComponent();
            openToolStripButton.Click += new EventHandler(openToolStripButton_Click);
            saveToolStripButton.Click += new EventHandler(saveToolStripButton_Click);
            tabControl.SelectedIndexChanged+=new EventHandler(tabControl_TabIndexChanged);
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        void saveToolStripButton_Click(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    {
                        saveFileDialog.Title = "Сохранить список новых значений в файл";
                        saveFileDialog.FileName = "Новые " + _dataname + ".txt";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                            {
                                sw.Write(textBoxInput.Text);
                                sw.Close();
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        saveFileDialog.Title = "Сохранить список использованных значений в файл";
                        saveFileDialog.FileName = "Использованные " + _dataname + ".txt";
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                            {
                                sw.Write(textBoxUsed.Text);
                                sw.Close();
                            }
                        }
                    }
                    break;
                default:
                    {
                    }
                    break;
            }
        }

        void tabControl_TabIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    {
                        openToolStripButton.Enabled = true;
                    } 
                    break;
                case 1:
                    {
                        openToolStripButton.Enabled = false;
                    }
                    break;
                default:
                    {
                    }
                    break;
            }
        }

        void openToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(openFileDialog.FileName, Encoding.UTF8))
                {
                    textBoxInput.Text = sr.ReadToEnd();
                    sr.Close();
                }
            }
        }

        /// <summary>
        /// Название модуля
        /// </summary>
        public string TitleText
        {
            get { return groupBox.Text; }
            set { groupBox.Text = value; }
        }

        string _dataname = string.Empty;
        /// <summary>
        /// Название данных
        /// </summary>
        public string DataName
        {
            get { return _dataname; }
            set { _dataname = value; }
        }

        /// <summary>
        /// Новые данные
        /// </summary>
        public string[] DataInput
        {
            get { return textBoxInput.Lines; }
            set { textBoxInput.Lines = value; }
        }

        /// <summary>
        /// Использованные данные
        /// </summary>
        public string[] DataUsed
        {
            get { return textBoxUsed.Lines; }
            set { textBoxUsed.Lines = value; }
        }

        /// <summary>
        /// Только для чтения
        /// </summary>
        public bool DataInputReadOnly
        {
            get { return textBoxInput.ReadOnly; }
            set { textBoxInput.ReadOnly = value; }
        }

        /// <summary>
        /// Внести уникальные использованные данные (повторов строк не будет)
        /// </summary>
        /// <param name="Line"></param>
        public void DataUsedOriginalAdd(string Line)
        {
            bool isContains = false;
            foreach (string var in textBoxUsed.Lines)
            {
                if (Line.ToLower() == var.ToLower())
                {
                    isContains = true;
                }
            }
            if (!isContains)
            {
                textBoxUsed.AppendText(Line + Environment.NewLine);
            }
        }

        /// <summary>
        /// Удалить строку новых данных (повторы тоже)
        /// </summary>
        /// <param name="Line"></param>
        public void DataInputRemoveAt(string Line)
        {
            string[] lines = textBoxInput.Lines;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].ToLower() == Line.ToLower())
                {
                    lines[i] = string.Empty;
                }
            }
            textBoxInput.Lines = lines;
        }

        /// <summary>
        /// Внести новые данные из файла
        /// </summary>
        /// <param name="File"></param>
        public void DataInputAddFromFile(string File)
        {
            using (StreamReader sr = new StreamReader(File, Encoding.UTF8))
            {
                textBoxInput.Text = sr.ReadToEnd();
                sr.Close();
            }
        }
    }


}
