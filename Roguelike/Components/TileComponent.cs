using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Roguelike.Entities;

namespace Roguelike.Components {
    class TileComponent : Component {
        public int TileID;
        public TileComponent(int ID) {
            this.TileID = ID;
        }
    }
}
