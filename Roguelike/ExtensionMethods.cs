using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roguelike.Extensions {
    static class ExtensionMethods {
        public static Texture2D SubSprite(this Texture2D Original, Rectangle Bounds) {
            if (!Original.Bounds.Contains(Bounds))
                Bounds = Original.Bounds;
            Color[] ColorData = new Color[Bounds.Width * Bounds.Height];
            Original.GetData<Color>(0, Bounds, ColorData, 0, ColorData.Length);
            Texture2D Final = new Texture2D(Original.GraphicsDevice, Bounds.Width, Bounds.Height);
            Final.SetData<Color>(ColorData);
            return Final;
        }
    }
}
