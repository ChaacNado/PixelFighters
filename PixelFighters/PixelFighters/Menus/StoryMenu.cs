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
    class StoryMenu : BaseMenu
    {
        #region Variables

        int characterPage;
        Rectangle characterTextRectangle, leftButtonRectangle, rightButtonRectangle, textRectangle, boxerRectangle, americanFootballerRectangle, curlerRectangle, baseballerRectangle;
        Rectangle theBoxerSrcRectangle, theAmericanFootballerSrcRectangle, theCurlerSrcRectangle, theBaseballerSrcRectangle, leftButtonSrcRectangle, rightButtonSrcRectangle, markedLeftButtonSrcRectangle, markedRightButtonSrcRectangle, textSrcRectangle;
        Rectangle boxerSrcRectangle, americanFootballerSrcRectangle, curlerSrcRectangle, baseballerSrcRectangle, hiddenBoxerSrcRectangle, hiddenAmericanFootballerSrcRectangle, hiddenCurlerSrcRectangle, hiddenBaseballerSrcRectangle;

        #endregion
        public StoryMenu()
        {
            characterPage = 1;

            characterTextRectangle = new Rectangle(0, 0, 832, 40);
            leftButtonRectangle = new Rectangle(0, 0, 32, 48);
            rightButtonRectangle = new Rectangle(0, 0, 32, 48);
            textRectangle = new Rectangle(0, 0, 836, 304);
            boxerRectangle = new Rectangle(0, 0, 200, 200);
            americanFootballerRectangle = new Rectangle(0, 0, 200, 200);
            curlerRectangle = new Rectangle(0, 0, 200, 200);
            baseballerRectangle = new Rectangle(0, 0, 200, 200);

            leftButtonSrcRectangle = new Rectangle(0, 16, 8, 12);
            rightButtonSrcRectangle = new Rectangle(15, 0, 8, 12);
            markedLeftButtonSrcRectangle = new Rectangle(0, 0, 8, 12);
            markedRightButtonSrcRectangle = new Rectangle(15, 16, 8, 12);

            theBoxerSrcRectangle = new Rectangle(0, 32, 208, 10);
            theAmericanFootballerSrcRectangle = new Rectangle(0, 48, 208, 10);
            theCurlerSrcRectangle = new Rectangle(0, 64, 208, 10);
            theBaseballerSrcRectangle = new Rectangle(0, 80, 208, 10);

            boxerSrcRectangle = new Rectangle(0, 96, 50, 50);
            americanFootballerSrcRectangle = new Rectangle(53, 96, 50, 50);
            curlerSrcRectangle = new Rectangle(106, 96, 50, 50);
            baseballerSrcRectangle = new Rectangle(159, 96, 50, 50);

            hiddenBoxerSrcRectangle = new Rectangle(0, 149, 50, 50);
            hiddenAmericanFootballerSrcRectangle = new Rectangle(53, 149, 50, 50);
            hiddenCurlerSrcRectangle = new Rectangle(106, 149, 50, 50);
            hiddenBaseballerSrcRectangle = new Rectangle(159, 149, 50, 50);

            textSrcRectangle = new Rectangle(0, 203, 209, 76);
        }
        #region Main Methods

        public override void Update(GameTime gameTime, Game1 game1)
        {
            characterTextRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 416;
            characterTextRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 370;
            leftButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 532;
            leftButtonRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2;
            rightButtonRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 + 500;
            rightButtonRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2;
            boxerRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 418;
            boxerRectangle.Y = characterTextRectangle.Y + 100;
            americanFootballerRectangle.X = boxerRectangle.X + 212;
            americanFootballerRectangle.Y = characterTextRectangle.Y + 100;
            curlerRectangle.X = americanFootballerRectangle.X + 212;
            curlerRectangle.Y = characterTextRectangle.Y + 100;
            baseballerRectangle.X = curlerRectangle.X + 212;
            baseballerRectangle.Y = characterTextRectangle.Y + 100;
            textRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 418;
            textRectangle.Y = boxerRectangle.Y + 212;

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
            //Bläddrar mellan olika karaktärer
            if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
            {
                if (characterPage == 4)
                {
                    characterPage = 1;
                }
                else if (characterPage < 4)
                {
                    characterPage++;
                }
            }
            if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
            {
                if (characterPage == 1)
                {
                    characterPage = 4;
                }
                else if (characterPage > 1)
                {
                    characterPage--;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, leftButtonRectangle, leftButtonSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, rightButtonRectangle, rightButtonSrcRectangle, Color.White);

            spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, boxerRectangle, hiddenBoxerSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, americanFootballerRectangle, hiddenAmericanFootballerSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, curlerRectangle, hiddenCurlerSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, baseballerRectangle, hiddenBaseballerSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, textRectangle, textSrcRectangle, Color.White);



            if (keyState.IsKeyDown(Keys.Left) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft))
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, leftButtonRectangle, markedLeftButtonSrcRectangle, Color.White);
            }
            if (keyState.IsKeyDown(Keys.Right) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight))
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, rightButtonRectangle, markedRightButtonSrcRectangle, Color.White);
            }
            //Boxare
            if (characterPage == 1)
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, characterTextRectangle, theBoxerSrcRectangle, Color.White);
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, boxerRectangle, boxerSrcRectangle, Color.White);

            }
            //Amerikansk fotbollsspelare
            if (characterPage == 2)
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, characterTextRectangle, theAmericanFootballerSrcRectangle, Color.White);
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, americanFootballerRectangle, americanFootballerSrcRectangle, Color.White);

            }
            //Curlare
            if (characterPage == 3)
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, characterTextRectangle, theCurlerSrcRectangle, Color.White);
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, curlerRectangle, curlerSrcRectangle, Color.White);
            }
            //Baseballspelare
            if (characterPage == 4)
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, characterTextRectangle, theBaseballerSrcRectangle, Color.White);
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, baseballerRectangle, baseballerSrcRectangle, Color.White);
            }
        }

        #endregion
    }
}
