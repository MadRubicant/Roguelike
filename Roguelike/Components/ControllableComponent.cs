using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Roguelike.Entities;

namespace Roguelike.Components {
    class ControllableComponent {
        Action<Entity> MoveLeft;
        Action<Entity> MoveUp;
        Action<Entity> MoveRight;
        Action<Entity> MoveDown;
    }
}
