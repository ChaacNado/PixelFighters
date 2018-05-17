using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PixelFighters
{
    class MainMenu : BaseMenu
    {
        #region Variables

        Rectangle playButtonRectangle, optionsButtonRectangle, creditsButtonRectangle, exitButtonRectangle;
        Rectangle playButtonSrcRectangle, optionsSrcRectangle, creditsButtonSrcRectangle, exitButtonSrcRectangle;
        Rectangle markedPlayButtonSrcRectanlge, markedOptionsSrcRectangle, markedCreditsSrcRectangle, markedExitButtonSrcRectangle;

        #endregion

        public MainMenu()
        {
            playButtonRectangle = new Rectangle(0, -100, 408, 96);
            optionsButtonRectangle = new Rectangle(0, -100, 408, 96);
            creditsButtonRectangle = new Rectangle(0, -100, 408, 96);
            exitButtonRectangle = new Rectangle(0, -100, 408, 96);

            playButtonSrcRectangle = new Rectangle(109, 2, 102, 24);
            optionsSrcRectangle = new Rectangle(109, 33, 102, 24);
            creditsButtonSrcRectangle = new Rectangle(109, 64, 102, 24);
            exitButtonSrcRectangle = new Rectangle(109, 95, 102, 24);

            markedPlayButtonSrcRectanlge = new Rectangle(2, 2, 102, 24);
            markedOptionsSrcRectangle = new Rectangle(2, 33, 102, 24);
            markedCreditsSrcRectangle = new Rectangle(2, 64, 102, 24);
            markedExitButtonSrcRectangle = new Rectangle(2, 95, 102, 24);
        }

        #region Main Methods

        public override void Update(GameTime gameTime, Game1 game1)
        {
            playButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - playButtonRectangle.Width / 2;
            playButtonRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 208;
            optionsButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - optionsButtonRectangle.Width / 2;
            optionsButtonRectangle.Y = playButtonRectangle.Y + 100;
            creditsButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - creditsButtonRectangle.Width / 2;
            creditsButtonRectangle.Y = optionsButtonRectangle.Y + 100;
            exitButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - exitButtonRectangle.Width / 2;
            exitButtonRectangle.Y = creditsButtonRectangle.Y + 100;

            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            previousGamePadState = game1.previousGamePadStateOne;
            gamePadState = game1.gamePadStateOne;

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A))
                    {
                        game1.currentGameState = GameState.CharacterSelect;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadState.IsButtonDown(Buttons.DPadDown) && previousGamePadState.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadState.IsButtonDown(Buttons.DPadUp) && previousGamePadState.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState4;
                    }
                    break;
                case MarkerState.MarkerState2:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A))
                    {
                        game1.currentGameState = GameState.Options;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadState.IsButtonDown(Buttons.DPadDown) && previousGamePadState.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadState.IsButtonDown(Buttons.DPadUp) && previousGamePadState.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    break;
                case MarkerState.MarkerState3:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A))
                    {
                        game1.currentGameState = GameState.Credits;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadState.IsButtonDown(Buttons.DPadDown) && previousGamePadState.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState4;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadState.IsButtonDown(Buttons.DPadUp) && previousGamePadState.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    break;
                case MarkerState.MarkerState4:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A))
                    {
                        game1.currentGameState = GameState.Quit;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down)|| gamePadState.IsButtonDown(Buttons.DPadDown) && previousGamePadState.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadState.IsButtonDown(Buttons.DPadUp) && previousGamePadState.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
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
