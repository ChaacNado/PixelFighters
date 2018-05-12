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
        public double frameTimer, frameInterval = 400;
        public bool facingRight, inAnimation;
        private Keys jabInput, lowInput, dashInput, dodgeInput, jumpInput, leftInput, downInput, rightInput;
        private PlayerIndex controllerIndex;
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
            currentHP = maxHP;

            InitializeInputs();
        }
        #endregion

        #region Main Methods
        public override void Update(GameTime gameTime)
        {
            previousKeyState = keyState;
            keyState = Keyboard.GetState();
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            isDunking = false;

            if (frameTimer <= 0)
            {
                inAnimation = false;
                isAttacking = false;
                isInvincible = false;
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

            ///Vad som leder till att man förlorar en stock
            if (pos.Y >= bY || pos.Y <= -bY / 3 || pos.X <= -300 || pos.X >= bX + 300 || currentHP <= 0)
            {
                currentHP = maxHP;
                stocksRemaining--;
                speed = Vector2.Zero;
                if (playerIndex == 1)
                {
                    pos = StageManager.Instance.startPosOne;
                }
                if (playerIndex == 2)
                {
                    pos = StageManager.Instance.startPosTwo;
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
            ///Ritar ut strids-hitbox
            if (frameTimer > 0 && isAttacking)
            {
                spriteBatch.Draw(tex, attackHitBox, srcRec, Color.Black);
            }

            if (playerIndex == 1)
            {
                if (!isInvincible)
                {
                    if (isHit)
                    {
                        color = Color.White;
                    }
                    else
                    {
                        color = Color.Red;
                    }
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
                    if (isHit)
                    {
                        color = Color.White;
                    }
                    else
                    {
                        color = Color.Blue;
                    }
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

        ///Håller spelaren ovanpå platformar
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
        public void InitializeInputs()
        {
            ///Inputs beroende på spelare
            if (playerIndex == 1)
            {
                jabInput = Keys.J;
                dashInput = Keys.K;
                lowInput = Keys.L;
                dodgeInput = Keys.O;
                jumpInput = Keys.W;
                leftInput = Keys.A;
                downInput = Keys.S;
                rightInput = Keys.D;
                controllerIndex = PlayerIndex.One;
            }
            if (playerIndex == 2)
            {
                jabInput = Keys.NumPad1;
                dashInput = Keys.NumPad2;
                lowInput = Keys.NumPad3;
                dodgeInput = Keys.NumPad6;
                jumpInput = Keys.Up;
                leftInput = Keys.Left;
                downInput = Keys.Down;
                rightInput = Keys.Right;
                controllerIndex = PlayerIndex.Two;
            }
        }

        public void HandleInputs()
        {
            ///Controller-inputs
            #region Controller
            capabilities = GamePad.GetCapabilities(controllerIndex);

            if (capabilities.IsConnected)
            {
                previousGamePadState = gamePadState;
                gamePadState = GamePad.GetState(controllerIndex);

                if (capabilities.HasDPadRightButton && this.capabilities.HasDPadLeftButton)
                {
                    if (gamePadState.DPad.Right == ButtonState.Pressed && !isInvincible && !inAnimation)
                    {
                        facingRight = true;
                        speed.X = 5;
                    }
                    if (gamePadState.DPad.Left == ButtonState.Pressed && !isInvincible && !inAnimation)
                    {
                        facingRight = false;
                        speed.X = -5;
                    }
                }

                if (capabilities.GamePadType == GamePadType.GamePad)
                {
                    if (jumpsAvailable >= 1 && !isInvincible)
                    {
                        if (gamePadState.IsButtonDown(Buttons.DPadUp) && previousGamePadState.IsButtonUp(Buttons.DPadUp) || gamePadState.IsButtonDown(Buttons.Y) && previousGamePadState.IsButtonUp(Buttons.Y))
                        {
                            speed.Y = -8;
                            isOnGround = false;
                            jumpsAvailable -= 1;
                        }
                    }
                    if (gamePadState.IsButtonDown(Buttons.DPadDown) && !inAnimation)
                    {
                        speed.Y += 5;
                        if (isOnGround)
                        {
                            speed.X *= 0.2f;
                        }
                    }
                }
                ///Strids-inputs
                if (frameTimer <= -75)
                {
                    ///Jab attack
                    if (gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A) && frameTimer < 0)
                    {
                        CharacterManager.Instance.JabAttack(this);
                    }
                    ///Low attack
                    if (gamePadState.IsButtonDown(Buttons.B) && previousGamePadState.IsButtonUp(Buttons.B) && frameTimer < 0)
                    {
                        if (isOnGround)
                        {
                            CharacterManager.Instance.LowAttack(this);
                        }
                        ///Air dunk
                        else
                        {
                            CharacterManager.Instance.AirDunk(this);
                        }
                    }
                    ///Dash attack
                    if (gamePadState.IsButtonDown(Buttons.X) && previousGamePadState.IsButtonUp(Buttons.X) && frameTimer < 0)
                    {
                        CharacterManager.Instance.DashAttack(this);
                    }
                }
                if (frameTimer <= -150)
                {
                    ///Dodge
                    if (frameTimer < 0)
                    {
                        if (gamePadState.IsButtonDown(Buttons.RightTrigger) && previousGamePadState.IsButtonUp(Buttons.RightTrigger) || gamePadState.IsButtonDown(Buttons.LeftTrigger) && previousGamePadState.IsButtonUp(Buttons.LeftTrigger))
                        {
                            isInvincible = true;
                            frameTimer = frameInterval;
                        }
                    }
                }

            }
            #endregion

            ///Keyboard-inputs
            #region Keyboard
            if (keyState.IsKeyDown(rightInput) && !isInvincible && !inAnimation)
            {
                facingRight = true;
                speed.X = 5;
            }
            else if (keyState.IsKeyDown(leftInput) && !isInvincible && !inAnimation)
            {
                facingRight = false;
                speed.X = -5;
            }

            if (keyState.IsKeyDown(jumpInput) && previousKeyState.IsKeyUp(jumpInput) && !isInvincible && jumpsAvailable >= 1)
            {
                speed.Y = -8;
                isOnGround = false;
                jumpsAvailable -= 1;
            }
            if (keyState.IsKeyDown(downInput) && previousKeyState.IsKeyUp(jumpInput) && !inAnimation)
            {
                speed.Y += 5;
                if (isOnGround)
                {
                    speed.X *= 0.2f;
                }
            }

            ///Strids-inputs
            if (frameTimer <= -75)
            {
                ///Jab attack
                if (keyState.IsKeyDown(jabInput) && previousKeyState.IsKeyUp(jabInput) && frameTimer < 0)
                {
                    CharacterManager.Instance.JabAttack(this);
                }
                ///Low attack
                if (keyState.IsKeyDown(lowInput) && previousKeyState.IsKeyUp(lowInput) && frameTimer < 0)
                {
                    if (isOnGround)
                    {
                        CharacterManager.Instance.LowAttack(this);
                    }
                    ///Air dunk
                    else
                    {
                        CharacterManager.Instance.AirDunk(this);
                    }
                }
                ///Dash attack
                if (keyState.IsKeyDown(dashInput) && previousKeyState.IsKeyUp(dashInput) && frameTimer < 0)
                {
                    CharacterManager.Instance.DashAttack(this);
                }
            }
            if (frameTimer <= -150)
            {
                ///Dodge
                if (keyState.IsKeyDown(dodgeInput) && previousKeyState.IsKeyUp(dodgeInput) && frameTimer < 0)
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