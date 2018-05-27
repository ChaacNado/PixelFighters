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
        Color menuColor;
        BaseMenu titlescreenMenu, mainMenu, optionsMenu, storyMenu, creditsMenu, quitMenu, graphicsMenu, controlsMenu;
        public BaseMenu soundMenu;
        CharacterSelectMenu characterSelectMenu;
        PausedMenu pausedMenu;
        ResultScreenMenu resultScreenMenu;

        public int currentCharacterOne = 1, currentCharacterTwo = 1, currentChosenMap = 1, currentChosenMinutes = 5;

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

            ///Skärmens dimenssioner vid start
            ScreenManager.Instance.Dimensions = new Vector2(1366, 768);
            graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ///Detta används när man hämtar data från GameplayManager och AssetManager.
            GameplayManager.Instance.LoadContent(Content, this);
            AssetManager.Instance.LoadContent(Content);

            menuColor = new Color(203, 219, 252);
            titlescreenMenu = new TitlescreenMenu();
            mainMenu = new MainMenu();
            optionsMenu = new OptionsMenu();
            storyMenu = new StoryMenu();
            creditsMenu = new CreditsMenu();
            quitMenu = new QuitMenu();
            graphicsMenu = new GraphicsMenu();
            soundMenu = new SoundMenu();
            pausedMenu = new PausedMenu();
            resultScreenMenu = new ResultScreenMenu();
            characterSelectMenu = new CharacterSelectMenu();
            controlsMenu = new ControlsMenu();
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
            GameplayManager.Instance.stageNumber = currentChosenMap;
            GameplayManager.Instance.matchLength = currentChosenMinutes * 60;

            if(currentGameState != GameState.Playtime)
            {
                camera.inMenu = true;
            }
            
            ///Kraven för att trigga övergångarna mellan olika GameStates
            switch (currentGameState)
            {
                case GameState.TitleScreen:
                    camera.zoom = 1;
                    camera.cameraFocus = new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2);
                    if (soundMenu.IsMusicOn)
                    {
                        SoundManager.Instance.Play(AssetManager.Instance.menuSong);
                    }
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
                    characterSelectMenu.ResetMarkers();
                    GameplayManager.Instance.timer = GameplayManager.Instance.matchLength;
                    GameplayManager.Instance.timerStart = false;
                    if (soundMenu.IsMusicOn)
                    {
                        SoundManager.Instance.Play(AssetManager.Instance.menuSong);
                    }                  
                    break;
                case GameState.CharacterSelect:
                    GameplayManager.Instance.Update(gameTime, camera);
                    camera.cameraFocus = new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2);
                    if (soundMenu.IsMusicOn)
                    {
                        SoundManager.Instance.Play(AssetManager.Instance.menuSong);
                    }
                    if (characterSelectMenu.player1Ready == true && characterSelectMenu.player2Ready == true) 
                    {
                        SoundManager.Instance.Stop();
                        currentGameState = GameState.Playtime;
                        GameplayManager.Instance.ReadFile();
                        characterSelectMenu.player1Ready = false;
                        characterSelectMenu.player2Ready = false;                        
                    }
                    characterSelectMenu.Update(gameTime, this);
                    break;
                case GameState.Playtime:
                    camera.inMenu = false;
                    GameplayManager.Instance.Update(gameTime, camera);
                    GameplayManager.Instance.timerStart = true;
                    if (GameplayManager.Instance.stageNumber == 1)
                    {
                        if (soundMenu.IsMusicOn)
                        {
                            SoundManager.Instance.Play(AssetManager.Instance.stage1Song);
                        }
                    }
                    if (GameplayManager.Instance.stageNumber == 2)
                    {
                        if (soundMenu.IsMusicOn)
                        {
                            SoundManager.Instance.Play(AssetManager.Instance.stage2Song);
                        }
                    }
                    if (GameplayManager.Instance.playerOneWon == true || GameplayManager.Instance.playerTwoWon == true)
                    {
                        SoundManager.Instance.Stop();
                        currentGameState = GameState.Results;
                    }

                    if (keyState.IsKeyDown(Keys.D8) && previousKeyState.IsKeyUp(Keys.D8) || keyState.IsKeyDown(Keys.NumPad8) && previousKeyState.IsKeyUp(Keys.NumPad8)
                        || gamePadStateOne.IsButtonDown(Buttons.Start) && previousGamePadStateOne.IsButtonUp(Buttons.Start) || gamePadStateTwo.IsButtonDown(Buttons.Start) && previousGamePadStateTwo.IsButtonUp(Buttons.Start))
                    {
                        currentGameState = GameState.Paused;
                    }
                    break;
                case GameState.Paused:
                    pausedMenu.Update(gameTime, this, camera);
                    break;
                case GameState.Results:
                    camera.cameraFocus = new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2);
                    GameplayManager.Instance.timer = GameplayManager.Instance.matchLength;
                    GameplayManager.Instance.timerStart = false;
                    resultScreenMenu.Update(gameTime, this);

                    if (resultScreenMenu.playAgain == true && (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A)))
                    {
                        GameplayManager.Instance.playerOneWon = false;
                        GameplayManager.Instance.playerTwoWon = false;
                        currentCharacterOne = 1;
                        currentCharacterTwo = 1;
                        currentChosenMap = 1;
                        currentChosenMinutes = 3;
                        GameplayManager.Instance.ReadFile();
                        currentGameState = GameState.CharacterSelect;
                    }

                    if (resultScreenMenu.playAgain == false && (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter) || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A)))
                    {
                        GameplayManager.Instance.playerOneWon = false;
                        GameplayManager.Instance.playerTwoWon = false;
                        currentCharacterOne = 1;
                        currentCharacterTwo = 1;
                        currentChosenMap = 1;
                        currentChosenMinutes = 3;
                        GameplayManager.Instance.ReadFile();
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
                case GameState.SoundMusic:
                    if (soundMenu.IsMusicOn)
                    {
                        SoundManager.Instance.Play(AssetManager.Instance.menuSong);
                    }
                    camera.cameraFocus = new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2);
                    soundMenu.Update(gameTime, this);
                    break;
                case GameState.Controls:
                    camera.cameraFocus = new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2);
                    controlsMenu.Update(gameTime, this);
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
                    GraphicsDevice.Clear(menuColor);
                    titlescreenMenu.Draw(spriteBatch);
                    break;
                case GameState.MainMenu:
                    GraphicsDevice.Clear(menuColor);
                    mainMenu.Draw(spriteBatch);
                    break;
                case GameState.CharacterSelect:
                    GraphicsDevice.Clear(menuColor);
                    characterSelectMenu.Draw(spriteBatch);
                    break;
                case GameState.Playtime:
                    GraphicsDevice.Clear(Color.LightSlateGray);
                    GameplayManager.Instance.Draw(spriteBatch, camera);
                    break;
                case GameState.Paused:
                    GraphicsDevice.Clear(Color.LightSlateGray);
                    GameplayManager.Instance.Draw(spriteBatch, camera);
                    pausedMenu.Draw(spriteBatch);
                    break;
                case GameState.Results:
                    GraphicsDevice.Clear(menuColor);
                    resultScreenMenu.Draw(spriteBatch);
                    break;
                case GameState.Options:
                    GraphicsDevice.Clear(menuColor);
                    optionsMenu.Draw(spriteBatch);
                    break;
                case GameState.Story:
                    GraphicsDevice.Clear(menuColor);
                    storyMenu.Draw(spriteBatch);
                    break;
                case GameState.Credits:
                    GraphicsDevice.Clear(menuColor);
                    creditsMenu.Draw(spriteBatch);
                    break;
                case GameState.Quit:
                    GraphicsDevice.Clear(menuColor);
                    quitMenu.Draw(spriteBatch);
                    break;
                case GameState.Graphics:
                    GraphicsDevice.Clear(menuColor);
                    graphicsMenu.Draw(spriteBatch);
                    break;
                case GameState.SoundMusic:
                    GraphicsDevice.Clear(menuColor);
                    soundMenu.Draw(spriteBatch);
                    break;
                case GameState.Controls:
                    GraphicsDevice.Clear(menuColor);
                    controlsMenu.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
