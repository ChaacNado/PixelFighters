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
        Game1 game;

        public int stageNumber = 1;
        public Vector2 startPosOne, startPosTwo;
        Rectangle srcRecOne, srcRecTwo;
        public Player p1, p2;
        public List<Player> players;
        public List<Platform> platforms;
        private List<string> strings;

        MouseState mouseState, previousMouseState;
        public Color color;

        public bool timerStart = false, timerStock = false;
        public float matchLength = 60, timer;

        public bool playerOneWon, playerTwoWon;

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
        public void LoadContent(ContentManager Content, Game1 game)
        {
            this.game = game;

            timer = matchLength;

            color = new Color(0, 0, 0, 1f);

            stageNumber = 1;

            p1 = new Player(AssetManager.Instance.characterSpriteSheet, AssetManager.Instance.rectTex, startPosOne, srcRecOne, 1, game, true);
            p2 = new Player(AssetManager.Instance.characterSpriteSheet, AssetManager.Instance.rectTex, startPosTwo, srcRecTwo, 2, game, false);
            srcRecOne = new Rectangle(0, 0, 0, 0);
            srcRecTwo = new Rectangle(0, 0, 0, 0);
            players = new List<Player>
            {
                p1,
                p2
            };

            platforms = new List<Platform>();
            strings = new List<string>();

            while (!AssetManager.Instance.streamReader.EndOfStream)
            {
                strings.Add(AssetManager.Instance.streamReader.ReadLine());
            }
            AssetManager.Instance.streamReader.Close();

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
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            srcRecOne.Width = p1.srcWidthModifier;
            srcRecOne.Height = p1.srcHeightModifier;
            srcRecTwo.Width = p2.srcWidthModifier;
            srcRecTwo.Height = p2.srcHeightModifier;

            ///Kamera
            camera.cameraFocus.X = ((p1.pos.X + p2.pos.X) / 2);
            camera.cameraFocus.Y = ((p1.pos.Y + p2.pos.Y) / 2);

            #region Platform Collision
            foreach (Platform p in platforms)
            {
                if (p1.IsTopColliding(p))
                {
                    p1.HandleTopCollision(p);
                    break;
                }
                if (p1.IsBottomColliding(p))
                {
                    p1.HandleBottomCollision(p);
                    break;
                }
                else
                {
                    p1.isOnGround = false;
                }
            }
            foreach (Platform p in platforms)
            {
                if (p2.IsTopColliding(p))
                {
                    p2.HandleTopCollision(p);
                    break;
                }
                if (p2.IsBottomColliding(p))
                {
                    p2.HandleBottomCollision(p);
                    break;
                }
                else
                {
                    p2.isOnGround = false;
                }
            }
            #endregion

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
                p1.knockBackModifierY = 0;
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
                p2.knockBackModifierY = 0;
            }
            #endregion

            #region Victory Conditions
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
                    //Om båda har lika mycket stocks, sudden Death
                    if(p1.stocksRemaining == p2.stocksRemaining)
                    {

                        p1.stocksRemaining = 1;
                        p2.stocksRemaining = 1;
                        p1.currentHP = 1;
                        p2.currentHP = 1;

                    }
                }               
            }
            #endregion

            foreach (Player player in players)
            {
                player.Update(gameTime);
            }

            ///Styr faden mellan skärmövergångar
            if (color.A > 0)
            {
                color.A--;
            }
            if (color.A <= 0.0f)
            {
                color.A = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            foreach (Platform p in platforms)
            {
                p.Draw(spriteBatch);
            }

            foreach (Player player in players)
            {
                player.Draw(spriteBatch);
            }

            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerOne HP: " + p1.currentHP, new Vector2((int)camera.pos.X + (ScreenManager.Instance.Dimensions.X * 0.05f), (int)camera.pos.Y + (ScreenManager.Instance.Dimensions.Y * 0.90f)), Color.Red);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerOne stocks: " + p1.stocksRemaining, new Vector2((int)camera.pos.X + (ScreenManager.Instance.Dimensions.X * 0.05f), (int)camera.pos.Y + ScreenManager.Instance.Dimensions.Y * 0.85f), Color.Red);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerTwo HP: " + p2.currentHP, new Vector2((int)camera.pos.X + (ScreenManager.Instance.Dimensions.X * 0.85f), (int)camera.pos.Y + (ScreenManager.Instance.Dimensions.Y * 0.90f)), Color.Blue);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerTwo stocks: " + p2.stocksRemaining, new Vector2((int)camera.pos.X + (ScreenManager.Instance.Dimensions.X * 0.85f), (int)camera.pos.Y + ScreenManager.Instance.Dimensions.Y * 0.85f), Color.Blue);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Time: " + timer.ToString("0"), new Vector2((int)camera.pos.X + ScreenManager.Instance.Dimensions.X * 0.45f, (int)camera.pos.Y + ScreenManager.Instance.Dimensions.Y * 0.1f), Color.White);
            spriteBatch.Draw(AssetManager.Instance.fadeTex, new Rectangle((int)camera.pos.X - (int)ScreenManager.Instance.Dimensions.X / 2, (int)camera.pos.Y - (int)ScreenManager.Instance.Dimensions.Y / 2, (int)ScreenManager.Instance.Dimensions.X * 2, (int)ScreenManager.Instance.Dimensions.Y * 2), color);

            if (timer <= 0)
            {
                spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Sudden Death", new Vector2((int)camera.pos.X + ScreenManager.Instance.Dimensions.X * 0.435f, (int)camera.pos.Y + ScreenManager.Instance.Dimensions.Y * 0.13f), Color.Black);
            }
        }
        #endregion
    }
}
