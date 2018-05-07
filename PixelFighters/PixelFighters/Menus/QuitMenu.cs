using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    class QuitMenu
    {
        #region Variables

        int screenWitdh = 1360;
        int screenHeight = 900;

        //1920x1080 skärmupplösning
        //int screenWitdh = 1920;
        //int screenHeight = 1080;

        private static QuitMenu instance;

        public KeyboardState keyState, previousKeyState;

        public MarkerState currentMarkerState;

        Rectangle textRectangle, buttonRectangle;
        Rectangle textSrcRectangle, yesButtonSrcRectangle, noButtonSrcRectangle;

        #endregion

        #region Properties

        public static QuitMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuitMenu();
                }
                return instance;
            }
        }

        #endregion

        #region Main Methods

        public void LoadContent(ContentManager content)
        {
            AssetManager.Instance.LoadContent(content);

            textRectangle = new Rectangle((screenWitdh / 2 - 540 / 2), (screenHeight / 3), 540, 164);
            buttonRectangle = new Rectangle((screenWitdh / 2 - 260 / 2), textRectangle.Y + 200, 260, 72);

            textSrcRectangle = new Rectangle(1, 2, 135, 41);
            yesButtonSrcRectangle = new Rectangle(4, 44, 65, 18);
            noButtonSrcRectangle = new Rectangle(4, 67, 65, 18);
        }

        public void Update(GameTime gameTime, Game1 game1)
        {
            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            if (keyState.IsKeyDown(Keys.Back) && previousKeyState.IsKeyUp(Keys.Back))
            {
                game1.currentGameState = GameState.MainMenu;
            }

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        game1.Exit();
                    }
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    break;
                case MarkerState.MarkerState2:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        game1.currentGameState = GameState.MainMenu;
                    }
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Instance.quitMenuSpritesheet, textRectangle, textSrcRectangle, Color.White);

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    spriteBatch.Draw(AssetManager.Instance.quitMenuSpritesheet, buttonRectangle, yesButtonSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState2:
                    spriteBatch.Draw(AssetManager.Instance.quitMenuSpritesheet, buttonRectangle, noButtonSrcRectangle, Color.White);
                    break;
            }
        }

        #endregion
    }
}
