using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.Systems {
    public class SystemManager {
        List<BaseSystem> ActiveSystems;
        
        public event SystemEventHandler Broadcast;

        public SystemManager() {
            ActiveSystems = new List<BaseSystem>();
        }

        public void RegisterSystem(BaseSystem Sys) {
            ActiveSystems.Add(Sys);
            Sys.Broadcast += this.DefaultSysEventHandler;
        }

        public void RemoveSystem(BaseSystem Sys) {
            Sys.Broadcast -= this.DefaultSysEventHandler;
            ActiveSystems.Remove(Sys);
        }

        public void DefaultSysEventHandler(BaseSystem Sender, SysMessage Message) {
            foreach (var Sys in ActiveSystems) {
                Sys.Receive(Message);
            }
            Broadcast.Invoke(Sender, Message);
        }

    }
}
