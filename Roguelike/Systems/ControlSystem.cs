using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Roguelike.Entities;
using Roguelike.Components;

namespace Roguelike.Systems {

    class ControlSystem : BaseSystem {
        List<SysMessage> Messages = new List<SysMessage>();

        public override void Receive(SysMessage Message) {
            Entity E = Message.Subject;
            string S = Message.Message;
            
        }

        public override void Update(EntityTileMap AllEntities, GameTime Time) {
            var Ents = AllEntities.AllEntities.Where(x => x.HasComponent<ControllableComponent>());
            foreach (Entity E in Ents) {
                var Comp = E.GetComponent<ControllableComponent>();
                string message = null;

                switch (Comp.Intent) {
                    case "move-up":
                        if (MoveEntity(E, new Point(0, -1), AllEntities))
                            message = "entity-moved";
                        break;
                    case "move-left":
                        if (MoveEntity(E, new Point(-1, 0), AllEntities))
                            message = "entity-moved";
                        break;
                    case "move-right":
                        if (MoveEntity(E, new Point(1, 0), AllEntities))
                            message = "entity-moved";
                        break;
                    case "move-down":
                        if (MoveEntity(E, new Point(0, 1), AllEntities))
                            message = "entity-moved";
                        break;
                }
                if (message != null) {
                    Messages.Add(new SysMessage(message, E));
                }
            }

            foreach (SysMessage M in Messages)
                BroadcastEvent(M);
            Messages.Clear();
        }

        private bool MoveEntity(Entity E, Point Distance, EntityTileMap AllEntities) {
            return (AllEntities.MoveEntity(E, E.Position + Distance));
        }
    }
}
