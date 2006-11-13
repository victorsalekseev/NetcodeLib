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
        /// ������ � ���� ������
        /// </summary>
        /// <param name="command">�������</param>
        /// <param name="connStr">������ �����������</param>
        /// <returns>true, ���� �������; false, ���� ���������</returns>
        public bool Insert(string command, string connStr)
        {
            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();
                OleDbTransaction trans = conn.BeginTransaction();
                OleDbCommand comm = new OleDbCommand(command, conn, trans);
                try
                {
                    // Execute ������� �� �����, � ����������� �� ����� �����
                    comm.ExecuteNonQuery();
                    // ���� �� ��, �� �������� ����������
                    trans.Commit();
                    return true;
                }
                catch (OleDbException ex)
                {
                    MessageBox.Show("��������! ��������� ������" + Environment.NewLine + Environment.NewLine + ex.Message);
                    trans.Rollback(); // ������ ����� ���������
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
