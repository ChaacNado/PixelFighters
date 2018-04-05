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
    public class PlayerOne : MovingObject
    {
        #region Variables
        KeyboardState keyState, previousKeyState;
        GamePadState gamePadState, previousGamePadState;
        public Vector2 speed;
        float rotation = 0;
        SpriteEffects playerFx = SpriteEffects.None;
        public int bX, bY;
        #endregion
        
        #region Player Object
        public PlayerOne(Texture2D tex, Vector2 pos, Rectangle srcRec) : base(tex, pos, srcRec)
        {
            this.srcRec = srcRec;
            speed = new Vector2(0, 0);
            bY = (int)ScreenManager.Instance.Dimensions.Y;
            bX = (int)ScreenManager.Instance.Dimensions.X;
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, srcRec.Width, srcRec.Height);
            groundHitBox = new Rectangle((int)pos.X + 32, (int)pos.Y + 32, srcRec.Width, 1);
            color = Color.Red;
        }
        #endregion
        
        #region Main Methods
        public override void Update(GameTime gameTime)
        {
            previousKeyState = keyState;
            keyState = Keyboard.GetState();
            
            if (!isOnGround)
            {
                speed.Y += 0.2f;
            }

            speed.X = 0;

            #region v0.1.4 GamePad kod
            //v0.1.4 - Hämtar de inbyggda gamepad funktionerna.
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            //v0.1.4 - GamePad inputs.
            if (capabilities.IsConnected)
            {
                // Get the current state of Controller1
                previousGamePadState = gamePadState;
                gamePadState = GamePad.GetState(PlayerIndex.One);

                // You can check explicitly if a gamepad has support for a certain feature
                if (capabilities.HasDPadRightButton && capabilities.HasDPadLeftButton)
                {
                    if (gamePadState.DPad.Right == ButtonState.Pressed)
                    {
                        speed.X = 5f;
                    }
                    if (gamePadState.DPad.Left == ButtonState.Pressed)
                    {
                        speed.X = -5f;
                    }
                }
                    
                // You can also check the controllers "type"
                if (capabilities.GamePadType == GamePadType.GamePad)
                {
                    if (gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A) && isOnGround)
                    {
                        speed.Y = -6;
                        isOnGround = false;
                    }
                }
            }
            #endregion

            //v0.1.3 - Liten justering som fixat att man kan röra sig åt höger
            //bX är det minsta X-värdet på skärmen, dvs 0. Därför gick det inte ha bX här, för det stoppade rörelsen åt höger ;)
            if (keyState.IsKeyDown(Keys.Right) && pos.X < 1360)
            {
                speed.X = 5f;
            }
            else if (keyState.IsKeyDown(Keys.Left) && pos.X > bX)
            {
                speed.X = -5f;
            }

            if (keyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space) && isOnGround)
            {
                speed.Y = -6;
                isOnGround = false;
            }

            System.Diagnostics.Debug.WriteLine(isOnGround);

            //v0.1.3 - Fixat så att karaktären dyker upp på startposition igen efter att den fallit ut
            if (pos.Y >= 900)
            {
                pos.X = 140;
                pos.Y = 300;
            }

            pos += speed;
            hitBox.X = (int)pos.X - 25;
            hitBox.Y = (int)pos.Y - 25;
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, srcRec, color, rotation, new Vector2(hitBox.Width / 2, hitBox.Height / 2), 1, playerFx, 1);
        }
        #endregion
        
        #region Collision Methods
        //v.0.1.3 skapat kollisionsmetoder
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
              // speed.Y = 0;
            }
            pos.X -= speed.X * 1.05f;

            base.HandleBottomCollision(p);
        }
        #endregion
    }
}