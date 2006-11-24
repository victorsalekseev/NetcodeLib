using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace Netcode.Network.Share
{
    /// <summary>
    /// ����� ��� ������ � ������ ���������
    /// </summary>
    public class Share
    {
        /// <summary>
        /// ������ ����� �����
        /// </summary>
        /// <param name="folderName">���. ���� �� �����</param>
        /// <param name="shareName">��� �������� �������</param>
        /// <param name="maxUsers">����. ���-�� �������������</param>
        /// <param name="pwd">������</param>
        /// <returns>������ ��� ���</returns>
        public bool SetShareFolder(string folderName, string shareName, int maxUsers, string pwd)
        {
            bool result = true;
            try
            {
                // ������� ������ ManagementClass
                ManagementClass managementClass =
                                        new ManagementClass("Win32_Share");
                // ������� ������ ManagementBaseObjects
                ManagementBaseObject inParams =
                              managementClass.GetMethodParameters("Create");
                ManagementBaseObject outParams;
                // ������ ��������� ������
                inParams["Description"] = shareName;
                inParams["Name"] = shareName;
                inParams["Path"] = folderName;
                inParams["Type"] = 0x0; // Disk Drive
                inParams["MaximumAllowed"] = maxUsers;
                inParams["Password"] = pwd;
                // �������� ����� Create
                outParams = managementClass.InvokeMethod(
                                       "Create", inParams, null);
                // ��������� ���������
                if ((uint)(outParams.Properties["ReturnValue"].Value) != 0)
                {
                    throw new Exception("�� ������� ������� ������� �����.");
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
