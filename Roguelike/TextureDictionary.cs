using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Roguelike {
    class TextureDictionary {
        Dictionary<string, Texture2D> Textures;
        ContentManager Content;

        public TextureDictionary(ContentManager Content) {
            this.Content = Content;
            Textures = new Dictionary<string, Texture2D>();
        }

        public void LoadAllTextures() {
            DirectoryInfo info = new DirectoryInfo(Content.RootDirectory + "/textures");
            var files = info.EnumerateFiles("*.xnb");
            foreach (var file in files) {
                string noExtension = file.Name;
                noExtension = noExtension.Substring(0, noExtension.LastIndexOf('.'));
                Textures.Add(noExtension, Content.Load<Texture2D>("textures/" + noExtension));
            }
        }
    }
}
