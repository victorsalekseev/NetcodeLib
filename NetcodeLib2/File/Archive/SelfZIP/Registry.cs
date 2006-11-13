
#region Класс AcedRegistry
/*
--------------------------------------------------------------------------------
public class AcedRegistry : IDisposable

  Предназначен для чтения и записи информации в реестр Windows.

--------------------------------------------------------------------------------
ctor
--------------------------------------------------------------------------------

public AcedRegistry(AcedBaseKey registryBaseKey, string registryKey,
  bool saveMode)

  Открывает ветвь реестра Windows, заданную параметром registryBaseKey,
  а затем ключ, наименование которого передается параметром registryKey.
  Если параметр saveMode равен False, ключ открывается в режиме чтения,
  иначе ключ открывается (или создается при его отсутствии) в режиме записи.
  Например, в registryBaseKey можно передать AcedBaseKey.LocalMachine, а
  в registryKey - строку: @"Software\CompanyName\ProgramName". Список
  возможных значений для параметра registryBaseKey, соответствующих
  каждой из ветвей реестра:

  AcedBaseKey.ClassesRoot,
  AcedBaseKey.CurrentConfig,
  AcedBaseKey.CurrentUser,
  AcedBaseKey.DynData,
  AcedBaseKey.LocalMachine,
  AcedBaseKey.PerformanceData,
  AcedBaseKey.Users

--------------------------------------------------------------------------------
Свойство
--------------------------------------------------------------------------------

public Microsoft.Win32.RegistryKey RegistryKey { get; }

  Возвращает ссылку на открытый ключ реестра Windows. Если значение этого
  свойства равно null, это означает, что при открытии или создании ключа
  в конструкторе данного класса произошла ошибка.

--------------------------------------------------------------------------------
Метод Dispose
--------------------------------------------------------------------------------

public void Dispose()

  Закрывает открытый ключ реестра, если он был открыт, и освобождает связанные
  с ним unmanaged-ресурсы.

--------------------------------------------------------------------------------
Работа со значениями типа String
--------------------------------------------------------------------------------

public void Put(string valueName, string value)

  Сохраняет значение value типа System.String с именем valueName.

public bool Get(string valueName, ref string value)

  Считывает значение типа System.String с именем valueName и сохраняет его
  в параметре value. Функция возвращает True в случае успеха и False при
  отсутствии значения с указанным именем.

public string GetDef(string valueName, string defaultValue)

  Считывает значение типа System.String с именем valueName и возвращает его
  как результат функции. При отсутствии значения с указанным именем функция
  возвращает defaultValue.

--------------------------------------------------------------------------------
Работа со значениями типа Byte[]
--------------------------------------------------------------------------------

public void Put(string valueName, byte[] value)

  Сохраняет значение value типа System.Byte[] с именем valueName.

public bool Get(string valueName, ref byte[] value)

  Считывает значение типа System.Byte[] с именем valueName и сохраняет его
  в параметре value. Функция возвращает True в случае успеха и False при
  отсутствии значения с указанным именем.

public byte[] GetDef(string valueName, byte[] defaultValue)

  Считывает значение типа System.Byte[] с именем valueName и возвращает его
  как результат функции. При отсутствии значения с указанным именем функция
  возвращает defaultValue.

--------------------------------------------------------------------------------
Работа со значениями типа Int32
--------------------------------------------------------------------------------

public void Put(string valueName, int value)

  Сохраняет значение value типа System.Int32 с именем valueName.

public bool Get(string valueName, ref int value)

  Считывает значение типа System.Int32 с именем valueName и сохраняет его
  в параметре value. Функция возвращает True в случае успеха и False при
  отсутствии значения с указанным именем.

public int GetDef(string valueName, int defaultValue)

  Считывает значение типа System.Int32 с именем valueName и возвращает его
  как результат функции. При отсутствии значения с указанным именем функция
  возвращает defaultValue.

--------------------------------------------------------------------------------
Работа со значениями типа Boolean
--------------------------------------------------------------------------------

public void Put(string valueName, bool value)

  Сохраняет значение value типа System.Boolean с именем valueName.

public bool Get(string valueName, ref bool value)

  Считывает значение типа System.Boolean с именем valueName и сохраняет его
  в параметре value. Функция возвращает True в случае успеха и False при
  отсутствии значения с указанным именем.

public bool GetDef(string valueName, bool defaultValue)

  Считывает значение типа System.Boolean с именем valueName и возвращает его
  как результат функции. При отсутствии значения с указанным именем функция
  возвращает defaultValue.

--------------------------------------------------------------------------------
Работа со значениями типа DateTime
--------------------------------------------------------------------------------

public void Put(string valueName, DateTime value)

  Сохраняет значение value типа System.DateTime с именем valueName.

public bool Get(string valueName, ref DateTime value)

  Считывает значение типа System.DateTime с именем valueName и сохраняет его
  в параметре value. Функция возвращает True в случае успеха и False при
  отсутствии значения с указанным именем.

public DateTime GetDef(string valueName, DateTime defaultValue)

  Считывает значение типа System.DateTime с именем valueName и возвращает его
  как результат функции. При отсутствии значения с указанным именем функция
  возвращает defaultValue.

--------------------------------------------------------------------------------
Работа со значениями типа Decimal
--------------------------------------------------------------------------------

public void Put(string valueName, decimal value)

  Сохраняет значение value типа System.Decimal с именем valueName.

public bool Get(string valueName, ref decimal value)

  Считывает значение типа System.Decimal с именем valueName и сохраняет его
  в параметре value. Функция возвращает True в случае успеха и False при
  отсутствии значения с указанным именем.

public decimal GetDef(string valueName, decimal defaultValue)

  Считывает значение типа System.Decimal с именем valueName и возвращает его
  как результат функции. При отсутствии значения с указанным именем или функция
  возвращает defaultValue.

--------------------------------------------------------------------------------
Работа со значениями типа Double
--------------------------------------------------------------------------------

public void Put(string valueName, double value)

  Сохраняет значение value типа System.Double с именем valueName.

public bool Get(string valueName, ref double value)

  Считывает значение типа System.Double с именем valueName и сохраняет его
  в параметре value. Функция возвращает True в случае успеха и False при
  отсутствии значения с указанным именем.

public double GetDef(string valueName, double defaultValue)

  Считывает значение типа System.Double с именем valueName и возвращает его
  как результат функции. При отсутствии значения с указанным именем функция
  возвращает defaultValue.

--------------------------------------------------------------------------------
Работа со значениями типа Guid
--------------------------------------------------------------------------------

public void Put(string valueName, Guid value)

  Сохраняет значение value типа System.Guid с именем valueName.

public bool Get(string valueName, ref Guid value)

  Считывает значение типа System.Guid с именем valueName и сохраняет его
  в параметре value. Функция возвращает True в случае успеха и False при
  отсутствии значения с указанным именем.

public Guid GetDef(string valueName, Guid defaultValue)

  Считывает значение типа System.Guid с именем valueName и возвращает его
  как результат функции. При отсутствии значения с указанным именем функция
  возвращает defaultValue.

--------------------------------------------------------------------------------
Работа со значениями типа Int64
--------------------------------------------------------------------------------

public void Put(string valueName, long value)

  Сохраняет значение value типа System.Int64 с именем valueName.

public bool Get(string valueName, ref long value)

  Считывает значение типа System.Int64 с именем valueName и сохраняет его
  в параметре value. Функция возвращает True в случае успеха и False при
  отсутствии значения с указанным именем.

public long GetDef(string valueName, long defaultValue)

  Считывает значение типа System.Int64 с именем valueName и возвращает его
  как результат функции. При отсутствии значения с указанным именем функция
  возвращает defaultValue.

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
