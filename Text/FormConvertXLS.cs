using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Netcode.Messages;

namespace Netcode.Common.Text
{
    public partial class FormConvertXLS : Form
    {
        DataTable _dt = null;

        public FormConvertXLS()
        {
            InitializeComponent();
        }

        public FormConvertXLS(DataTable dt)
        {
            InitializeComponent();
            _dt = dt;
            this.Shown += new EventHandler(FormConvertXLS_Shown);
        }

        void FormConvertXLS_Shown(object sender, EventArgs e)
        {
            try
            {
                int numrows = _dt.Rows.Count;
                int numcol = _dt.Columns.Count;

                progressBar.Maximum = numrows;

                string build_cvs = string.Empty;
                string tmp_cvs = string.Empty;

                for (int k = 0; k < numcol; k++)//назв. столбц.
                {
                    build_cvs += ";\"" + _dt.Columns[k].ColumnName + "\"";
                }
                if (build_cvs.Length > 0)
                {
                    build_cvs = build_cvs.Substring(1);
                }
                build_cvs += Environment.NewLine;

                for (int i = 0; i < numrows; i++)
                {
                    for (int j = 0; j < numcol; j++)//столбцы
                    {
                        switch (_dt.Columns[j].DataType.Name)
                        {
                            case "Float":
                            case "Double":
                            case "Int32":
                                {
                                    tmp_cvs += ";" + _dt.Rows[i][j].ToString();
                                }
                                break;
                            default:
                                {
                                    tmp_cvs += ";\"" + _dt.Rows[i][j].ToString() + "\"";
                                }
                                break;
                        }
                        Application.DoEvents();
                        progressBar.Value = numrows;
                    }
                    if (tmp_cvs.Length > 0)
                    {
                        tmp_cvs = tmp_cvs.Substring(1);
                    }
                    tmp_cvs += Environment.NewLine;
                    build_cvs += tmp_cvs;
                    tmp_cvs = string.Empty;
                }
                Application.DoEvents();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.GetEncoding(1251)))
                    {
                        sw.Write(build_cvs);
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                new CriticalErrors().PrintError("C3", "Ошибка конвертации: "+ex.Message + ex.TargetSite);
            }
            finally
            {
                this.Close();
            }
        }
    }
}