using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Roguelike.Entities {
    public class GameWorld {
        Tile[,] TileGrid;
        public readonly Point Bounds;

        LinkedList<Actor>[,] ActorPositions;
        HashSet<Actor> AllActiveActors;

        readonly Actor[] EmptyList = { };
        int ActiveLists;   
        int InactiveLists;

        public Dictionary<int, TileDefinition> TileDefs { get; private set; }

        public IEnumerable<Actor> AllActors {
            get { return AllActiveActors; }
        }

        public GameWorld(int width, int height) {
            TileGrid = new Tile[width, height];
            Bounds = new Point(width, height);
            ActorPositions = new LinkedList<Actor>[width, height];
            AllActiveActors = new HashSet<Actor>();
            ActiveLists = 0;
            InactiveLists = 0;
            TileDefs = new Dictionary<int, TileDefinition>();
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> containing all entities at the given tile
        /// </summary>
        /// <param name="Pos">The tile position to check</param>
        /// <returns>A list of entities</returns>
        public IEnumerable<Actor> ActorsAt(Point Pos) {
            if (!InBounds(Pos))
                return EmptyList;
            if (ActorPositions[Pos.X, Pos.Y] == null)
                return EmptyList;
            return ActorPositions[Pos.X, Pos.Y];
        }

        /// <summary>
        /// Adds the given <see cref="Entity"/> to the world
        /// </summary>
        /// <param name="Ent">The <see cref="Entity"/> to add</param>
        public void AddActor(Actor Ent) {
            Point Pos = Ent.Position;
            if (!InBounds(Pos))
                return;
            // TODO: Add logging for when an entity is attempted to be added out of bounds
            if (ActorPositions[Pos.X, Pos.Y] == null) {
                ActorPositions[Pos.X, Pos.Y] = new LinkedList<Actor>();
                ActiveLists++;
            }
            ActorPositions[Pos.X, Pos.Y].AddFirst(Ent);
            AllActiveActors.Add(Ent);
        }

        /// <summary>
        /// Removes the given <see cref="Entity"/> from the world
        /// </summary>
        /// <param name="Ent">The <see cref="Entity"/> to remove</param>
        public void RemoveActor(Actor Ent) {
            Point Pos = Ent.Position;
            if (!InBounds(Pos))
                return;
            if (ActorPositions[Pos.X, Pos.Y] == null) {
                return;
            }

            ActorPositions[Pos.X, Pos.Y].Remove(Ent);
            AllActiveActors.Remove(Ent);

            if (ActorPositions[Pos.X, Pos.Y].Count == 0)
                InactiveLists++;

            if ((float)ActiveLists / (float)InactiveLists < .5f)
                ClearInactive();
        }

        /// <summary>
        /// Moves the given <see cref="Actor"/> from its current position to <paramref name="Dest"/>. Does collision checking, may have
        /// unpredictable results if you pass an Actor not in the game world already
        /// </summary>
        /// <param name="E">The <see cref="Actor"/> to move</param>
        /// <param name="Dest">The destination</param>
        /// <returns>True if the move was successful, false otherwise</returns>
        public bool MoveActor(Actor E, Point Dest) {
            if (!CanMoveTo(Dest))
                return false;
            TileDefinition Def = TileDefs[this[Dest.X, Dest.Y].ID];
            if (Def.Blocks == true)
                return false;
            Point Pos = E.Position;
            // Remove the actor from its current linked list
            ActorPositions[Pos.X, Pos.Y].Remove(E);
            if (ActorPositions[Pos.X, Pos.Y].Count == 0)
                InactiveLists++;

            // Create a new linked list at Dest if we need one, then stick the actor in it
            if (ActorPositions[Dest.X, Dest.Y] == null) {
                ActorPositions[Dest.X, Dest.Y] = new LinkedList<Actor>();
                ActiveLists++;
            }
            ActorPositions[Dest.X, Dest.Y].AddFirst(E);
            E.Position = Dest;
            if ((float)ActiveLists / (float)InactiveLists < .5f)
                ClearInactive();

            return true;

        }

        /// <summary>
        /// Gets or sets the tile at <paramref name="x"/>, <paramref name="y"/>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile this[int x, int y]
        {
            get { return TileGrid[x, y]; }
            set { TileGrid[x, y] = value; }
        }

        private void ClearInactive() {
            for (int x = 0; x < ActorPositions.GetLength(0); x++) {
                for (int y = 0; y < ActorPositions.GetLength(1); y++) {
                    if (ActorPositions[x, y] != null && ActorPositions[x, y].Count == 0) {
                        ActorPositions[x, y] = null;
                        ActiveLists--;
                    }
                }
            }
            InactiveLists = 0;
        }

        private bool CanMoveTo(Point Pos) {
            if (!InBounds(Pos))
                return false;
            var actors = ActorsAt(Pos);
            foreach (Actor a in actors) {
                if (a.Blocks == true)
                    return false;
            }

            return true;

        }
        private bool InBounds(Point Pos) {
            return (Pos.X >= 0 && Pos.X < Bounds.X && Pos.Y >= 0 && Pos.Y < Bounds.Y);                
        }
    }
}
