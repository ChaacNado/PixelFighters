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
    class CreditsMenu : BaseMenu
    {
        #region Variables

        Rectangle creditsRectangle;
        Rectangle creditsSrcRectangle;

        #endregion

        public CreditsMenu()
        {
            creditsRectangle = new Rectangle(0, 0, 536, 384);

            creditsSrcRectangle = new Rectangle(0, 0, 134, 96);
        }

        #region Main Methods

        public override void Update(GameTime gameTime, Game1 game1)
        {
            creditsRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 268;
            creditsRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 192;

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

            //switch (currentMarkerState)
            //{
            //    //Markerstate1 = ja-knappen
            //    case MarkerState.MarkerState1:
            //        if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
            //        {
            //            game1.Exit();
            //        }
            //        if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
            //        {
            //            currentMarkerState = MarkerState.MarkerState2;
            //        }
            //        break;
            //    //Markerstate2 = nej-knappen
            //    case MarkerState.MarkerState2:
            //        if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
            //        {
            //            game1.currentGameState = GameState.MainMenu;
            //        }
            //        if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
            //        {
            //            currentMarkerState = MarkerState.MarkerState1;
            //        }
            //        break;
            //}
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Instance.creditsMenuSpritesheet, creditsRectangle, creditsSrcRectangle, Color.White);

            //switch (currentMarkerState)
            //{
            //    case MarkerState.MarkerState1:
            //        spriteBatch.Draw(AssetManager.Instance.quitMenuSpritesheet, buttonRectangle, yesButtonSrcRectangle, Color.White);
            //        break;
            //    case MarkerState.MarkerState2:
            //        spriteBatch.Draw(AssetManager.Instance.quitMenuSpritesheet, buttonRectangle, noButtonSrcRectangle, Color.White);
            //        break;
            //}
        }

        #endregion
    }
}
