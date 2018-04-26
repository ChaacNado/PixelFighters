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
        SpriteEffects playerFx = SpriteEffects.None;
        public int bX, bY, stocksRemaining;
        private int jumpsAvailable, frame;
        private float rotation = 0;
        private double frameTimer, frameInterval = 400;
        public bool facingRight;
        public bool inAnimation;
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
            attackHitBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height);
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

            isDunking = false;
            isHit = false;

            if (frameTimer <= 0)
            {
                inAnimation = false;
            }

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

            attackHitBox.X = (int)pos.X + rangeModifierX;
            attackHitBox.Y = (int)pos.Y + rangeModifierY;
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
                if (keyState.IsKeyDown(Keys.D) && !isInvincible && !inAnimation)
                {
                    facingRight = true;
                    speed.X = 5f;
                }
                else if (keyState.IsKeyDown(Keys.A) && !isInvincible && !inAnimation)
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
                if (keyState.IsKeyDown(Keys.S) && !inAnimation)
                {
                    speed.Y += 5;
                    if (isOnGround)
                    {
                        speed.X *= 0.2f;
                    }
                }

                ///Basic attack
                if(frameTimer <= -75)
                {
                    if (keyState.IsKeyDown(Keys.H) && previousKeyState.IsKeyUp(Keys.H) && frameTimer < 0)
                    {
                        frameTimer = frameInterval * 0.5f;
                        isAttacking = true;
                        knockBackModifierX = 15;
                        knockBackModifierY = 2;
                        attackHitBox.Width = 64;
                        attackHitBox.Height = 24;
                        rangeModifierY = -20;

                        if (facingRight)
                        {
                            rangeModifierX = 0;
                        }
                        else if (!facingRight)
                        {
                            rangeModifierX = -64;
                        }
                    }
                    if (keyState.IsKeyDown(Keys.J) && previousKeyState.IsKeyUp(Keys.J) && frameTimer < 0)
                    {
                        frameTimer = frameInterval * 0.4f;
                        if (isOnGround)
                        {
                            isAttacking = true;
                            knockBackModifierX = 5;
                            knockBackModifierY = 7;
                            attackHitBox.Width = 52;
                            attackHitBox.Height = 16;
                            rangeModifierY = 8;

                            if (facingRight)
                            {
                                rangeModifierX = 0;
                            }
                            else if (!facingRight)
                            {
                                rangeModifierX = -52;
                            }
                        }
                        else
                        {
                            isDunking = true;
                            if (frameTimer >= 160)
                            {
                                isAttacking = true;
                                knockBackModifierX = 1;
                                knockBackModifierY = -15;
                                attackHitBox.Width = 24;
                                attackHitBox.Height = 32;
                                rangeModifierX = -12;
                                rangeModifierY = 12;
                            }
                        }
                    }
                    if (keyState.IsKeyDown(Keys.Y) && previousKeyState.IsKeyUp(Keys.Y) && frameTimer < 0)
                    {
                        frameTimer = frameInterval * 0.9f;
                        if (frameTimer >= 340)
                        {
                            isAttacking = true;
                        }
                        inAnimation = true;
                        knockBackModifierX = 10;
                        knockBackModifierY = 3;
                        attackHitBox.Width = 32;
                        attackHitBox.Height = 32;
                        rangeModifierY = -15;

                        if (facingRight)
                        {
                            rangeModifierX = 0;
                            speed.X = 25f;
                        }
                        else if (!facingRight)
                        {
                            rangeModifierX = -32;
                            speed.X = -25f;
                        }
                    }
                }
                if (frameTimer <= -150)
                {
                    if (keyState.IsKeyDown(Keys.K) && previousKeyState.IsKeyUp(Keys.K) && frameTimer < 0)
                    {
                        isInvincible = true;
                        frameTimer = frameInterval;
                    }
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
                if (keyState.IsKeyDown(Keys.Right) && !isInvincible && !inAnimation)
                {
                    facingRight = true;
                    speed.X = 5f;
                }
                else if (keyState.IsKeyDown(Keys.Left) && !isInvincible && !inAnimation)
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
                if (keyState.IsKeyDown(Keys.Down) && !inAnimation)
                {
                    speed.Y += 5;
                    if (isOnGround)
                    {
                        speed.X *= 0.2f;
                    }
                }

                ///Basic attack
                if (frameTimer <= -75)
                {
                    if (keyState.IsKeyDown(Keys.NumPad1) && previousKeyState.IsKeyUp(Keys.NumPad1) && frameTimer < 0)
                    {
                        frameTimer = frameInterval * 0.5f;
                        isAttacking = true;
                        knockBackModifierX = 15;
                        knockBackModifierY = 2;
                        attackHitBox.Width = 64;
                        attackHitBox.Height = 24;
                        rangeModifierY = -20;

                        if (facingRight)
                        {
                            rangeModifierX = 0;
                        }
                        else if (!facingRight)
                        {
                            rangeModifierX = -64;
                        }
                    }
                    if (keyState.IsKeyDown(Keys.NumPad2) && previousKeyState.IsKeyUp(Keys.NumPad2) && frameTimer < 0)
                    {
                        frameTimer = frameInterval * 0.4f;
                        if (isOnGround)
                        {
                            isAttacking = true;
                            knockBackModifierX = 5;
                            knockBackModifierY = 7;
                            attackHitBox.Width = 52;
                            attackHitBox.Height = 16;
                            rangeModifierY = 8;

                            if (facingRight)
                            {
                                rangeModifierX = 0;
                            }
                            else if (!facingRight)
                            {
                                rangeModifierX = -52;
                            }
                        }
                        else
                        {
                            isDunking = true;
                            if (frameTimer >= 160)
                            {
                                isAttacking = true;
                                knockBackModifierX = 1;
                                knockBackModifierY = -15;
                                attackHitBox.Width = 24;
                                attackHitBox.Height = 32;
                                rangeModifierX = -12;
                                rangeModifierY = 12;
                            }
                        }
                    }
                    if (keyState.IsKeyDown(Keys.NumPad4) && previousKeyState.IsKeyUp(Keys.NumPad4) && frameTimer < 0)
                    {
                        frameTimer = frameInterval * 0.9f;
                        if (frameTimer >= 340)
                        {
                            isAttacking = true;
                        }
                        inAnimation = true;
                        knockBackModifierX = 10;
                        knockBackModifierY = 3;
                        attackHitBox.Width = 32;
                        attackHitBox.Height = 32;
                        rangeModifierY = -15;

                        if (facingRight)
                        {
                            rangeModifierX = 0;
                            speed.X = 25f;
                        }
                        else if (!facingRight)
                        {
                            rangeModifierX = -32;
                            speed.X = -25f;
                        }
                    }
                }
                if (frameTimer <= -150)
                {
                    if (keyState.IsKeyDown(Keys.NumPad3) && previousKeyState.IsKeyUp(Keys.NumPad3) && frameTimer < 0)
                    {
                        isInvincible = true;
                        frameTimer = frameInterval;
                    }
                }
            }
            #endregion
        }
        #endregion
    }
}