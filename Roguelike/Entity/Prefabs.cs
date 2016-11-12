using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Roguelike.Components;
namespace Roguelike.Entities {
    static class Prefabs {
        public static Entity Unit(Point Position, Texture2D Sprite) {
            Entity Ent = new Entity();
            Ent.Position = Position;
            Ent.ZPos = 1;
            Ent.AddComponent(new GraphicsComponent(Sprite));
            return Ent;
        }
    }
}
