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
        public GamePadState gamePadStateOne, previousGamePadStateOne, gamePadStateTwo, previousGamePadStateTwo;
        Camera camera;
        BaseMenu titlescreenMenu, mainMenu, optionsMenu, storyMenu, creditsMenu, quitMenu, graphicsMenu, pausedMenu;
        CharacterSelectMenu characterSelectMenu;

        public int currentCharacterOne = 1, currentCharacterTwo = 1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            currentGameState = GameState.TitleScreen;
            GameplayManager.Instance.stageNumber = 1;

            camera = new Camera(GraphicsDevice.Viewport);
            ///Här kan vi justera skärmstorleken
            ScreenManager.Instance.Dimensions = new Vector2(1366, 768);
            graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ///Detta används när man hämtar data från GameplayManager etc
            AssetManager.Instance.LoadContent(Content);

            GameplayManager.Instance.LoadContent(Content, this);
            titlescreenMenu = new TitlescreenMenu();
            mainMenu = new MainMenu();
            optionsMenu = new OptionsMenu();
            storyMenu = new StoryMenu();
            creditsMenu = new CreditsMenu();
            quitMenu = new QuitMenu();
            graphicsMenu = new GraphicsMenu();
            pausedMenu = new PausedMenu();
            characterSelectMenu = new CharacterSelectMenu();

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            camera.Update(gameTime);

            previousKeyState = keyState;
            keyState = Keyboard.GetState();

            previousGamePadStateOne = gamePadStateOne;
            gamePadStateOne = GamePad.GetState(PlayerIndex.One);
            previousGamePadStateTwo = gamePadStateTwo;
            gamePadStateTwo = GamePad.GetState(PlayerIndex.Two);

            GameplayManager.Instance.p1.currentCharacter = currentCharacterOne;
            GameplayManager.Instance.p2.currentCharacter = currentCharacterTwo;

            ///Kraven för att trigga övergångarna mellan olika GameStates
            switch (currentGameState)
            {
                case GameState.TitleScreen:
                    camera.zoom = 1;
                    camera.cameraFocus = new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2);
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter)
                        || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        currentGameState = GameState.MainMenu;
                    }
                    titlescreenMenu.Update(gameTime, this);
                    break;
                case GameState.MainMenu:
                    camera.zoom = 1;
                    camera.cameraFocus = new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2);
                    mainMenu.Update(gameTime, this);
                    GameplayManager.Instance.timer = GameplayManager.Instance.matchLength;
                    GameplayManager.Instance.timerStart = false;
                    GameplayManager.Instance.timerStock = false;
                    break;
                case GameState.CharacterSelect:
                    camera.inMenu = true;
                    GameplayManager.Instance.Update(gameTime, camera);
                    if (characterSelectMenu.player1Ready == true && characterSelectMenu.player2Ready == true) 
                    {
                        currentGameState = GameState.Playtime;
                        LoadContent();
                        characterSelectMenu.player1Ready = false;
                        characterSelectMenu.player2Ready = false;                        
                    }
                    characterSelectMenu.Update(gameTime, this);
                    break;
                case GameState.Playtime:
                    camera.inMenu = false;
                    GameplayManager.Instance.Update(gameTime, camera);
                    GameplayManager.Instance.timerStart = true;
                    if (GameplayManager.Instance.playerOneWon == true || GameplayManager.Instance.playerTwoWon == true)
                    {
                        currentGameState = GameState.Results;
                    }

                    if (keyState.IsKeyDown(Keys.D8) && previousKeyState.IsKeyUp(Keys.D8) || keyState.IsKeyDown(Keys.NumPad8) && previousKeyState.IsKeyUp(Keys.NumPad8)
                        || gamePadStateOne.IsButtonDown(Buttons.Start) && previousGamePadStateOne.IsButtonUp(Buttons.Start) || gamePadStateTwo.IsButtonDown(Buttons.Start) && previousGamePadStateTwo.IsButtonUp(Buttons.Start))
                    {
                        currentGameState = GameState.Paused;
                    }
                    break;
                case GameState.Paused:
                    camera.inMenu = true;
                    pausedMenu.Update(gameTime, this);
                    break;
                case GameState.Results:
                    camera.cameraFocus = new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2);
                    GameplayManager.Instance.timer = GameplayManager.Instance.matchLength;
                    GameplayManager.Instance.timerStart = false;
                    GameplayManager.Instance.timerStock = false;
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter)
                        || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {

                        GameplayManager.Instance.playerOneWon = false;
                        GameplayManager.Instance.playerTwoWon = false;

                        LoadContent();
                        currentGameState = GameState.MainMenu;
                    }
                    break;
                case GameState.Options:
                    optionsMenu.Update(gameTime, this);
                    break;
                case GameState.Story:
                    camera.cameraFocus = new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2);
                    storyMenu.Update(gameTime, this);
                    break;
                case GameState.Credits:
                    camera.cameraFocus = new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2);
                    creditsMenu.Update(gameTime, this);
                    break;
                case GameState.Graphics:
                    camera.cameraFocus = new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2);
                    graphicsMenu.Update(gameTime, this);
                    break;
                case GameState.Quit:
                    quitMenu.Update(gameTime, this);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.transform);

            switch (currentGameState)
            {
                case GameState.TitleScreen:
                    GraphicsDevice.Clear(new Color(203, 219, 252));
                    titlescreenMenu.Draw(spriteBatch);
                    //spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PIXELFIGHTERS", new Vector2(240, 90), Color.Orange);
                    //spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Press ENTER", new Vector2(240, 150), Color.White);
                    break;
                case GameState.MainMenu:
                    GraphicsDevice.Clear(new Color(203, 219, 252));
                    mainMenu.Draw(spriteBatch);
                    break;
                case GameState.CharacterSelect:
                    GraphicsDevice.Clear(new Color(203, 219, 252));
                    characterSelectMenu.Draw(spriteBatch);
                    break;
                case GameState.Playtime:
                    GraphicsDevice.Clear(Color.LightSlateGray);
                    GameplayManager.Instance.Draw(spriteBatch, camera);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Press 8 to pause the game", new Vector2(240, 90), Color.White);
                    break;
                case GameState.Paused:
                    GraphicsDevice.Clear(Color.LightSlateGray);
                    GameplayManager.Instance.Draw(spriteBatch, camera);
                    pausedMenu.Draw(spriteBatch);
                    break;
                case GameState.Results:
                    if (GameplayManager.Instance.playerOneWon == true)
                    {
                        GraphicsDevice.Clear(Color.Brown);
                        spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PLAYER ONE WON!", new Vector2(600, 350), Color.White);
                    }
                    if (GameplayManager.Instance.playerTwoWon == true)
                    {
                        GraphicsDevice.Clear(Color.RoyalBlue);
                        spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PLAYER TWO WON!", new Vector2(600, 350), Color.White);
                    }
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Press ENTER to quit to main menu", new Vector2(560, 400), Color.White);

                    break;
                case GameState.Options:
                    GraphicsDevice.Clear(new Color(203, 219, 252));
                    optionsMenu.Draw(spriteBatch);
                    break;
                case GameState.Story:
                    GraphicsDevice.Clear(new Color(203, 219, 252));
                    storyMenu.Draw(spriteBatch);
                    break;
                case GameState.Credits:
                    GraphicsDevice.Clear(new Color(203, 219, 252));
                    creditsMenu.Draw(spriteBatch);
                    break;
                case GameState.Quit:
                    GraphicsDevice.Clear(new Color(203, 219, 252));
                    quitMenu.Draw(spriteBatch);
                    break;
                case GameState.Graphics:
                    GraphicsDevice.Clear(new Color(203, 219, 252));
                    graphicsMenu.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
