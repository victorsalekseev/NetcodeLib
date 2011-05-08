using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Netcode.Messages;
using Netcode.Controls.dbPagedControl;

namespace Netcode.Controls
{
    public partial class MSQGrid : UserControl
    {
        /// <summary>
        /// при запросе на создание позиции
        /// </summary>
        /// <param name="tbl_key_value">Выбранная позиция</param>
        public delegate void OnAddNewItem(object tbl_key_value);
        /// <summary>
        /// Вызывается при запросе на создание позиции
        /// </summary>
        public event OnAddNewItem AddNewItem;

        /// <summary>
        /// при запросе на изменение позиции
        /// </summary>
        /// <param name="tbl_key_value">Выбранная позиция</param>
        public delegate void OnEditItem(object tbl_key_value);
        /// <summary>
        /// Вызывается при запросе на изменении позиции
        /// </summary>
        public event OnEditItem EditItem;

        /// <summary>
        /// при запросе на удаление позиции
        /// </summary>
        /// <param name="tbl_key_value">Выбранная позиция</param>
        public delegate void OnDeleteItem(object tbl_key_value);
        /// <summary>
        /// Вызывается при запросе на удаление позиции
        /// </summary>
        public event OnDeleteItem DeleteItem;

        public delegate void OnSelectPos(DataGridViewSelectedCellCollection Pos);
        public event OnSelectPos SelectPos;

        public delegate void OnDoubleClickCell(DataGridViewSelectedCellCollection Pos);
        public event OnDoubleClickCell DoubleClickCell;

        public MSQGrid()
        {
            InitializeComponent();
            cB_per_page.SelectedItem = "500";
            cB_per_page.SelectedIndexChanged += new EventHandler(cB_per_page_SelectedIndexChanged);
            btn_fwd.Click += new System.EventHandler(this.btn_fwd_Click);
            btn_prev.Click += new System.EventHandler(this.btn_prev_Click);
            dataGridView_db.Focus();
            добавитьToolStripMenuItem.Click += new EventHandler(добавитьToolStripMenuItem_Click);
            изменитьToolStripMenuItem.Click += new EventHandler(изменитьToolStripMenuItem_Click);
            удалитьToolStripMenuItem.Click += new EventHandler(удалитьToolStripMenuItem_Click);
            contextMenuStrip_m.Opening += new CancelEventHandler(contextMenuStrip_m_Opening);
            ActiveControl = dataGridView_db;
            //if (!string.IsNullOrEmpty(connection_string))
            //{
            //    dataGridView_db.DataSource = Generate(int.Parse(cB_per_page.SelectedItem.ToString()), ++g_page, btn_prev, btn_fwd);
            //}
        }

        void contextMenuStrip_m_Opening(object sender, CancelEventArgs e)
        {
            if (WrDataGridView.IsSelectedCellGrid(dataGridView_db))
            {
                изменитьToolStripMenuItem.Enabled = true;
                удалитьToolStripMenuItem.Enabled = true;
            }
            else
            {
                изменитьToolStripMenuItem.Enabled = false;
                удалитьToolStripMenuItem.Enabled = false;
            }
        }

