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
        public Rectangle damageableHitBox, groundHitBox, attackhitBox;
        protected Rectangle srcRec;
        public Vector2 speed;
        protected bool isOnGround;
        public bool isHit, isAttacking;

        #region Properties
        //v0.1.3 - Skapat tre properties
        public virtual bool IsOnGround { set { isOnGround = false; } }

        public virtual bool IsTopColliding(Platform p)
        {
            return new Rectangle(damageableHitBox.X, damageableHitBox.Y + 1, damageableHitBox.Width, damageableHitBox.Height).Intersects(p.topHitBox);
        }

        public virtual bool IsBottomColliding(Platform p)
        {
            return new Rectangle(damageableHitBox.X, damageableHitBox.Y, damageableHitBox.Width, damageableHitBox.Height).Intersects(p.bottomHitBox);
        }
        #endregion

        public MovingObject(Texture2D tex, Vector2 pos, Rectangle srcRec) : base(tex, pos)
        {
            this.tex = tex;
            this.pos = pos;
            this.srcRec = srcRec;
            damageableHitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }

        #region Collision Methods
        public virtual void HandleTopCollision(Platform p)
        {
            damageableHitBox.Y = p.topHitBox.Y - damageableHitBox.Height / 2;
            pos.Y = damageableHitBox.Y;
        }

        public virtual void HandleBottomCollision(Platform p)
        {
            damageableHitBox.Y = p.bottomHitBox.Y + damageableHitBox.Height / 2;
            damageableHitBox.X = p.bottomHitBox.X - damageableHitBox.Width / 2;
        }

        public virtual void HandlePlayerCollision(PlayerOne p1, PlayerTwo p2)
        {
            if (p1.attackhitBox.Intersects(p2.damageableHitBox) && p1.isAttacking)
            {
                if (p1.facingRight)
                {
                    p2.speed.X += 15;
                    p2.speed.Y -= 5;
                }
                else if (!p1.facingRight)
                {
                    p2.speed.X -= 15;
                    p2.speed.Y -= 5;
                }
            }
            else
            {
                p2.isHit = false;
                p2.speed.X = 0;
            }

            if (p2.attackhitBox.Intersects(p1.damageableHitBox) && p2.isAttacking)
            {

                if (p2.facingRight)
                {
                    p1.speed.X += 15;
                    p1.speed.Y -= 5;
                }
                else if (!p2.facingRight)
                {
                    p1.speed.X -= 15;
                    p1.speed.Y -= 5;
                }

            }
            else
            {
                p1.isHit = false;
                p1.speed.X *= 0;
            }
        }
        #endregion

        public override void Update(GameTime gameTime)
        {
            if (!isHit)
            {
                speed.X = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, color);
        }
    }
}
