﻿using Microsoft.Xna.Framework;
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
            ///Boxare
            if (player.currentCharacter == 1)
            {
                player.characterName = "Philip Albert Jackson";
                player.srcWidthModifier = 29;
                player.srcHeightModifier = 61;
            }
            ///Rugbyspelare
            if (player.currentCharacter == 2)
            {
                player.characterName = "Mac Enchiz";
                player.srcWidthModifier = 30;
                player.srcHeightModifier = 63;

                if (player.playerIndex == 1)
                {
                    player.srcRec.Y = 156;
                }
                else
                {
                    player.srcRec.Y = 227;
                }
            }
            ///Curlingspelare
            if (player.currentCharacter == 3)
            {
                player.characterName = "Aiden Fortin";
            }
        }

        #region Attack Methods
        public void JabAttack(Player player)
        {
            if (player.currentCharacter == 1)
            {
                player.attackFrameTimer = player.frameInterval * 0.5f;
                player.isAttacking = true;
                player.knockBackModifierX = 15;
                player.knockBackModifierY = 2;
                player.attackHitBox.Width = 64;
                player.attackHitBox.Height = 24;
                player.rangeModifierY = -20;
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
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -64;
                }
            }
        }

        public void LowAttack(Player player)
        {
            if (player.currentCharacter == 1)
            {
                player.attackFrameTimer = player.frameInterval * 0.4f;
                player.srcRec.X = 188;
                player.srcRec.Width = 47;
                player.isAttacking = true;
                player.knockBackModifierX = 5;
                player.knockBackModifierY = 7;
                player.attackHitBox.Width = 52;
                player.attackHitBox.Height = 16;
                player.rangeModifierY = 8;
                player.damageDealt = 4;

                if (player.facingRight)
                {
                    player.rangeModifierX = 0;
                }
                else if (!player.facingRight)
                {
                    player.rangeModifierX = -52;
                }
            }
        }

        public void AirDunk(Player player)
        {
            if (player.currentCharacter == 1)
            {
                player.attackFrameTimer = player.frameInterval * 0.4f;
                player.isDunking = true;
                if (player.attackFrameTimer >= 160)
                {
                    player.srcRec.X = 397;
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
        }

        public void DashAttack(Player player)
        {
            if (player.currentCharacter == 1)
            {
                player.attackFrameTimer = player.frameInterval * 0.9f;
                if (player.attackFrameTimer >= 340)
                {
                    player.isAttacking = true;
                }
                player.srcRec.X = 436;
                player.srcRec.Width = 80;
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
        }
        #endregion
    }
}
