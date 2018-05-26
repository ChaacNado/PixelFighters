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
        Game1 game1;
        GameState currentGamestate;
        KeyboardState keyState, previousKeyState;
        GamePadState gamePadState, previousGamePadState;
        GamePadCapabilities capabilities;

        Keys jabInput, lowInput, highInput, dashInput, dodgeInput, jumpInput, leftInput, downInput, rightInput;
        PlayerIndex controllerIndex;

        SpriteEffects playerFx = SpriteEffects.None;
        Texture2D attackTex;
        public Rectangle projectileSrcRec;
        Rectangle arrowSrcRec, playerArrowRec;
        Random rnd;
        public string characterName;
        public int bX, bY, jumpsAvailable, stocksRemaining, currentCharacter, srcWidthModifier, srcHeightModifier, cooldownModifier;
        public int speedXModifier, speedYModifier, projectileSpeedX, projectileSpeedY, projectileStartX, projectileStartY;
        int highAttackAvailable, frame;
        float rotation = 0;
        public double actionFrameTimer;
        double frameTimer, frameInterval;
        public bool attackIsProjectile, facingRight, inAnimation, isMoving, isRespawning, isCrouching, isChanneling;
        #endregion

        #region Player Object
        public Player(Texture2D tex, Texture2D attackTex, Vector2 pos, Rectangle srcRec, Rectangle projectileSrcRec, Rectangle arrowSrcRec, int playerIndex, Game1 game1, bool facingRight) : base(tex, pos, srcRec)
        {
            this.srcRec = srcRec;
            this.projectileSrcRec = projectileSrcRec;
            this.arrowSrcRec = arrowSrcRec;
            this.attackTex = attackTex;
            this.playerIndex = playerIndex;
            this.facingRight = facingRight;
            this.game1 = game1;
            frameInterval = 150;
            speed = new Vector2(0, 0);
            bY = (int)ScreenManager.Instance.Dimensions.Y;
            bX = (int)ScreenManager.Instance.Dimensions.X;
            damageableHitBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height);
            groundHitBox = new Rectangle((int)pos.X + 32, (int)pos.Y + 32, srcRec.Width, 1);
            attackHitBox = new Rectangle((int)pos.X, (int)pos.Y, projectileSrcRec.Width, projectileSrcRec.Height);
            playerArrowRec = new Rectangle((int)pos.X, (int)pos.Y, 22, 26);
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
            currentGamestate = game1.currentGameState;

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
                if (frameTimer <= 0 && isMoving && !isAttacking && isOnGround)
                {
                    frameTimer = frameInterval;
                    frame++;

                    srcRec.X = (frame % 4) * srcWidthModifier;
                    srcRec.Width = srcWidthModifier;
                    srcRec.Height = srcHeightModifier;
                }
                if (frameTimer <= -200 && isMoving && attackIsProjectile && isOnGround)
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
                else if (!isMoving && !inAnimation && !isAttacking && isOnGround)
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
                if (actionFrameTimer <= 0)
                {
                    rangeModifierX = projectileStartX;
                    rangeModifierY = projectileStartY;
                    isAttacking = false;
                    isChanneling = false;
                }
                if (actionFrameTimer <= -cooldownModifier)
                {
                    rangeModifierX = projectileStartX;
                    rangeModifierY = projectileStartY;
                    inAnimation = false;
                    isInvincible = false;
                    isRespawning = false;
                    attackIsProjectile = false;
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
                    if (!isChanneling)
                    {
                        jumpsAvailable = 2;
                        highAttackAvailable = 1;
                    }

                    isMoving = false;
                }
                if (!isOnGround)
                {
                    ///Fallfysik
                    if (!isChanneling)
                    {
                        if (!isRespawning || isHit)
                        {
                            speed.Y += 0.2f;
                        }
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
                if (!isChanneling)
                {
                    speed.X *= 0.9f;
                }
                else
                {
                    speed.X *= 0.99f;
                }

                HandleInputs();

                ///Vad som leder till att man förlorar en stock
                if (pos.Y >= bY + 300 || pos.Y <= -900 || pos.X <= -400 || pos.X >= bX + 400 || currentHP <= 0)
                {
                    currentHP = maxHP;
                    stocksRemaining--;
                    speed = Vector2.Zero;

                    Respawn();
                }

                UpdatePosition();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            #region Draw Hitboxes
            ///Ritar ut strids-hitbox
            if (actionFrameTimer > 0 && isAttacking)
            {
                if (attackIsProjectile)
                {
                    spriteBatch.Draw(tex, attackHitBox, projectileSrcRec, Color.White, 0, Vector2.Zero, playerFx, 1);
                }
                spriteBatch.Draw(attackTex, attackHitBox, attackHitBox, Color.Red * 0.7f);
            }

            ///Ritar ut kollisions-hitboxes
            spriteBatch.Draw(attackTex, damageableHitBox, damageableHitBox, Color.Yellow * 0.7f);
            //spriteBatch.Draw(attackTex, groundHitBox, groundHitBox, Color.Blue * 0.7f);
            #endregion

            ///Ritar ut spelar-objektet
            spriteBatch.Draw(tex, pos, srcRec, Color.White, rotation, new Vector2(srcRec.Width / 2, srcRec.Height / 2), 1, playerFx, 1);

            ///Ritar ut olika färg-overlays beroende på olika statusar
            #region Draw Color Overlays
            if (isInvincible)
            {
                color = new Color(Color.Snow, 0.1f);
            }
            if (playerIndex == 1)
            {
                if (!isInvincible)
                {
                    color = Color.White;
                }
                if (actionFrameTimer > 0 && isAttacking)
                {
                    color = Color.DeepPink * 0.9f;
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
                    color = Color.White * 0.0f;
                }
                if (actionFrameTimer > 0 && isAttacking)
                {
                    color = Color.Cyan * 0.9f;
                }
                if (isRespawning || highAttackAvailable <= 0 && !inAnimation)
                {
                    color = new Color((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble()) * 0.7f;
                }
            }
            spriteBatch.Draw(tex, pos, srcRec, color, rotation, new Vector2(srcRec.Width / 2, srcRec.Height / 2), 1, playerFx, 1);
            #endregion

            spriteBatch.Draw(tex, playerArrowRec, arrowSrcRec, Color.White);
        }
        #endregion

        ///Uppdaterar positioner på spelaren och alla tillhörande hitboxes
        #region Position Methods
        private void UpdatePosition()
        {
            pos += speed;
            damageableHitBox.X = (int)pos.X - srcWidthModifier / 2;
            damageableHitBox.Y = (int)pos.Y - srcHeightModifier / 2;

            groundHitBox.X = (int)pos.X - srcWidthModifier / 2;
            groundHitBox.Y = (int)pos.Y + srcHeightModifier / 2;

            if (attackIsProjectile)
            {
                attackHitBox.X += projectileSpeedX;
                attackHitBox.Y += projectileSpeedY;
            }
            else
            {
                attackHitBox.X = (int)pos.X + rangeModifierX;
                attackHitBox.Y = (int)pos.Y + rangeModifierY;
            }

            playerArrowRec.X = (int)pos.X - arrowSrcRec.Width * (int)1.4f;
            playerArrowRec.Y = (int)pos.Y - arrowSrcRec.Height * 5;
        }

        public void Respawn()
        {
            ///Respawn
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
        #endregion

        ///Skriver över kollisionsmetoderna i MovingObject
        #region Collision Methods
        public override void HandleTopCollision(Platform platform)
        {
            if (!isChanneling)
            {
                speed.Y = 0;
            }
            isOnGround = true;
            base.HandleTopCollision(platform);
        }

        ///Håller spelaren ovanpå platformar
        public override void HandleBottomCollision(Platform platform)
        {
            if (!isOnGround && !isChanneling)
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

        private void HandleInputs()
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
                    if (gamePadState.DPad.Right == ButtonState.Pressed && !isInvincible && !inAnimation && !isHit)
                    {
                        facingRight = true;
                        speed.X = speedXModifier;
                        if (isOnGround)
                        {
                            isMoving = true;
                        }
                    }
                    ///Gå vänster
                    if (gamePadState.DPad.Left == ButtonState.Pressed && !isInvincible && !inAnimation && !isHit)
                    {
                        facingRight = false;
                        speed.X = -speedXModifier;
                        if (isOnGround)
                        {
                            isMoving = true;
                        }
                    }
                }
                if (capabilities.GamePadType == GamePadType.GamePad)
                {
                    if (jumpsAvailable >= 1 && !isInvincible && highAttackAvailable >= 1 && !inAnimation && !isHit)
                    {
                        ///Hoppa
                        if (gamePadState.IsButtonDown(Buttons.DPadUp) && previousGamePadState.IsButtonUp(Buttons.DPadUp) || gamePadState.IsButtonDown(Buttons.Y) && previousGamePadState.IsButtonUp(Buttons.Y))
                        {
                            speed.Y = -speedYModifier;
                            isOnGround = false;
                            jumpsAvailable -= 1;
                            isMoving = false;

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
                    if (gamePadState.IsButtonDown(Buttons.DPadDown) && previousGamePadState.IsButtonUp(Buttons.DPadUp) && previousGamePadState.IsButtonUp(Buttons.Y) && !inAnimation && !isHit && !isChanneling)
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
                    if (gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A) && actionFrameTimer < -cooldownModifier && !inAnimation && highAttackAvailable >= 1 && !isHit)
                    {
                        CharacterManager.Instance.JabAttack(this);
                    }
                    ///Special attack
                    if (gamePadState.IsButtonDown(Buttons.B) && previousGamePadState.IsButtonUp(Buttons.B) && actionFrameTimer < -cooldownModifier && !inAnimation && highAttackAvailable >= 1 && !isHit)
                    {
                        if (isOnGround)
                        {
                            CharacterManager.Instance.SpecialAttack(this);
                        }
                        ///Air dunk
                        else
                        {
                            CharacterManager.Instance.AirDunk(this);
                        }
                    }
                    ///Recovery move
                    if (actionFrameTimer < -cooldownModifier && highAttackAvailable >= 1 && !inAnimation && !isHit)
                    {
                        if (gamePadState.IsButtonDown(Buttons.RightShoulder) && previousGamePadState.IsButtonUp(Buttons.RightShoulder) || gamePadState.IsButtonDown(Buttons.LeftShoulder) && previousGamePadState.IsButtonUp(Buttons.LeftShoulder))
                        {
                            CharacterManager.Instance.RecoveryMove(this);
                            highAttackAvailable--;
                        }
                    }
                    ///Dash attack
                    if (gamePadState.IsButtonDown(Buttons.X) && previousGamePadState.IsButtonUp(Buttons.X) && actionFrameTimer < -cooldownModifier && !inAnimation && highAttackAvailable >= 1 && !isHit)
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
            if (keyState.IsKeyDown(rightInput) && !isInvincible && !inAnimation && !isHit)
            {
                facingRight = true;
                speed.X = speedXModifier;
                if (isOnGround)
                {
                    isMoving = true;
                }
            }
            ///Gå vänster
            else if (keyState.IsKeyDown(leftInput) && !isInvincible && !inAnimation && !isHit)
            {
                facingRight = false;
                speed.X = -speedXModifier;
                if (isOnGround)
                {
                    isMoving = true;
                }
            }
            ///Hoppa
            if (keyState.IsKeyDown(jumpInput) && previousKeyState.IsKeyUp(jumpInput) && !isInvincible && jumpsAvailable >= 1 && highAttackAvailable >= 1 && !inAnimation && !isHit)
            {
                speed.Y = -speedYModifier;
                isOnGround = false;
                isMoving = false;
                jumpsAvailable--;
            }
            ///Fastfall eller crouch
            if (keyState.IsKeyDown(downInput) && previousKeyState.IsKeyUp(jumpInput) && !inAnimation && !isHit && !isChanneling)
            {
                if (isOnGround)
                {
                    speed.X *= 0.5f;
                    isCrouching = true;
                }
                else
                {
                    speed.Y = 12;
                }
            }

            ///Strids-inputs
            if (actionFrameTimer <= -75)
            {
                ///Jab attack
                if (keyState.IsKeyDown(jabInput) && previousKeyState.IsKeyUp(jabInput) && actionFrameTimer < -cooldownModifier && !inAnimation && highAttackAvailable >= 1 && !isHit)
                {
                    CharacterManager.Instance.JabAttack(this);
                }
                ///Special attack
                if (keyState.IsKeyDown(lowInput) && previousKeyState.IsKeyUp(lowInput) && actionFrameTimer < -cooldownModifier && !inAnimation && highAttackAvailable >= 1 && !isHit)
                {
                    if (isOnGround)
                    {
                        CharacterManager.Instance.SpecialAttack(this);
                    }
                    ///Air dunk
                    else
                    {
                        CharacterManager.Instance.AirDunk(this);
                    }
                }
                ///Recovery move
                if (keyState.IsKeyDown(highInput) && previousKeyState.IsKeyUp(highInput) && actionFrameTimer < -cooldownModifier && highAttackAvailable >= 1 && !inAnimation && !isHit)
                {
                    CharacterManager.Instance.RecoveryMove(this);
                    highAttackAvailable--;
                }
                ///Dash attack
                if (keyState.IsKeyDown(dashInput) && previousKeyState.IsKeyUp(dashInput) && actionFrameTimer < -cooldownModifier && !inAnimation && highAttackAvailable >= 1 && !isHit)
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