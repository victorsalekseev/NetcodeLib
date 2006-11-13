using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Netcode.db.Query
{
    class Query
    {
        /// <summary>
        /// Запрос в базу данных
        /// </summary>
        /// <param name="command">Команда</param>
        /// <param name="connStr">строка подключения</param>
        /// <returns>true, если успешно; false, если неуспешно</returns>
        public bool Insert(string command, string connStr)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                OleDbTransaction trans = conn.BeginTransaction();
                OleDbCommand comm = new OleDbCommand(command, conn, trans);
                try
                {
                    // Execute выбирай по вкусу, в зависимости от твоих целей
                    comm.ExecuteNonQuery();
                    // Если всё ОК, то проводим транзакцию
                    trans.Commit();
                    return true;
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show("Внимание! Исправьте ошибку" + Environment.NewLine + Environment.NewLine + ex.Message);
                    trans.Rollback(); // Делаем откат изменений
                    //return;
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
