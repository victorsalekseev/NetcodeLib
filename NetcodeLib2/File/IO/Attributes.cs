using System;
using System.Collections.Generic;
using System.Text;

namespace Netcode.File.IO
{
    #region ��������� ��������� �� ����
    /// <summary>
    /// ��������� �������� �� ����(�)
    /// </summary>
    public class SetFileArchAttr
    {
        /// <summary>
        /// ��������� �������� �� ����(�)
        /// </summary>
        /// <param name="files">���� � �����(-��)</param>
        /// <returns>������ � �������� (������, ���� ��� ������)</returns>
        public string SetFileAttr(string[] files)
        {
            string errfile = string.Empty;
            foreach (string file in files)
            {
                try
                {
                    System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal | System.IO.FileAttributes.Archive);
                }
                catch (System.IO.FileNotFoundException)
                {
                    errfile += "��� ����� \t" + file + Environment.NewLine;
                }
                catch (System.IO.IOException)
                {
                    errfile += "������ �����-������ " + file + Environment.NewLine;
                }
                catch (System.AccessViolationException)
                {
                    errfile += "������ ������� \t" + file + Environment.NewLine;
                }
                catch (System.UnauthorizedAccessException)
                {
                    errfile += "��� ���� ������� \t" + file + Environment.NewLine;
                }
                catch
                {
                    errfile += "������ ����� \t" + file + Environment.NewLine;
                }
            }
            return errfile;
        }
    }
    #endregion
}
