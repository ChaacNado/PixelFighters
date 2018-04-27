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
    class OptionsMenu
    {
        #region variables

        private static OptionsMenu instance;

        public KeyboardState keyState, previousKeyState;

        public MarkerState currentMarkerState;

        Rectangle buttonRectangle1, buttonRectangle2, buttonRectangle3;
        Rectangle buttonSrcRectangle1, buttonSrcRectangle2, buttonSrcRectangle3;
        Rectangle markedButtonSrcRectangle1, markedButtonSrcRectangle2, markedButtonSrcRectangle3;

        #endregion

        #region properties

        public static OptionsMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OptionsMenu();
                }
                return instance;
            }
        }

        #endregion

        #region main Methods

        public void LoadContent(ContentManager content, Game1 game1)
        {
            AssetManager.Instance.LoadContent(content);

            buttonRectangle1 = new Rectangle((1360 / 2 - 408 / 2), 100, 408, 96);
            buttonRectangle2 = new Rectangle((1360 / 2 - 408 / 2), 300, 408, 96);
            buttonRectangle3 = new Rectangle((1360 / 2 - 408 / 2), 500, 408, 96);

            buttonSrcRectangle1 = new Rectangle(109, 2, 102, 24);
            buttonSrcRectangle2 = new Rectangle(109, 33, 102, 24);
            buttonSrcRectangle3 = new Rectangle(109, 64, 102, 24);

            markedButtonSrcRectangle1 = new Rectangle(2, 2, 102, 24);
            markedButtonSrcRectangle2 = new Rectangle(2, 33, 102, 24);
            markedButtonSrcRectangle3 = new Rectangle(2, 64, 102, 24);
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
                        game1.currentGameState = GameState.SoundMusic;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    break;
                case MarkerState.MarkerState2:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        game1.currentGameState = GameState.Graphics;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    break;
                case MarkerState.MarkerState3:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        game1.currentGameState = GameState.Controls;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Instance.optionsMenuSpritesheet, buttonRectangle1, buttonSrcRectangle1, Color.White);
            spriteBatch.Draw(AssetManager.Instance.optionsMenuSpritesheet, buttonRectangle2, buttonSrcRectangle2, Color.White);
            spriteBatch.Draw(AssetManager.Instance.optionsMenuSpritesheet, buttonRectangle3, buttonSrcRectangle3, Color.White);

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    spriteBatch.Draw(AssetManager.Instance.optionsMenuSpritesheet, buttonRectangle1, markedButtonSrcRectangle1, Color.White);
                    break;
                case MarkerState.MarkerState2:
                    spriteBatch.Draw(AssetManager.Instance.optionsMenuSpritesheet, buttonRectangle2, markedButtonSrcRectangle2, Color.White);
                    break;
                case MarkerState.MarkerState3:
                    spriteBatch.Draw(AssetManager.Instance.optionsMenuSpritesheet, buttonRectangle3, markedButtonSrcRectangle3, Color.White);
                    break;
            }
        }

        #endregion
    }
}
