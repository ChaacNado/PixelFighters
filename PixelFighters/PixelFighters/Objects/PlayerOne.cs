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
    public class PlayerOne : MovingObject
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
        public PlayerOne(Texture2D tex, Vector2 pos, Rectangle srcRec) : base(tex, pos, srcRec)
        {
            this.srcRec = srcRec;
            speed = new Vector2(0, 0);
            bY = (int)ScreenManager.Instance.Dimensions.Y;
            bX = (int)ScreenManager.Instance.Dimensions.X;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height);
            groundHitBox = new Rectangle((int)pos.X + 32, (int)pos.Y + 32, srcRec.Width, 1);
            hurtBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height - 16);
            color = Color.Red;
            facingRight = true;
            testAttack = false;
            jumpsAvailable = 2;

        }
        #endregion
        
        #region Main Methods
        public override void Update(GameTime gameTime)
        {
            previousKeyState = keyState;
            keyState = Keyboard.GetState();

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

            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            if (capabilities.IsConnected)
            {
                // Get the current state of Controller1
                previousGamePadState = gamePadState;
                gamePadState = GamePad.GetState(PlayerIndex.One);

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
                        speed.Y = -8;
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

            
            if (keyState.IsKeyDown(Keys.D)/* && pos.X < 1360*/)
            {
                facingRight = true;
                speed.X = 5f;
            }
            else if (keyState.IsKeyDown(Keys.A)/* && pos.X > bX*/)
            {
                facingRight = false;
                speed.X = -5f;
            }

            if (keyState.IsKeyDown(Keys.W) && previousKeyState.IsKeyUp(Keys.W) && jumpsAvailable >= 1)
            {

                //Hoppar högre än platformen
                speed.Y = -8;
                isOnGround = false;
                jumpsAvailable -= 1;

            }
            //OM man klickar på ner knappen, går snabbare ner.
            else if (keyState.IsKeyDown(Keys.S))
            {
                speed.Y = 10;

            }
            if (isOnGround)
            {
                jumpsAvailable = 2;
            }

            if (keyState.IsKeyDown(Keys.X) && previousKeyState.IsKeyUp(Keys.X))
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

            if (keyState.IsKeyDown(Keys.X) && previousKeyState.IsKeyUp(Keys.X))
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
                pos.X = 140;
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
        //v.0.1.3 skapat kollisionsmetoder
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


        #region Attack Methods



        #endregion
    }
}