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
    public class Player : MovingObject
    {
        #region Variables
        KeyboardState keyState, previousKeyState;
        GamePadState gamePadState, previousGamePadState;
        GamePadCapabilities capabilities;
        private float rotation = 0;
        SpriteEffects playerFx = SpriteEffects.None;
        public int bX, bY, stocksRemaining, HP;
        private int jumpsAvailable, frame;
        public bool facingRight;
        private double frameTimer, frameInterval;
        #endregion

        #region Player Object
        public Player(Texture2D tex, Vector2 pos, Rectangle srcRec, int playerIndex) : base(tex, pos, srcRec)
        {
            this.srcRec = srcRec;
            this.playerIndex = playerIndex;
            speed = new Vector2(0, 0);
            bY = (int)ScreenManager.Instance.Dimensions.Y;
            bX = (int)ScreenManager.Instance.Dimensions.X;
            damageableHitBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height);
            groundHitBox = new Rectangle((int)pos.X + 32, (int)pos.Y + 32, srcRec.Width, 1);
            attackhitBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height - 16);
            facingRight = true;
            isHit = false;
            jumpsAvailable = 2;
            stocksRemaining = 3;
            HP = 10;
            frameTimer = 150;
            frameInterval = 150;
        }
        #endregion

        #region Main Methods
        public override void Update(GameTime gameTime)
        {
            previousKeyState = keyState;
            keyState = Keyboard.GetState();

            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

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
                if (speed.Y >= 20)
                {
                    speed.Y = 20;
                }
                if (jumpsAvailable == 2)
                {
                    jumpsAvailable = 1;
                }
            }

            speed.X *= 0.9f;

            ///Kod för controller- och keyboard-inputs
            #region P1
            if (playerIndex == 1)
            {
                ///Controller inputs för P1
                capabilities = GamePad.GetCapabilities(PlayerIndex.One);

                if (capabilities.IsConnected)
                {
                    previousGamePadState = gamePadState;
                    gamePadState = GamePad.GetState(PlayerIndex.One);

                    if (capabilities.HasDPadRightButton && this.capabilities.HasDPadLeftButton)
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
                            speed.Y = -8;
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

                ///Keyboard inputs för P1
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
                    speed.Y = -8;
                    isOnGround = false;
                    jumpsAvailable -= 1;
                }
                else if (keyState.IsKeyDown(Keys.S))
                {
                    speed.Y += 5;
                }

                ///Basic attack
                if (keyState.IsKeyDown(Keys.X) && previousKeyState.IsKeyUp(Keys.X) && frameTimer < 0)
                {
                    isAttacking = true;

                    if (facingRight)
                    {
                        attackhitBox.X = (int)pos.X + 25;
                        frameTimer = frameInterval;
                    }
                    else if (!facingRight)
                    {
                        attackhitBox.X = (int)pos.X - 75;
                        frameTimer = frameInterval;
                    }
                }
                else
                {
                    isAttacking = false;

                    //attackhitBox.X = (int)pos.X - 25; Denna förhindrar attackerna från att vara lite saktare. Vad är den till egentligen? /Jonas
                }
            }

            #endregion

            #region P2
            if (playerIndex == 2)
            {
                ///Controller inputs för P2
                capabilities = GamePad.GetCapabilities(PlayerIndex.Two);

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

                ///Keyboard inputs för P2
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

                ///Basic attack

                if (keyState.IsKeyDown(Keys.NumPad0) && previousKeyState.IsKeyUp(Keys.NumPad0) && frameTimer < 0)
                {
                    isAttacking = true;
                    if (facingRight)
                    {
                        attackhitBox.X = (int)pos.X + 25;
                        frameTimer = frameInterval;
                    }
                    else if (!facingRight)
                    {
                        attackhitBox.X = (int)pos.X - 75;
                        frameTimer = frameInterval;
                    }
                }
                else
                {
                    isAttacking = false;
                    //attackhitBox.X = (int)pos.X - 25; Denna förhindrar attackerna från att vara lite saktare. Vad är den till egentligen? /Jonas
                }
            }
            #endregion

            if (isOnGround)
            {
                jumpsAvailable = 2;
            }

            if (pos.Y >= 900 || HP == 0)
            {
                HP = 10;
                stocksRemaining--;
                pos.X = 140;
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
            if (frameTimer > 0)
            {
                spriteBatch.Draw(tex, attackhitBox, srcRec, Color.Black);
            }

            if (playerIndex == 1)
            {
                color = Color.Red;
                spriteBatch.Draw(tex, pos, srcRec, color, rotation, new Vector2(damageableHitBox.Width / 2, damageableHitBox.Height / 2), 1, playerFx, 1);
            }

            if (playerIndex == 2)
            {
                color = Color.Blue;
                spriteBatch.Draw(tex, pos, srcRec, color, rotation, new Vector2(damageableHitBox.Width / 2, damageableHitBox.Height / 2), 1, playerFx, 1);
            }
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