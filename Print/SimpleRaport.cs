using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Netcode.Common.Text;

namespace Netcode.Common.Print
{
    public partial class SimpleRaport : Form
    {
        public SimpleRaport()
        {
            InitializeComponent();
        }
        DataGridView m_datagridview;
        DataTable m_dt;
        private StringFormat m_stringFormat;
        private double m_totalWidth;
        private int m_rowPos;
        private bool m_newPage;
        private int m_pageNo;
        private string m_pageHeader = "Header";
        private string m_userName = "Printed by <username>";
        private ArrayList m_columnLefts = new ArrayList();
        private ArrayList m_columnWidths = new ArrayList();
        private int m_pageHeight = 0;
        private int m_pageCount = 0;

        public SimpleRaport(DataGridView dg, string header, string user)
        {
            InitializeComponent();
            m_datagridview = dg;
            m_pageHeader = header;
            m_userName = user;
            exporttoolStripButton.Visible = false;
            Init();
        }

        public SimpleRaport(DataTable dt, string header, string user)
        {
            InitializeComponent();
            m_dt = dt;
            dataGridView_tmp.DataSource = dt;
            dataGridView_tmp.Columns[0].Visible = false;
            dataGridView_tmp.Columns[1].Width = 150;

            m_datagridview = dataGridView_tmp;
            m_pageHeader = header;
            m_userName = user;

            Init();
        }

        private void Init()
        {
            if (m_datagridview.RowCount > 0)
            {
                printDocument.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(40, 40, 100, 100);
                //pr = new DataGridViewPrinter(dg, printDocument1, false, true, "title", new Font("Vardana", 9), Color.BlueViolet, true);
                printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument_PrintPage);
                printDocument.BeginPrint += new System.Drawing.Printing.PrintEventHandler(printDocument_BeginPrint);
                PageSettingstoolStripButton.Click += new EventHandler(PageSettingstoolStripButton_Click);
                PrintSettingstoolStripButton.Click += new EventHandler(PrintSettingstoolStripButton_Click);
                printDocument.DocumentName = "Отчет АСУ Горсвет";
                exporttoolStripButton.Click += new EventHandler(toolStripButton1_Click);
            }
            else
            {
            }
        }

        void toolStripButton1_Click(object sender, EventArgs e)
        {
            new FormConvertXLS(m_dt).ShowDialog();
        }

        private void printDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            m_stringFormat = new StringFormat();
            m_stringFormat.Alignment = StringAlignment.Near;
            m_stringFormat.LineAlignment = StringAlignment.Center;
            m_stringFormat.Trimming = StringTrimming.EllipsisCharacter;
            m_totalWidth = 0;

            foreach (DataGridViewColumn oColumn in m_datagridview.Columns)
            {
                if (oColumn.Visible)
                {
                    m_totalWidth += oColumn.Width;
                }
            }

            m_pageNo = 1;
            m_newPage = true;
            m_rowPos = 0;
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int nWidth, i;
            double nRowsPerPage = 0;
            int nTop = e.MarginBounds.Top;
            int nLeft = e.MarginBounds.Left;
            if (m_pageNo == 1)
            {
                m_columnLefts.Clear();
                m_columnWidths.Clear();
                m_pageHeight = 0;
                m_pageCount = 0;

                foreach (DataGridViewColumn oColumn in m_datagridview.Columns)
                {
                    if (oColumn.Visible)
                    {
                        double floorVal = Convert.ToDouble(oColumn.Width) / m_totalWidth * m_totalWidth * (Convert.ToDouble(e.MarginBounds.Width) / m_totalWidth);
                        nWidth = Convert.ToInt32(Math.Floor(floorVal));
                        m_pageHeight = Convert.ToInt32(e.Graphics.MeasureString(oColumn.HeaderText, oColumn.InheritedStyle.Font, nWidth).Height) + 11;
                        m_columnLefts.Add(nLeft);
                        m_columnWidths.Add(nWidth);
                        nLeft += nWidth;
                    }
                }
            }

