using System;
using System.Collections.Generic;
using System.Text;

namespace Netcode.Messages
{
    #region Звуки

    /// <summary>
    /// Проигрывание звуковых файлов
    /// </summary>
    public class SoundHelp
    {
        /// <summary>
        /// Проигрывание звуковых файлов
        /// </summary>

        System.Media.SoundPlayer player = new System.Media.SoundPlayer();

        public SoundHelp()
        {
            
        }


        /// <summary>
        /// Воспроизведение WAV-файлов
        /// </summary>
        /// <param name="file">Путь до файла</param>
        /// <param name="revers">Повтор (только для прямого воспроизведения) - true</param>
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
