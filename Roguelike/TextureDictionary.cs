using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Roguelike.Extensions;

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
            LoadDirectory(info, "");
        }

        private void LoadDirectory(DirectoryInfo DirInfo, string RelativePath) {
            var files = DirInfo.EnumerateFiles("*.xnb");
            var dirs = DirInfo.EnumerateDirectories();
            if (RelativePath != "")
                RelativePath += '/';
            foreach (var dir in dirs) {
                LoadDirectory(dir, RelativePath + DirInfo.Name);
            }

            foreach (var file in files) {
                string noExtension = file.Name;
                noExtension = noExtension.Substring(0, noExtension.LastIndexOf('.'));
                Textures.Add(noExtension, Content.Load<Texture2D>(RelativePath + DirInfo.Name + "/" + noExtension));
            }
        }
        public Texture2D this[string Name]
        {
            get { return Textures[Name]; }
        }

        public void SplitTexture(string TexName) {
            Texture2D Tex = Textures[TexName];
            int count = 0;
            for (int i = 0; i < Tex.Height; i += 32) {
                for (int j = 0; j < Tex.Width; j += 32) {
                    Textures.Add(TexName + count.ToString(), Tex.SubSprite(new Microsoft.Xna.Framework.Rectangle(j,i, 32, 32)));
                    WriteTexture(TexName + count.ToString());
                    count++;
                }
            }
        }

        public void WriteTexture(string TexName) {
            Directory.CreateDirectory("dump");
            var stream = File.Create("dump/" + TexName + ".png" );
            Texture2D tex = Textures[TexName];
            tex.SaveAsPng(stream, tex.Width, tex.Height);
        }
    }
}
