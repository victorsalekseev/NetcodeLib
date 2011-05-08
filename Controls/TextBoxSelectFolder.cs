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
    public partial class TextBoxSelectFolder : UserControl
    {
        public const string mydoc = "<��� ���������>";
        public const string deskt = "<������� ����>";
        public const string basepath = "<������� � ����������>";

        public TextBoxSelectFolder()
        {
            InitializeComponent();
            button_choose.Click += new EventHandler(button_choose_Click);

            comboBox_path.Items.Add(basepath);
            comboBox_path.Items.Add(mydoc);
            comboBox_path.Items.Add(deskt);
        }

        void button_choose_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(this.InputText))
            {
                folderBrowserDialog.SelectedPath = this.InputText;
            }

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string selpath = folderBrowserDialog.SelectedPath;
                comboBox_path.Text = selpath;
            }
        }

        public string InputText
        {
            get {
                return get_std_path(comboBox_path.Text);
            }
            set {
                comboBox_path.Text = set_std_path(value);
            }
        }

        /// <summary>
        /// ��� ������������� ������������� ���������
        /// ������������ ����������� ���� �����, �����
        /// ��� "������� ����", "��� ���������". �������
        /// �������� ������, ����� ���� �������� ��������
        /// ���� �� �������� ���������.
        /// </summary>
        /// <param name="selpath">���������� �������� � ����������</param>
        /// <returns>��������������, ���������������</returns>
        public static string get_std_path(string selpath)
        {
            switch (selpath)
            {
                case basepath:
                    {
                        selpath = AppDomain.CurrentDomain.BaseDirectory;
                    }
                    break;

                case deskt:
                    {
                        selpath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    }
                    break;

                case mydoc:
                    {
                        selpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    }
                    break;

                default:
                    break;
            }
            return selpath;
        }

        public static string set_std_path(string selpath)
        {
            if (selpath == AppDomain.CurrentDomain.BaseDirectory)
            {
                selpath = basepath;
            }

            if (selpath == Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory))
            {
                selpath = deskt;
            }

            if (selpath == Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments))
            {
                selpath = mydoc;
            }

            return selpath;
        }
    }
}
