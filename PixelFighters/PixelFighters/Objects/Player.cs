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
        Game1 game;
        GameState currentGamestate;
        public KeyboardState keyState, previousKeyState;
        public GamePadState gamePadState, previousGamePadState;
        GamePadCapabilities capabilities;

        SpriteEffects playerFx = SpriteEffects.None;
        Texture2D attackTex;
        public string characterName;
        public int bX, bY, stocksRemaining, currentCharacter, srcWidthModifier, srcHeightModifier;
        private int jumpsAvailable, frame;
        private float rotation = 0;
        public double frameTimer, attackFrameTimer, frameInterval = 150;
        public bool facingRight, inAnimation, moving;
        public Keys jabInput, lowInput, dashInput, dodgeInput, jumpInput, leftInput, downInput, rightInput;
        private PlayerIndex controllerIndex;

        #endregion

        #region Player Object
        public Player(Texture2D tex, Texture2D attackTex, Vector2 pos, Rectangle srcRec, int playerIndex, Game1 game, bool facingRight) : base(tex, pos, srcRec)
        {
            this.srcRec = srcRec;
            this.attackTex = attackTex;
            this.playerIndex = playerIndex;
            speed = new Vector2(0, 0);
            bY = (int)ScreenManager.Instance.Dimensions.Y;
            bX = (int)ScreenManager.Instance.Dimensions.X;
            damageableHitBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height);
            groundHitBox = new Rectangle((int)pos.X + 32, (int)pos.Y + 32, srcRec.Width, 1);
            attackHitBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height);
            this.facingRight = facingRight;
            jumpsAvailable = 2;
            stocksRemaining = 3;
            maxHP = 50;
            currentHP = maxHP;
            this.game = game;

            InitializeInputs();
        }
        #endregion

        #region Main Methods
        public override void Update(GameTime gameTime)
        {
            currentGamestate = game.currentGameState;

            previousKeyState = keyState;
            keyState = Keyboard.GetState();

            CharacterManager.Instance.SelectedCharacter(this);

            if (currentGamestate == GameState.Playtime)
            {

                attackFrameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

                isDunking = false;

                if (frameTimer <= 0 && moving && isOnGround)
                {
                    frameTimer = frameInterval;
                    frame++;

                    srcRec.X = (frame % 4) * srcWidthModifier;
                    srcRec.Width = srcWidthModifier;
                    srcRec.Height = srcHeightModifier;

                }
                else if (!moving && !inAnimation && !isAttacking && isOnGround)
                {
                    srcRec.X = 0;

                    srcRec.Width = srcWidthModifier;
                    srcRec.Height = srcHeightModifier;
                }

                if (attackFrameTimer <= 0)
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

                if (isOnGround && moving)
                {
                    moving = false;
                }
                HandleInputs();

                ///Vad som leder till att man förlorar en stock
                if (pos.Y >= bY + 300 || pos.Y <= -bY / 3 || pos.X <= -300 || pos.X >= bX + 300 || currentHP <= 0)
                {
                    currentHP = maxHP;
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
                damageableHitBox.X = (int)pos.X - srcWidthModifier / 2;
                damageableHitBox.Y = (int)pos.Y - srcHeightModifier / 2;

                groundHitBox.X = (int)pos.X - srcWidthModifier / 2;
                groundHitBox.Y = (int)pos.Y + srcHeightModifier / 2;

                attackHitBox.X = (int)pos.X + rangeModifierX;
                attackHitBox.Y = (int)pos.Y + rangeModifierY;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ///Ritar ut strids-hitbox
            if (attackFrameTimer > 0 && isAttacking)
            {
                spriteBatch.Draw(attackTex, attackHitBox, attackHitBox, Color.Red * 0.7f);
            }

            spriteBatch.Draw(attackTex, damageableHitBox, damageableHitBox, Color.Yellow * 0.7f);
            spriteBatch.Draw(attackTex, groundHitBox, groundHitBox, Color.Blue * 0.7f);

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
                        color = Color.White;
                    }
                    spriteBatch.Draw(tex, pos, srcRec, color, rotation, new Vector2(srcRec.Width / 2, srcRec.Height / 2), 1, playerFx, 1);
                }
                if (isInvincible)
                {
                    color = Color.Pink;
                    spriteBatch.Draw(tex, pos, srcRec, color * 0.5f, rotation, new Vector2(srcRec.Width / 2, srcRec.Height / 2), 1, playerFx, 1);
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
                        color = Color.White;
                    }
                    spriteBatch.Draw(tex, pos, srcRec, color, rotation, new Vector2(srcRec.Width / 2, srcRec.Height / 2), 1, playerFx, 1);
                }
                if (isInvincible)
                {
                    color = Color.Cyan;
                    spriteBatch.Draw(tex, pos, srcRec, color * 0.5f, rotation, new Vector2(srcRec.Width / 2, srcRec.Height / 2), 1, playerFx, 1);
                }
            }
        }
        #endregion

        ///Skriver över kollisionsmetoderna i MovingObject
        #region Collision Methods
        public override void HandleTopCollision(Platform platform)
        {
            speed.Y = 0;
            isOnGround = true;
            base.HandleTopCollision(platform);
        }

        ///Håller spelaren ovanpå platformar
        public override void HandleBottomCollision(Platform platform)
        {
            if (!isOnGround)
            {
                speed.Y = +2;
            }
            pos.X -= speed.X * 1.05f;

            base.HandleBottomCollision(platform);
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
                        if (isOnGround)
                        {
                            moving = true;
                        }
                    }
                    if (gamePadState.DPad.Left == ButtonState.Pressed && !isInvincible && !inAnimation)
                    {
                        facingRight = false;
                        speed.X = -5;
                        if (isOnGround)
                        {
                            moving = true;
                        }
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
                            moving = false;

                            if (currentCharacter == 1)
                            {
                                srcRec.X = 307;
                                srcRec.Width = srcWidthModifier;
                            }
                            else if (currentCharacter == 2)
                            {
                                srcRec.X = 488;
                            }
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
                if (attackFrameTimer <= -75)
                {
                    ///Jab attack
                    if (gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A) && attackFrameTimer < 0)
                    {
                        CharacterManager.Instance.JabAttack(this);
                    }
                    ///Low attack
                    if (gamePadState.IsButtonDown(Buttons.B) && previousGamePadState.IsButtonUp(Buttons.B) && attackFrameTimer < 0)
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
                    if (gamePadState.IsButtonDown(Buttons.X) && previousGamePadState.IsButtonUp(Buttons.X) && attackFrameTimer < 0)
                    {
                        CharacterManager.Instance.DashAttack(this);
                    }
                }
                if (attackFrameTimer <= -150)
                {
                    ///Dodge
                    if (attackFrameTimer < 0)
                    {
                        if (gamePadState.IsButtonDown(Buttons.RightTrigger) && previousGamePadState.IsButtonUp(Buttons.RightTrigger) || gamePadState.IsButtonDown(Buttons.LeftTrigger) && previousGamePadState.IsButtonUp(Buttons.LeftTrigger))
                        {
                            isInvincible = true;
                            attackFrameTimer = frameInterval;
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
                if (isOnGround)
                {
                    moving = true;
                }
            }
            else if (keyState.IsKeyDown(leftInput) && !isInvincible && !inAnimation)
            {
                facingRight = false;
                speed.X = -5;
                if (isOnGround)
                {
                    moving = true;
                }
            }

            if (keyState.IsKeyDown(jumpInput) && previousKeyState.IsKeyUp(jumpInput) && !isInvincible && jumpsAvailable >= 1)
            {
                speed.Y = -8;
                isOnGround = false;
                jumpsAvailable -= 1;
                moving = false;
            }
            if (keyState.IsKeyDown(downInput) && previousKeyState.IsKeyUp(jumpInput) && !inAnimation)
            {
                speed.Y = 10;
                speed.Y += 2;
                if (isOnGround)
                {
                    speed.X *= 0.2f;
                }
            }

            ///Strids-inputs
            if (attackFrameTimer <= -75)
            {
                ///Jab attack
                if (keyState.IsKeyDown(jabInput) && previousKeyState.IsKeyUp(jabInput) && attackFrameTimer < 0)
                {
                    CharacterManager.Instance.JabAttack(this);
                }
                ///Low attack
                if (keyState.IsKeyDown(lowInput) && previousKeyState.IsKeyUp(lowInput) && attackFrameTimer < 0)
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
                if (keyState.IsKeyDown(dashInput) && previousKeyState.IsKeyUp(dashInput) && attackFrameTimer < 0)
                {
                    CharacterManager.Instance.DashAttack(this);
                }
            }
            if (attackFrameTimer <= -150)
            {
                ///Dodge
                if (keyState.IsKeyDown(dodgeInput) && previousKeyState.IsKeyUp(dodgeInput) && attackFrameTimer < 0)
                {
                    isInvincible = true;
                    attackFrameTimer = frameInterval;
                }
            }
            #endregion
        }
        #endregion
    }
}