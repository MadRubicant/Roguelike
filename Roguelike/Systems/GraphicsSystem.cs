using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Roguelike.Entities;

namespace Roguelike {
    class MainRenderer {
        int TileScale = 32;
        public void DrawEntities(GameWorld Map, SpriteBatch spriteBatch, Rectangle Camera) {
                      
        }

        public void DrawTiles(GameWorld Map, SpriteBatch spriteBatch, Rectangle Camera) {
            
            for (int x = 0; x < Map.Bounds.X; x++) {
                for (int y = 0; y < Map.Bounds.Y; y++) {
                    Tile t = Map[x, y];
                    TileDefinition td = Map.TileDefs[t.ID];

                    spriteBatch.Draw(td.Text, new Vector2(x, y) * TileScale, Color.White);
                }
            }
        }
    }
}
