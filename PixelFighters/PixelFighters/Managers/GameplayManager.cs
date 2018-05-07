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
    ///Används för att inte få in för mycket skit i Game1
    public class GameplayManager
    {
        #region Variables
        public Game1 game;
        ContentManager content;

        MouseState mouseState, previousMouseState;

        //public Vector2 startPosOne, startPosTwo;
        public Color color;
        //List<Platform> platforms = new List<Platform>();
        //Player p1, p2;

        public bool TimerStart = false;
        public float Timer = 10;
        public float TimerTic = 0;
        public bool TimerStock = false;
        public GameState currentGameState;

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
            game.IsMouseVisible = true;
            content = new ContentManager(Content.ServiceProvider, "Content");

            //startPosOne = new Vector2(165, 300);
            //startPosTwo = new Vector2(1175, 300);

            //platforms.Add(new Platform(AssetManager.Instance.rectTex, new Rectangle(0, 0, 50, 50)));
            //platforms.Add(new Platform(AssetManager.Instance.rectTex, new Rectangle(170, 600, 1000, 400)));
            //platforms.Add(new Platform(AssetManager.Instance.rectTex, new Rectangle(120, 400, 200, 50)));
            //platforms.Add(new Platform(AssetManager.Instance.rectTex, new Rectangle(1020, 400, 200, 50)));

            //p1 = new Player(AssetManager.Instance.boxManTex, startPosOne, new Rectangle(0, 0, 50, 50), 1);
            //p2 = new Player(AssetManager.Instance.boxManTex, startPosTwo, new Rectangle(0, 0, 50, 50), 2);

            color = new Color(0, 0, 0, 1f);

            StageManager.Instance.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            ///Match-timer
            TimerTic += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (TimerStart == true)
            {
                TimerTic += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Timer >= 0)
                {
                    Timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                // Den med mest stock vinner när tiden går ut
                if (Timer <= 0)
                {
                    if (StageManager.Instance.p1.stocksRemaining > StageManager.Instance.p2.stocksRemaining)
                    {
                        playerOneWon = true;
                    }
                    else if (StageManager.Instance.p2.stocksRemaining > StageManager.Instance.p1.stocksRemaining)
                    {
                        playerTwoWon = true;
                    }
                }
            }



            ///Platformskollisioner
            foreach (Platform p in StageManager.Instance.platforms)
            {
                if (StageManager.Instance.p1.IsTopColliding(p))
                {
                    StageManager.Instance.p1.HandleTopCollision(p);
                    break;
                }
                if (StageManager.Instance.p1.IsBottomColliding(p))
                {
                    StageManager.Instance.p1.HandleBottomCollision(p);
                    break;
                }
                else
                {
                    StageManager.Instance.p1.isOnGround = false;
                }
            }
            foreach (Platform p in StageManager.Instance.platforms)
            {
                if (StageManager.Instance.p2.IsTopColliding(p))
                {
                    StageManager.Instance.p2.HandleTopCollision(p);
                    break;
                }
                if (StageManager.Instance.p2.IsBottomColliding(p))
                {
                    StageManager.Instance.p2.HandleBottomCollision(p);
                    break;
                }
                else
                {
                    StageManager.Instance.p2.isOnGround = false;
                }
            }

            ///Stridskollisioner
            if (StageManager.Instance.p1.attackHitBox.Intersects(StageManager.Instance.p2.damageableHitBox) && StageManager.Instance.p1.isAttacking == true && !StageManager.Instance.p2.isInvincible)
            {
                StageManager.Instance.p2.HandlePlayerCollision(StageManager.Instance.p1, StageManager.Instance.p2);
                StageManager.Instance.p2.isHit = true;
                StageManager.Instance.p2.HP--;
            }
            if (StageManager.Instance.p2.attackHitBox.Intersects(StageManager.Instance.p1.damageableHitBox) && StageManager.Instance.p2.isAttacking == true && !StageManager.Instance.p1.isInvincible)
            {
                StageManager.Instance.p2.HandlePlayerCollision(StageManager.Instance.p1, StageManager.Instance.p2);
                StageManager.Instance.p1.isHit = true;
                StageManager.Instance.p1.HP--;
            }
            if (StageManager.Instance.p1.isDunking && StageManager.Instance.p2.isOnGround)
            {
                StageManager.Instance.p1.knockBackModifierY = 0;
            }
            if (StageManager.Instance.p2.isDunking && StageManager.Instance.p1.isOnGround)
            {
                StageManager.Instance.p2.knockBackModifierY = 0;
            }

            ///Konditioner för vinst
            if (StageManager.Instance.p1.stocksRemaining == -1)
            {
                playerTwoWon = true;
            }
            if (StageManager.Instance.p2.stocksRemaining == -1)
            {
                playerOneWon = true;
            }



            StageManager.Instance.p1.Update(gameTime);
            StageManager.Instance.p2.Update(gameTime);

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

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Platform p in StageManager.Instance.platforms)
            {
                p.Draw(spriteBatch);
            }

            StageManager.Instance.p1.Draw(spriteBatch);
            StageManager.Instance.p2.Draw(spriteBatch);

            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerOne HP: " + StageManager.Instance.p1.HP, new Vector2(0, 800), Color.Red);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerOne stocks: " + StageManager.Instance.p1.stocksRemaining, new Vector2(0, 825), Color.Red);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerTwo HP: " + StageManager.Instance.p2.HP, new Vector2(1200, 800), Color.Blue);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerTwo stocks: " + StageManager.Instance.p2.stocksRemaining, new Vector2(1200, 825), Color.Blue);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Time: " + Timer.ToString("0"), new Vector2(680, 100), Color.White);
            spriteBatch.Draw(AssetManager.Instance.fadeTex, new Rectangle(0, 0, (int)ScreenManager.Instance.Dimensions.X, (int)ScreenManager.Instance.Dimensions.Y), color);
        }
        #endregion
    }

}
