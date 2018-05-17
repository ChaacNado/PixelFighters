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
    class QuitMenu:BaseMenu
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

            previousGamePadState = game1.previousGamePadStateOne;
            gamePadState = game1.gamePadStateOne;

            if (keyState.IsKeyDown(Keys.Back) && previousKeyState.IsKeyUp(Keys.Back) || gamePadState.IsButtonDown(Buttons.B) && previousGamePadState.IsButtonUp(Buttons.B))
            {
                game1.currentGameState = GameState.MainMenu;
            }

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A))
                    {
                        game1.Exit();
                    }
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadState.IsButtonDown(Buttons.DPadRight) && previousGamePadState.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    break;
                case MarkerState.MarkerState2:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A))
                    {
                        game1.currentGameState = GameState.MainMenu;
                    }
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadState.IsButtonDown(Buttons.DPadLeft) && previousGamePadState.IsButtonUp(Buttons.DPadLeft))
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
