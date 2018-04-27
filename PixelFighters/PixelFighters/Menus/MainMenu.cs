using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PixelFighters
{
    class MainMenu
    {
        #region variables

        int screenWitdh = 1360;
        int screenHeight = 900;

        //1920x1080 skärmupplösning
        //int screenWitdh = 1920;
        //int screenHeight = 1080;

        private static MainMenu instance;

        public KeyboardState keyState, previousKeyState;

        public MarkerState currentMarkerState;

        Rectangle playButtonRectangle, optionsButtonRectangle, creditsButtonRectangle, exitButtonRectangle;
        Rectangle playButtonSrcRectangle, optionsSrcRectangle, creditsButtonSrcRectangle, exitButtonSrcRectangle;
        Rectangle markedPlayButtonSrcRectanlge, markedOptionsSrcRectangle, markedCreditsSrcRectangle, markedExitButtonSrcRectangle;

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

            playButtonRectangle = new Rectangle((screenWitdh / 2 - 408 / 2), (screenHeight / 2) - 208, 408, 96);
            optionsButtonRectangle = new Rectangle((screenWitdh / 2 - 408 / 2), playButtonRectangle.Y + 100, 408, 96);
            creditsButtonRectangle = new Rectangle((screenWitdh / 2 - 408 / 2), optionsButtonRectangle.Y + 100, 408, 96);
            exitButtonRectangle = new Rectangle((screenWitdh / 2 - 408 / 2), creditsButtonRectangle.Y + 100, 408, 96);

            playButtonSrcRectangle = new Rectangle(109, 2, 102, 24);
            optionsSrcRectangle = new Rectangle(109, 33, 102, 24);
            creditsButtonSrcRectangle = new Rectangle(109, 64, 102, 24);
            exitButtonSrcRectangle = new Rectangle(109, 95, 102, 24);

            markedPlayButtonSrcRectanlge = new Rectangle(2, 2, 102, 24);
            markedOptionsSrcRectangle = new Rectangle(2, 33, 102, 24);
            markedCreditsSrcRectangle = new Rectangle(2, 64, 102, 24);
            markedExitButtonSrcRectangle = new Rectangle(2, 95, 102, 24);
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
            spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, playButtonRectangle, playButtonSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, optionsButtonRectangle, optionsSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, creditsButtonRectangle, creditsButtonSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, exitButtonRectangle, exitButtonSrcRectangle, Color.White);

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, playButtonRectangle, markedPlayButtonSrcRectanlge, Color.White);
                    break;
                case MarkerState.MarkerState2:
                    spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, optionsButtonRectangle, markedOptionsSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState3:
                    spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, creditsButtonRectangle, markedCreditsSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState4:
                    spriteBatch.Draw(AssetManager.Instance.mainMenuSpritesheet, exitButtonRectangle, markedExitButtonSrcRectangle, Color.White);
                    break;
            }
        }

        #endregion
    }
}
