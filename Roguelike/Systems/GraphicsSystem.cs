using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

using Roguelike.Entities;
using Roguelike.Components;

namespace Roguelike {
    class GraphicsSystem {
        int TileScale = 32;
        public void Draw(IEnumerable<Entity> EntList, SpriteBatch spriteBatch, Microsoft.Xna.Framework.Rectangle Camera) {
            var VisibleObjects = from Ent in EntList
                                 where Ent.HasComponent<GraphicsComponent>()
                                 orderby Ent.ZPos
                                 select Ent;

            foreach (Entity Ent in VisibleObjects) {
                var Graphics = Ent.GetComponent<GraphicsComponent>();

                spriteBatch.Draw(Graphics.Sprite, (Ent.Position.ToVector2() * TileScale), Microsoft.Xna.Framework.Color.White);
            }           
        }
    }
}
