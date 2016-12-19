using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;
using System;

using Roguelike.Entities;

namespace Roguelike {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameMain : Game {
        bool graphicsSettingsChanged = false;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D WallTexture;
        Texture2D FloorTexture;
        Texture2D PlayerTexture;
        MainRenderer Renderer;
        GameWorld Tiles;
        Point MapSize = new Point(25);
        Actor Player;
        TextureDictionary TextureDict;
        ActorSystem actorSystem = new ActorSystem();
        public GameMain() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            Renderer = new MainRenderer();
            Tiles = new GameWorld(25,25);
            this.Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowSizeChanged;
        }

        void WindowSizeChanged(object Sender, EventArgs E) {
            graphics.PreferredBackBufferHeight = Window.ClientBounds.Height;
            graphics.PreferredBackBufferWidth = Window.ClientBounds.Width;
            graphicsSettingsChanged = true;
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureDict = new TextureDictionary(Content);
            TextureDict.LoadAllTextures();
            //TextureDict.SplitTexture("tile");
            
            WallTexture = CreateTexture(Color.DarkSlateGray, new Point(32, 32));
            FloorTexture = CreateTexture(Color.LightGray, new Point(32, 32));
            PlayerTexture = CreateTexture(Color.Red, new Point(32, 32));
            for (int i = 0; i < 20; i++) {
                for (int j = 0; j < 20; j++) {
                    if (i == 0 || j == 0 || i == 19 || j == 19)
                        Tiles[i, j] = new Tile(0, 0);
                    else
                        Tiles[i, j] = new Tile(1, 0);
                }
            }
            // Temporary
            TileDefinition Floor = new TileDefinition();
            Floor.Blocks = false;
            Floor.ID = 1;
            Floor.Theme = "dungeon";
            Floor.Textures = new Texture2D[] { TextureDict["textures/tiles/dungeon/floor0"] };
            Tiles.TileDefs.Add(1, Floor);

            TileDefinition Wall = new TileDefinition();
            Wall.Blocks = true;
            Wall.ID = 0;
            Wall.Name = "wall";
            Wall.Theme = "dungeon";
            Wall.Textures = new Texture2D[] { TextureDict["textures/tiles/dungeon/wall0"] };
            Tiles.TileDefs.Add(0, Wall);

            Player = new Actor(new Point(1, 1), PlayerTexture, true);
            Tiles.AddEntity(Player);
            // TODO: use this.Content to load your game content here
            

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (graphicsSettingsChanged == true) {
                graphics.ApplyChanges();
                graphicsSettingsChanged = false;
            }
            InputHandler.GetInput();
            if (InputHandler.ButtonPressed(Keys.Escape))
                Exit();

            if (InputHandler.ButtonPressed(Keys.OemTilde))
                Console.WriteLine("Execution Halted"); // You should insert a breakpoint here
            if (InputHandler.ButtonPressed(Keys.Left))
                Player.Action = ActorAction.MoveLeft;
            else if (InputHandler.ButtonPressed(Keys.Right))
                Player.Action = ActorAction.MoveRight;
            else if (InputHandler.ButtonPressed(Keys.Down))
                Player.Action = ActorAction.MoveDown;
            else if (InputHandler.ButtonPressed(Keys.Up))
                Player.Action = ActorAction.MoveUp;
            // TODO: Add your update logic here

            actorSystem.ResolveMovement(Tiles);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            Rectangle camera = GraphicsDevice.Viewport.Bounds;
            camera.Location = new Point(Player.Position.X * 32, Player.Position.Y * 32);
            Renderer.Draw(Tiles, spriteBatch, camera);
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private Texture2D CreateTexture(Color color, Point Size) {
            Texture2D tex = new Texture2D(GraphicsDevice, Size.X, Size.Y);
            Color[] ColorArray = new Color[Size.X * Size.Y];
            for (int x = 0; x < Size.X; x++) {
                for (int y = 0; y < Size.Y; y++) {
                    ColorArray[x * Size.Y + y] = color;
                }
            }

            tex.SetData<Color>(ColorArray);
            return tex;
        }
    }
}
