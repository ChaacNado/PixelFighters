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
        public Rectangle damageableHitBox, groundHitBox, attackHitBox, srcRec;
        public Vector2 speed;
        public bool isOnGround, isHit, hasTakenDamage, isAttacking, isInvincible, isDunking;
        public int playerIndex, currentHP, maxHP, rangeModifierX, rangeModifierY, damageDealt;
        public float knockBackModifierX, knockBackModifierY;

        #region Properties
        ///Om en spelare kolliderar med toppen av en platform.
        public virtual bool IsTopColliding(Platform platform)
        {
            return new Rectangle(groundHitBox.X, groundHitBox.Y, groundHitBox.Width, groundHitBox.Height).Intersects(platform.topHitBox);
        }
        ///Om en spelare kolliderer med undersidan av en platform.
        public virtual bool IsBottomColliding(Platform platform)
        {
            return new Rectangle(damageableHitBox.X, damageableHitBox.Y, damageableHitBox.Width, damageableHitBox.Height).Intersects(platform.bottomHitBox);
        }
        #endregion

        public MovingObject(Texture2D tex, Vector2 pos, Rectangle srcRec) : base(tex)
        {
            this.tex = tex;
            this.pos = pos;
            this.srcRec = srcRec;
            damageableHitBox = new Rectangle((int)pos.X, (int)pos.Y, damageableHitBox.Width, damageableHitBox.Height);
        }

        #region Collision Methods
        ///Metod för kollisioner mellan spelare och ovansidan av platformarna
        public virtual void HandleTopCollision(Platform platform)
        {
            groundHitBox.Y = platform.topHitBox.Y - damageableHitBox.Height / 2;
            pos.Y = groundHitBox.Y;
        }

        ///Metod för kollisioner mellan spelare och undersidan av platformarna
        public virtual void HandleBottomCollision(Platform platform)
        {
            damageableHitBox.Y = platform.bottomHitBox.Y + damageableHitBox.Height / 2;
            damageableHitBox.X = platform.bottomHitBox.X - damageableHitBox.Width / 2;
        }

        ///Metod för stridskollisioner
        public virtual void HandlePlayerCollision(Player p1, Player p2)
        {
            if (p1.attackHitBox.Intersects(p2.damageableHitBox) && p1.isAttacking)
            {
                if (p1.facingRight)
                {
                    if (p1.isDunking && p2.isOnGround)
                    {
                        p2.speed.X += p1.knockBackModifierX * 6;
                    }
                    else
                    {
                        p2.speed.X += p1.knockBackModifierX / (p2.currentHP * 0.1f);
                        p2.speed.Y -= p1.knockBackModifierY / (p2.currentHP * 0.1f);
                    }
                }
                else if (!p1.facingRight)
                {
                    if (p1.isDunking && p2.isOnGround)
                    {
                        p2.speed.X -= p1.knockBackModifierX * 6;
                    }
                    else
                    {
                        p2.speed.X -= p1.knockBackModifierX / (p2.currentHP * 0.1f);
                        p2.speed.Y -= p1.knockBackModifierY / (p2.currentHP * 0.1f);
                    }
                }
            }

            if (p2.attackHitBox.Intersects(p1.damageableHitBox) && p2.isAttacking)
            {
                if (p2.facingRight)
                {
                    if (p2.isDunking && p1.isOnGround)
                    {
                        p1.speed.X += p2.knockBackModifierX * 3;
                    }
                    else
                    {
                        p1.speed.X += p2.knockBackModifierX / (p1.currentHP * 0.1f);
                        p1.speed.Y -= p2.knockBackModifierY / (p1.currentHP * 0.1f);
                    }
                }
                else if (!p2.facingRight)
                {
                    if (p2.isDunking && p1.isOnGround)
                    {
                        p1.speed.X -= p2.knockBackModifierX * 3;
                    }
                    else
                    {
                        p1.speed.X -= p2.knockBackModifierX / (p1.currentHP * 0.1f);
                        p1.speed.Y -= p2.knockBackModifierY / (p1.currentHP * 0.1f);
                    }
                }
            }
        }
        #endregion

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, color);
        }
    }
}
