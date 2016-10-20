using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Roguelike.Components;

namespace Roguelike.Entities {
    class Entity {
        List<Component> Comps;

        public Entity() {
            Comps = new List<Component>();
        }

        public Entity(Component[] Comps) {
            this.Comps = new List<Component>(Comps);
        }

        public bool HasComponent<T>() where T : Component {
            foreach (Component C in Comps) {
                if (C.GetType() == typeof(T))
                    return true;
            }
            return false;
        }

        public T GetComponent<T>() where T : Component {
            foreach (Component C in Comps) {
                if (C.GetType() == typeof(T)) {
                    return C as T;
                }
            }
            return null;
        }

        public void AddComponent<T>(T  Comp) where T : Component {
            if (!HasComponent<T>())
                Comps.Add(Comp);
        }

    }
}
