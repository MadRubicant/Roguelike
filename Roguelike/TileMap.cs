using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Roguelike.Entities;

using Microsoft.Xna.Framework;

namespace Roguelike {
    class TileMap {
        int[,] TileGrid;
        Point Bounds;
        LinkedList<Entity>[,] EntityPositions;
        readonly Entity[] EmptyList = { };
        int ActiveLists;
        int InactiveLists;
        public Dictionary<int, TileDefinition> TileDefs { get; private set; }

        public TileMap(int width, int height) {
            TileGrid = new int[width, height];
            Bounds = new Point(width, height);
            EntityPositions = new LinkedList<Entity>[width, height];
            ActiveLists = 0;
            InactiveLists = 0;
            TileDefs = new Dictionary<int, TileDefinition>();
        }

        public IEnumerable<Entity> EntitiesAt(Point Pos) {
            if (!InBounds(Pos)) ;
                return EmptyList;
            if (EntityPositions[Pos.X, Pos.Y] == null)
                return EmptyList;
            return EntityPositions[Pos.X, Pos.Y];
        }

        
        public void AddEntity(Entity Ent) {
            Point Pos = Ent.Position;
            if (!InBounds(Pos))
                return;
            // TODO: Add logging for when 
            if (EntityPositions[Pos.X, Pos.Y] == null) {
                EntityPositions[Pos.X, Pos.Y] = new LinkedList<Entity>();
                ActiveLists++;
            }
            EntityPositions[Pos.X, Pos.Y].AddLast(Ent);
        }

        public void RemoveEntity(Entity Ent) {
            Point Pos = Ent.Position;
            if (!InBounds(Pos))
                return;
            if (EntityPositions[Pos.X, Pos.Y] == null) {
                return;
            }
            EntityPositions[Pos.X, Pos.Y].Remove(Ent);
            if (EntityPositions[Pos.X, Pos.Y].Count == 0)
                InactiveLists++;

            if ((float)ActiveLists / (float)InactiveLists < .5f)
                ClearInactive();
        }

        public int this[int x, int y]
        {
            get { return TileGrid[x, y]; }
            set { TileGrid[x, y] = value; }
        }

        private void ClearInactive() {
            for (int x = 0; x < EntityPositions.GetLength(0); x++) {
                for (int y = 0; y < EntityPositions.GetLength(1); y++) {
                    if (EntityPositions[x, y] != null && EntityPositions[x, y].Count == 0) {
                        EntityPositions[x, y] = null;
                        ActiveLists--;
                    }
                }
            }
            InactiveLists = 0;
        }

        public void MoveEntity(Entity E, Point Dest) {
            if (!InBounds(Dest))
                return;
            TileDefinition Def = TileDefs[this[Dest.X, Dest.Y]];
            if (Def.Blocks == true)
                return;
            var Ents = EntitiesAt(E.Position);
            if (Ents.Contains(E))
                RemoveEntity(E);
            E.Position = Dest;
            AddEntity(E);
        }

        private bool InBounds(Point Pos) {
            return (Pos.X >= 0 && Pos.X < Bounds.X && Pos.Y >= 0 && Pos.Y < Bounds.Y);                
        }
    }
}
