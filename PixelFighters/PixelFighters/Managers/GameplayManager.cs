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
        List<Platform> platforms;
        PlayerOne playerOne;
        PlayerTwo playerTwo;

        private static GameplayManager instance;
        #endregion

        #region Properties
        ///Skapar bara en instance av klassen
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

            platforms = new List<Platform>();
            platforms.Add(new Platform(TextureManager.Instance.rectTex, new Vector2(0, 0), new Rectangle(0, 0, 50, 50)));
            platforms.Add(new Platform(TextureManager.Instance.rectTex, new Vector2(170, 600), new Rectangle(170, 600, 1000, 400)));
            platforms.Add(new Platform(TextureManager.Instance.rectTex, new Vector2(120, 400), new Rectangle(120, 400, 200, 50)));
            platforms.Add(new Platform(TextureManager.Instance.rectTex, new Vector2(1020, 400), new Rectangle(1020, 400, 200, 50)));
            playerOne = new PlayerOne(TextureManager.Instance.boxManTex, new Vector2(140, 300), new Rectangle(0, 0, 50, 50));
            playerTwo = new PlayerTwo(TextureManager.Instance.boxManTex, new Vector2(1050, 300), new Rectangle(0, 0, 50, 50));

            color = new Color(0, 0, 0, 1f);
        }

        public void Update(GameTime gameTime)
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

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
                //v0.1.3 - Gjort så att karaktären faller när den inte vidrör en platform
                else
                {
                    playerOne.IsOnGround = false;
                }
            }
            foreach (Platform p in platforms)
            { 
                //v0.1.4 - Player Two collisions
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

            if (playerOne.hurtBox.Intersects(playerTwo.hitBox) && playerOne.testAttack == true)
            {
                playerTwo.HandlePlayerCollision(playerOne, playerTwo);
            }
            if (playerTwo.hurtBox.Intersects(playerOne.hitBox) && playerTwo.testAttack == true)
            {
                playerTwo.HandlePlayerCollision(playerOne, playerTwo);
            }

            playerOne.Update(gameTime);
            playerTwo.Update(gameTime);

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

            spriteBatch.Draw(TextureManager.Instance.fadeTex, new Rectangle(0, 0, (int)ScreenManager.Instance.Dimensions.X, (int)ScreenManager.Instance.Dimensions.Y), color);
        }
        #endregion
    }
}
