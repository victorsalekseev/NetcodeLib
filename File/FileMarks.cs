using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Netcode.Common.File
{
    public class FileMarks
    {
        Dictionary<string, FileTypes> d_fm = new Dictionary<string, FileTypes>();

        public FileMarks()
        {
            d_fm.Add(BitConverter.ToString(new byte[] { 0xff, 0xd8, 0xff, 0xe0 }).ToLowerInvariant(), FileTypes.JPG);
            d_fm.Add(BitConverter.ToString(new byte[] { 0x89, 0x50, 0x4e, 0x47 }).ToLowerInvariant(), FileTypes.PNG);
            d_fm.Add(BitConverter.ToString(new byte[] { 0x47, 0x49, 0x46, 0x38/*, 0x39*/ }).ToLowerInvariant(), FileTypes.GIF);
            d_fm.Add(BitConverter.ToString(new byte[] { 0x42, 0x4d, 0x00, 0x00 }).ToLowerInvariant(), FileTypes.BMP);
        }

        /// <summary>
        /// Известные типы файлов
        /// </summary>
        public enum FileTypes
        {
            JPG = 10,
            PNG = 15,
            GIF = 17,
            BMP = 18,
            TXT = 100,
            UNSUPPORTED = TXT
        };

        public object GetFileType(byte[] b)
        {
            if (b[0] == 0x42 && b[1] == 0x4d)
            {
                return FileTypes.BMP;
            }

            if (d_fm.ContainsKey(BitConverter.ToString(b).ToLowerInvariant()))//там, где можно выровнять на 4
            {
                return (FileTypes)d_fm[BitConverter.ToString(b).ToLowerInvariant()];
            }
            else
            {
                return FileTypes.UNSUPPORTED;
            }
        }

        public bool IsTextFile(string Exstension)
        {
            switch (Exstension)
            {
                case ".txt":
                case ".log":
                case ".nfo":
                    {
                        return true;
                    }

                default:
                    {
                        return false;
                    }

            }
        }

        public bool IsImageFile(string Exstension)
        {
            switch (Exstension.ToLower())
            {
                case ".jpg":
                case ".png":
                case ".gif":
                case ".bmp":
                    {
                        return true;
                    }

                default:
                    {
                        return false;
                    }

            }
        }
    }
}
