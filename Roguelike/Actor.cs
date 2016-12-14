using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Roguelike.Entities;

namespace Roguelike {
    public enum ActorFaction : byte { None, Player, Monster }
    public enum ActorAction { None, MoveLeft, MoveRight, MoveUp, MoveDown,}
    public class Actor {
        public Point Position;
        public bool IsMobile;
        public ActorAction Action;
        public Texture2D ActorSprite;

        public Actor(Point Pos, Texture2D ActorSprite, bool Mobile) {
            this.Position = Pos;
            this.IsMobile = Mobile;
            this.ActorSprite = ActorSprite;
        }
    }
}
