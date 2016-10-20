using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

using Microsoft.Xna.Framework.Graphics;

using Roguelike.Components;
namespace Roguelike.Entities {
    static class Prefabs {
        public static Entity Unit(Vector2 Position, Texture2D Sprite) {
            Entity Ent = new Entity();
            Ent.AddComponent(new PositionComponent(Position, 1));
            Ent.AddComponent(new GraphicsComponent(Sprite));
            return Ent;
        }
    }
}
