using Microsoft.Xna.Framework;
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
        ///Denna bör nog enbart modifera och nås av Player eller möjligtvis MovingObject

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
            if (!player.isAttacking)
            {
                ///Boxare
                if (player.currentCharacter == 1)
                {
                    player.characterName = "Philip Albert Jackson";
                    player.srcWidthModifier = 29;
                    player.srcHeightModifier = 61;

                    if (player.playerIndex == 2)
                    {
                        player.srcRec.Y = 78;
                    }
                    if (!player.isOnGround && !player.isAttacking)
                    {
                        player.srcRec.X = 307;
                        player.srcRec.Width = player.srcWidthModifier;
                    }
                }
                ///Rugbyspelare
                if (player.currentCharacter == 2)
                {
                    player.characterName = "Mac Enchiz";
                    player.srcWidthModifier = 30;
                    player.srcHeightModifier = 71;

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
                    }
                }
                ///Curlingspelare
                if (player.currentCharacter == 3)
                {
                    player.characterName = "Aiden Fortin";
                    player.srcWidthModifier = 49;
                    player.srcHeightModifier = 61;

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
                        player.srcRec.X = 675;
                        player.srcRec.Width = player.srcWidthModifier;
                    }
                }
                ///Baseball spelaren
                if (player.currentCharacter == 4)
                {
                    player.characterName = "iunno";
                    player.srcWidthModifier = 40;
                    player.srcHeightModifier = 62;

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
                    }
                }
            }
        }

        #region Attack Methods
        public void JabAttack(Player player)
        {
            #region Boxer
            if (player.currentCharacter == 1)
            {
                player.attackFrameTimer = 200;
                player.cooldownModifier = 150;
                player.isAttacking = true;
                player.inAnimation = true;
                player.knockBackModifierX = 15;
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
                    player.speed.X = 4;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -32;
                    player.speed.X = -4;
                }
            }
            #endregion

            #region Rugby
            else if (player.currentCharacter == 2)
            {
                player.attackFrameTimer = 400;
                player.isAttacking = true;

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
            }
            #endregion

            #region Curling
            //Behöver bättre animering.
            else if (player.currentCharacter == 3)
            {
                player.attackFrameTimer = 400;
                player.isAttacking = true;

                if (player.isOnGround)
                {
                    player.srcRec.X = 250;
                }
                else if (!player.isOnGround)
                {
                    player.srcRec.X = 925;
                    player.srcRec.Width = 49;
                }
            }
            #endregion

            #region Baseball
            //Flyger bakåt. Vet inte varför.
            else if (player.currentCharacter == 4)
            {
                player.attackFrameTimer = 400;
                player.isAttacking = true;

                if (player.isOnGround)
                {
                    player.srcRec.X = 205;
                    player.srcRec.Width = 67;
                }
                else if (!player.isOnGround)
                {
                    player.srcRec.X = 860;
                    player.srcRec.Width = 66;
                }
            }
            #endregion
        }

        public void LowAttack(Player player)
        {
            #region Boxer
            if (player.currentCharacter == 1)
            {
                player.attackFrameTimer = 160;
                player.cooldownModifier = 300;
                player.srcRec.X = 188;
                player.srcRec.Width = 47;
                player.isAttacking = true;
                player.inAnimation = true;
                player.knockBackModifierX = 5;
                player.knockBackModifierY = 6;
                player.attackHitBox.Width = 40;
                player.attackHitBox.Height = 48;
                player.rangeModifierY = -32;
                player.damageDealt = 4;

                if (player.facingRight)
                {
                    player.rangeModifierX = -12;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -20;
                }
            }
            #endregion

            #region Rugby
            //Behöver bättre animering.
            else if (player.currentCharacter == 2)
            {
                player.attackFrameTimer = 160;
                player.srcRec.X = 279;
                player.srcRec.Width = 44;
                player.isAttacking = true;
            }
            #endregion

            #region Curling
            //Behöver bättre animering.
            else if (player.currentCharacter == 3)
            {
                player.attackFrameTimer = 160;
                player.srcRec.X = 490;
                player.srcRec.Width = 92;
                player.isAttacking = true;
            }
            #endregion

            #region Baseball    
            //Behöver bättre animering.
            else if (player.currentCharacter == 4)
            {
                player.attackFrameTimer = 160;
                player.srcRec.X = 355;
                player.srcRec.Width = 69;
                player.isAttacking = true;
            }
            #endregion
        }

        public void HighAttack(Player player)
        {
            #region Boxer
            if (player.currentCharacter == 1)
            {
                player.attackFrameTimer = 400;
                player.cooldownModifier = 250;
                player.srcRec.X = 367;
                player.srcRec.Width = 28;
                player.srcRec.Height = 77;
                player.isAttacking = true;
                player.inAnimation = true;
                player.knockBackModifierX = 4;
                player.knockBackModifierY = 4;
                player.attackHitBox.Width = 24;
                player.attackHitBox.Height = 64;
                player.rangeModifierX = -12;
                player.rangeModifierY = -40;
                player.damageDealt = 3;
                player.speed.Y = -10;
            }
            #endregion

            #region Rugby
            //Behöver bättre animering.
            else if (player.currentCharacter == 2)
            {
                player.attackFrameTimer = 160;
                player.srcRec.X = 279;
                player.srcRec.Width = 44;
                player.isAttacking = true;
            }
            #endregion

            #region Curling
            //Behöver bättre animering.
            else if (player.currentCharacter == 3)
            {
                player.attackFrameTimer = 160;
                player.srcRec.X = 490;
                player.srcRec.Width = 92;
                player.isAttacking = true;
            }
            #endregion

            #region Baseball    
            //Behöver bättre animering.
            else if (player.currentCharacter == 4)
            {
                player.attackFrameTimer = 160;
                player.srcRec.X = 355;
                player.srcRec.Width = 69;
                player.isAttacking = true;
            }
            #endregion
        }

        public void AirDunk(Player player)
        {
            #region Boxer
            if (player.currentCharacter == 1)
            {
                player.attackFrameTimer = 160;
                player.cooldownModifier = 400;
                player.isDunking = true;
                player.inAnimation = true;
                player.srcRec.X = 397;
                player.srcRec.Width = 37;
                player.isAttacking = true;
                player.knockBackModifierX = 1;
                player.knockBackModifierY = -15;
                player.attackHitBox.Width = 24;
                player.attackHitBox.Height = 32;
                player.rangeModifierY = 12;
                player.damageDealt = 6;

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

            #region Rugby
            else if (player.currentCharacter == 2)
            {
                player.attackFrameTimer = 160;
                player.isDunking = true;
                if (player.attackFrameTimer >= 160)
                {
                    player.srcRec.X = 540;
                    player.srcRec.Width = 37;
                    player.isAttacking = true;
                    player.knockBackModifierX = 1;
                    player.knockBackModifierY = -15;
                    player.attackHitBox.Width = 24;
                    player.attackHitBox.Height = 32;
                    player.rangeModifierX = -12;
                    player.rangeModifierY = 12;
                    player.damageDealt = 6;
                }
            }
            #endregion

            #region Curling
            else if (player.currentCharacter == 3)
            {
                player.attackFrameTimer = 160;
                player.isDunking = true;
                if (player.attackFrameTimer >= 160)
                {
                    player.srcRec.X = 760;
                    player.srcRec.Width = 32;
                    player.isAttacking = true;
                    player.knockBackModifierX = 1;
                    player.knockBackModifierY = -15;
                    player.attackHitBox.Width = 24;
                    player.attackHitBox.Height = 32;
                    player.rangeModifierX = -12;
                    player.rangeModifierY = 12;
                    player.damageDealt = 6;
                }
            }
            #endregion

            #region Baseball
            else if (player.currentCharacter == 4)
            {
                player.attackFrameTimer = 160;
                player.isDunking = true;
                if (player.attackFrameTimer >= 160)
                {
                    player.srcRec.X = 725;
                    player.srcRec.Width = 43;
                    player.isAttacking = true;
                    player.knockBackModifierX = 1;
                    player.knockBackModifierY = -15;
                    player.attackHitBox.Width = 24;
                    player.attackHitBox.Height = 32;
                    player.rangeModifierX = -12;
                    player.rangeModifierY = 12;
                    player.damageDealt = 6;
                }
            }
            #endregion
        }

        public void DashAttack(Player player)
        {
            #region Boxer
            if (player.currentCharacter == 1)
            {
                player.attackFrameTimer = 360;
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
                player.damageDealt = 5;

                if (player.facingRight)
                {
                    player.rangeModifierX = 12;
                    player.speed.X = 25f;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -44;
                    player.speed.X = -25f;
                }
            }
            #endregion

            #region Rugby
            else if (player.currentCharacter == 2)
            {
                player.attackFrameTimer = 360;
                if (player.attackFrameTimer >= 340)
                {
                    player.isAttacking = true;
                }
                player.srcRec.X = 577;
                player.srcRec.Width = 67;
                player.inAnimation = true;

                player.knockBackModifierX = 10;
                player.knockBackModifierY = 3;
                player.attackHitBox.Width = 32;
                player.attackHitBox.Height = 32;
                player.rangeModifierY = -15;
                player.damageDealt = 5;

                if (player.facingRight)
                {
                    player.rangeModifierX = 0;
                    player.speed.X = 25f;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -32;
                    player.speed.X = -25f;
                }
            }
            #endregion

            #region Curling
            else if (player.currentCharacter == 3)
            {
                player.attackFrameTimer = 360;
                if (player.attackFrameTimer >= 340)
                {
                    player.isAttacking = true;
                }
                player.srcRec.X = 835;
                player.srcRec.Width = 90;
                player.inAnimation = true;

                player.knockBackModifierX = 10;
                player.knockBackModifierY = 3;
                player.attackHitBox.Width = 32;
                player.attackHitBox.Height = 32;
                player.rangeModifierY = -15;
                player.damageDealt = 5;

                if (player.facingRight)
                {
                    player.rangeModifierX = 0;
                    player.speed.X = 25f;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -32;
                    player.speed.X = -25f;
                }
            }
            #endregion

            #region Baseball
            else if (player.currentCharacter == 4)
            {
                player.attackFrameTimer = 360;
                if (player.attackFrameTimer >= 340)
                {
                    player.isAttacking = true;
                }
                player.srcRec.X = 770;
                player.srcRec.Width = 87;
                player.inAnimation = true;

                player.knockBackModifierX = 10;
                player.knockBackModifierY = 3;
                player.attackHitBox.Width = 32;
                player.attackHitBox.Height = 32;
                player.rangeModifierY = -15;
                player.damageDealt = 5;

                if (player.facingRight)
                {
                    player.rangeModifierX = 0;
                    player.speed.X = 25f;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -32;
                    player.speed.X = -25f;
                }
            }
            #endregion
        }
        #endregion
    }
}
