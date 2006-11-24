using System;
using System.Collections.Generic;
using System.Text;

namespace Netcode.Sound
{
    public class Sound
    {
        #region �����

        /// <summary>
        /// ������������ �������� ������
        /// </summary>
        public class SoundHelp
        {
            /// <summary>
            /// ������������ �������� ������
            /// </summary>
            public SoundHelp()
            {
            }

            /// <summary>
            /// ��������������� WAV-������
            /// </summary>
            /// <param name="file">���� �� �����</param>
            /// <param name="revers">������ (������ ��� ������� ���������������) - true</param>
            /// <param name="sync">������ - true, �������� - false ���������������</param>
            public void PlayWAV(string file, bool revers, bool sync)
            {
                try
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                    player.SoundLocation = file;
                    player.LoadAsync();
                    if (revers)
                    {
                        player.PlayLooping();
                    }
                    if (sync)
                    {
                        player.Play();
                    }
                    else
                    {
                        player.PlaySync();
                    }
                }
                catch { }
            }
        }

        #endregion

    }
}
