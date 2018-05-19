﻿using Microsoft.Xna.Framework;
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

        BaseMenu mainMenu, quitMenu, optionsMenu, graphicsMenu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            currentGameState = GameState.TitleScreen;
            GameplayManager.Instance.stageNumber = 1;

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
            GameplayManager.Instance.LoadContent(Content);
            mainMenu = new MainMenu();
            optionsMenu = new OptionsMenu();
            quitMenu = new QuitMenu();
            graphicsMenu = new GraphicsMenu();

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousKeyState = keyState;
            keyState = Keyboard.GetState();

            previousGamePadStateOne = gamePadStateOne;
            gamePadStateOne = GamePad.GetState(PlayerIndex.One);
            previousGamePadStateTwo = gamePadStateTwo;
            gamePadStateTwo = GamePad.GetState(PlayerIndex.Two);

            ///Kraven för att trigga övergångarna mellan olika GameStates
            switch (currentGameState)
            {
                case GameState.TitleScreen:
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter)
                        || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        currentGameState = GameState.MainMenu;
                    }
                    break;
                case GameState.MainMenu:
                    mainMenu.Update(gameTime, this);
                    GameplayManager.Instance.timer = GameplayManager.Instance.matchLength;
                    GameplayManager.Instance.timerStart = false;
                    GameplayManager.Instance.timerStock = false;
                    break;
                case GameState.CharacterSelect:
                    mainMenu.Update(gameTime, this);
                    GameplayManager.Instance.Update(gameTime);
                    if (keyState.IsKeyDown(Keys.D) && previousKeyState.IsKeyUp(Keys.D) || keyState.IsKeyDown(Keys.Right) && previousKeyState.IsKeyUp(Keys.Right)
                        || gamePadStateOne.IsButtonDown(Buttons.DPadRight) && previousGamePadStateOne.IsButtonUp(Buttons.DPadRight) || gamePadStateTwo.IsButtonDown(Buttons.DPadRight) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadRight))
                    {
                        if (GameplayManager.Instance.stageNumber <= 1)
                        {
                            GameplayManager.Instance.stageNumber += 1;
                        }  
                    }
                    if (keyState.IsKeyDown(Keys.A) && previousKeyState.IsKeyUp(Keys.A) || keyState.IsKeyDown(Keys.Left) && previousKeyState.IsKeyUp(Keys.Left)
                        || gamePadStateOne.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateOne.IsButtonUp(Buttons.DPadLeft) || gamePadStateTwo.IsButtonDown(Buttons.DPadLeft) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadLeft))
                    {
                        if (GameplayManager.Instance.stageNumber > 1)
                        {
                            GameplayManager.Instance.stageNumber -= 1;
                        }
                    }
                    if (keyState.IsKeyDown(Keys.S) && previousKeyState.IsKeyUp(Keys.S) || gamePadStateOne.IsButtonDown(Buttons.DPadDown) && previousGamePadStateOne.IsButtonUp(Buttons.DPadDown))
                    {
                        if (GameplayManager.Instance.p1.currentCharacter <= 2)
                        {
                            GameplayManager.Instance.p1.currentCharacter += 1;
                        }  
                    }
                    if (keyState.IsKeyDown(Keys.W) && previousKeyState.IsKeyUp(Keys.W) || gamePadStateOne.IsButtonDown(Buttons.DPadUp) && previousGamePadStateOne.IsButtonUp(Buttons.DPadUp))
                    {
                        if (GameplayManager.Instance.p1.currentCharacter > 1)
                        {
                            GameplayManager.Instance.p1.currentCharacter -= 1;
                        }
                    }
                    if (keyState.IsKeyDown(Keys.Down) && previousKeyState.IsKeyUp(Keys.Down) || gamePadStateTwo.IsButtonDown(Buttons.DPadDown) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadDown))
                    {
                        if (GameplayManager.Instance.p2.currentCharacter <= 2)
                        {
                            GameplayManager.Instance.p2.currentCharacter += 1;
                        }
                    }
                    if (keyState.IsKeyDown(Keys.Up) && previousKeyState.IsKeyUp(Keys.Up) || gamePadStateTwo.IsButtonDown(Buttons.DPadUp) && previousGamePadStateTwo.IsButtonUp(Buttons.DPadUp))
                    {
                        if (GameplayManager.Instance.p2.currentCharacter > 1)
                        {
                            GameplayManager.Instance.p2.currentCharacter -= 1;
                        }
                    }
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter)
                        || gamePadStateOne.IsButtonDown(Buttons.A) && previousGamePadStateOne.IsButtonUp(Buttons.A) || gamePadStateTwo.IsButtonDown(Buttons.A) && previousGamePadStateTwo.IsButtonUp(Buttons.A))
                    {
                        currentGameState = GameState.Playtime;
                        LoadContent();
                    }
                    if (keyState.IsKeyDown(Keys.Back) && previousKeyState.IsKeyUp(Keys.Back))
                    {
                        currentGameState = GameState.MainMenu;
                    }
                    break;
                case GameState.Playtime:
                    GameplayManager.Instance.Update(gameTime);
                    GameplayManager.Instance.timerStart = true;
                    if (GameplayManager.Instance.playerOneWon == true || GameplayManager.Instance.playerTwoWon == true)
                    {
                        currentGameState = GameState.Results;
                    }

                    if (keyState.IsKeyDown(Keys.D8) && previousKeyState.IsKeyUp(Keys.D8) || keyState.IsKeyDown(Keys.NumPad8) && previousKeyState.IsKeyUp(Keys.NumPad8)
                        || gamePadStateOne.IsButtonDown(Buttons.Start) && previousGamePadStateOne.IsButtonUp(Buttons.Start) || gamePadStateTwo.IsButtonDown(Buttons.Start) && previousGamePadStateTwo.IsButtonUp(Buttons.Start))
                    {
                        currentGameState = GameState.Pause;
                    }
                    break;
                case GameState.Pause:
                    if (keyState.IsKeyDown(Keys.D8) && previousKeyState.IsKeyUp(Keys.D8) || keyState.IsKeyDown(Keys.NumPad8) && previousKeyState.IsKeyUp(Keys.NumPad8)
                        || gamePadStateOne.IsButtonDown(Buttons.Start) && previousGamePadStateOne.IsButtonUp(Buttons.Start) || gamePadStateTwo.IsButtonDown(Buttons.Start) && previousGamePadStateTwo.IsButtonUp(Buttons.Start))
                    {
                        currentGameState = GameState.Playtime;
                    }
                    if (keyState.IsKeyDown(Keys.Enter) && previousKeyState.IsKeyUp(Keys.Enter)
                        || gamePadStateOne.IsButtonDown(Buttons.Back) && previousGamePadStateOne.IsButtonUp(Buttons.Back) || gamePadStateTwo.IsButtonDown(Buttons.Back) && previousGamePadStateTwo.IsButtonUp(Buttons.Back))
                    {
                        LoadContent();
                        currentGameState = GameState.MainMenu;
                    }
                    break;
                case GameState.Results:
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
                case GameState.Graphics:
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

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);

            switch (currentGameState)
            {
                case GameState.TitleScreen:
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PIXELFIGHTERS", new Vector2(240, 90), Color.Orange);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Press ENTER", new Vector2(240, 150), Color.White);
                    break;
                case GameState.MainMenu:
                    GraphicsDevice.Clear(new Color(203, 219, 252));
                    mainMenu.Draw(spriteBatch);
                    break;
                case GameState.CharacterSelect:
                    GraphicsDevice.Clear(Color.LightGray);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Character Select", new Vector2(240, 90), Color.Black);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Stage: " + GameplayManager.Instance.stageNumber + "", new Vector2(240, 150), Color.Black);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Character: " + GameplayManager.Instance.p1.characterName + "", new Vector2(240, 210), Color.Black);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Character: " + GameplayManager.Instance.p2.characterName + "", new Vector2(720, 210), Color.Black);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Press ENTER to proceed", new Vector2(240, 270), Color.Black);
                    break;
                case GameState.Playtime:
                    GraphicsDevice.Clear(Color.LightSlateGray);
                    GameplayManager.Instance.Draw(spriteBatch);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Press 8 to pause the game", new Vector2(240, 90), Color.White);
                    break;
                case GameState.Pause:
                    GraphicsDevice.Clear(Color.GreenYellow * 0.5f);
                    GameplayManager.Instance.Draw(spriteBatch);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "PAUSE", new Vector2(630, 360), Color.HotPink);
                    spriteBatch.DrawString(AssetManager.Instance.spriteFont, "Press ENTER to quit to main menu", new Vector2(540, 420), Color.HotPink);
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
                case GameState.Graphics:
                    GraphicsDevice.Clear(new Color(203, 219, 252));
                    graphicsMenu.Draw(spriteBatch);
                    break;
                case GameState.Quit:
                    GraphicsDevice.Clear(new Color(203, 219, 252));
                    quitMenu.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
