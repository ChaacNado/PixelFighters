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
        public Color color;

        public bool timerStart = false, timerStock = false;
        public float matchLength = 60, timer, timerTic = 0;

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

            timer = matchLength;

            color = new Color(0, 0, 0, 1f);
        }

        public void Update(GameTime gameTime)
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            ///Match-timer
            timerTic += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timerStart == true)
            {
                timerTic += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timer >= 0)
                {
                    timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                ///Den med mest stock vinner när tiden går ut
                if (timer <= 0)
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
            else if (!StageManager.Instance.p1.isAttacking)
            {
                StageManager.Instance.p2.isHit = false;
            }

            if (StageManager.Instance.p1.isDunking && StageManager.Instance.p2.isOnGround)
            {
                StageManager.Instance.p1.knockBackModifierY = 0;
            }

            if (StageManager.Instance.p2.attackHitBox.Intersects(StageManager.Instance.p1.damageableHitBox) && StageManager.Instance.p2.isAttacking == true && !StageManager.Instance.p1.isInvincible)
            {
                StageManager.Instance.p2.HandlePlayerCollision(StageManager.Instance.p1, StageManager.Instance.p2);
                StageManager.Instance.p1.isHit = true;
                StageManager.Instance.p1.HP--;
            }
            else if(!StageManager.Instance.p2.isAttacking)
            {
                StageManager.Instance.p1.isHit = false;
            }

            if (StageManager.Instance.p2.isDunking && StageManager.Instance.p1.isOnGround)
            {
                StageManager.Instance.p2.knockBackModifierY = 0;
            }

            ///Konditioner för vinst
            if (StageManager.Instance.p1.stocksRemaining <= 0)
            {
                playerTwoWon = true;
            }
            if (StageManager.Instance.p2.stocksRemaining <= 0)
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

            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerOne HP: " + StageManager.Instance.p1.HP, new Vector2(0, 675), Color.Red);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerOne stocks: " + StageManager.Instance.p1.stocksRemaining, new Vector2(0, 700), Color.Red);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerTwo HP: " + StageManager.Instance.p2.HP, new Vector2(1200, 675), Color.Blue);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerTwo stocks: " + StageManager.Instance.p2.stocksRemaining, new Vector2(1200, 700), Color.Blue);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Time: " + timer.ToString("0"), new Vector2(680, 100), Color.White);
            spriteBatch.Draw(AssetManager.Instance.fadeTex, new Rectangle(0, 0, (int)ScreenManager.Instance.Dimensions.X, (int)ScreenManager.Instance.Dimensions.Y), color);
        }
        #endregion
    }

}
