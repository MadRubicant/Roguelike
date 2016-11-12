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
        LinkedList<Entity>[,] EntityPositions;
        readonly Entity[] EmptyList = { };
        int ActiveLists;
        int InactiveLists;
        public TileMap(int width, int height) {
            TileGrid = new int[width, height];
            EntityPositions = new LinkedList<Entity>[width, height];
            ActiveLists = 0;
            InactiveLists = 0;
        }

        public IEnumerable<Entity> EntitiesAt(Point Pos) {
            if (EntityPositions[Pos.X, Pos.Y] == null)
                return EmptyList;
            return EntityPositions[Pos.X, Pos.Y];
        }

        
        public void AddEntityAt(Point Pos, Entity Ent) {
            if (EntityPositions[Pos.X, Pos.Y] == null) {
                EntityPositions[Pos.X, Pos.Y] = new LinkedList<Entity>();
                ActiveLists++;
            }
            EntityPositions[Pos.X, Pos.Y].AddLast(Ent);
        }

        public void RemoveEntityAt(Point Pos, Entity Ent) {
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

        public void MoveEntity(Entity E, Point Source, Point Delta) {
            var Ents = EntitiesAt(Source);
            if (!Ents.Contains(E))
                return;
            RemoveEntityAt(Source, E);
            AddEntityAt(Source + Delta, E);
        }
    }
}
