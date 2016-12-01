using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Roguelike.Entities;
namespace Roguelike.Systems {
    public delegate void SystemEventHandler(BaseSystem Sender, SysMessage Args);

    public abstract class BaseSystem {
        public abstract void Receive(SysMessage Message);
        public event SystemEventHandler Broadcast;

        protected void BroadcastEvent(SysMessage Args) {
            var Temp = Broadcast;
            if (Temp != null)
                Temp.Invoke(this, Args);
        }

        public abstract void Update(EntityTileMap AllEntities, GameTime Time);
    }

    public class SysMessage {
        public string Message;
        public Entity Subject;

        public SysMessage(string Message, Entity Subject) {
            this.Message = Message;
            this.Subject = Subject;
        }

    }

    
}
