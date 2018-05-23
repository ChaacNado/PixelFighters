﻿using Microsoft.Xna.Framework;
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
        Random rnd;
        public string characterName;
        public int bX, bY, jumpsAvailable, stocksRemaining, currentCharacter, srcWidthModifier, srcHeightModifier, cooldownModifier, speedXModifier, speedYModifier;
        private int highAttackAvailable, frame;
        private float rotation = 0;
        public double frameTimer, actionFrameTimer, frameInterval;
        public bool facingRight, inAnimation, moving, isRespawning, isCrouching;
        public Keys jabInput, lowInput, highInput, dashInput, dodgeInput, jumpInput, leftInput, downInput, rightInput;
        private PlayerIndex controllerIndex;

        #endregion

        #region Player Object
        public Player(Texture2D tex, Texture2D attackTex, Vector2 pos, Rectangle srcRec, int playerIndex, Game1 game, bool facingRight) : base(tex, pos, srcRec)
        {
            this.srcRec = srcRec;
            this.attackTex = attackTex;
            this.playerIndex = playerIndex;
            this.facingRight = facingRight;
            this.game = game;
            frameInterval = 150;
            speed = new Vector2(0, 0);
            bY = (int)ScreenManager.Instance.Dimensions.Y;
            bX = (int)ScreenManager.Instance.Dimensions.X;
            damageableHitBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height);
            groundHitBox = new Rectangle((int)pos.X + 32, (int)pos.Y + 32, srcRec.Width, 1);
            attackHitBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height);
            rnd = new Random();
            jumpsAvailable = 2;
            highAttackAvailable = 1;
            stocksRemaining = 3;
            maxHP = 50;
            currentHP = maxHP;


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
                actionFrameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

                isDunking = false;

                isCrouching = false;

                ///Animering
                if (frameTimer <= 0 && moving && isOnGround && !isAttacking)
                {
                    frameTimer = frameInterval;
                    frame++;

                    srcRec.X = (frame % 4) * srcWidthModifier;
                    srcRec.Width = srcWidthModifier;
                    srcRec.Height = srcHeightModifier;
                }
                //else if (frameTimer <= 0 && isAttacking)
                //{
                //    frameTimer = frameInterval;
                //    attackFrame++;
                //    srcRec.X = (attackFrame % 2) * 310;

                //}
                else if (!moving && !inAnimation && !isAttacking && isOnGround)
                {
                    if (currentCharacter != 3)
                    {
                        srcRec.X = 0;
                    }
                    else
                    {
                        srcRec.X = 200;
                    }

                    srcRec.Width = srcWidthModifier;
                    srcRec.Height = srcHeightModifier;
                }

                ///Stoppar alla spelarens states när denne inte väntar på cooldown
                if (actionFrameTimer <= -cooldownModifier)
                {
                    inAnimation = false;
                    isAttacking = false;
                    isInvincible = false;
                    isRespawning = false;
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
                    highAttackAvailable = 1;

                    if (moving)
                    {
                        moving = false;
                    }
                }
                if (!isOnGround)
                {
                    ///Fallfysik
                    if (!isRespawning || isHit)
                    {
                        speed.Y += 0.2f;
                    }                    
                    if (speed.Y >= 20)
                    {
                        speed.Y = 20;
                    }

                    if (jumpsAvailable == 2)
                    {
                        jumpsAvailable = 1;
                    }
                }

                ///Friktion
                speed.X *= 0.9f;

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
                        facingRight = true;
                    }
                    if (playerIndex == 2)
                    {
                        pos = GameplayManager.Instance.startPosTwo;
                        facingRight = false;
                    }
                    if (actionFrameTimer < -cooldownModifier && !inAnimation && highAttackAvailable >= 1 && !isOnGround)
                    {
                        cooldownModifier = 700;
                        actionFrameTimer = cooldownModifier;
                        isInvincible = true;
                        isRespawning = true;
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
            if (actionFrameTimer > 0 && isAttacking)
            {
                spriteBatch.Draw(attackTex, attackHitBox, attackHitBox, Color.Red * 0.7f);
            }

            ///Ritar ut kollisions-hitboxes
            spriteBatch.Draw(attackTex, damageableHitBox, damageableHitBox, Color.Yellow * 0.7f);
            spriteBatch.Draw(attackTex, groundHitBox, groundHitBox, Color.Blue * 0.7f);

            if (playerIndex == 1)
            {
                if (!isInvincible)
                {
                    color = Color.White;
                }
                if (isInvincible)
                {
                    color = Color.Pink * 0.7f;
                }
                if (isRespawning || highAttackAvailable <= 0 && !inAnimation)
                {
                    color = new Color((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble()) * 0.7f;
                }
            }
            if (playerIndex == 2)
            {
                if (!isInvincible)
                {
                    color = Color.White;
                }
                if (isInvincible)
                {
                    color = Color.Cyan * 0.7f;
                }
                if (isRespawning || highAttackAvailable <= 0 && !inAnimation)
                {
                    color = new Color((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble()) * 0.7f;
                }
            }
            spriteBatch.Draw(tex, pos, srcRec, color, rotation, new Vector2(srcRec.Width / 2, srcRec.Height / 2), 1, playerFx, 1);
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
                highInput = Keys.I;
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
                highInput = Keys.NumPad5;
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
                    ///Gå höger
                    if (gamePadState.DPad.Right == ButtonState.Pressed && !isInvincible && !inAnimation)
                    {
                        facingRight = true;
                        speed.X = speedXModifier;
                        if (isOnGround)
                        {
                            moving = true;
                        }
                    }
                    ///Gå vänster
                    if (gamePadState.DPad.Left == ButtonState.Pressed && !isInvincible && !inAnimation)
                    {
                        facingRight = false;
                        speed.X = -speedXModifier;
                        if (isOnGround)
                        {
                            moving = true;
                        }
                    }
                }
                if (capabilities.GamePadType == GamePadType.GamePad)
                {
                    if (jumpsAvailable >= 1 && !isInvincible && highAttackAvailable >= 1)
                    {
                        ///Hoppa
                        if (gamePadState.IsButtonDown(Buttons.DPadUp) && previousGamePadState.IsButtonUp(Buttons.DPadUp) || gamePadState.IsButtonDown(Buttons.Y) && previousGamePadState.IsButtonUp(Buttons.Y))
                        {
                            speed.Y = -speedYModifier;
                            isOnGround = false;
                            jumpsAvailable -= 1;
                            moving = false;

                            if (currentCharacter == 1)
                            {
                                srcRec.X = 307;
                            }
                            else if (currentCharacter == 2)
                            {
                                srcRec.X = 488;
                            }
                        }
                    }
                    ///Fastfall eller crouch
                    if (gamePadState.IsButtonDown(Buttons.DPadDown) && previousGamePadState.IsButtonUp(Buttons.DPadUp) && previousGamePadState.IsButtonUp(Buttons.Y) && !inAnimation)
                    {
                        if (isOnGround)
                        {
                            speed.X *= 0.5f;
                            isCrouching = true;
                        }
                        else
                        {
                            speed.Y = 10;
                            speed.Y += 2;
                        }
                    }
                }
                ///Strids-inputs
                if (actionFrameTimer <= -75)
                {
                    ///Jab attack
                    if (gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A) && actionFrameTimer < -cooldownModifier && !inAnimation)
                    {
                        CharacterManager.Instance.JabAttack(this);
                    }
                    ///Low attack
                    if (gamePadState.IsButtonDown(Buttons.B) && previousGamePadState.IsButtonUp(Buttons.B) && actionFrameTimer < -cooldownModifier && !inAnimation)
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
                    ///High attack
                    if (actionFrameTimer < -cooldownModifier && highAttackAvailable >= 1 && !inAnimation)
                    {
                        if (gamePadState.IsButtonDown(Buttons.RightShoulder) && previousGamePadState.IsButtonUp(Buttons.RightShoulder) || gamePadState.IsButtonDown(Buttons.LeftShoulder) && previousGamePadState.IsButtonUp(Buttons.LeftShoulder))
                        {
                            CharacterManager.Instance.HighAttack(this);
                            highAttackAvailable--;
                        }
                    }
                    ///Dash attack
                    if (gamePadState.IsButtonDown(Buttons.X) && previousGamePadState.IsButtonUp(Buttons.X) && actionFrameTimer < -cooldownModifier && !inAnimation)
                    {
                        CharacterManager.Instance.DashAttack(this);
                    }
                }
                if (actionFrameTimer <= -750)
                {
                    ///Dodge
                    if (actionFrameTimer < -cooldownModifier && !inAnimation && highAttackAvailable >= 1)
                    {
                        if (gamePadState.IsButtonDown(Buttons.RightTrigger) && previousGamePadState.IsButtonUp(Buttons.RightTrigger) || gamePadState.IsButtonDown(Buttons.LeftTrigger) && previousGamePadState.IsButtonUp(Buttons.LeftTrigger))
                        {
                            cooldownModifier = 400;
                            actionFrameTimer = cooldownModifier;
                            isInvincible = true;
                        }
                    }
                }

            }
            #endregion

            ///Keyboard-inputs
            #region Keyboard
            ///Gå höger
            if (keyState.IsKeyDown(rightInput) && !isInvincible && !inAnimation)
            {
                facingRight = true;
                speed.X = speedXModifier;
                if (isOnGround)
                {
                    moving = true;
                }
            }
            ///Gå vänster
            else if (keyState.IsKeyDown(leftInput) && !isInvincible && !inAnimation)
            {
                facingRight = false;
                speed.X = -speedXModifier;
                if (isOnGround)
                {
                    moving = true;
                }
            }
            ///Hoppa
            if (keyState.IsKeyDown(jumpInput) && previousKeyState.IsKeyUp(jumpInput) && !isInvincible && jumpsAvailable >= 1 && highAttackAvailable >= 1)
            {
                speed.Y = -speedYModifier;
                isOnGround = false;
                jumpsAvailable -= 1;
                moving = false;
            }
            ///Fastfall eller crouch
            if (keyState.IsKeyDown(downInput) && previousKeyState.IsKeyUp(jumpInput) && !inAnimation)
            {
                if (isOnGround)
                {
                    speed.X *= 0.5f;
                    isCrouching = true;
                }
                else
                {
                    speed.Y = 10;
                    speed.Y += 2;
                }
            }

            ///Strids-inputs
            if (actionFrameTimer <= -75)
            {
                ///Jab attack
                if (keyState.IsKeyDown(jabInput) && previousKeyState.IsKeyUp(jabInput) && actionFrameTimer < -cooldownModifier && !inAnimation)
                {
                    CharacterManager.Instance.JabAttack(this);
                }
                ///Low attack
                if (keyState.IsKeyDown(lowInput) && previousKeyState.IsKeyUp(lowInput) && actionFrameTimer < -cooldownModifier && !inAnimation)
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
                ///High attack
                if (keyState.IsKeyDown(highInput) && previousKeyState.IsKeyUp(highInput) && actionFrameTimer < -cooldownModifier && highAttackAvailable >= 1 && !inAnimation)
                {
                    CharacterManager.Instance.HighAttack(this);
                    highAttackAvailable--;
                }
                ///Dash attack
                if (keyState.IsKeyDown(dashInput) && previousKeyState.IsKeyUp(dashInput) && actionFrameTimer < -cooldownModifier && !inAnimation)
                {
                    CharacterManager.Instance.DashAttack(this);
                }
            }
            if (actionFrameTimer <= -750)
            {
                ///Dodge
                if (keyState.IsKeyDown(dodgeInput) && previousKeyState.IsKeyUp(dodgeInput) && actionFrameTimer < -cooldownModifier && !inAnimation && highAttackAvailable >= 1)
                {
                    cooldownModifier = 400;
                    actionFrameTimer = cooldownModifier;
                    isInvincible = true;
                }
            }
            #endregion
        }
        #endregion
    }
}