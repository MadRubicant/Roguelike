using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;

using Roguelike.Entities;

namespace Roguelike {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D WallTexture;
        Texture2D FloorTexture;
        Texture2D PlayerTexture;
        GraphicsSystem Renderer;
        Entity[,] TileMap;
        HashSet<Entity> AllEntities;
        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            Renderer = new GraphicsSystem();
            TileMap = new Entity[5, 5];
            AllEntities = new HashSet<Entity>();
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
            WallTexture = CreateTexture(Color.DarkSlateGray, new Point(32, 32));
            FloorTexture = CreateTexture(Color.LightGray, new Point(32, 32));
            PlayerTexture = CreateTexture(Color.Red, new Point(32, 32));
            // TODO: use this.Content to load your game content here
            for (int x = 0; x < 5; x++) {
                for (int y = 0; y < 5; y++) {
                    if (x == 0 || x == 4 || y == 0 || y == 4)
                        TileMap[x, y] = Prefabs.Tile(1, new System.Numerics.Vector2(32 * x, 32 * y), WallTexture);
                    else
                        TileMap[x, y] = Prefabs.Tile(0, new System.Numerics.Vector2(32 * x, 32 * y), FloorTexture);
                    AllEntities.Add(TileMap[x, y]);
                }
            }

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            //spriteBatch.Draw(WallTexture, new Vector2(), Color.White);
            Renderer.Draw(AllEntities, spriteBatch, this.GraphicsDevice.Viewport.Bounds);
            // TODO: Add your drawing code here

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
