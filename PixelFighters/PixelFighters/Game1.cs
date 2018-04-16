using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PixelFighters
{
    public enum GameState
    {
        TitleScreen,
        MainMenu,
        CharacterSelect,
        Options,
        Playtime,
        Pause,
        Results,
    }

    public class Game1 : Game
    {
        public GameState currentGameState;

        SpriteBatch spriteBatch;
        public GraphicsDeviceManager graphics;
        public KeyboardState keyState, previousKeyState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            currentGameState = GameState.TitleScreen;

            ///Här kan vi justera skärmstorleken
            ScreenManager.Instance.Dimensions = new Vector2(1360, 900);
            graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ///Detta används när man hämtar data från GameplayManager etc
            TextureManager.Instance.LoadContent(Content);
            GameplayManager.Instance.LoadContent(Content, this);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousKeyState = keyState;
            keyState = Keyboard.GetState();

            switch (currentGameState)
            {
                case GameState.TitleScreen:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        currentGameState = GameState.MainMenu;
                    }
                    break;
                case GameState.MainMenu:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        currentGameState = GameState.CharacterSelect;
                    }
                    break;
                case GameState.CharacterSelect:
                    GameplayManager.Instance.Update(gameTime);
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        currentGameState = GameState.Playtime;
                    }
                    break;
                case GameState.Playtime:
                    GameplayManager.Instance.Update(gameTime);
                    if (GameplayManager.Instance.playerOneWon==true || GameplayManager.Instance.playerTwoWon == true)
                    {
                        currentGameState = GameState.Results;
                    }

                    if (keyState.IsKeyDown(Keys.P) && previousKeyState.IsKeyUp(Keys.P))
                    {
                        currentGameState = GameState.Pause;
                    }
                    break;
                case GameState.Pause:
                    if (keyState.IsKeyDown(Keys.P) && previousKeyState.IsKeyUp(Keys.P))
                    {
                        currentGameState = GameState.Playtime;
                    }
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        LoadContent();
                        currentGameState = GameState.MainMenu;
                    }
                    break;
                case GameState.Results:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        GameplayManager.Instance.playerOneWon = false;
                        GameplayManager.Instance.playerTwoWon = false;
                        LoadContent();
                        currentGameState = GameState.MainMenu;
                    }
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (currentGameState)
            {
                case GameState.TitleScreen:
                    spriteBatch.DrawString(TextureManager.Instance.spriteFont, "PIXELFIGHTERS", new Vector2(240, 90), Color.Orange);
                    spriteBatch.DrawString(TextureManager.Instance.spriteFont, "Press ENTER", new Vector2(240, 150), Color.White);
                    break;
                case GameState.MainMenu:
                    spriteBatch.DrawString(TextureManager.Instance.spriteFont, "Main Menu", new Vector2(240, 90), Color.White);
                    spriteBatch.DrawString(TextureManager.Instance.spriteFont, "Press ENTER to proceed", new Vector2(240, 150), Color.White);
                    break;
                case GameState.CharacterSelect:
                    GraphicsDevice.Clear(Color.LightGray);
                    GameplayManager.Instance.Draw(spriteBatch);
                    spriteBatch.DrawString(TextureManager.Instance.spriteFont, "Character Select", new Vector2(240, 90), Color.Black);
                    spriteBatch.DrawString(TextureManager.Instance.spriteFont, "Press ENTER to proceed", new Vector2(240, 150), Color.Black);
                    break;
                case GameState.Playtime:
                    GraphicsDevice.Clear(Color.MidnightBlue);
                    GameplayManager.Instance.Draw(spriteBatch);
                    spriteBatch.DrawString(TextureManager.Instance.spriteFont, "Press P to pause the game", new Vector2(240, 90), Color.White);
                    break;
                case GameState.Pause:
                    GraphicsDevice.Clear(Color.GreenYellow);
                    spriteBatch.DrawString(TextureManager.Instance.spriteFont, "PAUSE", new Vector2(630, 360), Color.HotPink);
                    spriteBatch.DrawString(TextureManager.Instance.spriteFont, "Press ENTER to quit to main menu", new Vector2(540, 420), Color.HotPink);
                    break;
                case GameState.Results:
                    GraphicsDevice.Clear(Color.Brown);
                    if (GameplayManager.Instance.playerOneWon == true)
                    {
                        spriteBatch.DrawString(TextureManager.Instance.spriteFont, "PLAYER ONE WON!", new Vector2(600, 450), Color.White);
                    }
                    if (GameplayManager.Instance.playerTwoWon == true)
                    {
                        spriteBatch.DrawString(TextureManager.Instance.spriteFont, "PLAYER TWO WON!", new Vector2(600, 450), Color.White);
                    }
                    spriteBatch.DrawString(TextureManager.Instance.spriteFont, "Press ENTER to quit to main menu", new Vector2(560, 550), Color.White);

                    break;
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
