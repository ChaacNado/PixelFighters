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
        public Vector2 speed;
        float rotation = 0;
        SpriteEffects playerFx = SpriteEffects.None;
        public int bX, bY;
        private int jumpsAvailable;
        public bool facingRight;
        public bool testAttack;
        #endregion

        #region Player Object
        public PlayerTwo(Texture2D tex, Vector2 pos, Rectangle srcRec) : base(tex, pos, srcRec)
        {
            this.srcRec = srcRec;
            speed = new Vector2(0, 0);
            bX = (int)ScreenManager.Instance.Dimensions.X;
            bY = (int)ScreenManager.Instance.Dimensions.Y;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height);
            groundHitBox = new Rectangle((int)pos.X + 32, (int)pos.Y + 32, srcRec.Width, 1);
            hurtBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height - 16);
            color = Color.Blue;
            facingRight = false;
            testAttack = false;
            jumpsAvailable = 2;
        }
        #endregion

        #region Main Methods
        public override void Update(GameTime gameTime)
        {
            ///Till att börja med kan PlayerTwo kodas till att agera som en punching-bag NPC möjligtvis

            previousKeyState = keyState;
            keyState = Keyboard.GetState();

            System.Diagnostics.Debug.WriteLine(jumpsAvailable);

            if (facingRight == true)
            {
                playerFx = SpriteEffects.None;
            }

            if (facingRight == false)
            {
                playerFx = SpriteEffects.FlipHorizontally;
            }

            if (!isOnGround)
            {
                speed.Y += 0.2f;
            }

            speed.X = 0;

            #region GamePad kod
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.Two);
            
            if (capabilities.IsConnected)
            {
                // Get the current state of Controller1
                previousGamePadState = gamePadState;
                gamePadState = GamePad.GetState(PlayerIndex.Two);

                // You can check explicitly if a gamepad has support for a certain feature
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

                // You can also check the controllers "type"
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
                        speed.Y = 10;
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
                //Hoppar högre än platformen
                speed.Y = -8;
                isOnGround = false;
                jumpsAvailable -= 1;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                speed.Y = 10;
            }

            if (isOnGround)
            {
                jumpsAvailable = 2;
            }

            if (keyState.IsKeyDown(Keys.NumPad0) && previousKeyState.IsKeyUp(Keys.NumPad0))
            {
                testAttack = true;

                if (facingRight == true)
                {
                    hurtBox.X = (int)pos.X + 25;
                }
                else if (facingRight == false)
                {
                    hurtBox.X = (int)pos.X - 50;
                }
            }
            else
            {
                testAttack = false;

                hurtBox.X = (int)pos.X - 25;
            }

            if (pos.Y >= 900)
            {
                pos.X = 1050;
                pos.Y = 300;
            }

            pos += speed;
            hitBox.X = (int)pos.X - 25;
            hitBox.Y = (int)pos.Y - 25;
            hurtBox.Y = (int)pos.Y - 25;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, srcRec, color, rotation, new Vector2(hitBox.Width / 2, hitBox.Height / 2), 1, playerFx, 1);
        }
        #endregion

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
