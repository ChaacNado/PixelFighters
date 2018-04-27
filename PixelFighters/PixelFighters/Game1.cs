using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PixelFighters
{
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
            AssetManager.Instance.LoadContent(Content);
            GameplayManager.Instance.LoadContent(Content, this);
            MainMenu.Instance.LoadContent(Content, this);
            OptionsMenu.Instance.LoadContent(Content, this);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousKeyState = keyState;
            keyState = Keyboard.GetState();

            ///Kraven för att trigga övergångarna mellan olika GameStates
            switch (currentGameState)
            {
                case GameState.TitleScreen:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        currentGameState = GameState.MainMenu;
                    }
                    break;
                case GameState.MainMenu:
                    MainMenu.Instance.Update(gameTime, this);
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
                    GameplayManager.Instance.TimerStart = true;
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
                    GameplayManager.Instance.Timer = 10;
                    GameplayManager.Instance.TimerStart = false;
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter))
                    {
                        GameplayManager.Instance.playerOneWon = false;
                        GameplayManager.Instance.playerTwoWon = false;
                        
                        LoadContent();
                        currentGameState = GameState.MainMenu;
                    }
                    break;
                case GameState.Options:
                    OptionsMenu.Instance.Update(gameTime, this);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);

            switch (currentGameState)
            {
                case GameState.TitleScreen:
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PIXELFIGHTERS", new Vector2(240, 90), Color.Orange);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Press ENTER", new Vector2(240, 150), Color.White);
                    break;
                case GameState.MainMenu:
                    GraphicsDevice.Clear(new Color(203, 219, 252));
                    MainMenu.Instance.Draw(spriteBatch);
                    break;
                case GameState.CharacterSelect:
                    GraphicsDevice.Clear(Color.LightGray);
                    GameplayManager.Instance.Draw(spriteBatch);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Character Select", new Vector2(240, 90), Color.Black);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Press ENTER to proceed", new Vector2(240, 150), Color.Black);
                    break;
                case GameState.Playtime:
                    GraphicsDevice.Clear(Color.LightSlateGray);
                    GameplayManager.Instance.Draw(spriteBatch);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Press P to pause the game", new Vector2(240, 90), Color.White);
                    break;
                case GameState.Pause:
                    GraphicsDevice.Clear(Color.GreenYellow * 0.5f);
                    GameplayManager.Instance.Draw(spriteBatch);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PAUSE", new Vector2(630, 360), Color.HotPink);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Press ENTER to quit to main menu", new Vector2(540, 420), Color.HotPink);
                    break;
                case GameState.Results:
                    GraphicsDevice.Clear(Color.Brown);
                    if (GameplayManager.Instance.playerOneWon == true)
                    {
                        spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PLAYER ONE WON!", new Vector2(600, 450), Color.White);
                    }
                    if (GameplayManager.Instance.playerTwoWon == true)
                    {
                        spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PLAYER TWO WON!", new Vector2(600, 450), Color.White);
                    }
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Press ENTER to quit to main menu", new Vector2(560, 550), Color.White);

                    break;
                case GameState.Options:
                    GraphicsDevice.Clear(new Color(203, 219, 252));
                    OptionsMenu.Instance.Draw(spriteBatch);
                    break;
            }
           
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
