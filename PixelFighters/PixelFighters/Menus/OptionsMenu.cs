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
    class OptionsMenu
    {
        #region Variables
        private static OptionsMenu instance;

        KeyboardState keyState, previousKeyState;
        GamePadState gamePadState, previousGamePadState;

        MarkerState currentMarkerState;

        Rectangle soundButtonRectangle, graphicsButtonRectangle, controlsButtonRectangle;
        Rectangle soundButtonSrcRectangle, graphicsButtonSrcRectangle, controlsButtonSrcRectangle;
        Rectangle markedSoundButtonSrcRectangle, markedGraphicsButtonSrcRectangle, markedControlsButtonSrcRectangle;

        #endregion

        #region Properties

        public static OptionsMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OptionsMenu();
                }
                return instance;
            }
        }

        #endregion

        #region main Methods

        public void LoadContent(ContentManager content)
        {
            soundButtonRectangle = new Rectangle(0, 0, 408, 96);
            graphicsButtonRectangle = new Rectangle(0, 0, 408, 96);
            controlsButtonRectangle = new Rectangle(0, 0, 408, 96);

            soundButtonSrcRectangle = new Rectangle(109, 2, 102, 24);
            graphicsButtonSrcRectangle = new Rectangle(109, 33, 102, 24);
            controlsButtonSrcRectangle = new Rectangle(109, 64, 102, 24);

            markedSoundButtonSrcRectangle = new Rectangle(2, 2, 102, 24);
            markedGraphicsButtonSrcRectangle = new Rectangle(2, 33, 102, 24);
            markedControlsButtonSrcRectangle = new Rectangle(2, 64, 102, 24);
        }

        public void Update(GameTime gameTime, Game1 game1)
        {
            soundButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - soundButtonRectangle.Width / 2;
            soundButtonRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 156;
            graphicsButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - graphicsButtonRectangle.Width / 2;
            graphicsButtonRectangle.Y = soundButtonRectangle.Y + 100;
            controlsButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - controlsButtonRectangle.Width / 2;
            controlsButtonRectangle.Y = graphicsButtonRectangle.Y + 100;

            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            previousGamePadState = game1.previousGamePadStateOne;
            gamePadState = game1.gamePadStateOne;

            if (keyState.IsKeyDown(Keys.Back) && previousKeyState.IsKeyUp(Keys.Back) || gamePadState.IsButtonDown(Buttons.B) && previousGamePadState.IsButtonUp(Buttons.B))
            {
                game1.currentGameState = GameState.MainMenu;
            }

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A))
                    {
                        game1.currentGameState = GameState.SoundMusic;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadState.IsButtonDown(Buttons.DPadDown) && previousGamePadState.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadState.IsButtonDown(Buttons.DPadUp) && previousGamePadState.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    break;
                case MarkerState.MarkerState2:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A))
                    {
                        game1.currentGameState = GameState.Graphics;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadState.IsButtonDown(Buttons.DPadDown) && previousGamePadState.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadState.IsButtonDown(Buttons.DPadUp) && previousGamePadState.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    break;
                case MarkerState.MarkerState3:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadState.IsButtonDown(Buttons.A) && previousGamePadState.IsButtonUp(Buttons.A))
                    {
                        game1.currentGameState = GameState.Controls;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadState.IsButtonDown(Buttons.DPadDown) && previousGamePadState.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadState.IsButtonDown(Buttons.DPadUp) && previousGamePadState.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
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
