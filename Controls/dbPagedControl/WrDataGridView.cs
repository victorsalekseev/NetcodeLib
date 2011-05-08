using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Netcode.Controls.dbPagedControl
{
    /// <summary>
    /// Обертка для удобства работы с DataGridView
    /// </summary>
    public class WrDataGridView
    {
        public WrDataGridView()
        {
        }

        /// <summary>
        /// Получить выбранный Value объекта на DataGrid
        /// </summary>
        /// <returns></returns>
        public static int GetSelectedCellGridValue(int cell_id, DataGridView dgv)//OK
        {
            int id = 0;
            if (IsSelectedCellGrid(dgv))
            {
                id = Convert.ToInt32(dgv.SelectedRows[0].Cells[cell_id].Value);
            }
            return id;
        }

        /// <summary>
        /// Получить выбранный Value объекта на DataGrid
        /// </summary>
        /// <returns></returns>
        public static int GetSelectedCellGridValue(string ColumnName, DataGridView dgv)//OK
        {
            int id = 0;
            if (IsSelectedCellGrid(dgv))
            {
                id = Convert.ToInt32(dgv.SelectedRows[0].Cells[ColumnName].Value);
            }
            return id;
        }

        /// <summary>
        /// Получить выбранный Text объекта на DataGrid
        /// </summary>
        /// <returns></returns>
        public static string GetSelectedCellGridText(int cell_id, DataGridView dgv)//OK
        {
            string text = string.Empty;
            if (IsSelectedCellGrid(dgv))
            {
                if (dgv.SelectedRows[0].Cells[cell_id].Value != null)
                {
                    text = dgv.SelectedRows[0].Cells[cell_id].Value.ToString();
                }
            }
            return text;
        }

        /// <summary>
        /// Выделена ли произвольная ячейка строчки грида
        /// </summary>
        /// <returns></returns>
        public static bool IsSelectedCellGrid(DataGridView dgv)//OK
        {
            bool ret = false;
            if (GetGridRowsCount(dgv) > 0 && dgv.SelectedCells.Count > 0)
            {
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// Сколько строчек в гриде
        /// </summary>
        /// <returns></returns>
        public static int GetGridRowsCount(DataGridView dgv)//OK
        {
            return dgv.RowCount;
        }
    }
}
