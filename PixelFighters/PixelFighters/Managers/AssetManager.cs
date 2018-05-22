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
        public SpriteFont spriteFont;
        public Texture2D rectTex, fadeTex, boxManTex;
        public Texture2D mainMenuSpritesheet, optionsMenuSpritesheet, storyMenuSpritesheet, creditsMenuSpritesheet, graphicsMenuSpritesheet, quitMenuSpritesheet, pausedMenuSpritesheet, characterSpriteSheet;

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
            rectTex = Content.Load<Texture2D>("tile");
            fadeTex = Content.Load<Texture2D>("fade");
            boxManTex = Content.Load<Texture2D>("boxMan");
            characterSpriteSheet = Content.Load<Texture2D>("characters");

            mainMenuSpritesheet = Content.Load<Texture2D>("MainMenu");
            optionsMenuSpritesheet = Content.Load<Texture2D>("OptionsMenu");
            storyMenuSpritesheet = Content.Load<Texture2D>("StoryMenu");
            creditsMenuSpritesheet = Content.Load<Texture2D>("CreditsMenu");
            graphicsMenuSpritesheet = Content.Load<Texture2D>("GraphicsMenu");
            quitMenuSpritesheet = Content.Load<Texture2D>("QuitMenu");
            pausedMenuSpritesheet = Content.Load<Texture2D>("pausedMenu");

        }

        #endregion
    }
}
