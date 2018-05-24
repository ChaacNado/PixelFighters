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
        string boxerStory, americanFootballerStory, curlerStory, baseballerStory;
        Rectangle characterTextRectangle, leftButtonRectangle, rightButtonRectangle, textRectangle, boxerRectangle, americanFootballerRectangle, curlerRectangle, baseballerRectangle;
        Rectangle theBoxerSrcRectangle, theAmericanFootballerSrcRectangle, theCurlerSrcRectangle, theBaseballerSrcRectangle, leftButtonSrcRectangle, rightButtonSrcRectangle, markedLeftButtonSrcRectangle, markedRightButtonSrcRectangle, textSrcRectangle;
        Rectangle boxerSrcRectangle, americanFootballerSrcRectangle, curlerSrcRectangle, baseballerSrcRectangle, hiddenBoxerSrcRectangle, hiddenAmericanFootballerSrcRectangle, hiddenCurlerSrcRectangle, hiddenBaseballerSrcRectangle;

        #endregion
        public StoryMenu()
        {
            characterPage = 1;

            #region Stories

            boxerStory = "Name: Phillip Albert Jackson@Age: 45@Background: Three years ago Phillip was a famous boxer who won many titles throughout@" +
                "his career. But an unfortunate accident happened right before his last match to become@" +
                "the greatest, he punched his opponent's chest so hard that it broke the opponents@" +
                "ribcage and the broken rib pierced his heart. Everyone became stunned in the audience@" +
                "as his opponent fell lifeless too the ground, most people would brush it of as a accident@ " +
                "but the smile on Phillips face was terrifying and showed no remorse on what he did.@" +
                "He just stood there looking at the body, he got a thrill. He wanted more, so he started@" +
                "punching the referee with everything he had, it took around 10 people to stop him.@" +
                "That day he lost all of his fame and titles. Because of his violent behavior that day he@" +
                "had to leave his boxing career behind. But that did not stop him from wanting to feel@" +
                "moreviolence in his life, he signed up for The Ultimate Battle of the Rejected Athletes@" +
                "to seek more violence.";

            americanFootballerStory = "Name: Mac Enchiz@Age: 20@Background: Mac was your regular person, he was happy with his life. Everything was@" +
                "fulfilled for him when he joined the school main rugby team. Mac was quite large for his@" +
                "age. During many game he could rush threw the defenders without any problems with his@" +
                "large build. Earning him the name mad bulldozer by other teams. Finals are up and Mac's@" +
                "team is losing by one touchdown, the match starts, they pass Mac one last time and he@" +
                "starts sprinting towards the goal. The defenders ran to try and stop him. They@" +
                "succeeded in making him drop the ball towards the ground, but he caught it again. He@" +
                "sprinted towards the touchdown zone and reach it, he looked back towards his@" +
                "teammates. Everyone was standing there, in shock and silence. Looking down at his feet@" +
                "he saw it, a head in a pool of blood, he could not apperhand what just happened. Later it@" +
                "was found out that Brain and consumed a lot of steroid during his life and was not@" +
                "allowed to join any teams anymore. All hope was lost until he got invited for The Ultimate@" +
                "Battle of the Rejected Athletes, wanting to play more he accepted it.";

            curlerStory = "Name: Aiden Fortin@Age: 29@Background: Known as the ghost of curling, Aiden was a very happy person. He was@" +
                "famous but easily forgotten as his presence wasn't very strong. The reason his@" +
                "presence is very weak was because of his childhood. During his childhood he got into a lot@" +
                "of trouble because he stood out to much. In highschool he started to learn how to hide@" +
                "his presence as it would trouble him in his studies to become the greatest curler. Now as@" +
                "a member of the national team and their greatest member, his dream came true. But it@" +
                "became boring after he won to many matches. So he quit and joined The Ultimate Battle@" +
                "of the Rejected Athletes and see if curling became more fun if thrown at other people.";

            baseballerStory = "Name: Rin Suzume@Age: 25@Background: Rin was your regular baseball player, she had a stable career, a healthy body@" +
                "and her teammates loved her as a leader, nothing could change her life. But not really,@" +
                "that was only what she saw, all of her teammates would call her the 'mistress of hell',@" +
                "she forced all the players to go through a hellish training everyday. The only reason her@" +
                "teammates and coach was turning a blind eye was that all of her hits were home runs.@" +
                "No one in the team could match her record on the home runs she makes and her fastball that@" +
                "was clocked at 125 m/s. Many of the teams was starting rumors that Rin was taking some@" +
                "kind of drug to improve her strength, but she came clean. One day a paparazzi saw Rin@" +
                "getting some kind of drink from a person in a black coat, he took many picture that night,@" +
                "next day it was all over the news. Her career quickly ended as her fame and fortune sank.@" +
                "Now as a rejected player her only choose was either quit her career or join the The@" +
                "Ultimate Battle of the Rejected Athletes. Her choice was simple as she had no choice@" +
                "because of a deal she made with the man clad in black.";

            boxerStory = boxerStory.Replace("@", " " + System.Environment.NewLine);
            americanFootballerStory = americanFootballerStory.Replace("@", " " + System.Environment.NewLine);
            curlerStory = curlerStory.Replace("@", " " + System.Environment.NewLine);
            baseballerStory = baseballerStory.Replace("@", " " + System.Environment.NewLine);
            #endregion

            characterTextRectangle = new Rectangle(0, 0, 832, 40);
            leftButtonRectangle = new Rectangle(0, 0, 32, 48);
            rightButtonRectangle = new Rectangle(0, 0, 32, 48);
            textRectangle = new Rectangle(0, 0, 836, 384);
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

            textSrcRectangle = new Rectangle(0, 203, 209, 96);
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
                spriteBatch.DrawString(AssetManager.Instance.storyPixelFont, boxerStory, new Vector2(ScreenManager.Instance.Dimensions.X / 2 - 400, ScreenManager.Instance.Dimensions.Y / 2 - 50), Color.White);

            }
            //Amerikansk fotbollsspelare
            if (characterPage == 2)
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, characterTextRectangle, theAmericanFootballerSrcRectangle, Color.White);
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, americanFootballerRectangle, americanFootballerSrcRectangle, Color.White);
                spriteBatch.DrawString(AssetManager.Instance.storyPixelFont, americanFootballerStory, new Vector2(ScreenManager.Instance.Dimensions.X / 2 - 400, ScreenManager.Instance.Dimensions.Y / 2 - 50), Color.White);

            }
            //Curlare
            if (characterPage == 3)
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, characterTextRectangle, theCurlerSrcRectangle, Color.White);
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, curlerRectangle, curlerSrcRectangle, Color.White);
                spriteBatch.DrawString(AssetManager.Instance.storyPixelFont, curlerStory, new Vector2(ScreenManager.Instance.Dimensions.X / 2 - 400, ScreenManager.Instance.Dimensions.Y / 2 - 50), Color.White);
            }
            //Baseballspelare
            if (characterPage == 4)
            {
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, characterTextRectangle, theBaseballerSrcRectangle, Color.White);
                spriteBatch.Draw(AssetManager.Instance.storyMenuSpritesheet, baseballerRectangle, baseballerSrcRectangle, Color.White);
                spriteBatch.DrawString(AssetManager.Instance.storyPixelFont, baseballerStory, new Vector2(ScreenManager.Instance.Dimensions.X / 2 - 400, ScreenManager.Instance.Dimensions.Y / 2 - 50), Color.White);
            }
        }

        #endregion
    }
}
