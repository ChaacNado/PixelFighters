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
    class StoryMenu : BaseMenu
    {
        #region Variables

        int characterPage;
        Rectangle characterTextRectangle, leftButtonRectangle, rightButtonRectangle, textRectangle, boxerRectangle, americanFootballerRectangle, curlerRectangle, baseballerRectangle;
        Rectangle theBoxerSrcRectangle, theAmericanFootballerSrcRectangle, theCurlerSrcRectangle, theBaseballerSrcRectangle, leftButtonSrcRectangle, rightButtonSrcRectangle, markedLeftButtonSrcRectangle, markedRightButtonSrcRectangle, textSrcRectangle/*, windowedButtonYesSrcRectangle, windowedButtonNoSrcRectangle, resolutionButtonSrcRectangle, applyButtonSrcRectangle*/;
        Rectangle boxerSrcRectangle, americanFootballerSrcRectangle, curlerSrcRectangle, baseballerSrcRectangle, hiddenBoxerSrcRectangle, hiddenAmericanFootballerSrcRectangle, hiddenCurlerSrcRectangle, hiddenBaseballerSrcRectangle;
        //Rectangle /*markedWindowedButtonYesSrcRectangle, markedWindowedButtonNoSrcRectangle, markedResolutionButtonSrcRectangle, markedApplyButtonSrcRectangle, leftMarkedButtonSrcRectangle, rightMarkedButtonSrcRectangle*/;
        //Rectangle resolution1366x768SrcRectangle, resolution1920x1080SrcRectangle;

        #endregion
        public StoryMenu()
        {
            characterPage = 1;
            
            //windowedButtonRectangle = new Rectangle(0, -100, 756, 72);
            //resolutionButtonRectangle = new Rectangle(0, -100, 756, 72);
            //resolutionRectangle = new Rectangle(0, -100, 308, 40);
            //applyButtonRectangle = new Rectangle(0, -100, 384, 72);

            characterTextRectangle = new Rectangle(0, 0, 832, 40);
            leftButtonRectangle = new Rectangle(0, 0, 32, 48);
            rightButtonRectangle = new Rectangle(0, 0, 32, 48);
            textRectangle = new Rectangle(0, 0, 836, 304);
            boxerRectangle = new Rectangle(0, 0, 200, 200);
            americanFootballerRectangle = new Rectangle(0, 0, 200, 200);
            curlerRectangle = new Rectangle(0, 0, 200, 200);
            baseballerRectangle = new Rectangle(0, 0, 200, 200);
            //textRectangle = new Rectangle(0, 0, 756, 72);
            //characterRectangle = new Rectangle(0, -100, 756, 72);

            leftButtonSrcRectangle = new Rectangle(0, 16, 8, 12);
            rightButtonSrcRectangle = new Rectangle(15, 0, 8, 12);
            markedLeftButtonSrcRectangle = new Rectangle(0, 0, 8, 12);
            markedRightButtonSrcRectangle = new Rectangle(15, 16, 8, 12);

            theBoxerSrcRectangle = new Rectangle(0, 32, 208, 10);
            theAmericanFootballerSrcRectangle = new Rectangle(0, 48, 208, 10);
            theCurlerSrcRectangle = new Rectangle(0, 64, 208, 10);
            theBaseballerSrcRectangle = new Rectangle(0, 80, 208, 10);

            boxerSrcRectangle = new Rectangle(0, 96, 50, 50);
            americanFootballerSrcRectangle = new Rectangle(53, 96, 50, 50);
            curlerSrcRectangle = new Rectangle(106, 96, 50, 50);
            baseballerSrcRectangle = new Rectangle(159, 96, 50, 50);

            hiddenBoxerSrcRectangle = new Rectangle(0, 149, 50, 50);
            hiddenAmericanFootballerSrcRectangle = new Rectangle(53, 149, 50, 50);
            hiddenCurlerSrcRectangle = new Rectangle(106, 149, 50, 50);
            hiddenBaseballerSrcRectangle = new Rectangle(159, 149, 50, 50);

            textSrcRectangle = new Rectangle(0, 203, 209, 76);



            //windowedButtonYesSrcRectangle = new Rectangle(0, 222, 202, 18);
            //windowedButtonNoSrcRectangle = new Rectangle(0, 199, 202, 18);
            //markedWindowedButtonYesSrcRectangle = new Rectangle(0, 0, 202, 18);
            //markedWindowedButtonNoSrcRectangle = new Rectangle(0, 23, 202, 18);
            //resolutionButtonSrcRectangle = new Rectangle(0, 51, 202, 18);
            //applyButtonSrcRectangle = new Rectangle(96, 170, 96, 18);
            //markedResolutionButtonSrcRectangle = new Rectangle(0, 73, 202, 18);
            //markedApplyButtonSrcRectangle = new Rectangle(96, 146, 96, 18);
            //leftMarkedButtonSrcRectangle = new Rectangle(0, 95, 202, 18);
            //rightMarkedButtonSrcRectangle = new Rectangle(0, 117, 202, 18);
            //resolution1366x768SrcRectangle = new Rectangle(0, 165, 77, 10);
            //resolution1920x1080SrcRectangle = new Rectangle(0, 187, 77, 10);
        }
        #region Main Methods

        public override void Update(GameTime gameTime, Game1 game1)
        {
            characterTextRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 416;
            characterTextRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 370;

            leftButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 532;
            leftButtonRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2;

            rightButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 + 500;
            rightButtonRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2;

            //boxerRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 3 - 200;
            boxerRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 418;
            boxerRectangle.Y = characterTextRectangle.Y + 100;

            americanFootballerRectangle.X = boxerRectangle.X + 212;
            americanFootballerRectangle.Y = characterTextRectangle.Y + 100;

            curlerRectangle.X = americanFootballerRectangle.X + 212;
            curlerRectangle.Y = characterTextRectangle.Y + 100;

            baseballerRectangle.X = curlerRectangle.X + 212;
            baseballerRectangle.Y = characterTextRectangle.Y + 100;

            textRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 418;
            textRectangle.Y = boxerRectangle.Y + 212;

            //windowedButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - windowedButtonRectangle.Width / 2;
            //windowedButtonRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 136;
            //resolutionButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - resolutionButtonRectangle.Width / 2;
            //resolutionButtonRectangle.Y = windowedButtonRectangle.Y + 100;
            //resolutionRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2;
            //resolutionRectangle.Y = resolutionButtonRectangle.Y + 15;
            //applyButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - applyButtonRectangle.Width / 2;
            //applyButtonRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y - 200;

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

            if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
            {
                if (characterPage == 4)
                {
                    characterPage = 1;
                }
                else if (characterPage < 4)
                {
                    characterPage++;
                }
            }

            if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
            {
                if (characterPage == 1)
                {
                    characterPage = 4;
                }
                else if (characterPage > 1)
                {
                    characterPage--;
                }
            }

            //switch (currentMarkerState)
            //{
            //    //Markerstate1 = yes-knapp till windowed mode
            //    case MarkerState.MarkerState1:
            //        windowedMode = true;
            //        if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
            //        {
            //            currentMarkerState = MarkerState.MarkerState2;
            //        }
            //        if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
            //        {
            //            currentMarkerState = MarkerState.MarkerState3;
            //        }
            //        break;
            //    //Markerstate2 = no-knapp till windowed mode
            //    case MarkerState.MarkerState2:
            //        windowedMode = false;
            //        if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
            //        {
            //            currentMarkerState = MarkerState.MarkerState1;
            //        }
            //        if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
            //        {
            //            currentMarkerState = MarkerState.MarkerState3;
            //        }
            //        break;
            //    //Markerstate3 = markerar vald upplösning 
            //    case MarkerState.MarkerState3:
            //        if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
            //        {
            //            currentMarkerState = MarkerState.MarkerState6;
            //        }
            //        if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
            //        {
            //            if (windowedMode == true)
            //            {
            //                currentMarkerState = MarkerState.MarkerState1;
            //            }

            //            if (windowedMode == false)
            //            {
            //                currentMarkerState = MarkerState.MarkerState2;
            //            }
            //        }
            //        if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
            //        {
            //            currentMarkerState = MarkerState.MarkerState4;
            //        }
            //        if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
            //        {
            //            currentMarkerState = MarkerState.MarkerState5;
            //        }
            //        break;
            //    //Markerstate4 = vänster pil till upplösning
            //    case MarkerState.MarkerState4:
            //        if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
            //        {
            //            currentMarkerState = MarkerState.MarkerState3;
            //        }
            //        if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft)
            //            || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
            //        {
            //            resolution = 1;
            //        }
            //        break;
            //    //Markerstate5 = höger pil till upplösning
            //    case MarkerState.MarkerState5:
            //        if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
            //        {
            //            currentMarkerState = MarkerState.MarkerState3;
            //        }
            //        if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right)
            //            || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
            //        {
            //            resolution = 2;
            //        }
            //        break;
            //    //Markerstate6 = Applyknappen
            //    case MarkerState.MarkerState6:

            //        if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
            //        {
            //            if (windowedMode == true)
            //            {
            //                game1.graphics.IsFullScreen = false;
            //            }
            //            if (windowedMode == false)
            //            {
            //                game1.graphics.IsFullScreen = true;
            //            }
            //            if (resolution == 1)
            //            {
            //                ScreenManager.Instance.Dimensions = new Vector2(1366, 768);
            //                game1.graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            //                game1.graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;

            //            }
            //            if (resolution == 2)
            //            {
            //                ScreenManager.Instance.Dimensions = new Vector2(1920, 1080);
            //                game1.graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            //                game1.graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            //            }
            //            game1.graphics.ApplyChanges();
            //        }
            //        if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
            //        {
            //            currentMarkerState = MarkerState.MarkerState3;
            //        }
            //        break;
            //}
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, leftButtonRectangle, leftButtonSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, rightButtonRectangle, rightButtonSrcRectangle, Color.White);

            spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, boxerRectangle, hiddenBoxerSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, americanFootballerRectangle, hiddenAmericanFootballerSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, curlerRectangle, hiddenCurlerSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, baseballerRectangle, hiddenBaseballerSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, textRectangle, textSrcRectangle, Color.White);



            if (keyState.IsKeyDown(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft))
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, leftButtonRectangle, markedLeftButtonSrcRectangle, Color.White);
            }
            if (keyState.IsKeyDown(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight))
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, rightButtonRectangle, markedRightButtonSrcRectangle, Color.White);
            }

            if (characterPage == 1)
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, characterTextRectangle, theBoxerSrcRectangle, Color.White);
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, boxerRectangle, boxerSrcRectangle, Color.White);

            }
            if (characterPage == 2)
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, characterTextRectangle, theAmericanFootballerSrcRectangle, Color.White);
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, americanFootballerRectangle, americanFootballerSrcRectangle, Color.White);

            }
            if (characterPage == 3)
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, characterTextRectangle, theCurlerSrcRectangle, Color.White);
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, curlerRectangle, curlerSrcRectangle, Color.White);
            }
            if (characterPage == 4)
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, characterTextRectangle, theBaseballerSrcRectangle, Color.White);
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, baseballerRectangle, baseballerSrcRectangle, Color.White);
            }

            //if (windowedMode == false)
            //{
            //    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, windowedButtonRectangle, windowedButtonNoSrcRectangle, Color.White);
            //}

            //switch (currentMarkerState)
            //{
            //    case MarkerState.MarkerState1:
            //        spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, windowedButtonRectangle, markedWindowedButtonYesSrcRectangle, Color.White);
            //        break;
            //    case MarkerState.MarkerState2:
            //        spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, windowedButtonRectangle, markedWindowedButtonNoSrcRectangle, Color.White);
            //        break;
            //    case MarkerState.MarkerState3:
            //        spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionButtonRectangle, markedResolutionButtonSrcRectangle, Color.White);
            //        break;
            //    case MarkerState.MarkerState4:
            //        spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionButtonRectangle, leftMarkedButtonSrcRectangle, Color.White);
            //        break;
            //    case MarkerState.MarkerState5:
            //        spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionButtonRectangle, rightMarkedButtonSrcRectangle, Color.White);
            //        break;
            //    case MarkerState.MarkerState6:
            //        spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, applyButtonRectangle, markedApplyButtonSrcRectangle, Color.White);
            //        break;
            //}

            //if (resolution == 1)
            //{
            //    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionRectangle, resolution1366x768SrcRectangle, Color.White);
            //}

            //if (resolution == 2)
            //{
            //    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionRectangle, resolution1920x1080SrcRectangle, Color.White);
            //}
        }

        #endregion
    }
}
