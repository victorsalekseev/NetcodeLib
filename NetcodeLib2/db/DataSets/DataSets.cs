/**
        DataSet dataSet1 = new DataSet();
        DataTable tbl = new DataTable("tbl");

        public Form1()
        {
            InitializeComponent();
            dataSet1.Tables.Add(tbl);
            tbl.Columns.Add("sss1");
            tbl.Columns.Add("sss2");

            dataGridView1.DataSource = dataSet1.Tables["tbl"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Netcode.db.DataSets.DataSets ds = new Netcode.db.DataSets.DataSets();
            dataSet1 = ds.LoadInFile(string.Empty);
            dataGridView1.DataSource = dataSet1.Tables["tbl"]; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Netcode.db.DataSets.DataSets ds = new Netcode.db.DataSets.DataSets();
            dataGridView1.DataSource = dataSet1.Tables["tbl"];
            ds.SaveInFile(string.Empty, dataSet1);
        }
 */


using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace Netcode.db.DataSets
{
    public class DataSets
    {
        /// <summary>
        /// Загружает данные в DataSet из файла
        /// </summary>
        /// <param name="filename">Имя файла. Если нет - вызовет диалог открытия файла</param>
        /// <returns>DataSet</returns>
        public DataSet LoadInFile(string filename)
        {
            DataSet ds = new DataSet();
            if (!System.IO.File.Exists(filename))
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "XML|*.xml";
                openFileDialog1.Title = "Open an XML File";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamReader sr = new
                        System.IO.StreamReader(openFileDialog1.FileName, Encoding.UTF8);
                    ds.ReadXml(sr);
                    sr.Close();
                }
                return ds;
            }
            else
            {
                ds.ReadXml(filename);
                return ds;
            }
        }

        /// <summary>
        /// Сохранение датасета в файл. Если filename is NullOrEmpry, вызовет диалог сохранения файла
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <returns></returns>
        public bool SaveInFile(string filename, DataSet ds)
        {
            if (string.IsNullOrEmpty(filename))
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "XML|*.xml";
                saveFileDialog1.Title = "Save an XML File";
                saveFileDialog1.ShowDialog();
                if ((!string.IsNullOrEmpty(saveFileDialog1.FileName)))
                {
                    try
                    {
                        System.IO.FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate);
                        ds.WriteXml(fs);
                        fs.Close();
                        saveFileDialog1.FileName = string.Empty;
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    System.IO.FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
                    ds.WriteXml(fs);
                    fs.Close();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
