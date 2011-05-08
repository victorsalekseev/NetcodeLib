using System;
using System.Collections.Generic;
using System.Text;

namespace Netcode.Messages
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

        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public SoundHelp()
        {
            
        }


        /// <summary>
        /// ��������������� WAV-������
        /// </summary>
        /// <param name="file">���� �� �����</param>
        /// <param name="revers">������ (������ ��� ������� ���������������) - true</param>
        public void PlayWAV(string file, bool revers)
        {
            try
            {
                //System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                player.SoundLocation = file;
                player.LoadAsync();
                if (revers)
                {
                    player.PlayLooping();
                }
                else
                {
                    player.Play();
                }
            }
            catch { }
        }

        public void StopWAV()
        {
            try
            {
                 player.Stop();
            }
            catch { }
        }
    }

    #endregion
}
