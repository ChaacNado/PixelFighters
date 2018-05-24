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
    class SoundMenu : BaseMenu
    {
        #region Variables
        Rectangle textRectangle, soundOnOffRectangle, musicOnOffRectangle;

        Rectangle textSrcRectangle, onButtonSrcRectangle, offButtonSrcRectangle, markedButtonOnSrcRectangle, markedButtonOffSrcRectangle;

        bool soundOn = true, musicOn = true;

        #endregion

        public SoundMenu()
        {
            musicOnOffRectangle = new Rectangle(0, 0, 260, 72);
            soundOnOffRectangle = new Rectangle(0, 0, 260, 72);
            textRectangle = new Rectangle(0, 0, 200, 164);

            offButtonSrcRectangle = new Rectangle(50, 71, 65, 18);
            onButtonSrcRectangle = new Rectangle(50, 48, 65, 18);
            markedButtonOffSrcRectangle = new Rectangle(50, 23, 65, 18);
            markedButtonOnSrcRectangle = new Rectangle(50, 0, 65, 18);
            textSrcRectangle = new Rectangle(0, 0, 50, 41);
        }

        #region Main Methods
        public override void Update(GameTime gameTime, Game1 game1)
        {
            textRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - textRectangle.Width;
            textRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - textRectangle.Height / 2;
            soundOnOffRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2;
            soundOnOffRectangle.Y = textRectangle.Y;
            musicOnOffRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2;
            musicOnOffRectangle.Y = soundOnOffRectangle.Y + soundOnOffRectangle.Height + 20;


            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            previousGamePadStateOne = game1.previousGamePadStateOne;
            gamePadStateOne = game1.gamePadStateOne;
            previousGamePadStateTwo = game1.previousGamePadStateTwo;
            gamePadStateTwo = game1.gamePadStateTwo;

            if (soundOn == true)
            {
                
            }
            if (musicOn == true)
            {

            }

            if (soundOn == false)
            {

            }
            if (musicOn == false)
            {

            }

            if (keyState.IsKeyDown(Keys.Back) && previousKeyState.IsKeyUp(Keys.Back) || gamePadStateOne.IsButtonDown(Buttons.B) && previousGamePadStateOne.IsButtonUp(Buttons.B) || gamePadStateTwo.IsButtonDown(Buttons.B) && previousGamePadStateTwo.IsButtonUp(Buttons.B))
            {
                game1.currentGameState = GameState.Options;
            }

            switch (currentMarkerState)
            {
                //stänger av/på ljud
                case MarkerState.MarkerState1:
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
                    {
                        soundOn = true;
                    }
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
                    {
                        soundOn = false;
                    }
                    break;
                //stänger av/på musik
                case MarkerState.MarkerState2:
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
                    {
                        musicOn = true;
                    }
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
                    {
                        musicOn = false;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Instance.soundMusicMenuSpriteSheet, textRectangle, textSrcRectangle, Color.White);

            if (soundOn == true)
            {
                spriteBatch.Draw(AssetManager.Instance.soundMusicMenuSpriteSheet, soundOnOffRectangle, onButtonSrcRectangle, Color.White);
            }
            if (musicOn == true)
            {
                spriteBatch.Draw(AssetManager.Instance.soundMusicMenuSpriteSheet, musicOnOffRectangle, onButtonSrcRectangle, Color.White);
            }

            if (soundOn == false)
            {
                spriteBatch.Draw(AssetManager.Instance.soundMusicMenuSpriteSheet, soundOnOffRectangle, offButtonSrcRectangle, Color.White);
            }
            if (musicOn == false)
            {
                spriteBatch.Draw(AssetManager.Instance.soundMusicMenuSpriteSheet, musicOnOffRectangle, offButtonSrcRectangle, Color.White);
            }

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    if (soundOn == true)
                    {
                        spriteBatch.Draw(AssetManager.Instance.soundMusicMenuSpriteSheet, soundOnOffRectangle, markedButtonOnSrcRectangle, Color.White);
                    }
                    if (soundOn == false)
                    {
                        spriteBatch.Draw(AssetManager.Instance.soundMusicMenuSpriteSheet, soundOnOffRectangle, markedButtonOffSrcRectangle, Color.White);
                    }
                    break;
                case MarkerState.MarkerState2:
                    if (musicOn == true)
                    {
                        spriteBatch.Draw(AssetManager.Instance.soundMusicMenuSpriteSheet, musicOnOffRectangle, markedButtonOnSrcRectangle, Color.White);
                    }
                    if (musicOn == false)
                    {
                        spriteBatch.Draw(AssetManager.Instance.soundMusicMenuSpriteSheet, musicOnOffRectangle, markedButtonOffSrcRectangle, Color.White);
                    }
                    break;
            }
        }
        #endregion
    }
}

