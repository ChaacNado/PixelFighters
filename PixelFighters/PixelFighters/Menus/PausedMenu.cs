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
    class PausedMenu
    {
        #region Variables
        protected KeyboardState keyState, previousKeyState;
        protected MarkerState currentMarkerState;
        protected GamePadState gamePadStateOne, previousGamePadStateOne, gamePadStateTwo, previousGamePadStateTwo;

        Rectangle greyRectangle, pausedRectangle, resumeQuitRectangle;
        Rectangle greySrcRectangle, pausedSrcRectangle, resumeSrcRectangle, quitSrcRectangle;

        #endregion

        public PausedMenu()
        {
            greyRectangle = new Rectangle(-1000, -1000, 5000, 5080);
            pausedRectangle = new Rectangle(0, 0, 416, 80);
            resumeQuitRectangle = new Rectangle(0, 0, 408, 180);

            greySrcRectangle = new Rectangle(0, 64, 16, 16);
            pausedSrcRectangle = new Rectangle(0, 51, 52, 10);
            resumeSrcRectangle = new Rectangle(2, 2, 102, 48);
            quitSrcRectangle = new Rectangle(109, 2, 102, 48);
        }

        #region Main Methods

        public void Update(GameTime gameTime, Game1 game1, Camera camera)
        {
            pausedRectangle.X = (int)camera.pos.X + (int)ScreenManager.Instance.Dimensions.X / 2 - 208;
            pausedRectangle.Y = (int)camera.pos.Y + (int)ScreenManager.Instance.Dimensions.X / 2 - 550;
            resumeQuitRectangle.X = (int)camera.pos.X + (int)ScreenManager.Instance.Dimensions.X / 2 - 204;
            resumeQuitRectangle.Y = (int)camera.pos.Y + pausedRectangle.Y + 100;

            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            previousGamePadStateOne = game1.previousGamePadStateOne;
            gamePadStateOne = game1.gamePadStateOne;
            previousGamePadStateTwo = game1.previousGamePadStateTwo;
            gamePadStateTwo = game1.gamePadStateTwo;

            switch (currentMarkerState)
            {
                //Markerstate1 = resume-knappen
                case MarkerState.MarkerState1:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        game1.currentGameState = GameState.Playtime;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    break;
                //Markerstate2 = quit-knappen
                case MarkerState.MarkerState2:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        game1.currentGameState = GameState.MainMenu;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Instance.pausedMenuSpritesheet, greyRectangle, greySrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.pausedMenuSpritesheet, pausedRectangle, pausedSrcRectangle, Color.White);

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    spriteBatch.Draw(AssetManager.Instance.pausedMenuSpritesheet, resumeQuitRectangle, resumeSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState2:
                    spriteBatch.Draw(AssetManager.Instance.pausedMenuSpritesheet, resumeQuitRectangle, quitSrcRectangle, Color.White);
                    break;
            }
        }

        #endregion
    }
}
