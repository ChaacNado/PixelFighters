using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    public class PlayerTwo : MovingObject
    {
        #region Variables
        KeyboardState keyState, previousKeyState;
        GamePadState gamePadState, previousGamePadState;
        float rotation = 0;
        SpriteEffects playerFx = SpriteEffects.None;
        public int bX, bY;
        private int jumpsAvailable;
        public bool facingRight;
        public int stocksRemaining;
        public int HP;
        #endregion

        #region Player Object
        public PlayerTwo(Texture2D tex, Vector2 pos, Rectangle srcRec) : base(tex, pos, srcRec)
        {
            this.srcRec = srcRec;
            speed = new Vector2(0, 0);
            bX = (int)ScreenManager.Instance.Dimensions.X;
            bY = (int)ScreenManager.Instance.Dimensions.Y;
            damageableHitBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height);
            groundHitBox = new Rectangle((int)pos.X + 32, (int)pos.Y + 32, srcRec.Width, 1);
            attackhitBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height - 16);
            color = Color.Blue;
            facingRight = false;
            isHit = false;
            jumpsAvailable = 2;
            stocksRemaining = 3;
            HP = 10;
        }
        #endregion

        #region Main Methods
        public override void Update(GameTime gameTime)
        {
            previousKeyState = keyState;
            keyState = Keyboard.GetState();

            if (facingRight)
            {
                playerFx = SpriteEffects.None;
            }

            if (!facingRight)
            {
                playerFx = SpriteEffects.FlipHorizontally;
            }

            if (!isOnGround)
            {
                speed.Y += 0.2f;
                if(speed.Y >= 20)
                {
                    speed.Y = 20;
                }
                if (jumpsAvailable == 2)
                {
                    jumpsAvailable = 1;
                }
            }

            speed.X *= 0.9f;

            ///Kod för kontroller-inputs
            #region GamePad
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.Two);
            
            if (capabilities.IsConnected)
            {
                previousGamePadState = gamePadState;
                gamePadState = GamePad.GetState(PlayerIndex.Two);

                if (capabilities.HasDPadRightButton && capabilities.HasDPadLeftButton)
                {
                    if (gamePadState.DPad.Right == ButtonState.Pressed)
                    {
                        facingRight = true;
                        speed.X = 5f;
                    }
                    if (gamePadState.DPad.Left == ButtonState.Pressed)
                    {
                        facingRight = false;
                        speed.X = -5f;
                    }
                }

                if (capabilities.GamePadType == GamePadType.GamePad)
                {
                    if (gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A) && jumpsAvailable >= 1)
                    {
                        speed.Y = -10;
                        isOnGround = false;
                        jumpsAvailable = -1;
                    }
                    if (gamePadState.IsButtonDown(Buttons.DPadDown))
                    {
                        speed.Y += 5;
                    }
                }
                if (isOnGround)
                {
                    jumpsAvailable = 2;
                }
            }
            #endregion
            
            if (keyState.IsKeyDown(Keys.Right)/* && pos.X < 1360*/)
            {
                facingRight = true;
                speed.X = 5f;
            }
            else if (keyState.IsKeyDown(Keys.Left)/* && pos.X > bX*/)
            {
                facingRight = false;
                speed.X = -5f;
            }

            if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) && jumpsAvailable >= 1)
            {
                speed.Y = -8;
                isOnGround = false;
                jumpsAvailable -= 1;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                speed.Y += 5;
            }

            if (isOnGround)
            {
                jumpsAvailable = 2;
            }

            if (keyState.IsKeyDown(Keys.NumPad0) && previousKeyState.IsKeyUp(Keys.NumPad0))
            {
                isAttacking = true;
                if (facingRight)
                {
                    attackhitBox.X = (int)pos.X + 25;
                }
                else if (!facingRight)
                {
                    attackhitBox.X = (int)pos.X - 50;
                }
            }
            else
            {
                isAttacking = false;
                attackhitBox.X = (int)pos.X - 25;
            }

            if (pos.Y >= 900 || HP == 0)
            {
                HP = 10;
                stocksRemaining--;
                pos.X = 1050;
                pos.Y = 300;
                speed = Vector2.Zero;
            }

            pos += speed;
            damageableHitBox.X = (int)pos.X - 25;
            damageableHitBox.Y = (int)pos.Y - 25;
            attackhitBox.Y = (int)pos.Y - 25;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, srcRec, color, rotation, new Vector2(damageableHitBox.Width / 2, damageableHitBox.Height / 2), 1, playerFx, 1);
        }
        #endregion

        ///Skriver över kollisionsmetoderna i MovingObject
        #region Collision Methods
        public override void HandleTopCollision(Platform p)
        {
            speed.Y = 0;
            isOnGround = true;
            base.HandleTopCollision(p);
        }

        public override void HandleBottomCollision(Platform p)
        {
            if (!isOnGround)
            {
                speed.Y = +2;
            }
            pos.X -= speed.X * 1.05f;

            base.HandleBottomCollision(p);
        }
        #endregion
    }
}
