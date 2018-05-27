using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    public class CharacterManager
    {
        ///Med denna kan vi byta ut texturer, attacklogik osv på spelarens karaktär

        #region Variables
        private static CharacterManager instance;
        #endregion

        #region Properties
        ///Skapar bara en instans av klassen
        public static CharacterManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CharacterManager();
                }
                return instance;
            }
        }
        #endregion

        ///Skriver ut namnet på vald karaktär i character select
        public void SelectedCharacter(Player player)
        {
            ///Standardvärden per karaktär
            if (!player.isAttacking && !player.inAnimation)
            {
                #region Boxer
                if (player.currentCharacter == 1)
                {
                    player.characterName = "Philip Albert Jackson";
                    player.srcWidthModifier = 29;
                    player.srcHeightModifier = 61;
                    if (player.isOnGround)
                    {
                        player.speedXModifier = 6;
                    }
                    else
                    {
                        player.speedXModifier = 4;
                    }
                    if (player.jumpsAvailable >= 2)
                    {
                        player.speedYModifier = 5;
                    }
                    else
                    {
                        player.speedYModifier = 8;
                    }

                    //if (player.isCrouching)
                    //{
                    //    player.srcRec.X = 235;
                    //    player.srcRec.Y = 22;
                    //    player.srcHeightModifier = 39;
                    //}

                    if (player.playerIndex == 2)
                    {
                        player.srcRec.Y = 78;
                    }
                    if (!player.isOnGround && !player.isAttacking)
                    {
                        player.srcRec.X = 307;
                        player.srcRec.Width = player.srcWidthModifier;
                        player.srcRec.Height = player.srcHeightModifier;
                    }
                }
                #endregion
                #region American Footballer
                if (player.currentCharacter == 2)
                {
                    player.characterName = "Mac Enchiz";
                    player.srcWidthModifier = 30;
                    player.srcHeightModifier = 71;

                    player.projectileStartX = 0;
                    player.projectileStartY = 20;

                    if (player.isOnGround)
                    {
                        player.speedXModifier = 5;
                    }
                    else
                    {
                        player.speedXModifier = 5;
                    }
                    if (player.jumpsAvailable >= 2)
                    {
                        player.speedYModifier = 5;
                    }
                    else
                    {
                        player.speedYModifier = 7;
                    }

                    if (player.playerIndex == 1)
                    {
                        player.srcRec.Y = 156;
                    }
                    else
                    {
                        player.srcRec.Y = 228;
                    }
                    if (!player.isOnGround && !player.isAttacking)
                    {
                        player.srcRec.X = 489;
                        player.srcRec.Width = player.srcWidthModifier - 1;
                        player.srcRec.Height = player.srcHeightModifier;
                    }
                }
                #endregion
                #region Curler
                if (player.currentCharacter == 3)
                {
                    player.characterName = "Aiden Fortin";
                    player.srcWidthModifier = 49;
                    player.srcHeightModifier = 61;

                    if (player.facingRight)
                    {
                        player.projectileStartX = 12;
                    }
                    if (!player.facingRight)
                    {
                        player.projectileStartX = -16;
                    }
                    player.projectileStartY = 16;

                    if (player.isOnGround)
                    {
                        player.speedXModifier = 4;
                    }
                    else
                    {
                        player.speedXModifier = 6;
                    }
                    if (player.jumpsAvailable >= 2)
                    {
                        player.speedYModifier = 6;
                    }
                    else
                    {
                        player.speedYModifier = 5;
                    }

                    if (player.playerIndex == 1)
                    {
                        player.srcRec.Y = 337;
                    }
                    else
                    {
                        player.srcRec.Y = 437;
                    }
                    if (!player.isOnGround && !player.isAttacking)
                    {
                        if (player.playerIndex == 1)
                        {
                            player.srcRec.Y = 331;
                        }
                        else
                        {
                            player.srcRec.Y = 431;
                        }
                        player.srcRec.X = 675;
                        player.srcRec.Width = player.srcWidthModifier;
                        player.srcRec.Height = player.srcHeightModifier + 6;
                    }
                }
                #endregion
                #region Baseballer
                if (player.currentCharacter == 4)
                {
                    player.characterName = "Rin Suzume";
                    player.srcWidthModifier = 40;
                    player.srcHeightModifier = 63;

                    player.projectileStartX = 0;
                    player.projectileStartY = -16;
                    if (player.isOnGround)
                    {
                        player.speedXModifier = 6;
                    }
                    else
                    {
                        player.speedXModifier = 4;
                    }
                    if (player.jumpsAvailable >= 2)
                    {
                        player.speedYModifier = 7;
                    }
                    else
                    {
                        player.speedYModifier = 5;
                    }

                    if (player.playerIndex == 1)
                    {
                        player.srcRec.Y = 533;
                    }
                    else
                    {
                        player.srcRec.Y = 633;
                    }
                    if (!player.isOnGround && !player.isAttacking)
                    {
                        player.srcRec.X = 565;
                        player.srcRec.Width = player.srcWidthModifier;
                        player.srcRec.Height = player.srcHeightModifier;
                    }
                }
                #endregion
            }
        }

        #region Attack Methods
        public void JabAttack(Player player)
        {
            #region Boxer
            if (player.currentCharacter == 1)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.smallswing2.Play();
                }
                player.actionFrameTimer = 125;
                player.cooldownModifier = 150;
                player.isAttacking = true;
                player.inAnimation = true;
                player.knockBackModifierX = 9;
                player.knockBackModifierY = 2;
                player.attackHitBox.Width = 32;
                player.attackHitBox.Height = 24;
                player.rangeModifierY = -30;
                player.damageDealt = 2;

                if (player.isOnGround)
                {
                    player.srcRec.X = 116;
                    player.srcRec.Width = 43;
                }
                else if (!player.isOnGround)
                {
                    player.srcRec.X = 516;
                    player.srcRec.Width = 41;
                }
                if (player.facingRight)
                {
                    player.rangeModifierX = 0;
                    player.speed.X = 5;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -32;
                    player.speed.X = -5;
                }
            }
            #endregion

            #region American Footballer
            else if (player.currentCharacter == 2)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.smallswing.Play();
                }
                player.actionFrameTimer = 175;
                player.cooldownModifier = 400;
                player.isAttacking = true;
                player.inAnimation = true;
                player.knockBackModifierX = 22;
                player.knockBackModifierY = 1;
                player.attackHitBox.Width = 56;
                player.attackHitBox.Height = 40;
                player.rangeModifierY = -40;
                player.damageDealt = 4;

                if (player.isOnGround)
                {
                    player.srcRec.X = 147;
                    player.srcRec.Width = 54;
                }
                else if (!player.isOnGround)
                {
                    player.srcRec.X = 644;
                    player.srcRec.Width = 54;
                }
                if (player.facingRight)
                {
                    player.rangeModifierX = -24;
                    player.speed.X = 2;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -32;
                    player.speed.X = -2;
                }
            }
            #endregion

            #region Curler
            else if (player.currentCharacter == 3)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.smallswing3.Play();
                }
                if (player.isOnGround)
                {
                    player.actionFrameTimer = 110;
                    player.srcRec.X = 250;
                    player.srcRec.Width = 59;
                    player.knockBackModifierX = 5;
                    player.knockBackModifierY = 1;
                    player.attackHitBox.Height = 24;
                    player.rangeModifierY = -16;
                }
                else if (!player.isOnGround)
                {
                    player.actionFrameTimer = 200;
                    player.srcRec.X = 925;
                    if (player.playerIndex == 1)
                    {
                        player.srcRec.Y = 308;
                    }
                    else
                    {
                        player.srcRec.Y = 408;
                    }
                    player.srcRec.Width = 49;
                    player.srcRec.Height = 92;
                    player.knockBackModifierX = 10;
                    player.knockBackModifierY = -1;
                    player.attackHitBox.Height = 64;
                    player.rangeModifierY = -32;
                }
                player.cooldownModifier = 0;
                player.attackHitBox.Width = 36;
                player.isAttacking = true;
                player.inAnimation = true;
                player.damageDealt = 2;

                if (player.facingRight)
                {
                    player.rangeModifierX = 4;
                    player.speed.X = 0;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -40;
                    player.speed.X = -0;
                }
            }
            #endregion

            #region Baseballer
            else if (player.currentCharacter == 4)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.swing.Play();
                }
                player.actionFrameTimer = 75;
                player.cooldownModifier = 475;
                player.isAttacking = true;
                player.inAnimation = true;
                player.knockBackModifierX = 21;
                player.knockBackModifierY = 7;
                player.attackHitBox.Width = 32;
                player.attackHitBox.Height = 16;
                player.rangeModifierY = -16;
                player.damageDealt = 4;

                if (player.isOnGround)
                {
                    player.srcRec.X = 205;
                    if (player.playerIndex == 1)
                    {
                        player.srcRec.Y = 532;
                    }
                    if (player.playerIndex == 2)
                    {
                        player.srcRec.Y = 632;
                    }
                    player.srcRec.Height = 64;
                    player.srcRec.Width = 67;
                }
                else if (!player.isOnGround)
                {
                    player.srcRec.X = 860;
                    player.srcRec.Width = 66;
                }
                if (player.facingRight)
                {
                    player.rangeModifierX = 4;
                    player.speed.X = 1;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -36;
                    player.speed.X = -1;
                }
            }
            #endregion
        }

        public void SpecialAttack(Player player)
        {
            #region Boxer
            if (player.currentCharacter == 1)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.smallswing.Play();
                }
                player.actionFrameTimer = 100;
                player.cooldownModifier = 300;
                player.srcRec.X = 188;
                player.srcRec.Width = 47;
                player.isAttacking = true;
                player.inAnimation = true;
                player.knockBackModifierX = 5;
                player.knockBackModifierY = 4;
                player.attackHitBox.Width = 40;
                player.attackHitBox.Height = 48;
                player.rangeModifierY = -32;
                player.damageDealt = 4;

                if (player.facingRight)
                {
                    player.rangeModifierX = -8;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -32;
                }
            }
            #endregion

            #region American Footballer
            else if (player.currentCharacter == 2)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.blaster.Play();
                }
                player.rangeModifierY = player.projectileStartY;

                player.actionFrameTimer = 770;
                player.cooldownModifier = 5;
                player.srcRec.X = 279;
                player.srcRec.Width = 44;

                player.isAttacking = true;
                player.inAnimation = true;
                player.attackIsProjectile = true;
                player.knockBackModifierX = -2;
                player.knockBackModifierY = 1;
                player.attackHitBox.Width = 11;
                player.attackHitBox.Height = 7;
                player.damageDealt = 2;

                player.projectileSrcRec.X = 710;
                player.projectileSrcRec.Y = 160;
                player.projectileSrcRec.Width = 11;
                player.projectileSrcRec.Height = 7;

                if (player.facingRight)
                {
                    player.rangeModifierX = player.projectileStartX;
                    player.projectileSpeedX = -11;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = player.projectileStartX;
                    player.projectileSpeedX = 11;
                }
            }
            #endregion

            #region Curler
            else if (player.currentCharacter == 3)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.crack.Play();
                }
                player.actionFrameTimer = 250;
                player.cooldownModifier = 450;
                player.srcRec.X = 410;
                if (player.playerIndex == 1)
                {
                    player.srcRec.Y = 340;
                }
                else
                {
                    player.srcRec.Y = 440;
                }
                player.srcRec.Width = 75;
                player.srcRec.Height = 59;
                player.isAttacking = true;
                player.inAnimation = true;
                player.knockBackModifierX = -3;
                player.knockBackModifierY = 5;
                player.attackHitBox.Width = 44;
                player.attackHitBox.Height = 48;
                player.rangeModifierY = -12;
                player.damageDealt = 5;

                if (player.facingRight)
                {
                    player.rangeModifierX = 8;
                    player.speed.X = 4;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -52;
                    player.speed.X = -4;
                }
            }
            #endregion

            #region Baseballer    
            else if (player.currentCharacter == 4)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.blip.Play();
                }
                player.rangeModifierY = player.projectileStartY;

                player.actionFrameTimer = 500;
                player.cooldownModifier = 5;
                player.srcRec.X = 355;
                player.srcRec.Width = 69;

                player.isAttacking = true;
                player.inAnimation = true;
                player.attackIsProjectile = true;
                player.knockBackModifierX = 2;
                player.knockBackModifierY = 1;
                player.attackHitBox.Width = 11;
                player.attackHitBox.Height = 7;
                player.damageDealt = 1;
                player.speedXModifier = 0;

                player.projectileSrcRec.X = 710;
                player.projectileSrcRec.Y = 160;
                player.projectileSrcRec.Width = 11;
                player.projectileSrcRec.Height = 7;

                if (player.facingRight)
                {
                    player.rangeModifierX = player.projectileStartX;
                    player.projectileSpeedX = 17;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = player.projectileStartX;
                    player.projectileSpeedX = -17;
                }
            }
            #endregion
        }

        public void RecoveryMove(Player player)
        {
            #region Boxer
            if (player.currentCharacter == 1)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.smallswing.Play();
                }
                player.actionFrameTimer = 120;
                player.cooldownModifier = 250;
                player.srcRec.X = 367;
                player.srcRec.Width = 28;
                player.srcRec.Height = 77;
                player.isAttacking = true;
                player.isChanneling = true;
                player.inAnimation = true;
                player.knockBackModifierX = 2;
                player.knockBackModifierY = 4;
                player.attackHitBox.Width = 32;
                player.attackHitBox.Height = 64;
                player.rangeModifierX = -16;
                player.rangeModifierY = -48;
                player.damageDealt = 6;
                player.speed.X = 0;
                player.speed.Y = -9;
            }
            #endregion

            #region American Footballer
            else if (player.currentCharacter == 2)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.smallswing.Play();
                }
                player.actionFrameTimer = 210;
                player.cooldownModifier = 290;
                player.srcRec.X = 518;
                if (player.playerIndex == 1)
                {
                    player.srcRec.Y = 156;
                }
                else
                {
                    player.srcRec.Y = 227;
                }
                player.srcRec.Width = 22;
                player.srcRec.Height = 71;
                player.isAttacking = true;
                player.isChanneling = true;
                player.inAnimation = true;
                player.knockBackModifierX = 5;
                player.knockBackModifierY = 2;
                player.attackHitBox.Width = 40;
                player.attackHitBox.Height = 72;
                player.rangeModifierX = -20;
                player.rangeModifierY = -40;
                player.damageDealt = 3;
                player.speed.X = 0;
                player.speed.Y = -6;
            }
            #endregion

            #region Curler
            else if (player.currentCharacter == 3)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.smallswing.Play();
                }
                player.actionFrameTimer = 100;
                player.cooldownModifier = 250;
                player.srcRec.X = 725;
                if (player.playerIndex == 1)
                {
                    player.srcRec.Y = 298;
                }
                else
                {
                    player.srcRec.Y = 398;
                }
                player.srcRec.Width = 23;
                player.srcRec.Height = 100;
                player.isAttacking = true;
                player.isChanneling = true;
                player.inAnimation = true;
                player.knockBackModifierX = 6;
                player.knockBackModifierY = 4;
                player.attackHitBox.Width = 24;
                player.attackHitBox.Height = 64;
                player.rangeModifierX = -12;
                player.rangeModifierY = -64;
                player.damageDealt = 4;
                player.speed.X = 0;
                player.speed.Y = -7;
            }
            #endregion

            #region Baseballer 
            else if (player.currentCharacter == 4)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.woosh.Play();
                }
                player.actionFrameTimer = 950;
                player.cooldownModifier = 475;
                player.srcRec.X = 685;
                if (player.playerIndex == 1)
                {
                    player.srcRec.Y = 498;
                }
                else
                {
                    player.srcRec.Y = 598;
                }
                player.srcRec.Width = 40;
                player.srcRec.Height = 91;
                player.isAttacking = true;
                player.isChanneling = true;
                player.knockBackModifierX = 0;
                player.knockBackModifierY = -2;
                player.attackHitBox.Width = 4;
                player.attackHitBox.Height = 4;
                player.rangeModifierX = -12;
                player.rangeModifierY = -40;
                player.damageDealt = 0;
                player.speed.Y = -5;
            }
            #endregion
        }

        public void AirDunk(Player player)
        {
            #region Boxer
            if (player.currentCharacter == 1)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.smallswing3.Play();
                }
                player.actionFrameTimer = 160;
                player.cooldownModifier = 250;
                player.isDunking = true;
                player.inAnimation = true;
                player.srcRec.X = 397;
                player.srcRec.Width = 37;
                player.isAttacking = true;
                player.knockBackModifierX = 1;
                player.knockBackModifierY = -12;
                player.attackHitBox.Width = 24;
                player.attackHitBox.Height = 32;
                player.rangeModifierY = 12;
                player.damageDealt = 1;
                player.speed.Y = -1;

                if (player.facingRight)
                {
                    player.rangeModifierX = -4;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -20;
                }
            }
            #endregion

            #region American Footballer
            else if (player.currentCharacter == 2)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.smallswing.Play();
                }
                player.actionFrameTimer = 175;
                player.cooldownModifier = 600;
                player.srcRec.X = 540;
                player.srcRec.Width = 37;
                player.isDunking = true;
                player.inAnimation = true;
                player.isAttacking = true;
                player.knockBackModifierX = 7;
                player.knockBackModifierY = -8;
                player.attackHitBox.Width = 32;
                player.attackHitBox.Height = 32;
                player.rangeModifierY = 12;
                player.damageDealt = 6;

                if (player.facingRight)
                {
                    player.rangeModifierX = 0;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -32;
                }
            }
            #endregion

            #region Curler
            else if (player.currentCharacter == 3)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.blaster.Play();
                }
                player.rangeModifierX = player.projectileStartX;
                player.rangeModifierY = player.projectileStartY;

                player.actionFrameTimer = 500;
                player.cooldownModifier = 5;
                player.srcRec.X = 760;
                player.srcRec.Width = 32;
                player.isDunking = true;
                player.inAnimation = true;
                player.attackIsProjectile = true;
                player.isAttacking = true;

                player.knockBackModifierX = 3;
                player.knockBackModifierY = -12;
                player.attackHitBox.Width = 10;
                player.attackHitBox.Height = 8;
                player.rangeModifierY = 12;
                player.damageDealt = 5;


                player.projectileSrcRec.X = 824;
                if (player.playerIndex == 1)
                {
                    player.projectileSrcRec.Y = 390;
                }
                else
                {
                    player.projectileSrcRec.Y = 490;
                }
                player.projectileSrcRec.Width = 10;
                player.projectileSrcRec.Height = 8;

                player.rangeModifierX = player.projectileStartX;
                player.projectileSpeedY = 6;
                player.inAnimation = false;
            }
            #endregion

            #region Baseballer
            else if (player.currentCharacter == 4)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.swing.Play();
                }
                player.actionFrameTimer = 200;
                player.cooldownModifier = 150;
                player.isDunking = true;
                //player.inAnimation = true;
                player.srcRec.X = 725;
                if (player.playerIndex == 1)
                {
                    player.srcRec.Y = 525;
                }
                else
                {
                    player.srcRec.Y = 625;
                }
                player.srcRec.Width = 43;
                player.srcRec.Height = 69;
                player.isAttacking = true;
                player.knockBackModifierX = 2;
                player.knockBackModifierY = -8;
                player.attackHitBox.Width = 24;
                player.attackHitBox.Height = 20;
                player.rangeModifierY = 12;
                player.damageDealt = 2;

                if (player.facingRight)
                {
                    player.rangeModifierX = -4;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -20;
                }

            }
            #endregion
        }

        public void DashAttack(Player player)
        {
            #region Boxer
            if (player.currentCharacter == 1)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.smallswing2.Play();
                }
                player.actionFrameTimer = 320;
                player.cooldownModifier = 400;
                player.isAttacking = true;
                player.srcRec.X = 436;
                player.srcRec.Width = 80;
                player.inAnimation = true;
                player.knockBackModifierX = 5;
                player.knockBackModifierY = 2;
                player.attackHitBox.Width = 32;
                player.attackHitBox.Height = 24;
                player.rangeModifierY = -15;
                player.damageDealt = 3;

                if (player.facingRight)
                {
                    player.rangeModifierX = 12;
                    player.speed.X = 17f;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -44;
                    player.speed.X = -17f;
                }
            }
            #endregion

            #region American Footballer
            else if (player.currentCharacter == 2)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.woosh.Play();
                }
                player.actionFrameTimer = 175;
                player.cooldownModifier = 375;
                player.isAttacking = true;
                player.srcRec.X = 577;
                player.srcRec.Width = 67;
                player.inAnimation = true;
                player.knockBackModifierX = -12;
                player.knockBackModifierY = 1;
                player.attackHitBox.Width = 32;
                player.attackHitBox.Height = 32;
                player.rangeModifierY = -20;
                player.damageDealt = 5;

                if (player.facingRight)
                {
                    player.rangeModifierX = 4;
                    player.speed.X = 25f;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -36;
                    player.speed.X = -25f;
                }
            }
            #endregion

            #region Curler
            else if (player.currentCharacter == 3)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.woosh.Play();
                }
                if (player.isOnGround)
                {
                    player.isChanneling = true;
                    player.actionFrameTimer = 470;
                    player.cooldownModifier = 350;
                }
                else
                {
                    player.srcRec.Height = 69;
                    player.isChanneling = false;
                    player.actionFrameTimer = 300;
                    player.cooldownModifier = 100;
                }
                player.isAttacking = true;
                player.srcRec.X = 835;
                player.srcRec.Width = 90;
                player.inAnimation = true;
                player.knockBackModifierX = 0;
                player.knockBackModifierY = 2;
                player.attackHitBox.Width = 48;
                player.attackHitBox.Height = 24;
                player.rangeModifierY = -15;
                player.damageDealt = 1;

                if (player.facingRight)
                {
                    player.rangeModifierX = 0;
                    player.speed.X = 15;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -48;
                    player.speed.X = -15;
                }
            }
            #endregion

            #region Baseballer
            else if (player.currentCharacter == 4)
            {
                if (GameplayManager.Instance.soundMenu.IsSoundOn)
                {
                    SoundManager.Instance.smallswing3.Play();
                }
                player.actionFrameTimer = 150;
                player.cooldownModifier = 150;
                player.isAttacking = true;
                player.srcRec.X = 770;
                player.srcRec.Width = 87;
                player.inAnimation = true;
                player.knockBackModifierX = 5;
                player.knockBackModifierY = 2;
                player.attackHitBox.Width = 32;
                player.attackHitBox.Height = 24;
                player.rangeModifierY = 0;
                player.damageDealt = 2;

                if (player.facingRight)
                {
                    player.rangeModifierX = 12;
                    player.speed.X = 14f;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -44;
                    player.speed.X = -14f;
                }

            }
            #endregion
        }
        #endregion
    }
}