        void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DeleteItem != null)
            {
                if (dataGridView_db.RowCount>0 && dataGridView_db.SelectedRows.Count>0)
                {
                    DeleteItem.Invoke(dataGridView_db.SelectedRows[0].Cells[0].Value);
                }
            }
        }

        void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditItem != null)
            {
                if (IsSelectedCellGrid())
                {
                    EditItem.Invoke(dataGridView_db.SelectedRows[0].Cells[0].Value);
                }
            }
        }

        void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AddNewItem != null)
            {
                if (IsSelectedCellGrid())
                {
                    AddNewItem.Invoke(dataGridView_db.SelectedRows[0].Cells[0].Value);
                }
                else
                {
                    AddNewItem.Invoke(null);
                }
            }
        }

        int g_page = 0;
        string _tbl_name = "REG";
        string _tbl_key_field = "KEY";
        string _tbl_select_field = "NAIM + ' ' + NAIM";
        string _tbl_where_query = string.Empty;//"id_place IN (1,2)";
        string _tbl_order_field = "NAIM";
        string connection_string = string.Empty;


        /// <summary>
        /// Строка подключения
        /// </summary>
        public String Connection_string
        {
            get
            {
                return connection_string;
            }
            set
            {
                connection_string = value;
                dataGridView_db.DataSource = Generate(int.Parse(cB_per_page.SelectedItem.ToString()), 1, btn_prev, btn_fwd);
                g_page = 1;
            }
        }

        /// <summary>
        /// Имя таблицы
        /// </summary>
        public String Tbl_name
        {
            get
            {
                return _tbl_name;
            }
            set
            {
                _tbl_name = value;
            }
        }

        /// <summary>
        /// Ключевое поле таблицы
        /// </summary>
        public String Tbl_key_field
        {
            get
            {
                return _tbl_key_field;
            }
            set
            {
                _tbl_key_field = value;
            }
        }

        /// <summary>
        /// Сортировка по умолчанию по полю таблицы
        /// </summary>
        public String Tbl_order_field
        {
            get
            {
                return _tbl_order_field;
            }
            set
            {
                _tbl_order_field = value;
            }
        }

        /// <summary>
        /// Условие отбора, i.e.: id_place=1.
        /// </summary>
        public String Tbl_where_query
        {
            get
            {
                return _tbl_where_query;
            }
            set
            {
                _tbl_where_query = value;
            }
        }

        /// <summary>
        /// Запрашиваемое поле
        /// </summary>
        public String Tbl_select_field
        {
            get
            {
                return _tbl_select_field;
            }
            set
            {
                _tbl_select_field = value;
            }
        }

        /// <summary>
        /// Название столбцов
        /// </summary>
        public string[] DataGridColumnsHeaderText
        {
            set
            {
                int iteration = dataGridView_db.Columns.Count;
                if (value.Length < iteration)
                {
                    iteration = value.Length;
                }
                for (int i = 0; i < iteration; i++)
                {
                    dataGridView_db.Columns[i].HeaderText = value[i];
                }
                
            }
        }

        /// <summary>
        /// Ширина столбцов
        /// </summary>
        public int[] DataGridColumnsWidth
        {
            set
            {
                int iteration = dataGridView_db.Columns.Count;
                if (value.Length < iteration)
                {
                    iteration = value.Length;
                }
                for (int i = 0; i < iteration; i++)
                {
                    dataGridView_db.Columns[i].Width = value[i];
                }

            }
        }

        /// <summary>
        /// Кол-во записей
        /// </summary>
        public Int32 RowCount
        {
            get
            {
                return dataGridView_db.RowCount;
            }
        }

        /// <summary>
        /// Текущий набор данных
        /// </summary>
        public DataTable DataSource
        {
            get
            {
                return (DataTable)dataGridView_db.DataSource;
            }
        }

        /// <summary>
        /// Расширить столбец по всей длине таблицы
        /// </summary>
        /// <param name="num_clmn">Индексный номер столбца</param>
        public void SetFullWidthColumn(int num_clmn)
        {
            if (dataGridView_db.Columns.Count >= num_clmn + 1)
            {
                dataGridView_db.Columns[num_clmn].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        /// <summary>
        /// Обновить текущий набор данных
        /// </summary>
        public void UpdateData()
        {
            int pos = 0; ;
            if (IsSelectedCellGrid())
            {
                pos = dataGridView_db.CurrentRow.Index;
            }
            dataGridView_db.DataSource = Generate(int.Parse(cB_per_page.SelectedItem.ToString()), g_page, btn_prev, btn_fwd);

            if (dataGridView_db.RowCount > pos)
            {
                dataGridView_db.Rows[pos].Cells[0].Selected = true;
            }
        }

        private DataTable Generate(int page_size, int page_num, Button btn_p, Button btn_f)
        {
            //Wait relax = new Wait(this.Bounds);
            try
            {
                //relax.ShowWait(true);
                btn_p.Enabled = true;
                btn_f.Enabled = true;
                int pMp = page_num * page_size;
                string local_wqc = string.Empty;
                string local_wqs = string.Empty;
                if (!string.IsNullOrEmpty(_tbl_where_query))
                {
                    local_wqc = " where " + _tbl_where_query;
                    local_wqs = "(" + _tbl_where_query + ") AND ";
                }               

                #region Заполнение 0

                System.Data.SqlClient.SqlDataAdapter adapter_0 = new System.Data.SqlClient.SqlDataAdapter("select count([" + _tbl_name + "].[" + _tbl_key_field + "]) from [" + _tbl_name + "] " + local_wqc, connection_string);
                System.Data.DataTable topics_0 = new System.Data.DataTable();
                adapter_0.Fill(topics_0);
                int num_all_rows = Int32.Parse(topics_0.Rows[0][0].ToString());

                if (page_num == 1)
                {
                    btn_p.Enabled = false;
                    btn_f.Enabled = true;
                    if (pMp == num_all_rows)//если знаем, что будет только 1 страница
                    {
                        btn_f.Enabled = false;
                    }
                }

                if (pMp > num_all_rows)
                {
                    if (page_size > (pMp - num_all_rows))
                    {
                        page_size = page_size - (pMp - num_all_rows);
                    }
                    pMp = num_all_rows;
                    btn_p.Enabled = true;
                    if (page_num == 1)//но если это первая страница, значит, что прсто записей оч. мало
                    {
                        btn_p.Enabled = false;
                    }
                    btn_f.Enabled = false;
                }
                lb_info.Text = "Страница " + page_num.ToString() + ". Показано с " + string.Format("{0}", pMp - page_size + 1) + " по " + pMp.ToString() + " из " + num_all_rows.ToString() + " записей.";
                #endregion

                string g_command = "" +
                    "SELECT " + _tbl_key_field + ", " + _tbl_select_field + " FROM      [" + _tbl_name + "] " +
                    "WHERE " + local_wqs + "  " + _tbl_name + "." + _tbl_key_field + " IN " +
                    "                    (SELECT   TOP " + page_size.ToString() + " " + _tbl_name + "." + _tbl_key_field + " " +
                    "                     FROM      [" + _tbl_name + "] " +
                    "                     WHERE " + local_wqs + "  " + _tbl_name + "." + _tbl_key_field + " IN " +
                    "                                         (SELECT   TOP " + pMp.ToString() + " " + _tbl_name + "." + _tbl_key_field + " " +
                    "                                          FROM      [" + _tbl_name + "] " + local_wqc + 
                    "                                          ORDER BY " + _tbl_name + "." + _tbl_key_field + ") " +
                    "                     ORDER BY " + _tbl_name + "." + _tbl_key_field + " DESC) " +
                    "ORDER BY [" + _tbl_name + "]." + _tbl_order_field + " ";

                #region Заполнение A
                System.Data.SqlClient.SqlDataAdapter adapter_a = new System.Data.SqlClient.SqlDataAdapter(g_command, connection_string);
                System.Data.DataTable topics = new System.Data.DataTable();
                adapter_a.Fill(topics);
                #endregion
                return topics;
            }
            catch (Exception)
            {
                return new DataTable();
            }
            finally
            {
                //relax.ShowWait(false);
            }
        }

        void cB_per_page_SelectedIndexChanged(object sender, EventArgs e)
        {
            int RowOnPage = 0;
            if(!string.IsNullOrEmpty(cB_per_page.SelectedItem.ToString()))
            {
                int.TryParse(cB_per_page.SelectedItem.ToString(), out RowOnPage);
            }
            dataGridView_db.DataSource = Generate(RowOnPage, 1, btn_prev, btn_fwd);
            g_page = 1;
            ActiveControl = dataGridView_db;
        }

        private void btn_fwd_Click(object sender, EventArgs e)
        {
            dataGridView_db.DataSource = Generate(int.Parse(cB_per_page.SelectedItem.ToString()), ++g_page, btn_prev, btn_fwd);
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            //if (g_page > 1)
            {
                dataGridView_db.DataSource = Generate(int.Parse(cB_per_page.SelectedItem.ToString()), --g_page, btn_prev, btn_fwd);
            }
        }

        private void dataGridView_db_SelectionChanged(object sender, EventArgs e)
        {
            if (WrDataGridView.IsSelectedCellGrid(dataGridView_db) && SelectPos != null)
                    SelectPos.Invoke(dataGridView_db.SelectedCells);
        }

        public bool IsSelectedCellGrid()
        {
            return WrDataGridView.IsSelectedCellGrid(dataGridView_db);
        }

        private void dataGridView_db_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

        }

        private void dataGridView_db_DoubleClick(object sender, EventArgs e)
        {
            if (DoubleClickCell != null)
            {
                if (IsSelectedCellGrid())
                {
                    DoubleClickCell.Invoke(dataGridView_db.SelectedCells);
                }
            }
        }
    }

}
