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
    class ControlsMenu : BaseMenu
    {
        #region Variables

        Rectangle menusRectangle, gameplayRectangle, menusButtonsRectangle, gameplayButtonsRectangle, leftButtonRectangle, rightButtonRectangle;
        Rectangle menusSrcRectangle, gameplaySrcRectangle, menusButtonsSrcRectangle, gameplayButtonsSrcRectangle, leftButtonSrcRectangle, rightButtonSrcRectangle, markedleftButtonSrcRectangle, markedRightButtonSrcRectangle;

        #endregion

        public ControlsMenu()
        {
            menusRectangle = new Rectangle(0, 0, 270, 60);
            gameplayRectangle = new Rectangle(0, 0, 438, 60);
            menusButtonsRectangle = new Rectangle(0, 0, 537, 339);
            gameplayButtonsRectangle = new Rectangle(0, 0, 537, 573);
            leftButtonRectangle = new Rectangle(0, 0, 32, 48);
            rightButtonRectangle = new Rectangle(0, 0, 32, 48);

            menusSrcRectangle = new Rectangle(0, 0, 45, 10);
            gameplaySrcRectangle = new Rectangle(0, 144, 73, 10);
            menusButtonsSrcRectangle = new Rectangle(0, 23, 179, 113);
            gameplayButtonsSrcRectangle = new Rectangle(0, 163, 179, 191);
            leftButtonSrcRectangle = new Rectangle(53, 1, 8, 12);
            rightButtonSrcRectangle = new Rectangle(96, 1, 8, 12);
            markedleftButtonSrcRectangle = new Rectangle(81, 1, 8, 12);
            markedRightButtonSrcRectangle = new Rectangle(68, 1, 8, 12);
        }

        #region Main Methods

        public override void Update(GameTime gameTime, Game1 game1)
        {
            menusRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 135;
            menusRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 360;
            menusButtonsRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 268;
            menusButtonsRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 185;
            leftButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 532;
            leftButtonRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2;
            rightButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 + 500;
            rightButtonRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2;

            gameplayRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 219;
            gameplayRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 360;
            gameplayButtonsRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 268;
            gameplayButtonsRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 240;

            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            previousGamePadStateOne = game1.previousGamePadStateOne;
            gamePadStateOne = game1.gamePadStateOne;
            previousGamePadStateTwo = game1.previousGamePadStateTwo;
            gamePadStateTwo = game1.gamePadStateTwo;

            if (keyState.IsKeyDown(Keys.Back) && previousKeyState.IsKeyUp(Keys.Back) || gamePadStateOne.IsButtonDown(Buttons.B) && previousGamePadStateOne.IsButtonUp(Buttons.B) || gamePadStateTwo.IsButtonDown(Buttons.B) && previousGamePadStateTwo.IsButtonUp(Buttons.B))
            {
                game1.currentGameState = GameState.MainMenu;
            }

            switch (currentMarkerState)
            {
                //Markerstate1 = menus buttons
                case MarkerState.MarkerState1:
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    break;
                //Markerstate2 = gameplay buttons
                case MarkerState.MarkerState2:
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Instance.controlsMenuSpritesheet, leftButtonRectangle, leftButtonSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.controlsMenuSpritesheet, rightButtonRectangle, rightButtonSrcRectangle, Color.White);

            if (keyState.IsKeyDown(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft))
            {
                spriteBatch.Draw(AssetManager.Instance.controlsMenuSpritesheet, leftButtonRectangle, markedleftButtonSrcRectangle, Color.White);
            }
            if (keyState.IsKeyDown(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight))
            {
                spriteBatch.Draw(AssetManager.Instance.controlsMenuSpritesheet, rightButtonRectangle, markedRightButtonSrcRectangle, Color.White);
            }

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    spriteBatch.Draw(AssetManager.Instance.controlsMenuSpritesheet, menusRectangle, menusSrcRectangle, Color.White);
                    spriteBatch.Draw(AssetManager.Instance.controlsMenuSpritesheet, menusButtonsRectangle, menusButtonsSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState2:
                    spriteBatch.Draw(AssetManager.Instance.controlsMenuSpritesheet, gameplayRectangle, gameplaySrcRectangle, Color.White);
                    spriteBatch.Draw(AssetManager.Instance.controlsMenuSpritesheet, gameplayButtonsRectangle, gameplayButtonsSrcRectangle, Color.White);
                    break;
            }
        }

        #endregion
    }
}
