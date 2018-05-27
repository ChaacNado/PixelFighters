using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    public class SoundManager
    {

        #region Variables
        public Song song;

        public int playCounter = 1, stopCounter = 1;

        public SoundEffect blip = AssetManager.Instance.blip;
        public SoundEffect blaster = AssetManager.Instance.blaster;
        public SoundEffect crack = AssetManager.Instance.crack;
        public SoundEffect hardhit = AssetManager.Instance.hardhit;
        public SoundEffect hardhit2 = AssetManager.Instance.hardhit2;
        public SoundEffect mediumhit = AssetManager.Instance.mediumhit;
        public SoundEffect poke = AssetManager.Instance.poke;
        public SoundEffect slap = AssetManager.Instance.slap;
        public SoundEffect slap2 = AssetManager.Instance.slap2;
        public SoundEffect smallswing = AssetManager.Instance.smallswing;
        public SoundEffect smallswing2 = AssetManager.Instance.smallswing2;
        public SoundEffect smallswing3 = AssetManager.Instance.smallswing3;
        public SoundEffect softhit = AssetManager.Instance.softhit;
        public SoundEffect swing = AssetManager.Instance.swing;
        public SoundEffect woosh = AssetManager.Instance.woosh;


        private static SoundManager instance;
        #endregion

        #region Properties
        ///Skapar bara en instans av klassen
        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundManager();
                }
                return instance;
            }
        }
        #endregion

        public void Play(Song song)
        {
            this.song = song;
            if (playCounter >= 1 && GameplayManager.Instance.soundMenu.IsMusicOn)
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
