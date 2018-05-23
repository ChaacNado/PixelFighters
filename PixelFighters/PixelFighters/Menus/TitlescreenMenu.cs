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
    class TitlescreenMenu : BaseMenu
    {
        #region Variables

        Rectangle shineRectangle, centerRectangle, textRectangle;
        Rectangle shineSrcRectangle, centerSrcRectangle, textSrcRectangle;

        #endregion

        public TitlescreenMenu()
        {
            shineRectangle = new Rectangle(0, 0, 1920, 1080);
            centerRectangle = new Rectangle(0, 0, 594, 591);
            textRectangle = new Rectangle(0, 0, 1314, 741);

            shineSrcRectangle = new Rectangle(0, 0, 640, 360);
            centerSrcRectangle = new Rectangle(0, 0, 198, 197);
            textSrcRectangle = new Rectangle(0, 0, 438, 247);
        }

        #region Main Methods

        public override void Update(GameTime gameTime, Game1 game1)
        {
            shineRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 960;
            shineRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 540;

            centerRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 297;
            centerRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 295;

            textRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 657;
            textRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 370;

            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            previousGamePadStateOne = game1.previousGamePadStateOne;
            gamePadStateOne = game1.gamePadStateOne;
            previousGamePadStateTwo = game1.previousGamePadStateTwo;
            gamePadStateTwo = game1.gamePadStateTwo;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Instance.shineTitlescreenTexture, shineRectangle, shineSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.centerTitlescreenTexture, centerRectangle, centerSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.textTitlescreenTexture, textRectangle, textSrcRectangle, Color.White);
        }

        #endregion
    }
}
