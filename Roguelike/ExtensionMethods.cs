using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    static class ExtensionMethods {
        public static Microsoft.Xna.Framework.Vector2 AsXNA(this System.Numerics.Vector2 Vector) {
            return new Microsoft.Xna.Framework.Vector2(Vector.X, Vector.Y);
        }

        public static System.Numerics.Vector2 AsNumerics(this Microsoft.Xna.Framework.Vector2 Vector) {
            return new System.Numerics.Vector2(Vector.X, Vector.Y);
        }
    }
}
