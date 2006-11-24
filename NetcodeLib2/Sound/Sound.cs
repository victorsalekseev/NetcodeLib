using System;
using System.Collections.Generic;
using System.Text;

namespace Netcode.Sound
{
    public class Sound
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
            public SoundHelp()
            {
            }

            /// <summary>
            /// Воспроизведение WAV-файлов
            /// </summary>
            /// <param name="file">Путь до файла</param>
            /// <param name="revers">Повтор (только для прямого воспроизведения) - true</param>
            /// <param name="sync">Прямое - true, обратное - false воспроизведение</param>
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
