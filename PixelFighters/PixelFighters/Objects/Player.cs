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
        public int bX, bY, stocksRemaining;
        private int jumpsAvailable, frame;
        public bool facingRight;
        private double frameTimer, frameInterval = 400;
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
            attackHitBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height - 16);
            facingRight = true;
            jumpsAvailable = 2;
            stocksRemaining = 3;
            maxHP = 50;
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

            if (isOnGround)
            {
                jumpsAvailable = 2;
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

            HandleInputs();

            if (isOnGround)
            {
                jumpsAvailable = 2;
            }

            if (frameTimer <= 0)
            {
                isAttacking = false;
                isInvincible = false;
            }

            if (pos.Y >= 900 || HP == 0)
            {
                HP = maxHP;
                stocksRemaining--;
                speed = Vector2.Zero;
                if (playerIndex == 1)
                {
                    pos = GameplayManager.Instance.startPosOne;
                }
                if (playerIndex == 2)
                {
                    pos = GameplayManager.Instance.startPosTwo;
                }
            }

            pos += speed;
            damageableHitBox.X = (int)pos.X - 25;
            damageableHitBox.Y = (int)pos.Y - 25;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (frameTimer > 0 && isAttacking)
            {
                spriteBatch.Draw(tex, attackHitBox, srcRec, Color.Black);
            }

            if (playerIndex == 1)
            {
                if (!isInvincible)
                {
                    color = Color.Red;
                    spriteBatch.Draw(tex, pos, srcRec, color, rotation, new Vector2(damageableHitBox.Width / 2, damageableHitBox.Height / 2), 1, playerFx, 1);
                }
                if (isInvincible)
                {
                    color = Color.Pink;
                    spriteBatch.Draw(tex, pos, srcRec, color * 0.5f, rotation, new Vector2(damageableHitBox.Width / 2, damageableHitBox.Height / 2), 1, playerFx, 1);
                }
            }
            if (playerIndex == 2)
            {
                if (!isInvincible)
                {
                    color = Color.Blue;
                    spriteBatch.Draw(tex, pos, srcRec, color, rotation, new Vector2(damageableHitBox.Width / 2, damageableHitBox.Height / 2), 1, playerFx, 1);
                }
                if (isInvincible)
                {
                    color = Color.Cyan;
                    spriteBatch.Draw(tex, pos, srcRec, color * 0.5f, rotation, new Vector2(damageableHitBox.Width / 2, damageableHitBox.Height / 2), 1, playerFx, 1);
                }
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

        ///Metoder för controller- och keyboard-inputs
        #region Input Methods
        public void HandleInputs()
        {
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
                            if (isOnGround)
                            {
                                speed.X *= 0.2f;
                            }
                        }
                    }
                }

                ///Keyboard inputs för P1
                if (keyState.IsKeyDown(Keys.D) && !isInvincible)
                {
                    facingRight = true;
                    speed.X = 5f;
                }
                else if (keyState.IsKeyDown(Keys.A) && !isInvincible)
                {
                    facingRight = false;
                    speed.X = -5f;
                }

                if (keyState.IsKeyDown(Keys.W) && previousKeyState.IsKeyUp(Keys.W) && !isInvincible && jumpsAvailable >= 1)
                {
                    speed.Y = -8;
                    isOnGround = false;
                    jumpsAvailable -= 1;
                }
                if (keyState.IsKeyDown(Keys.S))
                {
                    speed.Y += 5;
                    if (isOnGround)
                    {
                        speed.X *= 0.2f;
                    }
                }

                ///Basic attack
                if (keyState.IsKeyDown(Keys.G) && previousKeyState.IsKeyUp(Keys.G) && frameTimer < 0)
                {
                    isAttacking = true;
                    knockBackModifierX = 15;
                    knockBackModifierY = 2;
                    frameTimer = frameInterval * 0.5f;

                    if (facingRight)
                    {
                        attackHitBox.X = (int)pos.X + 25;
                        attackHitBox.Y = (int)pos.Y - 25;
                    }
                    else if (!facingRight)
                    {
                        attackHitBox.X = (int)pos.X - 75;
                        attackHitBox.Y = (int)pos.Y - 25;
                    }
                }
                else if (keyState.IsKeyDown(Keys.H) && previousKeyState.IsKeyUp(Keys.H) && frameTimer < 0)
                {
                    isAttacking = true;
                    knockBackModifierX = 5;
                    knockBackModifierY = 7;
                    frameTimer = frameInterval * 0.5f;

                    if (facingRight)
                    {
                        attackHitBox.X = (int)pos.X + 25;
                        attackHitBox.Y = (int)pos.Y + 5;
                    }
                    else if (!facingRight)
                    {
                        attackHitBox.X = (int)pos.X - 75;
                        attackHitBox.Y = (int)pos.Y + 5;
                    }
                }
                else if (keyState.IsKeyDown(Keys.J) && previousKeyState.IsKeyUp(Keys.J) && frameTimer < 0)
                {
                    isInvincible = true;
                    frameTimer = frameInterval;
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
                            if (isOnGround)
                            {
                                speed.X *= 0.2f;
                            }
                        }
                    }
                }

                ///Keyboard inputs för P2
                if (keyState.IsKeyDown(Keys.Right) && !isInvincible)
                {
                    facingRight = true;
                    speed.X = 5f;
                }
                else if (keyState.IsKeyDown(Keys.Left) && !isInvincible)
                {
                    facingRight = false;
                    speed.X = -5f;
                }

                if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) && !isInvincible && jumpsAvailable >= 1)
                {
                    speed.Y = -8;
                    isOnGround = false;
                    jumpsAvailable -= 1;
                }
                if (keyState.IsKeyDown(Keys.Down))
                {
                    speed.Y += 5;
                    if (isOnGround)
                    {
                        speed.X *= 0.2f;
                    }
                }

                ///Basic attack
                if (keyState.IsKeyDown(Keys.NumPad1) && previousKeyState.IsKeyUp(Keys.NumPad1) && frameTimer < 0)
                {
                    isAttacking = true;
                    knockBackModifierX = 15;
                    knockBackModifierY = 2;

                    if (facingRight)
                    {
                        attackHitBox.X = (int)pos.X + 25;
                        attackHitBox.Y = (int)pos.Y - 25;
                        frameTimer = frameInterval * 0.5f;
                    }
                    else if (!facingRight)
                    {
                        attackHitBox.X = (int)pos.X - 75;
                        attackHitBox.Y = (int)pos.Y - 25;
                        frameTimer = frameInterval * 0.5f;
                    }
                }
                else if (keyState.IsKeyDown(Keys.NumPad2) && previousKeyState.IsKeyUp(Keys.NumPad2) && frameTimer < 0)
                {
                    isAttacking = true;
                    knockBackModifierX = 5;
                    knockBackModifierY = 7;

                    if (facingRight)
                    {
                        attackHitBox.X = (int)pos.X + 25;
                        attackHitBox.Y = (int)pos.Y + 5;
                        frameTimer = frameInterval * 0.5f;
                    }
                    else if (!facingRight)
                    {
                        attackHitBox.X = (int)pos.X - 75;
                        attackHitBox.Y = (int)pos.Y + 5;
                        frameTimer = frameInterval * 0.5f;
                    }
                }
                else if (keyState.IsKeyDown(Keys.NumPad3) && previousKeyState.IsKeyUp(Keys.NumPad3) && frameTimer < 0)
                {
                    isInvincible = true;
                    frameTimer = frameInterval;
                }
            }
            #endregion
        }
        #endregion
    }
}