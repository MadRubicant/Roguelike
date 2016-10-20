using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

namespace Roguelike.Components {
    class GraphicsComponent : Component {
        public Texture2D Sprite;

        public GraphicsComponent(Texture2D Sprite) {
            this.Sprite = Sprite;
        }
    }
}
