using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    public class Platform : GameObject
    {
        public Rectangle bottomHitBox, topHitBox;

        public Platform(Texture2D tex, Rectangle hitBox) : base(tex)
        {
            this.tex = tex;

            bottomHitBox = new Rectangle(hitBox.X, hitBox.Y + 1, hitBox.Width, hitBox.Height);
            topHitBox = new Rectangle(hitBox.X, hitBox.Y, hitBox.Width, 25);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (GameplayManager.Instance.stageNumber == 1)
            {
                color = Color.White * 0f;
            }
            else if (GameplayManager.Instance.stageNumber == 2)
            {
                color = Color.White;
            }
            spriteBatch.Draw(tex, bottomHitBox, color);
        }
    }
}
