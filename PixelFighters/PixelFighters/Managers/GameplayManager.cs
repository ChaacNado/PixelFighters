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
        SpriteFont Time;

        MouseState mouseState, previousMouseState;

        public Color color;
        List<Platform> platforms = new List<Platform>();
        Player playerOne, playerTwo;

        public bool TimerStart = false;
        public float Timer = 10;
        public float TimerTic = 0;
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

            platforms.Add(new Platform(AssetManager.Instance.rectTex, new Vector2(0, 0), new Rectangle(0, 0, 50, 50)));
            platforms.Add(new Platform(AssetManager.Instance.rectTex, new Vector2(170, 600), new Rectangle(170, 600, 1000, 400)));
            platforms.Add(new Platform(AssetManager.Instance.rectTex, new Vector2(120, 400), new Rectangle(120, 400, 200, 50)));
            platforms.Add(new Platform(AssetManager.Instance.rectTex, new Vector2(1020, 400), new Rectangle(1020, 400, 200, 50)));

            playerOne = new Player(AssetManager.Instance.boxManTex, new Vector2(140, 300), new Rectangle(0, 0, 50, 50), 1);
            playerTwo = new Player(AssetManager.Instance.boxManTex, new Vector2(1050, 300), new Rectangle(0, 0, 50, 50), 2);

            color = new Color(0, 0, 0, 1f);
            Time = Content.Load<SpriteFont>("Time");
        }

        public void Update(GameTime gameTime)
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            ///Match-timer
            TimerTic += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(TimerStart == true)
            {
                TimerTic += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Timer >= 0)
                {
                    Timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
         
            ///Platformskollisioner
            foreach (Platform p in platforms)
            {
                if (playerOne.IsTopColliding(p))
                {
                    playerOne.HandleTopCollision(p);
                    break;
                }
                if (playerOne.IsBottomColliding(p))
                {
                    playerOne.HandleBottomCollision(p);
                    break;
                }
                else
                {
                    playerOne.IsOnGround = false;
                }
            }
            foreach (Platform p in platforms)
            { 
                if (playerTwo.IsTopColliding(p))
                {
                    playerTwo.HandleTopCollision(p);
                    break;
                }
                if (playerTwo.IsBottomColliding(p))
                {
                    playerTwo.HandleBottomCollision(p);
                    break;
                }
                else
                {
                    playerTwo.IsOnGround = false;
                }
            }

            ///Stridskollisioner
            if (playerOne.attackhitBox.Intersects(playerTwo.damageableHitBox) && playerOne.isAttacking == true)
            {
                playerTwo.isHit = true;
                playerTwo.HP--;
                playerTwo.HandlePlayerCollision(playerOne, playerTwo);
            }
            if (playerTwo.attackhitBox.Intersects(playerOne.damageableHitBox) && playerTwo.isAttacking == true)
            {
                playerOne.isHit = true;
                playerOne.HP--;
                playerTwo.HandlePlayerCollision(playerOne, playerTwo);
            }

            ///Konditioner för vinst
            if (playerOne.stocksRemaining == -1)
            {
                playerTwoWon = true;
            }
            if (playerTwo.stocksRemaining == -1)
            {
                playerOneWon = true;
            }

            playerOne.Update(gameTime);
            playerTwo.Update(gameTime);

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
            foreach (Platform p in platforms)
            {
                p.Draw(spriteBatch);
            }
     
            playerOne.Draw(spriteBatch);
            playerTwo.Draw(spriteBatch);

            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerOne HP: " + playerOne.HP, new Vector2(0, 800),Color.Red);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerOne stocks: " + playerOne.stocksRemaining, new Vector2(0, 825), Color.Red);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerTwo HP: " + playerTwo.HP, new Vector2(1200, 800), Color.Blue);
            spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PlayerTwo stocks: " + playerTwo.stocksRemaining, new Vector2(1200, 825), Color.Blue);
            spriteBatch.DrawString(Time, Timer.ToString("0"), new Vector2(680, 100), Color.White);
            spriteBatch.Draw(AssetManager.Instance.fadeTex, new Rectangle(0, 0, (int)ScreenManager.Instance.Dimensions.X, (int)ScreenManager.Instance.Dimensions.Y), color);
        }
        #endregion
    }
    
}
