using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    public class Platform :StationaryObject
    {
        public Rectangle bottomHitBox, topHitBox;

        public Platform(Texture2D tex, Vector2 pos, Rectangle hitBox) : base(tex, pos)
        {
            this.tex = tex;
            color = Color.White;

            ///Att ha flera hitboxar underlättar i flera scenarion, här är ett förslag
            bottomHitBox = new Rectangle(hitBox.X, hitBox.Y + 1, hitBox.Width, hitBox.Height);
            topHitBox = new Rectangle(hitBox.X, hitBox.Y, hitBox.Width, 1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, bottomHitBox, color);
        }
    }
}
