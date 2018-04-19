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
        public Rectangle damageableHitBox, groundHitBox, attackHitBox;
        protected Rectangle srcRec;
        public Vector2 speed;
        protected bool isOnGround;
        public bool isHit, isAttacking, isInvincible;
        public int playerIndex, HP, maxHP;
        public float knockBackModifierX, knockBackModifierY;

        #region Properties
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
        ///Metod för kollisioner mellan spelare och ovansidan av platformarna
        public virtual void HandleTopCollision(Platform p)
        {
            damageableHitBox.Y = p.topHitBox.Y - damageableHitBox.Height / 2;
            pos.Y = damageableHitBox.Y;
        }

        ///Metod för kollisioner mellan spelare och undersidan av platformarna
        public virtual void HandleBottomCollision(Platform p)
        {
            damageableHitBox.Y = p.bottomHitBox.Y + damageableHitBox.Height / 2;
            damageableHitBox.X = p.bottomHitBox.X - damageableHitBox.Width / 2;
        }

        ///Metod för stridskollisioner
        public virtual void HandlePlayerCollision(Player p1, Player p2)
        {
            if (p1.attackHitBox.Intersects(p2.damageableHitBox) && p1.isAttacking && !p2.isInvincible)
            {
                if (p1.facingRight)
                {
                    p2.speed.X += p1.knockBackModifierX / (p2.HP * 0.1f);
                    p2.speed.Y -= p1.knockBackModifierY / (p2.HP * 0.1f);
                }
                else if (!p1.facingRight)
                {
                    p2.speed.X -= p1.knockBackModifierX / (p2.HP * 0.1f);
                    p2.speed.Y -= p1.knockBackModifierY / (p2.HP * 0.1f);
                }
            }
            else
            {
                p2.isHit = false;
                p2.speed.X = 0;
            }

            if (p2.attackHitBox.Intersects(p1.damageableHitBox) && p2.isAttacking && !p1.isInvincible)
            {
                if (p2.facingRight)
                {
                    p1.speed.X += p2.knockBackModifierX / (p1.HP * 0.1f);
                    p1.speed.Y -= p2.knockBackModifierY / (p1.HP * 0.1f);
                }
                else if (!p2.facingRight)
                {
                    p1.speed.X -= p2.knockBackModifierX / (p1.HP * 0.1f);
                    p1.speed.Y -= p2.knockBackModifierY / (p1.HP * 0.1f);
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

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, color);
        }
    }
}
