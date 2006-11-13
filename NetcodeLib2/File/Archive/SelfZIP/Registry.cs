
#region ����� AcedRegistry
/*
--------------------------------------------------------------------------------
public class AcedRegistry : IDisposable

  ������������ ��� ������ � ������ ���������� � ������ Windows.

--------------------------------------------------------------------------------
ctor
--------------------------------------------------------------------------------

public AcedRegistry(AcedBaseKey registryBaseKey, string registryKey,
  bool saveMode)

  ��������� ����� ������� Windows, �������� ���������� registryBaseKey,
  � ����� ����, ������������ �������� ���������� ���������� registryKey.
  ���� �������� saveMode ����� False, ���� ����������� � ������ ������,
  ����� ���� ����������� (��� ��������� ��� ��� ����������) � ������ ������.
  ��������, � registryBaseKey ����� �������� AcedBaseKey.LocalMachine, �
  � registryKey - ������: @"Software\CompanyName\ProgramName". ������
  ��������� �������� ��� ��������� registryBaseKey, ���������������
  ������ �� ������ �������:

  AcedBaseKey.ClassesRoot,
  AcedBaseKey.CurrentConfig,
  AcedBaseKey.CurrentUser,
  AcedBaseKey.DynData,
  AcedBaseKey.LocalMachine,
  AcedBaseKey.PerformanceData,
  AcedBaseKey.Users

--------------------------------------------------------------------------------
��������
--------------------------------------------------------------------------------

public Microsoft.Win32.RegistryKey RegistryKey { get; }

  ���������� ������ �� �������� ���� ������� Windows. ���� �������� �����
  �������� ����� null, ��� ��������, ��� ��� �������� ��� �������� �����
  � ������������ ������� ������ ��������� ������.

--------------------------------------------------------------------------------
����� Dispose
--------------------------------------------------------------------------------

public void Dispose()

  ��������� �������� ���� �������, ���� �� ��� ������, � ����������� ���������
  � ��� unmanaged-�������.

--------------------------------------------------------------------------------
������ �� ���������� ���� String
--------------------------------------------------------------------------------

public void Put(string valueName, string value)

  ��������� �������� value ���� System.String � ������ valueName.

public bool Get(string valueName, ref string value)

  ��������� �������� ���� System.String � ������ valueName � ��������� ���
  � ��������� value. ������� ���������� True � ������ ������ � False ���
  ���������� �������� � ��������� ������.

public string GetDef(string valueName, string defaultValue)

  ��������� �������� ���� System.String � ������ valueName � ���������� ���
  ��� ��������� �������. ��� ���������� �������� � ��������� ������ �������
  ���������� defaultValue.

--------------------------------------------------------------------------------
������ �� ���������� ���� Byte[]
--------------------------------------------------------------------------------

public void Put(string valueName, byte[] value)

  ��������� �������� value ���� System.Byte[] � ������ valueName.

public bool Get(string valueName, ref byte[] value)

  ��������� �������� ���� System.Byte[] � ������ valueName � ��������� ���
  � ��������� value. ������� ���������� True � ������ ������ � False ���
  ���������� �������� � ��������� ������.

public byte[] GetDef(string valueName, byte[] defaultValue)

  ��������� �������� ���� System.Byte[] � ������ valueName � ���������� ���
  ��� ��������� �������. ��� ���������� �������� � ��������� ������ �������
  ���������� defaultValue.

--------------------------------------------------------------------------------
������ �� ���������� ���� Int32
--------------------------------------------------------------------------------

public void Put(string valueName, int value)

  ��������� �������� value ���� System.Int32 � ������ valueName.

public bool Get(string valueName, ref int value)

  ��������� �������� ���� System.Int32 � ������ valueName � ��������� ���
  � ��������� value. ������� ���������� True � ������ ������ � False ���
  ���������� �������� � ��������� ������.

public int GetDef(string valueName, int defaultValue)

  ��������� �������� ���� System.Int32 � ������ valueName � ���������� ���
  ��� ��������� �������. ��� ���������� �������� � ��������� ������ �������
  ���������� defaultValue.

--------------------------------------------------------------------------------
������ �� ���������� ���� Boolean
--------------------------------------------------------------------------------

public void Put(string valueName, bool value)

  ��������� �������� value ���� System.Boolean � ������ valueName.

public bool Get(string valueName, ref bool value)

  ��������� �������� ���� System.Boolean � ������ valueName � ��������� ���
  � ��������� value. ������� ���������� True � ������ ������ � False ���
  ���������� �������� � ��������� ������.

public bool GetDef(string valueName, bool defaultValue)

  ��������� �������� ���� System.Boolean � ������ valueName � ���������� ���
  ��� ��������� �������. ��� ���������� �������� � ��������� ������ �������
  ���������� defaultValue.

--------------------------------------------------------------------------------
������ �� ���������� ���� DateTime
--------------------------------------------------------------------------------

public void Put(string valueName, DateTime value)

  ��������� �������� value ���� System.DateTime � ������ valueName.

public bool Get(string valueName, ref DateTime value)

  ��������� �������� ���� System.DateTime � ������ valueName � ��������� ���
  � ��������� value. ������� ���������� True � ������ ������ � False ���
  ���������� �������� � ��������� ������.

public DateTime GetDef(string valueName, DateTime defaultValue)

  ��������� �������� ���� System.DateTime � ������ valueName � ���������� ���
  ��� ��������� �������. ��� ���������� �������� � ��������� ������ �������
  ���������� defaultValue.

--------------------------------------------------------------------------------
������ �� ���������� ���� Decimal
--------------------------------------------------------------------------------

public void Put(string valueName, decimal value)

  ��������� �������� value ���� System.Decimal � ������ valueName.

public bool Get(string valueName, ref decimal value)

  ��������� �������� ���� System.Decimal � ������ valueName � ��������� ���
  � ��������� value. ������� ���������� True � ������ ������ � False ���
  ���������� �������� � ��������� ������.

public decimal GetDef(string valueName, decimal defaultValue)

  ��������� �������� ���� System.Decimal � ������ valueName � ���������� ���
  ��� ��������� �������. ��� ���������� �������� � ��������� ������ ��� �������
  ���������� defaultValue.

--------------------------------------------------------------------------------
������ �� ���������� ���� Double
--------------------------------------------------------------------------------

public void Put(string valueName, double value)

  ��������� �������� value ���� System.Double � ������ valueName.

public bool Get(string valueName, ref double value)

  ��������� �������� ���� System.Double � ������ valueName � ��������� ���
  � ��������� value. ������� ���������� True � ������ ������ � False ���
  ���������� �������� � ��������� ������.

public double GetDef(string valueName, double defaultValue)

  ��������� �������� ���� System.Double � ������ valueName � ���������� ���
  ��� ��������� �������. ��� ���������� �������� � ��������� ������ �������
  ���������� defaultValue.

--------------------------------------------------------------------------------
������ �� ���������� ���� Guid
--------------------------------------------------------------------------------

public void Put(string valueName, Guid value)

  ��������� �������� value ���� System.Guid � ������ valueName.

public bool Get(string valueName, ref Guid value)

  ��������� �������� ���� System.Guid � ������ valueName � ��������� ���
  � ��������� value. ������� ���������� True � ������ ������ � False ���
  ���������� �������� � ��������� ������.

public Guid GetDef(string valueName, Guid defaultValue)

  ��������� �������� ���� System.Guid � ������ valueName � ���������� ���
  ��� ��������� �������. ��� ���������� �������� � ��������� ������ �������
  ���������� defaultValue.

--------------------------------------------------------------------------------
������ �� ���������� ���� Int64
--------------------------------------------------------------------------------

public void Put(string valueName, long value)

  ��������� �������� value ���� System.Int64 � ������ valueName.

public bool Get(string valueName, ref long value)

  ��������� �������� ���� System.Int64 � ������ valueName � ��������� ���
  � ��������� value. ������� ���������� True � ������ ������ � False ���
  ���������� �������� � ��������� ������.

public long GetDef(string valueName, long defaultValue)

  ��������� �������� ���� System.Int64 � ������ valueName � ���������� ���
  ��� ��������� �������. ��� ���������� �������� � ��������� ������ �������
  ���������� defaultValue.

--------------------------------------------------------------------------------
*/
#endregion

