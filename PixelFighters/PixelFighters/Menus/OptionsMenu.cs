using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelFighters
{
    class OptionsMenu : BaseMenu
    {
        #region Variables

        Rectangle soundButtonRectangle, graphicsButtonRectangle, controlsButtonRectangle;
        Rectangle soundButtonSrcRectangle, graphicsButtonSrcRectangle, controlsButtonSrcRectangle;
        Rectangle markedSoundButtonSrcRectangle, markedGraphicsButtonSrcRectangle, markedControlsButtonSrcRectangle;

        #endregion
        public OptionsMenu()
        {
            soundButtonRectangle = new Rectangle(0, -100, 408, 96);
            graphicsButtonRectangle = new Rectangle(0, -100, 408, 96);
            controlsButtonRectangle = new Rectangle(0, -100, 408, 96);

            soundButtonSrcRectangle = new Rectangle(109, 2, 102, 24);
            graphicsButtonSrcRectangle = new Rectangle(109, 33, 102, 24);
            controlsButtonSrcRectangle = new Rectangle(109, 64, 102, 24);

            markedSoundButtonSrcRectangle = new Rectangle(2, 2, 102, 24);
            markedGraphicsButtonSrcRectangle = new Rectangle(2, 33, 102, 24);
            markedControlsButtonSrcRectangle = new Rectangle(2, 64, 102, 24);
        }

        #region main Methods

        public override void Update(GameTime gameTime, Game1 game1)
        {
            soundButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - soundButtonRectangle.Width / 2;
            soundButtonRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 156;
            graphicsButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - graphicsButtonRectangle.Width / 2;
            graphicsButtonRectangle.Y = soundButtonRectangle.Y + 100;
            controlsButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - controlsButtonRectangle.Width / 2;
            controlsButtonRectangle.Y = graphicsButtonRectangle.Y + 100;

            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            previousGamePadStateOne = game1.previousGamePadStateOne;
            gamePadStateOne = game1.gamePadStateOne;
            previousGamePadStateTwo = game1.previousGamePadStateTwo;
            gamePadStateTwo = game1.gamePadStateTwo;

            if (keyState.IsKeyDown(Keys.Back) && previousKeyState.IsKeyUp(Keys.Back) || gamePadStateOne.IsButtonDown(Buttons.B) && previousGamePadStateOne.IsButtonUp(Buttons.B) || gamePadStateTwo.IsButtonDown(Buttons.B) && previousGamePadStateTwo.IsButtonUp(Buttons.B))
            {
                game1.currentGameState = GameState.MainMenu;
            }

            switch (currentMarkerState)
            {
                //Markerstate1 = Soundknappen
                case MarkerState.MarkerState1:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        game1.currentGameState = GameState.SoundMusic;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    break;
                //Markerstate2 = Graphicsknappen
                case MarkerState.MarkerState2:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        game1.currentGameState = GameState.Graphics;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    break;
                //Markerstate3 = Controlsknappen
                case MarkerState.MarkerState3:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        game1.currentGameState = GameState.Controls;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Instance.optionsMenuSpritesheet, soundButtonRectangle, soundButtonSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.optionsMenuSpritesheet, graphicsButtonRectangle, graphicsButtonSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.optionsMenuSpritesheet, controlsButtonRectangle, controlsButtonSrcRectangle, Color.White);

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    spriteBatch.Draw(AssetManager.Instance.optionsMenuSpritesheet, soundButtonRectangle, markedSoundButtonSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState2:
                    spriteBatch.Draw(AssetManager.Instance.optionsMenuSpritesheet, graphicsButtonRectangle, markedGraphicsButtonSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState3:
                    spriteBatch.Draw(AssetManager.Instance.optionsMenuSpritesheet, controlsButtonRectangle, markedControlsButtonSrcRectangle, Color.White);
                    break;
            }
        }

        #endregion
    }
}
