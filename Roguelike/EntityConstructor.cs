using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roguelike {
    public class EntityConstructor {
        XmlReader MainReader;
        public EntityConstructor() {
            //XmlReader.Create();
        }
    }

    public class MonsterDefinition {
        int ID;
        Texture2D Text;
        string GenusName;

    }
}
