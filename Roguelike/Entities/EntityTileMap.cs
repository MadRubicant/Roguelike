using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Roguelike.Entities {
    public class EntityTileMap {
        int[,] TileGrid;
        Point Bounds;

        LinkedList<Entity>[,] EntityPositions;
        HashSet<Entity> AllActiveEntities;

        readonly Entity[] EmptyList = { };
        int ActiveLists;
        int InactiveLists;

        public Dictionary<int, TileDefinition> TileDefs { get; private set; }

        public IEnumerable<Entity> AllEntities {
            get { return AllActiveEntities; }
        }

        public EntityTileMap(int width, int height) {
            TileGrid = new int[width, height];
            Bounds = new Point(width, height);
            EntityPositions = new LinkedList<Entity>[width, height];
            AllActiveEntities = new HashSet<Entity>();
            ActiveLists = 0;
            InactiveLists = 0;
            TileDefs = new Dictionary<int, TileDefinition>();
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> containing all entities at the given tile
        /// </summary>
        /// <param name="Pos">The tile position to check</param>
        /// <returns>A list of entities</returns>
        public IEnumerable<Entity> EntitiesAt(Point Pos) {
            if (!InBounds(Pos))
                return EmptyList;
            if (EntityPositions[Pos.X, Pos.Y] == null)
                return EmptyList;
            return EntityPositions[Pos.X, Pos.Y];
        }

        /// <summary>
        /// Adds the given <see cref="Entity"/> to the world
        /// </summary>
        /// <param name="Ent">The <see cref="Entity"/> to add</param>
        public void AddEntity(Entity Ent) {
            Point Pos = Ent.Position;
            if (!InBounds(Pos))
                return;
            // TODO: Add logging for when an entity is attempted to be added out of bounds
            if (EntityPositions[Pos.X, Pos.Y] == null) {
                EntityPositions[Pos.X, Pos.Y] = new LinkedList<Entity>();
                ActiveLists++;
            }
            EntityPositions[Pos.X, Pos.Y].AddLast(Ent);
            AllActiveEntities.Add(Ent);
        }

        /// <summary>
        /// Removes the given <see cref="Entity"/> from the world
        /// </summary>
        /// <param name="Ent">The <see cref="Entity"/> to remove</param>
        public void RemoveEntity(Entity Ent) {
            Point Pos = Ent.Position;
            if (!InBounds(Pos))
                return;
            if (EntityPositions[Pos.X, Pos.Y] == null) {
                return;
            }

            EntityPositions[Pos.X, Pos.Y].Remove(Ent);
            AllActiveEntities.Remove(Ent);

            if (EntityPositions[Pos.X, Pos.Y].Count == 0)
                InactiveLists++;

            if ((float)ActiveLists / (float)InactiveLists < .5f)
                ClearInactive();
        }

        /// <summary>
        /// Moves the given <see cref="Entity"/> from its current position to <paramref name="Dest"/>. Does collision checking 
        /// </summary>
        /// <param name="E">The <see cref="Entity"/> to move</param>
        /// <param name="Dest">The destination</param>
        /// <returns>True if the move was successful, false otherwise</returns>
        public bool MoveEntity(Entity E, Point Dest) {

            if (!InBounds(Dest))
                return false;
            TileDefinition Def = TileDefs[this[Dest.X, Dest.Y]];
            if (Def.Blocks == true)
                return false;
            var Ents = EntitiesAt(E.Position);
            if (Ents.Contains(E))
                RemoveEntity(E);
            E.Position = Dest;
            AddEntity(E);
            return true;
        }

        /// <summary>
        /// Gets or sets the tile at <paramref name="x"/>, <paramref name="y"/>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
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


        private bool InBounds(Point Pos) {
            return (Pos.X >= 0 && Pos.X < Bounds.X && Pos.Y >= 0 && Pos.Y < Bounds.Y);                
        }
    }
}
