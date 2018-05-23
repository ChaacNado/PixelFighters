using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    public class AssetManager
    {
        #region Variables

        public StreamReader streamReader;
        public SpriteFont spriteFont, timerPixelFont, storyPixelFont;
        public Texture2D rectTex, fadeTex, boxManTex;
        public Texture2D shineTitlescreenTexture, centerTitlescreenTexture, textTitlescreenTexture, mainMenuSpritesheet, optionsMenuSpritesheet, storyMenuSpritesheet, creditsMenuSpritesheet, graphicsMenuSpritesheet, quitMenuSpritesheet,
            pausedMenuSpritesheet, characterSpriteSheet, characterSelectSpritesheet, playTimeHUDSpritesheet, backgroundTex;

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
            streamReader = new StreamReader("stage" + GameplayManager.Instance.stageNumber + ".txt");

            spriteFont = Content.Load<SpriteFont>("font1");
            timerPixelFont = Content.Load<SpriteFont>("mainPixelFont");
            storyPixelFont = Content.Load<SpriteFont>("smallPixelFont");
            rectTex = Content.Load<Texture2D>("tile");
            fadeTex = Content.Load<Texture2D>("fade");
            boxManTex = Content.Load<Texture2D>("boxMan");
            characterSpriteSheet = Content.Load<Texture2D>("characters");
            backgroundTex = Content.Load<Texture2D>("background");

            shineTitlescreenTexture = Content.Load<Texture2D>("shineTitlescreen");
            centerTitlescreenTexture = Content.Load<Texture2D>("center");
            textTitlescreenTexture = Content.Load<Texture2D>("textTitlescreen");
            mainMenuSpritesheet = Content.Load<Texture2D>("MainMenu");
            optionsMenuSpritesheet = Content.Load<Texture2D>("OptionsMenu");
            storyMenuSpritesheet = Content.Load<Texture2D>("StoryMenu");
            creditsMenuSpritesheet = Content.Load<Texture2D>("CreditsMenu");
            graphicsMenuSpritesheet = Content.Load<Texture2D>("GraphicsMenu");
            quitMenuSpritesheet = Content.Load<Texture2D>("QuitMenu");
            pausedMenuSpritesheet = Content.Load<Texture2D>("PausedMenu");
            characterSelectSpritesheet = Content.Load<Texture2D>("CharacterSelectMenu");
            playTimeHUDSpritesheet = Content.Load<Texture2D>("PlaytimeHUDSpritesheet");
        }

        #endregion
    }
}
