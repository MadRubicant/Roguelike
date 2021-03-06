﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;
using System;

using Roguelike.Entities;
using Roguelike.Components;
using Roguelike.Systems;

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
        HashSet<Entity> AllEntities;
        EntityTileMap Tiles;
        Point MapSize = new Point(5, 5);
        Entity Player;
        SystemManager Manager;
        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            Renderer = new GraphicsSystem();
            Tiles = new EntityTileMap(5, 5);
            AllEntities = new HashSet<Entity>();
            Manager = new Systems.SystemManager();
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
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    if (i == 0 || j == 0 || i == 4 || j == 4)
                        Tiles[i, j] = 1;
                    else
                        Tiles[i, j] = 0;
                }
            }
            // Temporary
            TileDefinition Floor = new TileDefinition();
            Floor.Blocks = false;
            Floor.ID = 0;
            Floor.Name = "floor";
            Floor.Text = FloorTexture;
            Tiles.TileDefs.Add(0, Floor);

            TileDefinition Wall = new TileDefinition();
            Wall.Blocks = true;
            Wall.ID = 1;
            Wall.Name = "wall";
            Wall.Text = WallTexture;
            Tiles.TileDefs.Add(1, Wall);

            Player = Prefabs.Unit(new Point(1, 1), PlayerTexture);
            AllEntities.Add(Player);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Manager.Update
            if (InputHandler.ButtonPressed(Keys.OemTilde))
                Console.WriteLine("Execution Halted");
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
            for (int i = 0; i < MapSize.X; i++) {
                for (int j = 0; j < MapSize.Y; j++) {
                    spriteBatch.Draw(Tiles.TileDefs[Tiles[i, j]].Text, new Vector2(32 * i, 32 * j), Color.White);
                }
            }
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
