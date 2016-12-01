using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Roguelike.Entities;

namespace Roguelike.Components {
    class ControllableComponent : Component {
        public string Intent;
        public ControllableComponent(string Intent) {
            this.Intent = Intent;
        }
    }
}
