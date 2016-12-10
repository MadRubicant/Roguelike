using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
namespace Roguelike {
    public class TileDefinition {
        public static Dictionary<int, TileDefinition> TileDefs = new Dictionary<int, TileDefinition>();
        public int ID;
        public string Name;
        public bool Blocks;
        public Texture2D Text;
    }
}
