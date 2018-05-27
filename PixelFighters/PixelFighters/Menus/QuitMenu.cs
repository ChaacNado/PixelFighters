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
    class QuitMenu : BaseMenu
    {
        #region Variables

        Rectangle textRectangle, buttonRectangle;
        Rectangle textSrcRectangle, yesButtonSrcRectangle, noButtonSrcRectangle;

        #endregion

        public QuitMenu()
        {
            textRectangle = new Rectangle(0, -100, 540, 164);
            buttonRectangle = new Rectangle(0, -100, 260, 72);

            textSrcRectangle = new Rectangle(1, 2, 135, 41);
            yesButtonSrcRectangle = new Rectangle(4, 44, 65, 18);
            noButtonSrcRectangle = new Rectangle(4, 67, 65, 18);
        }

        #region Main Methods
        public override void Update(GameTime gameTime, Game1 game1)
        {
            textRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - textRectangle.Width / 2;
            textRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 3;
            buttonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - buttonRectangle.Width / 2;
            buttonRectangle.Y = textRectangle.Y + 200;

            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            previousGamePadStateOne = game1.previousGamePadStateOne;
            gamePadStateOne = game1.gamePadStateOne;
            previousGamePadStateTwo = game1.previousGamePadStateTwo;
            gamePadStateTwo = game1.gamePadStateTwo;

            if (keyState.IsKeyDown(Keys.Back) && previousKeyState.IsKeyUp(Keys.Back) || gamePadStateOne.IsButtonDown(Buttons.B) && previousGamePadStateOne.IsButtonUp(Buttons.B) || gamePadStateTwo.IsButtonDown(Buttons.B) && previousGamePadStateTwo.IsButtonUp(Buttons.B))
            {
                currentMarkerState = MarkerState.MarkerState1;
                currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState1;
                game1.currentGameState = GameState.MainMenu;
            }

            switch (currentMarkerState)
            {
                ///Markerstate1 = ja-knappen
                case MarkerState.MarkerState1:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        game1.Exit();
                    }
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    break;
                ///Markerstate2 = nej-knappen
                case MarkerState.MarkerState2:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        game1.currentGameState = GameState.MainMenu;
                    }
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
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
