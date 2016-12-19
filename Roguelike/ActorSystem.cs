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
            var Actors = Map.AllActors;
            foreach (Actor A in Actors) {
                switch (A.Action) {
                    case ActorAction.MoveLeft:
                        Map.MoveActor(A, A.Position + new Point(-1, 0));
                        break;
                    case ActorAction.MoveRight:
                        Map.MoveActor(A, A.Position + new Point(1, 0));
                        break;
                    case ActorAction.MoveDown:
                        Map.MoveActor(A, A.Position + new Point(0, 1));
                        break;
                    case ActorAction.MoveUp:
                        Map.MoveActor(A, A.Position + new Point(0, -1));
                        break;
                    default:
                        break;
                }
                A.Action = ActorAction.None;
            }
        }
    }
}
