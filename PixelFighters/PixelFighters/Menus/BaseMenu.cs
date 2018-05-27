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
    public abstract class BaseMenu
    {
        protected bool isSoundOn, isMusicOn;

        protected KeyboardState keyState, previousKeyState;
        protected GamePadState gamePadStateOne, previousGamePadStateOne, gamePadStateTwo, previousGamePadStateTwo;

        protected MarkerState currentMarkerState;
        protected SecondaryMarkerState currentSecondaryMarkerState;

        #region Properties
        public bool IsSoundOn { get { return isSoundOn; } }

        public bool IsMusicOn { get { return isMusicOn; } }
        #endregion
        public abstract void Update(GameTime gameTime, Game1 game1);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
