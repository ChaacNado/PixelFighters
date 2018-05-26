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
    class CharacterSelectMenu : BaseMenu
    {
        #region Variables

        public int lastMarkedCharacter, secondaryLastMarkedCharacter, player1ChosenCharacter, player2ChosenCharacter, mapChosen, minutesChosen;
        public bool player1Ready, player2Ready;
        Rectangle livesTextRectangle, minutesTextRectangle, mapTextRectangle, livesBoxRectangle, minutesBoxRectangle, mapBoxRectangle, charactersRectangle, ready1Rectangle, ready2Rectangle, player1Rectangle, player2Rectangle, player1MarkerRectangle, player2MarkerRectangle, player1TextRectangle, player2TextRectangle;
        Rectangle livesTextSrcRectangle, minutesTextSrcRectangle, mapTextSrcRectangle, livesBoxSrcRectangle, minutesBoxSrcRectangle, mapBoxSrcRectangle, smallBoxleftSrcRectangle, smallBoxRightSrcRectangle, bigBoxLeftSrcRectangle, bigBoxRightSrcRectangle, charactersSrcRectangle, readySrcRectangle, notReadySrcRectangle, markedReadySrcRectangle, markedNotReadySrcRectangle, playerSrcRectangle, player1MarkerSrcRectangle, player2MarkerSrcRectangle, standardMapSrcRectangle, spaceMapSrcRectangle, player1TextSrcRectangle, player2TextSrcRectangle;
        Rectangle boxer1SrcRectangle, boxer2SrcRectangle, americanFootballer1SrcRectangle, americanFootballer2SrcRectangle, curler1SrcRectangle, curler2SrcRectangle, baseballer1SrcRectangle, baseballer2SrcRectangle;

        #endregion
        public CharacterSelectMenu()
        {
            player1Ready = false;
            player2Ready = false;
            player1ChosenCharacter = 1;
            player2ChosenCharacter = 1;
            mapChosen = 1;
            minutesChosen = 5;

            livesTextRectangle = new Rectangle(0, 0, 156, 40);
            minutesTextRectangle = new Rectangle(0, 0, 236, 40);
            mapTextRectangle = new Rectangle(0, 0, 112, 40);

            livesBoxRectangle = new Rectangle(0, 0, 184, 72);
            minutesBoxRectangle = new Rectangle(0, 0, 184, 72);
            mapBoxRectangle = new Rectangle(0, 0, 404, 72);

            charactersRectangle = new Rectangle(0, 0, 836, 200);
            ready1Rectangle = new Rectangle(0, 0, 480, 90);
            ready2Rectangle = new Rectangle(0, 0, 480, 90);
            player1Rectangle = new Rectangle(0, 0, 300, 300);
            player2Rectangle = new Rectangle(0, 0, 300, 300);
            player1TextRectangle = new Rectangle(0, 0, 220, 140);
            player2TextRectangle = new Rectangle(0, 0, 240, 140);

            player1MarkerRectangle = new Rectangle(0, 0, 216, 216);
            player2MarkerRectangle = new Rectangle(0, 0, 216, 216);

            livesTextSrcRectangle = new Rectangle(53, 182, 39, 10);
            minutesTextSrcRectangle = new Rectangle(53, 198, 59, 10);
            mapTextSrcRectangle = new Rectangle(53, 214, 28, 10);

            livesBoxSrcRectangle = new Rectangle(0, 179, 46, 18);
            minutesBoxSrcRectangle = new Rectangle(0, 179, 46, 18);
            mapBoxSrcRectangle = new Rectangle(99, 303, 101, 18);

            smallBoxleftSrcRectangle = new Rectangle(0, 199, 46, 18);
            smallBoxRightSrcRectangle = new Rectangle(0, 219, 46, 18);
            bigBoxLeftSrcRectangle = new Rectangle(99, 323, 101, 18);
            bigBoxRightSrcRectangle = new Rectangle(99, 343, 101, 18);

            charactersSrcRectangle = new Rectangle(0, 0, 209, 50);
            readySrcRectangle = new Rectangle(110, 244, 96, 18);
            notReadySrcRectangle = new Rectangle(3, 244, 96, 18);
            markedReadySrcRectangle = new Rectangle(110, 273, 96, 18);
            markedNotReadySrcRectangle = new Rectangle(3, 273, 96, 18);
            playerSrcRectangle = new Rectangle(159, 104, 50, 50);
            player1TextSrcRectangle = new Rectangle(160, 160, 11, 7);
            player2TextSrcRectangle = new Rectangle(176, 160, 12, 7);

            player1MarkerSrcRectangle = new Rectangle(0, 102, 54, 54);
            player2MarkerSrcRectangle = new Rectangle(56, 102, 54, 54);

            boxer1SrcRectangle = new Rectangle(0, 0, 50, 50);
            boxer2SrcRectangle = new Rectangle(0, 51, 50, 50);
            americanFootballer1SrcRectangle = new Rectangle(53, 0, 50, 50);
            americanFootballer2SrcRectangle = new Rectangle(53, 51, 50, 50);
            curler1SrcRectangle = new Rectangle(106, 0, 50, 50);
            curler2SrcRectangle = new Rectangle(106, 51, 50, 50);
            baseballer1SrcRectangle = new Rectangle(159, 0, 50, 50);
            baseballer2SrcRectangle = new Rectangle(159, 51, 50, 50);

            standardMapSrcRectangle = new Rectangle(99, 361, 101, 18);
            spaceMapSrcRectangle = new Rectangle(99, 377, 101, 18);
        }
        #region Main Methods

        public override void Update(GameTime gameTime, Game1 game1)
        {
            livesTextRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 500;
            livesTextRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 370;
            minutesTextRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 500/*118*/;
            minutesTextRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 370;
            mapTextRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 + 356;
            mapTextRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 370;

            livesBoxRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 513;
            livesBoxRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 320;
            minutesBoxRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 474/*92*/;
            minutesBoxRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 320;
            mapBoxRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 + 215;
            mapBoxRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 320;

            charactersRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 418;
            charactersRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 190;
            ready1Rectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 330;
            ready1Rectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 + 268;
            ready2Rectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 + 340;
            ready2Rectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 + 268;
            player1TextRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 290;
            player1TextRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 + 100;
            player2TextRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 + 370;
            player2TextRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 + 100;

            player1Rectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 660;
            player1Rectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 + 60;
            player2Rectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 + 10;
            player2Rectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 + 60;

            previousKeyState = game1.previousKeyState;
            keyState = game1.keyState;

            previousGamePadStateOne = game1.previousGamePadStateOne;
            gamePadStateOne = game1.gamePadStateOne;
            previousGamePadStateTwo = game1.previousGamePadStateTwo;
            gamePadStateTwo = game1.gamePadStateTwo;

            GameplayManager.Instance.stageNumber = mapChosen;

            if (keyState.IsKeyDown(Keys.Back) && previousKeyState.IsKeyUp(Keys.Back) || gamePadStateOne.IsButtonDown(Buttons.B) && previousGamePadStateOne.IsButtonUp(Buttons.B) || gamePadStateTwo.IsButtonDown(Buttons.B) && previousGamePadStateTwo.IsButtonUp(Buttons.B))
            {
                game1.currentGameState = GameState.MainMenu;
            }

            switch (currentMarkerState)
            {
                ///Markerstate1 = Boxare
                case MarkerState.MarkerState1:
                    lastMarkedCharacter = 1;
                    player1MarkerRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 426;
                    player1MarkerRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 198;

                    if (keyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A))
                    {
                        ///P1 väljer karaktär
                        game1.currentCharacterOne = 1;
                        player1ChosenCharacter = 1;
                    }
                    if (keyState.IsKeyDown(Keys.D) && previousKeyState.IsKeyUp(Keys.D) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.S) && previousKeyState.IsKeyUp(Keys.S) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState11;
                    }
                    if (keyState.IsKeyDown(Keys.W) && previousKeyState.IsKeyUp(Keys.W) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState7;
                    }
                    break;
                ///Markerstate2 = Amerikansk fotbollsspelare
                case MarkerState.MarkerState2:
                    lastMarkedCharacter = 2;
                    player1MarkerRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 214;
                    player1MarkerRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 198;

                    if (keyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A))
                    {
                        ///P1 väljer karaktär
                        game1.currentCharacterOne = 2;
                        player1ChosenCharacter = 2;
                    }
                    if (keyState.IsKeyDown(Keys.D) && previousKeyState.IsKeyUp(Keys.D) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    if (keyState.IsKeyDown(Keys.A) && previousKeyState.IsKeyUp(Keys.A) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentMarkerState = MarkerState.MarkerState1;
                    }
                    if (keyState.IsKeyDown(Keys.S) && previousKeyState.IsKeyUp(Keys.S) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState11;
                    }
                    if (keyState.IsKeyDown(Keys.W) && previousKeyState.IsKeyUp(Keys.W) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState7;
                    }
                    break;
                ///MarkerState3 = Curlare
                case MarkerState.MarkerState3:
                    lastMarkedCharacter = 3;
                    player1MarkerRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 2;
                    player1MarkerRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 198;

                    if (keyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A))
                    {
                        ///P1 väljer karaktär
                        game1.currentCharacterOne = 3;
                        player1ChosenCharacter = 3;
                    }
                    if (keyState.IsKeyDown(Keys.D) && previousKeyState.IsKeyUp(Keys.D) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState4;
                    }
                    if (keyState.IsKeyDown(Keys.A) && previousKeyState.IsKeyUp(Keys.A) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentMarkerState = MarkerState.MarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.S) && previousKeyState.IsKeyUp(Keys.S) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState11;
                    }
                    if (keyState.IsKeyDown(Keys.W) && previousKeyState.IsKeyUp(Keys.W) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState7;
                    }
                    break;
                ///Markerstate4 = Baseballspelare
                case MarkerState.MarkerState4:
                    lastMarkedCharacter = 4;
                    player1MarkerRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 + 210;
                    player1MarkerRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 198;

                    if (keyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A))
                    {
                        ///P1 väljer karaktär
                        game1.currentCharacterOne = 4;
                        player1ChosenCharacter = 4;
                    }
                    if (keyState.IsKeyDown(Keys.A) && previousKeyState.IsKeyUp(Keys.A) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentMarkerState = MarkerState.MarkerState3;
                    }
                    if (keyState.IsKeyDown(Keys.S) && previousKeyState.IsKeyUp(Keys.S) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown))
                    {
                        currentMarkerState = MarkerState.MarkerState11;
                    }
                    if (keyState.IsKeyDown(Keys.W) && previousKeyState.IsKeyUp(Keys.W) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp))
                    {
                        currentMarkerState = MarkerState.MarkerState7;
                    }
                    break;
                ///Markerstate5 = Lives vänster knapp
                case MarkerState.MarkerState5:
                    //if (keyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A))
                    //{
                    //    Färre liv
                    //}
                    if (keyState.IsKeyDown(Keys.D) && previousKeyState.IsKeyUp(Keys.D) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState6;
                    }
                    if (keyState.IsKeyDown(Keys.S) && previousKeyState.IsKeyUp(Keys.S) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown))
                    {
                        if (lastMarkedCharacter == 1)
                        {
                            currentMarkerState = MarkerState.MarkerState1;
                        }
                        if (lastMarkedCharacter == 2)
                        {
                            currentMarkerState = MarkerState.MarkerState2;
                        }
                        if (lastMarkedCharacter == 3)
                        {
                            currentMarkerState = MarkerState.MarkerState3;
                        }
                        if (lastMarkedCharacter == 4)
                        {
                            currentMarkerState = MarkerState.MarkerState4;
                        }
                    }
                    break;
                ///Markerstate6 = Lives höger knapp
                case MarkerState.MarkerState6:
                    //if (keyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A))
                    //{
                    //    Fler liv
                    //}
                    if (keyState.IsKeyDown(Keys.D) && previousKeyState.IsKeyUp(Keys.D) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState7;
                    }
                    if (keyState.IsKeyDown(Keys.A) && previousKeyState.IsKeyUp(Keys.A) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentMarkerState = MarkerState.MarkerState5;
                    }
                    if (keyState.IsKeyDown(Keys.S) && previousKeyState.IsKeyUp(Keys.S) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown))
                    {
                        if (lastMarkedCharacter == 1)
                        {
                            currentMarkerState = MarkerState.MarkerState1;
                        }
                        if (lastMarkedCharacter == 2)
                        {
                            currentMarkerState = MarkerState.MarkerState2;
                        }
                        if (lastMarkedCharacter == 3)
                        {
                            currentMarkerState = MarkerState.MarkerState3;
                        }
                        if (lastMarkedCharacter == 4)
                        {
                            currentMarkerState = MarkerState.MarkerState4;
                        }
                    }
                    break;
                ///Markerstate7 = Minutes vänster knapp
                case MarkerState.MarkerState7:
                    if (keyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A))
                    {
                        if (game1.currentChosenMinutes > 1)
                        {
                            game1.currentChosenMinutes -= 1;
                            minutesChosen -= 1;
                        }

                    }
                    if (keyState.IsKeyDown(Keys.D) && previousKeyState.IsKeyUp(Keys.D) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState8;
                    }
                    //if (keyState.IsKeyDown(Keys.A) && previousKeyState.IsKeyUp(Keys.A) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft))
                    //{
                    //    currentMarkerState = MarkerState.MarkerState6;
                    //}
                    if (keyState.IsKeyDown(Keys.S) && previousKeyState.IsKeyUp(Keys.S) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown))
                    {
                        if (lastMarkedCharacter == 1)
                        {
                            currentMarkerState = MarkerState.MarkerState1;
                        }
                        if (lastMarkedCharacter == 2)
                        {
                            currentMarkerState = MarkerState.MarkerState2;
                        }
                        if (lastMarkedCharacter == 3)
                        {
                            currentMarkerState = MarkerState.MarkerState3;
                        }
                        if (lastMarkedCharacter == 4)
                        {
                            currentMarkerState = MarkerState.MarkerState4;
                        }
                    }
                    break;
                ///Markerstate8 = Minutes höger knapp
                case MarkerState.MarkerState8:
                    if (keyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A))
                    {
                        if (game1.currentChosenMinutes <= 8)
                        {
                            game1.currentChosenMinutes += 1;
                            minutesChosen += 1;
                        }

                    }
                    if (keyState.IsKeyDown(Keys.D) && previousKeyState.IsKeyUp(Keys.D) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState9;
                    }
                    if (keyState.IsKeyDown(Keys.A) && previousKeyState.IsKeyUp(Keys.A) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentMarkerState = MarkerState.MarkerState7;
                    }
                    if (keyState.IsKeyDown(Keys.S) && previousKeyState.IsKeyUp(Keys.S) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown))
                    {
                        if (lastMarkedCharacter == 1)
                        {
                            currentMarkerState = MarkerState.MarkerState1;
                        }
                        if (lastMarkedCharacter == 2)
                        {
                            currentMarkerState = MarkerState.MarkerState2;
                        }
                        if (lastMarkedCharacter == 3)
                        {
                            currentMarkerState = MarkerState.MarkerState3;
                        }
                        if (lastMarkedCharacter == 4)
                        {
                            currentMarkerState = MarkerState.MarkerState4;
                        }
                    }
                    break;
                ///Markerstate9 = Map vänster knapp
                case MarkerState.MarkerState9:
                    if (keyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        if (mapChosen > 1)
                        {
                            mapChosen -= 1;
                            game1.currentChosenMap -= 1;
                        }
                    }
                    if (keyState.IsKeyDown(Keys.D) && previousKeyState.IsKeyUp(Keys.D) || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight))
                    {
                        currentMarkerState = MarkerState.MarkerState10;
                    }
                    if (keyState.IsKeyDown(Keys.A) && previousKeyState.IsKeyUp(Keys.A) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentMarkerState = MarkerState.MarkerState8;
                    }
                    if (keyState.IsKeyDown(Keys.S) && previousKeyState.IsKeyUp(Keys.S) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown))
                    {
                        if (lastMarkedCharacter == 1)
                        {
                            currentMarkerState = MarkerState.MarkerState1;
                        }
                        if (lastMarkedCharacter == 2)
                        {
                            currentMarkerState = MarkerState.MarkerState2;
                        }
                        if (lastMarkedCharacter == 3)
                        {
                            currentMarkerState = MarkerState.MarkerState3;
                        }
                        if (lastMarkedCharacter == 4)
                        {
                            currentMarkerState = MarkerState.MarkerState4;
                        }
                    }
                    break;
                ///Markerstate10 = Map höger knapp
                case MarkerState.MarkerState10:
                    if (keyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        if (mapChosen <= 1)
                        {
                            mapChosen += 1;
                            game1.currentChosenMap += 1;
                        }
                    }
                    if (keyState.IsKeyDown(Keys.A) && previousKeyState.IsKeyUp(Keys.A) || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentMarkerState = MarkerState.MarkerState9;
                    }
                    if (keyState.IsKeyDown(Keys.S) && previousKeyState.IsKeyUp(Keys.S) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown))
                    {
                        if (lastMarkedCharacter == 1)
                        {
                            currentMarkerState = MarkerState.MarkerState1;
                        }
                        if (lastMarkedCharacter == 2)
                        {
                            currentMarkerState = MarkerState.MarkerState2;
                        }
                        if (lastMarkedCharacter == 3)
                        {
                            currentMarkerState = MarkerState.MarkerState3;
                        }
                        if (lastMarkedCharacter == 4)
                        {
                            currentMarkerState = MarkerState.MarkerState4;
                        }
                    }
                    break;
                ///Markerstate11 = Player 1 ready button
                case MarkerState.MarkerState11:
                    if (keyState.IsKeyDown(Keys.Space) && previousKeyState.IsKeyUp(Keys.Space) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A))
                    {
                        player1Ready = true;
                    }
                    if (keyState.IsKeyDown(Keys.W) && previousKeyState.IsKeyUp(Keys.W) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp))
                    {
                        if (lastMarkedCharacter == 1)
                        {
                            currentMarkerState = MarkerState.MarkerState1;
                        }
                        if (lastMarkedCharacter == 2)
                        {
                            currentMarkerState = MarkerState.MarkerState2;
                        }
                        if (lastMarkedCharacter == 3)
                        {
                            currentMarkerState = MarkerState.MarkerState3;
                        }
                        if (lastMarkedCharacter == 4)
                        {
                            currentMarkerState = MarkerState.MarkerState4;
                        }
                    }
                    break;
            }

            switch (currentSecondaryMarkerState)
            {
                ///Markerstate1 = Boxare
                case SecondaryMarkerState.SecondaryMarkerState1:
                    secondaryLastMarkedCharacter = 1;
                    player2MarkerRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 426;
                    player2MarkerRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 198;

                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        ///P2 väljer karaktär
                        game1.currentCharacterTwo = 1;
                        player2ChosenCharacter = 1;
                    }
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
                    {
                        currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
                    {
                        currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState5;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
                    {
                        currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState5;
                    }
                    break;
                ///Markerstate2 = Amerikansk fotbollsspelare
                case SecondaryMarkerState.SecondaryMarkerState2:
                    secondaryLastMarkedCharacter = 2;
                    player2MarkerRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 214;
                    player2MarkerRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 198;

                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        ///P2 väljer karaktär
                        game1.currentCharacterTwo = 2;
                        player2ChosenCharacter = 2;
                    }
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
                    {
                        currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState3;
                    }
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState1;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
                    {
                        currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState5;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
                    {
                        currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState5;
                    }
                    break;
                ///MarkerState3 = Curlare
                case SecondaryMarkerState.SecondaryMarkerState3:
                    secondaryLastMarkedCharacter = 3;
                    player2MarkerRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 - 2;
                    player2MarkerRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 198;

                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        ///P2 väljer karaktär
                        game1.currentCharacterTwo = 3;
                        player2ChosenCharacter = 3;
                    }
                    if (keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
                    {
                        currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState4;
                    }
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState2;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
                    {
                        currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState5;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
                    {
                        currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState5;
                    }
                    break;
                ///Markerstate4 = Baseballspelare
                case SecondaryMarkerState.SecondaryMarkerState4:
                    secondaryLastMarkedCharacter = 4;
                    player2MarkerRectangle.X = (int)ScreenManager.Instance.Dimensions.X / 2 + 210;
                    player2MarkerRectangle.Y = (int)ScreenManager.Instance.Dimensions.Y / 2 - 198;

                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        ///P2 väljer karaktär
                        game1.currentCharacterTwo = 4;
                        player2ChosenCharacter = 4;
                    }
                    if (keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
                    {
                        currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState3;
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
                    {
                        currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState5;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
                    {
                        currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState5;
                    }
                    break;
                ///Markerstate5 = Player 2 ready button
                case SecondaryMarkerState.SecondaryMarkerState5:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        ///P2 är redo
                        player2Ready = true;
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
                    {
                        if (secondaryLastMarkedCharacter == 1)
                        {
                            currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState1;
                        }
                        if (secondaryLastMarkedCharacter == 2)
                        {
                            currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState2;
                        }
                        if (secondaryLastMarkedCharacter == 3)
                        {
                            currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState3;
                        }
                        if (secondaryLastMarkedCharacter == 4)
                        {
                            currentSecondaryMarkerState = SecondaryMarkerState.SecondaryMarkerState4;
                        }
                    }
                    break;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, livesTextRectangle, livesTextSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, minutesTextRectangle, minutesTextSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, mapTextRectangle, mapTextSrcRectangle, Color.White);

            //spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, livesBoxRectangle, livesBoxSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, minutesBoxRectangle, minutesBoxSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, mapBoxRectangle, mapBoxSrcRectangle, Color.White);

            spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, charactersRectangle, charactersSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, ready1Rectangle, notReadySrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, ready2Rectangle, notReadySrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player1TextRectangle, player1TextSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player2TextRectangle, player2TextSrcRectangle, Color.White);


            spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player1Rectangle, playerSrcRectangle, Color.White);
            spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player2Rectangle, playerSrcRectangle, Color.White);

            if (player1Ready == true)
            {
                spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, ready1Rectangle, readySrcRectangle, Color.White);
            }
            if (player2Ready == true)
            {
                spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, ready2Rectangle, readySrcRectangle, Color.White);
            }

            if (player1ChosenCharacter == 1)
            {
                spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player1Rectangle, boxer1SrcRectangle, Color.White);
            }
            if (player2ChosenCharacter == 1)
            {
                spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player2Rectangle, boxer2SrcRectangle, Color.White);
            }
            if (player1ChosenCharacter == 2)
            {
                spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player1Rectangle, americanFootballer1SrcRectangle, Color.White);
            }
            if (player2ChosenCharacter == 2)
            {
                spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player2Rectangle, americanFootballer2SrcRectangle, Color.White);
            }
            if (player1ChosenCharacter == 3)
            {
                spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player1Rectangle, curler1SrcRectangle, Color.White);
            }
            if (player2ChosenCharacter == 3)
            {
                spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player2Rectangle, curler2SrcRectangle, Color.White);
            }
            if (player1ChosenCharacter == 4)
            {
                spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player1Rectangle, baseballer1SrcRectangle, Color.White);
            }
            if (player2ChosenCharacter == 4)
            {
                spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player2Rectangle, baseballer2SrcRectangle, Color.White);
            }

            switch (currentMarkerState)
            {
                case MarkerState.MarkerState1:
                    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player1MarkerRectangle, player1MarkerSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState2:
                    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player1MarkerRectangle, player1MarkerSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState3:
                    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player1MarkerRectangle, player1MarkerSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState4:
                    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player1MarkerRectangle, player1MarkerSrcRectangle, Color.White);
                    break;
                //case MarkerState.MarkerState5:
                //    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, livesBoxRectangle, smallBoxleftSrcRectangle, Color.White);
                //    break;
                //case MarkerState.MarkerState6:
                //    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, livesBoxRectangle, smallBoxRightSrcRectangle, Color.White);
                //    break;
                case MarkerState.MarkerState7:
                    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, minutesBoxRectangle, smallBoxleftSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState8:
                    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, minutesBoxRectangle, smallBoxRightSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState9:
                    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, mapBoxRectangle, bigBoxLeftSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState10:
                    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, mapBoxRectangle, bigBoxRightSrcRectangle, Color.White);
                    break;
                case MarkerState.MarkerState11:
                    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, ready1Rectangle, markedNotReadySrcRectangle, Color.White);
                    if (player1Ready == true)
                    {
                        spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, ready1Rectangle, markedReadySrcRectangle, Color.White);
                    }
                    break;
            }

            switch (currentSecondaryMarkerState)
            {
                case SecondaryMarkerState.SecondaryMarkerState1:
                    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player2MarkerRectangle, player2MarkerSrcRectangle, Color.White);
                    break;
                case SecondaryMarkerState.SecondaryMarkerState2:
                    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player2MarkerRectangle, player2MarkerSrcRectangle, Color.White);
                    break;
                case SecondaryMarkerState.SecondaryMarkerState3:
                    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player2MarkerRectangle, player2MarkerSrcRectangle, Color.White);
                    break;
                case SecondaryMarkerState.SecondaryMarkerState4:
                    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, player2MarkerRectangle, player2MarkerSrcRectangle, Color.White);
                    break;
                case SecondaryMarkerState.SecondaryMarkerState5:
                    spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, ready2Rectangle, markedNotReadySrcRectangle, Color.White);
                    if (player2Ready == true)
                    {
                        spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, ready2Rectangle, markedReadySrcRectangle, Color.White);
                    }
                    break;
            }

            if (mapChosen == 1)
            {
                spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, mapBoxRectangle, standardMapSrcRectangle, Color.White);
            }
            if (mapChosen == 2)
            {
                spriteBatch.Draw(AssetManager.Instance.characterSelectSpritesheet, mapBoxRectangle, spaceMapSrcRectangle, Color.White);
            }
            spriteBatch.DrawString(AssetManager.Instance.timerPixelFont, minutesChosen.ToString("0"), new Vector2(minutesBoxRectangle.X + 74, minutesBoxRectangle.Y - 6), Color.Black);
        }

        #endregion
    }
}
