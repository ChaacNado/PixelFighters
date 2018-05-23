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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Instance.creditsMenuSpritesheet, creditsRectangle, creditsSrcRectangle, Color.White);
        }

        #endregion
    }
}
