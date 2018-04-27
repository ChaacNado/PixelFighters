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
    class MainMenu
    {
        #region variables

        private static MainMenu instance;

        public KeyboardState keyState, previousKeyState;

        public MarkerState currentMarkerState;

        Rectangle buttonRectangle1, buttonRectangle2, buttonRectangle3, buttonRectangle4;
        Rectangle buttonSrcRectangle1, buttonSrcRectangle2, buttonSrcRectangle3, buttonSrcRectangle4;
        Rectangle markedButtonSrcRectangle1, markedButtonSrcRectangle2, markedButtonSrcRectangle3, markedButtonSrcRectangle4;

        #endregion

        #region properties

        public static MainMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainMenu();
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
            buttonRectangle4 = new Rectangle((1360 / 2 - 408 / 2), 700, 408, 96);

            buttonSrcRectangle1 = new Rectangle(109, 2, 102, 24);
            buttonSrcRectangle2 = new Rectangle(109, 33, 102, 24);
            buttonSrcRectangle3 = new Rectangle(109, 64, 102, 24);
            buttonSrcRectangle4 = new Rectangle(109, 95, 102, 24);

            markedButtonSrcRectangle1 = new Rectangle(2, 2, 102, 24);
            markedButtonSrcRectangle2 = new Rectangle(2, 33, 102, 24);
            markedButtonSrcRectangle3 = new Rectangle(2, 64, 102, 24);
            markedButtonSrcRectangle4 = new Rectangle(2, 95, 102, 24);
        }

        public void Update(GameTime gameTime, Game1 game1)
        {
            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        game1.currentGameState = GameState.CharacterSelect;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up))
                    {
                        currentMarkerState = MarkerState.MarkerState4;
                    }
                    break;
                case MarkerState.MarkerState2:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        game1.currentGameState = GameState.Options;
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
                        game1.currentGameState = GameState.Credits;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down))
                    {
                        currentMarkerState = MarkerState.MarkerState4;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    break;
                case MarkerState.MarkerState4:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        game1.currentGameState = GameState.Quit;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, buttonRectangle1, buttonSrcRectangle1, Color.White);
            spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, buttonRectangle2, buttonSrcRectangle2, Color.White);
            spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, buttonRectangle3, buttonSrcRectangle3, Color.White);
            spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, buttonRectangle4, buttonSrcRectangle4, Color.White);

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, buttonRectangle1, markedButtonSrcRectangle1, Color.White);
                    break;
                case MarkerState.MarkerState2:
                    spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, buttonRectangle2, markedButtonSrcRectangle2, Color.White);
                    break;
                case MarkerState.MarkerState3:
                    spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, buttonRectangle3, markedButtonSrcRectangle3, Color.White);
                    break;
                case MarkerState.MarkerState4:
                    spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, buttonRectangle4, markedButtonSrcRectangle4, Color.White);
                    break;
            }
        }

        #endregion
    }
}
