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
    public class GameplayManager
    {
        #region Variables
        Game1 game1;

        public int stageNumber = 1;
        public Vector2 startPosOne, startPosTwo;
        public Player p1, p2;
        List<Player> players;
        List<Platform> platforms;
        List<string> strings;

        Color color;
        Random rnd;

        public bool timerStart = false;
        public float matchLength, timer;

        public bool playerOneWon, playerTwoWon;

        Rectangle srcRecOne, srcRecTwo, projectileSrcRec, spaceSrcRec;
        Rectangle timerBoxRect, p1HPbarRect, p1currentHPbarRect, p1Heart1Rect, p1Heart2Rect, p1Heart3Rect, p2HPbarRect, p2currentHPbarRect, p2Heart1Rect, p2Heart2Rect, p2Heart3Rect;
        Rectangle timerBoxSrc, hpBarSrc, currentHPbarSrc, redHeartSrc, p1ArrowSrc, p2ArrowSrc, spaceRec;

        private static GameplayManager instance;
        #endregion

        #region Properties
        ///Skapar bara en instans av klassen
        public static GameplayManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameplayManager();
                }
                return instance;
            }
        }
        #endregion

        #region Main Methods
        public void LoadContent(ContentManager Content, Game1 game1)
        {
            this.game1 = game1;

            timer = matchLength;

            spaceRec = new Rectangle(-1000, -800, 3840, 2160);
            spaceSrcRec = new Rectangle(940, -170, 3840, 2160);

            timerBoxRect = new Rectangle(0, 0, 140, 72);
            p1HPbarRect = new Rectangle(0, 0, 444, 72);
            p2HPbarRect = p1HPbarRect;
            p1currentHPbarRect = new Rectangle(0, 0, 400, 40);
            p2currentHPbarRect = p1currentHPbarRect;
            p1Heart1Rect = new Rectangle(0, 0, 56, 48);
            p1Heart2Rect = p1Heart1Rect;
            p1Heart3Rect = p1Heart1Rect;
            p2Heart1Rect = p1Heart1Rect;
            p2Heart2Rect = p1Heart1Rect;
            p2Heart3Rect = p1Heart1Rect;

            timerBoxSrc = new Rectangle(0, 54, 35, 18);
            hpBarSrc = new Rectangle(0, 15, 112, 19);
            currentHPbarSrc = new Rectangle(6, 38, 101, 11);
            redHeartSrc = new Rectangle(1, 1, 15, 13);
            p1ArrowSrc = new Rectangle(617, 0, 11, 13);
            p2ArrowSrc = new Rectangle(633, 0, 12, 13);

            color = new Color(0, 0, 0, 1f);
            rnd = new Random();

            stageNumber = 1;

            ///Här skapas player 1 och 2 objekten.
            p1 = new Player(AssetManager.Instance.characterSpriteSheet, AssetManager.Instance.rectTex, startPosOne, srcRecOne, projectileSrcRec, p1ArrowSrc, 1, game1, true);
            p2 = new Player(AssetManager.Instance.characterSpriteSheet, AssetManager.Instance.rectTex, startPosTwo, srcRecTwo, projectileSrcRec, p2ArrowSrc, 2, game1, false);
            srcRecOne = new Rectangle(0, 0, 0, 0);
            srcRecTwo = new Rectangle(0, 0, 0, 0);
            projectileSrcRec = new Rectangle(0, 0, 0, 0);
            players = new List<Player>
            {
                p1,
                p2
            };

            platforms = new List<Platform>();
            strings = new List<string>();

            ///Läser strängar från fil
            while (!AssetManager.Instance.streamReader.EndOfStream)
            {
                strings.Add(AssetManager.Instance.streamReader.ReadLine());
            }
            AssetManager.Instance.streamReader.Close();

            ///Delar upp olika delar av strängarna, både genom radindex och med hjälp av tecken
            for (int j = 0; j < strings.Count; j++)
            {
                string[] coordinates = strings[j].Split(';');
                for (int i = 0; i < coordinates.Length; i++)
                {
                    string[] xy = coordinates[i].Split(',');
                    try
                    {
                        int x = Convert.ToInt32(xy[0]);
                        int y = Convert.ToInt32(xy[1]);
                        Vector2 pos = new Vector2(x, y);

                        Rectangle rect = new Rectangle(0, 0, 0, 0);
                        if (xy.Length == 4)
                        {
                            int w = Convert.ToInt32(xy[2]);
                            int h = Convert.ToInt32(xy[3]);
                            rect = new Rectangle(x, y, w, h);
                        }
                        if (j == 0)
                        {
                            startPosOne = new Vector2(x, y);
                        }
                        if (j == 1)
                        {
                            startPosTwo = new Vector2(x, y);
                        }
                        if (j == 2)
                        {
                            platforms.Add(new Platform(AssetManager.Instance.rectTex, rect));
                        }

                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Input string is not a sequence of digits.");
                    }
                }
            }
        }

        public void Update(GameTime gameTime, Camera camera)
        {
            System.Diagnostics.Debug.WriteLine(p1.speed.Y);

            ///Ändrar source rektangeln beroende på vilken karaktär spelaren valt.
            srcRecOne.Width = p1.srcWidthModifier;
            srcRecOne.Height = p1.srcHeightModifier;
            srcRecTwo.Width = p2.srcWidthModifier;
            srcRecTwo.Height = p2.srcHeightModifier;

            if (game1.currentGameState == GameState.Playtime)
            {
                ///Uppdaterar kamerafokus
                camera.cameraFocus.X = ((p1.pos.X + p2.pos.X) / 2);
                camera.cameraFocus.Y = ((p1.pos.Y + p2.pos.Y) / 2);
            }

            ///Kollision mellan spelarna och platformar
            #region Platform Collision
            foreach (Platform platform in platforms)
            {
                if (p1.IsTopColliding(platform))
                {
                    p1.HandleTopCollision(platform);
                    break;
                }
                if (p1.IsBottomColliding(platform))
                {
                    p1.HandleBottomCollision(platform);
                    break;
                }
                else
                {
                    p1.isOnGround = false;
                }
            }
            foreach (Platform platform in platforms)
            {
                if (p2.IsTopColliding(platform))
                {
                    p2.HandleTopCollision(platform);
                    break;
                }
                if (p2.IsBottomColliding(platform))
                {
                    p2.HandleBottomCollision(platform);
                    break;
                }
                else
                {
                    p2.isOnGround = false;
                }
            }
            #endregion

            ///Kollision mellan spelarnas offensiva hitbox och motståndarens hitbox
            #region Combat Collision
            if (p1.attackHitBox.Intersects(p2.damageableHitBox) && p1.isAttacking == true && !p2.isInvincible)
            {
                p2.HandlePlayerCollision(p1, p2);
                p2.isHit = true;
                if (p2.hasTakenDamage == false)
                {
                    p2.currentHP -= p1.damageDealt;
                }
                p2.hasTakenDamage = true;
            }
            else if (!p1.isAttacking)
            {
                p2.isHit = false;
                p2.hasTakenDamage = false;
            }

            if (p1.isDunking && p2.isOnGround)
            {
                p1.knockBackModifierY = -p1.knockBackModifierY / 4;
            }

            if (p2.attackHitBox.Intersects(p1.damageableHitBox) && p2.isAttacking == true && !p1.isInvincible)
            {
                p2.HandlePlayerCollision(p1, p2);
                p1.isHit = true;
                if (p1.hasTakenDamage == false)
                {
                    p1.currentHP -= Instance.p2.damageDealt;
                }
                p1.hasTakenDamage = true;
            }
            else if (!p2.isAttacking)
            {
                p1.isHit = false;
                p1.hasTakenDamage = false;
            }

            if (p2.isDunking && p1.isOnGround)
            {
                p2.knockBackModifierY = -p2.knockBackModifierY / 4;
            }
            #endregion

            ///Konditioner för vinst
            #region Victory Conditions
            ///Slut på stocks
            if (p1.stocksRemaining <= 0)
            {
                playerTwoWon = true;
            }
            if (p2.stocksRemaining <= 0)
            {
                playerOneWon = true;
            }

            ///Match-timer
            if (timerStart == true)
            {
                if (timer >= 0)
                {
                    timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                ///Den med mest stock vinner när tiden går ut
                if (timer <= 0)
                {
                    if (p1.stocksRemaining > p2.stocksRemaining)
                    {
                        playerOneWon = true;
                    }
                    else if (p2.stocksRemaining > p1.stocksRemaining)
                    {
                        playerTwoWon = true;
                    }
                    ///Om båda har lika mycket stocks, avgörs matchen genom sudden Death
                    if (p1.stocksRemaining == p2.stocksRemaining)
                    {
                        p1.stocksRemaining = 1;
                        p2.stocksRemaining = 1;
                        p1.currentHP = 1;
                        p2.currentHP = 1;
                    }
                }
            }
            #endregion

            ///Uppdaterar positionerna på HUD-objekten
            #region HUD
            timerBoxRect.X = (int)camera.pos.X + (int)ScreenManager.Instance.Dimensions.X / 2 - timerBoxRect.Width / 2;
            timerBoxRect.Y = (int)camera.pos.Y + (int)ScreenManager.Instance.Dimensions.Y / 50;

            p1HPbarRect.X = (int)camera.pos.X + 10;
            p1HPbarRect.Y = (int)camera.pos.Y + (int)ScreenManager.Instance.Dimensions.Y - p1HPbarRect.Height - 10;
            p2HPbarRect.X = (int)camera.pos.X + (int)ScreenManager.Instance.Dimensions.X - p2HPbarRect.Width - 10;
            p2HPbarRect.Y = (int)camera.pos.Y + (int)ScreenManager.Instance.Dimensions.Y - p2HPbarRect.Height - 10;

            p1currentHPbarRect.X = p1HPbarRect.X + 24;
            p1currentHPbarRect.Y = p1HPbarRect.Y + 16;
            p1currentHPbarRect.Width = p1.currentHP * 8;

            p2currentHPbarRect.Width = p2.currentHP * 8;
            p2currentHPbarRect.X = (int)camera.pos.X + (int)ScreenManager.Instance.Dimensions.X - p2currentHPbarRect.Width - 30;
            p2currentHPbarRect.Y = (int)camera.pos.Y + (int)ScreenManager.Instance.Dimensions.Y - p2currentHPbarRect.Height - 26;

            p1Heart1Rect.X = p1HPbarRect.X;
            p1Heart1Rect.Y = p1HPbarRect.Y - p1HPbarRect.Height + 20;
            p1Heart2Rect.X = p1Heart1Rect.X + p1Heart2Rect.Width + 5;
            p1Heart2Rect.Y = p1Heart1Rect.Y;
            p1Heart3Rect.X = p1Heart2Rect.X + p1Heart3Rect.Width + 5;
            p1Heart3Rect.Y = p1Heart1Rect.Y;

            p2Heart1Rect.X = p2HPbarRect.X + p2HPbarRect.Width / 2 + 45;
            p2Heart1Rect.Y = p1Heart1Rect.Y;
            p2Heart2Rect.X = p2Heart1Rect.X + p1Heart2Rect.Width + 5;
            p2Heart2Rect.Y = p2Heart1Rect.Y;
            p2Heart3Rect.X = p2Heart2Rect.X + p1Heart3Rect.Width + 5;
            p2Heart3Rect.Y = p2Heart1Rect.Y;
            #endregion

            foreach (Player player in players)
            {
                player.Update(gameTime);
            }

            ///Styr in-och uttoning mellan skärmövergångar
            #region Fade
            if (color.A > 0)
            {
                color.A--;
            }
            if (color.A <= 0.0f)
            {
                color.A = 0;
            }
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            ///Ritar ut vilken stage som valts.
            if (stageNumber == 1)
            {
                spriteBatch.Draw(AssetManager.Instance.skyscraperBackgroundTex, new Vector2(-1000, -800), Color.White);
            }
            if (stageNumber == 2)
            {
                spriteBatch.Draw(AssetManager.Instance.spaceBackgroundTex, spaceRec, spaceSrcRec, Color.White);
            }

            ///Ritar ut platformarna ifrån en lista.
            foreach (Platform platform in platforms)
            {
                platform.Draw(spriteBatch);
            }

            ///Ritar ut spelarna ifrån en lista.
            foreach (Player player in players)
            {
                player.Draw(spriteBatch);
            }

            ///Utritning av HUD.
            spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, timerBoxRect, timerBoxSrc, Color.White);

            spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p1HPbarRect, hpBarSrc, Color.White);
            spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p2HPbarRect, hpBarSrc, Color.White);
            spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p1currentHPbarRect, currentHPbarSrc, Color.White);
            spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p2currentHPbarRect, currentHPbarSrc, Color.White);

            if (p1.stocksRemaining == 1)
            {
                spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p1Heart1Rect, redHeartSrc, Color.White);
            }
            if (p1.stocksRemaining == 2)
            {
                spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p1Heart1Rect, redHeartSrc, Color.White);
                spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p1Heart2Rect, redHeartSrc, Color.White);
            }
            if (p1.stocksRemaining == 3)
            {
                spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p1Heart1Rect, redHeartSrc, Color.White);
                spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p1Heart2Rect, redHeartSrc, Color.White);
                spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p1Heart3Rect, redHeartSrc, Color.White);
            }
            if (p2.stocksRemaining == 1)
            {
                spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p2Heart3Rect, redHeartSrc, Color.White);
            }
            if (p2.stocksRemaining == 2)
            {
                spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p2Heart3Rect, redHeartSrc, Color.White);
                spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p2Heart2Rect, redHeartSrc, Color.White);
            }
            if (p2.stocksRemaining == 3)
            {
                spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p2Heart1Rect, redHeartSrc, Color.White);
                spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p2Heart2Rect, redHeartSrc, Color.White);
                spriteBatch.Draw(AssetManager.Instance.playTimeHUDSpritesheet, p2Heart3Rect, redHeartSrc, Color.White);
            }

            //spriteBatch.DrawString(AssetManager.Instance.spriteFont, timer.ToString("0"), new Vector2(timerBoxRect.X + timerBoxRect.Width / 2 - 10, timerBoxRect.Y + timerBoxRect.Height / 2 - 10), Color.Black);

            spriteBatch.Draw(AssetManager.Instance.fadeTex, new Rectangle((int)camera.pos.X - (int)ScreenManager.Instance.Dimensions.X / 2, (int)camera.pos.Y - (int)ScreenManager.Instance.Dimensions.Y / 2, (int)ScreenManager.Instance.Dimensions.X * 2, (int)ScreenManager.Instance.Dimensions.Y * 2), color);

            if (timer > 99.5)
            {
                spriteBatch.DrawString(AssetManager.Instance.timerPixelFont, timer.ToString("0"), new Vector2(timerBoxRect.X + 12, timerBoxRect.Y - 6), Color.Black);
            }
            if (timer < 99.5 && timer > 9.5)
            {
                spriteBatch.DrawString(AssetManager.Instance.timerPixelFont, timer.ToString("0"), new Vector2(timerBoxRect.X + 32, timerBoxRect.Y + -6), Color.Black);
            }
            if (timer < 9.5 && timer > 0.5)
            {
                spriteBatch.DrawString(AssetManager.Instance.timerPixelFont, timer.ToString("0"), new Vector2(timerBoxRect.X + 52, timerBoxRect.Y + -6), new Color((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble()));
            }

            if (timer <= 0)
            {
                spriteBatch.DrawString(AssetManager.Instance.timerPixelFont, "Sudden Death", new Vector2((int)camera.pos.X + ScreenManager.Instance.Dimensions.X * 0.340f, (int)camera.pos.Y + ScreenManager.Instance.Dimensions.Y * 0.13f), Color.Red);
            }
        }
        #endregion
    }
}