using System;

namespace Netcode.File.Archive.SelfZIP.CryptoArch
{
    using MS = Microsoft.Win32;

    // AcedBaseKey enumeration

    public enum AcedBaseKey
    {
        ClassesRoot,
        CurrentConfig,
        CurrentUser,
        DynData,
        LocalMachine,
        PerformanceData,
        Users
    }

    // AcedRegistry class

    public class AcedRegistry : IDisposable
    {
        private MS.RegistryKey _registryKey;

        public AcedRegistry(AcedBaseKey registryBaseKey, string registryKey, bool saveMode)
        {
            switch (registryBaseKey)
            {
                case AcedBaseKey.ClassesRoot:
                    _registryKey = !saveMode
                        ? MS.Registry.ClassesRoot.OpenSubKey(registryKey, false)
                        : MS.Registry.ClassesRoot.CreateSubKey(registryKey);
                    break;
                case AcedBaseKey.CurrentConfig:
                    _registryKey = !saveMode
                        ? MS.Registry.CurrentConfig.OpenSubKey(registryKey, false)
                        : MS.Registry.CurrentConfig.CreateSubKey(registryKey);
                    break;
                case AcedBaseKey.CurrentUser:
                    _registryKey = !saveMode
                        ? MS.Registry.CurrentUser.OpenSubKey(registryKey, false)
                        : MS.Registry.CurrentUser.CreateSubKey(registryKey);
                    break;
                case AcedBaseKey.DynData:
                    _registryKey = !saveMode
                        ? MS.Registry.DynData.OpenSubKey(registryKey, false)
                        : MS.Registry.DynData.CreateSubKey(registryKey);
                    break;
                case AcedBaseKey.LocalMachine:
                    _registryKey = !saveMode
                        ? MS.Registry.LocalMachine.OpenSubKey(registryKey, false)
                        : MS.Registry.LocalMachine.CreateSubKey(registryKey);
                    break;
                case AcedBaseKey.PerformanceData:
                    _registryKey = !saveMode
                        ? MS.Registry.PerformanceData.OpenSubKey(registryKey, false)
                        : MS.Registry.PerformanceData.CreateSubKey(registryKey);
                    break;
                case AcedBaseKey.Users:
                    _registryKey = !saveMode
                        ? MS.Registry.Users.OpenSubKey(registryKey, false)
                        : MS.Registry.Users.CreateSubKey(registryKey);
                    break;
            }
        }

