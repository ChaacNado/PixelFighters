using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    public class MovingObject : GameObject
    {
        public Rectangle hitBox, groundHitBox, hurtBox;
        protected Rectangle srcRec;
        protected bool isOnGround;

        #region Properties
        //v0.1.3 - Skapat tre properties
        public virtual bool IsOnGround { set { isOnGround = false; } }

        public virtual bool IsTopColliding(Platform p)
        {
            return new Rectangle(hitBox.X, hitBox.Y + 1, hitBox.Width, hitBox.Height).Intersects(p.topHitBox);
        }
        
        public virtual bool IsBottomColliding(Platform p)
        {
            return new Rectangle(hitBox.X, hitBox.Y, hitBox.Width, hitBox.Height).Intersects(p.bottomHitBox);
        }
        #endregion

        public MovingObject(Texture2D tex, Vector2 pos, Rectangle srcRec) : base(tex, pos)
        {
            this.tex = tex;
            this.pos = pos;
            this.srcRec = srcRec;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }

        #region Collision Methods
        public virtual void HandleTopCollision(Platform p)
        {
            hitBox.Y = p.topHitBox.Y - hitBox.Height / 2;
            pos.Y = hitBox.Y;
        }
        
        public virtual void HandleBottomCollision(Platform p)
        {
            hitBox.Y = p.bottomHitBox.Y + hitBox.Height / 2;
            hitBox.X = p.bottomHitBox.X - hitBox.Width / 2;
        }

        public virtual void HandlePlayerCollision(PlayerOne p1, PlayerTwo p2)
        {
            if (p1.hurtBox.Intersects(p2.hitBox))
            {
                if (p1.facingRight == true)
                {
                    p2.speed.X += 15f;
                    p2.speed.Y -= 5;
                }
                else if (p1.facingRight == false)
                {
                    p2.speed.X -= 15f;
                    p2.speed.Y -= 5;
                }
            }

            if (p2.hurtBox.Intersects(p1.hitBox))
            {
                if (p2.facingRight == true)
                {
                    p1.speed.X += 15f;
                    p1.speed.Y -= 5;
                }
                else if (p2.facingRight == false)
                {
                    p1.speed.X -= 15f;
                    p1.speed.Y -= 5;
                }
            }
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, color);
        }
    }
}
