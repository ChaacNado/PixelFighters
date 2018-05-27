using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    public class AssetManager
    {
        #region Variables
        public Song menuSong, stage1Song, stage2Song;
        public SoundEffect KOScream, blip, blaster, crack, hardhit, hardhit2, mediumhit, poke, slap, slap2, smallswing, smallswing2, smallswing3, softhit, swing, woosh;

        public SpriteFont spriteFont, timerPixelFont, storyPixelFont;
        public Texture2D rectTex, fadeTex, boxManTex;
        public Texture2D shineTitlescreenTexture, centerTitlescreenTexture, textTitlescreenTexture, mainMenuSpritesheet, optionsMenuSpritesheet, storyMenuSpritesheet, creditsMenuSpritesheet, graphicsMenuSpritesheet, quitMenuSpritesheet,
            pausedMenuSpritesheet, characterSpriteSheet, characterSelectSpritesheet, playTimeHUDSpritesheet, spaceBackgroundTex, skyscraperBackgroundTex, soundMusicMenuSpriteSheet, controlsMenuSpritesheet, resultsMenuSpriteSheet;

        private static AssetManager instance;

        #endregion

        #region Properties
        ///Skapar bara en instans av klassen
        public static AssetManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AssetManager();
                }
                return instance;
            }
        }
        #endregion

        #region Main Methods
        public void LoadContent(ContentManager Content)
        {
            menuSong = Content.Load<Song>("menuost");
            stage1Song = Content.Load<Song>("stage1ost");
            stage2Song = Content.Load<Song>("stage2ost");

            KOScream = Content.Load<SoundEffect>("wScream");
            blip = Content.Load<SoundEffect>("blip");
            blaster = Content.Load<SoundEffect>("Blaster");
            crack = Content.Load<SoundEffect>("crack");
            hardhit = Content.Load<SoundEffect>("hardhit");
            hardhit2 = Content.Load<SoundEffect>("hardhit2");
            mediumhit = Content.Load<SoundEffect>("mediumhit");
            poke = Content.Load<SoundEffect>("poke");
            slap = Content.Load<SoundEffect>("slap");
            slap2 = Content.Load<SoundEffect>("slap2");
            smallswing = Content.Load<SoundEffect>("smallswing");
            smallswing2 = Content.Load<SoundEffect>("smallswing2");
            smallswing3 = Content.Load<SoundEffect>("smallswing3");
            softhit = Content.Load<SoundEffect>("softhit");
            swing = Content.Load<SoundEffect>("swing");
            woosh = Content.Load<SoundEffect>("woosh");

            spriteFont = Content.Load<SpriteFont>("font1");
            timerPixelFont = Content.Load<SpriteFont>("mainPixelFont");
            storyPixelFont = Content.Load<SpriteFont>("smallPixelFont");

            rectTex = Content.Load<Texture2D>("tile");
            fadeTex = Content.Load<Texture2D>("fade");
            boxManTex = Content.Load<Texture2D>("boxMan");
            characterSpriteSheet = Content.Load<Texture2D>("characters");
            spaceBackgroundTex = Content.Load<Texture2D>("background");
            skyscraperBackgroundTex = Content.Load<Texture2D>("standardBackground");

            shineTitlescreenTexture = Content.Load<Texture2D>("shineTitlescreen");
            centerTitlescreenTexture = Content.Load<Texture2D>("center");
            textTitlescreenTexture = Content.Load<Texture2D>("textTitlescreen");
            mainMenuSpritesheet = Content.Load<Texture2D>("MainMenu");
            optionsMenuSpritesheet = Content.Load<Texture2D>("OptionsMenu");
            storyMenuSpritesheet = Content.Load<Texture2D>("StoryMenu");
            creditsMenuSpritesheet = Content.Load<Texture2D>("CreditsMenu");
            graphicsMenuSpritesheet = Content.Load<Texture2D>("GraphicsMenu");
            soundMusicMenuSpriteSheet = Content.Load<Texture2D>("SoundMusicMenu");
            quitMenuSpritesheet = Content.Load<Texture2D>("QuitMenu");
            pausedMenuSpritesheet = Content.Load<Texture2D>("PausedMenu");
            characterSelectSpritesheet = Content.Load<Texture2D>("CharacterSelectMenu");
            playTimeHUDSpritesheet = Content.Load<Texture2D>("PlaytimeHUDSpritesheet");
            controlsMenuSpritesheet = Content.Load<Texture2D>("ControlsMenu");
            resultsMenuSpriteSheet = Content.Load<Texture2D>("ResultsMenu");
        }

        #endregion
    }
}
