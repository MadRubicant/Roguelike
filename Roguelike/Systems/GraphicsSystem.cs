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
        public void Draw(GameWorld Map, SpriteBatch spriteBatch, Rectangle Camera) {
            Vector2 Offset = new Vector2(Camera.Width / 2, Camera.Height / 2);
            Vector2 CameraOffset = Camera.Location.ToVector2();
            for (int x = 0; x < Map.Bounds.X; x++) {
                for (int y = 0; y < Map.Bounds.Y; y++) {
                    Tile t = Map[x, y];
                    TileDefinition td = Map.TileDefs[t.ID];
                    Vector2 TileLocation = new Vector2(x, y) * TileScale + Offset - CameraOffset;
                    spriteBatch.Draw(td.Textures[t.Variant], TileLocation, Color.White);
                }
            }

            foreach (Actor A in Map.AllEntities) {
                Vector2 ActorLocation = A.Position.ToVector2() * TileScale + Offset - CameraOffset;
                spriteBatch.Draw(A.ActorSprite, ActorLocation, Color.White);
            }
        }
    }
}
