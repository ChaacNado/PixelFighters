﻿using Microsoft.Xna.Framework;
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

        int screenWitdh = 1360;
        int screenHeight = 900;

        //1920x1080 skärmupplösning
        //int screenWitdh = 1920;
        //int screenHeight = 1080;

        private static OptionsMenu instance;

        public KeyboardState keyState, previousKeyState;

        public MarkerState currentMarkerState;

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
            AssetManager.Instance.LoadContent(content);

            soundButtonRectangle = new Rectangle((screenWitdh / 2 - 408 / 2), (screenHeight / 2) - 156, 408, 96);
            graphicsButtonRectangle = new Rectangle((screenWitdh / 2 - 408 / 2), soundButtonRectangle.Y + 100, 408, 96);
            controlsButtonRectangle = new Rectangle((screenWitdh / 2 - 408 / 2), graphicsButtonRectangle.Y + 100, 408, 96);

            soundButtonSrcRectangle = new Rectangle(109, 2, 102, 24);
            graphicsButtonSrcRectangle = new Rectangle(109, 33, 102, 24);
            controlsButtonSrcRectangle = new Rectangle(109, 64, 102, 24);

            markedSoundButtonSrcRectangle = new Rectangle(2, 2, 102, 24);
            markedGraphicsButtonSrcRectangle = new Rectangle(2, 33, 102, 24);
            markedControlsButtonSrcRectangle = new Rectangle(2, 64, 102, 24);
        }

        public void Update(GameTime gameTime, Game1 game1)
        {
            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            if (keyState.IsKeyDown(Keys.Back) && previousKeyState.IsKeyUp(Keys.Back))
            {
                game1.currentGameState = GameState.MainMenu;
            }

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        game1.currentGameState = GameState.SoundMusic;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    break;
                case MarkerState.MarkerState2:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        game1.currentGameState = GameState.Graphics;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    break;
                case MarkerState.MarkerState3:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        game1.currentGameState = GameState.Controls;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up))
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