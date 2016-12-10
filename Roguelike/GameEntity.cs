using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Roguelike.Entities;

enum Faction : byte { None, Player, Monster }
namespace Roguelike {
    public class GameEntity {
        public Point Position;
        public bool IsStatic { get; private set; }
        int EntID;

    }
}
