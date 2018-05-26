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
    class ResultScreenMenu : BaseMenu
    {
        #region Variables
        Rectangle winnerCharacterRectangle, loserCharacterRectangle, p1Rectangle, p2Rectangle, winnerRectangle, loserRectangle, buttonsRectangle;
        Rectangle p1BoxerSrcRectangle, p1AmericanFootballerSrcRectangle, p1CurlerSrcRectangle, p1BaseballerSrcRectangle, p2BoxerSrcRectangle, p2AmericanFootballerSrcRectangle, p2CurlerSrcRectangle, p2BaseballerSrcRectangle
            , p1SrcRectangle, p2SrcRectangle, winnerSrcRectangle, loserSrcRectangle, markedPlayAgainSrcRectangle, markedQuitSrcRectangle;
        Rectangle player1ChosenCharacterSrc, player2ChosenCharacterSrc;

        public bool playAgain = true;
        #endregion

        public ResultScreenMenu()
        {
            winnerCharacterRectangle = new Rectangle(0, 0, 300, 300);
            loserCharacterRectangle = new Rectangle(0, 0, 200, 200);
            p1Rectangle = new Rectangle(0, 0, 90, 42);
            p2Rectangle = new Rectangle(0, 0, 90, 42);
            winnerRectangle = new Rectangle(0, 0, 300, 60);
            loserRectangle = new Rectangle(0, 0, 200, 40);
            buttonsRectangle = new Rectangle(0, 0, 384, 168);

            p1BoxerSrcRectangle = new Rectangle(0, 0, 50, 50);
            p1AmericanFootballerSrcRectangle = new Rectangle(53, 0, 50, 50);
            p1CurlerSrcRectangle = new Rectangle(106, 0, 50, 50);
            p1BaseballerSrcRectangle = new Rectangle(159, 0, 50, 50);
            p2BoxerSrcRectangle = new Rectangle(0, 51, 50, 50);
            p2AmericanFootballerSrcRectangle = new Rectangle(53, 51, 50, 50);
            p2CurlerSrcRectangle = new Rectangle(106, 51, 50, 50);
            p2BaseballerSrcRectangle = new Rectangle(159, 51, 50, 50);

            p1SrcRectangle = new Rectangle(0, 105, 15, 7);
            p2SrcRectangle = new Rectangle(16, 105, 15, 7);

            winnerSrcRectangle = new Rectangle(0, 122, 50, 10);
            loserSrcRectangle = new Rectangle(0, 138, 50, 10);

            markedPlayAgainSrcRectangle = new Rectangle(0, 154, 96, 42);
            markedQuitSrcRectangle = new Rectangle(107, 154, 96, 42);
        }

        #region Main Methods

        public override void Update(GameTime gameTime, Game1 game1)
        {
            winnerCharacterRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - winnerCharacterRectangle.Width;
            winnerCharacterRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 250;
            loserCharacterRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 + 50;
            loserCharacterRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 250;

            winnerRectangle.X = winnerCharacterRectangle.X;
            winnerRectangle.Y = winnerCharacterRectangle.Y - winnerRectangle.Height - 10;
            loserRectangle.X = loserCharacterRectangle.X;
            loserRectangle.Y = loserCharacterRectangle.Y - loserRectangle.Height - 10;
            buttonsRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - buttonsRectangle.Width / 2;
            buttonsRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 + 170;

            //Om spelare 1 vunnit.
            if (GameplayManager.Instance.playerOneWon == true)
            {
                p1Rectangle.X = winnerCharacterRectangle.X + 5;
                p1Rectangle.Y = winnerCharacterRectangle.Y + winnerCharacterRectangle.Height - p1Rectangle.Height - 5;
                p2Rectangle.X = loserCharacterRectangle.X + 5;
                p2Rectangle.Y = loserCharacterRectangle.Y + loserCharacterRectangle.Height - p2Rectangle.Height - 5;
            }
            //Om spelare 2 vunnit.
            else if (GameplayManager.Instance.playerTwoWon == true)
            {
                p2Rectangle.X = winnerCharacterRectangle.X + 5;
                p2Rectangle.Y = winnerCharacterRectangle.Y + winnerCharacterRectangle.Height - p1Rectangle.Height - 5;
                p1Rectangle.X = loserCharacterRectangle.X + 5;
                p1Rectangle.Y = loserCharacterRectangle.Y + loserCharacterRectangle.Height - p2Rectangle.Height - 5;
            }

            //Väljer så att resultat skärmen visar samma karaktär som spelare 1 valde.
            if (game1.currentCharacterOne == 1)
            {
                player1ChosenCharacterSrc = p1BoxerSrcRectangle;
            }
            if (game1.currentCharacterOne == 2)
            {
                player1ChosenCharacterSrc = p1AmericanFootballerSrcRectangle;
            }
            if (game1.currentCharacterOne == 3)
            {
                player1ChosenCharacterSrc = p1CurlerSrcRectangle;
            }
            if (game1.currentCharacterOne == 4)
            {
                player1ChosenCharacterSrc = p1BaseballerSrcRectangle;
            }
            //Väljer så att resultat skärmen visar samma karaktär som spelare 2 valde.
            if (game1.currentCharacterTwo == 1)
            {
                player2ChosenCharacterSrc = p2BoxerSrcRectangle;
            }
            if (game1.currentCharacterTwo == 2)
            {
                player2ChosenCharacterSrc = p2AmericanFootballerSrcRectangle;
            }
            if (game1.currentCharacterTwo == 3)
            {
                player2ChosenCharacterSrc = p2CurlerSrcRectangle;
            }
            if (game1.currentCharacterTwo == 4)
            {
                player2ChosenCharacterSrc = p2BaseballerSrcRectangle;
            }

            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            previousGamePadStateOne = game1.previousGamePadStateOne;
            gamePadStateOne = game1.gamePadStateOne;
            previousGamePadStateTwo = game1.previousGamePadStateTwo;
            gamePadStateTwo = game1.gamePadStateTwo;

            //Menu knapparna för om man vill spela igen eller avsluta.
            if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
            {
                playAgain = false;
            }
            if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
            {
                playAgain = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(AssetManager.Instance.resultsMenuSpriteSheet, winnerRectangle, winnerSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.resultsMenuSpriteSheet, loserRectangle, loserSrcRectangle, Color.White);

            if (GameplayManager.Instance.playerOneWon == true)
            {
                spriteBatch.Draw(AssetManager.Instance.resultsMenuSpriteSheet, winnerCharacterRectangle, player1ChosenCharacterSrc, Color.White);
                spriteBatch.Draw(AssetManager.Instance.resultsMenuSpriteSheet, loserCharacterRectangle, player2ChosenCharacterSrc, Color.White);
            }

            if (GameplayManager.Instance.playerTwoWon == true)
            {
                spriteBatch.Draw(AssetManager.Instance.resultsMenuSpriteSheet, winnerCharacterRectangle, player2ChosenCharacterSrc, Color.White);
                spriteBatch.Draw(AssetManager.Instance.resultsMenuSpriteSheet, loserCharacterRectangle, player1ChosenCharacterSrc, Color.White);
            }

            spriteBatch.Draw(AssetManager.Instance.resultsMenuSpriteSheet, p1Rectangle, p1SrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.resultsMenuSpriteSheet, p2Rectangle, p2SrcRectangle, Color.White);


            if (playAgain == true)
            {
                spriteBatch.Draw(AssetManager.Instance.resultsMenuSpriteSheet, buttonsRectangle, markedPlayAgainSrcRectangle, Color.White);
            }
            if (playAgain == false)
            {
                spriteBatch.Draw(AssetManager.Instance.resultsMenuSpriteSheet, buttonsRectangle, markedQuitSrcRectangle, Color.White);
            }
        }
        #endregion
    }
}
