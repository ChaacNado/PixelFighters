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
    class GraphicsMenu
    {
        #region Variables

        int screenWitdh = 1360;
        int screenHeight = 900;

        //1920x1080 skärmupplösning
        //int screenWitdh = 1920;
        //int screenHeight = 1080;

        private static GraphicsMenu instance;

        public KeyboardState keyState, previousKeyState;

        public MarkerState currentMarkerState;

        Rectangle windowedButtonRectangle, resolutionButtonRectangle, applyButtonRectangle;
        Rectangle windowedButtonSrcRectangle, windowedYesButtonSrcRectangle, windowedNoButtonSrcRectangle, resolutionButtonSrcRectangle, applyButtonSrcRectangle;
        Rectangle markedResolutionButtonSrcRectangle, markedApplyButtonSrcRectangle, leftMarkedButtonSrcRectangle, rightMarkedButtonSrcRectangle;

        #endregion

        #region Properties

        public static GraphicsMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GraphicsMenu();
                }
                return instance;
            }
        }

        #endregion

        #region Main Methods

        public void LoadContent(ContentManager content)
        {
            AssetManager.Instance.LoadContent(content);

            windowedButtonRectangle = new Rectangle((screenWitdh / 2 - 756 / 2), (screenHeight / 2) - 136, 756, 72);
            resolutionButtonRectangle = new Rectangle((screenWitdh / 2 - 756 / 2), windowedButtonRectangle.Y + 100, 756, 72);
            applyButtonRectangle = new Rectangle((screenWitdh / 2 - 408 / 2), screenHeight - 200, 384, 72);

            windowedButtonSrcRectangle = new Rectangle(0, 199, 202, 18);
            windowedYesButtonSrcRectangle = new Rectangle(0, 0, 202, 18);
            windowedNoButtonSrcRectangle = new Rectangle(0, 23, 202, 18);
            resolutionButtonSrcRectangle = new Rectangle(0, 51, 202, 18);
            applyButtonSrcRectangle = new Rectangle(96, 170, 96, 18);
            markedResolutionButtonSrcRectangle = new Rectangle(0, 73, 202, 18);
            markedApplyButtonSrcRectangle = new Rectangle(96, 146, 96, 18);
            leftMarkedButtonSrcRectangle = new Rectangle(0, 95, 202, 18);
            rightMarkedButtonSrcRectangle = new Rectangle(0, 117, 202, 18);
        }

        public void Update(GameTime gameTime, Game1 game1)
        {
            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            if (keyState.IsKeyDown(Keys.Back) && previousKeyState.IsKeyUp(Keys.Back))
            {
                game1.currentGameState = GameState.Options;
            }

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    break;
                case MarkerState.MarkerState2:
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    break;
                case MarkerState.MarkerState3:
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down))
                    {
                        currentMarkerState = MarkerState.MarkerState6;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left))
                    {
                        currentMarkerState = MarkerState.MarkerState4;
                    }
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right))
                    {
                        currentMarkerState = MarkerState.MarkerState5;
                    }
                    break;
                case MarkerState.MarkerState4:
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down))
                    {
                        currentMarkerState = MarkerState.MarkerState6;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    //if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    //{
                    //    //Här ska skärmupplösningens siffror ändras på skärmen (bläddras)
                    //}
                    break;
                case MarkerState.MarkerState5:
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down))
                    {
                        currentMarkerState = MarkerState.MarkerState6;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    //if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    //{
                    //    //Här ska skärmupplösningens siffror ändras på skärmen (bläddras)
                    //}
                    break;
                case MarkerState.MarkerState6:

                    //if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    //{
                    //    //Här ska alla inställningar bli applicerade
                    //}
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, windowedButtonRectangle, windowedButtonSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionButtonRectangle, resolutionButtonSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, applyButtonRectangle, applyButtonSrcRectangle, Color.White);

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, windowedButtonRectangle, windowedYesButtonSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState2:
                    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, windowedButtonRectangle, windowedNoButtonSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState3:
                    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionButtonRectangle, markedResolutionButtonSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState4:
                    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionButtonRectangle, leftMarkedButtonSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState5:
                    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, resolutionButtonRectangle, rightMarkedButtonSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState6:
                    spriteBatch.Draw(AssetManager.Instance.graphicsMenuSpritesheet, applyButtonRectangle, markedApplyButtonSrcRectangle, Color.White);
                    break;
            }
        }

        #endregion
    }
}
