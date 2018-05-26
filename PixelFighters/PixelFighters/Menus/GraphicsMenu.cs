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
    class GraphicsMenu : BaseMenu
    {
        #region Variables

        private bool windowedMode;
        private int resolution = 1;

        Rectangle windowedButtonRectangle, resolutionButtonRectangle, applyButtonRectangle, resolutionRectangle;
        Rectangle windowedButtonYesSrcRectangle, windowedButtonNoSrcRectangle, resolutionButtonSrcRectangle, applyButtonSrcRectangle;
        Rectangle markedWindowedButtonYesSrcRectangle, markedWindowedButtonNoSrcRectangle, markedResolutionButtonSrcRectangle, markedApplyButtonSrcRectangle, leftMarkedButtonSrcRectangle, rightMarkedButtonSrcRectangle;
        Rectangle resolution1366x768SrcRectangle, resolution1920x1080SrcRectangle;

        #endregion
        public GraphicsMenu()
        {
            windowedButtonRectangle = new Rectangle(0, -100, 756, 72);
            resolutionButtonRectangle = new Rectangle(0, -100, 756, 72);
            resolutionRectangle = new Rectangle(0, -100, 308, 40);
            applyButtonRectangle = new Rectangle(0, -100, 384, 72);

            windowedButtonYesSrcRectangle = new Rectangle(0, 222, 202, 18);
            windowedButtonNoSrcRectangle = new Rectangle(0, 199, 202, 18);
            markedWindowedButtonYesSrcRectangle = new Rectangle(0, 0, 202, 18);
            markedWindowedButtonNoSrcRectangle = new Rectangle(0, 23, 202, 18);
            resolutionButtonSrcRectangle = new Rectangle(0, 51, 202, 18);
            applyButtonSrcRectangle = new Rectangle(96, 170, 96, 18);
            markedResolutionButtonSrcRectangle = new Rectangle(0, 73, 202, 18);
            markedApplyButtonSrcRectangle = new Rectangle(96, 146, 96, 18);
            leftMarkedButtonSrcRectangle = new Rectangle(0, 95, 202, 18);
            rightMarkedButtonSrcRectangle = new Rectangle(0, 117, 202, 18);
            resolution1366x768SrcRectangle = new Rectangle(0, 165, 77, 10);
            resolution1920x1080SrcRectangle = new Rectangle(0, 187, 77, 10);
        }
        #region Main Methods

        public override void Update(GameTime gameTime, Game1 game1)
        {
            windowedButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - windowedButtonRectangle.Width / 2;
            windowedButtonRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 136;
            resolutionButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - resolutionButtonRectangle.Width / 2;
            resolutionButtonRectangle.Y = windowedButtonRectangle.Y + 100;
            resolutionRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2;
            resolutionRectangle.Y = resolutionButtonRectangle.Y + 15;
            applyButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - applyButtonRectangle.Width / 2;
            applyButtonRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y - 200;

            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            previousGamePadStateOne = game1.previousGamePadStateOne;
            gamePadStateOne = game1.gamePadStateOne;
            previousGamePadStateTwo = game1.previousGamePadStateTwo;
            gamePadStateTwo = game1.gamePadStateTwo;

            if (keyState.IsKeyDown(Keys.Back) && previousKeyState.IsKeyUp(Keys.Back) || gamePadStateOne.IsButtonDown(Buttons.B) && previousGamePadStateOne.IsButtonUp(Buttons.B) || gamePadStateTwo.IsButtonDown(Buttons.B) && previousGamePadStateTwo.IsButtonUp(Buttons.B))
            {
                game1.currentGameState = GameState.Options;
            }

            switch (currentMarkerState)
            {
                ///Markerstate1 = yes-knapp till windowed mode
                case MarkerState.MarkerState1:
                    windowedMode = true;
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    break;
                ///Markerstate2 = no-knapp till windowed mode
                case MarkerState.MarkerState2:
                    windowedMode = false;
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    break;
                ///Markerstate3 = markerar vald upplösning 
                case MarkerState.MarkerState3:
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState6;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
                    {
                        if (windowedMode == true)
                        {
                            currentMarkerState = MarkerState.MarkerState1;
                        }

                        if (windowedMode == false)
                        {
                            currentMarkerState = MarkerState.MarkerState2;
                        }
                    }
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentMarkerState = MarkerState.MarkerState4;
                    }
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState5;
                    }
                    break;
                ///Markerstate4 = vänster pil till upplösning
                case MarkerState.MarkerState4:
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft)
                        || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        resolution = 1;
                    }
                    break;
                ///Markerstate5 = höger pil till upplösning
                case MarkerState.MarkerState5:
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right)
                        || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
                    {
                        resolution = 2;
                    }
                    break;
                ///Markerstate6 = Applyknappen
                case MarkerState.MarkerState6:

                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        if (windowedMode == true)
                        {
                            game1.graphics.IsFullScreen = false;
                        }
                        if (windowedMode == false)
                        {
                            game1.graphics.IsFullScreen = true;
                        }
                        if (resolution == 1)
                        {
                            ScreenManager.Instance.Dimensions = new Vector2(1366, 768);
                            game1.graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
                            game1.graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;

                        }
                        if (resolution == 2)
                        {
                            ScreenManager.Instance.Dimensions = new Vector2(1920, 1080);
                            game1.graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
                            game1.graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
                        }
                        game1.graphics.ApplyChanges();
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionButtonRectangle, resolutionButtonSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, applyButtonRectangle, applyButtonSrcRectangle, Color.White);

            if (windowedMode == true)
            {
                spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, windowedButtonRectangle, windowedButtonYesSrcRectangle, Color.White);
            }

            if (windowedMode == false)
            {
                spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, windowedButtonRectangle, windowedButtonNoSrcRectangle, Color.White);
            }

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, windowedButtonRectangle, markedWindowedButtonYesSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState2:
                    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, windowedButtonRectangle, markedWindowedButtonNoSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState3:
                    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionButtonRectangle, markedResolutionButtonSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState4:
                    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionButtonRectangle, leftMarkedButtonSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState5:
                    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionButtonRectangle, rightMarkedButtonSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState6:
                    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, applyButtonRectangle, markedApplyButtonSrcRectangle, Color.White);
                    break;
            }

            if (resolution == 1)
            {
                spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionRectangle, resolution1366x768SrcRectangle, Color.White);
            }

            if (resolution == 2)
            {
                spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionRectangle, resolution1920x1080SrcRectangle, Color.White);
            }
        }
        #endregion
    }
}
