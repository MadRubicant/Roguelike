using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;
namespace Roguelike.Components {
    class PositionComponent : Component {
        public Vector2 Position;
        public byte ZPos;

        public PositionComponent(Vector2 Position, byte ZPos) {
            this.Position = Position;
            this.ZPos = ZPos;
        }
    }
}
