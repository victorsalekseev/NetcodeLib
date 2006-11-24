using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace Netcode.Network.Share
{
    /// <summary>
    /// Класс для работы с общими ресурсами
    /// </summary>
    public class Share
    {
        /// <summary>
        /// Делаем папку общей
        /// </summary>
        /// <param name="folderName">Физ. путь до папки</param>
        /// <param name="shareName">Имя сетевого ресурса</param>
        /// <param name="maxUsers">Макс. кол-во пользователей</param>
        /// <param name="pwd">Пароль</param>
        /// <returns>Удачно или нет</returns>
        public bool SetShareFolder(string folderName, string shareName, int maxUsers, string pwd)
        {
            bool result = true;
            try
            {
                // Создаем объект ManagementClass
                ManagementClass managementClass =
                                        new ManagementClass("Win32_Share");
                // Создаем объект ManagementBaseObjects
                ManagementBaseObject inParams =
                              managementClass.GetMethodParameters("Create");
                ManagementBaseObject outParams;
                // Задаем параметры вызова
                inParams["Description"] = shareName;
                inParams["Name"] = shareName;
                inParams["Path"] = folderName;
                inParams["Type"] = 0x0; // Disk Drive
                inParams["MaximumAllowed"] = maxUsers;
                inParams["Password"] = pwd;
                // Вызываем метод Create
                outParams = managementClass.InvokeMethod(
                                       "Create", inParams, null);
                // Проверяем результат
                if ((uint)(outParams.Properties["ReturnValue"].Value) != 0)
                {
                    throw new Exception("Не удалось сделать каталог общим.");
                    result = false;
                }
            }
            catch (Exception)
            {
                //Console.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }
    }
}
