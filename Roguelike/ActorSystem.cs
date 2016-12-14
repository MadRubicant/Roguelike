using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Roguelike.Entities;

namespace Roguelike {
    class ActorSystem { // I don't like this name
        public void ResolveMovement(GameWorld Map) {
            var Actors = Map.AllEntities;
            foreach (Actor A in Actors) {
                switch (A.Action) {
                    case ActorAction.MoveLeft:
                        Map.MoveEntity(A, A.Position + new Point(-1, 0));
                        break;
                    case ActorAction.MoveRight:
                        Map.MoveEntity(A, A.Position + new Point(1, 0));
                        break;
                    case ActorAction.MoveDown:
                        Map.MoveEntity(A, A.Position + new Point(0, 1));
                        break;
                    case ActorAction.MoveUp:
                        Map.MoveEntity(A, A.Position + new Point(0, -1));
                        break;
                    default:
                        break;
                }
                A.Action = ActorAction.None;
            }
        }
    }
}
