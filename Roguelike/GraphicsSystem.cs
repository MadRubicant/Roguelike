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
        public void Draw(IEnumerable<Entity> EntList, SpriteBatch spriteBatch, Microsoft.Xna.Framework.Rectangle Camera) {
            var VisibleObjects = from Ent in EntList
                                 where Ent.HasComponent<GraphicsComponent>() && Ent.HasComponent<PositionComponent>()
                                 orderby Ent.GetComponent<PositionComponent>().ZPos
                                 select Ent;

            foreach (Entity Ent in VisibleObjects) {
                var Graphics = Ent.GetComponent<GraphicsComponent>();
                var Position = Ent.GetComponent<PositionComponent>();

                spriteBatch.Draw(Graphics.Sprite, Position.Position.AsXNA(), Microsoft.Xna.Framework.Color.White);
            }           
        }
    }
}