            while (m_rowPos < m_datagridview.Rows.Count)
            {
                DataGridViewRow oRow = m_datagridview.Rows[m_rowPos];
                if (nTop + m_pageHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    DrawFooter(e, nRowsPerPage);
                    m_newPage = true;
                    m_pageNo++;
                    e.HasMorePages = true;
                    return;
                }
                else
                {
                    if (m_newPage)
                    {
                        // Draw Header
                        e.Graphics.DrawString(m_pageHeader, new Font(m_datagridview.Font, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top - e.Graphics.MeasureString(m_pageHeader, new Font(m_datagridview.Font, FontStyle.Bold), e.MarginBounds.Width).Height - 13);
                        // Draw Columns
                        nTop = e.MarginBounds.Top;
                        i = 0;
                        foreach (DataGridViewColumn oColumn in m_datagridview.Columns)
                        {
                            if (oColumn.Visible)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle((int)m_columnLefts[i], nTop, (int)m_columnWidths[i], m_pageHeight));
                                e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)m_columnLefts[i], nTop, (int)m_columnWidths[i], m_pageHeight));
                                e.Graphics.DrawString(oColumn.HeaderText, oColumn.InheritedStyle.Font, new SolidBrush(oColumn.InheritedStyle.ForeColor), new RectangleF((int)m_columnLefts[i], nTop, (int)m_columnWidths[i], m_pageHeight), m_stringFormat);
                                i++;
                            }
                        }
                        m_newPage = false;
                    }

                    nTop += m_pageHeight;
                    i = 0;

                    foreach (DataGridViewCell oCell in oRow.Cells)
                    {
                        if (oCell.Visible)
                        {
                            string sValue = String.Empty;
                            if (oCell.Value != null)
                            {
                                sValue = oCell.Value.ToString();
                            }
                            e.Graphics.DrawString(sValue, oCell.InheritedStyle.Font, new SolidBrush(oCell.InheritedStyle.ForeColor), new RectangleF((int)m_columnLefts[i], nTop, (int)m_columnWidths[i], m_pageHeight), m_stringFormat);
                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)m_columnLefts[i], nTop, (int)m_columnWidths[i], m_pageHeight));
                            i++;
                        }
                    }
                }
                m_rowPos++;
                nRowsPerPage++;
            }

            DrawFooter(e, nRowsPerPage);
            e.HasMorePages = false;
            if (printPreviewControl.Rows != m_pageCount)
            {
                printPreviewControl.Rows = m_pageCount;
            }
        }

        private void DrawFooter(System.Drawing.Printing.PrintPageEventArgs e, double nRowsPerPage)
        {
            if (m_pageCount == 0)
            {
                m_pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(m_datagridview.Rows.Count) / nRowsPerPage));
            }

            string sPageNo = String.Format("Страница {0} из {1}", m_pageNo.ToString(), m_pageCount.ToString()).ToString();
            // Right Align - User Name
            e.Graphics.DrawString(m_userName, m_datagridview.Font, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(sPageNo, m_datagridview.Font, e.MarginBounds.Width).Width), e.MarginBounds.Top + e.MarginBounds.Height + 7);
            // Left Align - Date/Time
            e.Graphics.DrawString(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString(), m_datagridview.Font, Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top + e.MarginBounds.Height + 7);
            // Center - Page No. Info
            e.Graphics.DrawString(sPageNo, m_datagridview.Font, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(sPageNo, m_datagridview.Font, e.MarginBounds.Width).Width) / 2, e.MarginBounds.Top + e.MarginBounds.Height + 31);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //if(pr!=null)
            //pr.DrawDataGridView(e.Graphics);
        }

        void PrintSettingstoolStripButton_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            } 
        }

        void PageSettingstoolStripButton_Click(object sender, EventArgs e)
        {
            if (pageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                printPreviewControl.Refresh();
            }
        }
    }
}