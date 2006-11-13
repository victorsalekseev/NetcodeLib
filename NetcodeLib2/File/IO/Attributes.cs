using System;
using System.Collections.Generic;
using System.Text;

namespace Netcode.File.IO
{
    #region Установка аттрибута на файл
    /// <summary>
    /// Установка атрибута на файл(ы)
    /// </summary>
    public class SetFileArchAttr
    {
        /// <summary>
        /// Установка атрибута на файл(ы)
        /// </summary>
        /// <param name="files">Пути к файлу(-ам)</param>
        /// <returns>Строка с ошибками (пустая, если нет ошибок)</returns>
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
                    errfile += "Нет файла \t" + file + Environment.NewLine;
                }
                catch (System.IO.IOException)
                {
                    errfile += "Ошибка ввода-вывода " + file + Environment.NewLine;
                }
                catch (System.AccessViolationException)
                {
                    errfile += "Ошибка доступа \t" + file + Environment.NewLine;
                }
                catch (System.UnauthorizedAccessException)
                {
                    errfile += "Нет прав доступа \t" + file + Environment.NewLine;
                }
                catch
                {
                    errfile += "Ошибка файла \t" + file + Environment.NewLine;
                }
            }
            return errfile;
        }
    }
    #endregion
}
