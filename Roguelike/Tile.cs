using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike {
    public struct Tile : IComparable<Tile>, IEquatable<Tile>{
        public short ID { get; }
        public byte Variant { get; }
        public Tile(short ID, byte Variant) {
            this.ID = ID;
            this.Variant = Variant;
        }

        public static bool operator ==(Tile left, Tile right) {
            return left.ID == right.ID;
        }

        public static bool operator !=(Tile left, Tile right) {
            return left.ID != right.ID;
        }

        public override bool Equals(object obj) {
            if (obj.GetType() == typeof(Tile)) {
                Tile other = (Tile)obj;
                return this == other;
            }
            return false;
        }

        public override int GetHashCode() {
            return ID.GetHashCode();
        }

        public int CompareTo(Tile other) {
            return ID - other.ID;
        }

        public bool Equals(Tile other) {
            return ID == other.ID;
        }
    }
}
