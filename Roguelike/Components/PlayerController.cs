using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roguelike.Entities;
using Roguelike.Systems;

using Microsoft.Xna.Framework.Input;

namespace Roguelike.Components {
    class PlayerController : Controller {
        Dictionary<Keys, string> KeyMappings;

        static Dictionary<Keys, string> DefaultMappings = new Dictionary<Keys, string>() 
        {   { Keys.Up, "move-up" },
            { Keys.Down, "move-down" },
            { Keys.Left, "move-left" },
            { Keys.Right, "move-right" } };

        public PlayerController() {
            KeyMappings = DefaultMappings;
        }

        public override void CreateAction(Entity E, EntityTileMap World) {
            var Control = E.GetComponent<ControllableComponent>();
            if (Control != null) {
                foreach (Keys K in KeyMappings.Keys) {
                    if (InputHandler.ButtonPressed(K))
                        Control.Intent = DefaultMappings[K];
                }
            }
        }
    }
}
