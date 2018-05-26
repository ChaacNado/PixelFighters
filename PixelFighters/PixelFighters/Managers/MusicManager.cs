using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    public class MusicManager
    {
        public Song song;

        public int playCounter = 1, stopCounter = 1;

        #region Variables
        private static MusicManager instance;
        #endregion

        #region Properties
        ///Skapar bara en instans av klassen
        public static MusicManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MusicManager();
                }
                return instance;
            }
        }
        #endregion

        public void Play(Song song)
        {
            this.song = song;
            if (playCounter >= 1)
            {
                MediaPlayer.Play(song);
                MediaPlayer.IsRepeating = true;
                playCounter = 0;
                stopCounter = 1;
            }
        }

        public void Stop()
        {
            if (stopCounter >= 1)
            {
                MediaPlayer.Stop();
                stopCounter = 0;
                playCounter = 1;
            }
        }
    }
}