        public void Dispose()
        {
            if (_registryKey != null)
            {
                _registryKey.Close();
                _registryKey = null;
            }
        }

        public MS.RegistryKey RegistryKey
        {
            get { return _registryKey; }
        }

        // String values

        public void Put(string valueName, string value)
        {
            if (_registryKey != null)
                _registryKey.SetValue(valueName, value == null ? "" : value);
        }

        public bool Get(string valueName, ref string value)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                {
                    value = (string)v;
                    return true;
                }
            }
            return false;
        }

        public string GetDef(string valueName, string defaultValue)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                    defaultValue = (string)v;
            }
            return defaultValue;
        }

        // Byte[] values

        public void Put(string valueName, byte[] value)
        {
            if (_registryKey != null)
                _registryKey.SetValue(valueName, value == null ? new byte[0] : value);
        }

        public bool Get(string valueName, ref byte[] value)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                {
                    value = (byte[])v;
                    return true;
                }
            }
            return false;
        }

        public byte[] GetDef(string valueName, byte[] defaultValue)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                    defaultValue = (byte[])v;
            }
            return defaultValue;
        }

        // Int32 values

        public void Put(string valueName, int value)
        {
            if (_registryKey != null)
                _registryKey.SetValue(valueName, value);
        }

        public bool Get(string valueName, ref int value)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                {
                    value = (int)v;
                    return true;
                }
            }
            return false;
        }

        public int GetDef(string valueName, int defaultValue)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                    defaultValue = (int)v;
            }
            return defaultValue;
        }

        // Boolean values

        public void Put(string valueName, bool value)
        {
            if (_registryKey != null)
                _registryKey.SetValue(valueName, value ? 1 : 0);
        }

        public bool Get(string valueName, ref bool value)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                {
                    value = (int)v != 0;
                    return true;
                }
            }
            return false;
        }

        public bool GetDef(string valueName, bool defaultValue)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                    defaultValue = (int)v != 0;
            }
            return defaultValue;
        }

        // DateTime values

        public unsafe void Put(string valueName, DateTime value)
        {
            if (_registryKey != null)
            {
                byte[] bytes = new byte[8];
                fixed (byte* pBytes = &bytes[0])
                    *((DateTime*)pBytes) = value;
                _registryKey.SetValue(valueName, bytes);
            }
        }

        public unsafe bool Get(string valueName, ref DateTime value)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                {
                    byte[] bytes = (byte[])v;
                    fixed (byte* pBytes = &bytes[0])
                        value = *((DateTime*)pBytes);
                    return true;
                }
            }
            return false;
        }

        public unsafe DateTime GetDef(string valueName, DateTime defaultValue)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                {
                    byte[] bytes = (byte[])v;
                    fixed (byte* pBytes = &bytes[0])
                        defaultValue = *((DateTime*)pBytes);
                }
            }
            return defaultValue;
        }

        // Decimal values

        public unsafe void Put(string valueName, decimal value)
        {
            if (_registryKey != null)
            {
                byte[] bytes = new byte[16];
                fixed (byte* pBytes = &bytes[0])
                    *((decimal*)pBytes) = value;
                _registryKey.SetValue(valueName, bytes);
            }
        }

        public unsafe bool Get(string valueName, ref decimal value)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                {
                    byte[] bytes = (byte[])v;
                    fixed (byte* pBytes = &bytes[0])
                        value = *((decimal*)pBytes);
                    return true;
                }
            }
            return false;
        }

        public unsafe decimal GetDef(string valueName, decimal defaultValue)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                {
                    byte[] bytes = (byte[])v;
                    fixed (byte* pBytes = &bytes[0])
                        defaultValue = *((decimal*)pBytes);
                }
            }
            return defaultValue;
        }

        // Double values

        public unsafe void Put(string valueName, double value)
        {
            if (_registryKey != null)
            {
                byte[] bytes = new byte[8];
                fixed (byte* pBytes = &bytes[0])
                    *((double*)pBytes) = value;
                _registryKey.SetValue(valueName, bytes);
            }
        }

        public unsafe bool Get(string valueName, ref double value)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                {
                    byte[] bytes = (byte[])v;
                    fixed (byte* pBytes = &bytes[0])
                        value = *((double*)pBytes);
                    return true;
                }
            }
            return false;
        }

        public unsafe double GetDef(string valueName, double defaultValue)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                {
                    byte[] bytes = (byte[])v;
                    fixed (byte* pBytes = &bytes[0])
                        defaultValue = *((double*)pBytes);
                }
            }
            return defaultValue;
        }

        // Guid values

        public void Put(string valueName, Guid value)
        {
            if (_registryKey != null)
                _registryKey.SetValue(valueName, value.ToByteArray());
        }

        public bool Get(string valueName, ref Guid value)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                {
                    value = new Guid((byte[])v);
                    return true;
                }
            }
            return false;
        }

        public Guid GetDef(string valueName, Guid defaultValue)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                    defaultValue = new Guid((byte[])v);
            }
            return defaultValue;
        }

        // Int64 values

        public unsafe void Put(string valueName, long value)
        {
            if (_registryKey != null)
            {
                byte[] bytes = new byte[8];
                fixed (byte* pBytes = &bytes[0])
                    *((long*)pBytes) = value;
                _registryKey.SetValue(valueName, bytes);
            }
        }

        public unsafe bool Get(string valueName, ref long value)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                {
                    byte[] bytes = (byte[])v;
                    fixed (byte* pBytes = &bytes[0])
                        value = *((long*)pBytes);
                    return true;
                }
            }
            return false;
        }

        public unsafe long GetDef(string valueName, long defaultValue)
        {
            if (_registryKey != null)
            {
                object v = _registryKey.GetValue(valueName, null);
                if (v != null)
                {
                    byte[] bytes = (byte[])v;
                    fixed (byte* pBytes = &bytes[0])
                        defaultValue = *((long*)pBytes);
                }
            }
            return defaultValue;
        }
    }
}
