using Roguelike.Entities;
using Roguelike.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike.Components {
    public abstract class Controller {
        public abstract void CreateAction(Entity E, EntityTileMap World);
    }
}
